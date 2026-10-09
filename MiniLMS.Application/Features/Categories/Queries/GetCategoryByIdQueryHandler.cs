using AutoMapper;
using MediatR;
using MiniLMS.Application.Common.Exceptions;
using MiniLMS.Application.Common.Interfaces;
using MiniLMS.Application.Features.Categories.DTOs;
using MiniLMS.Application.Features.Categories.Specifications;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Categories.Queries
{
    public class GetCategoryByIdQueryHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new CategoryByIdSpecifications(request.Id);
            var data = await _unitOfWork.GetRepo<Category, int>().GetWithSpecAsync(spec);
            if (data == null)
                throw new NotFoundException($"Category with id {request.Id} was not found.");
            return _mapper.Map<CategoryDto>(data);
        }
    }
}
