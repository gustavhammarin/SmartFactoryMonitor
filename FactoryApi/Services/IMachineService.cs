using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartFactory.Contracts.Machines;

namespace FactoryApi.Services
{
    public interface IMachineService
    {
        Task<IReadOnlyList<MachineResponse>> GetAllMachinesAsync(CancellationToken ct);
    }
}