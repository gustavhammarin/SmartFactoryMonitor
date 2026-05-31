using SmartFactory.Contracts.Telemetry;

namespace MachineSimulator
{
    public class HydraulicPressGenerator(string machineId) : MachineGeneratorBase(machineId)
    {
        private double _hydraulicPressure = 120;
        private double _ramPosition = 0;
        private int _cyclesPerHour = 45;
        private bool _ramExtending = true;

        public override MachineTelemetry Generate()
        {
            _hydraulicPressure = Math.Clamp(_hydraulicPressure + RandomBetween(-2, 2), 40, 200);

            // simulate ram cycling up and down
            _ramPosition += _ramExtending ? RandomBetween(8, 15) : RandomBetween(-15, -8);
            if (_ramPosition >= 250) _ramExtending = false;
            if (_ramPosition <= 0) { _ramPosition = 0; _ramExtending = true; _cyclesPerHour += _random.Next(-2, 3); }
            _ramPosition = Math.Clamp(_ramPosition, 0, 250);
            _cyclesPerHour = Math.Clamp(_cyclesPerHour, 20, 70);

            var alarmActive = _hydraulicPressure > 180 || _hydraulicPressure < 50;

            return new HydraulicPressTelemetry(
                MachineId,
                IsRunning: true,
                AlarmActive: alarmActive,
                Timestamp: DateTimeOffset.UtcNow,
                HydraulicPressure: Math.Round(_hydraulicPressure, 1),
                RamPosition: Math.Round(_ramPosition, 1),
                CyclesPerHour: _cyclesPerHour
            );
        }
    }
}
