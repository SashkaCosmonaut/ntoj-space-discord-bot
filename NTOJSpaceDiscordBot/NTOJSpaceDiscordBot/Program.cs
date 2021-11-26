using System;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Threading;
using Discord.WebSocket;
using Discord.Commands;

namespace NTOJSpaceDiscordBot
{
    /// <summary>
    /// Настраиваем подключение к Дискорду.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Главная функция программы.
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

            // Настраиваем логирования клиента и команд
            client.Log += LogAsync;
            commandService.Log += LogAsync;

            // Tokens should be considered secret data and never hard-coded.
            // We can read from the environment variable to avoid hard coding.
            await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("NTOJSpaceBotToken"));
            await client.StartAsync();

            await Task.Delay(Timeout.Infinite);
        }

        /// <summary>
        /// Подключаем сервисы, используем инверсию зависимостей.
        /// </summary>
        /// <returns>Провайдер сервисов, который осдержит все подключенные сервисы.</returns>
        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .BuildServiceProvider();
        }

        /// <summary>
        /// Логгировать событие Дискорда.
        /// </summary>
        /// <param name="log">Объект сообщения Дискорда.</param>
        /// <returns>Успешная асинхронная операция.</returns>
        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());

            return Task.CompletedTask;
        }
    }
}
