using AutoMapper;
using MiniLMS.Application.Features.Categories.Commands.CreateCategory;
using MiniLMS.Application.Features.Categories.Commands.UpdateCategory;
using MiniLMS.Application.Features.Categories.DTOs;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Application.Features.Categories.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryCommand, Category>();
        CreateMap<UpdateCategoryCommand, Category>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

    }
}