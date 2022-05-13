﻿using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class LoginController : Controller
    {
        UserManager userManager = new UserManager(new EfUserDal());
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Index(User p)
        {
            Context c = new Context();
            var datavalue = c.Users.FirstOrDefault(x => x.Email == p.Email && x.Password == p.Password);
            if (datavalue != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, p.Email)
                };
                var useridentity = new ClaimsIdentity(claims, "a");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                //HttpContext.Session.SetString("username", p.Email);
                return RedirectToAction("AddStudent", "Students");
            }
            else
            {
                return View();
            }
        }
        //[HttpGet]
        //public ActionResult AdminIndex()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult AdminIndex(Admin p)
        //{
        //    Context c = new Context();
        //    var adminuserinfo = c.Admins.FirstOrDefault(x => x.AdminUserName == p.AdminUserName && x.AdminPassword == p.AdminPassword);
        //    if (adminuserinfo != null)
        //    {
        //        FormsAuthentication.SetAuthCookie(adminuserinfo.AdminUserName, false);
           //    Session["AdminUserName"] = adminuserinfo.AdminUserName;
        //        return RedirectToAction("Index", "AdminCategory");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}
    }
}
