using PickEm.Api.Domain;
using PickEm.Api.Dto;

namespace PickEm.Api.Mappers;

public static class UserMapper
{
    public static UserDto MapToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            IsDeleted = user.IsDeleted,
            OwnedLeagues = user.OwnedLeagues.MapToDto(),
            MemberLeagues = user.MemberLeagues.MapToDto()
        };
    }

    public static IEnumerable<UserDto> MapToDto(this IEnumerable<User> users)
    {
        return users.Select(user => user.MapToDto());
    }
}