using BulletinBoardSampleFrame.DAOs;
using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.ViewModel.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulletinBoardSampleFrame.Services
{
    /// <summary>
    ///   Service Class
    ///     Contains all method validing and connecting the Controller and DAO
    /// </summary>
    public class LoginService
    {
        LoginDAO loginDAO = new LoginDAO();

        /// <summary>
        /// This is login 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public user Login(LoginModel login)
        {
            var obj = loginDAO.Login(login);
            return obj;
        }

        /// <summary>
        /// This is to change password
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public user ChangePassword(LoginModel login)
        {
            var data = loginDAO.ChangePassword(login);
            return data;
        }
    }
}