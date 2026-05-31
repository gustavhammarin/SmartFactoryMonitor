using Microsoft.EntityFrameworkCore;
using SmartFactory.Data;
using SmartFactory.Data.Repositories;
using TelemetryWorker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddFactoryData(builder.Configuration.GetConnectionString("DefaultConnection"), ServiceType.TelemetryWorker);

builder.Services.AddSingleton<MqttService>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
