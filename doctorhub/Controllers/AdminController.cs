using doctorhubBusinessEntities;
using doctorhubBusinessEntities.viewModels;
using doctorhubDataAccess;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace doctorhub.Controllers
{
    public class AdminController : Controller
    {
        private DirectoryDataAccess objDirectoryDataAccess;
        private CommonController objCommonController;

        public AdminController()
        {
            this.objDirectoryDataAccess = new DirectoryDataAccess();
            this.objCommonController = new CommonController();
        }

        [HttpGet]
        public ActionResult UserRegistration()
        {
            ViewBag.ModelroleList = objCommonController.BindRoleDropDownList();
            ViewBag.ModelBSList = objCommonController.BindBSDropDownList();
            ViewBag.ModelCAPList = objCommonController.BindCAPDropDownList();
            ViewBag.ModellobList = objCommonController.BindLOBDropDownList();

            string UserNTID = Session["UserNTID"].ToString();
            var userDetails = objDirectoryDataAccess.GetListOfRegisteredUser().Where(x => x.UserNTID == UserNTID).FirstOrDefault();
            if (userDetails != null && userDetails.IsActive == true && userDetails.Status == Convert.ToInt32(StatusType.Approve))
            {
                return RedirectToAction("index", "Home");
            }
            UserRegistrationModel Urm = new UserRegistrationModel();

            ViewBag.roleList = objCommonController.BindRoleDropDownList().Where(m => m.Text == "User");

            foreach (var Business in objDirectoryDataAccess.GetBusinessSegment().Where(f => f.IsActive == true))
            {
                Urm.BusinessSegment.Add(new SelectListItem { Text = Business.Name, Value = Business.Id.ToString() });
            }
            return View(Urm);
        }

        [HttpPost]
        public ActionResult UserRegistration([Bind(Exclude = "UserPhoto")]UserRegistrationModel model, int? BusinessSegmentId, int? CapabilitiesId, int? LineBusinessId)
        {
            UserRegistrationModel Urm = new UserRegistrationModel();
            ViewBag.roleList = objCommonController.BindRoleDropDownList().Where(m => m.Text == "User");
            foreach (var Business in objDirectoryDataAccess.GetBusinessSegment().Where(f => f.IsActive == true))
            {
                Urm.BusinessSegment.Add(new SelectListItem { Text = Business.Name, Value = Business.Id.ToString() });
            }
            if (BusinessSegmentId.HasValue)
            {
                foreach (var Capabiliti in objDirectoryDataAccess.GetCapabilities().Where(m => m.BsId == BusinessSegmentId && m.IsActive == true))
                {
                    Urm.Capabilities.Add(new SelectListItem { Text = Capabiliti.Name, Value = Capabiliti.Id.ToString() });
                }

                if (CapabilitiesId.HasValue)
                {
                    foreach (var Lbo in objDirectoryDataAccess.GetLob().Where(m => m.CapId == CapabilitiesId && m.IsActive == true))
                    {
                        Urm.LineOfBusiness.Add(new SelectListItem { Text = Lbo.Name, Value = Lbo.Id.ToString() });
                    }
                }
            }
            bool IsAdmin = false;
            try
            {
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    int msg = 0;
                    byte[] imageData = null;
                    string targetPath;
                    HttpPostedFileBase poImgFile = Request.Files["UserPhoto"];
                    if (poImgFile != null)
                    {
                        if (!string.IsNullOrWhiteSpace(poImgFile.FileName) && poImgFile.ContentLength > 0)
                        {
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
                                    imageData = System.IO.File.ReadAllBytes(targetPath);

                                }
                            }
                        }
                    }
                    if (model.Id == 0)
                    {
                        var userDetails = objDirectoryDataAccess.GetListOfRegisteredUser().Where(x => x.UserNTID == model.UserNTID).FirstOrDefault();
                        if (userDetails == null)
                        {
                            if (poImgFile.ContentLength == 0)
                            {
                                var imagPath = Server.MapPath("~/images/logo.jpg");
                                Image img = Image.FromFile(imagPath);
                                byte[] imageDatabytes = (byte[])(new ImageConverter()).ConvertTo(img, typeof(byte[]));
                                msg = objDirectoryDataAccess.SubmitUserRequest(null, imageDatabytes, model);
                            }
                            else
                            {
                                msg = objDirectoryDataAccess.SubmitUserRequest(poImgFile, imageData, model);
                            }
                        }

                        else if (userDetails != null && userDetails.IsActive == false)
                            msg = objDirectoryDataAccess.UpdateUserDetails(model, userDetails.Id);

                        else if (userDetails != null && userDetails.IsActive == true)
                            msg = -1; // random number

                        if (msg > 0)
                        {
                            objDirectoryDataAccess.SendEmail(model, "KMT1");
                            objDirectoryDataAccess.SendEmail(model, "KMT2");
                            TempData["registerSuccess"] = "User Is Registered Successfully";
                        }
                        else if (msg == 0)
                        {
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                        }
                        else if (msg == -1)
                        {
                            TempData["userExist"] = "User Already Exist.";
                        }
                    }
                    else
                    {
                        IsAdmin = true;
                        msg = objDirectoryDataAccess.UpdateUserDetails(model);
                        if (msg > 0)
                        {
                            objDirectoryDataAccess.SendEmail(model, "KMT4");
                            TempData["success"] = "User Details Updated Successfully";
                        }
                        else
                        {
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                        }
                    }
                }
                else
                {
                    var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (IsAdmin)
                return RedirectToAction("ManageUser", "Admin");
            else
                return View(Urm);
        }

        public ActionResult ManageUser()
        {
            string keyId = Request.QueryString["key"];
            ViewBag.roleList = objCommonController.BindRoleDropDownList();
            ViewBag.BSList = objCommonController.BindBSDropDownList();
            ViewBag.CAPList = objCommonController.BindCAPDropDownList();
            ViewBag.lobList = objCommonController.BindLOBDropDownList();

            if (!string.IsNullOrEmpty(keyId))
            {
                ViewBag.KeyId = keyId;
                var details = objDirectoryDataAccess.GetListOfRegisteredUser().Where(x => x.Id == Convert.ToInt32(keyId)).FirstOrDefault();
                if (details != null) { ViewBag.UserName = details.UserName; }
                return View(details);
            }
            return View();
        }

        [HttpGet]
        public ActionResult ManageFeedback()
        {
            return View();
        }

        public JsonResult GetManageUser()
        {
            var list = objDirectoryDataAccess.GetManageUser();
            return new JsonResult { Data = list, MaxJsonLength = Int32.MaxValue };
        }

        public JsonResult UpdateUserRequestStatus(int UserId, string Status)
        {
            int count = objDirectoryDataAccess.UpdateUserRequestStatus(UserId, Status);
            if (count > 0)
            {
                UserRegistrationModel model = objDirectoryDataAccess.GetListOfRegisteredUser().Where(x => x.Id == UserId).FirstOrDefault();
                if (Status == "Approve")
                    objDirectoryDataAccess.SendEmail(model, "KMT3");
                else
                    objDirectoryDataAccess.SendEmail(model, "KMT26");
            }
            return Json(count, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateUserRequestStatus(UserRegistrationModel model)
        {
            return Json(objDirectoryDataAccess.CreateUserProfile(model), JsonRequestBehavior.AllowGet);

        }

        public JsonResult UpdateBulkRequestStatus(string[] RequestIds)
        {
            return Json(objDirectoryDataAccess.UpdateBulkUserRequestStatus(RequestIds), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateBulkRequestStatusReject(string[] RequestIds)
        {
            return Json(objDirectoryDataAccess.UpdateBulkUserRequestStatusReject(RequestIds), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserFeedbackDetails(string Status)
        {
            var list = objDirectoryDataAccess.GetListOfAdminFeedback(Status);
            return new JsonResult { Data = list, MaxJsonLength = Int32.MaxValue };
        }

        public JsonResult SubmitAdminReply(string[] feedbackIds)
        {
            return Json(objDirectoryDataAccess.UpdateAdminReply(feedbackIds), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDocs()
        {
            var list = objDirectoryDataAccess.GetListOfUploadDoc().Where(m => m.Status == 1);
            return new JsonResult { Data = list, MaxJsonLength = Int32.MaxValue };
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

        public ActionResult UpdateBlog()
        {
            string keyId = Request.QueryString["key"];
            if (!string.IsNullOrEmpty(keyId))
            {
                ViewBag.KeyId = keyId;
                var details = objDirectoryDataAccess.GetBlogList().Where(x => x.BlogId == Convert.ToInt32(keyId)).FirstOrDefault();
                return View(details);
            }
            return View();
        }

        public ActionResult MyBlog(int Id)
        {

            var details = objDirectoryDataAccess.GetBlogList().Where(x => x.BlogId == Convert.ToInt32(Id)).FirstOrDefault();
            if (details == null)
            {
                return HttpNotFound();
            }
            Blog getfetcheddata = new Blog();
            return PartialView(@"~/Views/MyProfile/_myModal.cshtml", details);

        }

        public ActionResult CreateNewBlog()
        {
            // return PartialView("~/Views/MyProfile/_CreateBlog.cshtml");
            return View();

        }

        public ActionResult Troublelogging()
        {
            ViewBag.ModelroleList = objCommonController.BindRoleDropDownList();
            ViewBag.ModelBSList = objCommonController.BindBSDropDownList();
            ViewBag.ModelCAPList = objCommonController.BindCAPDropDownList();
            ViewBag.ModellobList = objCommonController.BindLOBDropDownList();
            return PartialView("~/Views/Shared/_Troublelogging.cshtml");

        }
        public ActionResult DeleteBlog()
        {
            string keyId = Request.QueryString["key"];
            objDirectoryDataAccess.DeleteBlogPost(int.Parse(keyId));
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ApproveUploadDoc()
        {
            return View();
        }

        public JsonResult GetApproveUploadDocDetails(string Status)
        {
            var list = objDirectoryDataAccess.GetListOfUploadDoc().Where(m => m.Status == int.Parse(Status.ToString()));
            return new JsonResult { Data = list, MaxJsonLength = Int32.MaxValue };
        }

        public JsonResult ApproveDocument(string[] Id)
        {
            return Json(objDirectoryDataAccess.UpdateUploaddocStatus(Id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApproveBlogs()
        {
            return View();
        }

        public JsonResult GetApproveBlosDetails(string Status)
        {
            var list = objDirectoryDataAccess.GetBlogList().Where(m => m.Status == int.Parse(Status.ToString()));
            return new JsonResult { Data = list, MaxJsonLength = Int32.MaxValue };
        }

        public JsonResult ApproveBlosStatus(string[] Id)
        {
            return Json(objDirectoryDataAccess.UpdateBlogStatus(Id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApproveImage()
        {
            return View();
        }

        public JsonResult GetApproveImageDetails(string Status)
        {

            var list = objDirectoryDataAccess.GetProfileImgList().Where(m => m.ImgStatus == int.Parse(Status.ToString()));
            return new JsonResult { Data = list, MaxJsonLength = Int32.MaxValue };
        }

        public JsonResult ApproveImgStatus(string[] Id, string status)
        {
            return Json(objDirectoryDataAccess.UpdateImgStatus(Id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApprovePost()
        {
            return View();
        }

        public JsonResult GetApprovePostDetails(string Status)
        {
            var list = objDirectoryDataAccess.GetListOfUserFlagPost().Where(m => m.Status == Status.ToString());
            return new JsonResult { Data = list, MaxJsonLength = Int32.MaxValue };
        }

        public JsonResult DeleteFlagPost(string[] Id)
        {
            return Json(objDirectoryDataAccess.DeleteFlagpost(Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UnFlagPost(string[] Id)
        {
            return Json(objDirectoryDataAccess.UnFlagPost(Id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeletePost()
        {
            int keyId = int.Parse(Request.QueryString["keyId"]);
            string Identifier = Request.QueryString["Identifier"];
            try
            {
                int msg = 0;
                msg = objDirectoryDataAccess.DeletPost(keyId, Identifier);
                if (msg > 0)
                    TempData["success"] = "Post Deleted";
                else
                    TempData["error"] = "Some Error Occured. Please Contact Admin";
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                ViewBag.DocList = objCommonController.BindDocCategoryList();
                ViewBag.UpladDocBlogList_detail = objDirectoryDataAccess.GetListOfUploadDoc();
                return PartialView("~/Views/MyProfile/_Document.cshtml");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult PostLikes()
        {
            int keyId = int.Parse(Request.QueryString["key"]);
            string Identifier = Request.QueryString["Identifier"];
            try
            {
                int msg = 0;
                msg = objDirectoryDataAccess.SubmitLike(keyId, Identifier);
                if (msg > 0)
                    TempData["success"] = "Post Liked";
                else
                    TempData["error"] = "Some Error Occured. Please Contact Admin";

            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                ViewBag.DocList = objCommonController.BindDocCategoryList();
                ViewBag.UpladDocBlogList_detail = objDirectoryDataAccess.GetListOfUploadDoc();
                return PartialView("~/Views/MyProfile/_Document.cshtml");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult FlagPost()
        {
            string keyId = Request.QueryString["key"];
            string Id = Request.QueryString["Id"].PadLeft(9, '0');
            string Identifier = Request.QueryString["Identifier"];
            try
            {
                int msg = 0;
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (Identifier == "POST")
            {
                ViewBag.PostList = objDirectoryDataAccess.GetPost();
                return PartialView("~/Views/MyProfile/_MyPostAndComment.cshtml");
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Get Business Segment
        /// </summary>
        /// <returns></returns>
        public JsonResult GetBusinessSegment()
        {
            return Json(objDirectoryDataAccess.GetBusinessSegment(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageBusinessSegment()
        {
            string keyId = Request.QueryString["key"];

            if (!string.IsNullOrEmpty(keyId))
            {
                ViewBag.KeyId = keyId;
                var details = objDirectoryDataAccess.GetBusinessSegment().Where(x => x.Id == Convert.ToInt32(keyId)).FirstOrDefault();
                return View(details);
            }

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ManageBusinessSegment(BusinessSegmentModel model)
        {
            try
            {
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    int msg = 0;
                    if (model.Id == 0)
                    {
                        msg = objDirectoryDataAccess.SubmitBusinessSegment(model);
                        if (msg > 0)
                            TempData["success"] = "Business Segment Added Successfully";
                        else
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                    }
                    else
                    {
                        msg = objDirectoryDataAccess.UpdateBusinessSegment(model);
                        if (msg > 0)
                            TempData["success"] = "Business Segment Updated Successfully";
                        else
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ModelState.Clear();
            return RedirectToAction("ManageBusinessSegment");
        }

        /// <summary>
        /// Manage Capabilities
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCapabilities()
        {
            return Json(objDirectoryDataAccess.GetCapabilities(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageCapabilities()
        {
            ViewBag.BSList = objCommonController.BindBSDropDownList();
            string keyId = Request.QueryString["key"];

            if (!string.IsNullOrEmpty(keyId))
            {
                ViewBag.KeyId = keyId;
                var details = objDirectoryDataAccess.GetCapabilities().Where(x => x.Id == Convert.ToInt32(keyId)).FirstOrDefault();
                return View(details);
            }

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ManageCapabilities(CapabilitiesModel model)
        {
            try
            {
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    int msg = 0;
                    if (model.Id == 0)
                    {
                        msg = objDirectoryDataAccess.SubmitCapabilities(model);
                        if (msg > 0)
                            TempData["success"] = "Capability Added Successfully";
                        else
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                    }
                    else
                    {
                        msg = objDirectoryDataAccess.UpdateCapabilities(model);
                        if (msg > 0)
                            TempData["success"] = "Capability Updated Successfully";
                        else
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ModelState.Clear();
            return RedirectToAction("ManageCapabilities");
        }

        /// <summary>//
        /// Manage LOB test
        /// </summary>
        /// <returns></returns>
        public JsonResult GetLineofBusiness()
        {
            return Json(objDirectoryDataAccess.GetLob(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageLineofBusiness()
        {
            ViewBag.CAPList = objCommonController.BindCAPDropDownList();
            string keyId = Request.QueryString["key"];

            if (!string.IsNullOrEmpty(keyId))
            {
                ViewBag.KeyId = keyId;
                var details = objDirectoryDataAccess.GetLob().Where(x => x.Id == Convert.ToInt32(keyId)).FirstOrDefault();
                return View(details);
            }

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ManageLineofBusiness(LobModel model)
        {
            try
            {
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    int msg = 0;
                    if (model.Id == 0)
                    {
                        msg = objDirectoryDataAccess.SubmitLob(model);
                        if (msg > 0)
                            TempData["success"] = "LOB Added Successfully";
                        else
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                    }
                    else
                    {
                        msg = objDirectoryDataAccess.UpdateLob(model);
                        if (msg > 0)
                            TempData["success"] = "LOB Updated Successfully";
                        else
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ModelState.Clear();
            return RedirectToAction("ManageLineofBusiness");
        }

        #region Code written by shivnandan update
        public ActionResult ManagePoll()
        {
            try
            {
                int keyId = Convert.ToInt32(Request.QueryString["key"]);

                if (keyId > 0)
                {
                    var model = objDirectoryDataAccess.GetManagePollsByID(keyId);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }

        //[HttpPost]
        //public ActionResult ManagePoll(ManagePoll model)
        //{
        //    try
        //    {
        //        var ManagePoll = objDirectoryDataAccess.GetManagePolls();
        //        List<Poll> ManageVote = objDirectoryDataAccess.GetUserVoteResultCount(model.Questions);
        //        string ModelFromDate = Convert.ToDateTime(model.FromDate).ToString("MM/dd/yyyy");
        //        string FromDate = Convert.ToDateTime(ManagePoll.Select(x => x.FromDate).FirstOrDefault()).ToString("MM/dd/yyyy");
        //        string ToDate = Convert.ToDateTime(ManagePoll.Select(x => x.ToDate).FirstOrDefault()).ToString("MM/dd/yyyy");
        //        int msg = 0;
        //        if (model != null && model.PollID == 0)
        //        {
        //            if (Convert.ToDateTime(ToDate) >= Convert.ToDateTime(ModelFromDate) && model.IsActive == true)
        //            {
        //                TempData["error"] = "Question is already there between. " + FromDate + " - " + ToDate;
        //            }
        //            else
        //            {
        //                msg = objDirectoryDataAccess.SubmitPoll(model);
        //                if (msg > 0)
        //                {
        //                    objDirectoryDataAccess.SendEmail(Convert.ToInt32(HttpContext.Session["RoleId"]), "KMT20");
        //                    TempData["success"] = "User poll has been created successfully.";
        //                }
        //                else
        //                {
        //                    TempData["error"] = "Some Error Occured. Please Contact Admin";
        //                }
        //            }
        //        }
        //        else if (model.PollID > 0)
        //        {
        //            if (ManageVote.Count > 0 && model.IsActive == true)
        //            {
        //                TempData["message"] = "You can't update poll because voting has started.";
        //            }
        //            else
        //            {
        //                msg = objDirectoryDataAccess.SubmitPoll(model);
        //                if (msg > 0)
        //                {
        //                    objDirectoryDataAccess.SendEmail(Convert.ToInt32(HttpContext.Session["RoleId"]), "KMT21");
        //                    TempData["success"] = "User poll has been updated successfully.";
        //                }
        //                else
        //                {
        //                    TempData["error"] = "Some Error Occured. Please Contact Admin";
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return RedirectToAction("ManagePoll");
        //}

        public JsonResult ManagePollDetails()
        {
            try
            {
                var pollList = objDirectoryDataAccess.GetManagePolls();

                return new JsonResult { Data = pollList, MaxJsonLength = Int32.MaxValue };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult DeleteGridItem(int PollID)
        {
            try
            {
                if (PollID > 0)
                {
                    objDirectoryDataAccess.DeleteManagePoll(PollID);
                    return RedirectToAction("ManagePoll");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }
        #endregion

        public JsonResult GetDocumentCategory()
        {
            return Json(objDirectoryDataAccess.GetDocumentCategory(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageDocumentCategory()
        {
            ViewBag.DCList = objCommonController.BindDocCategoryList();
            string keyId = Request.QueryString["key"];

            if (!string.IsNullOrEmpty(keyId))
            {
                ViewBag.KeyId = keyId;
                var details = objDirectoryDataAccess.GetDocumentCategory().Where(x => x.ID == Convert.ToInt32(keyId)).FirstOrDefault();
                return View(details);
            }

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ManageDocumentCategory(UploadDocument model)
        {
            try
            {
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    int msg = 0;
                    if (model.ID == 0)
                    {
                        msg = objDirectoryDataAccess.SubmitDocumentCategory(model);
                        if (msg > 0)
                            TempData["success"] = "Document Category Added successfully";
                        else
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                    }
                    else
                    {
                        msg = objDirectoryDataAccess.SubmitDocumentCategory(model);
                        if (msg > 0)
                            TempData["success"] = "Document Category Updated Successfully";
                        else
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ModelState.Clear();
            return RedirectToAction("ManageDocumentCategory");
        }

        /// <summary>
        /// Manage FAQ
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFAQ()
        {
            return Json(objDirectoryDataAccess.GetFaqList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageFaq()
        {
            string keyId = Request.QueryString["key"];

            if (!string.IsNullOrEmpty(keyId))
            {
                ViewBag.KeyId = keyId;
                var details = objDirectoryDataAccess.GetFaqList().Where(x => x.Id == Convert.ToInt32(keyId)).FirstOrDefault();
                return View(details);
            }
            return View();

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ManageFaq(FaqModel model)
        {
            try
            {
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    int msg = 0;
                    if (model.Id == 0)
                    {
                        msg = objDirectoryDataAccess.FaqUpdateAndSubmit(model);
                        if (msg > 0)
                            TempData["success"] = "FAQ Added Successfully";
                        else
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                    }
                    else
                    {
                        msg = objDirectoryDataAccess.FaqUpdateAndSubmit(model);
                        if (msg > 0)
                            TempData["success"] = "FAQ Updated Successfully";
                        else
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ModelState.Clear();
            return RedirectToAction("ManageFaq");
        }


        /// <summary>
        /// Manage Training
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTraining()
        {
            return Json(objDirectoryDataAccess.GetTrainingLinkList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageTraining()
        {
            string keyId = Request.QueryString["key"];

            if (!string.IsNullOrEmpty(keyId))
            {
                ViewBag.KeyId = keyId;
                var details = objDirectoryDataAccess.GetTrainingLinkList().Where(x => x.TrainingId == Convert.ToInt32(keyId)).FirstOrDefault();
                return View(details);
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SubmitTraining(Training Model)
        {
            try
            {
                int msg = 0;
                if (Model != null && Model.TrainingId == 0)
                {
                    msg = objDirectoryDataAccess.TrainingUpdateAndSubmit(Model);
                    if (msg > 0)
                    {
                        objDirectoryDataAccess.SendEmail(Convert.ToInt32(HttpContext.Session["RoleId"]), "KMT22");
                        TempData["success"] = "Training Created Successfully.";
                    }
                    else
                        TempData["error"] = "Some Error Occured. Please Contact Admin";
                }
                else if (Model.TrainingId > 0)
                {
                    msg = objDirectoryDataAccess.TrainingUpdateAndSubmit(Model);
                    if (msg > 0)
                    {
                        objDirectoryDataAccess.SendEmail(Convert.ToInt32(HttpContext.Session["RoleId"]), "KMT23");
                        TempData["success"] = "Training Updated Successfully.";
                    }
                    else
                        TempData["error"] = "Some Error Occured. Please Contact Admin";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("ManageTraining");
        }

        /// <summary>
        /// Manage Capabilities
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSystemLink()
        {
            return Json(objDirectoryDataAccess.GetSystemLinkList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageSystemLink()
        {
            string keyId = Request.QueryString["key"];

            if (!string.IsNullOrEmpty(keyId))
            {
                ViewBag.KeyId = keyId;
                var details = objDirectoryDataAccess.GetSystemLinkList().Where(x => x.Id == Convert.ToInt32(keyId)).FirstOrDefault();
                return View(details);
            }
            return View();

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ManageSystemLink(SystemLinkModel model)
        {
            try
            {
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    int msg = 0;
                    if (model.Id == 0)
                    {
                        msg = objDirectoryDataAccess.SystemLinkUpdateAndSubmit(model);
                        if (msg > 0)
                            TempData["success"] = "System Link Added Successfully";
                        else
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                    }
                    else
                    {
                        msg = objDirectoryDataAccess.SystemLinkUpdateAndSubmit(model);
                        if (msg > 0)
                            TempData["success"] = "System Link Updated Successfully";
                        else
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ModelState.Clear();
            return RedirectToAction("ManageSystemLink");
        }

        /// <summary>
        /// Manage BulletinBoard
        /// </summary>
        /// <returns></returns>
        public JsonResult GetBulletinBoard()
        {
            return Json(objDirectoryDataAccess.GetListOfBulletin(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageBulletinBoard()
        {
            try
            {
                int keyId = Convert.ToInt32(Request.QueryString["key"]);

                if (keyId > 0)
                {
                    var model = objDirectoryDataAccess.GetListOfBulletin().Where(x => x.Id == Convert.ToInt32(keyId)).FirstOrDefault();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }

        [HttpPost]
        public ActionResult ManageBulletinBoard(HttpPostedFileBase FileUpload1, BulletinBoard Upd)
        {
            int msg = 0;
            ModelState.Remove("Id");
            if (FileUpload1 == null)
            {
                TempData["error"] = "Please Select Banner For This Operation ";
                return RedirectToAction("ManageBulletinBoard");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (Upd.Id == 0)
                    {
                        string FilePath = Server.MapPath("~/SliderImages");
                        string _FileName = Path.GetFileName(FileUpload1.FileName);
                        string _path = Path.Combine(FilePath, _FileName);
                        if (!System.IO.Directory.Exists(FilePath))
                        {
                            System.IO.Directory.CreateDirectory(FilePath);
                        }
                        FileUpload1.SaveAs(_path);
                        if (FileUpload1.ContentLength > 0)
                            msg = objDirectoryDataAccess.SubmitBulletin(FileUpload1, Upd);
                        else if (FileUpload1 != null && Upd.Id == 0)
                            msg = objDirectoryDataAccess.SubmitBulletin(FileUpload1, Upd);
                        if (msg > 0)
                            TempData["success"] = "Bulletin Board Created Successfully";
                        else if (msg == 0)
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                    }
                    else
                    {
                        string FilePath = Server.MapPath("~/SliderImages");
                        string _FileName = Path.GetFileName(FileUpload1.FileName);
                        string _path = Path.Combine(FilePath, _FileName);
                        if (!System.IO.Directory.Exists(FilePath))
                        {
                            System.IO.Directory.CreateDirectory(FilePath);
                        }
                        FileUpload1.SaveAs(_path);
                        if (FileUpload1.ContentLength > 0)
                            msg = objDirectoryDataAccess.SubmitBulletin(FileUpload1, Upd);
                        if (msg > 0)
                            TempData["success"] = "Bulletin Board Updated Successfully";
                        else
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return RedirectToAction("ManageBulletinBoard");
        }

        [HttpPost]
        public ActionResult UploadDoc(string message, UploadDocument Upd, string title)
        {

            //HttpPostedFileBase FileUpload1 = Request.Files[0];
            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase FileUpload1 = files[0];

            int msg = 0;
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                try
                {
                    if (FileUpload1.ContentLength > 0)
                    {
                        string FilePath = Server.MapPath("~/UploadDocument");
                        string _FileName = Path.GetFileName(FileUpload1.FileName);
                        string _path = Path.Combine(FilePath, _FileName);
                        if (!System.IO.Directory.Exists(FilePath))
                        {
                            System.IO.Directory.CreateDirectory(FilePath);
                        }
                        FileUpload1.SaveAs(_path);
                        if (FileUpload1.ContentLength > 0)
                            msg = objDirectoryDataAccess.UploadDocument(FileUpload1, message, Upd);
                        else if (FileUpload1 != null && Upd.Status == 0)
                            msg = objDirectoryDataAccess.UploadDocument(FileUpload1, message, Upd);
                        if (msg > 0)
                        {
                            UserRegistrationModel model = objDirectoryDataAccess.GetListOfRegisteredUser().Where(x => x.UserCode == Convert.ToString(HttpContext.Session["UserEmployeeId"])).FirstOrDefault();
                            objDirectoryDataAccess.SendEmail(model, "KMT12");
                            objDirectoryDataAccess.SendEmail(model, "KMT13");
                            TempData["Success"] = "Your Documnet has been Uploaded Successfully. It will be available after approval by Admin";
                        }
                        else if (msg == 0)
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                    }
                    else
                    {
                        TempData["error"] = "Please Select Document File";
                        return RedirectToAction("index", "Home");
                        // return Json("Uploaded " + Request.Files.Count + " files");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            ModelState.Clear();
            ViewBag.DocList = objCommonController.BindDocCategoryList();
            ViewBag.UpladDocBlogList_detail = objDirectoryDataAccess.GetListOfUploadDoc();
            return PartialView("~/Views/MyProfile/_Document.cshtml");
            // return Json(TempData["Success"]);
            // return RedirectToAction("index", "Home");
            //return Json("Uploaded " + Request.Files.Count + " files");
        }

        /// <summary>
        /// Manage ManageRSS Link
        /// </summary>
        /// <returns></returns>
        public JsonResult GetRSS()
        {
            return Json(objDirectoryDataAccess.GetRssFeedList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageRSS()
        {
            string keyId = Request.QueryString["key"];


            if (!string.IsNullOrEmpty(keyId))
            {
                ViewBag.KeyId = keyId;
                var details = objDirectoryDataAccess.GetRssFeedList().Where(x => x.Id == Convert.ToInt32(keyId)).FirstOrDefault();
                return View(details);
            }
            return View();

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ManageRSS(RSSFeed model)
        {
            try
            {
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    int msg = 0;
                    if (model.Id == 0)
                    {
                        msg = objDirectoryDataAccess.RssFeedUpdateAndSubmit(model);
                        if (msg > 0)
                            TempData["success"] = "RssFeed URL Added Successfully";
                        else
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                    }
                    else
                    {
                        msg = objDirectoryDataAccess.RssFeedUpdateAndSubmit(model);
                        if (msg > 0)
                            TempData["success"] = "RssFeed URL Updated Successfully";
                        else
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ModelState.Clear();
            return RedirectToAction("ManageRSS");
        }

        [HttpGet]
        public ActionResult SelectQuiz(QuizVM quiz)
        {
            string QuizDId = objDirectoryDataAccess.Decrypt(quiz.Assessment);
            var QuizList = objDirectoryDataAccess.GetQuiz().Where(q => q.QuizID == int.Parse(QuizDId)).FirstOrDefault();
            quiz.QuizID = QuizList.QuizID;
            quiz.QuizName = QuizList.QuizName;
            ViewBag.CreatedByMailId = QuizList.CreatedByMailId;
            return View(quiz);

        }

            // [HttpPost]
            //public ActionResult Quiz(List<QuizAnswersVM> resultQuiz, string mailId)
            //{
            //    List<QuizAnswersVM> finalResultQuiz = new List<QuizAnswersVM>();
            //    if (resultQuiz != null)
            //    {
            //        foreach (QuizAnswersVM answser in resultQuiz)
            //        {
            //            if (answser.AnswerQ != null)
            //            {
            //                QuizAnswersVM result = objDirectoryDataAccess.GetQuizAnswer().Where(x => x.QuestionID == answser.QuestionID).Select(a => new QuizAnswersVM
            //                {
            //                    QuestionID = a.QuestionID,
            //                    AnswerQ = a.AnswerText,
            //                    isCorrect = (answser.AnswerQ.ToLower().Equals(a.AnswerText.ToLower()))
            //                }).FirstOrDefault();
            //                finalResultQuiz.Add(result);
            //            }
            //            else
            //            {
            //                TempData["error"] = "Some Error Occured. Please Contact Admin";
            //                return RedirectToAction("Admin", "SelectQuiz");
            //            }
            //        }
            //        int correct = finalResultQuiz.Where(a => a.isCorrect == true).Count();
            //        int totalCorrect = (correct * 100) / finalResultQuiz.Count();

            //        int msg = objDirectoryDataAccess.SubmitAssessment(resultQuiz[0].QuizID, totalCorrect);

            //        if (msg > 0)
            //        {
            //            UserRegistrationModel model = objDirectoryDataAccess.GetListOfRegisteredUser().Where(x => x.UserCode == Convert.ToString(HttpContext.Session["UserEmployeeId"])).FirstOrDefault();
            //            objDirectoryDataAccess.SendEmail(model, totalCorrect.ToString(), "KMT15");
            //            objDirectoryDataAccess.SendEmail(model, mailId, "KMT16");
            //        }

            //        return Json(new { result = finalResultQuiz, totalCorrect }, JsonRequestBehavior.AllowGet);
            //    }
            //    else
            //    {
            //        return RedirectToAction("Admin", "SelectQuiz");
            //    }
            //}

            /// <summary>
            /// Manage BulletinBoard
            /// </summary>
            /// <returns></returns>
        public JsonResult GetBadge()
        {
            return Json(objDirectoryDataAccess.GetListOfBadge(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageBadge()
        {
            try
            {
                int keyId = Convert.ToInt32(Request.QueryString["key"]);

                if (keyId > 0)
                {
                    var model = objDirectoryDataAccess.GetListOfBadge().Where(x => x.Id == Convert.ToInt32(keyId)).FirstOrDefault();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }

        [HttpPost]
        public ActionResult ManageBadge([Bind(Exclude = "BadgeImage")]BadgeModel model)
        {
            try
            {
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    int msg = 0;
                    byte[] imageData = null;
                    string targetPath;
                    HttpPostedFileBase poImgFile = Request.Files["BadgeImage"];
                    if (poImgFile != null)
                    {
                        Stream strm = poImgFile.InputStream;
                        if (Request.Files.Count > 0)
                        {
                            using (var image = System.Drawing.Image.FromStream(strm))
                            {
                                // Get Original Size of file (Height or Width)   
                                string imagesze = image.Size.ToString();
                                int newWidth = 50; // New Width of Image in Pixel  
                                int newHeight = 50; // New Height of Image in Pixel 
                                var thumbImg = new Bitmap(newWidth, newHeight);
                                var thumbGraph = Graphics.FromImage(thumbImg);
                                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                                thumbGraph.DrawImage(image, imgRectangle);
                                // Save the file  
                                targetPath = Server.MapPath(@"~\Images\UserBadge\") + poImgFile.FileName.Split('\\').Last();
                                thumbImg.Save(targetPath, image.RawFormat);
                                //int thumWidth = 35; // New Width of thumImage in Pixel  
                                //int thumHeight = 35; // New Height of thumImage in Pixel
                                //var newthumbImg = new Bitmap(thumWidth, thumHeight);
                                //string targetthumPath = Server.MapPath(@"~\Images\profileimages\")+"thum" + poImgFile.FileName.Split('\\').Last();
                                //newthumbImg.Save(targetthumPath, image.RawFormat);
                                imageData = System.IO.File.ReadAllBytes(targetPath);

                            }
                        }
                    }
                    if (model.Id == 0)
                    {
                        msg = objDirectoryDataAccess.SubmitBadge(model, imageData);
                        if (msg > 0)
                            TempData["success"] = "Badge Added Successfully";
                        else
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                    }
                    else
                    {
                        msg = objDirectoryDataAccess.SubmitBadge(model, imageData);
                        if (msg > 0)
                            TempData["success"] = "Badge Updated Successfully";
                        else
                            TempData["error"] = "Some Error Occured. Please Contact Admin";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("ManageBadge");
        }

        [HttpGet]
        public ActionResult Follower1(string Id)
        {
            string[] status = Id.Split('_');
            List<Followers> listOfFollowers = new List<Followers>();

            if (status[1] == "Following")
            {
                listOfFollowers = objDirectoryDataAccess.GetListFollower(status[0]);
                ViewBag.listOfFollowers = listOfFollowers.Where(m => m.status == "Following");
            }
            else if (status[1] == "Follower")
            {
                listOfFollowers = objDirectoryDataAccess.GetListFollower(status[0]);
                ViewBag.listOfFollowers = listOfFollowers.Where(m => m.status == "Follower"); ;
            }

            if (listOfFollowers == null)
            {
                return HttpNotFound();
            }
            Followers getfetcheddata = new Followers();
            return PartialView(@"~/Views/MyProfile/_MyModalFollow.cshtml", listOfFollowers);

        }

        public ActionResult Following(string type, string UserId)
        {
            try
            {
                int msg = 0;
                msg = objDirectoryDataAccess.UpdateFollower(type, HttpContext.Session["ID"].ToString(), UserId);
                if (msg > 0)
                    TempData["Success"] = "You Followed " + HttpContext.Session["FullName"].ToString();
                else if (msg == 0)
                    TempData["error"] = "Some Error Occured. Please Contact Admin";

            }
            catch
            {
                return RedirectToAction("index", "Home");
            }
            return RedirectToAction("index", "Home");
        }

        /// <summary>
        /// Reports
        /// </summary>
        /// <returns></returns>
        public ActionResult Reports()
        {
            try
            {
                ViewBag.ReportsTypeList = objCommonController.BindReportsDropDownList();
                ViewBag.FilterCriteriaList = objCommonController.BindFilterCriteriaList();
                //if (Session["Adminstrator"].ToString() == "True")
                ViewBag.CAPList = objCommonController.BindCAPDropDownList();

            }
            catch (Exception ex)
            {
                TempData["error"] = "Some Error Occured. Please Contact Admin";
                objDirectoryDataAccess.SaveErrorLog(this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name, Server.GetLastError().ToString(), HttpContext.Session["UserNTID"].ToString());
            }
            return View();
        }

        //public JsonResult GetUtilizationReport(int ReportId, string StartDate, string EndDate, string Criteria, string Capabilities, string CapText)
        //{
        //    return Json(objDirectoryDataAccess.GetUtilizationReport(ReportId, StartDate, EndDate, Criteria, Capabilities, CapText), JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        public JsonResult SubmitErrorLogin(FormCollection formCollection)
        {
            bool isMailSent = false;
            string ddlBS = formCollection["ddlBS"];
            string ddlCAP = formCollection["ddlCAP"];
            string ddlLOB = formCollection["ddlLOB"];
            string ddlRole = formCollection["ddlRole"];
            string txtPost = formCollection["txtPost"];

            try
            {
                {
                    string UserCode = HttpContext.Session["UserEmployeeId"].ToString();
                    string UserEmailId = HttpContext.Session["UserEmailId"].ToString();
                    string UserName = HttpContext.Session["FullName"].ToString();
                    //  UserRegistrationModel userModel = objDirectoryDataAccess.GetListOfRegisteredUser().Where(x => x.UserCode == UserCode).FirstOrDefault();
                    isMailSent = objDirectoryDataAccess.SendEmail(UserEmailId, UserCode, UserName, "KMT28", txtPost, int.Parse(ddlCAP));
                    isMailSent = objDirectoryDataAccess.SendEmail(UserEmailId, UserCode, UserName, "KMT29", txtPost, int.Parse(ddlCAP));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new JsonResult { Data = isMailSent, MaxJsonLength = Int32.MaxValue };
        }

        public ActionResult AssessmentResult()
        {
            try
            {
                ViewBag.CAPList = objCommonController.BindCAPDropDownList();
                List<SelectListItem> Quiz = new List<SelectListItem>();
                var QuizList = objDirectoryDataAccess.GetQuiz().Select(l => new { l.QuizID, l.QuizName });
                foreach (var item in QuizList)
                {
                    Quiz.Add(new SelectListItem
                    {
                        Text = item.QuizName,
                        Value = item.QuizID.ToString(),
                    });
                }
                ViewBag.BSList = Quiz.ToList();

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.ToString() + " Error Occured. Please Contact Admin";
                objDirectoryDataAccess.SaveErrorLog(this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name, Server.GetLastError().ToString(), HttpContext.Session["UserNTID"].ToString());
            }
            return View();
        }

        //public JsonResult GetAssessmentResult()
        //{
        //    return Json(objDirectoryDataAccess.GetAssessmentResult(), JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetSelectAssessmentResult(string QuizNameText)
        //{
        //    if (QuizNameText != "- All -")
        //    {
        //        return Json(objDirectoryDataAccess.GetAssessmentResult().Where(m => m.QuizName == QuizNameText), JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //        return Json(objDirectoryDataAccess.GetAssessmentResult(), JsonRequestBehavior.AllowGet);
        //}
    }
}