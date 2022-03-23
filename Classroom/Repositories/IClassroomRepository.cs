using API.Classroom.Models;

namespace API.Classroom.Repositories
{
    public interface IClassroomRepository
    {
        public ClassroomModel? FindByIdAsync(int id);
    }
}