using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MQTTnet;

namespace MachineSimulator
{
    public class MqttService
    {
        private readonly IMqttClient _mqttClient;

        public MqttService(IMqttClient mqttClient)
        {
            _mqttClient = mqttClient;
        }

        public async Task PublishMessageAsync(string topic, string content, CancellationToken ct)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(content)
                .Build();

            await _mqttClient.PublishAsync(message, ct);

            Console.WriteLine(content);

            await Task.Delay(2000, ct);
        }
    }
}