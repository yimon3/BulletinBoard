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
        /// This is to get user list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserViewModel> getUserList()
        {
            var userList = (from user in db.users
                            select new
                            {
                                id = user.id,
                                name = user.name,
                                email = user.email,
                                type = user.type,
                                createdUser = user.name,
                                phone = user.phone,
                                dob = user.dob,
                                address = user.address,
                                created_at = user.created_at,
                                updated_at = user.updated_at
                            }).ToList()
                          .Select(userView => new UserViewModel()
                          {
                              id = userView.id,
                              name = userView.name,
                              email = userView.email,
                              type = userView.type,
                              createdUser = userView.name,
                              phone = userView.phone,
                              dob = userView.dob,
                              address = userView.address,
                              created_at = userView.created_at,
                              updated_at = userView.updated_at
                          });
            return userList;
        }

        /// <summary>
        /// This is to save new user to database
        /// </summary>
        /// <param name="newUser"></param>
        public void SaveUser(user newUser)
        {
            db.users.Add(newUser);
            db.SaveChanges();
        }

        /// <summary>
        /// This is to delete user
        /// </summary>
        /// <param name="userId"></param>
        public void DeleteUser(int userId)
        {
            var data = (from item in db.users

                        where item.id == userId

                        select item).SingleOrDefault();

            data.deleted_at = DateTime.Now;
            data.deleted_user_id = data.create_user_id;

            db.users.Remove(data);
            db.SaveChanges();
        }
    }
}