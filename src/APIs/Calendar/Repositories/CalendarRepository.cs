using APIs.Calendar.DTOs;
using APIs.Calendar.Models;
using APIs.Classroom.Repositories;
using APIs.Database;
using Microsoft.EntityFrameworkCore;

namespace APIs.Calendar.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly LearningLanternContext learningLanternContext;
        private readonly IClassroomRepository classroomRepository;

        public CalendarRepository(LearningLanternContext learningLanternContext,
            IClassroomRepository classroomRepository)
        {
            this.learningLanternContext = learningLanternContext;
            this.classroomRepository = classroomRepository;
        }

        public async Task<List<EventDTO>?> GetAsync(int classroomId, string userId)
        {
            var classroomUser = await learningLanternContext.ClassroomUsers.Where(classroomUser => classroomUser.UserId == userId && classroomUser.ClassroomId == classroomId).FirstOrDefaultAsync();

            if (classroomUser == null)
            {
                return null;
            }

            return await learningLanternContext.Events.Where(eventModel => eventModel.ClassroomId == classroomId).Select(eventModel => new EventDTO(eventModel)).ToListAsync();
        }

        public async Task<int?> AddAsync(AddEventDTO addEventDTO, string userId)
        {
            var classroomUser = await learningLanternContext.ClassroomUsers.Where(classroomUser => classroomUser.UserId == userId && classroomUser.ClassroomId == addEventDTO.ClassroomId).FirstOrDefaultAsync();

            if (classroomUser == null)
            {
                return null;
            }

            var eventModel = await learningLanternContext.Events.AddAsync(new EventModel(addEventDTO));

            return (eventModel != null && await learningLanternContext.SaveChangesAsync() != 0) ? eventModel.Entity.Id : 0;
        }

        public async Task<bool?> UpdateAsync(EventDTO eventDTO, string userId)
        {
            var eventModel = await learningLanternContext.Events.Where(eventModel => eventModel.Id == eventDTO.Id && eventModel.ClassroomId == eventDTO.ClassroomId).FirstOrDefaultAsync();

            if (eventModel == null)
            {
                return null;
            }

            var classroomUser = await learningLanternContext.ClassroomUsers.Where(classroomUser => classroomUser.UserId == userId && classroomUser.ClassroomId == eventModel.ClassroomId).FirstOrDefaultAsync();

            if (classroomUser == null)
            {
                return null;
            }

            eventModel = learningLanternContext.Events.Update(new EventModel(eventDTO)).Entity;

            return eventModel != null && await learningLanternContext.SaveChangesAsync() != 0;
        }

        public async Task<bool?> RemoveAsync(int eventId, string userId)
        {
            var eventModel = await learningLanternContext.Events.Where(eventModel => eventModel.Id == eventId).FirstOrDefaultAsync();

            if (eventModel == null)
            {
                return null;
            }

            var classroomUser = await learningLanternContext.ClassroomUsers.Where(classroomUser => classroomUser.UserId == userId && classroomUser.ClassroomId == eventModel.ClassroomId).FirstOrDefaultAsync();

            if (classroomUser == null)
            {
                return null;
            }

            eventModel = learningLanternContext.Events.Remove(eventModel).Entity;

            return eventModel != null && await learningLanternContext.SaveChangesAsync() != 0;
        }
    }
}