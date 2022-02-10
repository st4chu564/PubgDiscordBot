using DiscordBot.Helpers;
using System.Reflection;

namespace DiscordBot.Models.Pubg
{
    public enum GameModesEnum
    {
        [EnumName("duo")]
        [Translation("Duo TPP")]
        Duo,
        [EnumName("duo-fpp")]
        [Translation("Duo FPP")]
        DuoFpp,
        [EnumName("solo")]
        [Translation("Solo TPP")]
        Solo,
        [EnumName("solo-fpp")]
        [Translation("Solo FPP")]
        SoloFpp,
        [EnumName("squad")]
        [Translation("Squad TPP")]
        Squad,
        [EnumName("squad-fpp")]
        [Translation("Squad FPP")]
        SquadFpp
    }

    public static class GameModesEnumExtensions
    {
        public static string GetEnumName(this Enum value)
        {
            Type type = value.GetType();

            string? name = Enum.GetName(type, value);

            if (name != null)
            {
                FieldInfo? field = type.GetField(name);
                if (field != null)
                {
                    EnumNameAttribute? attr = Attribute.GetCustomAttribute(field,
                        typeof(EnumNameAttribute)) as EnumNameAttribute;

                    if (attr != null)
                    {
                        return attr.Name;
                    }
                }
            }
            return null;
        }

        public static GameModesEnum GetGameModeEnumValueByString(string name)
        {
            Type type = typeof(GameModesEnum);

            return (GameModesEnum)type.GetFields().FirstOrDefault(field => field.GetCustomAttribute<EnumNameAttribute>()?.Name == name).GetValue(type);
        }
    }
}
