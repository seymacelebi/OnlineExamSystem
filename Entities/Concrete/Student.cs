using Core.Entities.Abstract;
using System;

namespace Entities.Concrete
{
    public class Student : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StudentId { get; set; }
        public User User{ get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
