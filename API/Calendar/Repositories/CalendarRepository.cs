//using API.Calendar.DTOs;
//using API.Calendar.Models;
//using API.Classroom.Repositories;
//using API.Database;
//using Microsoft.EntityFrameworkCore;

//namespace API.Calendar.Repositories
//{
//    public class CalendarRepository : ICalendarRepository
//    {
//        private readonly LearningLanternContext learningLanternContext;
//        private readonly IClassroomRepository classroomRepository;

//        public CalendarRepository(LearningLanternContext learningLanternContext,
//            IClassroomRepository classroomRepository)
//        {
//            this.learningLanternContext = learningLanternContext;
//            this.classroomRepository = classroomRepository;
//        }

//        public async Task<List<EventDTO>> GetAsync(int classroomId)
//        {
//            return await learningLanternContext.Events
//                .Where(eventModel => eventModel.ClassroomId == classroomId)
//                .Select(eventModel => new EventDTO(eventModel)).ToListAsync();
//        }

//        public async Task<int?> AddAsync(AddEventDTO addEventDTO)
//        {
//            var classroom = await classroomRepository.GetAsync(addEventDTO.ClassroomId);

//            if (classroom == null)
//            {
//                return null;
//            }

//            var eventModel = await learningLanternContext.Events.AddAsync(new EventModel(addEventDTO, classroom));

//            if (eventModel == null)
//            {
//                return 0;
//            }

//            return await learningLanternContext.SaveChangesAsync() == 0 ? 0 : eventModel.Entity.Id;
//        }

//        public async Task<bool?> UpdateAsync(EventDTO eventDTO)
//        {
//            var classroom = await classroomRepository.GetAsync(eventDTO.ClassroomId);

//            if (classroom == null)
//            {
//                return null;
//            }

//            var eventModel = learningLanternContext.Events.Update(new EventModel(eventDTO, classroom));

//            if (eventModel == null)
//            {
//                return false;
//            }

//            return await learningLanternContext.SaveChangesAsync() != 0;
//        }

//        public async Task<bool?> RemoveAsync(int eventId)
//        {
//            var eventModel = learningLanternContext.Events.Remove(new EventModel() { Id = eventId });

//            if (eventModel == null)
//            {
//                return false;
//            }

//            return await learningLanternContext.SaveChangesAsync() != 0;
//        }
//    }
//}