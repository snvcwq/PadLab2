using FoodDeliveryAPI.Context;
using FoodDeliveryAPI.Entities;
using FoodDeliveryAPI.Interfaces;
using FoodDeliveryAPI.Repositories;
using FoodDeliveryAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FoodDeliveryContext>(options =>
{
    string connectionString = "";
    if (builder.Environment.EnvironmentName == "Development1")
    {
        connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    }
    else if (builder.Environment.EnvironmentName == "Development2")
    {
        connectionString = builder.Configuration.GetConnectionString("DefaultConnection2");
    }
    options.UseSqlServer(connectionString);
});

/*builder.Services.AddDbContext<FoodDeliveryContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection2"));
});*/


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IRepository<User>, Repository<User>>();

builder.Services.AddScoped<ISyncService<User>, SyncService<User>>();

builder.Services.Configure<SyncServiceSettings>(builder.
    Configuration.GetSection("SyncServiceSettings"));

builder.Services.AddSingleton<ISyncServiceSettings>(provider =>
    provider.GetRequiredService<IOptions<SyncServiceSettings>>().Value);



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
