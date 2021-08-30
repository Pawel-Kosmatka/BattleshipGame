using Battleship.Logic;
using Battleship.Logic.StatusNotification;
using Battleship.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBattleshipGame(this IServiceCollection services)
        {
            services.AddScoped<IGameController, GameController>();
            services.AddSingleton<GameContext>();
            services.AddSingleton<AutoPlay>();
            services.AddSingleton<StatusNotificator>();

            return services;
        }
    }
}
