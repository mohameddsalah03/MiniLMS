namespace MiniLMS.Application.Features.Categories.DTOs;

public record CategoryDto(
    int Id,
    string NameEn,
    string NameAr,
    DateTime CreatedAt
    );