using System.ComponentModel.DataAnnotations;

namespace APIs.ToDo.DTOs
{
    public class TaskDTO : AddTaskDTO
    {
        [Required, Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = null!;

        public TaskDTO() { }
        public TaskDTO(AddTaskDTO addTaskDTO) : base(addTaskDTO) { }
        public TaskDTO(TaskDTO taskDTO) : base(taskDTO)
        {
            Id = taskDTO.Id;
            UserId = taskDTO.UserId;
        }
    }
}
