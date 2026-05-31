using Microsoft.EntityFrameworkCore;
using SmartFactory.Data;
using SmartFactory.Data.Repositories;
using TelemetryWorker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddFactoryData(builder.Configuration.GetConnectionString("DefaultConnection"), ServiceType.TelemetryWorker);
builder.Services.AddSingleton<MqttService>();
builder.Services.AddHostedService<Worker>();
builder.Services.AddHttpClient<FactoryApiClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5034");
});


var host = builder.Build();
host.Run();
