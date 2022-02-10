using Discord;

namespace DiscordBot.Services
{
    public interface IPubgApiService
    {
        Task<Embed> GetPlayerLatestMatch(string playerId, bool isPlayerId = false, CancellationToken cancelToken = default);
        Task<Embed> GetPlayerStats(string playerId, string statName = "kills", bool isPlayerId = false, CancellationToken cancelToken = default);
    }
}