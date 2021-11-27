using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace NTOJSpaceDiscordBot.Services
{
    /// <summary>
    /// Сервис обработки команд боту.
    /// </summary>
    public class CommandHandlingService
    {
        private readonly CommandService _commandService;
        private readonly DiscordSocketClient _discordClient;
        private readonly IServiceProvider _services;

        /// <summary>
        /// Конструктор сервиса.
        /// </summary>
        /// <param name="services">Keep DI container around for use with commands.</param>
        public CommandHandlingService(IServiceProvider services)
        {
            _commandService = services.GetRequiredService<CommandService>();
            _discordClient = services.GetRequiredService<DiscordSocketClient>();

            _services = services;

            _discordClient.MessageReceived += MessageReceivedAsync;

            _commandService.CommandExecuted += CommandExecutedAsync;
        }

        /// <summary>
        /// Инициализация сервиса.
        /// You also need to pass your 'IServiceProvider' instance now,
        /// so make sure that's done before you get here.
        /// </summary>
        /// <returns>Асинхронная операция.</returns>
        public async Task InitializeAsync()
        {
            // Register modules that are public and inherit ModuleBase<T>.
            // Either search the program and add all Module classes that can be found.
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

            // Or add Modules manually if you prefer to be a little more explicit:
            // await _commands.AddModuleAsync<SomeModule>(_services);
        }

        /// <summary>
        /// Hook MessageReceived so we can process each message to see if it qualifies as a command.
        /// </summary>
        /// <param name="rawMessage">Объект сообщения.</param>
        /// <returns>Асинхронная операция.</returns>
        public async Task MessageReceivedAsync(SocketMessage rawMessage)
        {
            // Ignore system messages, or messages from other bots
            if (!(rawMessage is SocketUserMessage message) || message.Source != MessageSource.User) return;

            // This value holds the offset where the prefix ends
            var argPos = 0;

            // Perform prefix check. You may want to replace this with
            // (!message.HasMentionPrefix(_discord.CurrentUser, ref argPos))
            // for a less traditional command format like !help.
            if (!message.HasCharPrefix('!', ref argPos)) return;

            var context = new SocketCommandContext(_discordClient, message);

            // Perform the execution of the command. In this method,
            // the command service will perform precondition and parsing check
            // then execute the command if one is matched.
            await _commandService.ExecuteAsync(context, argPos, _services);

            // Note that normally a result will be returned by this format, but here
            // we will handle the result in CommandExecutedAsync,
        }

        /// <summary>
        /// Hook CommandExecuted to handle post-command-execution logic.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="context">Контекст команды.</param>
        /// <param name="result">Результат выполнения команды.</param>
        /// <returns>Асинхронная операция.</returns>
        public async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            // command is unspecified when there was a search failure (command not found); we don't care about these errors
            if (!command.IsSpecified)
                return;

            // the command was successful, we don't care about this result, unless we want to log that a command succeeded.
            if (result.IsSuccess)
                return;

            // the command failed, let's notify the user that something happened.
            await context.Channel.SendMessageAsync($"error: {result}");
        }
    }
}
