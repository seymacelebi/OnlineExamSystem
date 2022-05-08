using Business.Abstract;
using Business.Constants;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        //[ValidationAspect(typeof(UserValidator), Priority = 1)]
        public void Add(User user)
        {
            _userDal.Add(user);
           
        }

        public void Delete(User user)
        {
            _userDal.Delete(user);
      
        }

        public List<User> GetById(int userId)
        {
            return _userDal.GetList(x => x.Id == userId);
        }

        public User GetByLastName(string lastName)
        {
            return _userDal.Get(u => u.LastName == lastName);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        public User GetByName(string firstName)
        {
            return _userDal.Get(u => u.FirstName == firstName);
        }

        //public List<OperationClaim> GetClaims(User user)
        //{
        //    return _userDal.GetClaims(user);
        //}

        public List<User> GetList()
        {
           return _userDal.GetList().ToList();
            
        }

        public void TransactionalOperation(User user)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            _userDal.Update(user);
            
        }
    }
}