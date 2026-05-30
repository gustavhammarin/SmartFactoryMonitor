using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactoryApi.Services
{
    public class MqttHostedService : BackgroundService
    {
        private readonly MqttService _service;

        public MqttHostedService(MqttService service)
        {
            _service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            _service.SetOnEventReceived();
            await _service.ConnectAsync(ct);
            await _service.SubscribeAsync(ct);

            while (!ct.IsCancellationRequested)
            {
                await Task.Delay(1000, ct);
            }
        }

        public override async Task StopAsync(CancellationToken ct)
        {
            await _service.DisconnectAsync(ct);
            await base.StopAsync(ct);
        }
    }
}