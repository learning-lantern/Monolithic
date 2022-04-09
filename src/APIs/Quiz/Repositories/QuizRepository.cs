using APIs.Database;
using APIs.Quiz.DTOs;
using APIs.Quiz.Models;
using APIs.User.Repositories;
using Microsoft.EntityFrameworkCore;


namespace APIs.Quiz.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly LearningLanternContext learningLanternContext;
        private readonly IUserRepository userRepository;

        public QuizRepository(LearningLanternContext learningLanternContext, IUserRepository userRepository)
        {
            this.learningLanternContext = learningLanternContext;
            this.userRepository = userRepository;
        }

        public async Task<List<QuizDTO>> GetAsync(string userId)
        {
            return await learningLanternContext.Quizes.Where(quiz => quiz.UserId == userId).Select(quiz => new QuizDTO(quiz)).ToListAsync();
        }

        public async Task<List<QuizDTO>> GetAsync(int classroomId)
        {
            return await learningLanternContext.Quizes.Where(quiz => quiz.ClassroomId == classroomId).Select(quiz => new QuizDTO(quiz)).ToListAsync();
        }

        public async Task<int?> AddAsync(AddQuizDTO addQuizDTO, string userId)
        {
            var user = await userRepository.FindUserByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            var quiz = await learningLanternContext.Quizes.AddAsync(new QuizModel(addQuizDTO, userId));

            return (quiz != null && await learningLanternContext.SaveChangesAsync() != 0) ? quiz.Entity.Id : 0;
        }


        public async Task<bool?> UpdateAsync(QuizDTO quizDTO)
        {
            var user = await userRepository.FindUserByIdAsync(quizDTO.UserId);

            if (user == null)
            {
                return null;
            }

            var quiz = learningLanternContext.Quizes.Update(new QuizModel(quizDTO));

            return quiz != null && await learningLanternContext.SaveChangesAsync() != 0;
        }

        public async Task<bool?> RemoveAsync(int quizId, string userId)
        {
            var quiz = await learningLanternContext.Quizes.Where(quiz => quiz.Id == quizId && quiz.UserId == userId).FirstOrDefaultAsync();

            if (quiz == null)
            {
                return null;
            }

            quiz = learningLanternContext.Quizes.Remove(quiz).Entity;

            return quiz != null && await learningLanternContext.SaveChangesAsync() != 0;
        }
    }
}
