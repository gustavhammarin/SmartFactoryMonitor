using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FactoryApi.Hubs;
using Microsoft.AspNetCore.SignalR;
using MQTTnet;
using SmartFactory.Contracts.Telemetry;

namespace FactoryApi.Services
{
    public class MqttService
    {
        private readonly IMqttClient _client;
        private readonly MqttClientSubscribeOptions _subscribeOpts;
        private readonly MqttClientOptions _clientOpts;
        private readonly IHubContext<TelemetryHub> _hubContext;
        private readonly ILogger<MqttService> _logger;

        public MqttService(IHubContext<TelemetryHub> hubContext, ILogger<MqttService> logger)
        {
            var factory = new MqttClientFactory();
            _client = factory.CreateMqttClient();

            _clientOpts = new MqttClientOptionsBuilder()
                .WithTcpServer("localhost", 1883)
                .WithClientId("factory-api")
                .Build();

            _subscribeOpts = new MqttClientSubscribeOptionsBuilder()
                .WithTopicFilter("factory/machines/+/telemetry/processed")
                .Build();
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task ConnectAsync(CancellationToken ct)
        {
            await _client.ConnectAsync(_clientOpts, ct);
            _logger.LogInformation("FactoryApi MQTT client connected");
        }

        public async Task DisconnectAsync(CancellationToken ct)
        {
            await _client.DisconnectAsync(cancellationToken: ct);
            _logger.LogInformation("FactoryApi MQTT client disconnected");
        }

        public async Task SubscribeAsync(CancellationToken ct)
        {
            await _client.SubscribeAsync(_subscribeOpts, ct);
            _logger.LogInformation("Subscribed to factory/machines/+/telemetry/processed");
        }

        public void SetOnEventReceived()
        {
            _client.ApplicationMessageReceivedAsync += HandleMessageReceivedAsync;
        }

        public async Task HandleMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

            try
            {
                var liveUpdate = JsonSerializer.Deserialize<MachineTelemetryLiveUpdate>(
                    payload,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );
                if (liveUpdate is null)
                {
                    _logger.LogWarning("Could not deserialize live telemetry update.");
                    return;
                }
                
                await _hubContext.Clients
                    .Group(liveUpdate.MachineId)
                    .SendAsync("TelemetryReceived", liveUpdate);

            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error while handling MQTT processed telemetry.");
            }
        }
    }
}