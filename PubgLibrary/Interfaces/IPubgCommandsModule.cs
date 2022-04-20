namespace PubgLibrary.Interfaces
{
    public interface IPubgCommandsModule
    {
        Task GetBasicInfo(string statName);
        Task GetLatestMatch();
        Task GetPlayerInfo(string playerName = null);
        Task GetSeasonStats(string seasonNumber = null);
        Task Help();
    }
}