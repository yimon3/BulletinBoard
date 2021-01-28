using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.Services;
using BulletinBoardSampleFrame.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BulletinBoardSampleFrame.Controllers
{
    public class UserController : Controller
    {
        UserService userService = new UserService();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public new ActionResult User()
        {
            return View();
        }

        /// <summary>
        /// This is to get user list
        /// </summary>
        /// <returns></returns>
        public ActionResult UserList()
        {
            var userList = userService.showUser();
            return View("UserList", userList);
        }

        /// <summary>
        /// This is get user list by keyword
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult Search(string name, string email)
        {
            var userList = userService.getUserList(name, email);
            return View("UserList", userList);
        }

        /// <summary>
        /// This is to confirm for new user added to database
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ConfirmUser(UserViewModel userViewModel)
        {
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
            newUser.name = userData.name;
            newUser.email = userData.email;
            userData.createdUser = (string)Session["Name"];
            newUser.password = userData.password;
            if (userData.type == "admin")
            {
                newUser.type = "0";
            }
            else if(userData.type == "user")
            {
                newUser.type = "1";
            }
            else
            {
                newUser.type = "2";
            }
            newUser.phone = userData.phone;
            newUser.dob = userData.dob;
            newUser.address = userData.address;
            newUser.profile = userData.profile;
            newUser.create_user_id = (int)Session["Id"];
            newUser.updated_user_id = (int)Session["Id"];
            newUser.created_at = DateTime.Now;
            newUser.updated_at = DateTime.Now;

            var exist = userService.SaveUser(newUser);
            if (exist != null)
            {
                ViewData["Message"] = "User with email already exist.";
                return View("User", userData);
            }

            userService.SaveUser(newUser);

            return RedirectToAction("UserList",userData);
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
            userView.name = data.name;
            userView.email = data.email;
            userView.type = data.type;
            userView.phone = data.phone;
            userView.dob = data.dob;
            userView.address = data.address;
            userView.profile = data.profile;

            return View("Edit", userView);
        }

        /// <summary>
        /// This is to return edit confirm view
        /// </summary>
        /// <param name="userView"></param>
        /// <returns></returns>
        public ActionResult EditConfirm(UserViewModel userView)
        {
            return View("EditConfirm", userView);
        }

        /// <summary>
        /// This is to edit user data into database
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
    }
}