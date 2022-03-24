using API.Quiz.DTOs;
using API.User.Models;

namespace API.Quiz.Models
{
    public class QuizModel : QuizDTO
    {
        public UserModel Instructor { get; set; } = null!;

        public QuizModel() { }
        public QuizModel(QuizDTO quizDTO, UserModel userModel) : base(quizDTO)
        {
            Instructor = userModel;
        }

        public QuizModel(AddQuizDTO addQuizDTO, UserModel userModel) : base(addQuizDTO)
        {
            Instructor = userModel;
        }



    }
}
