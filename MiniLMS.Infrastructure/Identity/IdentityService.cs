using Microsoft.AspNetCore.Identity;
using MiniLMS.Application.Common.Exceptions;
using MiniLMS.Application.Common.Interfaces;
using MiniLMS.Application.Features.Auth.DTOs;
using MiniLMS.Domain.Constants;
using MiniLMS.Domain.Entities;
namespace MiniLMS.Infrastructure.Identity
{
    internal sealed class IdentityService(
        UserManager<ApplicationUser> _userManager,
        IUnitOfWork _unitOfWork)
        : IIdentityService
    {
        public async Task<UserDto> RegisterStudentAsync(string FullName,
        string Email,
        string Password)
        {
            var existsEmail = await _userManager.FindByEmailAsync(Email);
            if (existsEmail != null)
                throw new ConflictException("This email is already registered.");

            var user = new ApplicationUser
            {
                Email = Email,
                FullName = FullName,
                UserName = Email
            };

            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                ThrowIfFailed(await _userManager.CreateAsync(user, Password));
                ThrowIfFailed(await _userManager.AddToRoleAsync(user, Roles.Student));

            });


            return new UserDto(user.Id, user.FullName, user.Email, new[] { Roles.Student });


        }


        public async Task<UserDto> ValidateCredentialsAsync(string Email, string Password)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user is null)
                throw new UnauthorizedException("Invalid email or password.");

            if (await _userManager.IsLockedOutAsync(user))
                throw new UnauthorizedException("Account is temporarily locked.");

            if (!await _userManager.CheckPasswordAsync(user, Password))
            {
                await _userManager.AccessFailedAsync(user);
                throw new UnauthorizedException("Invalid email or password.");
            }

            if (await _userManager.GetAccessFailedCountAsync(user) > 0)
                await _userManager.ResetAccessFailedCountAsync(user);

            var roles = await _userManager.GetRolesAsync(user);
            return new UserDto(user.Id, user.FullName, user.Email!, roles.ToArray());
        }
        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new UnauthorizedException("User no longer exists.");
            var roles = await _userManager.GetRolesAsync(user);
            return new UserDto(user.Id, user.FullName, user.Email!, roles.ToArray());

        }

        private static void ThrowIfFailed(IdentityResult result)
        {
            if (result.Succeeded)
                return;

            // أكواد Identity: PasswordTooShort / PasswordRequiresDigit ... وDuplicateUserName / InvalidEmail ...
            var errors = result.Errors
                .GroupBy(e => e.Code.StartsWith("Password") ? "Password" : "Email")
                .ToDictionary(g => g.Key, g => g.Select(e => e.Description).ToArray());

            throw new ValidationException(errors);
        }

        public async Task<bool> IsUserInRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user is not null && await _userManager.IsInRoleAsync(user, role);
        }
    }
}
