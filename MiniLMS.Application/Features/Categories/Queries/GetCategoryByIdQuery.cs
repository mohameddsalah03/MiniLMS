using MediatR;
using MiniLMS.Application.Features.Categories.DTOs;

namespace MiniLMS.Application.Features.Categories.Queries
{
    public record GetCategoryByIdQuery(int Id) : IRequest<CategoryDto>
    {

    }
}
