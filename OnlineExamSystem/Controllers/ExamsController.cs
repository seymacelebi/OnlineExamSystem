﻿using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    public class ExamsController : Controller
    {
        public class QuizVM
        {
            public List<QuizViewModel> Questions { get; set; } = new List<QuizViewModel>();
            public int CourseId { get; set; }
        }
        ExamManager examManager = new ExamManager(new EfExamDal());
        Context c = new Context();
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var examvalues = examManager.GetList();
            return View(examvalues);
        }
        
        [HttpGet]
        [Authorize(Policy = "Ogretmen")]
        public IActionResult AddCourse()
        {
            List<Course> courselist = c.Course.OrderByDescending(x => x.CourseId).ToList();
            ViewData["list"] = courselist;
            return View();
        }
        [HttpPost]
        [Authorize(Policy = "Ogretmen")]
        public IActionResult AddCourse(Course course)
        {
            List<Course> courselist = c.Course.OrderByDescending(x => x.CourseId).ToList();
            ViewData["list"] = courselist;
            Course courses = new Course();
            courses.Title = course.Title;
            //courses.CourseId = Convert.ToInt32(Session["ad_id"].ToString());
            c.Course.Add(courses);
            c.SaveChanges();
            return View();
        }
        [HttpGet]
        [Authorize(Policy = "Ogretmen")]
        public IActionResult AddQuestions(int CourseId)
        {
            var studentId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

           
           
            return View(CourseId);
        }
        [HttpPost]
        [Authorize(Policy = "Ogretmen")]
        public IActionResult AddQuestions(Question question)
        {
         
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
            
            return View(question.CourseId);
        }
      
        public IActionResult ExamDashboard()
        {
            return View();

        }
        [HttpPost]
        public IActionResult ExamDashboard(string course)
        {
            List<Course> listCourse = c.Course.ToList();
            foreach (var item in listCourse)
            {
                if (item.Title==course)
                {
                    List<Question> list = c.Questions.Where(x => x.CourseId == item.CourseId).ToList();
                    Queue<Question> queue = new Queue<Question>();
                    foreach (Question question in list)
                    {
                        queue.Enqueue(question);
                    }
                    TempData["questions"] = queue;

                    TempData["score"] = 0;

                    TempData["examid"] = item.CourseId;
                    TempData.Keep();
                    return RedirectToAction("Start Quiz");

                }
                else
                {
                    ViewBag.error = "No course found ...";
                }
            }
            return View();
        }
  
        public IActionResult StartQuiz(int CourseId)
        {
            var question = c.Questions.Where(x => x.CourseId == CourseId).ToList();
            StartQuizVm startQuizVm = new StartQuizVm()
            {
                Question = question,
                CourseId=CourseId
            };
            
              
       
            return View(startQuizVm);
        }

       
        [HttpPost]
        public IActionResult StartQuiz(QuizVM model)
        {
            var studentId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Result = 0;
            foreach (var x in model.Questions)
            {
                var check = x.OkeyQuestion.Trim();
                if (x.Answer == check)
                {
                    Result += 10;
                }
            }
            ExamResult examResult = new ExamResult()
            {
                CourseId = model.CourseId,
                LevelQuiz = 1,
                Score = Result,
                UserId = Convert.ToInt32(studentId)


            };
            c.ExamResult.Add(examResult);
            c.SaveChanges();
            return Json("/Course/Index");
        }



        public IActionResult ExamResult()
        {
            var studentId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = from ur in c.ExamResult
                         join r in c.Course
                         on ur.CourseId equals r.CourseId
                         where ur.UserId == Convert.ToInt32(studentId)
                         
                         select new ExamResultVm
                         {
                            ClassName=r.Title,
                            Result=ur.Score,
                            Level=ur.LevelQuiz
                            
                         };


            return View(result.ToList());
        }



        public IActionResult ViewAllQuestions(int ?id, int page)
        {
            //if (Session["ad_id"]==null) 
            //{              
            //    return RedirectToAction("tlogin");
            //}
            //if (id==null)
            //{
            //    return RedirectToAction("Dashboard");
            //}
            int pagesize = 15, pageIndex = 1;
         
            return View(c.Questions.Where(x => x.CourseId == 1).ToList());
            
        }
        public IActionResult EndExam(Question question)
        {
            return View();
        }
    }
}
