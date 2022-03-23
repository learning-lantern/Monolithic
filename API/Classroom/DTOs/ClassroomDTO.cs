using System.ComponentModel.DataAnnotations;

namespace API.Classroom.DTOs
{
    public class ClassroomDTO : AddClassroomDTO
    {
        [Required, Key]
        public int Id { get; set; }
    }
}
