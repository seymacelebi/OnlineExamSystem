using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class StudentsController : Controller
    {
        //IStudentService studentManager = new StudentManager(new EfStudentDal());
        IUserService _userService;
        Context c = new Context();


        public StudentsController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Policy = "Ogretmen")]
        //[Authorize(Policy = "Admin")]
        public IActionResult StudentList(User Student)
        {
            //List<User> list = c.Users.Where(x => x.IsStudent == Student.IsStudent).ToList();
            List<User> list = c.Users.Where(x => x.IsStudent == true).ToList();

            return View(list);
        }
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public IActionResult AddStudent(User User)
        {
            _userService.Add(User);
            return RedirectToAction("UserList");
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Delete(int userId)
        {
            var user = _userService.GetById(userId);
            _userService.Delete(user);
            return RedirectToAction("StudentList");
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult UserList(User user)
        {
            //List<User> list = c.Users.Where(x => x.IsStudent == Student.IsStudent).ToList();
            List<User> list = c.Users.Where(x => x.IsStudent == true || x.IsStudent == false && x.IsAdmin == false).ToList();

            return View(list);
        }
        [Authorize(Policy = "Admin")]

        public IActionResult StudentAssignedCourse()
        {
            StudentCourseDTo studentCourseDTo = new StudentCourseDTo();
            studentCourseDTo.Course = c.Course.ToList();
            studentCourseDTo.Student = c.Users.Where(x => x.IsStudent == true && x.IsAdmin == false).ToList();

            return View(studentCourseDTo);
        }
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public IActionResult StudentAssignedCourse(StudentCourseDTo studentCourseDTo)
        {
            StudentCourse studentCourse = new StudentCourse();
            studentCourse.CourseId = studentCourseDTo.CourseId;
            studentCourse.UserId = studentCourseDTo.UserId;
            c.StudentCourses.Add(studentCourse);
            c.SaveChanges();
            return Redirect("StudentAssignedCourse");
        }


    }
}
