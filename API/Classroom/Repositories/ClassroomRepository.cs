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

        public async Task<List<ClassroomDTO>> GetAsync(string userId) => await learningLanternContext.ClassroomUsers.Where(classroomUser => classroomUser.UserId == userId).Select(classroomUser => new ClassroomDTO(classroomUser.Classroom)).ToListAsync();

        public async Task<int?> AddAsync(AddClassroomDTO addClassroomDTO, string userId)
        {
            var user = await userRepository.FindUserByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            var classroom = await learningLanternContext.Classrooms.AddAsync(new ClassroomModel(addClassroomDTO));

            if (classroom == null || await learningLanternContext.SaveChangesAsync() == 0)
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

        public async Task<bool?> AddUserAsync(int classroomId, string requestUserId, string userId)
        {
            var classroomUser = await learningLanternContext.ClassroomUsers.Where(classroomUser => classroomUser.ClassroomId == classroomId && classroomUser.UserId == requestUserId).FirstOrDefaultAsync();

            if (classroomUser == null)
            {
                return null;
            }

            var addAsyncResult = await learningLanternContext.ClassroomUsers.AddAsync(new ClassroomUserModel(classroomId, userId));

            if (addAsyncResult == null)
            {
                return false;
            }

            return await learningLanternContext.SaveChangesAsync() != 0;
        }

        public async Task<bool?> UpdateAsync(ClassroomDTO classroomDTO, string userId)
        {
            var classroomUser = await learningLanternContext.ClassroomUsers.Where(classroomUser => classroomUser.ClassroomId == classroomDTO.Id && classroomUser.UserId == userId).FirstOrDefaultAsync();

            if (classroomUser == null)
            {
                return null;
            }

            var classroom = learningLanternContext.Classrooms.Update(new ClassroomModel(classroomDTO));

            if (classroom == null)
            {
                return false;
            }

            return await learningLanternContext.SaveChangesAsync() != 0;
        }

        public async Task<bool?> RemoveUserAsync(int classroomId, string requestUserId, string userId)
        {
            var requestUser = await userRepository.FindUserByIdAsync(requestUserId);

            if (requestUser == null)
            {
                return null;
            }

            var classroomUser = await learningLanternContext.ClassroomUsers.Where(classroomUser => classroomUser.UserId == userId && classroomUser.ClassroomId == classroomId).FirstOrDefaultAsync();

            if (classroomUser == null)
            {
                return null;
            }

            classroomUser = learningLanternContext.ClassroomUsers.Remove(classroomUser).Entity;

            if (classroomUser == null)
            {
                return false;
            }

            return await learningLanternContext.SaveChangesAsync() != 0;
        }

        public async Task<bool?> RemoveAsync(int classroomId, string userId)
        {
            var classroomUser = await learningLanternContext.ClassroomUsers.Where(classroomUser => classroomUser.ClassroomId == classroomId && classroomUser.UserId == userId).FirstOrDefaultAsync();

            if (classroomUser == null)
            {
                return null;
            }

            var classroom = learningLanternContext.Classrooms.Remove(classroomUser.Classroom).Entity;

            if (classroom == null)
            {
                return false;
            }

            return await learningLanternContext.SaveChangesAsync() != 0;
        }
    }
}