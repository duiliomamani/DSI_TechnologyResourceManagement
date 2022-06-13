using BlazorApp.TechResourceManagement.Domain;
using BlazorApp.TechResourceManagement.Utils;

namespace BlazorApp.TechResourceManagement.Bussiness
{
    public class GestorRTRT : IGestorRTRT
    {
        internal virtual HttpClient _httpClient { get; set; }
        public Usuario usuarioActual { get; set; }
        private TipoRecursoTecnologico? tipoRecursoSeleccionado { get; set; }
        private IList<TipoRecursoTecnologico> tiposRecursosTecnologicos { get; set; }
        private IList<Marca> marcas { get; set; }

        public async Task<IList<TipoRecursoTecnologico>> reservarTurnoRecursoTecnologico()
        {
            tiposRecursosTecnologicos = await buscarTipoRecursoTecnologico();
            return tiposRecursosTecnologicos;
        }

        public async Task<IList<TipoRecursoTecnologico>> buscarTipoRecursoTecnologico()
        {
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

        public async Task<IList<RecursoTecnologico>> tomarTipoRecurso(string tipoRecursoSeleccionado)
        {
            this.tipoRecursoSeleccionado = tiposRecursosTecnologicos.Where(x => x.MostrarCategoria() == tipoRecursoSeleccionado).FirstOrDefault();
            
            return await buscarRecursoTecnologico(this.tipoRecursoSeleccionado);
        }
        public async Task<IList<RecursoTecnologico>> buscarRecursoTecnologico(TipoRecursoTecnologico? tipoRecursoSeleccionado)
        {
            try
            {
                var fileJson = await _httpClient.GetByteArrayAsync("fake-data/recursoTecnologico.json");

                IList<RecursoTecnologico> recursos = JsonHelper.JsonReader<List<RecursoTecnologico>>(fileJson);

                if(tipoRecursoSeleccionado != null)
                {
                    //Se buscan los recursos de ese tipo agrupados por Centros de Investigacion
                    recursos = recursos.Where(x => 
                                                    x.EsTipoSeleccionado(tipoRecursoSeleccionado) &&
                                                    !x.EstaDadoBaja())
                                       .ToList();


                    await obtenerMarcas();

                    recursos.ToList().ForEach(async x => {
                        Marca marcaDelRt = await obtenerMarcaDelModelo(x.GetModelo());
                        if(marcaDelRt != null)
                            x.SetMarca(marcaDelRt);
                    });
                }
                return recursos ?? new List<RecursoTecnologico>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IList<Marca>> obtenerMarcas()
        {
            var fileJson = await _httpClient.GetByteArrayAsync("fake-data/marca.json");

            marcas = JsonHelper.JsonReader<List<Marca>>(fileJson);

            return marcas;
        }

        public async Task<Marca> obtenerMarcaDelModelo(Modelo modelo)
        {

            foreach (var m in marcas)
            {
                if (modelo.EsTuMarca(m))
                {
                    return m;
                    break;
                }
            }
            return null;
        }
    }
}