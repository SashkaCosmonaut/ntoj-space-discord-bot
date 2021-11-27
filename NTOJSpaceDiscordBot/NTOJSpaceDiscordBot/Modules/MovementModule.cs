using Discord.Commands;
using System.Threading.Tasks;

namespace NTOJSpaceDiscordBot.Modules
{
    /// <summary>
    /// Модуль обработки сигналов для движения корабля.
    /// Modules must be public and inherit from an IModuleBase.
    /// </summary>
    public class MovementModule : ModuleBase<SocketCommandContext>
    {
        [Command("forward")]
        public async Task Forward()
        {
            await ReplyAsync($"{Context.User} двигается вперёд");
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
