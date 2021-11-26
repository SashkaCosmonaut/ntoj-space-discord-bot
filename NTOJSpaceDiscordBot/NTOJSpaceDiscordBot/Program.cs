using System;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Threading;

namespace NTOJSpaceDiscordBot
{
    public class Program
    {
        public static void Main() 
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            using var services = ConfigureServices();

            await Task.Delay(Timeout.Infinite);
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .BuildServiceProvider();
        }

        private Task LogAsync(LogMessage log)
        {
            Console.Write(log.ToString());

            return Task.CompletedTask;
        }
    }
}
