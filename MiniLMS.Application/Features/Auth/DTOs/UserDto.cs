namespace MiniLMS.Application.Features.Auth.DTOs
{
    public record UserDto(
        string Id,
        string FullName,
        string Email,
        IReadOnlyList<string> Roles
        )
    {

    }
}
