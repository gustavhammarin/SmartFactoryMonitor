using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartFactory.Data.Repositories;

namespace SmartFactory.Data
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFactoryData(this IServiceCollection services, string? connectionString, ServiceType serviceType)
        {
            if (connectionString is null)
            {
                throw new ArgumentException("Connectionstring is null");
            }
            services.AddDbContext<AppDbContext>(opts =>
            {
                opts.UseNpgsql(connectionString);
            });

            switch (serviceType)
            {
                case ServiceType.FactoryApi:
                services.AddScoped<IMachineRepository, MachineRepository>();
                break;
                case ServiceType.TelemetryWorker:
                services.AddScoped<IMachineTelemetryRepository, MachineTelemetryRepository>();
                break;
                default:
                break;
            }
        }
    }

    public enum ServiceType
    {
        FactoryApi,
        TelemetryWorker
    }
}