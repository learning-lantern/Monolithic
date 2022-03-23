using System.ComponentModel.DataAnnotations;

namespace API.Calendar.DTOs
{
    public class EventDTO : AddEventDTO
    {
        [Required, Key]
        public int Id { get; set; }

        public EventDTO() { }
        public EventDTO(AddEventDTO addEventDTO) : base(addEventDTO) { }
        public EventDTO(EventDTO eventDTO) : base(eventDTO)
        {
            Id = eventDTO.Id;
        }
    }
}