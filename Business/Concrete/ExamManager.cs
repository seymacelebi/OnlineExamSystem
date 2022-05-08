using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ExamManager : IExamService
    {
        IExamDal _examDal;
        public ExamManager(IExamDal examDal)
        {
            _examDal = examDal;
        }
        public void Add(ExamDto examDto)
        {
            var exam = new Exam
            {
                Title = examDto.Title,
                Information = examDto.Information,
                NumberOfQuestion = examDto.NumberOfQuestion,
                AddedAt = DateTime.Now,
                StartTime = DateTime.Parse(examDto.StartTime),
                EndTime = DateTime.Parse(examDto.EndTime)
            };
            _examDal.Add(exam);
        }

        public void Delete(Exam examId)
        {
            _examDal.Delete(examId);
        }

        public List<Exam> GetByCourseName(string courseName)
        {
            throw new NotImplementedException();
        }

        public List<Exam> GetById(int examId)
        {
            return _examDal.GetList(x => x.Id == examId);

        }

        public List<Exam> GetByTitle(string examTitle)
        {
            throw new NotImplementedException();
        }

        public List<Exam> GetList()
        {
            return _examDal.GetList().ToList();
        }

        public void TransactionalOperation(Exam exam)
        {
            throw new NotImplementedException();
        }

        public void Update(Exam examId)
        {
            _examDal.Update(examId);
        }
    }
}
