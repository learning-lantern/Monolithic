using API.Classroom.Models;

namespace API.Classroom.Repositories
{
    public interface IClassroomRepository
    {
        public Task<ClassroomModel?> GetAsync(int classroomId);
    }
}