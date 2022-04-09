using APIs.Exam.DTOs;
using APIs.Exam.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIs.Exam.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController, Authorize]
    public class ExamController : ControllerBase
    {
        private readonly IExamRepository examRepository;

        public ExamController(IExamRepository examRepository)
        {
            this.examRepository = examRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string InstructorId)
        {
            var exams = await examRepository.GetAsync(InstructorId);

            return Ok(JsonConvert.SerializeObject(exams));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddExamDTO addExamDTO)
        {
            var addAsyncResult = await examRepository.AddAsync(addExamDTO);

            if (addAsyncResult is null)
            {
                return NotFound(JsonConvert.SerializeObject(
                "There is no user in this University with this Id."));
            }
            if (addAsyncResult.Value > 0)
            {
                return Ok(JsonConvert.SerializeObject(new ExamDTO(addExamDTO) { Id = addAsyncResult.Value }));
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ExamDTO examDTO)
        {
            var updateAsyncResult = await examRepository.UpdateAsync(examDTO);

            if (updateAsyncResult is null)
            {
                return NotFound(JsonConvert.SerializeObject(
                "There is no user in this University with this Id."));
            }

            if (updateAsyncResult.Value)
            {
                return Ok(JsonConvert.SerializeObject(examDTO));
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Remove([FromQuery] int examId)
        {
            var removeAsyncResult = await examRepository.RemoveAsync(examId);

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
