using API.Calendar.DTOs;
using API.Classroom.Models;

namespace API.Calendar.Models
{
    public class EventModel : EventDTO
	{
		public ClassroomModel Classroom { get; set; } = null!;

		public EventModel() { }
		public EventModel(AddEventDTO addEventDTO, ClassroomModel classroomModel) : base(addEventDTO)
        {
			Classroom = classroomModel;
        }
		public EventModel(EventDTO eventDTO, ClassroomModel classroomModel) : base(eventDTO)
		{
			Classroom = classroomModel;
		}
	}
}