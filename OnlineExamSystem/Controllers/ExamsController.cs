using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
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
 
    public class ExamsController : Controller
    {
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
        public IActionResult AddCourse()
        {
            List<Course> courselist = c.Courses.OrderByDescending(x => x.CourseId).ToList();
            ViewData["list"] = courselist;
            return View();
        }
        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            List<Course> courselist = c.Courses.OrderByDescending(x => x.CourseId).ToList();
            ViewData["list"] = courselist;
            Course courses = new Course();
            courses.Title = course.Title;
            //courses.CourseId = Convert.ToInt32(Session["ad_id"].ToString());
            c.Courses.Add(courses);
            c.SaveChanges();
            return View();
        }
        [HttpGet]
        public IActionResult AddQuestions()
        {
            var studentId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //int studentId = Convert.ToInt32(Session["ad_id"]);
            //List<Course> list = c.Courses.Where(x => x.CourseId == studentId).ToList();
            //ViewBag.list = new SelectList(list, "CourseId", "Title");
           
            return View();//buradan course ıd gönderilip diğer sayfaya gönderilecek yapabilirsen yap yapamazsan yarın bakarız
        }
        [HttpPost]
        public IActionResult AddQuestions(Question question)
        {
            var userId = Convert.ToInt32(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            //int studentId = Convert.ToInt32(Session["ad_id"]);
            List<Course> list = c.Courses.Where(x => x.InstructorId == userId).ToList();
            ViewBag.list = new SelectList(list, "CourseId", "Title");

            Question q = new Question();
            q.QuestionText = question.QuestionText;
            q.QuestionA = question.QuestionA;
            q.QuestionB = question.QuestionB;
            q.QuestionC = question.QuestionC;
            q.QuestionD = question.QuestionD;
            q.QCorrectAns = question.QCorrectAns;

            q.CourseId = 1;//bu nerden geliyor bilmiyorum yarın bakarız
            c.Questions.Add(q);
            c.SaveChanges();
            /*ViewBag.message = "Question successfully added";*///bunuda hallederiz yarın test edip
            return View();
        }
      
        public IActionResult ExamDashboard()
        {
            return View();

        }
        [HttpPost]
        public IActionResult ExamDashboard(string course)
        {
            List<Course> listCourse = c.Courses.ToList();
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
  
        public IActionResult StartQuiz()
        {
           
            //if (Session["studentId"]==null)
            //{
            //    return RedirectToAction("ExamDashboard");
            //}
            Question question = null;
            if (TempData["questions"]==null)
            {
                Queue<Question> qlist = (Queue<Question>)TempData["questions"];
                if (qlist.Count>0)
                {
                    question = qlist.Peek();
                    qlist.Dequeue();

                    TempData["questions"] = qlist;
                    TempData.Keep();
                }
                else
                {
                    return RedirectToAction("EndExam");
                }
            }
            return View(question);
        }

        [HttpPost]
        public IActionResult StartQuiz(Question question)
        {
            string correctans;           
            if (question.QuestionA!=null)
            {
                correctans = "A";
            }
            else if (question.QuestionB != null)
            {
                correctans = "B";   
            }
            else if (question.QuestionC != null)
            {
                correctans = "C";
            }
            else if (question.QuestionD != null)
            {
                correctans = "D";
            }

            //if (correctans.Equals(question.QuestionA))
            //{
            //    TempData["score"] = Convert.ToInt32(TempData["score"]) + 1 ;
            //}
            TempData.Keep();
        
            return RedirectToAction("StartQuiz");
        }
        public IActionResult ViewAllQuestions(int ?id, int page)
        {
            //if (Session["ad_id"]==null) 
            //{              
            //    return RedirectToAction("tlogin");
            //}
            if (id==null)
            {
                return RedirectToAction("Dashboard");
            }
            int pagesize = 15, pageIndex = 1;
         
            return View(c.Questions.Where(x => x.CourseId == id).ToList());
            
        }
        public IActionResult EndExam(Question question)
        {
            return View();
        }
    }
}
