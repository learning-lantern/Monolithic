using API.Classroom.DTOs;
using API.Classroom.Models;
using API.Database;
using API.User.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Classroom.Repositories
{
    public class ClassroomRepository : IClassroomRepository
    {
        private readonly IUserRepository userRepository;
        private readonly LearningLanternContext learningLanternContext;

        public ClassroomRepository(IUserRepository userRepository, LearningLanternContext learningLanternContext)
        {
            this.userRepository = userRepository;
            this.learningLanternContext = learningLanternContext;
        }

        public async Task<ClassroomDTO?> GetAsync(int classroomId, string userId) => await learningLanternContext.ClassroomUsers.Where(classroomUser => classroomUser.UserId == userId && classroomUser.ClassroomId == classroomId).Select(classroomUser => new ClassroomDTO(classroomUser.Classroom)).FirstOrDefaultAsync();

        public async Task<int?> AddAsync(AddClassroomDTO addClassroomDTO, string userId)
        {
            var user = await userRepository.FindUserByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            var classroom = await learningLanternContext.Classrooms.AddAsync(new ClassroomModel(addClassroomDTO));

            if (classroom == null)
            {
                return 0;
            }

            var classroomUser = await learningLanternContext.ClassroomUsers.AddAsync(new ClassroomUserModel(classroom.Entity.Id, userId));

            if (classroomUser == null)
            {
                return 0;
            }

            return await learningLanternContext.SaveChangesAsync() == 0 ? 0 : classroom.Entity.Id;
        }

        public async Task<bool?> AddUserAsync(int classroomId, string userId)
        {
            var user = await userRepository.FindUserByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            var classroom = await learningLanternContext.Classrooms.Where(classroom => classroom.Id == classroomId).FirstOrDefaultAsync();

            if (classroom == null)
            {
                return false;
            }

            var classroomUser = await learningLanternContext.ClassroomUsers.AddAsync(new ClassroomUserModel(classroomId, userId));

            if (classroomUser == null)
            {
                return false;
            }

            return await learningLanternContext.SaveChangesAsync() == 0 ? false : true;
        }

        public async Task<bool?> UpdateAsync(ClassroomDTO classroomDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool?> RemoveAsync(int classroomId)
        {
            throw new NotImplementedException();
        }
    }
}