using MiniLMS.Application.Common.Specifications;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Categories.Specifications
{
    public class CategoriesOrderedSpecifications : BaseSpecifications<Category, int>
    {
        public CategoriesOrderedSpecifications()
        {
            ApplyOrderBy(c => c.NameEn);
        }
    }
}
