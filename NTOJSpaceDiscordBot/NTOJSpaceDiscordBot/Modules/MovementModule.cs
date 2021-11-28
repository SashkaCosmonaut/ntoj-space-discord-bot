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

        //[Command("forward")]
        //[Alias("w")]
        [Command("2")]
        public async Task Forward()
        {
            var result = await _serialPortService.SendCommandAsync(SerialPortService.FORWARD);

            await ReplyAsync($"{Context.User} двигается вперёд: {result}");
        }

        //[Command("back")]
        //[Alias("s")]
        [Command("8")]
        public async Task Back()
        {
            var result = await _serialPortService.SendCommandAsync(SerialPortService.BACK);

            await ReplyAsync($"{Context.User} двигается назад: {result}");
        }

        //[Command("left")]
        //[Alias("a")]
        [Command("4")]
        public async Task Left()
        {
            var result = await _serialPortService.SendCommandAsync(SerialPortService.LEFT);

            await ReplyAsync($"{Context.User} двигается влево: {result}");
        }

        //[Command("right")]
        //[Alias("d")]
        [Command("6")]
        public async Task Right()
        {
            var result = await _serialPortService.SendCommandAsync(SerialPortService.RIGHT);

            await ReplyAsync($"{Context.User} двигается вправо: {result}");
        }

        //[Command("stop")]
        //[Alias("x")]
        [Command("5")]
        public async Task Stop()
        {
            var result = await _serialPortService.SendCommandAsync(SerialPortService.STOP);

            await ReplyAsync($"{Context.User} сам не двигается: {result}");
        }
    }
}
