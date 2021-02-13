using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using ThomasWoodcock.Service;

await CreateHostBuilder()
    .Build()
    .RunAsync();

static IHostBuilder CreateHostBuilder()
{
    return Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>());
}