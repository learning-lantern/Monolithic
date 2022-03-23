using API.Classroom.Models;

namespace API.Classroom.Repositories
{
    public interface IClassroomRepository
    {
        public Task<ClassroomModel?> FindByIdAsync(int id);
    }
}