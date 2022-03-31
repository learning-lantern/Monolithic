using API.Calendar.DTOs;
using API.Calendar.Repositories;
using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace API.Calendar.Controllers
{

    [Route("api/Classroom/[controller]/[action]")]
    [ApiController, Authorize]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarRepository calendarRepository;

        public CalendarController(ICalendarRepository calendarRepository)
        {
            this.calendarRepository = calendarRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int classroomId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var events = await calendarRepository.GetAsync(classroomId, userId);

            if (events == null)
            {
                return NotFound(JsonConvert.SerializeObject(Message.ClassroomNotFound));
            }

            return Ok(JsonConvert.SerializeObject(events));
        }

        [HttpPost, Authorize(Roles = Role.Instructor)]
        public async Task<IActionResult> Add([FromBody] AddEventDTO addEventDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var addAsyncResult = await calendarRepository.AddAsync(addEventDTO, userId);

            if (addAsyncResult == null)
            {
                return NotFound(JsonConvert.SerializeObject(Message.ClassroomNotFound));
            }

            if (addAsyncResult.Value != 0)
            {
                return Ok(JsonConvert.SerializeObject(new EventDTO(addEventDTO) { Id = addAsyncResult.Value }));
            }

            return BadRequest();
        }
        
        [HttpPut, Authorize(Roles = Role.Instructor)]
        public async Task<IActionResult> Update([FromBody] EventDTO eventDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var updateAsyncResult = await calendarRepository.UpdateAsync(eventDTO, userId);

            if (updateAsyncResult == null)
            {
                return NotFound(JsonConvert.SerializeObject(Message.ClassroomNotFound));
            }

            if (updateAsyncResult.Value)
            {
                return Ok(JsonConvert.SerializeObject(eventDTO));
            }

            return BadRequest();
        }

        [HttpDelete, Authorize(Roles = Role.Instructor)]
        public async Task<IActionResult> Remove([FromQuery] int eventId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var removeAsyncResult = await calendarRepository.RemoveAsync(eventId, userId);

            if (removeAsyncResult == null)
            {
                return NotFound(JsonConvert.SerializeObject(Message.ClassroomNotFound));
            }

            if (removeAsyncResult.Value)
            {
                return Ok(JsonConvert.SerializeObject(Message.EventDeleted));
            }

            return BadRequest();
        }
    }
}