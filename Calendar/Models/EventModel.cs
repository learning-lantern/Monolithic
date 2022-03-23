using API.Calendar.DTOs;
using API.Classroom.Models;

namespace API.Calendar.Models
{
	public class EventModel
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Discription { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public int? ClassroomId { get; set; }
		public ClassroomModel? Classroom { get; set; }

		public EventModel()
		{
		}

		public EventModel(AddEventDTO eventDTO, int? classroomId)
		{
			Name = eventDTO.Name;
			Discription = eventDTO.Discription;
			StartTime = eventDTO.StartTime;
			EndTime = eventDTO.EndTime;
			ClassroomId = classroomId;
		}

		public EventModel(int id, AddEventDTO eventDTO, int? classroomId) : this(eventDTO, classroomId)
		{
			Id = id;
		}
	}
}