using API.Calendar.DTOs;
using API.Calendar.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Calendar.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController, Authorize(Roles = "Instructor")]
    public class CalenderController : ControllerBase
    {
        private readonly ICalendarRepository calendarRepository;

        public CalenderController(ICalendarRepository calendarRepository)
        {
            this.calendarRepository = calendarRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int classroomId)
        {
            var events = await calendarRepository.GetAsync(classroomId);

            return Ok(JsonConvert.SerializeObject(events));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddEventDTO addEventDTO)
        {
            var addAsyncResult = await calendarRepository.AddAsync(addEventDTO);

            if (addAsyncResult == null)
            {
                return NotFound(JsonConvert.SerializeObject(
                "There is no user in this University with this Id."));
            }
            if (addAsyncResult.Value > 0)
            {
                return Ok(JsonConvert.SerializeObject(new EventDTO(addEventDTO) { Id = addAsyncResult.Value }));
            }

            return BadRequest();
        }
        
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EventDTO eventDTO)
        {
            var updateAsyncResult = await calendarRepository.UpdateAsync(eventDTO);

            if (updateAsyncResult == null)
            {
                return NotFound(JsonConvert.SerializeObject(
                "There is no user in this University with this Id."));
            }

            if (updateAsyncResult.Value)
            {
                return Ok(JsonConvert.SerializeObject(eventDTO));
            }

            return BadRequest();
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> Remove([FromRoute] int eventId)
        {
            var removeAsyncResult = await calendarRepository.RemoveAsync(eventId);

            if (removeAsyncResult == null)
            {
                return NotFound(JsonConvert.SerializeObject(
                "There is no user in this University with this Id."));
            }

            if (removeAsyncResult.Value)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}