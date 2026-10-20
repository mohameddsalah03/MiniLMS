using MiniLMS.Application.Common.Specifications;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Courses.Specifications
{
    public class CourseByNameSpecifications : BaseSpecifications<Course, int>
    {
        public CourseByNameSpecifications(string titleEn, string titleAr, int? excludeId = null)
            : base(c => c.TitleEn == titleEn && c.TitleAr == titleAr
            && (excludeId == null || c.Id != excludeId))
        {

        }
    }
}
