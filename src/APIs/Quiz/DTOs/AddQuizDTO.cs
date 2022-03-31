using System.ComponentModel.DataAnnotations;

namespace APIs.Quiz.DTOs
{
    public class AddQuizDTO
    {
        public decimal Score { get; set; } = 0;
        [Required]
        public DateTime Time { get; set; }
        public bool IsAttendace { get; set; } = false;

        [Required, StringLength(450)]
        public string InstructorId { get; set; } = null!;
        

        public AddQuizDTO() { }
        public AddQuizDTO(AddQuizDTO addQuizDTO)
        {
            Score = addQuizDTO.Score;
            Time = addQuizDTO.Time;
            IsAttendace = addQuizDTO.IsAttendace;
            InstructorId = addQuizDTO.InstructorId;
            
        }
    }
}
