using FluentValidation;

namespace MiniLMS.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(c => c.TitleEn).NotEmpty().MaximumLength(200);
            RuleFor(c => c.TitleAr).NotEmpty().MaximumLength(200);
            RuleFor(c => c.DescriptionEn).NotEmpty().MaximumLength(2000);
            RuleFor(c => c.DescriptionAr).NotEmpty().MaximumLength(2000);
            RuleFor(c => c.CategoryId).GreaterThan(0);
        }
    }
}