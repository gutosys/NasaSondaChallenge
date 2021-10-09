using Microsoft.Extensions.DependencyInjection;
using NasaSondaChallenge.Repository.Contract;
using NasaSondaChallenge.Repository.Repository;
using NasaSondaChallenge.Service.Contract;
using NasaSondaChallenge.Service.Service;

namespace NasaSondaChallenge.Infra.DI
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IExploreRepository, ExploreRepository>();
            services.AddScoped<IExploreService, ExploreService>();
            return services;
        }
    }
}
