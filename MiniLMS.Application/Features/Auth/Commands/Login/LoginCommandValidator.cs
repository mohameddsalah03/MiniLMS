using FluentValidation;

namespace MiniLMS.Application.Features.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(l => l.Login.Email).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(l => l.Login.Password).NotEmpty();
        }
    }
}
