using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class Question : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "QuestionText")]
        public string QuestionText { get; set; }
        public string QuestionA { get; set; }
        public string QuestionB { get; set; }
        public string QuestionC { get; set; }
        public string QuestionD { get; set; }
        public string QCorrectAns { get; set; }

        //[ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        public int CourseId { get; set; }
    }
}
