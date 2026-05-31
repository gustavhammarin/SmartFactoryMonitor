using SmartFactory.Contracts.Telemetry;
using SmartFactory.Data.Entities;

namespace SmartFactory.Data.Mappings
{
    public static class MachineTelemetryMappings
    {
        public static MachineTelemetryEntity ToEntity(this MachineTelemetry dto) => dto switch
        {
            CncTelemetry cnc => new CncTelemetryEntity
            {
                MachineId = cnc.MachineId,
                IsRunning = cnc.IsRunning,
                AlarmActive = cnc.AlarmActive,
                Timestamp = cnc.Timestamp,
                SpindleRpm = cnc.SpindleRpm,
                ToolWear = cnc.ToolWear,
                FeedRate = cnc.FeedRate
            },
            HydraulicPressTelemetry press => new HydraulicPressTelemetryEntity
            {
                MachineId = press.MachineId,
                IsRunning = press.IsRunning,
                AlarmActive = press.AlarmActive,
                Timestamp = press.Timestamp,
                HydraulicPressure = press.HydraulicPressure,
                RamPosition = press.RamPosition,
                CyclesPerHour = press.CyclesPerHour
            },
            ConveyorTelemetry conveyor => new ConveyorTelemetryEntity
            {
                MachineId = conveyor.MachineId,
                IsRunning = conveyor.IsRunning,
                AlarmActive = conveyor.AlarmActive,
                Timestamp = conveyor.Timestamp,
                BeltSpeed = conveyor.BeltSpeed,
                ItemsPerMinute = conveyor.ItemsPerMinute,
                MotorCurrent = conveyor.MotorCurrent
            },
            _ => throw new ArgumentException($"Unknown telemetry type: {dto.GetType().Name}")
        };

        public static MachineTelemetryLiveUpdate ToLiveUpdate(this MachineTelemetry up) => up switch
        {
            CncTelemetry cnc => new CncLiveUpdate(
                cnc.MachineId, cnc.IsRunning, cnc.AlarmActive, cnc.Timestamp, DateTimeOffset.UtcNow,
                cnc.SpindleRpm, cnc.ToolWear, cnc.FeedRate),
            HydraulicPressTelemetry press => new HydraulicPressLiveUpdate(
                press.MachineId, press.IsRunning, press.AlarmActive, press.Timestamp, DateTimeOffset.UtcNow,
                press.HydraulicPressure, press.RamPosition, press.CyclesPerHour),
            ConveyorTelemetry conveyor => new ConveyorLiveUpdate(
                conveyor.MachineId, conveyor.IsRunning, conveyor.AlarmActive, conveyor.Timestamp, DateTimeOffset.UtcNow,
                conveyor.BeltSpeed, conveyor.ItemsPerMinute, conveyor.MotorCurrent),
            _ => throw new ArgumentException($"Unknown telemetry type: {up.GetType().Name}")
        };

    }
}