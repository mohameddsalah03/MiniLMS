using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniLMS.API.Models;

namespace MiniLMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase(ISender sender) : ControllerBase
    {
        protected ISender Sender { get; } = sender;

        protected IActionResult OkResponse<T>(T data, string? message = null)
            => Ok(BaseResponse<T>.Success(data, message));

        protected IActionResult CreatedResponse<T>(string actionName, object routeValues, T data, string? message = null)
           => CreatedAtAction(actionName, routeValues, BaseResponse<T>.Success(data, message));

    }
}
