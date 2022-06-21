using Microsoft.AspNetCore.Mvc;

namespace OnlineExamSystem.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
