namespace MiniLMS.Application.Features.Auth.DTOs
{
    public record AuthResponse(string Token, DateTime ExpiresAt, UserDto User)
    {

    }
}