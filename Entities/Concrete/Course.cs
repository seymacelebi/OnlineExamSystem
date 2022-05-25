using Core.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("Course")]
    public class Course : IEntity
    {
        [Key]
        public int CourseId { get; set; }
        public string Title { get; set; }
        public DateTime AddedAt { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int UserId { get; set; }
    }
}
