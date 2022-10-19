using Allivet.WebAPI.Infrastructure.Common.Interfaces;
using Allivet.WebAPI.Infrastructure.Repositories;
using Allivet.WebAPI.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Allivet.WebAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IVeterinaryLocationRepository, VeterinaryLocationRepository>();
            services.AddScoped<IExcelService, ExcelService>();
            return services;
        }
    }
}
