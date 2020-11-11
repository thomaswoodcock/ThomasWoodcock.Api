using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using ThomasWoodcock.Api;

await CreateHostBuilder()
        .Build()
        .RunAsync()
        .ConfigureAwait(true);

static IHostBuilder CreateHostBuilder() =>
    Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(builder =>
        {
            builder.UseStartup<Startup>();
        });
