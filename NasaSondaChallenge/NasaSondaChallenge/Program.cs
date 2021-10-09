using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NasaSondaChallenge.Service.Contract;
using NasaSondaChallenge.Service.Service;
using System;
using System.IO;

namespace NasaSondaChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = AppStartup();

            var exploreService = ActivatorUtilities.CreateInstance<ExploreService>(host.Services);
            exploreService.Explore();
        }

        static void ConfigSetup(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();
        }

        static IHost AppStartup()
        {
            var builder = new ConfigurationBuilder();
            ConfigSetup(builder);           

            // Initiated the denpendency injection container 
            var host = Host.CreateDefaultBuilder()
                        .ConfigureServices((context, services) => {
                            services.AddTransient<IExploreService, ExploreService>();
                        })                        
                        .Build();

            return host;
        }
    }
}
