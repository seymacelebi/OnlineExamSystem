using Business.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace OnlineExamSystem.Controllers
{
    public class UserController : Controller
    {
        IUserService _userService;
        Context c = new Context();
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult UserList(User user)
        {
            List<User> list = c.Users.Where(x => x.IsStudent == true || x.IsStudent == false && x.IsAdmin == false).ToList();

            return View(list);
        }
    }
}
