using MediatR;
using MiniLMS.Application.Features.Auth.DTOs;

namespace MiniLMS.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(RegisterDto Register) : IRequest<AuthResponse>
    {
    }
}
