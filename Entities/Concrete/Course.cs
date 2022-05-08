using Core.Entities.Abstract;
using System;

namespace Entities.Concrete
{
    public class Course : IEntity
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
