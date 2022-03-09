using System.ComponentModel.DataAnnotations;

namespace API.Data.DTOs
{
    public class TaskDTO : AddTaskDTO
    {
        [Required, Key]
        public int Id { get; set; }

        public TaskDTO() { }
        public TaskDTO(AddTaskDTO addTaskDTO) : base(addTaskDTO) { }
        public TaskDTO(TaskDTO taskDTO) : base(taskDTO)
        {
            Id = taskDTO.Id;
        }
    }
}
