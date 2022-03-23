using API.Calendar.DTOs;
using API.Calendar.Repositories;
using API.ToDo.DTOs;
using API.ToDo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Calendar.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CalenderController : Controller
    {
        private readonly ICalendarRepository calendarRepository;

        public CalenderController(ICalendarRepository calendarRepository)
        {
            this.calendarRepository = calendarRepository;
        }

        [HttpGet("/api/Classroom/{classroomId}/[controller]")]
        public async Task<IActionResult> Get([FromRoute] int classroomId)
        {
            var events = await calendarRepository.GetAllEventsAsync(classroomId);

            return Ok(JsonConvert.SerializeObject(events));
        }

        [HttpPost("/api/Classroom/{classroomId}/[controller]")]
        public async Task<IActionResult> Add([FromRoute] int classroomId, [FromBody] AddEventDTO newEvent)
        {
            var result = await calendarRepository.AddAsync(classroomId, newEvent);

            if (result > 0)
            {
                return Ok(JsonConvert.SerializeObject(new EventDTO(result, newEvent)));
            }

            return BadRequest();
        }
        
        [HttpPut("{eventId}")]
        public async Task<IActionResult> Update([FromRoute] int eventId, [FromBody] AddEventDTO eventDTO)
        {
            var update = await calendarRepository.UpdateAsync(eventId, eventDTO);

            if (!update)
            {
                return NotFound(JsonConvert.SerializeObject(
                    "There is no event in this classroom with this Id."));
            }

            return Ok(JsonConvert.SerializeObject(eventDTO));
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> Remove([FromRoute] int eventId)
        {
            var remove = await calendarRepository.RemoveAsync(eventId);

            if (!remove)
            {
                return NotFound(JsonConvert.SerializeObject(
                    "There is no event in this classroom with this Id."));
            }

            return Ok();
        }
    }
}