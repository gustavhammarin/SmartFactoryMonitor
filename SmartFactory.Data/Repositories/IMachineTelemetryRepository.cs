using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartFactory.Contracts.Telemetry;
using SmartFactory.Data.Entities;

namespace SmartFactory.Data.Repositories
{
    public interface IMachineTelemetryRepository
    {
        Task SaveMachineTelemetryAsync(MachineTelemetry dto);
    }
}