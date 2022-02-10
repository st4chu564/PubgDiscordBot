using System.Collections.Generic;
using System.Reflection;

namespace DiscordBot.Helpers
{
    public static class ClassPropertyNameHelper<T> where T : class
    {
        public static IEnumerable<string> GetProperties()
        {
            Type classType = typeof(T);

            var properties = classType.GetProperties().Select(prop => prop.Name);

            return properties;
        }

        public static Dictionary<string, string> GetPropertiesWithTranslation()
        {
            Type classType = typeof(T);

            var properties = classType.GetProperties()
                .Select(prop => new { prop.Name, prop.GetCustomAttribute<TranslationAttribute>().Translation})
                .ToDictionary(prop => prop.Name, prop => prop.Translation);

            return properties;
        }
    }
}
