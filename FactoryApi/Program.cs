using FactoryApi.Hubs;
using FactoryApi.Services;
using SmartFactory.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFactoryData(builder.Configuration.GetConnectionString("DefaultConnection"), ServiceType.FactoryApi);

builder.Services.AddSignalR();
builder.Services.AddSingleton<MqttService>();
builder.Services.AddHostedService<MqttHostedService>();
builder.Services.AddScoped<IMachineService, MachineService>();

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

app.MapGet("api/machines", async (IMachineService service, CancellationToken ct) =>
{
    var machines = await service.GetAllMachinesAsync(ct);
    return Results.Ok(machines);
});

app.UseCors("BlazorClient");

app.MapHub<TelemetryHub>("/hubs/telemetry");

app.Run();

