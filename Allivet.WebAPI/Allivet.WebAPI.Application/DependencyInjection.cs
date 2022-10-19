using Allivet.WebAPI.Application.VeterinaryLocationManagement.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Allivet.WebAPI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IVeterinaryLocationQueries, VeterinaryLocationQueries>();

            return services;
        }
    }
}
