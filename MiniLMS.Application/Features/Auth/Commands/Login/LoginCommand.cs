using MediatR;
using MiniLMS.Application.Features.Auth.DTOs;

namespace MiniLMS.Application.Features.Auth.Commands.Login
{
    public record LoginCommand(LoginDto Login) : IRequest<AuthResponse>
    {
    }
}
