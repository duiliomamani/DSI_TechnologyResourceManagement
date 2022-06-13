using BlazorApp.TechResourceManagement.Domain;
using System.Net.Http.Json;

namespace BlazorApp.TechResourceManagement.Bussiness
{
    public interface IGestorRTRT
    {
        public Task<IList<TipoRecursoTecnologico>> buscarTipoRecursoTecnologico();
    }
}