using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.Properties;
using BulletinBoardSampleFrame.Services;
using BulletinBoardSampleFrame.ViewModel.Post;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        /// <summary>
        /// GET: BulletinBoard
        /// </summary>
        /// <returns></returns>
        public ActionResult PostViewDefault(int? page)
        {
            int pageSize = 5;
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            var postList = postServices.ShowPost().ToPagedList(pageIndex, pageSize);

            return View("PostViewDefault",postList);
        }

        /// <summary>
        /// This is to get post list for admin.
        /// </summary>
        /// <param name="search">Search keyword for Post</param>
        public ActionResult PostView(int? page)
        {
            int pageSize = 5;
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            var postList = postServices.ShowPost().ToPagedList(pageIndex, pageSize);
            if((string)Session["Type"] == "Admin")
            {
                return View("PostView", postList);
            }
            return View("PostViewForVisitor", postList);
        }

        /// <summary>
        /// This is post list for user and visitor
        /// </summary>
        /// <returns></returns>
        public ActionResult PostList(int? page)
        {
            int id = (int)Session["Id"];
            int pageSize = 5;
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            var postList = postServices.GetUserPost(id).ToPagedList(pageIndex, pageSize);

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
            pVM.UpdatedUserId = (int)Session["Id"];

            postServices.EditPost(id, pVM);
            return RedirectToAction("PostView");
        }

        /// <summary>
        /// Clear text input 
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearInput()
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }
            return View("CreatePost");
        }

        /// <summary>
        /// Clear text input
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearEditInput()
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }
            return View("EditPost");
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
            newpost.status = CommonConstant.status_active;

            var exist = postServices.ConfirmPost(newpost);
            if (exist != null)
            {
                ViewData["Message"] = "Duplicate Data are not inserted.";
                return View("CreatePost", postData);
            }
            if((string)Session["Type"] == "User")
            {
                return RedirectToAction("PostList", postData);
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
            int loginId = (int)Session["Id"];
            postServices.DeletePost(id, loginId);
            return RedirectToAction("PostView");
        }

        /// <summary>
        /// This is search for posts
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public ActionResult Search(string searchString, int? page)
        {
            int pageSize = 5;
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            if ((string)Session["Type"] != "Admin")
            {
                int id = (int)Session["Id"];
                var postData = postServices.ShowUserPostByKeyword(searchString, id).ToPagedList(pageIndex, pageSize);

                return RedirectToAction("PostList", postData);
            }
            var postList = postServices.ShowPostByKeyword(searchString).ToPagedList(pageIndex, pageSize);

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
                                Title = row.Split(',')[0],
                                Description = row.Split(',')[1],
                                Created_at = Convert.ToDateTime(row.Split(',')[2])
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
                    newPost.title = posts.Title;
                    newPost.description = posts.Description;
                    newPost.status = CommonConstant.status_active;
                    newPost.create_user_id = (int)Session["Id"];
                    newPost.updated_user_id = (int)Session["Id"];
                    newPost.created_at = posts.Created_at;
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
