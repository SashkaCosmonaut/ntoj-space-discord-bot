using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace NTOJSpaceDiscordBot
{
    public class Program
    {
        public static void Main() 
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            using var services = ConfigureServices();
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .BuildServiceProvider();
        }
    }
}
