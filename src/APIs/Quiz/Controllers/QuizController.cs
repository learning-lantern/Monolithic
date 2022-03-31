using APIs.Quiz.DTOs;
using APIs.Quiz.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIs.Quiz.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController, Authorize]
    public class QuizController : ControllerBase
    {
        private readonly IQuizRepository quizRepository;

        public QuizController(IQuizRepository quizRepository)
        {
            this.quizRepository = quizRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string InstructorId)
        {
            var quizs = await quizRepository.GetAsync(InstructorId);

            return Ok(JsonConvert.SerializeObject(quizs));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddQuizDTO addQuizDTO)
        {
            var addAsyncResult = await quizRepository.AddAsync(addQuizDTO);

            if (addAsyncResult is null)
            {
                return NotFound(JsonConvert.SerializeObject(
                "There is no user in this University with this Id."));
            }
            if (addAsyncResult.Value > 0)
            {
                return Ok(JsonConvert.SerializeObject(new QuizDTO(addQuizDTO) { Id = addAsyncResult.Value }));
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] QuizDTO quizDTO)
        {
            var updateAsyncResult = await quizRepository.UpdateAsync(quizDTO);

            if (updateAsyncResult is null)
            {
                return NotFound(JsonConvert.SerializeObject(
                "There is no user in this University with this Id."));
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
            var removeAsyncResult = await quizRepository.RemoveAsync(quizId);

            if (removeAsyncResult is null)
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
