using Business.Concrete;
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
        CourseManager courseManager = new CourseManager (new EfCourseDal());
        Context c = new Context();
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetList()
        {
            //var examvalues = examManager.GetList();
            //return View(examvalues);
            var coursevalues = courseManager.GetList();
            return View(coursevalues);
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
            var course = c.Course.Where(x => x.CourseId == model.CourseId).FirstOrDefault();
            course.check = true;
            c.Course.Update(course);
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

        public IActionResult ExamResultStudent()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var rest = c.Course.Where(x => x.UserId == Convert.ToInt32(userId)).ToList();


            return View(rest);
        }
       
        public IActionResult ExamResultStudentScore(int CourseId)
        {
            
            var result = from ur in c.ExamResult
                         join r in c.Course
                         on ur.CourseId equals r.CourseId
                         join u in c.Users
                         on ur.UserId equals u.UserId
                         where ur.CourseId == CourseId

                         select new ExamResultStudentVm
                         {
                             User = u.FirstName +" "+ u.LastName,
                             Result = ur.Score,
                             level=ur.LevelQuiz,


                         };
            var a = result.ToList();

            return View(result.ToList());
        }

        //public IActionResult StartQuiz(int CourseId)
        //{
        //    var question = c.Questions.Where(x => x.CourseId == CourseId).ToList();
        //    StartQuizVm startQuizVm = new StartQuizVm()
        //    {
        //        Question = question,
        //        CourseId = CourseId
        //    };



        //    return View(startQuizVm);
        //}


        //public IActionResult ViewAllQuestions(QuizVM model)
        //{
        //    //if (Session["ad_id"]==null) 
        //    //{              
        //    //    return RedirectToAction("tlogin");
        //    //}
        //    //if (id==null)
        //    //{
        //    //    return RedirectToAction("Dashboard");
        //    //}
        //    //int pagesize = 15, pageIndex = 1;
         
        //    //return View(c.Questions.Where(x => x.CourseId == model.CourseId).ToList());
        //    return View();
            
        //}
        public IActionResult EndExam(Question question)
        {
            return View();
        }

    }
}
