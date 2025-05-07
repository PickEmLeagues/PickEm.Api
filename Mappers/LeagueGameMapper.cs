using PickEm.Api.Domain;
using PickEm.Api.Dto;

namespace PickEm.Api.Mappers;

public static class LeagueGameMapper
{
    public static LeagueGameDto MapToDto(this LeagueGame gameLeague)
    {
        if (gameLeague == null)
        {
            return null;
        }

        return new LeagueGameDto
        {
            Id = gameLeague.Id,
            PicksClosed = gameLeague.PicksClosed,
            Game = gameLeague.Game.MapToDto(),
            Picks = gameLeague.Picks.MapToDto().ToList()
        };
    }


    public static IEnumerable<LeagueGameDto> MapToDto(this IEnumerable<LeagueGame> gameLeagues)
    {
        if (gameLeagues == null)
        {
            return null;
        }

        return gameLeagues.Select(gl => gl.MapToDto());
    }
}