
namespace DiscordBot.Handlers
{
    public interface ICommandHandler
    {
        Task InstallCommandsAsync(IServiceProvider services = null);
    }
}