using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Threading.Tasks;

namespace MaterialeShop.Admin
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            var application = await builder.AddApplicationAsync<MaterialeShopBlazorModule>(options =>
            {
                options.UseAutofac();
            });

            var host = builder.Build();

            await application.InitializeApplicationAsync(host.Services);

            await host.RunAsync();
        }
    }
}