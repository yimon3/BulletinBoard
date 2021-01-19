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

        public void SaveUser(user newUser)
        {
            db.users.Add(newUser);
            db.SaveChanges();
        }
    }
}