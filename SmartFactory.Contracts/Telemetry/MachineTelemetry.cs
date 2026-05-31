using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFactory.Contracts.Telemetry
{
    public record MachineTelemetry(
      string MachineId,
      double Temperature,
      double Vibration,
      double Pressure,
      bool IsRunning,
      int ProductCount,
      bool AlarmActive,
      DateTimeOffset Timestamp
  );
}