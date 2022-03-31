//using APIs.Database;
//using APIs.TextLesson.DTOs;
//using APIs.TextLesson.Models;
//using APIs.User.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//namespace APIs.TextLesson.Repositories
//{
//    public class TextLessonRepository : ITextLessonRepository
//    {
//        private readonly LearningLanternContext learningLanternContext;
//        private readonly UserManager<UserModel> userManager;

//        public TextLessonRepository(LearningLanternContext learningLanternContext, UserManager<UserModel> userManager)
//        {
//            this.learningLanternContext = learningLanternContext;
//            this.userManager = userManager;
//        }

//        public TextLessonDTO Get(int textLessonId)
//        {
//            return (TextLessonDTO) learningLanternContext.TextLessons
//                .Where(textLesson => textLesson.Id == textLessonId)
//                .Select(textLesson => new TextLessonDTO(textLesson));
//        }
//        public async Task<List<TextLessonPart> > GetAllAsync(int ClassRoomId)
//        {
//            return await learningLanternContext.TextLessons
//                .Where(textLesson => textLesson.ClassRoomId == ClassRoomId).Select(textLesson => new TextLessonPart( textLesson.Id, textLesson.Name) )
//                .ToListAsync();
//        }
//        public async Task<int?> AddAsync(AddTextLessonDTO addTextLessonDTO)
//        {
//            var user = await userManager.FindByIdAsync(addTextLessonDTO.UserId);

//            if (user is null)
//            {
//                return null;
//            }
//            //var classRoom =  learningLanternContext.TextLesson.Find(addTextLessonDTO.ClassRoomId);

//            //if (classRoom is null)
//            //{
//            //    return null;
//            //}
//            var TextLesson = await learningLanternContext.TextLessons.AddAsync(new TextLessonModel(addTextLessonDTO, user));

//            if (TextLesson is null)
//            {
//                return 0;
//            }

//            var saveChangesAsyncResult = await learningLanternContext.SaveChangesAsync();

//            return saveChangesAsyncResult == 0 ? 0 : TextLesson.Entity.Id;
//        }

//        public async Task<bool?> UpdateAsync(TextLessonDTO textLessonDTO)
//        {
//            var user = await userManager.FindByIdAsync(textLessonDTO.UserId);

//            if (user is null)
//            {
//                return null;
//            }

//            var task = learningLanternContext.TextLessons.Update(new TextLessonModel(textLessonDTO, user));

//            if (task is null)
//            {
//                return false;
//            }

//            var saveChangesAsyncResult = await learningLanternContext.SaveChangesAsync();

//            return saveChangesAsyncResult != 0;
//        }

//        public async Task<bool?> RemoveAsync(int textLessonId)
//        {
//            var TextLesson = learningLanternContext.TextLessons.Remove(new TextLessonModel() { Id = textLessonId });

//            if (TextLesson is null)
//            {
//                return false;
//            }

//            var saveChangesAsyncResult = await learningLanternContext.SaveChangesAsync();

//            return saveChangesAsyncResult != 0;
//        }
//    }
//}
