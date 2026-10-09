using AutoMapper;
using MediatR;
using MiniLMS.Application.Common.Exceptions;
using MiniLMS.Application.Common.Interfaces;
using MiniLMS.Application.Features.Categories.DTOs;
using MiniLMS.Application.Features.Categories.Specifications;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
        : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var spec = new CategoryByNamesSpecifications(request.NameEn, request.NameAr);
            var exist = await _unitOfWork.GetRepo<Category, int>().AnyAsync(spec);
            if (exist)
                throw new ConflictException($"this category already Exist!");

            var categrymapped = _mapper.Map<Category>(request);
            await _unitOfWork.GetRepo<Category, int>().AddAsync(categrymapped);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CategoryDto>(categrymapped);
        }
    }


}
