using BulletinBoardSampleFrame.Services;
using BulletinBoardSampleFrame.Utility;
using BulletinBoardSampleFrame.ViewModel.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using TweetSharp;
namespace BulletinBoardSampleFrame.Controllers
{
    /// <summary>
    /// This is login controller
    /// </summary>
    public class LoginController : Controller
    {
        #region membervariable
        LoginService loginService = new LoginService();
        EncryptDecryptPassword endePassword = new EncryptDecryptPassword();
        #endregion

        #region public action methods
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //Get : login
        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                LoginModel login = new LoginModel();
                if (!String.IsNullOrEmpty(Request.Cookies["Email"].Value) && !String.IsNullOrEmpty(Request.Cookies["Password"].Value))
                {
                    login.Email = Request.Cookies["Email"].Value;
                    login.Password = Request.Cookies["Password"].Value;
                }
                return View(login);
            }
            catch (NullReferenceException)
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
        public ActionResult Validate(LoginModel model)
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
                    Response.Cookies["Email"].Expires = DateTime.Now.AddMinutes(10);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddMinutes(10);
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

        /// <summary>
        /// This is for clear input data
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearInput()
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

        /// <summary>
        /// This is for authentication of twitter account
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TwitterAuth()
        {
            string Key = "P3xleqTzSi2myNSRgC7nkABSi";
            string Secret = "Bt8resPY8UIP6AafYRgMMynmRyy1HKlLoXp63c1HegtXZVS9eH";

            TwitterService service = new TwitterService(Key, Secret);

            OAuthRequestToken requestToken = service.GetRequestToken("https://localhost:44323/Login/TwitterCallBack");

            Uri uri = service.GetAuthenticationUrl(requestToken);

            return Redirect(uri.ToString());
        }

        /// <summary>
        /// This is to retrieve user data from twitter account
        /// </summary>
        /// <param name="oauth_token"></param>
        /// <param name="oauth_verifier"></param>
        /// <returns></returns>
        public ActionResult TwitterCallback(string oauth_token, string oauth_verifier)
        {
            List<TwitterViewModel> lstTwitterModels = new List<TwitterViewModel>();
            var requestToken = new OAuthRequestToken { Token = oauth_token };

            string Key = "P3xleqTzSi2myNSRgC7nkABSi";
            string Secret = "Bt8resPY8UIP6AafYRgMMynmRyy1HKlLoXp63c1HegtXZVS9eH";

            try
            {
                TwitterService service = new TwitterService(Key, Secret);

                OAuthAccessToken accessToken = service.GetAccessToken(requestToken, oauth_verifier);

                service.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);

                var currentTweets = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions
                {
                    ScreenName = "Katieeee59",
                    Count = 200,
                }).ToList();

                foreach (var tweet in currentTweets)
                {
                    TempData["Name"] = tweet.User.Name;
                    TwitterViewModel twitterView = new TwitterViewModel();
                    twitterView.Name = tweet.User.Name;
                    twitterView.CreatedDate = tweet.CreatedDate;
                    twitterView.Status = tweet.Text;

                    lstTwitterModels.Add(twitterView);
                }
                return View(lstTwitterModels);
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}






