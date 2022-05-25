using Core.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class Course : IEntity
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public DateTime AddedAt { get; set; }

        [ForeignKey("UserId")]
        public User Instructor { get; set; }

        public int InstructorId { get; set; }
    }
}
