using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.Services;
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
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(user objUser)
        {
            if (ModelState.IsValid)
            {
                 user obj = db.users.Where(a => a.email.Equals(objUser.email) && a.password.Equals(objUser.password)).FirstOrDefault();
                 if (obj != null)
                 {
                     Session["Email"] = obj.email.ToString();
                     Session["Password"] = obj.password.ToString();
                     return RedirectToAction("Index");
                 }
            }
            return View(objUser);
        }
    }
}