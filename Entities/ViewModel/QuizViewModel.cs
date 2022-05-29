using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModel
{
    public class QuizViewModel
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public string OkeyQuestion { get; set; }
        public string Answer { get; set; }
    }
}
