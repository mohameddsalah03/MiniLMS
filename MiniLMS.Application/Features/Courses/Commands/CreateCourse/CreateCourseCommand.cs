using MediatR;
using MiniLMS.Application.Features.Courses.DTOs;

namespace MiniLMS.Application.Features.Courses.Commands.CreateCourse
{
    public record CreateCourseCommand(
        string TitleEn,
        string TitleAr,
        string DescriptionEn,
        string DescriptionAr,
        int CategoryId,
        string? InstructorId = null // عشان فيه اتنين رول يضيفو الادمن هيحتاج وقتها يجيبه من الداتا بيز || بس لو الانستراكتور اللي هيضيف مش هيحتاجه هيحتاج التوكن
        )
        : IRequest<CourseDto>;

}
