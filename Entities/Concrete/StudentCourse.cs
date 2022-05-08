using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class StudentCourse : IEntity
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
