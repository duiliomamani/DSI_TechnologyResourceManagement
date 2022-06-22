using JsonNet.ContractResolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace BlazorApp.TechResourceManagement.Utils
{
    public static class JsonHelper
    {
        public static T JsonReader<T>(byte[] json) where T : new()
        {
            Stream st = new MemoryStream(json);
            StreamReader stream = new(st, Encoding.GetEncoding("UTF-8"));
            var jsonRead = stream.ReadToEnd();
            T obj = Deserialize<T>(jsonRead);
            stream.Close();
            return obj;
        }

        public static T Deserialize<T>(string text)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatString = "yyyy-MM-ddTHH:mm:ss",
                ContractResolver = new PrivateSetterAndCtorCamelCasePropertyNamesContractResolver()
                { NamingStrategy = new CamelCaseNamingStrategy(true, true) }
            };
            return JsonConvert.DeserializeObject<T>(text, settings);
        }

        public static string Serialize<T>(T obj)
        {
            var setting = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatString = "yyyy-MM-ddTHH:mm:ss",
                ContractResolver = new PrivateSetterAndCtorCamelCasePropertyNamesContractResolver()
                { NamingStrategy = new CamelCaseNamingStrategy(true, true) }
            };
            return JsonConvert.SerializeObject(obj, setting);
        }

        public static object? JsonConvertObject(object data)
        {
            var setting = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatString = "yyyy-MM-ddTHH:mm:ss",
                ContractResolver = new PrivateSetterAndCtorCamelCasePropertyNamesContractResolver()
                { NamingStrategy = new CamelCaseNamingStrategy(true, true) }
            };
            var json = JsonConvert.SerializeObject(data, setting);
            return JsonConvert.DeserializeObject<object>(json);
        }
    }
}
