using API.Data.DTOs;
using System.ComponentModel.DataAnnotations;

namespace API.Data.Models
{
    public class TaskModel : TaskDTO
    {
        [Required]
        public UserModel User { get; set; } = null!;

        public TaskModel() { }
        public TaskModel(TaskDTO taskDTO, UserModel userModel) : base(taskDTO)
        {
            User = userModel;
        }
        public TaskModel(AddTaskDTO addTaskDTO, UserModel userModel) : base(addTaskDTO)
        {
            User = userModel;
        }
    }
}
