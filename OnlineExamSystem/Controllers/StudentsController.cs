using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
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
        IUserService _studentService;
        Context c = new Context();


        public StudentsController(IUserService productService)
        {
            _studentService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
       
        [HttpGet]
        [Authorize(Policy = "Ogretmen")]
        public IActionResult StudentList(User Student)
        {
            List<User> list = c.Users.Where(x => x.IsStudent == true).ToList();
           
            return View(list);
        }
        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "Ogretmen")]
        public IActionResult AddStudent(User User)
        {
            _studentService.Add(User);
            return RedirectToAction("StudentList");
        }
        [HttpPost("delete")]
        [Authorize(Policy = "Ogretmen")]
        public IActionResult Delete(User student)
        {
            _studentService.Delete(student);       
            return RedirectToAction("Index");
        }
    }
}
