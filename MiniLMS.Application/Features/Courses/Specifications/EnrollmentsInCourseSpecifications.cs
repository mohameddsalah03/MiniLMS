using MiniLMS.Application.Common.Specifications;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Courses.Specifications
{
    public class EnrollmentsInCourseSpecifications : BaseSpecifications<Enrollment, int>

    {
        public EnrollmentsInCourseSpecifications(int courseId)
            : base(e => e.CourseId == courseId)
        {

        }
    }
}
