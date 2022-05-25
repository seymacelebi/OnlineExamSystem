using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class ExamResult : IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ExamId { get; set; }
        public int Score { get; set; }
       // public DateTime PublishedAt { get; set; }
    }
}
