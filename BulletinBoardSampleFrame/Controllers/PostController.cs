using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.Properties;
using BulletinBoardSampleFrame.Services;
using BulletinBoardSampleFrame.ViewModel.Post;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
        public ActionResult EditPost(int id, PostViewModel postView)
        {
            postServices.EditPost(id, postView);

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
        public ActionResult EditPost(PostViewModel pVM)
        {
            postServices.EditPost(pVM);
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

        /// <summary>
        /// This is to upload csv file
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadCSV()
        {
            return View();
        }

        /// <summary>
        /// This is upload csv file into database
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadCSV(HttpPostedFileBase postedFile)
        {
            List<PostViewModel> postData = new List<PostViewModel>();
            string filePath = string.Empty;
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                if (postedFile.FileName.EndsWith(".csv"))
                {

                    string path = Server.MapPath("~/Uploads/@Session['Id']/csv/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    filePath = path + Path.GetFileName(postedFile.FileName);
                    string extension = Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    //Read the contents of CSV file.
                    string csvData = System.IO.File.ReadAllText(filePath);

                    //Execute a loop over the rows.
                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            postData.Add(new PostViewModel
                            {
                                title = row.Split(',')[0],
                                description = row.Split(',')[1],
                                created_at = Convert.ToDateTime(row.Split(',')[2])
                            });
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "This file format is not supported");
                    return View();
                }
                foreach (var posts in postData)
                {
                    var newPost = new post();
                    newPost.title = posts.title;
                    newPost.description = posts.description;
                    newPost.status = CommonConstant.stauts_active;
                    newPost.create_user_id = (int)Session["Id"];
                    newPost.updated_user_id = (int)Session["Id"];
                    newPost.created_at = posts.created_at;
                    newPost.updated_at = DateTime.Now;

                    postServices.UploadCSV(newPost);
                }
            }
            return RedirectToAction("PostView", "Post");
        }

        /// <summary>
        /// Download Csv file
        /// </summary>
        /// <returns></returns>
        public FileResult DownloadCSV()
        {
            string[] columnNames = new string[] { "Id", "Title", "Description", "Posted User", "Status", "Created User Id", "Updated User Id", "Deleted User Id", "Created At", "Update At", "Deleted At" };
            var data = postServices.DownloadCSV();
            string csv = string.Empty;

            foreach (string columnName in columnNames)
            {
                csv += columnName + ',';
            }

            csv += "\r\n";

            foreach (var post in data)
            {
                csv += post.id.ToString().Replace(",", ";") + ',';
                csv += post.title.Replace(",", ";") + ',';
                csv += post.description.Replace(",", ";") + ',';
                csv += post.user.name.Replace(",", ";") + ',';
                csv += post.status.ToString().Replace(",", ";") + ',';
                csv += post.create_user_id.ToString().Replace(",", ";") + ',';
                csv += post.updated_user_id.ToString().Replace(",", ";") + ',';
                csv += post.deleted_user_id.ToString().Replace(",", ";") + ',';
                csv += post.created_at.ToString().Replace(",", ";") + ',';
                csv += post.updated_at.ToString().Replace(",", ";") + ',';
                csv += post.deleted_at.ToString().Replace(",", ";") + ',';
                csv += "\r\n";
            }

            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", "PostData.csv");
        }
    
}
}
