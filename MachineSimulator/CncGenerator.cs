using SmartFactory.Contracts.Telemetry;

namespace MachineSimulator
{
    public class CncGenerator(string machineId) : MachineGeneratorBase(machineId)
    {
        private double _spindleRpm = 2000;
        private double _toolWear = 0;
        private double _feedRate = 200;

        public override MachineTelemetry Generate()
        {
            _spindleRpm = Math.Clamp(_spindleRpm + RandomBetween(-50, 50), 500, 3000);
            _toolWear = Math.Clamp(_toolWear + RandomBetween(0.05, 0.4), 0, 100);
            _feedRate = Math.Clamp(_feedRate + RandomBetween(-5, 5), 50, 500);

            var alarmActive = _spindleRpm < 800 || _spindleRpm > 2800 || _toolWear > 85;

            return new CncTelemetry(
                MachineId,
                IsRunning: true,
                AlarmActive: alarmActive,
                Timestamp: DateTimeOffset.UtcNow,
                SpindleRpm: Math.Round(_spindleRpm, 0),
                ToolWear: Math.Round(_toolWear, 2),
                FeedRate: Math.Round(_feedRate, 1)
            );
        }
    }
}
