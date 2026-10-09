using FluentValidation;

namespace MiniLMS.Application.Features.Categories.Queries
{
    public class GetCategoriesQueryValidator : AbstractValidator<GetCategoriesQuery>
    {
        public GetCategoriesQueryValidator()
        {
            RuleFor(c => c.PageIndex).InclusiveBetween(1, 100000);
            RuleFor(c => c.PageSize).InclusiveBetween(1, 50);

        }
    }
}
