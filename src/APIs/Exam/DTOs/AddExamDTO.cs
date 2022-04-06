using System.ComponentModel.DataAnnotations;

namespace APIs.Exam.DTOs
{
    public class AddExamDTO
    {
        public decimal Score { get; set; } = 0;
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public DateTime StartDate { get; set; } 

        public bool Shuffle { get; set; } = false;
        public bool ShowScore { get; set; } = false;



        [Required, StringLength(450)]
        public string InstructorId { get; set; } = null!;
        [Required]
        public string ClassroomId { get; set; } = null!;


        public AddExamDTO() { }
        public AddExamDTO(AddExamDTO addExamDTO)
        {
            Score = addExamDTO.Score;
            Time = addExamDTO.Time;
            Shuffle = addExamDTO.Shuffle;
            StartDate = addExamDTO.StartDate;
            InstructorId = addExamDTO.InstructorId;
            ClassroomId = addExamDTO.ClassroomId;
            ShowScore = addExamDTO.ShowScore;   


        }
    }
}

