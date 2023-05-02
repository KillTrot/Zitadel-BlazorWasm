using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Zitadel_BlazorWasm;

namespace Zitadel_BlazorWasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("Zitadel", options.ProviderOptions);
                options.ProviderOptions.ResponseType = "code";
                options.ProviderOptions.ResponseMode = "query";
            });

            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
            await builder.Build().RunAsync();
        }
    }
}