using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Exam : IEntity
    {
        public int ExamId { get; set; }
        public string Title { get; set; }
        public string Information { get; set; }
        public int NumberOfQuestion { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
