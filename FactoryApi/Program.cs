using FactoryApi.Hubs;
using FactoryApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddSingleton<MqttService>();
builder.Services.AddHostedService<MqttHostedService>();

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("BlazorClient", policy =>
    {
        policy
            .WithOrigins("https://localhost:5210", "http://localhost:5210")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}

app.UseCors("BlazorClient");

app.MapHub<TelemetryHub>("/hubs/telemetry");

app.Run();

