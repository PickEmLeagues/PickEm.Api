using PickEm.Api.Domain;
using PickEm.Api.Dto;

namespace PickEm.Api.Mappers;

public static class SportInfoMapper
{
    public static SportInfoDto MapToDto(this Sport sport)
    {
        if (sport == null)
        {
            return null;
        }

        return new SportInfoDto
        {
            Id = sport.Id,
            Name = sport.Name,
            Season = sport.Season,
            Week = sport.Week,
            StartDate = sport.StartDate,
            EndDate = sport.EndDate
        };
    }

    public static IEnumerable<SportInfoDto> MapToDto(this IEnumerable<Sport> sports)
    {
        if (sports == null)
        {
            return null;
        }

        return sports.Select(s => s.MapToDto());
    }
}

