using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.ViewModel.Login;
using System;
using System.Linq;

namespace BulletinBoardSampleFrame.DAOs
{
    /// <summary>
    /// DAO (Data Access Class)
    ///  contains all method that connected to the database
    /// </summary>
    public class LoginDAO
    {
        BulletinBoardEntities5 db = new BulletinBoardEntities5();

        /// <summary>
        /// This is login 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public user Login(LoginModel model)
        {
            var obj = db.users.Where(a => a.email.Equals(model.Email) && a.password.Equals(model.Password)).FirstOrDefault();
            return obj;
        }

        /// <summary>
        /// This is to change password
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public user ChangePassword(LoginModel login)
        {
            var data = db.users.Where(u => u.password == login.Password).FirstOrDefault();
            if (data != null)
            {
                if (login.NewPassword == login.ConfirmPassword)
                {
                    data.password = login.ConfirmPassword;
                    data.updated_at = DateTime.Now;
                    db.SaveChanges();
                }
            }
            return data;
        }
    }
}
