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

        public void ConfirmPost(post postData)
        {
            postDAO.confirmPost(postData);
        }

        /// <summary>
        /// This is to edit post for view
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postView"></param>
        public void EditPost(int id, PostViewModel postView)
        {
            postDAO.EditPost(id, postView);
        }

        /// <summary>
        /// This is to edit post to database
        /// </summary>
        /// <param name="postData"></param>
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