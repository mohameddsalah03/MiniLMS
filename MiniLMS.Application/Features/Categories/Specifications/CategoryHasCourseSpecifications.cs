using MiniLMS.Application.Common.Specifications;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Categories.Specifications
{
    public class CoursesInCategorySpecifications : BaseSpecifications<Course, int>
    {
        public CoursesInCategorySpecifications(int categoryId)
            : base(course => course.CategoryId == categoryId)
        {

        }
    }
}
