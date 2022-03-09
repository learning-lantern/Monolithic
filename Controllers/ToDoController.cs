using API.Data.DTOs;
using API.Repositories.ToDo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
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
        public async Task<IActionResult> Get([FromQuery] string userId, [FromQuery] string? filter)
        {
            var tasks = await toDoRepository.GetAsync(userId, filter);

            return Ok(JsonConvert.SerializeObject(tasks));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddTaskDTO addTaskDTO)
        {
            var addAsyncResult = await toDoRepository.AddAsync(addTaskDTO);

            if (addAsyncResult is null)
            {
                return NotFound(JsonConvert.SerializeObject(
                "There is no user in this University whith this Id."));
            }
            if (addAsyncResult.Value > 0)
            {
                return Ok(JsonConvert.SerializeObject(new TaskDTO(addTaskDTO) { Id = addAsyncResult.Value }));
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TaskDTO taskDTO)
        {
            var updateAsyncResult = await toDoRepository.UpdateAsync(taskDTO);

            if (updateAsyncResult is null)
            {
                return NotFound(JsonConvert.SerializeObject(
                "There is no user in this University whith this Id."));
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
            var removeAsyncResult = await toDoRepository.RemoveAsync(taskId);

            if (removeAsyncResult is null)
            {
                return NotFound(JsonConvert.SerializeObject(
                "There is no user in this University whith this Id."));
            }

            if (removeAsyncResult.Value)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
