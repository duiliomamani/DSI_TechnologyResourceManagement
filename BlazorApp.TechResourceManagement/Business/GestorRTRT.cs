using BlazorApp.TechResourceManagement.Bussiness;
using BlazorApp.TechResourceManagement.Domain;
using BlazorApp.TechResourceManagement.Utils;
using JsonNet.ContractResolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorApp.TechResourceManagement.Bussiness
{
    public class GestorRTRT : IGestorRTRT
    {
        public virtual HttpClient _httpClient { get; set; }
        public async Task<IList<TipoRecursoTecnologico>> buscarTipoRecursoTecnologico()
        {
            try
            {
                var fileJson = await _httpClient.GetByteArrayAsync("fake-data/tipoRecursoTecnologico.json");

                IList<TipoRecursoTecnologico> tiposRecursos = JsonHelper.JsonReader<List<TipoRecursoTecnologico>>(fileJson);
                
                return tiposRecursos ?? new List<TipoRecursoTecnologico>();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}