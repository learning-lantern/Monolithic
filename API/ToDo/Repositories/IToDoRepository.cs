using API.ToDo.DTOs;

namespace API.ToDo.Repositories
{
    public interface IToDoRepository
    {
        public Task<List<TaskDTO>> GetAsync(string userId, string? list);

        public Task<int?> AddAsync(string userId, AddTaskDTO addTaskDTO);

        public Task<bool?> UpdateAsync(TaskDTO taskDTO);

        public Task<bool?> RemoveAsync(string userId, int taskId);
    }
}
