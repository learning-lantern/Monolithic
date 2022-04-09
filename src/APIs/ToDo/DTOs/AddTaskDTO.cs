namespace APIs.ToDo.DTOs
{
    public class AddTaskDTO : TaskProperties
    {
        public string? TempId { get; set; }

        public AddTaskDTO() { }
        public AddTaskDTO(AddTaskDTO addTaskDTO) : base(addTaskDTO)
        {
            TempId = addTaskDTO.TempId;
        }
    }
}
