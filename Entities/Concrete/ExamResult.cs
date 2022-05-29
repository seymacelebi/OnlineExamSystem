using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class ExamResult : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int Score { get; set; }
        public int LevelQuiz { get; set; }
       
    }
}
