using MiniLMS.Application.Features.Auth.DTOs;

namespace MiniLMS.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<UserDto> RegisterStudentAsync(RegisterDto register);
        Task<UserDto> ValidateCredentialsAsync(LoginDto login);
        Task<UserDto> GetUserByIdAsync(string userId);
        Task<bool> IsUserInRoleAsync(string userId, string role);
    }
}
