using BlazorApp.TechResourceManagement.Domain;
using BlazorApp.TechResourceManagement.Utils;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp.TechResourceManagement.Bussiness
{
    public class GestorRTRT
    {
        private AuthenticationStateProvider _authStateProvider { get; set; }
        private HttpClient _httpClient { get; set; }

        //Se injectan las dependencias del GestorRTRT
        public GestorRTRT(AuthenticationStateProvider authenticationStateProvider,
                          HttpClient httpClient)
        {
            _authStateProvider = authenticationStateProvider;
            _httpClient = httpClient;
        }
        public Usuario usuarioActual { get; set; }
        private TipoRecursoTecnologico? tipoRecursoSeleccionado { get; set; }
        private RecursoTecnologico recursoTecnologicoSeleccionado { get; set; }
        private CentroDeInvestigacion centroInvestigacionSeleccionado { get; set; }
        private IList<TipoRecursoTecnologico> tiposRecursosTecnologicos { get; set; }
        private PersonalCientifico personalCientifico { get; set; }
        private IList<CentroDeInvestigacion> centrosDeInvestigacion { get; set; }

        public async Task<IList<TipoRecursoTecnologico>> ReservarTurnoRecursoTecnologico()
        {
            //Metodo que retorna los tipos de Recursos tecnologicos
            tiposRecursosTecnologicos = await BuscarTipoRecursoTecnologico();
            return tiposRecursosTecnologicos;
        }
        public async Task<IList<TipoRecursoTecnologico>> BuscarTipoRecursoTecnologico()
        {
            //Obtengo todos los tipos de recursos
            try
            {
                var fileJson = await _httpClient.GetByteArrayAsync("fake-data/tipoRecursoTecnologico.json");

                IList<TipoRecursoTecnologico> tiposRecursos = JsonHelper.JsonReader<List<TipoRecursoTecnologico>>(fileJson);

                return tiposRecursos ?? new List<TipoRecursoTecnologico>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IList<dynamic>> TomarTipoRecurso(string tipoRecursoSeleccionado)
        {
            //Obtengo de todos los recursos el seleccionado
            this.tipoRecursoSeleccionado = tiposRecursosTecnologicos.Where(x => x.EsTipoActual(tipoRecursoSeleccionado)).FirstOrDefault();

            return await BuscarRecursoTecnologicoPorCI(this.tipoRecursoSeleccionado);
        }
        public async Task<IList<dynamic>> BuscarRecursoTecnologicoPorCI(TipoRecursoTecnologico? tipoRecursoSeleccionado)
        {
            try
            {
                //Limpio la lista de centros de investigacion a mostrar en la pantalla
                IList<dynamic> centrosDeInvestigacionFiltrados = new List<dynamic>();

                //Obtengo los centros de investigacion
                var fileJson = await _httpClient.GetByteArrayAsync("fake-data/centroDeInvestigacion.json");

                centrosDeInvestigacion = JsonHelper.JsonReader<List<CentroDeInvestigacion>>(fileJson);

                foreach (var ci in centrosDeInvestigacion)
                {
                    IList<RecursoTecnologico> recursosFiltrados = ci.MisRecursosTecnologicos();

                    if (tipoRecursoSeleccionado != null)
                    {
                        //Se buscan los recursos de ese tipo agrupados por Centros de Investigacion y que no es dado de baja
                        recursosFiltrados = recursosFiltrados.Where(recurso =>
                                                                        recurso.EsTipoSeleccionado(tipoRecursoSeleccionado) &&
                                                                       !recurso.EstaDadoBaja())
                                                             .ToList();
                    }
                    else
                    {
                        //Se buscan los recursos que no esten dados de baja agrupados por Centros de Investigacion
                        recursosFiltrados = recursosFiltrados.Where(recurso => !recurso.EstaDadoBaja())
                                                             .ToList();
                    }
                    IList<dynamic> ciRTAux = new List<dynamic>();

                    //Obtengo las marcas para hacer una dependencia hacia el modelo
                    var fileMarcasJson = await _httpClient.GetByteArrayAsync("fake-data/marca.json");

                    List<Marca> marcas = JsonHelper.JsonReader<List<Marca>>(fileMarcasJson);

                    //Recorro toda la lista de recursos para buscar la marca
                    recursosFiltrados.ToList().ForEach(async recurso =>
                    {
                        //Verificamos que el modelo pertenezca a su Marca implementando dependencia
                        //Obtengo la marca del modelo
                        Marca marcaDelRt = marcas.First(marca => recurso.MostrarModelo().EsTuMarca(marca));

                        ciRTAux.Add(new
                        {
                            Recurso = recurso.MostrarRT(),
                            MarcaDelRt = marcaDelRt.MostrarMarca()
                        });
                    });

                    //Agrego un centro de investigacion
                    centrosDeInvestigacionFiltrados.Add(new
                    {
                        CI = ci.MostrarCI(),
                        RecursosTecnologicos = ciRTAux
                    });
                }

                return centrosDeInvestigacionFiltrados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task ObtenerUsuarioActual()
        {
            //A traves del estado actual de la sesion busco el usuario actual logueado
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            usuarioActual = new Usuario(authState.User.Identity.Name, "**********");
        }

        public async Task<bool> ValidarPertenencia(string siglaCI)
        {
            //Obtengo el usuario actual
            await ObtenerUsuarioActual();

            var fileJson = await _httpClient.GetByteArrayAsync("fake-data/cientifico.json");

            //Obtengo todos los cientificos de los centros de investigaciones
            IList<PersonalCientifico> personalCientificos = JsonHelper.JsonReader<List<PersonalCientifico>>(fileJson);

            //Filtro dentro de todos los cientificos el usuario actual con su cientifico
            personalCientifico = personalCientificos.First(x => x.EsTuUsuario(usuarioActual));

            //Centro Investigacion Seleccionado
            centroInvestigacionSeleccionado = centrosDeInvestigacion.First(x => x.EsCIActual(siglaCI));

            //Verifico que sea el cientifico activo este dentro del CI selecciondo por el RecursoTecnologico
            return centroInvestigacionSeleccionado.EsCientificoDelCI(personalCientifico);
        }
        public DateTime GetFechaHoraActual()
        {
            return DateTime.Now;
        }
        public async Task<IList<Turno>> TomarRecursoTecnologico(long numeroRT, string siglaCI)
        {
            IList<Turno> turnos;
            bool esCientificoActivo = await ValidarPertenencia(siglaCI);

            //Obtener Turnos Corresponde al cientifico usuario logueado
            if (esCientificoActivo)
            {
                //RecursoTecnologico Seleccionado
                recursoTecnologicoSeleccionado = centroInvestigacionSeleccionado.MisRecursosTecnologicos().First(x => x.EsRecursoActual(numeroRT));
                turnos = recursoTecnologicoSeleccionado.MostrarMisTurnos(GetFechaHoraActual());
                return turnos.Any() ? turnos : null;
            }
            else
            {
                return null;
            }
        }

    }
}