using AutoMapper;
using MediatR;
using MiniLMS.Application.Common.Exceptions;
using MiniLMS.Application.Common.Interfaces;
using MiniLMS.Application.Features.Courses.DTOs;
using MiniLMS.Application.Features.Courses.Specifications;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Courses.Queries
{
    public class GetCourseByIdQueryHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
        : IRequestHandler<GetCourseByIdQuery, CourseDto>
    {
        public async Task<CourseDto> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new CourseByIdSpecifications(request.Id);
            var course = await _unitOfWork.GetRepo<Course, int>().GetWithSpecAsync(spec) ?? throw new NotFoundException($"Course with id {request.Id} was not found.");
            return _mapper.Map<CourseDto>(course);
        }
    }
}
