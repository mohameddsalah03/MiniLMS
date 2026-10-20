using MiniLMS.Application.Common.Specifications;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Courses.Specifications
{
    public class CourseByIdSpecifications : BaseSpecifications<Course, int>
    {
        public CourseByIdSpecifications(int id) : base(c => c.Id == id)
        {
            AddInclude(c => c.Instructor);
            AddInclude(c => c.Category);

        }
    }
}
