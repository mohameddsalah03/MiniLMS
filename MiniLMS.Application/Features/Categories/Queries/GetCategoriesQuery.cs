using MediatR;
using MiniLMS.Application.Common.Models;
using MiniLMS.Application.Features.Categories.DTOs;

namespace MiniLMS.Application.Features.Categories.Queries
{
    public record GetCategoriesQuery(int PageIndex = 1, int PageSize = 10)
        : IRequest<PaginatedResult<CategoryDto>>
    {
    }
}
