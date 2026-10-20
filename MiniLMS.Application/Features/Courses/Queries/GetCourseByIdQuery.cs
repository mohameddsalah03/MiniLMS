using MediatR;
using MiniLMS.Application.Features.Courses.DTOs;

namespace MiniLMS.Application.Features.Courses.Queries
{
    public record GetCourseByIdQuery(int Id) : IRequest<CourseDto>;

}
