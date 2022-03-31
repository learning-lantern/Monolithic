using APIs.Calendar.DTOs;
using APIs.Classroom.Models;

namespace APIs.Calendar.Models
{
    public class EventModel : EventDTO
	{
		public ClassroomModel Classroom { get; set; } = null!;

		public EventModel() { }
		public EventModel(AddEventDTO addEventDTO) : base(addEventDTO) { }
		public EventModel(EventDTO eventDTO) : base(eventDTO) { }
	}
}