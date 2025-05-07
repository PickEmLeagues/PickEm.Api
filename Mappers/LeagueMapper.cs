
using PickEm.Api.Domain;
using PickEm.Api.Dto;

namespace PickEm.Api.Mappers;

public static class LeagueMapper
{
    public static LeagueDto MapToDto(this League league)
    {
        if (league == null)
        {
            return null;
        }

        return new LeagueDto
        {
            Id = league.Id,
            Name = league.Name,
            Sport = league.Sport.MapToDto(),
            CreatedAt = league.CreatedAt,
            UpdatedAt = league.UpdatedAt,
            IsDeleted = league.IsDeleted,
            IsPublic = league.IsPublic,
            OwnerId = league.OwnerId,
            Schedule = league.Schedule.MapToDto()
        };
    }

    public static IEnumerable<LeagueDto> MapToDto(this IEnumerable<League> leagues)
    {
        if (leagues == null)
        {
            return null;
        }

        return leagues.Select(l => l.MapToDto());
    }
}