using System.ComponentModel.DataAnnotations;

namespace APIs.Calendar.DTOs
{
    public class AddEventDTO
    {
        [Required, StringLength(450)]
        public string Name { get; set; } = null!;
        public string? Discription { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int ClassroomId { get; set; }

        public AddEventDTO() { }
        public AddEventDTO(AddEventDTO addEventDTO)
        {
            Name = addEventDTO.Name;
            Discription = addEventDTO.Discription;
            StartTime = addEventDTO.StartTime;
            EndTime = addEventDTO.EndTime;
            ClassroomId = addEventDTO.ClassroomId;
        }
    }
}