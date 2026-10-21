using MediatR;
using MiniLMS.Application.Features.Auth.DTOs;

namespace MiniLMS.Application.Features.Auth.Commands.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<AuthResponse>
    {
    }
}
