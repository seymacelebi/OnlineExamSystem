using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IStudentService
    {
        List<Student> GetById(int userId);
        List<Student> GetByStudentId(string studentId);
        List<Student> GetList();
        void Add(Student student);
        void Delete(Student student);
        void Update(Student student);
    }
}
