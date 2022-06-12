using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace OnlineExamSystem.Controllers
{
    public class CourseController : Controller
    {
        Context c = new Context();
        CourseManager courseManager = new CourseManager(new EfCourseDal());

        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
         
            var result = from ur in c.StudentCourses
                         join r in c.Course
                         on ur.CourseId equals r.CourseId
                         where ur.UserId == Convert.ToInt32(userId)
                         select new Course { 
                            CourseId = ur.CourseId,
                            UserId = Convert.ToInt32(userId),
                            Title=r.Title,
                            AddedAt=r.AddedAt,
                            check=r.check
                         };
          
            return View(result.ToList());
        }

       

    }
}
