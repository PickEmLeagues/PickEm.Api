using PickEm.Api.Domain;
using PickEm.Api.Dto;

namespace PickEm.Api.Mappers;

public static class GameLeagueMapper
{
    public static GameLeagueDto MapToDto(this GameLeague gameLeague)
    {
        if (gameLeague == null)
        {
            return null;
        }

        return new GameLeagueDto
        {
            Id = gameLeague.Id,
            PicksClosed = gameLeague.PicksClosed,
            Game = gameLeague.Game.MapToDto(),
            Picks = gameLeague.Picks.MapToDto().ToList()
        };
    }


    public static IEnumerable<GameLeagueDto> MapToDto(this IEnumerable<GameLeague> gameLeagues)
    {
        if (gameLeagues == null)
        {
            return null;
        }

        return gameLeagues.Select(gl => gl.MapToDto());
    }
}