using API.ToDo.DTOs;

namespace API.ToDo.Repositories
{
    public interface IToDoRepository
    {
        public Task<List<TaskDTO>> GetAsync(string userId, string? list);

        public Task<int?> AddAsync(AddTaskDTO addTaskDTO, string userId);

        public Task<bool?> UpdateAsync(TaskDTO taskDTO);

        public Task<bool?> RemoveAsync(int taskId, string userId);
    }
}
