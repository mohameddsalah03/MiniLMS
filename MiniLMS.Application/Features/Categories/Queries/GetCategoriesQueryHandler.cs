using AutoMapper;
using MediatR;
using MiniLMS.Application.Common.Interfaces;
using MiniLMS.Application.Common.Models;
using MiniLMS.Application.Features.Categories.DTOs;
using MiniLMS.Application.Features.Categories.Specifications;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Categories.Queries
{
    public class GetCategoriesQueryHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
        : IRequestHandler<GetCategoriesQuery, PaginatedResult<CategoryDto>>
    {
        async Task<PaginatedResult<CategoryDto>> IRequestHandler<GetCategoriesQuery, PaginatedResult<CategoryDto>>.Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepo<Category, int>();
            var spec = new CategoriesPagedSpecifications(request.PageIndex, request.PageSize);
            var data = await repo.GetAllWithSpecAsync(spec);

            var totalCount = await repo.GetCountAsync(spec);
            var mappedData = _mapper.Map<IReadOnlyList<CategoryDto>>(data);
            return new PaginatedResult<CategoryDto>(mappedData, request.PageIndex, request.PageSize, totalCount);

        }
    }
}
