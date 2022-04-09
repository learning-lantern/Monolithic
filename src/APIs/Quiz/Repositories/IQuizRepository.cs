using APIs.Quiz.DTOs;


namespace APIs.Quiz.Repositories
{
    public interface IQuizRepository
    {
        public Task<List<QuizDTO>> GetAsync(string userId);

        public Task<List<QuizDTO>> GetAsync(int classroomId);

        public Task<int?> AddAsync(AddQuizDTO addQuizDTO, string userId);

        public Task<bool?> UpdateAsync(QuizDTO quizDTO);

        public Task<bool?> RemoveAsync(int quizId, string userId);
    }
}
