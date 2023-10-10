using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.UseCases.Users.Commands;
using UserManagement.Application.UseCases.Users.Queries;
using UserManagement.Domain.DTO;
using UserManagement.Domain.Entities;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            var result = await _sender.Send(new GetUsersQuery());

            return Ok(result);
        }

        [HttpPost("createuser")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDTO user)
        {
            var result = await _sender.Send(new CreateUserCommand(user));

            return Ok(result);
        }

        [HttpPost("createuserdevice")]
        public async Task<IActionResult> CreateUserDeviceAsync([FromBody] CreateUserDeviceDTO userDevice)
        {
            var result = await _sender.Send(new CreateUserDeviceCommand(userDevice));

            return Ok(result);
        }
    }
}
