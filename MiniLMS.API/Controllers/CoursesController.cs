using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniLMS.Application.Features.Courses.Commands.CreateCourse;
using MiniLMS.Application.Features.Courses.Commands.DeleteCourse;
using MiniLMS.Application.Features.Courses.Commands.UpdateCourse;
using MiniLMS.Application.Features.Courses.DTOs;
using MiniLMS.Application.Features.Courses.Queries;
using MiniLMS.Domain.Constants;

namespace MiniLMS.API.Controllers
{
    public class CoursesController(ISender sender) : ApiControllerBase(sender)
    {
        [HttpGet]
        public async Task<IActionResult> GetCourses([FromQuery] CourseSpecParams specParams)
            => OkResponse(await Sender.Send(new GetCoursesQuery(specParams)));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
            => OkResponse(await Sender.Send(new GetCourseByIdQuery(id)));

        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseCommand command)
        {
            var course = await Sender.Send(command);
            return CreatedResponse(nameof(GetById), new { id = course.Id }, course);
        }

        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCourseCommand command)
            => OkResponse(await Sender.Send(command with { Id = id }), "Course Updated Successfully!");

        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Sender.Send(new DeleteCourseCommand(id));
            return NoContent();
        }

    }
}
