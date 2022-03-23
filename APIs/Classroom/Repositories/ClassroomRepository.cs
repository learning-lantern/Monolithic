using API.Calendar.Repositories;
using API.Classroom.Models;

namespace API.Classroom.Repositories
{
    public class ClassroomRepository : IClassroomRepository
    {
        public async Task<ClassroomModel?> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}