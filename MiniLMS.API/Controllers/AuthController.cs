using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniLMS.Application.Features.Auth.Commands.Login;
using MiniLMS.Application.Features.Auth.Commands.Register;
using MiniLMS.Application.Features.Auth.Queries;

namespace MiniLMS.API.Controllers
{
    public class AuthController(ISender sender) : ApiControllerBase(sender)
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
            => OkResponse(await Sender.Send(command), "Registered successfully");

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
         => OkResponse(await Sender.Send(command), "Logged in successfully");

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
    => OkResponse(await Sender.Send(new GetCurrentUserQuery()));
    }
}
