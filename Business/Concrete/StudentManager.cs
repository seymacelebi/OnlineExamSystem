using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class StudentManager : IStudentService
    {
        private IStudentDal _studentDal;

        public StudentManager(IStudentDal studentDal)
        {
            _studentDal = studentDal;
        }

        public void Add(Student student)
        {
            _studentDal.Add(student);
            
        }

        public void Delete(Student student)
        {
            _studentDal.Delete(student);
          
        }

        public List<Student> GetById(int userId)
        {
            return _studentDal.GetList(x => x.Id == userId);
           
        }

        public List<Student> GetByStudentId(int studentId)
        {
            return _studentDal.GetList(x => x.StudentId == studentId);
        }

        public List<Student> GetList()
        {
            return _studentDal.GetList().ToList();
        }

        public void Update(Student student)
        {
            _studentDal.Update(student);
            
        }
    }
}
