using System;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Threading;

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

            await Task.Delay(Timeout.Infinite);
        }

        /// <summary>
        /// Подключаем сервисы, используем инверсию зависимостей.
        /// </summary>
        /// <returns>Провайдер сервисов, который осдержит все подключенные сервисы.</returns>
        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .BuildServiceProvider();
        }

        /// <summary>
        /// Логгировать событие Дискорда.
        /// </summary>
        /// <param name="log">Объект сообщения Дискорда.</param>
        /// <returns>Успешная асинхронная операция.</returns>
        private Task LogAsync(LogMessage log)
        {
            Console.Write(log.ToString());

            return Task.CompletedTask;
        }
    }
}
