using API.Classroom.Models;

namespace API.Classroom.Repositories
{
    public class ClassroomRepository : IClassroomRepository
    {
        public async Task<ClassroomModel?> GetAsync(int classroomId)
        {
            throw new NotImplementedException();
        }
    }
}