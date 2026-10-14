namespace MiniLMS.Application.Features.Auth.DTOs
{
    public record RegisterDto(
        string FullName,
        string Email,
        string Password
        )
    {
    }
}
