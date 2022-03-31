using API.Calendar.DTOs;

namespace API.Calendar.Repositories
{
    public interface ICalendarRepository
    {
        public Task<List<EventDTO>?> GetAsync(int classroomId, string userId);

        public Task<int?> AddAsync(AddEventDTO addEventDTO, string userId);

        public Task<bool?> UpdateAsync(EventDTO eventDTO, string userId);

        public Task<bool?> RemoveAsync(int eventId, string userId);
    }
}