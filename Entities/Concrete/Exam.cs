using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    [Table("Exam")]
    public class Exam : IEntity
    {
        public int ExamId { get; set; }
        public string Title { get; set; }
        //public string? Information { get; set; }
        //public int NumberOfQuestion { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool check { get; set; }

        public  User User { get; set; }
        public int  UserId { get; set; }
        public  Course Course { get; set; }
        public int? CourseId { get; set; }
    }
}
