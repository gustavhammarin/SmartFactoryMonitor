using Microsoft.EntityFrameworkCore;
using SmartFactory.Data.Entities;

namespace SmartFactory.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<MachineTelemetryEntity> MachineTelemetries => Set<MachineTelemetryEntity>();
        public DbSet<MachineEntity> Machines => Set<MachineEntity>();
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MachineEntity>().HasData(

                new MachineEntity
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    MachineId = "press-01",
                    Name = "Press 01",
                    Type = "Press",
                    IsActive = true
                },

                new MachineEntity
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    MachineId = "cnc-01",
                    Name = "CNC 01",
                    Type = "CNC",
                    IsActive = true
                }
            );
        }
    }
}