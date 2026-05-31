using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartFactory.Contracts.Machines;
using SmartFactory.Data.Entities;
using SmartFactory.Data.Mappings;

namespace SmartFactory.Data.Repositories
{
    public class MachineRepository : IMachineRepository
    {
        private readonly AppDbContext _context;

        public MachineRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<MachineResponse>> ListMachinesAsync(CancellationToken ct)
        {
            var entities = await _context.Machines.AsNoTracking().ToListAsync(ct);
            return [.. entities.Select(e => e.ToResponse())];
        }
    }
}