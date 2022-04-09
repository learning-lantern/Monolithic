namespace APIs.ToDo.DTOs
{
    public class AddTaskResponseDTO
    {
        public TaskDTO? Task { get; set; }
        public string? TempId { get; set; }

        public AddTaskResponseDTO() { }
        public AddTaskResponseDTO(TaskDTO? taskDTO, string? tempId)
        {
            Task = taskDTO;
            TempId = tempId;
        }
    }
}
