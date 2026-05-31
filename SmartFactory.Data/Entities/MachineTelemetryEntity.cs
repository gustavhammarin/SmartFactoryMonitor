namespace SmartFactory.Data.Entities
{
    public abstract class MachineTelemetryEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string MachineId { get; set; } = string.Empty;
        public bool IsRunning { get; set; }
        public bool AlarmActive { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;
    }

    public class CncTelemetryEntity : MachineTelemetryEntity
    {
        public double SpindleRpm { get; set; }
        public double ToolWear { get; set; }
        public double FeedRate { get; set; }
    }

    public class HydraulicPressTelemetryEntity : MachineTelemetryEntity
    {
        public double HydraulicPressure { get; set; }
        public double RamPosition { get; set; }
        public int CyclesPerHour { get; set; }
    }

    public class ConveyorTelemetryEntity : MachineTelemetryEntity
    {
        public double BeltSpeed { get; set; }
        public int ItemsPerMinute { get; set; }
        public double MotorCurrent { get; set; }
    }
}