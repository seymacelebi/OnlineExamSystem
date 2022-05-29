using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModel
{
    public class StartQuizVm
    {
        public List<Question> Question { get; set; }
        public int CourseId { get; set; }
    }
}
