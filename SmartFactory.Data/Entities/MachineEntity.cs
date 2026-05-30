using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFactory.Data.Entities
{
    public class MachineEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string MachineId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Type { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}