using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Commands.Classess;
using TaskManagementSystem.Application.Queries.Handlers;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Interface.Dtos;

namespace TaskManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] TokenRequestCommand tokenRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var authModel = await _mediator.Send(tokenRequest);
            return Ok(authModel);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterCommand register)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var authModel = await _mediator.Send(register);
            return Ok(authModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addrole")]
        public async Task<IActionResult> AddUserRole([FromBody] AddRoleCommand addRoleModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var roleModel = await _mediator.Send(addRoleModel);

            return Ok(roleModel);
        }
    }
}
