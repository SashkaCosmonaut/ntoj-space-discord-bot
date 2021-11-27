using System;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Threading;
using Discord.WebSocket;
using Discord.Commands;
using NTOJSpaceDiscordBot.Services;

namespace NTOJSpaceDiscordBot
{
    /// <summary>
    /// Настраиваем подключение к Дискорду.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Главная функция программы.
        /// Call the Program constructor, followed by the 
        /// MainAsync method and wait until it finishes (which should be never).
        /// </summary>
        public static void Main() 
            => new Program().MainAsync().GetAwaiter().GetResult();

        /// <summary>
        /// Главная асинхронная функция программы. 
        /// Discord.Net heavily utilizes TAP for async, 
        /// so we create an asynchronous context from the beginning.
        /// </summary>
        /// <returns>Программа работает вечно, пока не будет закрыта.</returns>
        public async Task MainAsync()
        {
            // You should dispose a service provider created using ASP.NET
            // when you are finished using it, at the end of your app's lifetime.
            // Dependency injection handles calling Dispose for us.
            // It is recommended to Dispose of a DiscordSocketClient when you are finished
            // using it, at the end of your app's lifetime.
            using var services = ConfigureServices();

            var client = services.GetRequiredService<DiscordSocketClient>();
            var commandService = services.GetRequiredService<CommandService>();

            await services.GetRequiredService<LoggingService>().InitializeAsync();

            // Tokens should be considered secret data and never hard-coded.
            // We can read from the environment variable to avoid hard coding.
            await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("NTOJSpaceBotToken"));
            await client.StartAsync();

            // Here we initialize the logic required to register our commands.
            await services.GetRequiredService<CommandHandlingService>().InitializeAsync();

            await Task.Delay(Timeout.Infinite);
        }

        /// <summary>
        /// Подключаем сервисы, используем инверсию зависимостей.
        /// Setup your DI container.
        /// </summary>
        /// <returns>Провайдер сервисов, который осдержит все подключенные сервисы.</returns>
        private ServiceProvider ConfigureServices()
        {
            var discordSocketConfig = new DiscordSocketConfig
            {
                // How much logging do you want to see?
                LogLevel = LogSeverity.Info,

                // If you or another service needs to do anything with messages
                // (eg. checking Reactions, checking the content of edited/deleted messages),
                // you must set the MessageCacheSize. You may adjust the number as needed.
                // MessageCacheSize = 50,
            };

            var commandServiceConfig = new CommandServiceConfig
            {
                // Again, log level:
                LogLevel = LogSeverity.Info,

                // There's a few more properties you can set,
                // for example, case-insensitive commands.
                CaseSensitiveCommands = false                
            };

            return new ServiceCollection()

                // Repeat this for all the service classes
                // and other dependencies that your commands might need.
                .AddSingleton(new DiscordSocketClient(discordSocketConfig))
                .AddSingleton(new CommandService(commandServiceConfig))
                .AddSingleton<LoggingService>()
                .AddSingleton<CommandHandlingService>()

                // When all your required services are in the collection, build the container.
                // Tip: There's an overload taking in a 'validateScopes' bool to make sure
                // you haven't made any mistakes in your dependency graph.
                .BuildServiceProvider();
        }
    }
}
