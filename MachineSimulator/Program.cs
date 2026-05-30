using System.Text.Json;
using MachineSimulator;
using MQTTnet;

var mqttFactory = new MqttClientFactory();
var mqttClient = mqttFactory.CreateMqttClient();

var mqttOptions = new MqttClientOptionsBuilder()
    .WithTcpServer("localhost", 1883)
    .WithClientId("machine-simulator-press-01")
    .Build();

await mqttClient.ConnectAsync(mqttOptions);

Console.WriteLine("Machine simulator connected to MQTT broker.");
Console.WriteLine("Publishing machine telemetry every 2 seconds...");
Console.WriteLine();

var generator = new MachineDataGenerator();
var mqttService = new MqttService(mqttClient);

while (true)
{
    var machines = generator.Generate();

    await Parallel.ForEachAsync(machines, async (machine, ct) =>
    {
        var json = JsonSerializer.Serialize(machine, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        await mqttService.PublishMessageAsync($"factory/machines/{machine.MachineId}/telemetry", json, ct);
    });
}
