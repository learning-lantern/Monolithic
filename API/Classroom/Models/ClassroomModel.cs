using API.Calendar.Models;
using API.Classroom.DTOs;

namespace API.Classroom.Models
{
    public class ClassroomModel : ClassroomDTO
    {
        public ICollection<EventModel> Events { get; set; } = null!;
    }
}