using DiscordBot.Helpers;

namespace DiscordBot.Models.Pubg
{
    public class GameModeStatsModel
    {
        [Translation("Asysty")]
        public int Assists { get; set; } = 0;

        [Translation("Boosty")]
        public int Boosts { get; set; } = 0;

        [Translation("Powalenia bez dobicia")]
        public int Dbnos { get; set; } = 0;

        [Translation("Dzienne zabójstwa")]
        public int DailyKills { get; set; } = 0;

        [Translation("Dzienne wygrane")]
        public int DailyWins { get; set; } = 0;

        [Translation("Zadane obrażenia")]
        public float DamageDealt { get; set; } = 0;

        [Translation("Dni")]
        public int Days { get; set; } = 0;

        [Translation("Zabójstwa hedami")]
        public int HeadshotKills { get; set; } = 0;

        [Translation("Leczenia")]
        public int Heals { get; set; } = 0;

        [Translation("Punkty zabójstw")]
        public int KillPoints { get; set; } = 0;

        [Translation("Zabójstwa")]
        public int Kills { get; set; } = 0;

        [Translation("Najdłuższe zabójstwo")]
        public float LongestKill { get; set; } = 0;

        [Translation("Najdłuższy czas przetrwania")]
        public int LongestTimeSurvived { get; set; } = 0;

        [Translation("Przegrane")]
        public int Losses { get; set; } = 0;

        [Translation("Najdłuższa seria zabójstw")]
        public int MaxKillStreaks { get; set; } = 0;

        [Translation("Najdłuższy czas przetrwania")]
        public float MostSurvivalTime { get; set; } = 0;

        [Translation("Punkty rankingowe")]
        public int RankPoints { get; set; } = 0;

        [Translation("Tytuł rankignowy")]
        public string RankPointsTitle { get; set; } = string.Empty;

        [Translation("Ożywnieia")]
        public int Revives { get; set; } = 0;

        [Translation("Przejechany dystans")]
        public float RideDistance { get; set; } = 0;

        [Translation("Zabójstwa pojazdami")]
        public int RoadKills { get; set; } = 0;

        [Translation("Najwięcej zabójstw w rundzie")]
        public int RoundMostKills { get; set; } = 0;

        [Translation("Rozegrane rundy")]
        public int RoundsPlayed { get; set; } = 0;

        [Translation("Samobójstwa")]
        public int Suicides { get; set; } = 0;

        [Translation("Przepłynięty dystans")]
        public float SwimDistances { get; set; } = 0;

        [Translation("Zabójstw członków drużyny")]
        public int TeamKills { get; set; } = 0;

        [Translation("Czas przetrwania")]
        public int TimeSurived { get; set; } = 0;

        [Translation("Top 10tek")]
        public int Top10s { get; set; } = 0;

        [Translation("Zniszczone pojazdy")]
        public int VehicleDestroys { get; set; } = 0;

        [Translation("Dystans pokonany z buta")]
        public double WalkDistance { get; set; } = 0;

        [Translation("Zanelzione bronie")]
        public int WeaponsAcquired { get; set; } = 0;

        [Translation("Tygodniowe zabójstwa")]
        public int WeeklyKills { get; set; } = 0;

        [Translation("Punkty wgranych")]
        public int WinPoints { get; set; } = 0;

        [Translation("Wygrane")]
        public int Wins { get; set; } = 0;
    }


}
