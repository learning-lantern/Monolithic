using APIs.Classroom.Models;
using System.ComponentModel.DataAnnotations;

namespace APIs.Classroom.DTOs
{
    public class ClassroomDTO : AddClassroomDTO
    {
        [Required, Key]
        public int Id { get; set; }

        public ClassroomDTO() { }
        public ClassroomDTO(AddClassroomDTO addTaskDTO) : base(addTaskDTO) { }
        public ClassroomDTO(ClassroomDTO classroomDTO) : base(classroomDTO)
        {
            Id = classroomDTO.Id;
        }
    }
}
