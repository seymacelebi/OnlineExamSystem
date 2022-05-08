using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class ExamQuestion : IEntity
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int QuestionId { get; set; }
        public int Point { get; set; }
    }
}
