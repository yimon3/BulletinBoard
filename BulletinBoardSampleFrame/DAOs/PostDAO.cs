using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.Properties;
using BulletinBoardSampleFrame.ViewModel.Post;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BulletinBoardSampleFrame.DAO
{
    /// <summary>
    ///  DAO (Data Access Class)
    ///  contains all method that connected to the database
    /// </summary>
    /// 
    public class PostDAO
    {
        #region member variables
        BulletinBoardEntities5 db = new BulletinBoardEntities5();
        #endregion

        #region public methods
        /// <summary>
        /// This is to get all post and created user name from database for admin and visitor.
        /// </summary>
        public IEnumerable<PostViewModel> GetPosts()
        {
            var postList = (from post in db.posts
                            join user in db.users
                            on post.create_user_id equals user.id
                            where post.status.ToString().Contains(CommonConstant.status_active.ToString())
                            select new
                            {
                                Id = post.id,
                                Title = post.title,
                                Description = post.description,
                                CreatedUser = user.name,
                                Created_at = post.created_at,

                            }).ToList()
                          .Select(postView => new PostViewModel()
                          {
                              Id = postView.Id,
                              Title = postView.Title,
                              Description = postView.Description,
                              CreatedUser = postView.CreatedUser,
                              Created_at = postView.Created_at
                          });
            return postList;
        }

        /// <summary>
        /// This is to get post list for users
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<PostViewModel> GetUserPosts(int id)
        {
            var postList = (from post in db.posts
                            join user in db.users
                            on post.create_user_id equals user.id
                            where post.create_user_id.ToString().Contains(id.ToString()) &&
                            post.status.ToString().Contains(CommonConstant.status_active.ToString())
                            select new
                            {
                                Id = post.id,
                                Title = post.title,
                                Description = post.description,
                                CreatedUser = user.name,
                                Created_at = post.created_at
                            }).ToList()
                          .Select(postView => new PostViewModel()
                          {
                              Id = postView.Id,
                              Title = postView.Title,
                              Description = postView.Description,
                              CreatedUser = postView.CreatedUser,
                              Created_at = postView.Created_at
                          });
            return postList;
        }

        /// <summary>
        /// This is to get post and created username from database by using keyword.
        /// </summary>
        /// <param name="search">Search keyword for Post</param>
        public IEnumerable<PostViewModel> GetPostsByKeyword(string search)
        {
            var postList = (from post in db.posts
                            join user in db.users
                            on post.create_user_id equals user.id
                            where post.status.ToString().Contains(CommonConstant.status_active.ToString()) &&
                            post.title.ToUpper().Contains(search.ToUpper()) ||
                            post.description.ToUpper().Contains(search.ToUpper()) ||
                            user.name.ToUpper().Contains(search.ToUpper())
                            select new
                            {
                                Id = post.id,
                                Title = post.title,
                                Description = post.description,
                                CreatedUser = user.name,
                                Created_at = post.created_at
                            }).ToList()
                          .Select(postView => new PostViewModel()
                          {
                              Id = postView.Id,
                              Title = postView.Title,
                              Description = postView.Description,
                              CreatedUser = postView.CreatedUser,
                              Created_at = postView.Created_at
                          });
            return postList;
        }

        /// <summary>
        /// This is to get user posts list by keyword
        /// </summary>
        /// <param name="search"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<PostViewModel> GetUserPostsByKeyword(string search, int id)
        {
            var postList = (from post in db.posts
                            join user in db.users
                            on post.create_user_id equals user.id
                            where post.create_user_id.ToString().Contains(id.ToString()) &&
                            post.status.ToString().Contains(CommonConstant.status_active.ToString()) &&
                            post.title.ToUpper().Contains(search.ToUpper()) ||
                            post.description.ToUpper().Contains(search.ToUpper()) ||
                            user.name.ToUpper().Contains(search.ToUpper())
                            select new
                            {
                                Id = post.id,
                                Title = post.title,
                                Description = post.description,
                                CreatedUser = user.name,
                                Created_at = post.created_at
                            }).ToList()
                          .Select(postView => new PostViewModel()
                          {
                              Id = postView.Id,
                              Title = postView.Title,
                              Description = postView.Description,
                              CreatedUser = postView.CreatedUser,
                              Created_at = postView.Created_at
                          });
            return postList;
        }

        /// <summary>
        ///     This is to get post list from database.
        /// </summary>
        /// <param name="search">Search keyword for Post</param>
        public IEnumerable<PostViewModel> GetPostList(string search)
        {
            if (search == null || search.Equals(""))
            {
                return GetPosts();
            }
            return GetPostsByKeyword(search);
        }

        /// <summary>
        /// This is to get user post list
        /// </summary>
        /// <param name="search"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<PostViewModel> GetUserPostList(string search, int id)
        {
            if (search == null || search.Equals(""))
            {
                return GetUserPosts(id);
            }
            return GetUserPostsByKeyword(search, id);
        }

        /// <summary>
        /// This is to confirm post to database
        /// </summary>
        /// <param name="postData"></param>
        public post ConfirmPost(post postData)
        {
            var exist = db.posts.Where(w => w.title == postData.title && w.description == postData.description).FirstOrDefault();
            if (exist != null)
            {
            }
            else
            {
                db.posts.Add(postData);
                db.SaveChanges();
            }
            return exist;
        }

        /// <summary>
        /// This is to return for edit post view
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public PostViewModel Edit(int id, PostViewModel postData)
        {
            var data = db.posts.Where(p => p.id == id).FirstOrDefault();
            postData.Title = data.title;
            postData.Description = data.description;
            if (data.status == 1)
            {
                postData.Status = true;
            }
            else
            {
                postData.Status = false;
            }
            return postData;
        }

        /// <summary>
        /// This is for edit post
        /// </summary>
        /// <param name="postData"></param>
        public void EditPost(int id, PostViewModel postData)
        {
            var data = db.posts.Where(x => x.id == id).FirstOrDefault();
            if (data != null)
            {
                data.title = postData.Title;
                data.description = postData.Description;
                if (postData.Status)
                {
                    data.status = 1;
                }
                else
                {
                    data.status = 0;
                }
                data.updated_user_id = postData.UpdatedUserId;
                data.updated_at = DateTime.Now;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// This is for delete post
        /// </summary>
        /// <param name="postId"></param>
        public void DeletePost(int postId, int loginId)
        {
            var data = (from item in db.posts

                        where item.id == postId

                        select item).SingleOrDefault();

            data.status = 0;
            data.deleted_at = DateTime.Now;
            data.deleted_user_id = loginId;
            db.Entry(data).State = EntityState.Modified;

            db.SaveChanges();
        }

        /// <summary>
        /// This is upload csv into database
        /// </summary>
        /// <param name="postData"></param>
        public void UploadCSV(post postData)
        {
            db.posts.Add(postData);
            db.SaveChanges();
        }

        /// <summary>
        /// This is download CSV
        /// </summary>
        /// <returns></returns>
        public IQueryable<post> DownloadCSV()
        {
            var postData = from post in db.posts.Take(10)
                           select post;

            return postData;
        }
        #endregion
    }
}
