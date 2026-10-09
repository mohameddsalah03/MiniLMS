using AutoMapper;
using MediatR;
using MiniLMS.Application.Common.Exceptions;
using MiniLMS.Application.Common.Interfaces;
using MiniLMS.Application.Features.Categories.DTOs;
using MiniLMS.Application.Features.Categories.Specifications;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
        : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepo<Category, int>();
            var category = await repo.GetByIdAsync(request.Id);

            if (category == null)
                throw new NotFoundException($"category with this {request.Id} not exist!");

            var spec = new CategoryByNamesSpecifications(request.NameEn, request.NameAr, request.Id);
            var duplicate = await repo.AnyAsync(spec);
            if (duplicate)
                throw new ConflictException("Another category with the same name already exists.");

            _mapper.Map(request, category);
            // not using update from repo cause cahnge tracker?? 
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);



        }
    }
}
