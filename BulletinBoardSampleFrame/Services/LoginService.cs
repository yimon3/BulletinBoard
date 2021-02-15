using BulletinBoardSampleFrame.DAOs;
using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.ViewModel.Login;

namespace BulletinBoardSampleFrame.Services
{
    /// <summary>
    /// Service Class
    /// Contains all method validing and connecting the Controller and DAO
    /// </summary>
    public class LoginService
    {
        #region member variables
        LoginDAO loginDAO = new LoginDAO();
        #endregion

        #region public methods
        public user LoginForm(LoginModel model)
        {
            var obj = loginDAO.LoginForm(model);
            return obj;
        }

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

        /// <summary>
        /// This is to change password when user input wrong data
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public user ForgotPassword(string email)
        {
            var data = loginDAO.ForgotPassword(email);
            return data;
        }
        #endregion
    }
}
