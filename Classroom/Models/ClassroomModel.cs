using API.Calendar.Models;

namespace API.Classroom.Models
{
    public class ClassroomModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<EventModel> Events { get; set; } = null!;
    }
}