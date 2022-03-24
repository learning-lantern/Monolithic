using API.Classroom.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Classroom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomRepository classroomRepository;

        public ClassroomController(IClassroomRepository classroomRepository)
        {
            this.classroomRepository = classroomRepository;
        }


    }
}
