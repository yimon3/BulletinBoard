using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BulletinBoardSampleFrame.DAO
{
    /// <summary>
    ///  DAO (Data Access Class)
    ///  contains all method that connected to the database
    /// </summary>
    /// 
    public class UserDAO
    {
        #region member variables
        BulletinBoardEntities5 db = new BulletinBoardEntities5();
        #endregion

        #region public methods
        /// <summary>
        /// This is to get user list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserViewModel> getUser()
        {
            var userList = (from user in db.users
                            select new
                            {
                                Id = user.id,
                                Name = user.name,
                                Email = user.email,
                                Type = user.type,
                                CreatedUser = user.name,
                                Phone = user.phone,
                                Dob = user.dob,
                                Address = user.address,
                                Created_at = user.created_at,
                                Updated_at = user.updated_at
                            }).ToList()
                          .Select(userView => new UserViewModel()
                          {
                              Id = userView.Id,
                              Name = userView.Name,
                              Email = userView.Email,
                              Type = userView.Type,
                              CreatedUser = userView.Name,
                              Phone = userView.Phone,
                              Dob = userView.Dob,
                              Address = userView.Address,
                              Created_at = userView.Created_at,
                              Updated_at = userView.Updated_at
                          });
            return userList;
        }

        /// <summary>
        /// This is to get user list from database by using keyword
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IEnumerable<UserViewModel> getUsersByKeyword(string name, string email, DateTime? createdTo, DateTime? createdFrom)
        {
            var userList = (from user in db.users
                            where user.name.ToUpper().Contains(name.ToUpper()) ||
                            user.email.ToUpper().Contains(email.ToUpper()) ||
                            user.created_at >= createdTo ||
                            user.created_at <= createdFrom
                            select new
                            {
                                Name = user.name,
                                Email = user.email,
                                CreatedUser = user.name,
                                Phone = user.phone,
                                Birthday = user.dob,
                                Address = user.address,
                                Created_at = user.created_at,
                                Updated_at = user.updated_at
                            }).ToList()
                          .Select(userView => new UserViewModel()
                          {
                              Name = userView.Name,
                              Email = userView.Email,
                              CreatedUser = userView.Name,
                              Phone = userView.Phone,
                              Dob = userView.Birthday,
                              Address = userView.Address,
                              Created_at = userView.Created_at,
                              Updated_at = userView.Updated_at
                          });
            return userList;
        }

        /// <summary>
        ///  This is to get user list from database.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IEnumerable<UserViewModel> getUserList(string name, string email, DateTime? createdTo, DateTime? createdFrom)
        {
            if (name == null || name.Equals(""))
            {
                return getUser();
            }
            if (email == null || email.Equals(""))
            {
                return getUser();
            }
            return getUsersByKeyword(name, email, createdTo, createdFrom);
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
        /// This is to delete user
        /// </summary>
        /// <param name="userId"></param>
        public void DeleteUser(int userId)
        {
            var data = (from item in db.users

                        where item.id == userId

                        select item).SingleOrDefault();

            db.users.Remove(data);
            db.SaveChanges();
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
            var userData = db.users.Where(u => u.id == userView.Id).FirstOrDefault();
            if (userData != null)
            {
                userData.name = userView.Name;
                userData.email = userView.Email;
                if (userView.Type == "admin")
                {
                    userData.type = "0";
                }
                else if (userView.Type == "user")
                {
                    userData.type = "1";
                }
                else
                {
                    userData.type = "2";
                }
                userData.phone = userView.Phone;
                userData.dob = userView.Dob;
                userData.address = userView.Address;
                userData.profile = userView.Profile;
                userData.updated_user_id = userView.Id;
                userData.updated_at = DateTime.Now;
                db.SaveChanges();
            }
        }
        #endregion
    }
}
