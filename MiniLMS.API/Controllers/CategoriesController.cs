using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniLMS.Application.Features.Categories.Commands.CreateCategory;
using MiniLMS.Application.Features.Categories.Commands.DeleteCategory;
using MiniLMS.Application.Features.Categories.Commands.UpdateCategory;
using MiniLMS.Application.Features.Categories.Queries;

namespace MiniLMS.API.Controllers
{
    public class CategoriesController(ISender sender) : ApiControllerBase(sender)
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageIndex, [FromQuery] int pageSize)
        => OkResponse(await Sender.Send(new GetCategoriesQuery(pageIndex, pageSize)));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        => OkResponse(await Sender.Send(new GetCategoryByIdQuery(id)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
        {
            var category = await Sender.Send(command);
            return CreatedResponse(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryCommand command)
        => OkResponse(await Sender.Send(command with { Id = id }));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Sender.Send(new DeleteCategoryCommand(id));
            return NoContent();
        }




    }
}
