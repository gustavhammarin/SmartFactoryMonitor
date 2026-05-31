using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartFactory.Contracts.Machines;
using SmartFactory.Data;
using SmartFactory.Data.Repositories;

namespace FactoryApi.Services
{
    public class MachineService : IMachineService
    {
        private readonly IMachineRepository _machineRepository;
        public MachineService(IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository;
        }

        public async Task<IReadOnlyList<MachineResponse>> GetAllMachinesAsync(CancellationToken ct)
        {
            return await _machineRepository.ListMachinesAsync(ct);
        }
    }
}