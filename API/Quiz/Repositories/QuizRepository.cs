using API.Database;
using API.Quiz.DTOs;
using API.Quiz.Models;
using API.User.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace API.Quiz.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly LearningLanternContext learningLanternContext;
        private readonly UserManager<UserModel> userManager;

        public QuizRepository(LearningLanternContext learningLanternContext, UserManager<UserModel> userManager)
        {
            this.learningLanternContext = learningLanternContext;
            this.userManager = userManager;
        }

        public async Task<List<QuizDTO>> GetAsync(string userId)
        {

            return await learningLanternContext.Quizes.Where(quiz => quiz.InstructorId == userId).Select(quiz => new QuizDTO(quiz)).ToListAsync();

        }

        public async Task<int?> AddAsync(AddQuizDTO addQuizDTO)
        {
            var user = await userManager.FindByIdAsync(addQuizDTO.InstructorId);

            if (user is null)
            {
                return null;
            }

            var quiz = await learningLanternContext.Quizes.AddAsync(new QuizModel(addQuizDTO, user));

            if (quiz is null)
            {
                return 0;
            }

            var saveChangesAsyncResult = await learningLanternContext.SaveChangesAsync();

            return saveChangesAsyncResult == 0 ? 0 : quiz.Entity.Id;
        }


        public async Task<bool?> UpdateAsync(QuizDTO quizDTO)
        {
            var user = await userManager.FindByIdAsync(quizDTO.InstructorId);

            if (user is null)
            {
                return null;
            }

            var quiz = learningLanternContext.Quizes.Update(new QuizModel(quizDTO, user));

            if (quiz is null)
            {
                return false;
            }

            var saveChangesAsyncResult = await learningLanternContext.SaveChangesAsync();

            return saveChangesAsyncResult != 0;
        }

        public async Task<bool?> RemoveAsync(int quizId)
        {
            var quiz = learningLanternContext.Quizes.Remove(new QuizModel() { Id = quizId });

            if (quiz is null)
            {
                return false;
            }

            var saveChangesAsyncResult = await learningLanternContext.SaveChangesAsync();

            return saveChangesAsyncResult != 0;
        }
    }
}
