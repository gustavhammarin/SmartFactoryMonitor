using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartFactory.Contracts.Machines;
using SmartFactory.Data.Entities;

namespace SmartFactory.Data.Repositories
{
    public interface IMachineRepository
    {
        Task<IReadOnlyList<MachineResponse>> ListMachinesAsync(CancellationToken ct);
    }
}