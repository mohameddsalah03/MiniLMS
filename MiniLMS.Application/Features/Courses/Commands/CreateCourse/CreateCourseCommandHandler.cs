using AutoMapper;
using MediatR;
using MiniLMS.Application.Common.Exceptions;
using MiniLMS.Application.Common.Interfaces;
using MiniLMS.Application.Features.Courses.DTOs;
using MiniLMS.Application.Features.Courses.Specifications;
using MiniLMS.Domain.Constants;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandHandler(
        IUnitOfWork _unitOfWork,
        IMapper _mapper,
        ICurrentUserService _currentUser,
        IIdentityService _identityService)
        : IRequestHandler<CreateCourseCommand, CourseDto>
    {
        public async Task<CourseDto> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            string instructorId;


            if (_currentUser.IsInRole(Roles.Admin))
            {
                if (string.IsNullOrWhiteSpace(request.InstructorId) ||
                    !await _identityService.IsUserInRoleAsync(request.InstructorId, Roles.Instructor))
                {
                    throw new ValidationException(new Dictionary<string, string[]>
                    {
                        [nameof(request.InstructorId)] = new[] { "InstructorId must be the id of an existing instructor." }
                    });
                }

                instructorId = request.InstructorId;
            }
            else
            {
                // Instructor: 
                instructorId = _currentUser.UserId
                    ?? throw new UnauthorizedException("User is not authenticated.");
            }

            var category = await _unitOfWork.GetRepo<Category, int>().GetByIdAsync(request.CategoryId);
            if (category is null)
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    [nameof(request.CategoryId)] = new[]
                    {
                        $"Category with id {request.CategoryId} does not exist."
                    }
                });

            var repo = _unitOfWork.GetRepo<Course, int>();
            var spec = new CourseByNameSpecifications(request.TitleEn, request.TitleAr);
            var exists = await repo.AnyAsync(spec);
            if (exists)
                throw new ConflictException("A course with the same title already exists.");

            var course = _mapper.Map<Course>(request);
            course.InstructorId = instructorId;

            await repo.AddAsync(course);
            await _unitOfWork.SaveChangesAsync();

            var created = await repo.GetWithSpecAsync(new CourseByIdSpecifications(course.Id));
            return _mapper.Map<CourseDto>(created);
        }
    }
}