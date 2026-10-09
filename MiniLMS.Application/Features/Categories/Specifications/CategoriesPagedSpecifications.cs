using MiniLMS.Application.Common.Specifications;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Categories.Specifications
{
    public class CategoriesPagedSpecifications : BaseSpecifications<Category, int>
    {
        public CategoriesPagedSpecifications(int PageIndex, int PageSize)
            : base()
        {
            ApplyOrderBy(c => c.Id);
            ApplyPagination(PageIndex, PageSize);
        }
    }
}
