using PickEm.Api.Domain.Enums;

namespace PickEm.Api.Dto;
public class ScheduleDto
{
    public SportInfoDto SportInfo { get; set; }
    public IEnumerable<ScheduledGameDto> ScheduledGames { get; set; } = Enumerable.Empty<ScheduledGameDto>();
}
