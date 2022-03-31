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
                return await learningLanternContext.Tasks.Where(task => task.UserId == userId).Select(task => new TaskDTO(task)).ToListAsync();
            }
            else if (list == "MyDay")
            {
                return await learningLanternContext.Tasks.Where(task => task.UserId == userId && task.MyDay).Select(task => new TaskDTO(task)).ToListAsync();
            }
            else if (list == "Completed")
            {
                return await learningLanternContext.Tasks.Where(task => task.UserId == userId && task.Completed).Select(task => new TaskDTO(task)).ToListAsync();
            }
            else
            {
                return await learningLanternContext.Tasks.Where(task => task.UserId == userId && task.Important).Select(task => new TaskDTO(task)).ToListAsync();
            }
        }

        public async Task<int?> AddAsync(AddTaskDTO addTaskDTO, string userId)
        {
            var user = await userRepository.FindUserByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            var task = await learningLanternContext.Tasks.AddAsync(new TaskModel(addTaskDTO, userId));

            if (task == null)
            {
                return 0;
            }

            return await learningLanternContext.SaveChangesAsync() == 0 ? 0 : task.Entity.Id;
        }

        public async Task<bool?> UpdateAsync(TaskDTO taskDTO)
        {
            var task = await learningLanternContext.Tasks.Where(task => task.Id == taskDTO.Id && task.UserId == taskDTO.UserId).FirstOrDefaultAsync();

            if (task == null)
            {
                return null;
            }

            task = learningLanternContext.Tasks.Update(new TaskModel(taskDTO)).Entity;

            if (task == null)
            {
                return false;
            }

            return await learningLanternContext.SaveChangesAsync() != 0;
        }

        public async Task<bool?> RemoveAsync(int taskId, string userId)
        {
            var task = await learningLanternContext.Tasks.Where(task => task.Id == taskId && task.UserId == userId).FirstOrDefaultAsync();

            if (task == null)
            {
                return null;
            }

            task = learningLanternContext.Tasks.Remove(task).Entity;

            if (task == null)
            {
                return false;
            }

            return await learningLanternContext.SaveChangesAsync() != 0;
        }
    }
}
