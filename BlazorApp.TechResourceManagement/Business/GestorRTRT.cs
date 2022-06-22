using BlazorApp.TechResourceManagement.Domain;
using BlazorApp.TechResourceManagement.Utils;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace BlazorApp.TechResourceManagement.Bussiness
{
    public class GestorRTRT
    {
        private readonly IServiceProvider _serviceProvider;

        //Se injectan las dependencias del GestorRTRT
        public GestorRTRT(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        private Usuario usuarioActual { get; set; }
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
        private async Task<IList<TipoRecursoTecnologico>> BuscarTipoRecursoTecnologico()
        {
            //Obtengo todos los tipos de recursos
            try
            {
                IList<TipoRecursoTecnologico> tiposRecursos = await GetJsonAsync<List<TipoRecursoTecnologico>>("fake-data/tipoRecursoTecnologico.json");

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
        private async Task<IList<dynamic>> BuscarRecursoTecnologicoPorCI(TipoRecursoTecnologico? tipoRecursoSeleccionado)
        {
            try
            {
                //Limpio la lista de centros de investigacion a mostrar en la pantalla
                IList<dynamic> centrosDeInvestigacionFiltrados = new List<dynamic>();

                //Obtengo los centros de investigacion
                centrosDeInvestigacion = await GetJsonAsync<List<CentroDeInvestigacion>>("fake-data/centroDeInvestigacion.json");

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
                    IList<Marca> marcas = await GetJsonAsync<List<Marca>>("fake-data/marca.json");

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
        private async Task ObtenerUsuarioActual()
        {
            //A traves del estado actual de la sesion busco el usuario actual logueado
            using (var scope = _serviceProvider.CreateScope())
            {
                var _authStateProvider = scope.ServiceProvider.GetRequiredService<AuthenticationStateProvider>();
                // do something with context
                var authState = await _authStateProvider.GetAuthenticationStateAsync();
                usuarioActual = new Usuario(authState.User.Identity.Name, "**********");
            }
        }
        private async Task<bool> ValidarPertenencia(string siglaCI)
        {
            //Obtengo el usuario actual
            await ObtenerUsuarioActual();

            //Obtengo todos los cientificos de los centros de investigaciones
            IList<PersonalCientifico> personalCientificos = await GetJsonAsync<List<PersonalCientifico>>("fake-data/cientifico.json");

            //Filtro dentro de todos los cientificos el usuario actual con su cientifico
            personalCientifico = personalCientificos.First(x => x.EsTuUsuario(usuarioActual));

            //Centro Investigacion Seleccionado
            centroInvestigacionSeleccionado = centrosDeInvestigacion.First(x => x.EsCIActual(siglaCI));

            //Verifico que sea el cientifico activo este dentro del CI selecciondo por el RecursoTecnologico
            return centroInvestigacionSeleccionado.EsCientificoDelCI(personalCientifico);
        }
        private DateTime GetFechaHoraActual()
        {
            return DateTime.Now;
        }
        public async Task<IList<Turno>> TomarRecursoTecnologico(RecursoTecnologico RT, string siglaCI)
        {
            recursoTecnologicoSeleccionado = RT;
            IList<Turno> turnos;
            bool esCientificoActivo = await ValidarPertenencia(siglaCI);
            //Obtener Turnos Corresponde al cientifico usuario logueado
            if (esCientificoActivo)
            {
                //RecursoTecnologico Seleccionado
                BuscarTurnos(recursoTecnologicoSeleccionado, out turnos);
            }
            else
            {
                BuscarTurnosNOCi(recursoTecnologicoSeleccionado, out turnos);
            }
            return turnos.Any() ? turnos : null;
        }
        private void BuscarTurnos(RecursoTecnologico RT, out IList<Turno> turnos)
        {
            turnos = RT.MostrarMisTurnos(GetFechaHoraActual());
        }
        private void BuscarTurnosNOCi(RecursoTecnologico RT, out IList<Turno> turnos)
        {
            turnos = RT.MostrarMisTurnosNOCI(GetFechaHoraActual(),centroInvestigacionSeleccionado.TiempoAntelacionReserva());
        }
        public IDictionary<bool, Tuple<RecursoTecnologico, Turno>> TomarTurno(Turno turno)
        {
            bool disponibilidad = ValidarDisponibilidadTurno(turno);

            MostrarDatosReserva(disponibilidad, turno, out Dictionary<bool, Tuple<RecursoTecnologico, Turno>> dictionary);

            return dictionary;
        }
        private void MostrarDatosReserva(bool disponibilidad, Turno turno, out Dictionary<bool, Tuple<RecursoTecnologico, Turno>> dictionary)
        {
            dictionary = new Dictionary<bool, Tuple<RecursoTecnologico, Turno>>
            {
                { disponibilidad, new Tuple<RecursoTecnologico, Turno>(recursoTecnologicoSeleccionado.MostrarRT(), turno.MostrarTurno()) }
            };
        }
        private bool ValidarDisponibilidadTurno(Turno turno)
        {
            return turno.EstoyDisponible();
        }
        public IList<Turno> TomarConfirmacion(Turno turno)
        {
            RegistrarReserva(turno);
            return null;
        }
        public async void RegistrarReserva(Turno turno)
        {
            //Obtengo los centros de investigacion
            IList<Estado> estados = await GetJsonAsync<List<Estado>>("fake-data/estado.json");

            var reservado = estados.First(x => x.EsAmbitoTurno() && x.EsReservado());

            turno.Reservar(reservado);

            centrosDeInvestigacion.ToList().ForEach(ci =>
            {
                if (ci.EsCIActual(centroInvestigacionSeleccionado.Sigla))
                {
                    ci.MisRecursosTecnologicos().Remove(recursoTecnologicoSeleccionado);
                    recursoTecnologicoSeleccionado.MostrarMisTurnos(DateTime.Now).Remove(turno);
                    recursoTecnologicoSeleccionado.MostrarMisTurnos(DateTime.Now).Add(turno);
                    ci.MisRecursosTecnologicos().Add(recursoTecnologicoSeleccionado);
                }
            });
            //PostJsonAsync("fake-data/centroDeInvestigacion.json", centrosDeInvestigacion);
            //JsonHelper.JsonWriter("fake-data", "centroDeInvestigacion.json", JsonHelper.Serialize(centrosDeInvestigacion));
        }
        private async Task<T> GetJsonAsync<T>(string path) where T : new()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _httpClient = scope.ServiceProvider.GetRequiredService<HttpClient>();
                // do something with context
                var fileJson = await _httpClient.GetByteArrayAsync(path);
                T ret = JsonHelper.JsonReader<T>(fileJson);
                return ret;
            }

        }
    }
}