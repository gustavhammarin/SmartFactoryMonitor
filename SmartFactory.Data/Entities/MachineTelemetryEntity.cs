namespace SmartFactory.Data.Entities
{
    public class MachineTelemetryEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string MachineId { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public double Vibration { get; set; }
        public double Pressure { get; set; }
        public bool IsRunning { get; set; }
        public int ProductCount { get; set; }
        public bool AlarmActive { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;
    }
}