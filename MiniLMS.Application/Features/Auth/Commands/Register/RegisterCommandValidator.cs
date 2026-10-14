using FluentValidation;

namespace MiniLMS.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(r => r.Register.FullName).NotEmpty().MaximumLength(100);
            RuleFor(r => r.Register.Email).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(r => r.Register.Password).NotEmpty();
        }
    }
}
