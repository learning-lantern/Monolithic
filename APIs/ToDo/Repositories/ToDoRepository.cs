using API.Database;
using API.ToDo.DTOs;
using API.ToDo.Models;
using API.User.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.ToDo.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly LearningLanternContext learningLanternContext;
        private readonly UserManager<UserModel> userManager;

        public ToDoRepository(LearningLanternContext learningLanternContext, UserManager<UserModel> userManager)
        {
            this.learningLanternContext = learningLanternContext;
            this.userManager = userManager;
        }

        public async Task<List<TaskDTO>> GetAsync(string userId, string? list)
        {
            if (list is null)
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

        public async Task<int?> AddAsync(AddTaskDTO addTaskDTO)
        {
            var user = await userManager.FindByIdAsync(addTaskDTO.UserId);

            if (user is null)
            {
                return null;
            }

            var task = await learningLanternContext.Tasks.AddAsync(new TaskModel(addTaskDTO, user));

            if (task is null)
            {
                return 0;
            }

            var saveChangesAsyncResult = await learningLanternContext.SaveChangesAsync();

            return saveChangesAsyncResult == 0 ? 0 : task.Entity.Id;
        }

        public async Task<bool?> UpdateAsync(TaskDTO taskDTO)
        {
            var user = await userManager.FindByIdAsync(taskDTO.UserId);

            if (user is null)
            {
                return null;
            }

            var task = learningLanternContext.Tasks.Update(new TaskModel(taskDTO, user));

            if (task is null)
            {
                return false;
            }

            var saveChangesAsyncResult = await learningLanternContext.SaveChangesAsync();

            return saveChangesAsyncResult != 0;
        }

        public async Task<bool?> RemoveAsync(int taskId)
        {
            var task = learningLanternContext.Tasks.Remove(new TaskModel() { Id = taskId });

            if (task is null)
            {
                return false;
            }

            var saveChangesAsyncResult = await learningLanternContext.SaveChangesAsync();

            return saveChangesAsyncResult != 0;
        }
    }
}
