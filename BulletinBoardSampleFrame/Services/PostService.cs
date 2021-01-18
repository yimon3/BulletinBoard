using BulletinBoardSampleFrame.DAO;
using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.ViewModel.Post;
using System.Collections.Generic;

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
        /// <param name="search">Search keyword for Post</param>
        public IEnumerable<PostViewModel> ShowPost()
        {
            var postList = postDAO.getPosts();
            return postList;
        }

        public void ConfirmPost(PostViewModel postData)
        {
            postDAO.confirmPost(postData);
        }

        public void EditPost(int id)
        {
            postDAO.EditPost(id);
        }

        public void EditPost(PostViewModel postData)
        {
            postDAO.EditPost(postData);
        }

        public IEnumerable<PostViewModel> ShowPostByKeyword(string search)
        {
            var postList = postDAO.getPostList(search);
            return postList;
        }

        public void DeletePost(int postId)
        {
            postDAO.DeletePost(postId);
        }
    }
}