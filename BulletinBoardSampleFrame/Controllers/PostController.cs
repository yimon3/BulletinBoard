using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.Properties;
using BulletinBoardSampleFrame.Services;
using BulletinBoardSampleFrame.ViewModel.Post;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BulletinBoardSampleFrame.Controllers
{
    /// <summary>
    ///     This is Post Controller class.
    /// </summary>
    public class PostController : Controller
    {
        PostServices postServices = new PostServices();

        public ActionResult Index()
        {
            return View();
        }
        
        // GET: BulletinBoard
        /// <summary>
        /// This is to get post list.
        /// </summary>
        /// <param name="search">Search keyword for Post</param>
        public ActionResult PostView()
        {
            var postList = postServices.ShowPost();
            return View("PostView", postList);
        }

        /// <summary>
        /// This is to get post for edit
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postView"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id, PostViewModel postView)
        {
            postServices.Edit(id, postView);

            return View("EditPost", postView);
        }

        /// <summary>
        /// This is to confirm post for edit
        /// </summary>
        /// <param name="postView"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ConfirmUpdatePost(PostViewModel postView)
        {
            return View("ConfirmUpdatePost", postView);
        }

        /// <summary>
        /// This is to edit post
        /// </summary>
        /// <param name="pVM"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditPost(int id, PostViewModel pVM)
        {
            postServices.EditPost(id, pVM);
            return RedirectToAction("PostView");
        }

        /// <summary>
        /// Get : create post
        /// </summary>
        /// <returns></returns>
        public ActionResult CreatePost()
        {
            return View("CreatePost");
        }

        /// <summary>
        /// This is to confirm post
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ConfirmPost(PostViewModel postData)
        {
            return View("ConfirmPost", postData);
        }

        /// <summary>
        /// This is to save new post to database
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(PostViewModel postData, post newpost)
        {
            newpost.create_user_id = (int)Session["Id"];
            newpost.updated_user_id = (int)Session["Id"];
            newpost.created_at = DateTime.Now;
            newpost.updated_at = DateTime.Now;
            newpost.status = CommonConstant.stauts_active;

            var exist = postServices.ConfirmPost(newpost);
            if (exist != null)
            {
                ViewData["Message"] = "Duplicate Data are not inserted.";
                return View("CreatePost", postData);
            }
            return RedirectToAction("PostView", postData);
        }

        /// <summary>
        /// This is for delete post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            postServices.DeletePost(id);
            return RedirectToAction("PostView");
        }

        /// <summary>
        /// This is for search post
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult Search(string searchString)
        {
            var postList = postServices.ShowPostByKeyword(searchString);

            return View("PostView", postList);
        }

        /// <summary>
        /// This is post view list for visitor
        /// </summary>
        /// <returns></returns>
        public ActionResult PostViewForVisitor()
        {
            var postList = postServices.ShowPost();

            return View("PostViewForVisitor", postList);
        }
    }
}
