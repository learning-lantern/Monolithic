using System.ComponentModel.DataAnnotations;

namespace API.TextLesson.DTOs
{
    public class AddTextLessonDTO
    {
        [Required, StringLength(450)]
        public string Name { get; set; } = null!;
        public string? Discription { get; set; }
        public string? ContentPath { get; set; }
        public bool Editable { get; set; } = false;
        public bool Printable { get; set; } = false;
        [Required, StringLength(450)]
        public string UserId { get; set; } = null!;
        [Required, StringLength(450)]
        public int ClassRoomId { get; set; } 


        public AddTextLessonDTO() { }
        public AddTextLessonDTO(AddTextLessonDTO addTextLessonDTO)
        {
            Name = addTextLessonDTO.Name;
            Discription = addTextLessonDTO.Discription;
            ContentPath = addTextLessonDTO.ContentPath;
            Editable = addTextLessonDTO.Editable;
            Printable = addTextLessonDTO.Printable;
            UserId = addTextLessonDTO.UserId;
            ClassRoomId = addTextLessonDTO.ClassRoomId;
        }
    }
}
