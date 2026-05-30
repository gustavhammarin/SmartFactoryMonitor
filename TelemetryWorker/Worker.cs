namespace TelemetryWorker;

public class Worker(ILogger<Worker> logger, MqttService service) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        logger.LogInformation("worker started...");
        await service.ConnectAsync(ct);
        await service.SubscribeAsync(ct);
        service.SetEvent();

        while (!ct.IsCancellationRequested)
        {
            await Task.Delay(1000, ct);
        }
    }
}
