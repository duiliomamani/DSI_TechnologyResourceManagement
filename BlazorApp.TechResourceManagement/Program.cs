using BlazorApp.TechResourceManagement.Bussiness;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorApp.TechResourceManagement
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IGestorRTRT>(provider =>
            {
                var apiShared = new GestorRTRT
                {
                    _httpClient = new HttpClient()
                    {
                        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
                        Timeout = TimeSpan.FromMinutes(20)
                    },
                    usuarioActual = new Domain.Usuario("C-4445511", "12345678")
                };
                return apiShared;
            });
            var str = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            await builder.Build().RunAsync();
        }
    }
}