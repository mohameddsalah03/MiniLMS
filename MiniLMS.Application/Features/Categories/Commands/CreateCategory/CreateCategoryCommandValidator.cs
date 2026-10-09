using FluentValidation;

namespace MiniLMS.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(command => command.NameEn).NotEmpty().MaximumLength(100);
        RuleFor(command => command.NameAr).NotEmpty().MaximumLength(100);
    }
}