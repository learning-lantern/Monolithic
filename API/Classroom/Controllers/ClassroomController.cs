using API.Classroom.DTOs;
using API.Classroom.Repositories;
using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace API.Classroom.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController, Authorize]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomRepository classroomRepository;

        public ClassroomController(IClassroomRepository classroomRepository)
        {
            this.classroomRepository = classroomRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int classroomId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var classroom = await classroomRepository.GetAsync(classroomId, userId);

            return classroom == null ? BadRequest(JsonConvert.SerializeObject(Message.ClassroomNotFound)) :
                Ok(JsonConvert.SerializeObject(classroom));
        }

        [HttpPost, Authorize(Roles = Role.UniversityAdmin)]
        public async Task<IActionResult> Add([FromBody] AddClassroomDTO addClassroomDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var addAsyncResult = await classroomRepository.AddAsync(addClassroomDTO, userId);

            if (addAsyncResult == null)
            {
                return NotFound(JsonConvert.SerializeObject(Message.UserIdNotFound));
            }

            if (addAsyncResult.Value > 0)
            {
                return Ok(JsonConvert.SerializeObject(new ClassroomDTO(addClassroomDTO) { Id = addAsyncResult.Value }));
            }

            return BadRequest();
        }

        [HttpPost, Authorize(Roles = Role.UniversityAdmin + ", " + Role.Instructor)]
        public async Task<IActionResult> AddUser([FromQuery] int classroomId, [FromQuery] string userId)
        {
            var addUserAsyncResult = await classroomRepository.AddUserAsync(classroomId, userId);

            if (addUserAsyncResult == null)
            {
                return NotFound(JsonConvert.SerializeObject(Message.UserIdNotFound));
            }

            if (addUserAsyncResult.Value)
            {
                return Ok(JsonConvert.SerializeObject(Message.AddClassroomUser(userId, classroomId)));
            }

            return BadRequest();
        }

        [HttpPut, Authorize(Roles = Role.UniversityAdmin)]
        public async Task<IActionResult> Update([FromBody] ClassroomDTO classroomDTO)
        {
            throw new NotImplementedException();
        }

        [HttpDelete, Authorize(Roles = Role.UniversityAdmin)]
        public async Task<IActionResult> Remove([FromQuery] int classroomId)
        {
            throw new NotImplementedException();
        }
    }
}
