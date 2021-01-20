using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulletinBoardSampleFrame.DAO
{
    public class UserDAO
    {
        BulletinBoardEntity db = new BulletinBoardEntity();

        /// <summary>
        /// This is to save new user to database
        /// </summary>
        /// <param name="newUser"></param>
        public void SaveUser(user newUser)
        {
            db.users.Add(newUser);
            db.SaveChanges();
        }
    }
}