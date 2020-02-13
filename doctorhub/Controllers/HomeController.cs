using doctorhubBusinessEntities;
using doctorhubDataAccess;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doctorhub.Controllers
{
    public class HomeController : Controller
    {
        private DirectoryDataAccess objDirectoryDataAccess;
        private Feedback objFeedback;

        public HomeController()
        {
            objDirectoryDataAccess = new DirectoryDataAccess();
            objFeedback = new Feedback();
            objFeedback.UserFeedBackList = new List<FeedBackList>();
        }

        public ActionResult Index()
        {
            string userNTId = HttpContext.Session["UserNTID"].ToString();
            var userDetails = objDirectoryDataAccess.GetListOfRegisteredUser().Where(x => x.UserNTID == userNTId && x.IsActive == true).FirstOrDefault();
            if (userDetails != null)
            {
                HttpContext.Session["Adminstrator"] = userDetails.IsAdmin;
                HttpContext.Session["ID"] = userDetails.Id;
                HttpContext.Session["CapabilitiesId"] = userDetails.CapabilitiesId;
                HttpContext.Session["RoleId"] = userDetails.RoleId;
                HttpContext.Session["UserPhoto"] = userDetails.UserPhoto;
                HttpContext.Session["AboutMe"] = userDetails.AboutMe;
                HttpContext.Session["BusinessSegmen"] = userDetails.BusinessSegmentName;
                HttpContext.Session["CapabilitiesName"] = userDetails.CapabilitiesName;
                if (!string.IsNullOrEmpty(userDetails.UserCode))
                {
                    if (userDetails.Status == Convert.ToInt32(StatusType.Approve))
                    {
                        ViewBag.UserName = userDetails.UserName;
                        ViewBag.LOBName = userDetails.LOBName;
                        ViewBag.BusinessSegment = userDetails.BusinessSegmentName;
                        ViewBag.AboutMe = userDetails.AboutMe;
                        ViewBag.ImgStatus = userDetails.ImgStatus;
                        ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(userDetails.UserPhoto, 0, userDetails.UserPhoto.Length);
                        return View();
                    }
                    else if (userDetails.Status == Convert.ToInt32(StatusType.Reject))
                    {
                        return RedirectToAction("UserRegistration", "Admin");
                    }
                    else
                    {
                        TempData["statusPending"] = "User Registration Is Successful";
                        return RedirectToAction("UserRegistration", "Admin");
                    }
                }
                else
                {
                    return RedirectToAction("UserRegistration", "Admin");
                }
            }
            else
            {
                return RedirectToAction("UserRegistration", "Admin");
            }

            return View();
        }

        public JsonResult GetNotificationsDetails()
        {
            List<Notification> list = new List<Notification>();
            List<Notification> fiteredList = new List<Notification>();
            int roleId = Convert.ToInt32(HttpContext.Session["RoleId"]);
            var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;

            if (roleId == Convert.ToInt32(RoleType.Admin))
            {
                list = objDirectoryDataAccess.GetListOfNotification().Where(x => x.IsAdminFlag == false && x.CapabilityId == Convert.ToInt32(HttpContext.Session["CapabilitiesId"])).ToList();
                foreach (var itm in list)
                {
                    if (itm.RoleId.Split(',').Contains(Convert.ToInt32(RoleType.Admin).ToString()))
                        fiteredList.Add(new Notification { AdminDescripation = itm.AdminDescripation, Id = itm.Id });
                    list = fiteredList;
                }
            }
            else if (roleId == Convert.ToInt32(RoleType.Facilitator))
            {
                list = objDirectoryDataAccess.GetListOfNotification().Where(x => x.IsFacilitatorFlag == false && x.CapabilityId == Convert.ToInt32(HttpContext.Session["CapabilitiesId"])).ToList();
                foreach (var itm in list)
                {
                    if (itm.RoleId.Split(',').Contains(Convert.ToInt32(RoleType.Facilitator).ToString()))
                        fiteredList.Add(new Notification { AdminDescripation = itm.AdminDescripation, Id = itm.Id });
                    list = fiteredList;
                }
            }
            else if (roleId == Convert.ToInt32(RoleType.User))
            {
                list = objDirectoryDataAccess.GetListOfNotification().Where(x => x.UserCode == Convert.ToString(HttpContext.Session["UserEmployeeId"]) && x.IsUserFlag == false).ToList();
                foreach (var itm in list)
                {
                    if (itm.RoleId.Split(',').Contains(Convert.ToInt32(RoleType.User).ToString()))
                        fiteredList.Add(new Notification { UserDescripation = itm.UserDescripation, Id = itm.Id });
                    list = fiteredList;
                }
            }

            else if (roleId == Convert.ToInt32(RoleType.Contributor))
            {
                list = objDirectoryDataAccess.GetListOfNotification().Where(x => x.UserCode == Convert.ToString(HttpContext.Session["UserEmployeeId"]) && x.IsUserFlag == false).ToList();
                foreach (var itm in list)
                {
                    if (itm.RoleId.Split(',').Contains(Convert.ToInt32(RoleType.User).ToString()))
                        fiteredList.Add(new Notification { UserDescripation = itm.UserDescripation, Id = itm.Id });
                    list = fiteredList;
                }
            }
            Session["LastUpdate"] = DateTime.Now;
            var genericResult = new { data = list, count = list.Count() };
            var result = this.Json(genericResult, JsonRequestBehavior.AllowGet);

            return result;
        }

        public JsonResult AdminReply()
        {
            var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
            var list = objDirectoryDataAccess.GetListOfUserFeedback().Where(x => x.UserCode == Convert.ToString(HttpContext.Session["UserEmployeeId"]) && x.AdminReply != "").OrderByDescending(a => a.ModifiedOn).ToList();
            Session["LastUpdate"] = DateTime.Now;

            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public ActionResult SubmitFeedBack()
        {
            string userNTId = HttpContext.Session["UserNTID"].ToString();
            var userFeedBackdetails = objDirectoryDataAccess.GetListOfUserFeedback().Where(x => x.UserNTID == userNTId).ToList();
            if (userFeedBackdetails.Count() == 0)
            {
                var userDetails = objDirectoryDataAccess.GetListOfRegisteredUser().Where(x => x.UserNTID == userNTId && x.IsActive == true).FirstOrDefault();
                return View(ConvertModelToFeedBack(userDetails));
            }
            else
            {
                ConvertModelToFeedbackList(userFeedBackdetails);
            }

            return View(objFeedback);
        }

        [HttpPost]
        public ActionResult SubmitFeedBack(Feedback model)
        {
            int msg = 0;
            if (ModelState.IsValid)
            {
                msg = objDirectoryDataAccess.SubmitUserFeedBack(model);
                if (msg > 0)
                {
                    UserRegistrationModel userModel = objDirectoryDataAccess.GetListOfRegisteredUser().Where(x => x.UserCode == model.UserCode).FirstOrDefault();
                    objDirectoryDataAccess.SendEmail(userModel, "KMT17");
                    objDirectoryDataAccess.SendEmail(userModel, "KMT18");
                    TempData["success"] = "Thank you for your feedback.  We’ll review your comments and respond to you through email within 24-48 hours";
                }
                else
                    TempData["error"] = "Some Error Occured. Please Contact Admin";
            }
            else
            {
                return RedirectToAction("SubmitFeedBack");
            }
            return RedirectToAction("SubmitFeedBack");
        }

        private Feedback ConvertModelToFeedBack(UserRegistrationModel model)
        {
            Feedback objFeedback = new Feedback();

            objFeedback.UserCode = model.UserCode;
            objFeedback.UserName = model.UserName;
            objFeedback.UserNTID = model.UserNTID;
            objFeedback.UserEmail = model.UserEmail;
            objFeedback.UserLOB = model.LOBId;
            objFeedback.UserLobName = model.LOBName;

            return objFeedback;
        }

        private Feedback ConvertModelToFeedbackList(List<Feedback> model)
        {
            for (int i = 0; i < model.Count(); i++)
            {
                objFeedback.UserCode = model[0].UserCode;
                objFeedback.UserName = model[0].UserName;
                objFeedback.UserNTID = model[0].UserNTID;
                objFeedback.UserEmail = model[0].UserEmail;
                objFeedback.UserLOB = model[0].UserLOB;
                objFeedback.UserLobName = model[0].UserLobName;

                FeedBackList objFeedBackList = new FeedBackList
                {
                    FeedBackId = model[i].FeedBackId,
                    UserFeedback = model[i].FeedbackQuestion,
                    AdminReply = model[i].AdminReply,
                    FeedbackQuestionDate = ChangeDateFormatToName(model[i].FeedbackQuestionDate),
                    AdminReplyDate = ChangeDateFormatToName(model[i].AdminReplyDate)
                };
                objFeedback.UserFeedBackList.Add(objFeedBackList);
            }

            return objFeedback;
        }

        private string ChangeDateFormatToName(string dateTime)
        {
            string date = string.Empty;

            if (!string.IsNullOrEmpty(dateTime))
            {
                string[] monthNames = new string[] { "Jan", "Feb", "Mar", "April", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec" };

                string[] dateVal = dateTime.Split('/');
                date = dateVal[1] + "-" + monthNames[Convert.ToInt32(dateVal[0]) - 1] + "-" + dateVal[2];
            }
            return date;
        }

        public ActionResult LoadAbotMe()
        {
            ViewBag.AboutMe = Session["AboutMe"];
            return PartialView("~/Views/MyProfile/_AboutMe.cshtml");
        }

        public ActionResult LoadPostsAndCommentPartialView()
        {
            ViewBag.PostList = objDirectoryDataAccess.GetPost();
            return PartialView("~/Views/MyProfile/_MyPostAndComment.cshtml");
        }

        public ActionResult LoadDocumentPartialView()
        {

            List<SelectListItem> items = new List<SelectListItem>();
            var DcList = objDirectoryDataAccess.GetDocumentCategory().Where(a => a.IsActive == true).Select(l => new { l.ID, l.Name });

            foreach (var item in DcList)
            {
                items.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.ID.ToString(),
                });
            }
            ViewBag.DocList = items;
            ViewBag.UpladDocBlogList_detail = objDirectoryDataAccess.GetListOfUploadDoc();

            return PartialView("~/Views/MyProfile/_Document.cshtml");
        }

        public ActionResult LoadBlogePartialView()
        {
            ViewBag.BlogList = objDirectoryDataAccess.GetBlogList();
            return PartialView("~/Views/MyProfile/_MyBlog.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BlogPost(Blog model, string message, string title)
        {
            int msg = 0;
            if (ModelState.IsValid)
            {
                int UserCode = int.Parse(HttpContext.Session["ID"].ToString());
                msg = objDirectoryDataAccess.SubmitBlogPost(model, UserCode);
                if (msg > 0)
                {
                    ViewBag.BlogList_detail = objDirectoryDataAccess.GetBlogList();
                    UserRegistrationModel userModel = objDirectoryDataAccess.GetListOfRegisteredUser().Where(x => x.UserCode == Convert.ToString(HttpContext.Session["UserEmployeeId"])).FirstOrDefault();
                    objDirectoryDataAccess.SendEmail(userModel, "KMT9");
                    objDirectoryDataAccess.SendEmail(userModel, "KMT10");
                    TempData["success"] = "Blog Submitted Successfully. It will be available after Admin Approval";
                }
                else
                {
                    TempData["error"] = "Some Error Occured. Please Contact Admin";
                    return RedirectToAction("index", "Home");
                }
            }

            ModelState.Clear();
            return RedirectToAction("index", "Home");
        }

        [HttpPost]
        public ActionResult UpdateProfileImg([Bind(Exclude = "UserPhoto")]UserRegistrationModel model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                int msg = 0;
                byte[] imageData = null;
                string targetPath;
                HttpPostedFileBase poImgFile = Request.Files["UserPhoto"];
                Stream strm = poImgFile.InputStream;
                if (Request.Files.Count > 0)
                {
                    using (var image = System.Drawing.Image.FromStream(strm))
                    {
                        // Get Original Size of file (Height or Width)   
                        string imagesze = image.Size.ToString();
                        int newWidth = 225; // New Width of Image in Pixel  
                        int newHeight = 225; // New Height of Image in Pixel 
                        var thumbImg = new Bitmap(newWidth, newHeight);
                        var thumbGraph = Graphics.FromImage(thumbImg);
                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                        thumbGraph.DrawImage(image, imgRectangle);
                        // Save the file  
                        targetPath = Server.MapPath(@"~\Images\profileimages\") + poImgFile.FileName.Split('\\').Last();
                        thumbImg.Save(targetPath, image.RawFormat);
                        //int thumWidth = 35; // New Width of thumImage in Pixel  
                        //int thumHeight = 35; // New Height of thumImage in Pixel
                        //var newthumbImg = new Bitmap(thumWidth, thumHeight);
                        //string targetthumPath = Server.MapPath(@"~\Images\profileimages\")+"thum" + poImgFile.FileName.Split('\\').Last();
                        //newthumbImg.Save(targetthumPath, image.RawFormat);
                        imageData = System.IO.File.ReadAllBytes(targetPath);

                    }
                }
                msg = objDirectoryDataAccess.UpdateProfileImg(poImgFile, imageData);
                if (msg > 0)
                    TempData["Success"] = "Display picture successfully uploaded and will be visible post admin approval";
                else if (msg == 0)
                    TempData["error"] = "Some Error Occured. Please Contact Admin";
            }

            ModelState.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AboutMe(string info)
        {
            int msg = 0;
            msg = objDirectoryDataAccess.UpdateAboutMe(info);
            if (msg > 0)
                TempData["Success"] = "About You Is Updated SuccessFully";
            else if (msg == 0)
                TempData["error"] = "Some Error Occured. Please Contact Admin";

            return RedirectToAction("Index", "Home");
            //return PartialView("~/Views/MyProfile/_AboutMe.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SavePost(string Message)
        {
            var msg = objDirectoryDataAccess.SubmitPost(Message.Replace("!!", "&").Replace("!!!", "'\'"));
            ViewBag.PostList = objDirectoryDataAccess.GetPost();

            return PartialView("~/Views/MyProfile/_MyPostAndComment.cshtml");
        }

        [HttpPost]
        public ActionResult SaveComment(int PostId, string txtcomment, string Identifier)
        {
            int msg = 0;
            msg = objDirectoryDataAccess.SubmitCommentt(PostId, txtcomment, Identifier);
            if (msg > 0)
                TempData["Success"] = "Comment Posted";
            else if (msg == 0)
                TempData["error"] = "Some Error Occured. Please Contact Admin";

            if (Identifier == "POST")
            {
                ViewBag.PostList = objDirectoryDataAccess.GetPost();
                return PartialView("~/Views/MyProfile/_MyPostAndComment.cshtml");
            }
            if (Identifier == "BLOG")
            {
                ViewBag.BlogList = objDirectoryDataAccess.GetBlogList();
                return PartialView("~/Views/MyProfile/_MyBlog.cshtml");
            }
            if (Identifier == "DOC")
            {
                List<SelectListItem> items = new List<SelectListItem>();
                var DcList = objDirectoryDataAccess.GetDocumentCategory().Where(a => a.IsActive == true).Select(l => new { l.ID, l.Name });

                foreach (var item in DcList)
                {
                    items.Add(new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.ID.ToString(),
                    });
                }
                ViewBag.DocList = items;
                //ViewBag.DocList = objCommonController.BindDocCategoryList();
                ViewBag.UpladDocBlogList_detail = objDirectoryDataAccess.GetListOfUploadDoc();

                return PartialView("~/Views/MyProfile/_Document.cshtml");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult Index(string Prefix)
        {
            var UserName = objDirectoryDataAccess.GetUserList(Prefix);
            return Json(UserName, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageVote()
        {
            string CurrentDate = Convert.ToDateTime(DateTime.Now).ToString("MM/dd/yyyy");
            var VoteDetaills = objDirectoryDataAccess.GetPolls(CurrentDate);
            if (VoteDetaills.Count > 0 && VoteDetaills != null)
            {
                return View(VoteDetaills.ToList());
            }
            return View();
        }

        public ActionResult SubmitVote(string strTitle, string rbtnValue)
        {
            int msg = 0;
            if (strTitle != null && rbtnValue != null)
            {
                msg = objDirectoryDataAccess.CreateVote(strTitle, rbtnValue);
                if (msg > 0)
                {
                    TempData["VoteResult"] = "Vote Successful";
                }
                else if (msg == 0)
                {
                    TempData["error"] = "Some Error Occured. Please Contact Admin";
                }
            }
            return RedirectToAction("index", "Home");
        }

        [HttpPost]
        public JsonResult IsAttendWebex(int TrainingId, string TrainingTitle)
        {
            int msg = 0;
            msg = objDirectoryDataAccess.IsAttendWebExTraing(TrainingId, TrainingTitle);

            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateNotificationCount()
        {
            List<Notification> list = new List<Notification>();
            List<Notification> fiteredList = new List<Notification>();
            List<Notification> withoutfitereList = new List<Notification>();
            int roleId = Convert.ToInt32(HttpContext.Session["RoleId"]);
            var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;

            if (roleId == Convert.ToInt32(RoleType.Admin))
            {
                list = objDirectoryDataAccess.GetListOfNotification().Where(x => x.CapabilityId == Convert.ToInt32(HttpContext.Session["CapabilitiesId"]) && x.AdminDescripation != null && x.AdminDescripation != "").OrderByDescending(x => x.Id).ToList();
                withoutfitereList = list;
                foreach (var itm in list.Take(5))
                {
                    if (itm.RoleId.Split(',').Contains(Convert.ToInt32(RoleType.Admin).ToString()))
                        fiteredList.Add(new Notification { AdminDescripation = itm.AdminDescripation, Id = itm.Id, IsAdminFlag = itm.IsAdminFlag });
                    list = fiteredList;
                }
                var count = withoutfitereList.Where(x => x.IsAdminFlag == false).Count();
                if (count > 0)
                    objDirectoryDataAccess.UpdateNotificationCount(notificationRegisterTime, withoutfitereList, "AdminFlag");
            }
            else if (roleId == Convert.ToInt32(RoleType.Facilitator))
            {
                list = objDirectoryDataAccess.GetListOfNotification().Where(x => x.CapabilityId == Convert.ToInt32(HttpContext.Session["CapabilitiesId"]) && x.AdminDescripation != null && x.AdminDescripation != "").OrderByDescending(x => x.Id).ToList();
                withoutfitereList = list;
                foreach (var itm in list.Take(5))
                {
                    if (itm.RoleId.Split(',').Contains(Convert.ToInt32(RoleType.Facilitator).ToString()))
                        fiteredList.Add(new Notification { AdminDescripation = itm.AdminDescripation, Id = itm.Id, IsFacilitatorFlag = itm.IsFacilitatorFlag });
                    list = fiteredList;
                }
                var count = withoutfitereList.Where(x => x.IsFacilitatorFlag == false).Count();
                if (count > 0)
                    objDirectoryDataAccess.UpdateNotificationCount(notificationRegisterTime, withoutfitereList, "FacilitatorFlag");
            }
            else if (roleId == Convert.ToInt32(RoleType.User))
            {
                list = objDirectoryDataAccess.GetListOfNotification().Where(x => x.UserCode == Convert.ToString(HttpContext.Session["UserEmployeeId"]) && x.UserDescripation != null && x.UserDescripation != "").ToList().OrderByDescending(x => x.Id).ToList();
                withoutfitereList = list;
                foreach (var itm in list.Take(5))
                {
                    if (itm.RoleId.Split(',').Contains(Convert.ToInt32(RoleType.User).ToString()))
                        fiteredList.Add(new Notification { UserDescripation = itm.UserDescripation, Id = itm.Id, IsUserFlag = itm.IsUserFlag });
                    list = fiteredList;
                }
                var count = withoutfitereList.Where(x => x.IsUserFlag == false).Count();
                if (count > 0)
                    objDirectoryDataAccess.UpdateNotificationCount(notificationRegisterTime, withoutfitereList, "UserFlag");
            }
            else if (roleId == Convert.ToInt32(RoleType.Contributor))
            {
                list = objDirectoryDataAccess.GetListOfNotification().Where(x => x.UserCode == Convert.ToString(HttpContext.Session["UserEmployeeId"]) && x.UserDescripation != null && x.UserDescripation != "").ToList().OrderByDescending(x => x.Id).ToList();
                withoutfitereList = list;
                foreach (var itm in list.Take(5))
                {
                    if (itm.RoleId.Split(',').Contains(Convert.ToInt32(RoleType.User).ToString()))
                        fiteredList.Add(new Notification { UserDescripation = itm.UserDescripation, Id = itm.Id, IsUserFlag = itm.IsUserFlag });
                    list = fiteredList;
                }
                var count = withoutfitereList.Where(x => x.IsUserFlag == false).Count();
                if (count > 0)
                    objDirectoryDataAccess.UpdateNotificationCount(notificationRegisterTime, withoutfitereList, "UserFlag");
            }
            Session["LastUpdate"] = DateTime.Now;
            var genericResult = new { data = list, loggedRoleId = roleId };
            var result = this.Json(genericResult, JsonRequestBehavior.AllowGet);

            return result;
        }

        public ActionResult AllNotification()
        {
            List<Notification> list = new List<Notification>();
            List<Notification> fiteredList = new List<Notification>();
            int roleId = Convert.ToInt32(HttpContext.Session["RoleId"]);
            var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;

            if (roleId == Convert.ToInt32(RoleType.Admin))
            {
                list = objDirectoryDataAccess.GetListOfNotification().Where(x => x.CapabilityId == Convert.ToInt32(HttpContext.Session["CapabilitiesId"])).OrderByDescending(x => x.Id).ToList();
                foreach (var itm in list)
                {
                    if (itm.RoleId.Split(',').Contains(Convert.ToInt32(RoleType.Admin).ToString()))
                        fiteredList.Add(new Notification { AdminDescripation = itm.AdminDescripation, Id = itm.Id, IsAdminFlag = itm.IsAdminFlag, CreatedOn = itm.CreatedOn });
                    list = fiteredList;
                }
            }
            else if (roleId == Convert.ToInt32(RoleType.Facilitator))
            {
                list = objDirectoryDataAccess.GetListOfNotification().Where(x => x.CapabilityId == Convert.ToInt32(HttpContext.Session["CapabilitiesId"])).OrderByDescending(x => x.Id).ToList();
                foreach (var itm in list)
                {
                    if (itm.RoleId.Split(',').Contains(Convert.ToInt32(RoleType.Facilitator).ToString()))
                        fiteredList.Add(new Notification { AdminDescripation = itm.AdminDescripation, Id = itm.Id, IsFacilitatorFlag = itm.IsFacilitatorFlag, CreatedOn = itm.CreatedOn });
                    list = fiteredList;
                }
            }
            else if (roleId == Convert.ToInt32(RoleType.User))
            {
                list = objDirectoryDataAccess.GetListOfNotification().Where(x => x.UserCode == Convert.ToString(HttpContext.Session["UserEmployeeId"])).OrderByDescending(x => x.Id).ToList();
                foreach (var itm in list)
                {
                    if (itm.RoleId.Split(',').Contains(Convert.ToInt32(RoleType.User).ToString()))
                        fiteredList.Add(new Notification { UserDescripation = itm.UserDescripation, Id = itm.Id, IsUserFlag = itm.IsUserFlag, CreatedOn = itm.CreatedOn });
                    list = fiteredList;
                }
            }
            Session["LastUpdate"] = DateTime.Now;
            ViewBag.RoleId = roleId;

            return View(list);
        }

        public ActionResult Error()
        {
            return PartialView("~/Views/Shared/Error.cshtml");
        }
    }
}