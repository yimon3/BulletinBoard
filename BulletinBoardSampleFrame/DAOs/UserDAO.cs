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
        public IEnumerable<UserViewModel> GetUser()
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
        /// This is to get user list from database by using keyword
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IEnumerable<UserViewModel> GetUsersByKeyword(string name, string email)
        {
            var userList = (from user in db.users 
                            join post in db.posts
                            on user.id equals post.create_user_id
                            where user.name.ToUpper().Contains(name.ToUpper()) ||
                            user.email.ToUpper().Contains(email.ToUpper())
                            select new
                            {
                                name = user.name,
                                email = user.email,
                                createdUser = post.user1.name,
                                phone = user.phone,
                                birthday = user.dob,
                                address = user.address,
                                created_at = user.created_at,
                                updated_at = user.updated_at
                            }).ToList()
                          .Select(userView => new UserViewModel()
                          {
                              name = userView.name,
                              email = userView.email,
                              createdUser = userView.name,
                              phone = userView.phone,
                              dob = userView.birthday,
                              address = userView.address,
                              created_at = userView.created_at,
                              updated_at = userView.updated_at
                          });
            return userList;
        }

        /// <summary>
        ///  This is to get user list from database.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IEnumerable<UserViewModel> GetUserList(string name, string email)
        {
            if (name == null || name.Equals(""))
            {
                return GetUser();
            }
            if (email == null || email.Equals(""))
            {
                return GetUser();
            }
            return GetUsersByKeyword(name, email);
        }

        /// <summary>
        /// This is to save new user to database
        /// </summary>
        /// <param name="newUser"></param>
        public user SaveUser(user newUser)
        {
            var exist = db.users.Where(w => w.email == newUser.email).FirstOrDefault();
            if (exist != null)
            {
            }
            else
            {
                db.users.Add(newUser);
                db.SaveChanges();
            }
            return exist;
        }

        /// <summary>
        /// This is edit user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public user EditUser(int id)
        {
            var data = db.users.Where(w => w.id == id).FirstOrDefault();

            return data;
        }

        /// <summary>
        /// This is edit user data into database
        /// </summary>
        /// <param name="userView"></param>
        public void EditConfirmUser(UserViewModel userView)
        {
            var userData = db.users.Where(u => u.id == userView.id).FirstOrDefault();
            if (userData != null)
            {
                userData.name = userView.name;
                userData.email = userView.email;
                if (userView.type == "admin")
                {
                    userData.type = "0";
                }
                else if (userView.type == "user")
                {
                    userData.type = "1";
                }
                else
                {
                    userData.type = "2";
                }
                userData.phone = userView.phone;
                userData.dob = userView.dob;
                userData.address = userView.address;
                userData.profile = userView.profile;
                userData.updated_user_id = userView.id;
                userData.updated_at = DateTime.Now;
                db.SaveChanges();
            }
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