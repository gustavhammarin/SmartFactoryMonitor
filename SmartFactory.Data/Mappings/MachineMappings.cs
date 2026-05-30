using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartFactory.Contracts.Machines;
using SmartFactory.Data.Entities;

namespace SmartFactory.Data.Mappings
{
    public static class MachineMappings
    {
        public static MachineResponse ToResponse(this MachineEntity ent) => 
            new MachineResponse(ent.MachineId, ent.Name, ent.Type, ent.IsActive);
    }
}