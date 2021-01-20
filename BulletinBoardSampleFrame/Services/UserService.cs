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
        /// This is to save user to database
        /// </summary>
        /// <param name="userData"></param>
        public void SaveUser(user userData)
        {
            userDAO.SaveUser(userData);
        }
    }
}