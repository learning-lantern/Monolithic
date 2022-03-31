using API.Helpers;
using API.ToDo.DTOs;
using API.ToDo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace API.ToDo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController, Authorize]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoRepository toDoRepository;

        public ToDoController(IToDoRepository toDoRepository)
        {
            this.toDoRepository = toDoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? list)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var tasks = await toDoRepository.GetAsync(userId, list);

            return Ok(JsonConvert.SerializeObject(tasks));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddTaskDTO addTaskDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var addAsyncResult = await toDoRepository.AddAsync(addTaskDTO, userId);

            if (addAsyncResult == null)
            {
                return NotFound(JsonConvert.SerializeObject(Message.UserIdNotFound));
            }
            if (addAsyncResult.Value > 0)
            {
                return Ok(JsonConvert.SerializeObject(new TaskDTO(addTaskDTO) { Id = addAsyncResult.Value, UserId = userId }));
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TaskDTO taskDTO)
        {
            taskDTO.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var updateAsyncResult = await toDoRepository.UpdateAsync(taskDTO);

            if (updateAsyncResult == null)
            {
                return NotFound(JsonConvert.SerializeObject(Message.TaskNotFound));
            }

            if (updateAsyncResult.Value)
            {
                return Ok(JsonConvert.SerializeObject(taskDTO));
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Remove([FromQuery] int taskId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var removeAsyncResult = await toDoRepository.RemoveAsync(taskId, userId);

            if (removeAsyncResult == null)
            {
                return NotFound(JsonConvert.SerializeObject(Message.TaskNotFound));
            }

            if (removeAsyncResult.Value)
            {
                return Ok(JsonConvert.SerializeObject(Message.TaskDeleted));
            }

            return BadRequest();
        }
    }
}
