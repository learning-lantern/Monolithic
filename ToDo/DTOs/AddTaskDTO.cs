using System.ComponentModel.DataAnnotations;

namespace API.ToDo.DTOs
{
    public class AddTaskDTO
    {
        [Required, StringLength(450)]
        public string Name { get; set; } = null!;
        public DateTime? DueDate { get; set; }
        public string? Note { get; set; }
        public bool MyDay { get; set; } = false;
        public bool Completed { get; set; } = false;
        public bool Important { get; set; } = false;
        public int Repeated { get; set; } = 0;

        [Required, StringLength(450)]
        public string UserId { get; set; } = null!;

        public AddTaskDTO() { }
        public AddTaskDTO(AddTaskDTO addTaskDTO)
        {
            Name = addTaskDTO.Name;
            DueDate = addTaskDTO.DueDate;
            Note = addTaskDTO.Note;
            MyDay = addTaskDTO.MyDay;
            Completed = addTaskDTO.Completed;
            Important = addTaskDTO.Important;
            Repeated = addTaskDTO.Repeated;
            UserId = addTaskDTO.UserId;
        }
    }
}
