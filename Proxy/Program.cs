using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

builder.Configuration.AddJsonFile("ocelot.json", false, true);
builder.Services.AddOcelot(builder.Configuration)
                .AddCacheManager(x =>
                {
                    x.WithDictionaryHandle();
                });

var app = builder.Build();
app.MapGet("/", () => "Smart Proxy");
app.UseOcelot().Wait();

app.Run();