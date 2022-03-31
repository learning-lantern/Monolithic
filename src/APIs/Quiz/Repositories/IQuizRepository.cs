using APIs.Quiz.DTOs;


namespace APIs.Quiz.Repositories
{
    public interface IQuizRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="InstructorId"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<List<QuizDTO>> GetAsync(string InstructorId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addQuizDTO"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<int?> AddAsync(AddQuizDTO addQuizDTO);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quizDTO"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<bool?> UpdateAsync(QuizDTO quizDTO);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns>
        /// 
        /// </returns>
        public Task<bool?> RemoveAsync(int quizId);
    }
}
