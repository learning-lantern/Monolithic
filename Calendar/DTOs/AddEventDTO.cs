namespace API.Calendar.DTOs
{
    public class AddEventDTO
    {
        public string? Name { get; set; }
        public string? Discription { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public AddEventDTO()
        {
        }
        public AddEventDTO(AddEventDTO copy)
        {
            Name = copy.Name;
            Discription = copy.Discription;
            StartTime = copy.StartTime;
            EndTime = copy.EndTime;
        }
    }
}