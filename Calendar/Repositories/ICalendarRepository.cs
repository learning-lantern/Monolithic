using API.Calendar.DTOs;

namespace API.Calendar.Repositories
{
    public interface ICalendarRepository
    {
        public Task<List<EventDTO>> GetAllEventsAsync(int classroomId);

        public Task<int> AddAsync(int classroomId, AddEventDTO newEvent);

        public Task<bool> UpdateAsync(int eventId, AddEventDTO eventDTO);

        public Task<bool> RemoveAsync(int eventId);
    }
}