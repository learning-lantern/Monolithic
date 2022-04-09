using System.ComponentModel.DataAnnotations;

namespace APIs.ToDo.DTOs
{
    public class TaskProperties
    {
        [Required, StringLength(450)]
        public string Name { get; set; } = null!;
        public DateTime? DueDate { get; set; }
        public string? Note { get; set; }
        public bool MyDay { get; set; } = false;
        public bool Completed { get; set; } = false;
        public bool Important { get; set; } = false;
        public int Repeated { get; set; } = 0;

        public TaskProperties() { }
        public TaskProperties(TaskProperties taskProperties)
        {
            Name = taskProperties.Name;
            DueDate = taskProperties.DueDate;
            Note = taskProperties.Note;
            MyDay = taskProperties.MyDay;
            Completed = taskProperties.Completed;
            Important = taskProperties.Important;
            Repeated = taskProperties.Repeated;
        }
    }
}
