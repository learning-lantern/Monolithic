using System.ComponentModel.DataAnnotations;

namespace APIs.Quiz.DTOs
{
    public class AddQuizDTO
    {
        [Required]
        public decimal Score { get; set; } = 0;
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public bool IsAttendace { get; set; } = false;
        [Required]
        public string Answer { get; set; } = string.Empty;

        [Required]
        public int ClassroomId { get; set; }

        public AddQuizDTO() { }
        public AddQuizDTO(AddQuizDTO addQuizDTO)
        {
            Score = addQuizDTO.Score;
            Time = addQuizDTO.Time;
            IsAttendace = addQuizDTO.IsAttendace;
            Answer = addQuizDTO.Answer;
            ClassroomId = addQuizDTO.ClassroomId;
        }
    }
}
