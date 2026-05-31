# SmartFactoryMonitor

Real-time monitoring of factory machines via MQTT and SignalR.

## Architecture

```
MachineSimulator
      │
      │  MQTT (factory/machines/{id}/telemetry)
      ▼
  Mosquitto (broker)
      │
      ▼
TelemetryWorker
  - Subscribes to raw telemetry
  - Persists to PostgreSQL
  - Publishes processed telemetry (factory/machines/{id}/telemetry/processed)
      │
      │  MQTT (processed)
      ▼
  FactoryApi
  - Subscribes to processed telemetry
  - Pushes live updates to clients via SignalR
  - REST API for machines
      │
      ├── SignalR (WebSocket)
      └── HTTP (REST)
            │
            ▼
    FactoryDashboard (Blazor WASM)
      - Displays real-time telemetry per machine
      - Switches machine group on navigation
```

## Projects

| Project | Type | Responsibility |
|---|---|---|
| `MachineSimulator` | Console | Generates and publishes simulated telemetry via MQTT |
| `TelemetryWorker` | Worker Service | Consumes raw telemetry, persists, publishes processed |
| `FactoryApi` | ASP.NET Core | REST API + SignalR hub for live updates |
| `FactoryDashboard` | Blazor WASM | Per-machine dashboard with real-time data |
| `SmartFactory.Data` | Class Library | DbContext, repositories, EF Core migrations |
| `SmartFactory.Contracts` | Class Library | Shared DTOs between projects |

## Getting Started

Start infrastructure:
```
docker-compose up -d
```

Run migrations:
```
make migrate-update
```

Start all services (four separate terminals):
```
dotnet run --project MachineSimulator
dotnet run --project FactoryApi
dotnet run --project TelemetryWorker
dotnet run --project FactoryDashboard
```

## Migrations

```
make migrate-add name=MigrationName
make migrate-update
```
