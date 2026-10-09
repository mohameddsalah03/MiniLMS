using MediatR;
using MiniLMS.Application.Features.Categories.DTOs;

namespace MiniLMS.Application.Features.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string NameEn, string NameAr) : IRequest<CategoryDto>;