using System.Text;
using System.Text.Json;
using MQTTnet;
using SmartFactory.Contracts.Telemetry;
using SmartFactory.Data.Mappings;
using SmartFactory.Data.Repositories;

namespace TelemetryWorker
{
    public class MqttService
    {
        private readonly IMqttClient _mqttClient;
        private readonly MqttClientOptions _mqttOptions;

        private readonly MqttClientSubscribeOptions _mqttSubscribeOpts;
        private readonly ILogger<MqttService> _logger;

        private readonly IMachineTelemetryRepository _telemetryRepository;

        public MqttService(ILogger<MqttService> logger, IMachineTelemetryRepository telemetryRepository)
        {
            var mqttFactory = new MqttClientFactory();
            _mqttClient = mqttFactory.CreateMqttClient();

            _mqttOptions = new MqttClientOptionsBuilder()
                .WithTcpServer("localhost", 1883)
                .WithClientId("telemetry-worker")
                .Build();
            _logger = logger;

            _mqttSubscribeOpts = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter("factory/machines/+/telemetry")
                .Build();
            _telemetryRepository = telemetryRepository;
        }

        public async Task ConnectAsync(CancellationToken ct)
        {
            await _mqttClient.ConnectAsync(_mqttOptions, ct);
            _logger.LogInformation("Client connected");
        }

        public async Task StopAsync(CancellationToken ct)
        {
            await _mqttClient.DisconnectAsync(cancellationToken: ct);
            _logger.LogInformation("Client Disconnected");
        }

        public async Task SubscribeAsync(CancellationToken ct)
        {
            await _mqttClient.SubscribeAsync(_mqttSubscribeOpts, ct);
            _logger.LogInformation($"Subscribed to topic: {_mqttOptions.TopicAliasMaximum}");
        }

        public void SetEvent()
        {
            _mqttClient.ApplicationMessageReceivedAsync += HandleMessageReceivedAsync;
        }

        public async Task HandleMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            var topic = e.ApplicationMessage.Topic;
            var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

            try
            {
                var telemetry = JsonSerializer.Deserialize<MachineTelemetryUnprocessed>(
                    payload,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (telemetry is null)
                {
                    _logger.LogWarning("Could not deserialize telemetry.");
                    return;
                }

                await SaveMachineTelemetryAsync(telemetry, topic);

                _logger.LogInformation(
                    "Machine {MachineId}: Temp={Temperature}, Vibration={Vibration}, Pressure={Pressure}, Alarm={AlarmActive}",
                    telemetry.MachineId,
                    telemetry.Temperature,
                    telemetry.Vibration,
                    telemetry.Pressure,
                    telemetry.AlarmActive
                );
                await Task.CompletedTask;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing message.");
            }
        }

        private async Task SaveMachineTelemetryAsync(MachineTelemetryUnprocessed dto, string topic)
        {
            await _telemetryRepository.SaveMachineTelemetryAsync(dto);
            await PublishProcessedTelemetryAsync(dto.ToLiveUpdate());
        }

        private async Task PublishProcessedTelemetryAsync(MachineTelemetryLiveUpdate liveUpdate)
        {
            var json = JsonSerializer.Serialize(liveUpdate);

            var message = new MqttApplicationMessageBuilder()
                .WithTopic($"factory/machines/{liveUpdate.MachineId}/telemetry/processed")
                .WithPayload(json)
                .Build();

            await _mqttClient.PublishAsync(message);

            _logger.LogInformation(
            "Published processed telemetry event for machine {MachineId}",
            liveUpdate.MachineId
            );
        }
    }
}