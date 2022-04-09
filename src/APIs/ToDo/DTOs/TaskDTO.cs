using System.ComponentModel.DataAnnotations;

namespace APIs.ToDo.DTOs
{
    public class TaskDTO : TaskProperties
    {
        [Required, Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = null!;

        public TaskDTO() { }
        public TaskDTO(TaskProperties taskProperties) : base(taskProperties) { }
        public TaskDTO(TaskDTO taskDTO) : base(taskDTO)
        {
            Id = taskDTO.Id;
            UserId = taskDTO.UserId;
        }
    }
}
