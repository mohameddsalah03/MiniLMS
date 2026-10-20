using AutoMapper;
using MiniLMS.Application.Features.Courses.Commands.CreateCourse;
using MiniLMS.Application.Features.Courses.Commands.UpdateCourse;
using MiniLMS.Application.Features.Courses.DTOs;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Courses.Mappings
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.CategoryNameAr, opt => opt.MapFrom(src => src.Category.NameAr))
                .ForMember(dest => dest.CategoryNameEn, opt => opt.MapFrom(src => src.Category.NameEn))
                .ForMember(dest => dest.InstructorFullName, opt => opt.MapFrom(src => src.Instructor.FullName));

            CreateMap<CreateCourseCommand, Course>();
            CreateMap<UpdateCourseCommand, Course>()
                .ForMember(c => c.Id, opt => opt.Ignore());
        }


    }
}
