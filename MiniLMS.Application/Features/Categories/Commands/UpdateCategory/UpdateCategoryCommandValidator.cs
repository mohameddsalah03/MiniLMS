using FluentValidation;

namespace MiniLMS.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(command => command.NameEn).NotEmpty().MaximumLength(100);
            RuleFor(command => command.NameAr).NotEmpty().MaximumLength(100);
        }
    }
}
