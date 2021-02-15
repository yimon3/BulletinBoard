using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.Utility;
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
        #region memberVariables
        BulletinBoardEntities5 db = new BulletinBoardEntities5();
        EncryptDecryptPassword endePassword = new EncryptDecryptPassword();
        #endregion

        #region public methods
        /// <summary>
        /// Login Form
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public user LoginForm(LoginModel model)
        {
            var obj = db.users.Where(w => w.email.Equals(model.Email)).FirstOrDefault();
            return obj;
        }

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
            login.Password = endePassword.Encrypt(login.Password);
            var data = db.users.Where(u => u.password == login.Password).FirstOrDefault();
            if (data != null)
            {
                if (login.NewPassword == login.ConfirmPassword)
                {
                    login.NewPassword = endePassword.Encrypt(login.NewPassword);
                    login.ConfirmPassword = endePassword.Encrypt(login.ConfirmPassword);
                    data.password = login.ConfirmPassword;
                    data.updated_at = DateTime.Now;
                    db.SaveChanges();
                }
            }
            return data;
        }

        /// <summary>
        /// This is for forgot password
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public user ForgotPassword(string email)
        {
            var account = db.users.Where(a => a.email == email).FirstOrDefault();
            return account;
        }
        #endregion
    }
}
