using API.Calendar.Models;

namespace API.Classroom.Models
{
    public class ClassroomModel
    {
        public int Id { get; set; }
        public ICollection<EventModel> Events { get; set; } = null!;
    }
}