using APIs.Calendar.Models;
using APIs.Classroom.DTOs;

namespace APIs.Classroom.Models
{
    public class ClassroomModel : ClassroomDTO
    {
        public ICollection<EventModel> Events { get; set; } = null!;
        public ICollection<ClassroomUserModel> ClassroomUsers { get; set; } = null!;

        public ClassroomModel() { }
        public ClassroomModel(ClassroomDTO classroomDTO) : base(classroomDTO) { }
        public ClassroomModel(AddClassroomDTO addClassroomDTO) : base(addClassroomDTO) { }
    }
}