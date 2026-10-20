using MediatR;

namespace MiniLMS.Application.Features.Courses.Commands.DeleteCourse
{
    public record DeleteCourseCommand(int Id) : IRequest
    {
    }
}
