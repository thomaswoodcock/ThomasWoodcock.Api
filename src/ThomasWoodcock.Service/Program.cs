using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ThomasWoodcock.Service.Infrastructure.Persistence.Accounts;

namespace ThomasWoodcock.Service
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = Program.CreateHostBuilder(args)
                .Build();

            using (IServiceScope scope = host.Services.CreateScope())
            {
                var accountContext = scope.ServiceProvider.GetRequiredService<AccountContext>();

                if (accountContext.Database.IsSqlite())
                {
                    await accountContext.Database.MigrateAsync();
                }
            }

            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>());
        }
    }
}
