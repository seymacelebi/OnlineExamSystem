using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
       // List<OperationClaim> GetClaims(User user);
        List<User> GetById(int userId);
        User GetByMail(string email);
        User GetByName(string firstName);
        User GetByLastName(string lastName);
        List<User> GetList();
        void Add(User user);
        void Delete(User user);
        void Update(User user);
        void TransactionalOperation(User user);
    }
}
