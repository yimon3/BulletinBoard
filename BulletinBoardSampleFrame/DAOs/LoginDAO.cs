using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.ViewModel.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulletinBoardSampleFrame.DAOs
{
    /// <summary>
    /// DAO (Data Access Class)
    ///  contains all method that connected to the database
    /// </summary>
    public class LoginDAO
    {
        BulletinBoardEntity db = new BulletinBoardEntity();

        /// <summary>
        /// This is login 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public user Login(LoginModel model)
        {
            var obj = db.users.Where(a => a.email.Equals(model.email) && a.password.Equals(model.password)).FirstOrDefault();
            return obj;
        }

        /// <summary>
        /// This is to change password
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public user ChangePassword(LoginModel login)
        {
            var data = db.users.Where(u => u.password == login.password).FirstOrDefault();
            if (data != null)
            {
                if (login.newPassword == login.confirmPassword)
                {
                    data.password = login.confirmPassword;
                    db.SaveChanges();
                }
            }
            return data;
        }
    }
}