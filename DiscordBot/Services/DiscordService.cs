using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Commands;
using DiscordBot.Handlers;

namespace DiscordBot.Services
{
    public class DiscordService : IDiscordService
    {
        private readonly DiscordSocketClient _client;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _serviceProvider;

        public DiscordService(IServiceProvider serviceProvider, IConfiguration config)
        {
            _config = config;
            _serviceProvider = serviceProvider;
            _client = new DiscordSocketClient();
            _client.Log += Log;

        }
        public async Task StartAsync()
        {
            var commandsService = new CommandService();

            await commandsService.AddModuleAsync<CommandsModule>(_serviceProvider);

            var handler = new CommandHandler(_client, commandsService);

            await handler.InstallCommandsAsync(_serviceProvider);

            string pubgToken = _config.GetValue<string>("DiscordApiKey");

            await _client.LoginAsync(TokenType.Bot, pubgToken);
            await _client.StartAsync();

            await Task.Delay(-1);

        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
