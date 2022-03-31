using APIs.ToDo.DTOs;
using APIs.User.Models;

namespace APIs.ToDo.Models
{
    public class TaskModel : TaskDTO
    {
        public UserModel User { get; set; } = null!;

        public TaskModel() { }
        public TaskModel(TaskDTO taskDTO) : base(taskDTO) { }
        public TaskModel(AddTaskDTO addTaskDTO, string userId) : base(addTaskDTO)
        {
            UserId = userId;
        }
    }
}
