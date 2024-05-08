using Desafio.Infrastructure.Repository;
using Desafio.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testes.Main;
using Testes.Repositories;
using Testes.Services;

namespace Testes.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            IConfiguration configuration = GetConfiguration();
            services.AddSingleton<IConfiguration>(configuration);
            RegisterDependencies(services);
            return services;
        }

        private static void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped<MainAppTest>();

            services.AddScoped<IRepository, ProductRepository>();
            services.AddScoped<TestRepository>();

            services.AddScoped<IService, ProductService>();
            services.AddScoped<TestProductService>();
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings.json").AddEnvironmentVariables();
            IConfiguration configuration = builder.Build();
            return configuration;
        }
    }
}
