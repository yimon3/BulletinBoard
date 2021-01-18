using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BulletinBoardSampleFrame.CommonValidation
{
    /// <summary>
    ///     This class is for checking validation.
    /// </summary>
    public class PostValidation
    {
        /// <summary>
        ///     This is to check the password regularexpression.
        /// </summary>
        public bool CheckPasswordValidation(string password, string newPassword, string confirmPassword)
        {
            string passwordRex = "^(?=.+[A-Za-z])(?=.*[0-9])[A-Za-z0-9]{8,}$";
            Regex regular = new Regex(passwordRex);
            if (!regular.IsMatch(password) || !regular.IsMatch(newPassword) || !regular.IsMatch(confirmPassword))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}