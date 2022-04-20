using Discord.Commands;

namespace DiscordBot.Commands
{
    [Group("basic")]
    public class CommandsModule : ModuleBase<CommandContext>, ICommandsModule
    {

        [Command("say")]
        [Summary("Returns what you entered")]
        [Alias("Powtorz")]
        public async Task RepeatAsync([Remainder] [Summary("Enter text to repeat")]
        string text)
        {
            await ReplyAsync(text);
        }
    }
}
