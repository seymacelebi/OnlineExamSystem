using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ExamDto : IDto
    {
        public string Title { get; set; }
        //public string Information { get; set; }
        //public int NumberOfQuestion { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool check { get; set; }
        public int? CourseId { get; set; }
        public int UserId { get; set; }


    }
}
