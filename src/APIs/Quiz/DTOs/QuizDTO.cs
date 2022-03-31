using APIs.User.Models;
using System.ComponentModel.DataAnnotations;

namespace APIs.Quiz.DTOs
{
    public class QuizDTO : AddQuizDTO
    {
        [Required, Key]
        public int Id { get; set; }
        

        public QuizDTO() { } 
        public QuizDTO(AddQuizDTO addQuizDTO) : base(addQuizDTO)
        {
            

        }
        public QuizDTO(QuizDTO quizDTO, UserModel userModel) : base(quizDTO) {
            Id = quizDTO.Id;
        }

    }
}
