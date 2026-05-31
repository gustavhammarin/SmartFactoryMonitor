using SmartFactory.Contracts.Telemetry;

namespace MachineSimulator
{
    public class MachineDataGenerator
    {
        private readonly IReadOnlyList<IMachineGenerator> _generators =
        [
            new CncGenerator("cnc-01"),
            new HydraulicPressGenerator("press-01"),
            new ConveyorGenerator("conveyor-01")
        ];

        public IReadOnlyList<MachineTelemetry> Generate() =>
            _generators.Select(g => g.Generate()).ToList().AsReadOnly();
    }
}
