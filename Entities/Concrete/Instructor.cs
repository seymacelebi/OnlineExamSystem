using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Instructor : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
    }
}
