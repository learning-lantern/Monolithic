using API.Calendar.DTOs;

namespace API.Calendar.Repositories
{
    public interface ICalendarRepository
    {
        public Task<List<EventDTO>> GetAsync(int classroomId);

        public Task<int?> AddAsync(AddEventDTO addEventDTO);

        public Task<bool?> UpdateAsync(EventDTO eventDTO);

        public Task<bool?> RemoveAsync(int eventId);
    }
}