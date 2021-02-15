using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.Properties;
using BulletinBoardSampleFrame.Services;
using BulletinBoardSampleFrame.Utility;
using BulletinBoardSampleFrame.ViewModel.User;
using PagedList;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace BulletinBoardSampleFrame.Controllers
{
    /// <summary>
    /// This is user controller
    /// </summary>
    public class UserController : Controller
    {
        #region Variables
        UserService userService = new UserService();
        EncryptDecryptPassword endePassword = new EncryptDecryptPassword();
        #endregion

        #region public Action method
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // Get : User
        public new ActionResult User()
        {
            return View();
        }

        /// <summary>
        /// This is clear input values
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearInput()
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }
            return View("User");
        }

        /// <summary>
        /// This is clear edit input value
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearEditInput()
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }
            return View("Edit");
        }

        /// <summary>
        /// This is to get user list
        /// </summary>
        /// <returns></returns>
        public ActionResult UserList(int? page)
        {
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            var userList = userService.showUser().ToPagedList(pageIndex, CommonConstant.pageSize);
            return View("UserList", userList);
        }

        /// <summary>
        /// This is get user list by keyword
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult Search(int? page, string name, string email, DateTime? createdTo, DateTime? createdFrom)
        {
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            var userList = userService.getUserList(name, email, createdTo, createdFrom).ToPagedList(pageIndex, CommonConstant.pageSize);
            return View("UserList", userList);
        }

        /// <summary>
        /// This is to confirm for new user added to database
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ConfirmUser(UserViewModel userViewModel, HttpPostedFileBase file)
        {
            file = Request.Files[0];
            if (file != null && file.ContentLength > 0)
            {
                var imageName = Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("~/Resources/lib/images/" + imageName);
                file.SaveAs(physicalPath);
                userViewModel.Profile = imageName;
            }
            return View("ConfirmUser", userViewModel);
        }

        /// <summary>
        /// This is to save new user
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(UserViewModel userData)
        {
            user newUser = new user();

            newUser.name = userData.Name;
            newUser.email = userData.Email;
            newUser.password = endePassword.Encrypt(userData.Password);
            newUser.profile = userData.Profile;

            if (userData.Type == "admin")
            {
                newUser.type = "0";
            }
            else if (userData.Type == "user")
            {
                newUser.type = "1";
            }
            else
            {
                newUser.type = "2";
            }
            newUser.phone = userData.Phone;
            newUser.dob = userData.Dob;
            newUser.address = userData.Address;
            newUser.create_user_id = (int)Session["Id"];
            newUser.updated_user_id = (int)Session["Id"];
            userData.CreatedUser = (string)Session["Name"];
            newUser.created_at = DateTime.Now;
            newUser.updated_at = DateTime.Now;

            var exist = userService.SaveUser(newUser);
            if (exist != null)
            {
                ViewData["Message"] = "User with email already exist.";
                return View("User", userData);
            }

            return RedirectToAction("UserList", userData);
        }

        /// <summary>
        /// This is to return user profile
        /// </summary>
        /// <param name="userView"></param>
        /// <returns></returns>
        public ActionResult UserProfile(UserViewModel userView)
        {
            return View(userView);
        }

        /// <summary>
        /// This is to return edit user view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id, UserViewModel userView)
        {
            var data = userService.EditUser(id);
            userView.Name = data.name;
            userView.Email = data.email;
            userView.Type = data.type;
            userView.Phone = data.phone;
            userView.Dob = data.dob;
            userView.Address = data.address;
            userView.Profile = data.profile;

            return View("Edit", userView);
        }

        /// <summary>
        /// This is edit confirm for user
        /// </summary>
        /// <param name="userView"></param>
        /// <returns></returns>
        public ActionResult EditConfirm(UserViewModel userView)
        {
            return View("EditConfirm", userView);
        }

        /// <summary>
        /// This is for confirm edit user into database
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ConfirmEditUser(UserViewModel viewModel)
        {
            userService.EditConfirmUser(viewModel);
            return RedirectToAction("UserList", viewModel);
        }

        /// <summary>
        /// This is for delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            userService.DeleteUser(id);
            return RedirectToAction("UserList");
        }
        #endregion
    }
}
