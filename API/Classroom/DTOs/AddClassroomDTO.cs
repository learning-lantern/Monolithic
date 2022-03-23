﻿using System.ComponentModel.DataAnnotations;

namespace API.Classroom.DTOs
{
    public class AddClassroomDTO
    {
        [Required, StringLength(30)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
