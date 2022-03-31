using APIs.TextLesson.DTOs;
using APIs.User.Models;

namespace APIs.TextLesson.Models
{
    public class TextLessonModel : TextLessonDTO
    {
        public UserModel User { get; set; } = null!;

        public TextLessonModel() { }
        public TextLessonModel(TextLessonDTO textLessonDTO, UserModel userModel) : base(textLessonDTO)
        {
            User = userModel;
        }
        public TextLessonModel(AddTextLessonDTO addTextLessonDTO, UserModel userModel) : base(addTextLessonDTO)
        {
            User = userModel;
        }
    }
}
