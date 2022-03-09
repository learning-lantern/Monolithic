using API.Data.DTOs;

namespace API.Repositories.ToDo
{
    public interface IToDoRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="filter"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<List<TaskDTO>> GetAsync(string userId, string? filter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addTaskDTO"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<int?> AddAsync(AddTaskDTO addTaskDTO);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskDTO"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<bool?> UpdateAsync(TaskDTO taskDTO);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<bool?> RemoveAsync(int taskId);
    }
}
