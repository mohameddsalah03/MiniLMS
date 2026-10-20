using MediatR;
using MiniLMS.Application.Features.Categories.DTOs;
using System.Text.Json.Serialization;

namespace MiniLMS.Application.Features.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(string NameEn, string NameAr) : IRequest<CategoryDto>
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}
