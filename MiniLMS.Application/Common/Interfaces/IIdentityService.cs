using MiniLMS.Application.Features.Auth.DTOs;

namespace MiniLMS.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<UserDto> RegisterStudentAsync(string FullName, string Email, string Password);
        Task<UserDto> ValidateCredentialsAsync(string Email, string Password);
        Task<UserDto> GetUserByIdAsync(string userId);
        Task<bool> IsUserInRoleAsync(string userId, string role);
    }
}
