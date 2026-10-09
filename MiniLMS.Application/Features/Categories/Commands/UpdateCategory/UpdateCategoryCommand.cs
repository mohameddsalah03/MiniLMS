using MediatR;
using MiniLMS.Application.Features.Categories.DTOs;

namespace MiniLMS.Application.Features.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(int Id, string NameEn, string NameAr) : IRequest<CategoryDto>
    {

    }
}
