using MediatR;
using MiniLMS.Application.Common.Models;
using MiniLMS.Application.Features.Courses.DTOs;

namespace MiniLMS.Application.Features.Courses.Queries
{
    public record GetCoursesQuery(CourseSpecParams SpecParams) : IRequest<PaginatedResult<CourseDto>>;
}
