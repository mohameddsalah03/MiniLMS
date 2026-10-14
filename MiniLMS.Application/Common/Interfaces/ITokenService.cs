using MiniLMS.Application.Common.Models;
using MiniLMS.Application.Features.Auth.DTOs;

namespace MiniLMS.Application.Common.Interfaces
{
    public interface ITokenService
    {
        public AccessToken Generate(UserDto user);
    }
}
