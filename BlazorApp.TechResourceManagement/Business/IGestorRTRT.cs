using BlazorApp.TechResourceManagement.Domain;
using System.Net.Http.Json;

namespace BlazorApp.TechResourceManagement.Bussiness
{
    public interface IGestorRTRT
    {
        public Task<IList<TipoRecursoTecnologico>> reservarTurnoRecursoTecnologico();
        public Task<IList<TipoRecursoTecnologico>> buscarTipoRecursoTecnologico();
        public Task<IList<RecursoTecnologico>> tomarTipoRecurso(string tipoRecursoSeleccionado);
        public Task<IList<RecursoTecnologico>> buscarRecursoTecnologico(TipoRecursoTecnologico? tipoRecursoSeleccionado);
        public Task<IList<Marca>> obtenerMarcas();
        public Task<Marca> obtenerMarcaDelModelo(Modelo modelo);


    }
}