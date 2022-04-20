using Discord.Commands;
using DiscordBot.Commands;
using DiscordBot.Handlers;
using DiscordBot.Services;
using DiscordBotShared.Interfaces;
using System.Linq;
using System.Reflection;

namespace DiscordBot
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();

            var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            var toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase) && 
            !r.Contains("Discord.", StringComparison.InvariantCultureIgnoreCase) && 
            !r.Contains("DiscordBot.", StringComparison.InvariantCultureIgnoreCase) &&
            !r.Contains("System", StringComparison.InvariantCultureIgnoreCase) &&
            !r.Contains("NewtonSoft", StringComparison.InvariantCultureIgnoreCase)).ToList();

            toLoad.ForEach(path => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path))));

            var services = new ServiceCollection();
            var provider = ConfigureServices(services);

            ((ConfigurationManager)provider.GetService<IConfiguration>()).AddJsonFile("appsettings.json");
            await provider.GetService<IDiscordService>().StartAsync();
            return;
        }

        private static ServiceProvider ConfigureServices(ServiceCollection services)
        {
            var assemblyServices = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(a => a.Name.Contains("ApiService") && a.IsClass)
                .Select(a => new { Interface = a.GetInterface($"I{a.Name}"), Implementation = a })
                .ToList();

            foreach (var service in assemblyServices)
            {
                services.AddScoped(service.Interface, service.Implementation);
            }

            var baseCommandModule = typeof(CommandsModule);
            var assemblyCommandsModules = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(a => a.Name.Contains("CommandsModule") && a.IsClass && a != baseCommandModule)
                .Select(a => new { Interface = a.GetInterface($"I{a.Name}"), Implementation = a })
                .ToList();

            foreach(var commandModule in assemblyCommandsModules)
            {
                services.AddScoped(commandModule.Interface, commandModule.Implementation);
            }

            var urlsInterfaceType = typeof(IUrls);
            var assemblyUrls = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(a => urlsInterfaceType.IsAssignableFrom(a) && a.IsClass)
                .Select(a => new { Interface = urlsInterfaceType, Implementation = a })
                .ToList();

            foreach(var urlList in assemblyUrls)
            {
                services.AddScoped(urlList.Interface, urlList.Implementation);
            }

            return services
                .AddScoped<IConfiguration, ConfigurationManager>()
                .AddScoped<CommandService, CommandService>()
                .AddScoped<IDiscordService, DiscordService>()
                .AddScoped<ICommandHandler, CommandHandler>()
                .AddScoped<ICommandsModule, CommandsModule>()
                .BuildServiceProvider();
        }

        private static IEnumerable<Assembly> GetDependentAssemblies(Assembly analyzedAssembly)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => GetNamesOfAssembliesReferencedBy(a)
                                    .Contains(analyzedAssembly.FullName));
        }

        public static IEnumerable<string> GetNamesOfAssembliesReferencedBy(Assembly assembly)
        {
            return assembly.GetReferencedAssemblies()
                .Select(assemblyName => assemblyName.FullName);
        }
    }
}