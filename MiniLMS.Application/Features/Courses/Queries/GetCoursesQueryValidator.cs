using FluentValidation;

namespace MiniLMS.Application.Features.Courses.Queries
{
    public class GetCoursesQueryValidator : AbstractValidator<GetCoursesQuery>
    {
        public GetCoursesQueryValidator()
        {
            RuleFor(c => c.SpecParams.PageIndex).InclusiveBetween(1, 100000);
            RuleFor(c => c.SpecParams.PageSize).InclusiveBetween(1, 50);
            RuleFor(q => q.SpecParams.Search).MaximumLength(100);
            RuleFor(q => q.SpecParams.Sort).IsInEnum();
            RuleFor(q => q.SpecParams.CategoryId).GreaterThan(0);
        }
    }
}
