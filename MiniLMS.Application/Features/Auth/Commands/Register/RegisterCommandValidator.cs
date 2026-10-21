using FluentValidation;

namespace MiniLMS.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(r => r.FullName).NotEmpty().MaximumLength(100);
            RuleFor(r => r.Email).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(r => r.Password).NotEmpty();
        }
    }
}
