using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Commands.Classess;
using TaskManagementSystem.Application.Queries.Classess;


namespace TaskManagementSystemAPI.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber=1,[FromQuery] int pageSize=10)
        {
            var query = new GetAllTasksQuery { PageNumber=pageNumber,PageSize = pageSize};
            var tasks = await _mediator.Send(query);
            return Ok(tasks);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {
            var taskId = await _mediator.Send(command);
            return Ok(taskId);
        }

        [HttpPost("gettaskbycode")]
        public async Task<IActionResult> GetTaskByCode([FromBody] GetTaskByCodeCommand command)
        {
            var task = await _mediator.Send(command);
            return Ok(task);
        }
    }
}
