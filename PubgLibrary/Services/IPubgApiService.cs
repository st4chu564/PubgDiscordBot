using Discord;
using DiscordBotShared.Interfaces;

namespace PubgLibrary.Services
{
    public interface IPubgApiService
    {
        Task<Embed> GetPlayerLatestMatchAsync(string playerId, bool isPlayerId = false, CancellationToken cancelToken = default);
        Task<Embed> GetPlayerStatsAsync(string playerId, string statName = "kills", bool isPlayerId = false, CancellationToken cancelToken = default);
    }
}