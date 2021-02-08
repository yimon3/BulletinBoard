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
using BulletinBoardSampleFrame.Utility;

namespace BulletinBoardSampleFrame.Controllers
{
    /// <summary>
    /// This is login controller
    /// </summary>
    public class LoginController : Controller
    {
        LoginService loginService = new LoginService();
        EncryptDecryptPassword endePassword = new EncryptDecryptPassword();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //Get : login
        [HttpGet]
        [Authorize]
        public ActionResult Login()
        {
            if (!string.IsNullOrEmpty(Request.Cookies["Email"].Value) && !string.IsNullOrEmpty(Request.Cookies["Password"].Value))
            {
                var login = new LoginModel
                {
                    Email = Request.Cookies["Email"].Value,
                    Password = Request.Cookies["Password"].Value
                };
                return View(login);
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// This is to validate user information and login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            model.Password = endePassword.Encrypt(model.Password);
            var obj = loginService.Login(model);
            if (obj != null)
            {
                FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
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

                if (model.RememberMe)
                {
                    obj.password = endePassword.Decrypt(model.Password);
                    Response.Cookies["Email"].Value = obj.email.ToString();
                    Response.Cookies["Password"].Value = obj.password.ToString();
                    Response.Cookies["Email"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    Response.Cookies["Email"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                }
            }
            else if (obj == null)
            {
                return RedirectToAction("ForgotPassword", "Login");
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
        /// Clear input for forgot password page
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearForgotPsw()
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }
            return View("ForgotPassword");
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

        /// <summary>
        /// Get: Forgot Password
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// Post: Forgot Password
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ForgotPassword(LoginModel login)
        {
            var data = loginService.ChangePassword(login);
            if (data != null)
            {
                if (login.NewPassword != login.ConfirmPassword)
                {
                    ViewData["Message"] = "New Password and Confirm password must same.";
                    return View(login);
                }
            }
            else
            {
                ViewData["Message"] = "Wrong Old Password";
                return View(login);
            }
            return RedirectToAction("Login", "Login");
        }
    }
}

