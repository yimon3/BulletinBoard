using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.ViewModel.Post;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace BulletinBoardSampleFrame.DAO
{
    /// <summary>
    ///  DAO (Data Access Class)
    ///  contains all method that connected to the database
    /// </summary>
    /// 
    public class PostDAO
    {
        BulletinBoardEntity db = new BulletinBoardEntity();
        /// <summary>
        /// This is to get all post and created user name from database.
        /// </summary>
        public IEnumerable<PostViewModel> getPosts()
        {
            var postList = (from post in db.posts
                            join user in db.users
                            on post.create_user_id equals user.id
                            select new
                            {
                                id = post.id,
                                title = post.title,
                                description = post.description,
                                status = post.status,
                                CreatedName = user.name,
                                created_at = post.created_at
                            }).ToList()
                          .Select(postView => new PostViewModel()
                          {
                              id = postView.id,
                              title = postView.title,
                              description = postView.description,
                              CreatedName = postView.CreatedName,
                              created_at = postView.created_at
                          });
            return postList;
        }
        /// <summary>
        /// This is to get post and created username from database by using keyword.
        /// </summary>
        /// <param name="search">Search keyword for Post</param>
        public IEnumerable<PostViewModel> getPostsByKeyword(string search)
        {
            var postList = (from post in db.posts
                            join user in db.users
                            on post.create_user_id equals user.id
                            where post.title.ToUpper().Contains(search.ToUpper()) ||
                            post.description.ToUpper().Contains(search.ToUpper()) ||
                            post.user1.name.ToUpper().Contains(search.ToUpper())
                            select new
                            {
                                id = post.id,
                                title = post.title,
                                description = post.description,
                                status = post.status,
                                CreatedName = user.name,
                                created_at = post.created_at
                            }).ToList()
                          .Select(postView => new PostViewModel()
                          {
                              id = postView.id,
                              title = postView.title,
                              description = postView.description,
                              CreatedName = postView.CreatedName,
                              created_at = postView.created_at
                          });
            return postList;
        }
        /// <summary>
        ///     This is to get post list from database.
        /// </summary>
        /// <param name="search">Search keyword for Post</param>
        public IEnumerable<PostViewModel> getPostList(string search)
        {
            if (search == null || search.Equals(""))
            {
                return getPosts();
            }
            return getPostsByKeyword(search);
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
            postData.title = data.title;
            postData.description = data.description;
            if (data.status == 1)
            {
                postData.status = true;
            }
            else
            {
                postData.status = false;
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
                data.title = postData.title;
                data.description = postData.description;
                if (postData.status)
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
        public void DeletePost(int postId)
        {
            var data = (from item in db.posts

                        where item.id == postId

                        select item).SingleOrDefault();

            db.posts.Remove(data);
            db.SaveChanges();
        }
    }
}