using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineSimulator
{
    public class MachineDataGenerator
    {
        private readonly IReadOnlyList<string> _machines = new List<string>(["press-01", "cnc-01"]);
        private readonly Random _random = new();
        private int _productCount = 0;
        private double _temperature = 65;
        private double _vibration = 1.2;
        private double _pressure = 5.0;
        public IReadOnlyList<MachineTelemetry> Generate()
        {
            var machineTelemetries = new List<MachineTelemetry>();
            foreach (var machine in _machines)
            {
                var isRunning = true;

                _temperature += RandomBetween(-0.5, 0.8);
                _vibration += RandomBetween(-0.1, 0.15);
                _pressure += RandomBetween(-0.2, 0.2);

                if (isRunning)
                {
                    _productCount += _random.Next(0, 3);
                }

                var alarmActive =
                    _temperature > 85 ||
                    _vibration > 3.5 ||
                    _pressure > 8;

                machineTelemetries.Add(new MachineTelemetry

                {
                    MachineId = machine,
                    Temperature = Math.Round(_temperature, 2),
                    Vibration = Math.Round(_vibration, 2),
                    Pressure = Math.Round(_pressure, 2),
                    IsRunning = isRunning,
                    ProductCount = _productCount,
                    AlarmActive = alarmActive,
                    Timestamp = DateTimeOffset.UtcNow
                });
            }
            return machineTelemetries.AsReadOnly();
        }

        private double RandomBetween(double min, double max)
        {
            return min + (_random.NextDouble() * (max - min));
        }
    }
}