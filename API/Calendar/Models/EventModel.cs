using API.Calendar.DTOs;
using API.Classroom.Models;

namespace API.Calendar.Models
{
    public class EventModel : EventDTO
	{
		public ClassroomModel Classroom { get; set; } = null!;

		public EventModel() { }
		public EventModel(AddEventDTO addEventDTO) : base(addEventDTO) { }
		public EventModel(EventDTO eventDTO) : base(eventDTO) { }
	}
}