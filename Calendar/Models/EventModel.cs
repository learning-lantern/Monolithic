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

		public EventModel(AddEventDTO eventDTO, ClassroomModel classroom)
		{
			Name = eventDTO.Name;
			Discription = eventDTO.Discription;
			StartTime = eventDTO.StartTime;
			EndTime = eventDTO.EndTime;
			ClassroomId = classroom.Id;
			Classroom = classroom;
		}

		public EventModel(int id, AddEventDTO eventDTO, ClassroomModel? classroom)
		{
			Id = id;
			Name = eventDTO.Name;
			Discription = eventDTO.Discription;
			StartTime = eventDTO.StartTime;
			EndTime = eventDTO.EndTime;
			if (classroom is not null)
			{
				ClassroomId = classroom.Id;
				Classroom = classroom;
			}
		}
	}
}