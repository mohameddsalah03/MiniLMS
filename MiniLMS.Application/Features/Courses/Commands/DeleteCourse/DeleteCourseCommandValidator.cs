using FluentValidation;

namespace MiniLMS.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommand>
    {
        public DeleteCourseCommandValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0);
        }
    }
}
