using MediatR;
using MiniLMS.Application.Common.Interfaces;
using MiniLMS.Application.Features.Auth.DTOs;

namespace MiniLMS.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler(IIdentityService _identityService, ITokenService _tokenService)
        : IRequestHandler<RegisterCommand, AuthResponse>
    {


        public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.RegisterStudentAsync(request.FullName, request.Email, request.Password);
            var token = _tokenService.Generate(user);

            return new AuthResponse(token.Token, token.ExpiresAt, user);
        }

    }
}
