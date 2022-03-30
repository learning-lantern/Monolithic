﻿using API.Classroom.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Classroom.DTOs
{
    public class ClassroomDTO : AddClassroomDTO
    {
        [Required, Key]
        public int Id { get; set; }

        public ClassroomDTO() { }
        public ClassroomDTO(AddClassroomDTO addTaskDTO) : base(addTaskDTO) { }
        public ClassroomDTO(ClassroomDTO classroomDTO) : base(classroomDTO)
        {
            Id = classroomDTO.Id;
        }
    }
}
