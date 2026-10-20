using AutoMapper;
using MediatR;
using MiniLMS.Application.Common.Interfaces;
using MiniLMS.Application.Common.Models;
using MiniLMS.Application.Features.Courses.DTOs;
using MiniLMS.Application.Features.Courses.Specifications;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Courses.Queries
{
    public class GetCoursesQueryHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
        : IRequestHandler<GetCoursesQuery, PaginatedResult<CourseDto>>
    {
        public async Task<PaginatedResult<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepo<Course, int>();
            var spec = new CoursesPagedSpecifications(request.SpecParams);
            var data = await repo.GetAllWithSpecAsync(spec);

            var totalCount = await repo.GetCountAsync(spec);

            var mappedData = _mapper.Map<IReadOnlyList<CourseDto>>(data);


            return new PaginatedResult<CourseDto>(mappedData, request.SpecParams.PageIndex, request.SpecParams.PageSize, totalCount);

        }
    }
}
