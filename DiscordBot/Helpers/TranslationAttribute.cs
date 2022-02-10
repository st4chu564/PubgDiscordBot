namespace DiscordBot.Helpers
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class TranslationAttribute : Attribute
    {
        private string translation;

        public string Translation => translation;

        public TranslationAttribute(string translation)
        {
            this.translation = translation;
        }
    }
}
