using MediatR;
using MiniLMS.Application.Features.Auth.DTOs;

namespace MiniLMS.Application.Features.Auth.Queries
{
    public record GetCurrentUserQuery : IRequest<UserDto>;

}
