using BulletinBoardSampleFrame.Models;
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

        [HttpGet]
        public ActionResult EditPost(int id)
        {
            postServices.EditPost(id);

            return View("EditPost");
        }

        [HttpPost]
        public ActionResult EditPost(PostViewModel pVM)
        {
            postServices.EditPost(pVM);
            return RedirectToAction("PostView");
        }

        public ActionResult CreatePost()
        {
            return View("CreatePost");
        }

        [HttpPost]
        public ActionResult ConfirmPost(PostViewModel postData)
        {
            return View("ConfirmPost", postData);
        }

        [HttpPost]
        public ActionResult Save(PostViewModel postData)
        {
            postServices.ConfirmPost(postData);
            return RedirectToAction("PostView", postData);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            postServices.DeletePost(id);
            return RedirectToAction("PostView");
        }


        public ActionResult Search(string search)
        {
            var postList = postServices.ShowPostByKeyword(search);

            return View("PostView", postList);
        }
    }
}
