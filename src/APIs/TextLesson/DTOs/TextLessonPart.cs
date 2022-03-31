namespace APIs.TextLesson.DTOs
{
    public class TextLessonPart
    {
        public int Id { get; set;}
        public string Name { get; set; }
        public TextLessonPart(int Id,string Name)
        {
            this.Id = Id;
            this.Name=Name;
        }
    }
}
