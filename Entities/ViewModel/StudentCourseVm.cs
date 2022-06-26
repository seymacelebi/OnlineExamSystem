using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModel
{
    public class StudentCourseVm
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string  CourseName { get; set; }
        public int CourseId { get; set; }
    }
}
