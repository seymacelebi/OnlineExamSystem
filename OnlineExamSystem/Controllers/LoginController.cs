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
        //public async Task<IActionResult> Login(Login user)
        //{
        //    var Login = idorContext.Users.FirstOrDefault(x => x.Email == user.Email && x.password == user.password);
        //    if (Login != null)
        //    {
        //        var claims = new List<Claim> { new Claim(ClaimTypes.Name, Login.Email), new Claim(ClaimTypes.NameIdentifier, Login.Id.ToString()) };
        //        var userIdentity = new ClaimsIdentity(claims, "login");
        //        ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity); await HttpContext.SignInAsync(principal);
        //        return Redirect("/Address/Index");
        //    }
        //    return View();
        //}
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
                    new Claim(ClaimTypes.Name,datavalue.Email),
                    new Claim(ClaimTypes.NameIdentifier,datavalue.UserId.ToString()),
                   
                };
           
                var useridentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                if (!datavalue.IsStudent)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                    return RedirectToAction("AddQuestions", "Exams");

                }

                return RedirectToAction("GetList", "Exams");
            }
            else
            {

                return View();
            }
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            
            return RedirectToAction("Index");
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
