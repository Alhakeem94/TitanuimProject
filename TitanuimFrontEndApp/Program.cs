using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TitanuimFrontEndApp.Services;

namespace TitanuimFrontEndApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Default HttpClient (used for static files such as sample-data/weather.json).
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices();

            // Typed client for the TitaniumBackendApi. The base URL comes from wwwroot/appsettings.json.
            var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7197";
            builder.Services.AddScoped(sp => new CustomersApiService(
                new HttpClient { BaseAddress = new Uri(apiBaseUrl) }));

            await builder.Build().RunAsync();
        }
    }
}
