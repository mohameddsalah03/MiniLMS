using MediatR;
using MiniLMS.Application.Features.Auth.DTOs;

namespace MiniLMS.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(string FullName,
        string Email,
        string Password) : IRequest<AuthResponse>
    {
    }
}
