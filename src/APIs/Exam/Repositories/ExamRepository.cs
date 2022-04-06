using APIs.Database;
using APIs.Exam.DTOs;
using APIs.Exam.Models;
using APIs.User.Repositories;
using Microsoft.EntityFrameworkCore;

namespace APIs.Exam.Repositories
{
    public class ExamRepository : IExamRepository
    {
        private readonly LearningLanternContext learningLanternContext;
        private readonly IUserRepository userRepository;

        public ExamRepository(LearningLanternContext learningLanternContext, IUserRepository userRepository)
        {
            this.learningLanternContext = learningLanternContext;
            this.userRepository = userRepository;
        }

        public async Task<List<ExamDTO>> GetAsync(string instructorId)
        {
            
                return await learningLanternContext.Exams.Where(exam => exam.InstructorId == instructorId).Select(exam => new ExamDTO(exam)).ToListAsync();
            
        }

        public async Task<int?> AddAsync(AddExamDTO addExamDTO)
        {
            var user = await userRepository.FindUserByIdAsync(addExamDTO.InstructorId);

            if (user == null)
            {
                return null;
            }

            var exam = await learningLanternContext.Exams.AddAsync(new ExamModel(addExamDTO));

            if (exam == null)
            {
                return 0;
            }

            return await learningLanternContext.SaveChangesAsync() == 0 ? 0 : exam.Entity.Id;
        }

        public async Task<bool?> UpdateAsync(ExamDTO examDTO)
        {
            var exam = await learningLanternContext.Exams.Where(exam => exam.Id == examDTO.Id && exam.InstructorId == examDTO.InstructorId).FirstOrDefaultAsync();

            if (exam == null)
            {
                return null;
            }

            exam = learningLanternContext.Exams.Update(new ExamModel(examDTO)).Entity;

            if (exam == null)
            {
                return false;
            }

            return await learningLanternContext.SaveChangesAsync() != 0;
        }

        public async Task<bool?> RemoveAsync(int examId)
        {
            var exam = await learningLanternContext.Exams.Where(exam => exam.Id == examId).FirstOrDefaultAsync();

            if (exam == null)
            {
                return null;
            }

            exam = learningLanternContext.Exams.Remove(exam).Entity;

            if (exam == null)
            {
                return false;
            }

            return await learningLanternContext.SaveChangesAsync() != 0;
        }
    }
}
