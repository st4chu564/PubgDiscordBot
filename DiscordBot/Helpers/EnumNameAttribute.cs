namespace DiscordBot.Helpers
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumNameAttribute : Attribute
    {
        private readonly string name;

        public string Name => name;

        public EnumNameAttribute(string name)
        {
            this.name = name;
        }
    }
}
