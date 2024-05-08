using Microsoft.Extensions.DependencyInjection;
using System;
using Testes.Extensions;
using Testes.Main;

namespace Testes
{
    class Program
    {
        public static void Main (string[] args)
        {
            try
            {
                var serviceCollection = ConfigureServices();
                IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
                var eventService = serviceProvider.GetRequiredService<MainAppTest>();
                eventService.Execute();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("Teste terminado com sucesso!");
        }

        static IServiceCollection ConfigureServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDependencies();
            return serviceCollection;
        }

    }
}