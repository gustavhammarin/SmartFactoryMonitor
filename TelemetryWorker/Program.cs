using Microsoft.EntityFrameworkCore;
using SmartFactory.Data;
using SmartFactory.Data.Repositories;
using TelemetryWorker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<MqttService>();

builder.Services.AddDbContextFactory<AppDbContext>(options =>
{
    options.UseSqlite("Data Source=smartfactory.db");
});

builder.Services.AddScoped<IMachineTelemetryRepository, MachineTelemetryRepository>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
