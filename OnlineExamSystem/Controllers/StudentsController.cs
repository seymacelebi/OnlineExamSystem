using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class StudentsController : Controller
    {
        //IStudentService studentManager = new StudentManager(new EfStudentDal());
        IStudentService _studentService;

        public StudentsController(IStudentService productService)
        {
            _studentService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
       
        [HttpGet]
        public IActionResult GetList()
        {
            var result = _studentService.GetList();
            return View(result);
        }
        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            _studentService.Add(student);
            return RedirectToAction("Index");
        }
        //[HttpPost("delete")]
        //public IActionResult Delete(Student student)
        //{
        //    var result = _studentService.Delete(student);
        //    if (result.Success)
        //    {
        //        return Ok(result.Message);
        //    }

        //    return BadRequest(result.Message);
        //    return RedirectToAction("Index");
        //}
    }
}
