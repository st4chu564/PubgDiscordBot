using Discord.Commands;

namespace DiscordBot.Commands
{
    public interface ICommandsModule
    {
        Task RepeatAsync([Remainder, Summary("Enter text to repeat")] string text);
    }
}