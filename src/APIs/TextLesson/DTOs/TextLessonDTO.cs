using System.ComponentModel.DataAnnotations;

namespace APIs.TextLesson.DTOs
{
    public class TextLessonDTO : AddTextLessonDTO
    {
        [Required, Key]
        public int Id { get; set; }

        public TextLessonDTO() { }
        public TextLessonDTO(AddTextLessonDTO addTextLessonDTO) : base(addTextLessonDTO) { }
        public TextLessonDTO(TextLessonDTO TextLessonDTO) : base(TextLessonDTO)
        {
            Id = TextLessonDTO.Id;
        }
    }
}
