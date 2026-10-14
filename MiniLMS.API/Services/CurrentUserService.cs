using MiniLMS.Application.Common.Interfaces;
using System.Security.Claims;

namespace MiniLMS.API.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        public string? UserId => httpContextAccessor.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public bool IsInRole(string role)
            => httpContextAccessor.HttpContext?.User?.IsInRole(role) ?? false;

    }
}
