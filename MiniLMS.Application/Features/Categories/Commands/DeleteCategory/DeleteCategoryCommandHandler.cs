using MediatR;
using MiniLMS.Application.Common.Exceptions;
using MiniLMS.Application.Common.Interfaces;
using MiniLMS.Application.Features.Categories.Specifications;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler(IUnitOfWork _unitOfWork)
        : IRequestHandler<DeleteCategoryCommand>
    {

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepo<Category, int>();
            var category = await repo.GetByIdAsync(request.Id);
            if (category == null)
                throw new NotFoundException($"category with this {request.Id} not exist!");

            var spec = new CoursesInCategorySpecifications(request.Id);
            var hasCourse = await _unitOfWork.GetRepo<Course, int>().AnyAsync(spec);
            if (hasCourse)
                throw new ConflictException("Cannot delete a category that has courses.");

            repo.Delete(category);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
