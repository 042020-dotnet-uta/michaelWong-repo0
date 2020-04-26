//Michael Wong
//Will Ruiz
//Ryan Oxford

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace RPS_Game
{
    class Program
    {
        static void Main(string[] args)
        {

            var services = new ServiceCollection().AddLogging(logging => logging.AddConsole())
                .AddTransient<GameSystem>();
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                GameSystem newGame = serviceProvider.GetService<GameSystem>(); //Creates a new RPS game.
                newGame.Run(); //Starts the game simulation.
            }

        }

    }

}
