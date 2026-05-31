using System.Text.Json.Serialization;

namespace SmartFactory.Contracts.Telemetry
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(CncLiveUpdate), "cnc")]
    [JsonDerivedType(typeof(HydraulicPressLiveUpdate), "press")]
    [JsonDerivedType(typeof(ConveyorLiveUpdate), "conveyor")]
    public abstract record MachineTelemetryLiveUpdate(
        string MachineId,
        bool IsRunning,
        bool AlarmActive,
        DateTimeOffset Timestamp,
        DateTimeOffset ReceivedAt
    );

    public record CncLiveUpdate(
        string MachineId,
        bool IsRunning,
        bool AlarmActive,
        DateTimeOffset Timestamp,
        DateTimeOffset ReceivedAt,
        double SpindleRpm,
        double ToolWear,
        double FeedRate
    ) : MachineTelemetryLiveUpdate(MachineId, IsRunning, AlarmActive, Timestamp, ReceivedAt);

    public record HydraulicPressLiveUpdate(
        string MachineId,
        bool IsRunning,
        bool AlarmActive,
        DateTimeOffset Timestamp,
        DateTimeOffset ReceivedAt,
        double HydraulicPressure,
        double RamPosition,
        int CyclesPerHour
    ) : MachineTelemetryLiveUpdate(MachineId, IsRunning, AlarmActive, Timestamp, ReceivedAt);

    public record ConveyorLiveUpdate(
        string MachineId,
        bool IsRunning,
        bool AlarmActive,
        DateTimeOffset Timestamp,
        DateTimeOffset ReceivedAt,
        double BeltSpeed,
        int ItemsPerMinute,
        double MotorCurrent
    ) : MachineTelemetryLiveUpdate(MachineId, IsRunning, AlarmActive, Timestamp, ReceivedAt);
}
