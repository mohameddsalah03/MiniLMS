using MiniLMS.Application.Common.Specifications;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Categories.Specifications
{
    public class CategoryByIdSpecifications : BaseSpecifications<Category, int>
    {
        public CategoryByIdSpecifications(int id)
            : base(c => c.Id == id)
        { }
    }
}
