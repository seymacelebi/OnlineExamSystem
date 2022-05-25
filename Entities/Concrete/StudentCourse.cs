using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("StudentCourse")]
    public class StudentCourse : IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CourseId { get; set; }
    }
}
