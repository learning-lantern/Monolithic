using API.TextLesson.DTOs;

namespace API.TextLesson.Repositories
{
    public interface ITextLessonRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>
        /// 
        /// </returns>
        public TextLessonDTO Get(int Id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClassRoomId"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<List<TextLessonPart>> GetAllAsync(int ClassRoomId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addTextLessonDTO"></param>
        /// <returns>
        /// 
        /// </returns>
        /// 
        public Task<int?> AddAsync(AddTextLessonDTO addTextLessonDTO);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textLessonDTO"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<bool?> UpdateAsync(TextLessonDTO textLessonDTO);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TextLessonId"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<bool?> RemoveAsync(int TextLessonId);
    }
}
