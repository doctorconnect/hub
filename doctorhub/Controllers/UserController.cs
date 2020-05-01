using doctorhubBusinessEntities;
using doctorhubDataAccess;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace doctorhub.Controllers
{
    public class UserController : Controller
    {
        private DirectoryDataAccess objDirectoryDataAccess;

        public UserController()
        {
            objDirectoryDataAccess = new DirectoryDataAccess();
        }

        [HttpGet]
        public ActionResult UserProfile(int Usercode)
        {

            string Userid =  Usercode.ToString();
            var userDetails = objDirectoryDataAccess.GetListOfRegisteredUser().Where(x => x.UserCode == Userid).FirstOrDefault();
            ViewBag.Userid = userDetails.Id;
            ViewBag.UserCode = userDetails.UserCode;
            ViewBag.UserName = userDetails.UserName;
            Session["SerchUserName"] = userDetails.UserName;
            ViewBag.LOBName = userDetails.LOBName;
            ViewBag.AboutMe = userDetails.AboutMe;
            ViewBag.UserPhotoStatus = userDetails.ImgStatus;
            ViewBag.UserPhoto = "data:image/png;base64," + Convert.ToBase64String(userDetails.UserPhoto, 0, userDetails.UserPhoto.Length);
            var Follower = objDirectoryDataAccess.GetMyFollower(userDetails.Id.ToString());
            ViewBag.MyFollowing = Follower[0].FollowingBy;
            ViewBag.MyFollowers = Follower[0].FollowerBy;
            var Followstatus = objDirectoryDataAccess.GetStatusFollower(HttpContext.Session["ID"].ToString(), userDetails.Id.ToString());
            Session["status"] = Followstatus[0].CountFollower;
            Session["Serchid"] = userDetails.Id;
            Session["UserCode"] = userDetails.UserCode;
            Session["SerchUserNTID"] = userDetails.UserNTID;
            ViewBag.User_BlogList_detail = objDirectoryDataAccess.GetBlogList().Where(f => f.BlogerId.ToString() == userDetails.Id.ToString());

            return View();
        }

        public ActionResult PostLikes()
        {
            int msg = 0;
            int keyId = int.Parse(Request.QueryString["key"]);
            string Identifier = Request.QueryString["Identifier"];

            msg = objDirectoryDataAccess.SubmitLike(keyId, Identifier);

            if (msg > 0)
                TempData["Success"] = "" + Identifier + " Liked";
            else if (msg == 0)
                TempData["error"] = "Some Error Occured. Please Contact Admin";

            return RedirectToAction("UserProfile", "User", new { @Usercode = Session["UserCode"] });
        }

        public ActionResult FlagPost()
        {
            int msg = 0;
            string keyId = Request.QueryString["key"];
            string Id = Request.QueryString["Id"].PadLeft(9, '0');
            msg = objDirectoryDataAccess.PostFlag(int.Parse(keyId));
            if (msg > 0)
            {
                UserRegistrationModel model = objDirectoryDataAccess.GetListOfRegisteredUser().Where(x => x.UserCode == Id).FirstOrDefault();
                objDirectoryDataAccess.SendEmail(model, "KMT6");
                objDirectoryDataAccess.SendEmail(model, "KMT7");
                TempData["success"] = "Post Flagged";
            }
            else
            {
                TempData["error"] = "Some Error Occured. Please Contact Admin";
            }
            return RedirectToAction("UserProfile", "User", new { @Usercode = Session["UserCode"] });
        }

        [HttpPost]
        public ActionResult SaveComment(int PostId, string txtcomment, string Identifier)
        {
            int msg = 0;
            msg = objDirectoryDataAccess.SubmitCommentt(PostId, txtcomment, Identifier);
            if (msg > 0)
                TempData["Success"] = "Comment Posted On " + Identifier + " Successfully";
            else if (msg == 0)
                TempData["error"] = "Some Error Occured. Please Contact Admin";

            return RedirectToAction("UserProfile", "User", new { @Usercode = Session["UserCode"] });
        }

        public JsonResult DeletePost(string keyId, string Identifier)
        {
            return Json(objDirectoryDataAccess.DeletPost(int.Parse(keyId), Identifier), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownLoadDoc(string txtValue)
        {
            if (txtValue == null)
                txtValue = Request.QueryString["key"];
            var Doclist = objDirectoryDataAccess.GetListOfUploadDoc().Where(m => m.ID == int.Parse(txtValue)).FirstOrDefault(); ;
            string path = AppDomain.CurrentDomain.BaseDirectory + "UploadDocument/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + Doclist.Name);
            string fileName = Doclist.Name;

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public ActionResult Following(string type)
        {
            int msg = 0;
            msg = objDirectoryDataAccess.UpdateFollower(type, HttpContext.Session["ID"].ToString(), HttpContext.Session["Serchid"].ToString());
            if (msg > 0)
                TempData["Success"] = "You are now Following " + HttpContext.Session["SerchUserName"].ToString();
            else if (msg == 0)
                TempData["error"] = "Some Error Occured. Please Contact Admin";
            var Followstatus = objDirectoryDataAccess.GetStatusFollower(HttpContext.Session["ID"].ToString(), HttpContext.Session["Serchid"].ToString());
            Session["status"] = Followstatus[0].CountFollower;

            return RedirectToAction("UserProfile", "User", new { @Usercode = Session["UserCode"] });
        }

        public ActionResult MyBlog(int Id)
        {
            var details = objDirectoryDataAccess.GetBlogList().Where(x => x.BlogId == Convert.ToInt32(Id)).FirstOrDefault();
            if (details == null)
            {
                return HttpNotFound();
            }
            Blog getfetcheddata = new Blog();

            return PartialView(@"~/Views/User/_myModal.cshtml", details);
        }

        [HttpPost]
        public ActionResult BlogPost(Blog model)
        {
            int msg = 0;
            if (ModelState.IsValid)
            {

                int UserCode = int.Parse(HttpContext.Session["ID"].ToString());
                msg = objDirectoryDataAccess.SubmitBlogPost(model, UserCode);
                if (msg > 0)
                {
                    ViewBag.BlogList_detail = objDirectoryDataAccess.GetBlogList();
                    TempData["success"] = "Blog Submitted Successfully. It will be available after Approval";
                }
                else
                {
                    TempData["error"] = "Some Error Occured. Please Contact Admin";
                }
            }
            else
            {
                return RedirectToAction("UserProfile", "User", new { @Usercode = Session["UserCode"] });
            }
            return RedirectToAction("UserProfile", "User", new { @Usercode = Session["UserCode"] });
        }

        public ActionResult Follower(int Id)
        {
            var details = objDirectoryDataAccess.GetBlogList().Where(x => x.BlogId == Convert.ToInt32(Id)).FirstOrDefault();
            if (details == null)
            {
                return HttpNotFound();
            }
            Blog getfetcheddata = new Blog();

            return PartialView(@"~/Views/MyProfile/_myModal.cshtml", details);
        }

        [HttpPost]
        public ActionResult SearchEmp(string txtSearch)
        {
            Session["SearchKey"] = txtSearch;
            ViewBag.user_detail = objDirectoryDataAccess.GetUserList(txtSearch);

            return this.View("SearchResult");
        }

        [HttpPost]
        public ActionResult SearchResult(UserRegistrationModel Name)
        {
            Session["SearchKey"] = Name.UserName;
            ViewBag.user_detail = objDirectoryDataAccess.GetUserList(Name.UserName);

            return View();
        }
    }
}
