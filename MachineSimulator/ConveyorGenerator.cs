using SmartFactory.Contracts.Telemetry;

namespace MachineSimulator
{
    public class ConveyorGenerator(string machineId) : MachineGeneratorBase(machineId)
    {
        private double _beltSpeed = 0.8;
        private int _itemsPerMinute = 20;
        private double _motorCurrent = 10;

        public override MachineTelemetry Generate()
        {
            _beltSpeed = Math.Clamp(_beltSpeed + RandomBetween(-0.02, 0.02), 0.1, 2.0);
            _itemsPerMinute = Math.Clamp(_itemsPerMinute + _random.Next(-2, 3), 5, 60);
            _motorCurrent = Math.Clamp(_motorCurrent + RandomBetween(-0.3, 0.3), 3, 22);

            var alarmActive = _motorCurrent > 18 || _beltSpeed < 0.2;

            return new ConveyorTelemetry(
                MachineId,
                IsRunning: true,
                AlarmActive: alarmActive,
                Timestamp: DateTimeOffset.UtcNow,
                BeltSpeed: Math.Round(_beltSpeed, 2),
                ItemsPerMinute: _itemsPerMinute,
                MotorCurrent: Math.Round(_motorCurrent, 2)
            );
        }
    }
}
