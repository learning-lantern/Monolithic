using API.Classroom.DTOs;

namespace API.Classroom.Repositories
{
    public interface IClassroomRepository
    {
        public Task<ClassroomDTO?> GetAsync(int classroomId, string userId);
        public Task<int?> AddAsync(AddClassroomDTO addClassroomDTO, string userId);
        public Task<bool?> AddUserAsync(int classroomId, string userId);
        public Task<bool?> UpdateAsync(ClassroomDTO classroomDTO);
        public Task<bool?> RemoveAsync(int classroomId);
    }
}