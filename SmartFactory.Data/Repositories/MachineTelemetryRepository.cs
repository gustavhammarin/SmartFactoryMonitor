using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartFactory.Contracts.Telemetry;
using SmartFactory.Data.Entities;
using SmartFactory.Data.Mappings;

namespace SmartFactory.Data.Repositories
{
    public class MachineTelemetryRepository : IMachineTelemetryRepository
    {
        private readonly AppDbContext _context;

        public MachineTelemetryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task SaveMachineTelemetryAsync(MachineTelemetryUnprocessed dto)
        {
            await _context.AddAsync(dto.ToEntity());
            await _context.SaveChangesAsync();
        }
    }
}