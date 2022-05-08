using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IExamService
    {
        List<Exam> GetById(int examId);
        List<Exam> GetByTitle(string examTitle);
        List<Exam> GetByCourseName(string courseName);
        List<Exam> GetList();
        void Add(ExamDto examDto);
        void Delete(Exam examId);
        void Update(Exam examId);
        void TransactionalOperation(Exam exam);
    }
}
