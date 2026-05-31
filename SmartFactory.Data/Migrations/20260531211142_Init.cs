using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartFactory.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CncTelemetries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MachineId = table.Column<string>(type: "text", nullable: false),
                    IsRunning = table.Column<bool>(type: "boolean", nullable: false),
                    AlarmActive = table.Column<bool>(type: "boolean", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    SavedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SpindleRpm = table.Column<double>(type: "double precision", nullable: false),
                    ToolWear = table.Column<double>(type: "double precision", nullable: false),
                    FeedRate = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CncTelemetries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConveyorTelemetries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MachineId = table.Column<string>(type: "text", nullable: false),
                    IsRunning = table.Column<bool>(type: "boolean", nullable: false),
                    AlarmActive = table.Column<bool>(type: "boolean", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    SavedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BeltSpeed = table.Column<double>(type: "double precision", nullable: false),
                    ItemsPerMinute = table.Column<int>(type: "integer", nullable: false),
                    MotorCurrent = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConveyorTelemetries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HydraulicPressTelemetries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MachineId = table.Column<string>(type: "text", nullable: false),
                    IsRunning = table.Column<bool>(type: "boolean", nullable: false),
                    AlarmActive = table.Column<bool>(type: "boolean", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    SavedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HydraulicPressure = table.Column<double>(type: "double precision", nullable: false),
                    RamPosition = table.Column<double>(type: "double precision", nullable: false),
                    CyclesPerHour = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HydraulicPressTelemetries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MachineId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Machines",
                columns: new[] { "Id", "IsActive", "MachineId", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), true, "press-01", "Press 01", "Press" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), true, "cnc-01", "CNC 01", "CNC" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), true, "conveyor-01", "Conveyor 01", "Conveyor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CncTelemetries");

            migrationBuilder.DropTable(
                name: "ConveyorTelemetries");

            migrationBuilder.DropTable(
                name: "HydraulicPressTelemetries");

            migrationBuilder.DropTable(
                name: "Machines");
        }
    }
}
