using MediatR;
using MiniLMS.Application.Common.Interfaces;
using MiniLMS.Application.Features.Auth.DTOs;

namespace MiniLMS.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler(IIdentityService _identityService, ITokenService _tokenService)
        : IRequestHandler<LoginCommand, AuthResponse>
    {
        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.ValidateCredentialsAsync(request.Email, request.Password);
            var token = _tokenService.Generate(user);
            return new AuthResponse(token.Token, token.ExpiresAt, user);
        }
    }
}
