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
            ClassroomModel? classroom = classroomRepository.FindByIdAsync(classroomId);
            if (classroom is null)
                return 0;
            EventModel newEvent = new EventModel(eventDTO, classroom);
            await learningLanternContext.Events.AddAsync(newEvent);
            await learningLanternContext.SaveChangesAsync();
            return newEvent.Id;
        }

        public async Task<bool> UpdateAsync(int eventId, AddEventDTO eventDTO)
        {
            EventModel? newEvent = await learningLanternContext.Events.FindAsync(eventId);
            if (newEvent is null)
                return false;
            learningLanternContext.Events.Update(new EventModel(eventId, eventDTO, newEvent.Classroom));
            return await learningLanternContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> RemoveAsync(int eventId)
        {
            learningLanternContext.Events.Remove(new EventModel() {Id = eventId});
            return await learningLanternContext.SaveChangesAsync() != 0;
        }
    }
}