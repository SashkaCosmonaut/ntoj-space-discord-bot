using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using NTOJSpaceDiscordBot.Services;
using System;
using System.Threading.Tasks;

namespace NTOJSpaceDiscordBot.Modules
{
    /// <summary>
    /// Модуль обработки сигналов для движения корабля.
    /// Modules must be public and inherit from an IModuleBase.
    /// </summary>
    public class MovementModule : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// Сервис последовательного порта.
        /// </summary>
        private readonly SerialPortService _serialPortService;

        /// <summary>
        /// Конструктор модуля.
        /// </summary>
        /// <param name="services">Контейнер сервисов.</param>
        public MovementModule(IServiceProvider services)
        {
            _serialPortService = services.GetRequiredService<SerialPortService>();
        }

        [Command("forward")]
        public async Task Forward()
        {
            var result = await _serialPortService.SendForwardAsync();

            await ReplyAsync($"{Context.User} двигается вперёд: {result}");
        }

        [Command("back")]
        public async Task Back()
        {
            await ReplyAsync($"{Context.User} двигается назад");
        }

        [Command("left")]
        public async Task Left()
        {
            await ReplyAsync($"{Context.User} двигается влево");
        }

        [Command("right")]
        public async Task Right()
        {
            await ReplyAsync($"{Context.User} двигается вправо");
        }
    }
}
