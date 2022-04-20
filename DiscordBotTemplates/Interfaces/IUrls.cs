using Microsoft.Extensions.Configuration;

namespace DiscordBotShared.Interfaces
{
    public interface IUrls
    {
        public void Initialize(IConfiguration config);
    }
}