using PickEm.Api.Domain;
using PickEm.Api.Dto;

namespace PickEm.Api.Mappers;

public static class PickMapper
{
    public static PickDto MapToDto(this Pick pick)
    {
        if (pick == null)
        {
            return null;
        }

        return new PickDto
        {
            Id = pick.Id,
            UserId = pick.UserId,
            GameLeagueId = pick.GameLeagueId,
            Wager = pick.Wager,
            TeamType = pick.TeamType,
            CreatedAt = pick.CreatedAt,
            UpdatedAt = pick.UpdatedAt,
            IsDeleted = pick.IsDeleted
        };
    }

    public static IEnumerable<PickDto> MapToDto(this IEnumerable<Pick> picks)
    {
        if (picks == null)
        {
            return null;
        }

        return picks.Select(p => p.MapToDto());
    }
}