using SmartFactory.Contracts.Telemetry;
using SmartFactory.Data.Entities;

namespace SmartFactory.Data.Mappings
{
    public static class MachineTelemetryMappings
    {
        public static MachineTelemetryEntity ToEntity(this MachineTelemetryUnprocessed dto) => new MachineTelemetryEntity
        {
            MachineId = dto.MachineId,
            Temperature = dto.Temperature,
            Vibration = dto.Vibration,
            Pressure = dto.Pressure,
            IsRunning = dto.IsRunning,
            ProductCount = dto.ProductCount,
            AlarmActive = dto.AlarmActive,
            Timestamp = dto.Timestamp
        };

        public static MachineTelemetryLiveUpdate ToLiveUpdate(this MachineTelemetryUnprocessed up) => new MachineTelemetryLiveUpdate(
            up.MachineId,
            up.Temperature,
            up.Vibration,
            up.Pressure,
            up.IsRunning,
            up.ProductCount,
            up.AlarmActive,
            up.Timestamp,
            DateTimeOffset.UtcNow
        );

    }
}