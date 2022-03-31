using APIs.TextLesson.DTOs;
using APIs.TextLesson.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIs.TextLesson.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController, Authorize]
    public class TextLessonController : ControllerBase
    {
        private readonly ITextLessonRepository TextLessonRepository;

        public TextLessonController(ITextLessonRepository textLessonRepository)
        {
            this.TextLessonRepository = textLessonRepository;
        }

        [HttpGet]
        public  IActionResult Get([FromQuery] int textLessonId)
        {
            var textLesson =  TextLessonRepository.Get(textLessonId);

            return Ok(JsonConvert.SerializeObject(textLesson));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int ClassRoomId)
        {
            var textLessons = await TextLessonRepository.GetAllAsync(ClassRoomId);

            return Ok(JsonConvert.SerializeObject(textLessons));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddTextLessonDTO addTextLessonDTO)
        {
            var addAsyncResult = await TextLessonRepository.AddAsync(addTextLessonDTO);

            if (addAsyncResult is null)
            {
                return NotFound(JsonConvert.SerializeObject(
                "There is no text lesson  with this Id."));
            }
            if (addAsyncResult.Value > 0)
            {
                return Ok(JsonConvert.SerializeObject(new TextLessonDTO(addTextLessonDTO) { Id = addAsyncResult.Value }));
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TextLessonDTO textLessonDTO)
        {
            var updateAsyncResult = await TextLessonRepository.UpdateAsync(textLessonDTO);

            if (updateAsyncResult is null)
            {
                return NotFound(JsonConvert.SerializeObject(
                "There is no text lesson  with this Id."));
            }

            if (updateAsyncResult.Value)
            {
                return Ok(JsonConvert.SerializeObject(textLessonDTO));
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Remove([FromQuery] int TextLessonId)
        {
            var removeAsyncResult = await TextLessonRepository.RemoveAsync(TextLessonId);

            if (removeAsyncResult is null)
            {
                return NotFound(JsonConvert.SerializeObject(
                "There is no text lesson  with this Id."));
            }

            if (removeAsyncResult.Value)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
