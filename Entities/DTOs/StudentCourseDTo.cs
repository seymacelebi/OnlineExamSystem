using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class StudentCourseDTo
    {
        public List<User> Student { get; set; }
        public List<Course> Course { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
    }
}
