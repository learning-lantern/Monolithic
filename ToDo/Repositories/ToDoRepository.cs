using API.Database;
using API.ToDo.DTOs;
using API.ToDo.Models;
using API.User.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.ToDo.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly LearningLanternContext learningLanternContext;
        private readonly IUserRepository userRepository;

        public ToDoRepository(LearningLanternContext learningLanternContext, IUserRepository userRepository)
        {
            this.learningLanternContext = learningLanternContext;
            this.userRepository = userRepository;
        }

        public async Task<List<TaskDTO>> GetAsync(string userId, string? list)
        {
            if (list == null)
            {
                return await learningLanternContext.Task.Where(task => task.UserId == userId).Select(task => new TaskDTO(task)).ToListAsync();
            }
            else if (list == "MyDay")
            {
                return await learningLanternContext.Task.Where(task => task.UserId == userId && task.MyDay).Select(task => new TaskDTO(task)).ToListAsync();
            }
            else if (list == "Completed")
            {
                return await learningLanternContext.Task.Where(task => task.UserId == userId && task.Completed).Select(task => new TaskDTO(task)).ToListAsync();
            }
            else
            {
                return await learningLanternContext.Task.Where(task => task.UserId == userId && task.Important).Select(task => new TaskDTO(task)).ToListAsync();
            }
        }

        public async Task<int?> AddAsync(AddTaskDTO addTaskDTO)
        {
            var user = await userRepository.FindUserByIdAsync(addTaskDTO.UserId);

            if (user == null)
            {
                return null;
            }

            var task = await learningLanternContext.Task.AddAsync(new TaskModel(addTaskDTO, user));

            if (task == null)
            {
                return 0;
            }

            return await learningLanternContext.SaveChangesAsync() == 0 ? 0 : task.Entity.Id;
        }

        public async Task<bool?> UpdateAsync(TaskDTO taskDTO)
        {
            var user = await userRepository.FindUserByIdAsync(taskDTO.UserId);

            if (user == null)
            {
                return null;
            }

            var task = learningLanternContext.Task.Update(new TaskModel(taskDTO, user));

            if (task == null)
            {
                return false;
            }

            return await learningLanternContext.SaveChangesAsync() != 0;
        }

        public async Task<bool?> RemoveAsync(int taskId)
        {
            var task = learningLanternContext.Task.Remove(new TaskModel() { Id = taskId });

            if (task == null)
            {
                return false;
            }

            return await learningLanternContext.SaveChangesAsync() != 0;
        }
    }
}
