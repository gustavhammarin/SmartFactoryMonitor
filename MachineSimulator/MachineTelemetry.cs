using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineSimulator
{
    public class MachineTelemetry
    {
        public string MachineId { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public double Vibration { get; set; }
        public double Pressure { get; set; }
        public bool IsRunning { get; set; }
        public int ProductCount { get; set; }
        public bool AlarmActive { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}