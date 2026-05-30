using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFactory.Contracts.Machines
{
    public record MachineResponse(
        string MachineId,
        string Name,
        string Type,
        bool IsActive
    );
}