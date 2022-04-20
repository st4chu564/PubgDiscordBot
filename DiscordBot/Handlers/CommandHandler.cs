using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Commands;

namespace DiscordBot.Handlers
{
    public class CommandHandler : ICommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private IServiceProvider _services;

        public CommandHandler(DiscordSocketClient client, CommandService commands)
        {
            _client = client;
            _commands = commands;
        }

        public async Task InstallCommandsAsync(IServiceProvider services = null)
        {
            _client.MessageReceived += HandleCommandAsync;
            _services = services;

            var type = typeof(ICommandsModule);
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(a => a.IsClass && a.Name.Contains("CommandsModule") && !type.IsAssignableFrom(a));

            foreach(var assembly in assemblies)
            {
                await _commands.AddModuleAsync(assembly, _services);
            }
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            if (!(message.HasCharPrefix('/', ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
            message.Author.IsBot)
                return;

            if (message.Content.StartsWith("/pubg"))
            {
                var context = new CommandContext(_client, message);

                await _commands.ExecuteAsync(
                    context: context,
                    argPos: argPos,
                    services: _services);
            }
            else
            {
                var context = new SocketCommandContext(_client, message);

                await _commands.ExecuteAsync(
                    context: context,
                    argPos: argPos,
                    services: null);
            }

        }
    }
}
