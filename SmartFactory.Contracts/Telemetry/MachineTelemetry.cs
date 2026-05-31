using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartFactory.Contracts.Telemetry
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(CncTelemetry), "cnc")]
    [JsonDerivedType(typeof(HydraulicPressTelemetry), "press")]
    [JsonDerivedType(typeof(ConveyorTelemetry), "conveyor")]
    public abstract record MachineTelemetry(
      string MachineId,
      bool IsRunning,      
      bool AlarmActive,
      DateTimeOffset Timestamp
    );

    public record CncTelemetry(
      string MachineId,
      bool IsRunning,
      bool AlarmActive,
      DateTimeOffset Timestamp,
      double SpindleRpm,
      double ToolWear,
      double FeedRate
    ): MachineTelemetry(MachineId, IsRunning, AlarmActive, Timestamp);

    public record HydraulicPressTelemetry(
      string MachineId,
      bool IsRunning,
      bool AlarmActive,
      DateTimeOffset Timestamp,
      double HydraulicPressure,
      double RamPosition,
      int CyclesPerHour
    ) : MachineTelemetry(MachineId, IsRunning, AlarmActive, Timestamp);

    public record ConveyorTelemetry(
      string MachineId,
      bool IsRunning,
      bool AlarmActive,
      DateTimeOffset Timestamp,
      double BeltSpeed,
      int ItemsPerMinute,
      double MotorCurrent
    ) : MachineTelemetry(MachineId, IsRunning, AlarmActive, Timestamp);
}