using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartFactory.Contracts.Telemetry;

namespace MachineSimulator
{
    public interface IMachineGenerator
    {
        MachineTelemetry Generate();
    }
}