using API.Calendar.DTOs;
using API.Calendar.Models;
using API.Classroom.Models;
using API.Classroom.Repositories;
using API.Database;
using Microsoft.EntityFrameworkCore;

namespace API.Calendar.Repositories
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

        public async Task<List<EventDTO>> GetAllEventsAsync(int classroomId)
        {
            return await learningLanternContext.Events.Where(x => x.ClassroomId == classroomId)
                .Select(x => new EventDTO(x)).ToListAsync();
        }

        public async Task<int> AddAsync(int classroomId, AddEventDTO eventDTO)
        {
            if (await classroomRepository.FindByIdAsync(classroomId) is null)
                return 0;
            EventModel newEvent = new EventModel(eventDTO, classroomId);
            await learningLanternContext.Events.AddAsync(newEvent);
            await learningLanternContext.SaveChangesAsync();
            return newEvent.Id;
        }

        public async Task<bool> UpdateAsync(int eventId, AddEventDTO eventDTO)
        {
            EventModel? currentEvent = await learningLanternContext.Events.FindAsync(eventId);
            if (currentEvent is null)
                return false;
            learningLanternContext.Events.Update(new EventModel(eventId, eventDTO, currentEvent.ClassroomId));
            return await learningLanternContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> RemoveAsync(int eventId)
        {
            learningLanternContext.Events.Remove(new EventModel() {Id = eventId});
            return await learningLanternContext.SaveChangesAsync() != 0;
        }
    }
}