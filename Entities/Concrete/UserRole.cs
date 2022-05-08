using Entities.Concrete;
using System;
using System.Text;

namespace Core.Entities.Concrete
{

    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
