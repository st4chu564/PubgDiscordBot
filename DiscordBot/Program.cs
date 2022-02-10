using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Commands;
using DiscordBot.Handlers;
using DiscordBot.Services;

public class Program
{
    private DiscordSocketClient _client;
    private readonly ConfigurationManager _config = new ConfigurationManager();
    public async static Task Main(string[] args)
    {
        var services = new ServiceCollection();
        var provider = ConfigureServices(services);

        ((ConfigurationManager)provider.GetService<IConfiguration>()).AddJsonFile("appsettings.json");
        await provider.GetService<IDiscordService>().StartAsync();
        return;
    }

    private static ServiceProvider ConfigureServices(ServiceCollection services)
    {
        return services
            .AddSingleton<IConfiguration, ConfigurationManager>()
            .AddSingleton<CommandService, CommandService>()
            .AddSingleton<IPubgApiService, PubgApiService>()
            .AddSingleton<IDiscordService, DiscordService>()
            .AddSingleton<ICommandHandler, CommandHandler>()
            .AddSingleton<PubgCommandsModule, PubgCommandsModule>()
            .AddSingleton<CommandsModule, CommandsModule>()
            .BuildServiceProvider();
    }
}