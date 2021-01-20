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
        public IEnumerable<UserViewModel> getUserList()
        {
            var userList = userDAO.getUserList();
            return userList;
        }

        /// <summary>
        /// This is to save user to database
        /// </summary>
        /// <param name="userData"></param>
        public void SaveUser(user userData)
        {
            userDAO.SaveUser(userData);
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