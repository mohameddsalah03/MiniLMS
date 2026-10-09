using MiniLMS.Application.Common.Specifications;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Categories.Specifications
{
    public class CategoryByNamesSpecifications : BaseSpecifications<Category, int>
    {

        // excludeId for update 
        public CategoryByNamesSpecifications(string nameEn, string nameAr, int? excludeId = null)
            : base(
                  c => (c.NameEn == nameEn || c.NameAr == nameAr)
                 && (excludeId == null || c.Id != excludeId)
                  )
        {

        }
    }
}
