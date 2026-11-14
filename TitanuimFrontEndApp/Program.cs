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

            builder.Services.AddMudServices();

            builder.Services.AddScoped(Sp => new HttpClient
                {
                    BaseAddress = new Uri(builder.Configuration["BaseUrl"] ?? "https://localhost:7197")
                }
            );

            builder.Services.AddScoped<CustomersApiService>(options => new CustomersApiService(
                new HttpClient
                {
                    BaseAddress = new Uri(builder.Configuration["BaseUrl"] ?? "https://localhost:7197")
                }
            ));




            await builder.Build().RunAsync();
        }
    }
}
