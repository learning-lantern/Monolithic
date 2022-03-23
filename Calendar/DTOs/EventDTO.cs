using API.Calendar.Models;

namespace API.Calendar.DTOs
{
    public class EventDTO : AddEventDTO
    {
        public int Id { get; set; }

        public EventDTO()
        {
        }

        public EventDTO(int id, AddEventDTO addEvent) : base(addEvent)
        {
            Id = id;
        }
        
        public EventDTO(EventModel copy)
        {
            Id = copy.Id;
            Name = copy.Name;
            Discription = copy.Discription;
            StartTime = copy.StartTime;
            EndTime = copy.EndTime;
        }
    }
}