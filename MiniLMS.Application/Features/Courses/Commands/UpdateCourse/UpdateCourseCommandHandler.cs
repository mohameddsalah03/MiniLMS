using AutoMapper;
using MediatR;
using MiniLMS.Application.Common.Exceptions;
using MiniLMS.Application.Common.Interfaces;
using MiniLMS.Application.Features.Courses.DTOs;
using MiniLMS.Application.Features.Courses.Specifications;
using MiniLMS.Domain.Constants;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandHandler(
        IUnitOfWork _unitOfWork,
        IMapper _mapper,
        ICurrentUserService _currentUser
        ) : IRequestHandler<UpdateCourseCommand, CourseDto>

    {
        public async Task<CourseDto> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {

            var repo = _unitOfWork.GetRepo<Course, int>();
            var course = await repo.GetByIdAsync(request.Id) ?? throw new NotFoundException($"Course By : {request.Id} Not Found To Update It.");

            if (!_currentUser.IsInRole(Roles.Admin) && course.InstructorId != _currentUser.UserId)
                throw new ForbiddenException("You can only modify your own courses.");

            var category = await _unitOfWork.GetRepo<Category, int>().GetByIdAsync(request.CategoryId)
                ?? throw new ValidationException(new Dictionary<string, string[]>
                {
                    [nameof(request.CategoryId)] = new[]
                    {
                        $"Category with id {request.CategoryId} does not exist."
                    }
                });

            var spec = new CourseByNameSpecifications(request.TitleEn, request.TitleAr, request.Id);
            var duplicate = await repo.AnyAsync(spec);
            if (duplicate)
                throw new ConflictException("Another course with the same title already exists.");
            _mapper.Map(request, course);
            await _unitOfWork.SaveChangesAsync();

            var updated = await repo.GetWithSpecAsync(new CourseByIdSpecifications(request.Id));
            return _mapper.Map<CourseDto>(updated);

        }
    }


}
