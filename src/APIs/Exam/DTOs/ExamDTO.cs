using APIs.Classroom.Models;
using APIs.User.Models;
using System.ComponentModel.DataAnnotations;

namespace APIs.Exam.DTOs
{
    public class ExamDTO : AddExamDTO
    {
        [Required, Key]
        public int Id { get; set; }


        public ExamDTO() { }
        public ExamDTO(AddExamDTO addExamDTO) : base(addExamDTO)
        {


        }
        public ExamDTO(ExamDTO examDTO, UserModel userModel,ClassroomModel classroomModel) : base(examDTO)
        {
            Id = examDTO.Id;
        }

    }
}
