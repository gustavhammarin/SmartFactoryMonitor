using SmartFactory.Contracts.Telemetry;

namespace MachineSimulator
{
    public abstract class MachineGeneratorBase(string machineId) : IMachineGenerator
    {
        protected readonly string MachineId = machineId;
        protected readonly Random _random = new();

        protected double RandomBetween(double min, double max) =>
            min + (_random.NextDouble() * (max - min));

        public abstract MachineTelemetry Generate();
    }
}
