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
            var userList = userService.getUserList();
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
        public ActionResult Edit()
        {
            return View();
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