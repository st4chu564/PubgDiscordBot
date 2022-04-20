namespace DiscordBot.Helpers
{
    public class ConstantsHelper : IConstantsHelper
    {
        private readonly IConfiguration _config;

        public ConstantsHelper(IConfiguration config)
        {
            _config = config;
            urls = config.GetSection("Urls") as Urls;
        }

        public Urls urls { get; init; }
    }
}
