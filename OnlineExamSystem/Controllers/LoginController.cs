using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineExamSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace WebUI.Controllers
{
  
    public class LoginController : Controller
    {
        UserManager userManager = new UserManager(new EfUserDal());
        //readonly UserManager<AppUser> _userManager;
        //readonly SignInManager<AppUser> _signInManager;
        //public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //}
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
                if (!datavalue.IsStudent && datavalue.IsAdmin)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                    var x = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal y = new ClaimsPrincipal(x);
                    await HttpContext.SignInAsync(y);
                    return Redirect("/Exams/Index");
                }
    
                if (!datavalue.IsStudent)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Ogretmen"));
                    var a = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal b = new ClaimsPrincipal(a);
                    await HttpContext.SignInAsync(b);
                    return Redirect("/Exams/Index");

                }

                var useridentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Exams");
            }
            else
            {

                return View();
            }
        }
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        //public IActionResult PasswordReset()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> PasswordReset(ResetPasswordViewModel model)
        //{
        //    AppUser user = await _userManager.FindByEmailAsync(model.Email);
        //    if (user != null)
        //    {
        //        string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        //        MailMessage mail = new MailMessage();
        //        mail.IsBodyHtml = true;
        //        mail.To.Add(user.Email);
        //        mail.From = new MailAddress("******@gmail.com", "Şifre Güncelleme", System.Text.Encoding.UTF8);
        //        mail.Subject = "Şifre Güncelleme Talebi";
        //        mail.Body = $"<a target=\"_blank\" href=\"https://localhost:5001{Url.Action("UpdatePassword", "User", new { userId = user.Id, token = HttpUtility.UrlEncode(resetToken) })}\">Yeni şifre talebi için tıklayınız</a>";
        //        mail.IsBodyHtml = true;
        //        SmtpClient smp = new SmtpClient();
        //        smp.Credentials = new NetworkCredential("*****@gmail.com", "******");
        //        smp.Port = 587;
        //        smp.Host = "smtp.gmail.com";
        //        smp.EnableSsl = true;
        //        smp.Send(mail);

        //        ViewBag.State = true;
        //    }
        //    else
        //        ViewBag.State = false;

        //    return View();
        //}

        //[HttpGet("[action]/{userId}/{token}")]
        //public IActionResult UpdatePassword(string userId, string token)
        //{
        //    return View();
        //}
        //[HttpPost("[action]/{userId}/{token}")]
        //public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel model, string userId, string token)
        //{
        //    AppUser user = await _userManager.FindByIdAsync(userId);
        //    IdentityResult result = await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(token), model.Password);
        //    if (result.Succeeded)
        //    {
        //        ViewBag.State = true;
        //        await _userManager.UpdateSecurityStampAsync(user);
        //    }
        //    else
        //        ViewBag.State = false;
        //    return View();
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> ForgotPassword([Required] string email)
        //{
        //    if (!ModelState.IsValid)
        //        return View(email);

        //    var user = await userManager.FindByEmailAsync(email);
        //    if (user == null)
        //        return RedirectToAction(nameof(ForgotPasswordConfirmation));

        //    var token = await userManager.GeneratePasswordResetTokenAsync(user);
        //    var link = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);

        //    EmailHelper emailHelper = new EmailHelper();
        //    bool emailResponse = emailHelper.SendEmailPasswordReset(user.Email, link);

        //    if (emailResponse)
        //        return RedirectToAction("ForgotPasswordConfirmation");
        //    else
        //    {
        //        // log email failed 
        //    }
        //    return View(email);
        //}

        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
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
