using JsonNet.ContractResolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace BlazorApp.TechResourceManagement.Utils
{
    public static class JsonHelper
    {
        public static void JsonWriter(string source, string destination, string output)
        {
            //File file;
            //var ms = new MemoryStream();
            //ms.Write(output);
            //var content = new MultipartFormDataContent {
            //            {
            //            new ByteArrayContent(ms.GetBuffer()), folderName, file.Name
            //        }
            //        }
            //string name = destination;
            //string fileName = Path.GetFileName(destination);
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\fake-data\" + name + @"\");

            //filePath = filePath.Replace("\\Server\\", "\\Client\\");

            //if (!Directory.Exists(filePath))
            //{
            //    Directory.CreateDirectory(filePath);
            //}
            //using (var stream = new FileStream(filePath + fileName, FileMode.OpenOrCreate))
            //{
            //    await file.CopyToAsync(stream);
            //}
            //var assembly = typeof(App).Assembly;
            //var filePaths = assembly.GetManifestResourceNames().Where(rnn => rnn.Contains("wwwroot"));

            //var path = Path.Combine("/wwwroot", source, destination);
            ////var path = "wwwroot\\fake-data\\" + source;
            //string rootpath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot");
            //if (!File.Exists(path))
            //    path = string.Format("{0}\\fake-data\\{1}", Directory.GetCurrentDirectory(), source);
            //if (File.Exists(path))
            //{
            //    File.WriteAllText(path, output, Encoding.UTF8);
            //}
        }
        public static T JsonReader<T>(string source) where T : new()
        {
            T obj = default(T);
            var path = "wwwroot\\fake-data\\" + source;
            if (!File.Exists(path))
                path = string.Format("{0}\\fake-data\\{1}", Directory.GetCurrentDirectory(), source);
            if (File.Exists(path))
            {
                StreamReader stream = new(path, Encoding.GetEncoding("UTF-8"));
                string jsonRead = stream.ReadToEnd();
                obj = Deserialize<T>(jsonRead);
            }
            return obj;
        }

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
