using APIs.Classroom.Models;
using APIs.Quiz.DTOs;
using APIs.User.Models;

namespace APIs.Quiz.Models
{
    public class QuizModel : QuizDTO
    {
        public UserModel User { get; set; } = null!;
        public ClassroomModel Classroom { get; set; } = null!;

        public QuizModel() { }
        public QuizModel(QuizDTO quizDTO) : base(quizDTO) { }
        public QuizModel(AddQuizDTO addQuizDTO, string userId) : base(addQuizDTO)
        {
            UserId = userId;
        }
    }
}
