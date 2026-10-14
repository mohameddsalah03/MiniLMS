using MediatR;
using MiniLMS.Application.Common.Exceptions;
using MiniLMS.Application.Common.Interfaces;
using MiniLMS.Application.Features.Auth.DTOs;

namespace MiniLMS.Application.Features.Auth.Queries
{
    public class GetCurrentUserQueryHandler(IIdentityService _identityService, ICurrentUserService _currentUserService) : IRequestHandler<GetCurrentUserQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ?? throw new UnauthorizedException("User is not authenticated.");
            var user = await _identityService.GetUserByIdAsync(userId);
            return user;
        }
    }
}
