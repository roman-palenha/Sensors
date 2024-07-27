using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sensors.Business.Background;
using Sensors.Business.Hubs;
using Sensors.Business.Services;
using Sensors.Business.Services.Interfaces;
using Sensors.Domain;
using Sensors.Domain.Configs;

var builder = WebApplication.CreateBuilder(args);

var corsSettings = builder.Configuration.GetSection("Cors");
var allowedOrigins = corsSettings.GetSection("AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("HubConfig",
        builder =>
        {
            builder.WithOrigins(allowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("sensorsDb")));

builder.Services.AddScoped<ISensorService, SensorService>();
builder.Services.AddHostedService<SensorBackgroundService>();
builder.Services.Configure<SensorConfiguration>(builder.Configuration.GetSection("SensorConfiguration"));
builder.Services.AddSignalR();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("HubConfig");

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<SensorHub>("/sensorHub");
});

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    var sensorConfiguration = services.GetRequiredService<IOptions<SensorConfiguration>>();

    var initializer = new DataInitializer(context, sensorConfiguration);
    await initializer.InitializeAsync();
}

app.Run();
