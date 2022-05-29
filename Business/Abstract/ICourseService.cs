using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
   public interface ICourseService
    {
        List<Course> GetById(int course);
        List<Course> GetByTitle(string course);
        List<Course> GetList();
        void Add(Course course);
        void Delete(Course course);
        void Update(Course course);
        void TransactionalOperation(Course course);
    }
}
