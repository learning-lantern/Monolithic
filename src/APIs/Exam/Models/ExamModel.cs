using APIs.Classroom.Models;
using APIs.Exam.DTOs;
using APIs.User.Models;

namespace APIs.Exam.Models
{
    public class ExamModel : ExamDTO
    {
        public UserModel Instructor { get; set; } = null!;
        public ClassroomModel  classroom{ get; set; }

        public ExamModel(AddExamDTO addExamDTO) : base(addExamDTO) { }
        public ExamModel(ExamDTO examDTO, UserModel userModel, ClassroomModel classroomModel) : base(examDTO)
        {
            Instructor = userModel;
            classroom = classroomModel;    
        }

        public ExamModel(AddExamDTO addExamDTO, UserModel userModel, ClassroomModel classroomModel) : base(addExamDTO)
        {
            Instructor = userModel;
            classroom = classroomModel;

        }
    }
}
