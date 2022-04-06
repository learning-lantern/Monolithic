using APIs.Exam.DTOs;

namespace APIs.Exam.Repositories
{
    public interface IExamRepository
    {
        public Task<List<ExamDTO>> GetAsync(string userId);

        public Task<int?> AddAsync(AddExamDTO addExamDTO);

        public Task<bool?> UpdateAsync(ExamDTO examDTO);

        public Task<bool?> RemoveAsync(int examId);

    }
}
