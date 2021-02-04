using BulletinBoardSampleFrame.DAO;
using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.ViewModel.Post;
using System.Collections.Generic;
using System.Linq;

namespace BulletinBoardSampleFrame.Services
{
    /// <summary>
    ///     Service Class
    ///     Contains all method validing and connecting the Controller and DAO
    /// </summary>
    public class PostServices
    {
        PostDAO postDAO = new PostDAO();
        /// <summary>
        ///     This is to show post list.
        /// </summary>
        public IEnumerable<PostViewModel> ShowPost()
        {
            var postList = postDAO.GetPosts();
            return postList;
        }

        /// <summary>
        /// This is to get user post list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<PostViewModel> GetUserPost(int id)
        {
            var postUserList = postDAO.GetUserPosts(id);
            return postUserList;
        }

        /// <summary>
        /// This is to show user post by keyword
        /// </summary>
        /// <param name="search"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<PostViewModel> ShowUserPostByKeyword(string search, int id)
        {
            var postList = postDAO.GetUserPostList(search, id);
            return postList;
        }

        /// <summary>
        /// This is to show post by keyword
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IEnumerable<PostViewModel> ShowPostByKeyword(string search)
        {
            var postList = postDAO.GetPostList(search);
            return postList;
        }

        /// <summary>
        /// This is to confirm post
        /// </summary>
        /// <param name="postData"></param>
        public post ConfirmPost(post postData)
        {
            return postDAO.ConfirmPost(postData);
        }

        /// <summary>
        /// This is to edit post for view
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postView"></param>
        public void Edit(int id, PostViewModel postView)
        {
            postDAO.Edit(id, postView);
        }

        /// <summary>
        /// This is to edit post to database
        /// </summary>
        /// <param name="postData"></param>
        public void EditPost(int id, PostViewModel postData)
        {
            postDAO.EditPost(id, postData);
        }

        /// <summary>
        /// This is to delete post
        /// </summary>
        /// <param name="postId"></param>
        public void DeletePost(int postId, int loginId)
        {
            postDAO.DeletePost(postId, loginId);
        }

        /// <summary>
        /// This is upload csv data
        /// </summary>
        /// <param name="postData"></param>
        public void UploadCSV(post postData)
        {
            postDAO.UploadCSV(postData);
        }

        /// <summary>
        /// This is download csv file
        /// </summary>
        /// <returns></returns>
        public IQueryable<post> DownloadCSV()
        {
            return postDAO.DownloadCSV();
        }
    }
}
