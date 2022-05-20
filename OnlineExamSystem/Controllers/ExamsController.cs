using DataAccess.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            //int studentId = Convert.ToInt32(Session["ad_id"]);
            //List<Course> list = c.Courses.Where(x => x.CourseId == studentId).ToList();
            //ViewBag.list = new SelectList(list, "CourseId", "Title");
            return View();
        }
        [HttpPost]
        public IActionResult AddQuestions(Question question)
        {
            var userId = Convert.ToInt32(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            //int studentId = Convert.ToInt32(Session["ad_id"]);
            //List<Course> list = c.Courses.Where(x => x.CourseId == studentId).ToList();
            //ViewBag.list = new SelectList(list, "CourseId", "Title");

            Question q = new Question();
            q.QuestionText = question.QuestionText;
            q.QuestionA = question.QuestionA;
            q.QuestionB = question.QuestionB;
            q.QuestionC = question.QuestionC;
            q.QuestionD = question.QuestionD;
            q.QCorrectAns = question.QCorrectAns;

            q.CourseId = question.CourseId;
            c.Questions.Add(q);
            c.SaveChanges();
            ViewBag.message = "Question successfully added";
            return View();
        }
        [HttpGet]
        public IActionResult StartQuiz()
        {
            if (TempData["i"] ==null)
            {
                TempData["i"] = 1;
            }
           
            //if (Session["StudentId"]==null)
            //{
            //    return RedirectToAction("studentlogin");
            //}
            if (TempData["ExamId"] == null)
            {
                return RedirectToAction("ExamDashboard");

            }
            try
            {
                Question question = null;
                int examId = Convert.ToInt32(TempData["ExamId"].ToString());

                if (TempData["Id"] == null)
                {
                    int q_id =Convert.ToInt32(TempData["Id"].ToString());
                    question = c.Questions.Where(x=>x.Id== q_id &&  x.CourseId == examId).SingleOrDefault();
                    var list =c.Questions.Skip(Convert.ToInt32(TempData["i"].ToString())).ToList();
                    //var list = c.Questions.Skip(1);
                     q_id = list.First().Id;
                    TempData["Id"] = ++question.Id;

                }
                else
                {
                    int questionId = Convert.ToInt32(TempData["questionId"].ToString());
                    question = c.Questions.Where(x => x.Id == questionId && x.CourseId == examId).SingleOrDefault();
                    var list = c.Questions.Skip(Convert.ToInt32(TempData["i"].ToString())).ToList();
                    questionId = list.First().Id;
                    TempData["Id"] = ++question.Id;
                    TempData[i] = Convert.ToInt32(TempData["i"].ToString()) + 1;

                }
                TempData.Keep();
                return View(question);

            }
            catch (Exception)
            {
                return RedirectToAction("studentlogin");

            }

        }
        [HttpPost]
        public IActionResult StartQuiz(Question question)
        {
            return RedirectToAction("StartQuiz");
        }
    }
}
