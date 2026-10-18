using FluentValidation;

namespace MiniLMS.Application.Features.Courses.Queries
{
    public class GetCourseByIdQueryValidator : AbstractValidator<GetCourseByIdQuery>
    {
        public GetCourseByIdQueryValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0);
        }

    }
}
