using PickEm.Api.Domain;
using PickEm.Api.Dto;

namespace PickEm.Api.Mappers;

public static class GameMapper
{
    public static GameDto MapToDto(this Game game)
    {
        if (game == null)
        {
            return null;
        }

        return new GameDto
        {
            Id = game.Id,
            SportId = game.SportId,
            Week = game.Week,
            HomeTeam = game.HomeTeam,
            AwayTeam = game.AwayTeam,
            StartTime = game.StartTime,
            CreatedAt = game.CreatedAt,
            UpdatedAt = game.UpdatedAt,
            AwayOdds = game.AwayOdds,
            HomeOdds = game.HomeOdds,
            DrawOdds = game.DrawOdds,
            HomeScore = game.HomeScore,
            AwayScore = game.AwayScore,
            IsFinal = game.IsFinal,
            OddsClosed = game.OddsClosed,
        };
    }

    public static IEnumerable<GameDto> MapToDto(this IEnumerable<Game> games)
    {
        if (games == null)
        {
            return null;
        }

        return games.Select(g => g.MapToDto());
    }
}