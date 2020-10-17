using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ThomasWoodcock.Api
{
    internal class Program
    {
        private async static Task Main()
        {
            await CreateHostBuilder()
                .Build()
                .RunAsync()
                .ConfigureAwait(true);
        }

        private static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Startup>();
                });
    }
}
