using Battleship;
using Battleship.Logic;
using ConsoleApp.Output;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = BuildServiceProvider();

            var game = serviceProvider.GetRequiredService<BattleshipGamePlay>();

            await game.PlayAsync();
           
        }

        private static ServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddBattleshipGame();
            services.AddSingleton<BattleshipGamePlay>();
            services.AddSingleton<ConsoleWriter>();
        }
    }
}
