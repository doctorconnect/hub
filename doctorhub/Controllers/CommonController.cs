using doctorhubBusinessEntities;
using doctorhubDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
//using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace doctorhub.Controllers
{
    public class CommonController : Controller
    {
        private ActiveDirectoryUser objActiveDirectoryUser;
        private DirectoryDataAccess objDirectoryDataAccess;

        public CommonController()
        {
            this.objActiveDirectoryUser = new ActiveDirectoryUser();
            this.objDirectoryDataAccess = new DirectoryDataAccess();
        }

        public ActionResult Index()
        {
            return View();
        }

        //public JsonResult GetActiveDirectoryUserDetail(string searchParameter)
        //{
        //    UserRegistrationModel objUserRegistrationModel = new UserRegistrationModel();
        //    try
        //    {
        //        var UserDetails = objActiveDirectoryUser.GetActiveDirectiryUserDetail(searchParameter);

        //        objUserRegistrationModel.UserCode = UserDetails.EmployeeId;
        //        objUserRegistrationModel.UserNTID = UserDetails.DomainUserId;
        //        objUserRegistrationModel.UserName = UserDetails.DisplayName;
        //        objUserRegistrationModel.UserEmail = UserDetails.Email;

        //        objUserRegistrationModel.ManagerCode = UserDetails.Manager.EmployeeId;
        //        objUserRegistrationModel.ManagerNTID = UserDetails.Manager.DomainUserId;
        //        objUserRegistrationModel.ManagerName = UserDetails.Manager.DisplayName;
        //        objUserRegistrationModel.ManagerEmail = UserDetails.Manager.Email;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return Json(objUserRegistrationModel, JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// Bind Dropdownlist of  
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> BindLOBDropDownList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                var lobList = objDirectoryDataAccess.GetLob().Select(l => new { l.Id, l.Name });

                foreach (var item in lobList)
                {
                    items.Add(new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }

        /// <summary>
        /// Bind Dropdownlist of Role 
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> BindRoleDropDownList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                var roleList = objDirectoryDataAccess.GetGenericDetails("Role");

                foreach (var item in roleList)
                {
                    items.Add(new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }

        /// <summary>
        /// Bind Dropdownlist of Business Segment 
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> BindBSDropDownList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {

                var BSList = objDirectoryDataAccess.GetBusinessSegment().Select(l => new { l.Id, l.Name });

                foreach (var item in BSList)
                {
                    items.Add(new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }

        public List<SelectListItem> BindCAPDropDownList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                var CAPList = objDirectoryDataAccess.GetCapabilities().Select(l => new { l.Id, l.Name });

                foreach (var item in CAPList)
                {
                    items.Add(new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }

        public List<SelectListItem> BindTransferTypeList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                var transferTypeList = objDirectoryDataAccess.GetGenericDetails("Transfer");

                foreach (var item in transferTypeList)
                {
                    items.Add(new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex + "Some Error Occured. Please Contact Admin";
                //  objDirectoryDataAccess.SaveErrorLog(this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name, Server.GetLastError().ToString(), HttpContext.Session["UserNTID"].ToString());
            }
            return items;
        }

        public List<SelectListItem> BindDocCategoryList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {

                var DcList = objDirectoryDataAccess.GetDocumentCategory().Where(a => a.IsActive == true).Select(l => new { l.ID, l.Name });

                foreach (var item in DcList)
                {
                    items.Add(new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.ID.ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }

        public ActionResult FAQ()
        {

            return View();
        }

        //public List<RSSFeed> GetRSSFeedList()
        //{
        //    var list = objDirectoryDataAccess.GetRssFeedList().Where(r => r.IsActive == true).Select(l => new { l.Url });

        //    List<RSSFeed> feeds = new List<RSSFeed>();
        //    foreach (var item in list)
        //    {
        //        feeds.AddRange(GetRSS(item.Url));

        //    }
        //    return feeds;
        //}

        //private List<RSSFeed> GetRSS(string url)
        //{
        //    List<RSSFeed> rssFeeds = new List<RSSFeed>();
        //    try
        //    {

        //        XmlReader reader = XmlReader.Create(url);
        //        SyndicationFeed feed = SyndicationFeed.Load(reader);
        //        reader.Close();
        //        foreach (SyndicationItem feedItem in feed.Items)
        //        {
        //            RSSFeed rssFeed = new RSSFeed();
        //            rssFeed.FeedUrl = feedItem.Links[0].GetAbsoluteUri().ToString();
        //            rssFeed.FeedTitle = feedItem.Title.Text;
        //            rssFeeds.Add(rssFeed);

        //        }
        //        return rssFeeds;
        //    }
        //    catch (Exception)
        //    {
        //        // throw ex;
        //    }
        //    return rssFeeds;
        //}

        /// Bind Dropdownlist of  
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> BindLOBDropDownListR(int capid)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                var lobList = objDirectoryDataAccess.GetLob().Where(m => m.CapId == capid).Select(l => new { l.Id, l.Name });

                foreach (var item in lobList)
                {
                    items.Add(new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }

        public List<SelectListItem> BindCAPDropDownListR(int bsid)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                var CAPList = objDirectoryDataAccess.GetCapabilities().Where(m => m.BsId == bsid).Select(l => new { l.Id, l.Name });

                foreach (var item in CAPList)
                {
                    items.Add(new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }

        /// <summary>
        /// Bind Dropdownlist of Reports
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> BindReportsDropDownList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                var reportsList = objDirectoryDataAccess.GetGenericDetails("ReportType");

                foreach (var item in reportsList)
                {
                    items.Add(new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Some Error Occured. Please Contact Admin";
                objDirectoryDataAccess.SaveErrorLog(this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name, Server.GetLastError().ToString(), HttpContext.Session["UserNTID"].ToString());
            }
            return items;
        }

        public List<SelectListItem> BindFilterCriteriaList()
        {

            List<SelectListItem> items = new List<SelectListItem>();
            try
            {

                var filterCriteriaList = objDirectoryDataAccess.GetGenericDetails("FilterCriteria");

                foreach (var item in filterCriteriaList)
                {
                    items.Add(new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Some Error Occured. Please Contact Admin";
                objDirectoryDataAccess.SaveErrorLog(this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name, Server.GetLastError().ToString(), HttpContext.Session["UserNTID"].ToString());
            }
            return items;
        }
    }
}