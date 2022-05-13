using DataAccess.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineExamSystem.Controllers
{
    public class ExamsController : Controller
    {
        Context c = new Context();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddQuestions()
        {
            int studentId = Convert.ToInt32(Session["ad_id"]);
            List<Course> list = c.Courses.Where(x => x.CourseId == studentId).ToList();
            ViewBag.list = new SelectList(list, "CourseId", "Title");
            return View();
        }
        [HttpPost]
        public IActionResult AddQuestions(Question question)
        {
            int studentId = Convert.ToInt32(Session["ad_id"]);
            List<Course> list = c.Courses.Where(x => x.CourseId == studentId).ToList();
            ViewBag.list = new SelectList(list, "CourseId", "Title");
            
            Question q = new Question();
            q.QuestionText = question.QuestionText;
            q.QuestionA=  question.QuestionA;
            q.QuestionB= question.QuestionB;
            q.QuestionC= question.QuestionC;
            q.QuestionD= question.QuestionD;    
            q.QCorrectAns  = question.QCorrectAns;

            q.CourseId = question.CourseId;
            c.Questions.Add(q);
            c.SaveChanges();
            ViewBag.message = "Question successfully added";
            return View();
        }
    }
}
