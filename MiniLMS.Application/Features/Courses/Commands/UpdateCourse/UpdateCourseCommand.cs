using MediatR;
using MiniLMS.Application.Features.Courses.DTOs;
using System.Text.Json.Serialization;

namespace MiniLMS.Application.Features.Courses.Commands.UpdateCourse
{
    public record UpdateCourseCommand(
        string TitleEn,
        string TitleAr,
        string DescriptionEn,
        string DescriptionAr,
        int CategoryId
        )
        : IRequest<CourseDto>
    {
        [JsonIgnore]
        public int Id { get; init; }
    }


}
