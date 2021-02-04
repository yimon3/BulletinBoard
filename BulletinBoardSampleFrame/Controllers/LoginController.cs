using BulletinBoardSampleFrame.Services;
using BulletinBoardSampleFrame.Properties;
using BulletinBoardSampleFrame.ViewModel.Login;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace BulletinBoardSampleFrame.Controllers
{
    /// <summary>
    /// This is login controller
    /// </summary>
    public class LoginController : Controller
    {
        LoginService loginService = new LoginService();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //Get : login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// This is to validate user information and login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var obj = loginService.Login(model);
            
            if (obj != null)
            {
                FormsAuthentication.SetAuthCookie(model.Email,model.RememberMe);
                Session["Id"] = obj.id;
                Session["Name"] = obj.name.ToString();
                Session["Email"] = obj.email.ToString();
                Session["Password"] = obj.password.ToString();
                if (obj.type == "0")
                {
                    Session["Type"] = "Admin";
                }
                else if (obj.type == "1")
                {
                    Session["Type"] = "User";
                }
                else
                {
                    Session["Type"] = "Visitor";
                }
                Session["Phone"] = obj.phone.ToString();
                Session["DOB"] = obj.dob;
                Session["Address"] = obj.address.ToString();
                Session["Profile"] = obj.profile.ToString();
            }
            else
            { 
                ViewData["Message"] = "Email or Password is incorrect";
                return View(model);
            }
            return RedirectToAction("PostViewDefault", "Post");
        }

        /// <summary>
        /// log out from system
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }

        public ActionResult ClearInput()
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }
            return View("ChangePassword");
        }

        /// <summary>
        /// Get : ChangePassword
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// This is to change password 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangePassword(LoginModel login)
        {
            var data = loginService.ChangePassword(login);
            if (data != null)
            {
                if (login.NewPassword != login.ConfirmPassword)
                {
                    ViewData["Message"] = "New Password and Confirm password must same.";
                    return View(login);
                }
                else if (Session["Type"].ToString() == "Visitor")
                {
                    return RedirectToAction("PostViewForVisitor", "Post");
                }
            }
            else
            {
                ViewData["Message"] = "Wrong Old Password";
                return View(login);
            }
            return RedirectToAction("PostView", "Post");
        }
    }
}
