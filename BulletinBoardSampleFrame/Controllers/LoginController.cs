using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.Services;
using BulletinBoardSampleFrame.ViewModel.Login;
using BulletinBoardSampleFrame.ViewModel.Post;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace BulletinBoardSampleFrame.Controllers
{
    public class LoginController : Controller
    {
        private BulletinBoardEntity db = new BulletinBoardEntity();
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var obj = db.users.Where(a => a.email.Equals(model.email) && a.password.Equals(model.password)).FirstOrDefault();
            if (obj != null)
            {
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
            if (Session["Type"].ToString() == "Admin" || Session["Type"].ToString() == "User")
            {
                return RedirectToAction("PostView", "Post");
            }
            if (Session["Type"].ToString() == "Visitor")
            {
                return RedirectToAction("PostViewForVisitor", "Post");
            }
            else
            {
                ViewData["Message"] = "Email or Password is incorrect";
            }
            return View(obj);
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
    }
}