using BlazorApp.TechResourceManagement.Domain;
using BlazorApp.TechResourceManagement.Utils;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;

namespace BlazorApp.TechResourceManagement.Bussiness
{
    public class GestorRTRT
    {
        private AuthenticationStateProvider _authStateProvider;
        private HttpClient _httpClient { get; set; }

        public GestorRTRT(AuthenticationStateProvider authenticationStateProvider, HttpClient httpClient)
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
        private IList<CentroDeInvestigacion> centrosDeInvestigacionFiltrados { get; set; } = new List<CentroDeInvestigacion>();
        private IList<Marca> marcas { get; set; }

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
        public async Task<IList<CentroDeInvestigacion>> TomarTipoRecurso(string tipoRecursoSeleccionado)
        {
            //Obtengo de todos los recursos el seleccionado
            this.tipoRecursoSeleccionado = tiposRecursosTecnologicos.Where(x => x.EsTipoActual(tipoRecursoSeleccionado)).FirstOrDefault();

            return await BuscarRecursoTecnologicoPorCI(this.tipoRecursoSeleccionado);
        }
        public async Task<IList<CentroDeInvestigacion>> BuscarRecursoTecnologicoPorCI(TipoRecursoTecnologico? tipoRecursoSeleccionado)
        {
            try
            {
                //Limpio la lista de centros de investigacion a mostrar en la pantalla
                centrosDeInvestigacionFiltrados.Clear();

                //Obtengo los centros de investigacion
                await ObtenerCIs();
                //Obtengo las marcas para hacer una dependencia hacia el recurso tecnologico
                await ObtenerMarcas();

                foreach (var ci in centrosDeInvestigacion)
                {
                    IList<RecursoTecnologico> recursosFiltrados = ci.MisRecursosTecnologicos();

                    if (tipoRecursoSeleccionado != null)
                    {
                        //Se buscan los recursos de ese tipo agrupados por Centros de Investigacion y que no es dado de baja
                        recursosFiltrados = recursosFiltrados.Where(x =>
                                                                        x.EsTipoSeleccionado(tipoRecursoSeleccionado) &&
                                                                       !x.EstaDadoBaja())
                                                             .ToList();
                    }
                    else
                    {
                        //Se buscan los recursos que no esten dados de baja agrupados por Centros de Investigacion
                        recursosFiltrados = recursosFiltrados.Where(x => !x.EstaDadoBaja())
                                                             .ToList();
                    }

                    //Recorro toda la lista de recursos para asignarle la marca
                    recursosFiltrados.ToList().ForEach(async x =>
                    {
                        //Obtengo la marca del modelo
                        Marca? marcaDelRt = await ObtenerMarcaDelModelo(x.GetModelo());
                        if (marcaDelRt != null)
                            x.SetMarca(marcaDelRt);
                    });

                    //Setteo los recursos tecnologicos filtrados a mostrar
                    ci.SetRecusosTecnologicos(recursosFiltrados);

                    //Agrego un centro de investigacion
                    centrosDeInvestigacionFiltrados.Add(ci);
                }

                return centrosDeInvestigacionFiltrados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Marca?> ObtenerMarcaDelModelo(Modelo modelo)
        {
            //Verificamos que el modelo pertenezca a su Marca implementando dependencia
            foreach (var marca in marcas)
            {
                if (modelo.EsTuMarca(marca))
                {
                    return marca;
                }
            }
            return null;
        }
        public async Task ObtenerMarcas()
        {
            var fileJson = await _httpClient.GetByteArrayAsync("fake-data/marca.json");

            marcas = JsonHelper.JsonReader<List<Marca>>(fileJson);
        }
        public async Task ObtenerCIs()
        {
            var fileJson = await _httpClient.GetByteArrayAsync("fake-data/centroDeInvestigacion.json");

            centrosDeInvestigacion = JsonHelper.JsonReader<List<CentroDeInvestigacion>>(fileJson);
        }
        public async Task GetUsuarioActual()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            usuarioActual = new Usuario(authState.User.Identity.Name, "**********");
        }
        public async Task ObtenerCientificoActual()
        {
            var fileJson = await _httpClient.GetByteArrayAsync("fake-data/cientifico.json");

            IList<PersonalCientifico> personalCientificos = JsonHelper.JsonReader<List<PersonalCientifico>>(fileJson);


            personalCientifico = personalCientificos.Where(x => x.EsTuUsuario(usuarioActual)).FirstOrDefault();
        }
        public async Task<List<Turno>> ObtenerTurnosRT(long numeroRT, string siglaCI)
        {
            //Centro Investigacion Seleccionado
            centroInvestigacionSeleccionado = centrosDeInvestigacionFiltrados.Where(x => x.EsCIActual(siglaCI)).First();

            //RecursoTecnologico Seleccionado
            recursoTecnologicoSeleccionado = centroInvestigacionSeleccionado.MisRecursosTecnologicos().Where(x => x.EsRecursoActual(numeroRT)).First();

            await ObtenerCientificoActual();

            var cientificos = centroInvestigacionSeleccionado.MisCientificos();

            //Verifico que sea el cientifico activo este dentro del CI selecciondo por el RecursoTecnologico
            bool esCientificoActivo = cientificos.Any(x => x.EsCientificoActivo(personalCientifico));

            if (esCientificoActivo)
            {
                //Obtener Turnos
                return new List<Turno>();
            }
            else
            {
                return null;
            }
        }

    }
}