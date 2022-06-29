using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CourseDto
    {
        public int UserId { get; set; }
        public string? Title { get; set; }
        public List<User>? User { get; set; }
    }
}
