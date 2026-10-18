namespace MiniLMS.Application.Features.Courses.DTOs;

public record CourseDto(
    int Id,
    string TitleEn,
    string TitleAr,
    string DescriptionEn,
    string DescriptionAr,
    string? ImageUrl,
    int CategoryId,
    string CategoryNameEn,
    string CategoryNameAr,
    string InstructorId,
    string InstructorFullName,
    DateTime CreatedAt
    );