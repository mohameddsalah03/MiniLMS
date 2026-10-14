using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MiniLMS.Application.Common.Interfaces;
using MiniLMS.Application.Common.Models;
using MiniLMS.Application.Features.Auth.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniLMS.Infrastructure.Identity
{
    internal sealed class JwtTokenService(IOptions<JwtSettings> options) : ITokenService
    {
        private readonly JwtSettings _settings = options.Value;

        public AccessToken Generate(UserDto user)
        {
            var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Email, user.Email)
        };
            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresAt = DateTime.UtcNow.AddMinutes(_settings.DurationInMinutes);

            var jwt = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            return new AccessToken(new JwtSecurityTokenHandler().WriteToken(jwt), expiresAt);
        }
    }
}