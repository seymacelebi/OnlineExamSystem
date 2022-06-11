using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CourseManager : ICourseService
    {
        ICourseDal _courseDal;
        public CourseManager(ICourseDal courseDal)
        {
            _courseDal = courseDal;
        }
        public void Add(Course course)
        {
            var courses = new Course
            {
               Title =course.Title,
               CourseId =course.CourseId,
               AddedAt =course.AddedAt,
               UserId=course.UserId
            };
            _courseDal.Add(courses);
        }

        public void Delete(Course course)
        {
            throw new NotImplementedException();
        }

        public List<Course> GetById(int course)
        {
            return _courseDal.GetList(x => x.CourseId == course);
        }

        public List<Course> GetByTitle(string course)
        {
            throw new NotImplementedException();
        }

        public List<Course> GetList()
        {
            return _courseDal.GetList().ToList();
        }

        public void TransactionalOperation(Course course)
        {
            throw new NotImplementedException();
        }

        public void Update(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
