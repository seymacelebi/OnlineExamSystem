using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Question : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Question")]
        public string QuestionText { get; set; }
        public char QuestionA { get; set; }
        public char QuestionB { get; set; }
        public char QuestionC { get; set; }
        public char QuestionD { get; set; }
        public string QCorrectAns { get; set; }
        public int CourseId { get; set; }
    }
}
