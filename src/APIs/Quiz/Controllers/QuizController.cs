using APIs.Helpers;
using APIs.Quiz.DTOs;
using APIs.Quiz.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace APIs.Quiz.Controllers
{
    [Route("api/Classroom/[controller]/[action]")]
    [ApiController, Authorize(Roles = Role.Instructor)]
    public class QuizController : ControllerBase
    {
        private readonly IQuizRepository quizRepository;

        public QuizController(IQuizRepository quizRepository)
        {
            this.quizRepository = quizRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            return Ok(JsonConvert.SerializeObject(await quizRepository.GetAsync(userId)));
        }

        [HttpGet]
        public async Task<IActionResult> GetByClassroom([FromQuery] int classroomId)
        {
            return Ok(JsonConvert.SerializeObject(await quizRepository.GetAsync(classroomId)));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddQuizDTO addQuizDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var addAsyncResult = await quizRepository.AddAsync(addQuizDTO, userId);

            if (addAsyncResult == null)
            {
                return NotFound(JsonConvert.SerializeObject(Message.UserIdNotFound));
            }

            if (addAsyncResult.Value > 0)
            {
                return Ok(JsonConvert.SerializeObject(new QuizDTO(addQuizDTO) { Id = addAsyncResult.Value, UserId = userId }));
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] QuizDTO quizDTO)
        {
            quizDTO.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var updateAsyncResult = await quizRepository.UpdateAsync(quizDTO);

            if (updateAsyncResult == null)
            {
                return NotFound(JsonConvert.SerializeObject(Message.QuizNotFound));
            }

            if (updateAsyncResult.Value)
            {
                return Ok(JsonConvert.SerializeObject(quizDTO));
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Remove([FromQuery] int quizId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var removeAsyncResult = await quizRepository.RemoveAsync(quizId, userId);

            if (removeAsyncResult == null)
            {
                return NotFound(JsonConvert.SerializeObject(Message.QuizNotFound));
            }

            if (removeAsyncResult.Value)
            {
                return Ok(JsonConvert.SerializeObject(Message.QuizRemoved));
            }

            return BadRequest();
        }
    
    }
}
