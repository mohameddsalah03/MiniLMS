using MediatR;
using MiniLMS.Application.Common.Exceptions;
using MiniLMS.Application.Common.Interfaces;
using MiniLMS.Application.Features.Courses.Specifications;
using MiniLMS.Domain.Constants;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommandHandler(
        IUnitOfWork _unitOfWork,
        ICurrentUserService _currentUser)
        : IRequestHandler<DeleteCourseCommand>
    {
        public async Task Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepo<Course, int>();

            var course = await repo.GetByIdAsync(request.Id)
                ?? throw new NotFoundException($"Course with id {request.Id} was not found.");

            // Ownership: الأدمن يمسح أي كورس، والمدرّس كورساته هو بس
            if (!_currentUser.IsInRole(Roles.Admin) && course.InstructorId != _currentUser.UserId)
                throw new ForbiddenException("You can only delete your own courses.");

            var hasEnrollments = await _unitOfWork.GetRepo<Enrollment, int>()
                .AnyAsync(new EnrollmentsInCourseSpecifications(request.Id));
            if (hasEnrollments)
                throw new ConflictException("Cannot delete a course that has enrolled students.");

            repo.Delete(course);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}