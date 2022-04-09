using System.ComponentModel.DataAnnotations;

namespace APIs.Quiz.DTOs
{
    public class QuizDTO : AddQuizDTO
    {
        [Required, Key]
        public int Id { get; set; }
        [Required, StringLength(450)]
        public string UserId { get; set; } = null!;

        public QuizDTO() { } 
        public QuizDTO(AddQuizDTO addQuizDTO) : base(addQuizDTO) { }
        public QuizDTO(QuizDTO quizDTO) : base(quizDTO)
        {
            Id = quizDTO.Id;
            UserId = quizDTO.UserId;
        }
    }
}
