using BulletinBoardSampleFrame.DAO;
using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulletinBoardSampleFrame.Services
{
    public class UserService
    {
        UserDAO userDAO = new UserDAO();

        /// <summary>
        /// This is to get user list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserViewModel> showUser()
        {
            var user = userDAO.getUser();
            return user;
        }

        /// <summary>
        /// This is to get user list by keyword
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserViewModel> getUserList(string search, string name)
        {
            var userList = userDAO.getUserList(search, name);
            return userList;
        }

        /// <summary>
        /// This is to add new user to database
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public user SaveUser(user userData)
        {
            return userDAO.SaveUser(userData);
        }

        /// <summary>
        /// This is to edit user for return
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public user EditUser(int id)
        {
            return userDAO.EditUser(id);
        }

        /// <summary>
        /// This is to edit user data into database
        /// </summary>
        /// <param name="userView"></param>
        public void EditConfirmUser(UserViewModel userView)
        {
            userDAO.EditConfirmUser(userView);
        }

        /// <summary>
        /// This is to delete user
        /// </summary>
        /// <param name="userId"></param>
        public void DeleteUser(int userId)
        {
            userDAO.DeleteUser(userId);
        }
    }
}