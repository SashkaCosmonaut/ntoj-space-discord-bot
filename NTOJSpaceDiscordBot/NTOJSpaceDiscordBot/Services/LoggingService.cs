using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace NTOJSpaceDiscordBot.Services
{
    /// <summary>
    /// Сервис для логгирования события Дискорда.
    /// </summary>
    public class LoggingService
    {
        private DiscordSocketClient _client;

        /// <summary>
        /// Настраиваем логирования клиента и команд.
        /// </summary>
        /// <param name="client">Клиент Дискорда.</param>
        /// <param name="commandService">Сервис команд Дискордаю</param>
        public LoggingService(DiscordSocketClient client, CommandService commandService)
        {
            _client = client;

            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;
            _client.MessageReceived += Client_MessageReceived;

            commandService.Log += LogAsync;
        }

        private Task Client_MessageReceived(SocketMessage arg)
        {
            if (arg.Author.Id == _client.CurrentUser.Id)
                return Task.CompletedTask;

            Console.WriteLine($"{arg.Author}, {arg.CreatedAt}, {arg.Channel}, {arg.Content}");

            return Task.CompletedTask;
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

        /// <summary>
        /// The Ready event indicates that the client has opened a
        /// connection and it is now safe to access the cache.
        /// </summary>
        /// <returns>Успешная асинхронная операция.</returns>
        private Task ReadyAsync()
        {
            Console.WriteLine("The Bot is connected!");

            return Task.CompletedTask;
        }
    }
}
