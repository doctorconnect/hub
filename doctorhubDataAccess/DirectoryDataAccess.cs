using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using doctorhubBusinessEntities;
using System.Data.Common;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using doctorhubBusinessEntities.viewModels;
using System.Net;
using System.Net.Mail;

namespace doctorhubDataAccess
{
    public class DirectoryDataAccess
    {
        #region Variables
        Database m_Database;
        Menu ObjMenu;
        Feedback ObjFeedback;
        UserRegistration ObjUserRegistration;
        Menu objMenu;
        ReportModel objReport;
        Feedback objFeebback;
        GenericModel objGenericModel;
        UserRegistrationModel objUserRegistrationModel;
        EmailTemplates objEmailTemplates;
        UploadDocument objUploadDocument;
        AboutMe objAboutMe;
        Followers objFollower;
        Blog objBlog;
        posts objPost;
        BulletinBoard objBulletinBoard;
        QuizAnswersVM objQuizAnswers;
        Notification objNotification;

        #endregion

        #region-- Constructor
        private DirectoryDataAccess ObjDirectoryDataAccess;

        public DirectoryDataAccess(string key = null)
        {
            string connectionkey = GetconnectionKey(key);
            m_Database = DatabaseFactory.CreateDatabase(connectionkey);
            // for ad // this.ObjADirectoryDataAccess = new ADirectoryDataAccess();
        }

        private string GetconnectionKey(string KEY)
        {
            string connectionkey = DBConstant.Connectstring;
            return connectionkey;
        }

        private string GetSiteURL(string SLA =null)
        {
            string siteURL = "";
            return siteURL;
        }

        #endregion

        #region User login  Method

        public int SubmitUserRequest(UserRegistration model)
        {
            int success = 0;
            try
            {
                using (DbCommand dbcommand = m_Database.GetStoredProcCommand("PrcSubmitUserrequest"))
                {
                    m_Database.AddInParameter(dbcommand, "@Name", DbType.String, model.UserName);
                    m_Database.AddInParameter(dbcommand, "@EmailId", DbType.String, model.EmailId);
                    m_Database.AddInParameter(dbcommand, "@Password", DbType.String, model.Password);

                    success = m_Database.ExecuteNonQuery(dbcommand);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;

        }

        #endregion

        #region Public Method

        public List<Menu> GetMenuList(string NTID)
        {
            List<Menu> objMenu = new List<Menu>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETMENULIST))
            {
                m_Database.AddInParameter(dbCommand, "@NTID", DbType.String, NTID);
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objMenu.Add(GetMenuDetailsFromDataReader(dataReader));
                    }
                }
            }
            return objMenu;
        }

        public List<GenericModel> GetGenericDetails(string Identifier)
        {
            List<GenericModel> objGeneric = new List<GenericModel>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETGENERICDETAILS))
            {
                m_Database.AddInParameter(dbCommand, "@Identifier", DbType.String, Identifier);

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objGeneric.Add(GetGenericTypeFromDataReader(dataReader));
                    }
                }
            }
            return objGeneric;
        }

        public List<UserRegistrationModel> GetListOfRegisteredUser()
        {
            List<UserRegistrationModel> objUserRegistrationModel = new List<UserRegistrationModel>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETLISTOFREGISTEREDUSER))
            {
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objUserRegistrationModel.Add(GetUserRegistrationDetailsFromDataReader(dataReader));
                    }
                }
            }
            return objUserRegistrationModel;
        }

        public List<UserRegistrationModel> GetManageUser()
        {
            List<UserRegistrationModel> objUserRegistrationModel = new List<UserRegistrationModel>();

            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETLISTOFREGISTEREDUSER))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objUserRegistrationModel.Add(GetUserRegistrationDetailsFromDataReader(dataReader));
                    }
                }
            }
            return objUserRegistrationModel;
        }

        public List<Feedback> GetListOfUserFeedback()
        {
            List<Feedback> objFeedback = new List<Feedback>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETLISTOFUSERFEEDBACK))
            {
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objFeedback.Add(GetUserFeedbackFromDataReader(dataReader));
                    }
                }
            }
            return objFeedback;
        }

        public List<Feedback> GetListOfAdminFeedback(string Status)
        {
            List<Feedback> objFeedback = new List<Feedback>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETLISTOFADMINFEEDBACK))
            {
                m_Database.AddInParameter(dbCommand, "Status", DbType.String, Status);
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objFeedback.Add(GetUserFeedbackFromDataReader(dataReader));
                    }
                }
            }
            return objFeedback;
        }

        public int SubmitUserRequest(HttpPostedFileBase poImgFile, byte[] imageData, UserRegistrationModel model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITUSERREQUEST))
            {
                m_Database.AddInParameter(dbCommand, "@UserCode", DbType.String, model.UserCode);
                m_Database.AddInParameter(dbCommand, "@UserNTID", DbType.String, model.UserNTID);
                m_Database.AddInParameter(dbCommand, "@UserEmail", DbType.String, model.UserEmail);
                m_Database.AddInParameter(dbCommand, "@UserName", DbType.String, model.UserName);
                m_Database.AddInParameter(dbCommand, "@ManagerCode", DbType.String, model.ManagerCode);
                m_Database.AddInParameter(dbCommand, "@ManagerEmail", DbType.String, model.ManagerEmail);
                m_Database.AddInParameter(dbCommand, "@ManagerNTID", DbType.String, model.ManagerNTID);
                m_Database.AddInParameter(dbCommand, "@ManagerName", DbType.String, model.ManagerName);
                m_Database.AddInParameter(dbCommand, "@RoleId", DbType.Int32, model.RoleId);
                m_Database.AddInParameter(dbCommand, "@BusinessSegmentId", DbType.Int32, model.BusinessSegmentId);
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.Int32, model.CapabilitiesId);
                m_Database.AddInParameter(dbCommand, "@LOBId", DbType.Int32, model.LOBId);
                m_Database.AddInParameter(dbCommand, "@Status", DbType.Int32, StatusType.Pending);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, true);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, "DOCTOR-HUB");
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@UserPhoto", DbType.Binary, imageData);
                m_Database.AddInParameter(dbCommand, "@Points", DbType.String, 0);
                if (poImgFile == null)
                {
                    m_Database.AddInParameter(dbCommand, "@AvatarExt", DbType.String, ".Png");
                }
                else
                {
                    m_Database.AddInParameter(dbCommand, "@AvatarExt", DbType.String, poImgFile.FileName.Split('\\').Last());
                }


                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int CreateUserProfile(UserRegistrationModel model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCCREATEUSERPROFILE))
            {
                m_Database.AddInParameter(dbCommand, "@UserCode", DbType.String, model.UserCode);
                m_Database.AddInParameter(dbCommand, "@UserNTID", DbType.String, model.UserNTID);
                m_Database.AddInParameter(dbCommand, "@UserEmail", DbType.String, model.UserEmail);
                m_Database.AddInParameter(dbCommand, "@UserName", DbType.String, model.UserName);
                m_Database.AddInParameter(dbCommand, "@ManagerCode", DbType.String, model.ManagerCode);
                m_Database.AddInParameter(dbCommand, "@ManagerEmail", DbType.String, model.ManagerEmail);
                m_Database.AddInParameter(dbCommand, "@ManagerNTID", DbType.String, model.ManagerNTID);
                m_Database.AddInParameter(dbCommand, "@ManagerName", DbType.String, model.ManagerName);
                m_Database.AddInParameter(dbCommand, "@RoleId", DbType.Int32, model.RoleId);
                m_Database.AddInParameter(dbCommand, "@BusinessSegmentId", DbType.Int32, model.BusinessSegmentId);
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.Int32, model.CapabilitiesId);
                m_Database.AddInParameter(dbCommand, "@LOBId", DbType.Int32, model.LOBId);
                m_Database.AddInParameter(dbCommand, "@Status", DbType.Int32, StatusType.Pending);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, true);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int UpdateUserDetails(UserRegistrationModel model, int UserId)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCUPDATEUSERDETAILS))
            {
                m_Database.AddInParameter(dbCommand, "@Id", DbType.Int32, UserId);
                m_Database.AddInParameter(dbCommand, "@RoleId", DbType.Int32, model.RoleId);
                m_Database.AddInParameter(dbCommand, "@BusinessSegmentId", DbType.Int32, model.BusinessSegmentId);
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.Int32, model.CapabilitiesId);
                m_Database.AddInParameter(dbCommand, "@LOBId", DbType.Int32, model.LOBId);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, model.IsActive);
                m_Database.AddInParameter(dbCommand, "@UserCode", DbType.String, model.UserCode);
                m_Database.AddInParameter(dbCommand, "@UserName", DbType.String, model.UserName);
                m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@Status", DbType.Int32, Convert.ToInt32(StatusType.Pending));

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int UpdateUserDetails(UserRegistrationModel model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCUPDATEUSERDETAILS))
            {
                m_Database.AddInParameter(dbCommand, "@Id", DbType.Int32, model.Id);
                m_Database.AddInParameter(dbCommand, "@RoleId", DbType.Int32, model.RoleId);
                m_Database.AddInParameter(dbCommand, "@BusinessSegmentId", DbType.Int32, model.BusinessSegmentId);
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.Int32, model.CapabilitiesId);
                m_Database.AddInParameter(dbCommand, "@LOBId", DbType.Int32, model.LOBId);
                m_Database.AddInParameter(dbCommand, "@UserCode", DbType.String, model.UserCode);
                m_Database.AddInParameter(dbCommand, "@UserName", DbType.String, model.UserName);
                m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@AdminUser", DbType.String, Convert.ToString(HttpContext.Current.Session["UserFirstName"]));
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.Int32, model.IsActive);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int UpdateUserRequestStatus(int Id, string Status)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCUPDATEUSERREQUESTSTATUS))
            {
                m_Database.AddInParameter(dbCommand, "@Id", DbType.Int32, Id);
                m_Database.AddInParameter(dbCommand, "@Status", DbType.Int32, Status == "Approve" ? Convert.ToInt32(StatusType.Approve) : Convert.ToInt32(StatusType.Reject));

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int UpdateBulkUserRequestStatus(string[] Ids)
        {
            int success = 0;
            foreach (var itm in Ids)
            {
                using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCUPDATEUSERREQUESTSTATUS))
                {
                    m_Database.AddInParameter(dbCommand, "@Id", DbType.Int32, itm);
                    m_Database.AddInParameter(dbCommand, "@Status", DbType.Int32, Convert.ToInt32(StatusType.Approve));
                    success = m_Database.ExecuteNonQuery(dbCommand);

                    if (success > 0)
                    {
                        UserRegistrationModel model = GetListOfRegisteredUser().Where(x => x.Id == Convert.ToInt32(itm)).FirstOrDefault();
                        SendEmail(model, "KMT3");
                    }
                }
            }
            return success;
        }

        public int UpdateBulkUserRequestStatusReject(string[] Ids)
        {
            int success = 0;
            foreach (var itm in Ids)
            {
                using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCUPDATEUSERREQUESTSTATUS))
                {
                    m_Database.AddInParameter(dbCommand, "@Id", DbType.Int32, itm);
                    m_Database.AddInParameter(dbCommand, "@Status", DbType.Int32, Convert.ToInt32(StatusType.Reject));
                    success = m_Database.ExecuteNonQuery(dbCommand);

                    if (success > 0)
                    {
                        UserRegistrationModel model = GetListOfRegisteredUser().Where(x => x.Id == Convert.ToInt32(itm)).FirstOrDefault();
                        SendEmail(model, "KMT26");
                    }
                }
            }
            return success;
        }

        public int SubmitUserFeedBack(Feedback model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITUSERFEEDBACK))
            {
                int Id = (GetListOfUserFeedback().Select(x => (int?)x.Id).Max() ?? 0) + 1;
                string FeedbackId = "SR" + Id.ToString().PadLeft(7, '0');
                m_Database.AddInParameter(dbCommand, "@UserCode", DbType.String, model.UserCode);
                m_Database.AddInParameter(dbCommand, "@UserNTID", DbType.String, model.UserNTID);
                m_Database.AddInParameter(dbCommand, "@UserEmail", DbType.String, model.UserEmail);
                m_Database.AddInParameter(dbCommand, "@UserName", DbType.String, model.UserName);
                m_Database.AddInParameter(dbCommand, "@LOBId", DbType.Int32, 1);
                m_Database.AddInParameter(dbCommand, "@FeedbackId", DbType.String, FeedbackId);
                m_Database.AddInParameter(dbCommand, "@RoleId", DbType.Int32, Convert.ToInt32(HttpContext.Current.Session["RoleId"]));
                m_Database.AddInParameter(dbCommand, "@UserFeedback", DbType.String, model.FeedbackQuestion);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int UpdateAdminReply(string[] feedbackId)
        {
            int success = 0;
            foreach (var itm in feedbackId)
            {
                using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCUPDATEADMINREPLY))
                {
                    if (!string.IsNullOrEmpty(itm))
                    {
                        string[] item = itm.Split('^');
                        m_Database.AddInParameter(dbCommand, "@FeedBackId", DbType.String, item[0]);
                        m_Database.AddInParameter(dbCommand, "@AdminReply", DbType.String, item[1]);
                        m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                        m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);

                        success = m_Database.ExecuteNonQuery(dbCommand);

                        if (success > 0)
                        {
                            UserRegistrationModel model = GetListOfRegisteredUser().Where(x => x.UserCode == item[2]).FirstOrDefault();
                            SendEmail(model, "KMT19");
                        }
                    }
                }
            }
            return success;
        }

        public List<Notification> GetListOfNotification()
        {
            List<Notification> objNotification = new List<Notification>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETLISTOFNOTIFICATION))
            {
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objNotification.Add(GetNotificationFromDataReader(dataReader));
                    }
                }
            }
            return objNotification;
        }

        public int UpdateNotificationCount(DateTime NotificationTime, List<Notification> notiList, string flag)
        {
            int success = 0;
            foreach (var itm in notiList)
            {
                using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCUPDATENOTIFICATIONCOUNT))
                {
                    m_Database.AddInParameter(dbCommand, "@RegisterNotificationTime", DbType.DateTime, NotificationTime);
                    m_Database.AddInParameter(dbCommand, "@UserId", DbType.Int32, itm.Id);
                    m_Database.AddInParameter(dbCommand, "@Flag", DbType.String, flag);
                    m_Database.AddInParameter(dbCommand, "@UserCode", DbType.String, HttpContext.Current.Session["UserEmployeeId"].ToString());

                    success = m_Database.ExecuteNonQuery(dbCommand);
                }
            }
            return success;
        }

        public void SaveErrorLog(string Method, string Exception, string UserNTID)
        {
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSAVEERROR))
            {
                m_Database.AddInParameter(dbCommand, "@Method", DbType.String, Method);
                m_Database.AddInParameter(dbCommand, "@Exception", DbType.String, Exception);
                m_Database.AddInParameter(dbCommand, "@UserNTID", DbType.String, UserNTID);

                m_Database.ExecuteNonQuery(dbCommand);
            }
        }

        public List<EmailTemplates> GetEmailTemplates()
        {
            List<EmailTemplates> objTemplates = new List<EmailTemplates>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETEMAILTEMPLATES))
            {
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objTemplates.Add(GetEmailTemplatesFromDataReader(dataReader));
                    }
                }
            }
            return objTemplates;
        }

        public List<posts> GetListOfUserFlagPost()
        {
            List<posts> objPost = new List<posts>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETLISTFLAGPOSTLIST))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objPost.Add(GetListOfUserFlagPosFromDataReader(dataReader));
                    }
                }
            }
            return objPost;
        }

        public List<UploadDocument> GetListOfUploadDoc()
        {
            List<UploadDocument> objUploadDocument = new List<UploadDocument>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETLISTOFUPLOADDOC))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objUploadDocument.Add(GetUserUploadDocFromDataReader(dataReader));
                    }
                }
            }
            return objUploadDocument;
        }

        public List<AboutMe> GetAbotMe(string id)
        {
            List<AboutMe> objAboutme = new List<AboutMe>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETABOUTME))
            {
                m_Database.AddInParameter(dbCommand, "@Id", DbType.String, id);
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objAboutme.Add(GetAboutMeFromDataReader(dataReader));
                    }
                }
            }
            return objAboutme;
        }

        public int UpdateAboutMe(string Abme)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCUPDATEABOUTME))
            {
                m_Database.AddInParameter(dbCommand, "@Id", DbType.String, HttpContext.Current.Session["ID"].ToString());
                m_Database.AddInParameter(dbCommand, "@AboutMe", DbType.String, Abme.ToString());
                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public List<Followers> GetMyFollower(string id)
        {
            List<Followers> objFollower = new List<Followers>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETMYFOLLOWER))
            {
                m_Database.AddInParameter(dbCommand, "@Id", DbType.String, id);
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objFollower.Add(GetMyFollowerFromDataReader(dataReader));
                    }
                }
            }
            return objFollower;
        }

        public int DeleteBlogPost(int BlogId)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCDELETEBLOGPOST))
            {
                m_Database.AddInParameter(dbCommand, "@BlogId", DbType.Int32, BlogId);
                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int SubmitBlogPost(Blog model, int UserCode)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITBLOGPOST))
            {
                m_Database.AddInParameter(dbCommand, "@BlogId", DbType.Int32, model.BlogId);
                m_Database.AddInParameter(dbCommand, "@CapId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@BlogBy", DbType.Int32, UserCode);
                m_Database.AddInParameter(dbCommand, "@Title", DbType.String, model.Title);
                m_Database.AddInParameter(dbCommand, "@Message", DbType.String, model.Message);
                m_Database.AddInParameter(dbCommand, "@BlogDate", DbType.DateTime, DateTime.Now);
                foreach (var item in GetPointList())
                {
                    if (item.InteractionType == "UploadBlog")
                    {
                        m_Database.AddInParameter(dbCommand, "@Points", DbType.String, item.Point);
                    }
                }
                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public List<Blog> GetBlogList()
        {
            List<Blog> objBlog = new List<Blog>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETBLOGLIST))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objBlog.Add(GetBlogListFromDataReader(dataReader));
                    }
                }
            }
            return objBlog;
        }

        public List<UserRegistrationModel> GetProfileImg(string id)
        {
            List<UserRegistrationModel> objUserRegisration = new List<UserRegistrationModel>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETPROFILEIMG))
            {
                m_Database.AddInParameter(dbCommand, "@Id", DbType.String, id);
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objUserRegisration.Add(GetProfileImgFromDataReader(dataReader));
                    }
                }
            }
            return objUserRegisration;
        }

        public int UpdateProfileImg(HttpPostedFileBase poImgFile, byte[] imageData)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCUPDATEUSERPROFILE))
            {
                m_Database.AddInParameter(dbCommand, "@Id", DbType.String, HttpContext.Current.Session["ID"].ToString());
                m_Database.AddInParameter(dbCommand, "@UserPhoto", DbType.Binary, imageData);
                m_Database.AddInParameter(dbCommand, "@AvatarExt", DbType.String, poImgFile.FileName.Split('\\').Last());
                m_Database.AddInParameter(dbCommand, "@UserCode", DbType.String, Convert.ToString(HttpContext.Current.Session["UserEmployeeId"]));
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.String, DateTime.Now);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public List<UserRegistrationModel> GetUserList(string name)
        {
            List<UserRegistrationModel> objUserRegisration = new List<UserRegistrationModel>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETSEARCHUSERLIST))
            {
                m_Database.AddInParameter(dbCommand, "@name", DbType.String, name);
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objUserRegisration.Add(GetUserListFromDataReader(dataReader));
                    }
                }
            }
            return objUserRegisration;
        }

        public List<Followers> GetStatusFollower(string id, string Userid)
        {
            List<Followers> objFollower = new List<Followers>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETFOLLOWERCOUNT))
            {
                m_Database.AddInParameter(dbCommand, "@Id", DbType.String, id);
                m_Database.AddInParameter(dbCommand, "@UserId", DbType.String, Userid);
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objFollower.Add(GetStatusFollowerFromDataReader(dataReader));
                    }
                }
            }
            return objFollower;
        }

        public int UpdateFollower(string type, string id, string Userid)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCUPDATEFOLLOWER))
            {
                m_Database.AddInParameter(dbCommand, "@Id", DbType.Int32, Convert.ToInt32(id));
                m_Database.AddInParameter(dbCommand, "@UserId", DbType.Int32, Convert.ToInt32(Userid));
                m_Database.AddInParameter(dbCommand, "@Type", DbType.String, type);
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public List<Blog> GetDocblogList()
        {
            List<Blog> objUserRegisration = new List<Blog>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETLISTOFPOSTDDOC))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objUserRegisration.Add(GetDocblogListFromDataReader(dataReader));
                    }
                }
            }
            return objUserRegisration;
        }

        public int UploadDocument(HttpPostedFileBase FileUpload1, string message, UploadDocument upd)
        {
            int success = 0;
            string filePath = FileUpload1.FileName;
            string filename1 = Path.GetFileName(filePath);
            string ext = Path.GetExtension(filename1);
            string type = String.Empty;
            switch (ext.ToLower())
            {
                case ".doc":
                    type = "application/msword";
                    break;
                case ".docx":
                    type = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case ".xls":
                    type = "application/vnd.ms-excel";
                    break;
                case ".xlsx":
                    type = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case ".png":
                    type = "image/png";
                    break;
                case ".gif":
                    type = "image/gif";
                    break;
                case ".jpg":
                    type = "image/jpeg";
                    break;
                case ".pdf":
                    type = "application/pdf";
                    break;
            }

            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITDOCUMENT))
            {
                m_Database.AddInParameter(dbCommand, "@UPLOADBY", DbType.Int32, HttpContext.Current.Session["ID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CapId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@Name", DbType.String, filename1);
                m_Database.AddInParameter(dbCommand, "@Message", DbType.String, message);
                m_Database.AddInParameter(dbCommand, "@TYPE", DbType.String, type);
                m_Database.AddInParameter(dbCommand, "@FilePath", DbType.String, filePath);
                m_Database.AddInParameter(dbCommand, "@STATUS", DbType.String, "0");
                m_Database.AddInParameter(dbCommand, "@Title", DbType.String, upd.Title);
                m_Database.AddInParameter(dbCommand, "@Category", DbType.String, upd.Category);
                m_Database.AddInParameter(dbCommand, "@UserCode", DbType.String, Convert.ToString(HttpContext.Current.Session["UserEmployeeId"]));
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.String, DateTime.Now);

                foreach (var item in GetPointList())
                {
                    if (item.InteractionType == "UploadDoc")
                    {
                        m_Database.AddInParameter(dbCommand, "@Points", DbType.String, item.Point);
                        break;
                    }
                }
                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int UpdateUploaddocStatus(string[] id)
        {
            int success = 0;
            foreach (var itm in id)
            {
                using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROUPDATEUPLOADDOCSTATUS))
                {
                    if (!string.IsNullOrEmpty(itm))
                    {
                        string[] item = itm.Split('^');
                        m_Database.AddInParameter(dbCommand, "@Id", DbType.String, item[0]);
                        m_Database.AddInParameter(dbCommand, "@Remarks", DbType.String, item[1]);
                        m_Database.AddInParameter(dbCommand, "@ApproveBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                        m_Database.AddInParameter(dbCommand, "@Approvedate", DbType.DateTime, DateTime.Now);

                        success = m_Database.ExecuteNonQuery(dbCommand);

                        if (success > 0)
                        {
                            string userId = GetListOfUploadDoc().Where(x => x.ID == Convert.ToInt32(item[0])).Select(x => x.UploadBy).FirstOrDefault();
                            UserRegistrationModel model = GetListOfRegisteredUser().Where(x => x.Id == Convert.ToInt32(userId)).FirstOrDefault();
                            model.Status = 6;
                            SendEmail(model, "KMT14");
                        }
                    }
                }
            }
            return success;
        }

        public int UpdateBlogStatus(string[] id)
        {
            int success = 0;
            foreach (var itm in id)
            {
                using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROUPDATEBLOGSSTATUS))
                {
                    if (!string.IsNullOrEmpty(itm))
                    {
                        string[] item = itm.Split('^');
                        m_Database.AddInParameter(dbCommand, "@Id", DbType.String, item[0]);
                        m_Database.AddInParameter(dbCommand, "@Remarks", DbType.String, item[1]);
                        m_Database.AddInParameter(dbCommand, "@ApproveBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                        m_Database.AddInParameter(dbCommand, "@Approvedate", DbType.DateTime, DateTime.Now);

                        success = m_Database.ExecuteNonQuery(dbCommand);

                        if (success > 0)
                        {
                            int userId = GetBlogList().Where(x => x.BlogId == Convert.ToInt32(item[0])).Select(x => x.BlogerId).FirstOrDefault();
                            UserRegistrationModel model = GetListOfRegisteredUser().Where(x => x.Id == userId).FirstOrDefault();
                            model.Status = 6;
                            SendEmail(model, "KMT11");
                        }
                    }
                }
            }
            return success;
        }

        public List<UserRegistrationModel> GetProfileImgList()
        {
            List<UserRegistrationModel> objUserRegisration = new List<UserRegistrationModel>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETPROFILEIMGLIST))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objUserRegisration.Add(GetProfileImgListFromDataReader(dataReader));
                    }
                }
            }
            return objUserRegisration;
        }

        public int UpdateImgStatus(string[] id)
        {
            int success = 0;
            foreach (var itm in id)
            {
                using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROUPDATEIMGSTATUS))
                {
                    if (!string.IsNullOrEmpty(itm))
                    {
                        string[] item = itm.Split('^');
                        m_Database.AddInParameter(dbCommand, "@Id", DbType.String, item[0]);
                        m_Database.AddInParameter(dbCommand, "@Remarks", DbType.String, item[1]);
                        m_Database.AddInParameter(dbCommand, "@Status", DbType.String, item[2]);
                        m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                        m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);
                        success = m_Database.ExecuteNonQuery(dbCommand);
                    }
                }
            }
            return success;
        }

        public List<posts> GetLikeCount(string Identifier)
        {
            List<posts> objPost = new List<posts>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETPOSTLIKE))
            {
                //  m_Database.AddInParameter(dbCommand, "@PostId", DbType.String, id);
                m_Database.AddInParameter(dbCommand, "@Identifier", DbType.String, Identifier);
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objPost.Add(GetLikeCountFromDataReader(dataReader));
                    }
                }
            }
            return objPost;
        }

        public List<posts> GetPost()
        {
            List<posts> objPost = new List<posts>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETPOSTLIST))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objPost.Add(GetPostFromDataReader(dataReader));
                    }
                }
            }
            return objPost;
        }

        public List<posts> GetCommentListOnPost(string Identifier)
        {
            List<posts> objPost = new List<posts>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETCOMMENT))
            {
                //  m_Database.AddInParameter(dbCommand, "@PostId", DbType.String, id);
                m_Database.AddInParameter(dbCommand, "@Identifier", DbType.String, Identifier);

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objPost.Add(GetCommentListOnPostFromDataReader(dataReader));
                    }
                }
            }
            return objPost;
        }

        public List<posts> GetListOfPost()
        {
            List<posts> objPost = new List<posts>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETLISTOFPOST))
            {
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objPost.Add(GetListOfPostFromDataReader(dataReader));
                    }
                }
            }
            return objPost;
        }

        public int SubmitPost(string msg)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITPOST))
            {
                m_Database.AddInParameter(dbCommand, "@PostId", DbType.Int32, "0");
                m_Database.AddInParameter(dbCommand, "@CapId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@PostedBy", DbType.Int32, HttpContext.Current.Session["ID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@Message", DbType.String, msg);
                m_Database.AddInParameter(dbCommand, "@PostedDate", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@Remarks", DbType.String, null);
                m_Database.AddInParameter(dbCommand, "@ApproveBy", DbType.String, null);
                m_Database.AddInParameter(dbCommand, "@ApprovedDate", DbType.DateTime, null);
                foreach (var item in GetPointList())
                {
                    if (item.InteractionType == "UploadPost")
                    {
                        m_Database.AddInParameter(dbCommand, "@Points", DbType.String, item.Point);
                    }
                }


                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int DeleteFlagpost(string[] id)
        {
            int success = 0;
            foreach (var itm in id)
            {
                using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCDELETEPOST))
                {
                    if (!string.IsNullOrEmpty(itm))
                    {
                        string[] item = itm.Split('^');

                        string userId = GetListOfPost().Where(x => x.PostId == Convert.ToInt32(item[0])).Select(x => x.PostedBy).FirstOrDefault();
                        UserRegistrationModel model = GetListOfRegisteredUser().Where(x => x.Id == Convert.ToInt32(userId)).FirstOrDefault();
                        SendEmail(model, "KMT27");

                        m_Database.AddInParameter(dbCommand, "@PostId", DbType.Int32, item[0]);
                        m_Database.AddInParameter(dbCommand, "@Identifier", DbType.String, item[1]);
                        m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);

                        success = m_Database.ExecuteNonQuery(dbCommand);
                    }
                }
            }
            return success;
        }

        public int UnFlagPost(string[] id)
        {
            int success = 0;

            foreach (var itm in id)
            {
                using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCUNFLAGEPOST))
                {
                    if (!string.IsNullOrEmpty(itm))
                    {
                        string[] item = itm.Split('^');
                        m_Database.AddInParameter(dbCommand, "@PostId", DbType.Int32, item[0]);
                        m_Database.AddInParameter(dbCommand, "@Identifier", DbType.String, item[1]);
                        m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);

                        success = m_Database.ExecuteNonQuery(dbCommand);

                        if (success > 0)
                        {
                            string userId = GetListOfPost().Where(x => x.PostId == Convert.ToInt32(item[0])).Select(x => x.PostedBy).FirstOrDefault();
                            UserRegistrationModel model = GetListOfRegisteredUser().Where(x => x.Id == Convert.ToInt32(userId)).FirstOrDefault();
                            SendEmail(model, "KMT8");
                        }

                    }
                }
            }
            return success;
        }

        public int SubmitCommentt(int PostId, string msg, string Identifier)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITCOMMENT))
            {
                m_Database.AddInParameter(dbCommand, "@PostId", DbType.Int32, PostId);
                m_Database.AddInParameter(dbCommand, "@CommentedBy", DbType.Int32, HttpContext.Current.Session["ID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@Message", DbType.String, msg);
                m_Database.AddInParameter(dbCommand, "@Identifier", DbType.String, Identifier);
                foreach (var item in GetPointList())
                {
                    if (item.InteractionType == "CommentPost")
                    {
                        m_Database.AddInParameter(dbCommand, "@Points", DbType.String, item.Point);
                    }
                }
                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int SubmitLike(int PostId, string Identifier)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITLIKE))
            {
                m_Database.AddInParameter(dbCommand, "@PostId", DbType.Int32, PostId);
                m_Database.AddInParameter(dbCommand, "@LikeBy", DbType.Int32, HttpContext.Current.Session["ID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@Identifier", DbType.String, Identifier);
                foreach (var item in GetPointList())
                {
                    if (item.InteractionType == "LikePost")
                    {
                        m_Database.AddInParameter(dbCommand, "@Points", DbType.String, item.Point);
                    }
                }
                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int PostFlag(int PostId)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITPOSTFLAG))
            {
                m_Database.AddInParameter(dbCommand, "@PostId", DbType.Int32, PostId);
                m_Database.AddInParameter(dbCommand, "@CapId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@FlagedBy", DbType.Int32, HttpContext.Current.Session["ID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                foreach (var item in GetPointList())
                {
                    if (item.InteractionType == "Flagpost")
                    {
                        m_Database.AddInParameter(dbCommand, "@Points", DbType.String, item.Point);
                    }
                }
                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        #region 

        public int SubmitPoll(ManagePoll Pollmodel)
        {
            int success = 0;
            if (Pollmodel.PollID > 0)
            {
                using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCUPDATEManagePoll))
                {
                    m_Database.AddInParameter(dbCommand, "@PollID", DbType.String, Pollmodel.PollID);
                    m_Database.AddInParameter(dbCommand, "@Title", DbType.String, Pollmodel.Questions);
                    string Options = Pollmodel.Options.Replace("\r\n", "~");
                    //string[] Options = Pollmodel.Options.Split().Where(x => x != "\n").ToArray();
                    m_Database.AddInParameter(dbCommand, "@Options", DbType.String, Options.TrimEnd('~'));
                    m_Database.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, Pollmodel.FromDate);
                    m_Database.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, Pollmodel.ToDate);
                    m_Database.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, Pollmodel.IsActive);
                    m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                    m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);

                    success = m_Database.ExecuteNonQuery(dbCommand);
                }
            }
            else
            {
                using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITMANAEPOLL))
                {
                    m_Database.AddInParameter(dbCommand, "@Title", DbType.String, Pollmodel.Questions);
                    string Options = Pollmodel.Options.Replace("\r\n", "~");
                    m_Database.AddInParameter(dbCommand, "@Options", DbType.String, Options.TrimEnd('~'));
                    m_Database.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, Pollmodel.FromDate);
                    m_Database.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, Pollmodel.ToDate);
                    m_Database.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, Pollmodel.IsActive);
                    m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                    m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                    m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, "NULL");
                    m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);

                    success = m_Database.ExecuteNonQuery(dbCommand);
                }
            }
            return success;
        }

        public List<ManagePoll> GetManagePolls()
        {
            List<ManagePoll> objManagePoll = new List<ManagePoll>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETManagePoll))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objManagePoll.Add(GetManagePolls(dataReader));
                    }
                }
            }
            return objManagePoll;
        }

        private ManagePoll GetManagePolls(IDataReader datareader)
        {
            ManagePoll objManagePoll = new ManagePoll();
            objManagePoll.PollID = SafeTypeHandling.ConvertStringToInt32(datareader["PollID"]);
            objManagePoll.Questions = SafeTypeHandling.ConvertToString(datareader["Title"]);
            string splitString = SafeTypeHandling.ConvertToString(datareader["Options"]).TrimEnd('~');
            foreach (var item in splitString.Replace("~", "<br/>"))
            {
                objManagePoll.Options += item.ToString();
            }
            objManagePoll.FromDate = SafeTypeHandling.ConvertToDateTime(datareader["FromDate"]);
            objManagePoll.ToDate = SafeTypeHandling.ConvertToDateTime(datareader["ToDate"]);
            objManagePoll.IsActive = SafeTypeHandling.ConvertStringToBoolean(datareader["IsActive"]);

            return objManagePoll;
        }

        public int DeleteManagePoll(int PollID)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCDELETEManagePoll))
            {
                m_Database.AddInParameter(dbCommand, "@PollID", DbType.Int32, PollID);
                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public List<ManagePoll> GetPolls(string currentDate)
        {
            List<ManagePoll> objPoll = new List<ManagePoll>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETPOLLS))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());
                m_Database.AddInParameter(dbCommand, "@CurrentDate", DbType.DateTime, currentDate);
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objPoll.Add(GetPolls(dataReader));
                    }
                }
            }
            return objPoll;
        }

        private ManagePoll GetPolls(IDataReader datareader)
        {
            ManagePoll objPolls = new ManagePoll();
            objPolls.Title = SafeTypeHandling.ConvertToString(datareader["Title"]);
            objPolls.Options = SafeTypeHandling.ConvertToString(datareader["Options"]).TrimEnd('~');
            objPolls.FromDate = SafeTypeHandling.ConvertToDateTime(datareader["FromDate"]);
            objPolls.ToDate = SafeTypeHandling.ConvertToDateTime(datareader["ToDate"]);

            return objPolls;
        }

        public int CreateVote(string Title, string rbtnAnswer)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITVOTE))
            {
                m_Database.AddInParameter(dbCommand, "@Question", DbType.String, Title);
                m_Database.AddInParameter(dbCommand, "@Options", DbType.String, rbtnAnswer);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, "true");
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString().Split('\\')[1]);
                m_Database.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, "NULL");
                m_Database.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, DateTime.Now);
                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        //public List<Poll> GetUserVoteResult(string UserNTID, string Question)
        //{
        //    List<Poll> objPoll = new List<Poll>();
        //    using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETUSERVOTERESULT))
        //    {
        //        m_Database.AddInParameter(dbCommand, "@UserNTID", DbType.String, UserNTID);
        //        m_Database.AddInParameter(dbCommand, "@Question", DbType.String, Question);
        //        using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
        //        {
        //            while (dataReader.Read())
        //            {
        //                objPoll.Add(GetUserVoteResult(dataReader));
        //            }
        //        }
        //    }
        //    return objPoll;
        //}

        //private Poll GetUserVoteResult(IDataReader datareader)
        //{
        //    Poll objPolls = new Poll();
        //    objPolls.Title = SafeTypeHandling.ConvertToString(datareader["Question"]);
        //    objPolls.Options = SafeTypeHandling.ConvertToString(datareader["Options"]);

        //    return objPolls;
        //}

        public ManagePoll GetManagePollsByID(int PollID)
        {
            ManagePoll objManagePoll = new ManagePoll();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETManagePollBYID))
            {
                m_Database.AddInParameter(dbCommand, "@PollID", DbType.Int32, PollID);
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objManagePoll = GetManagePollsByID(dataReader);
                    }
                }
            }
            return objManagePoll;
        }

        private ManagePoll GetManagePollsByID(IDataReader datareader)
        {
            ManagePoll objManagePoll = new ManagePoll();
            objManagePoll.PollID = SafeTypeHandling.ConvertStringToInt32(datareader["PollID"]);
            objManagePoll.Questions = SafeTypeHandling.ConvertToString(datareader["Title"]);
            string splitString = SafeTypeHandling.ConvertToString(datareader["Options"]).TrimEnd('~');
            foreach (var item in splitString.Replace("~", "\n"))
            {
                objManagePoll.Options += item.ToString();
            }
            objManagePoll.FromDate = SafeTypeHandling.ConvertToDateTime(datareader["FromDate"]);
            objManagePoll.ToDate = SafeTypeHandling.ConvertToDateTime(datareader["ToDate"]);
            objManagePoll.IsActive = SafeTypeHandling.ConvertStringToBoolean(datareader["IsActive"]);

            return objManagePoll;
        }

        //public List<Poll> GetUserVoteResultCount(string StrTitle)
        //{
        //    List<Poll> objPoll = new List<Poll>();
        //    using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETUSERVOTERESULTCOUNT))
        //    {
        //        m_Database.AddInParameter(dbCommand, "@Question", DbType.String, StrTitle);
        //        using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
        //        {
        //            while (dataReader.Read())
        //            {
        //                objPoll.Add(GetUserVoteResultCount(dataReader));
        //            }
        //        }
        //    }
        //    return objPoll;
        //}

        //private Poll GetUserVoteResultCount(IDataReader datareader)
        //{
        //    Poll objPolls = new Poll();
        //    objPolls.Title = SafeTypeHandling.ConvertToString(datareader["Question"]);
        //    objPolls.Options = SafeTypeHandling.ConvertToString(datareader["Options"]);
        //    objPolls.VotePercentage = SafeTypeHandling.ConvertStringToInt32(datareader["VotePerc"]);

        //    return objPolls;
        //}

        #endregion
        public int DeletPost(int PostId, string Identifier)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCDELETEPOST))
            {
                m_Database.AddInParameter(dbCommand, "@PostId", DbType.Int32, PostId);
                m_Database.AddInParameter(dbCommand, "@Identifier", DbType.String, Identifier);
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public List<BusinessSegmentModel> GetBusinessSegment()
        {
            List<BusinessSegmentModel> ObjBusinessSegment = new List<BusinessSegmentModel>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETBUSINESSSEGMENT))
            {
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ObjBusinessSegment.Add(GetBusinessSegmentFromDataReader(dataReader));
                    }
                }
            }
            return ObjBusinessSegment;
        }

        public int SubmitBusinessSegment(BusinessSegmentModel model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITBUSINESSSEGMENT))
            {
                m_Database.AddInParameter(dbCommand, "@id", DbType.String, model.Id);
                m_Database.AddInParameter(dbCommand, "@Name", DbType.String, model.Name);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.String, model.IsActive);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int UpdateBusinessSegment(BusinessSegmentModel model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITBUSINESSSEGMENT))
            {
                m_Database.AddInParameter(dbCommand, "@id", DbType.String, model.Id);
                m_Database.AddInParameter(dbCommand, "@Name", DbType.String, model.Name);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.String, model.IsActive);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public List<CapabilitiesModel> GetCapabilities()
        {
            List<CapabilitiesModel> ObjCapabilities = new List<CapabilitiesModel>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETCAPABILITIES))
            {
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ObjCapabilities.Add(GetCapabilitiesFromDataReader(dataReader));
                    }
                }
            }
            return ObjCapabilities;
        }

        public int SubmitCapabilities(CapabilitiesModel model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITCAPABILITIES))
            {
                m_Database.AddInParameter(dbCommand, "@id", DbType.String, model.Id);
                m_Database.AddInParameter(dbCommand, "@BsId", DbType.String, model.BsId);
                m_Database.AddInParameter(dbCommand, "@Name", DbType.String, model.Name);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.String, model.IsActive);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int UpdateCapabilities(CapabilitiesModel model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITCAPABILITIES))
            {
                m_Database.AddInParameter(dbCommand, "@id", DbType.String, model.Id);
                m_Database.AddInParameter(dbCommand, "@BsId", DbType.String, model.BsId);
                m_Database.AddInParameter(dbCommand, "@Name", DbType.String, model.Name);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.String, model.IsActive);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public List<LobModel> GetLob()
        {
            List<LobModel> ObjLob = new List<LobModel>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETLOB))
            {
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ObjLob.Add(GetLobFromDataReader(dataReader));
                    }
                }
            }
            return ObjLob;
        }

        public int SubmitLob(LobModel model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITLOB))
            {
                m_Database.AddInParameter(dbCommand, "@id", DbType.String, model.Id);
                m_Database.AddInParameter(dbCommand, "@CapId", DbType.String, model.CapId);
                m_Database.AddInParameter(dbCommand, "@Name", DbType.String, model.Name);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.String, model.IsActive);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int UpdateLob(LobModel model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITLOB))
            {
                m_Database.AddInParameter(dbCommand, "@id", DbType.String, model.Id);
                m_Database.AddInParameter(dbCommand, "@CapId", DbType.String, model.CapId);
                m_Database.AddInParameter(dbCommand, "@Name", DbType.String, model.Name);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.String, model.IsActive);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public List<UploadDocument> GetDocumentCategory()
        {
            List<UploadDocument> ObjCategory = new List<UploadDocument>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETDOCUMENTCATEGORY))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ObjCategory.Add(GetDocumentCategoryFromDataReader(dataReader));
                    }
                }
            }
            return ObjCategory;
        }

        public List<UploadDocument> GetDocumentCategoryForKT()
        {
            List<UploadDocument> ObjCategory = new List<UploadDocument>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETDOCUMENTCATEGORY))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, 1);
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ObjCategory.Add(GetDocumentCategoryFromDataReader(dataReader));
                    }
                }
            }
            return ObjCategory;
        }

        public int SubmitDocumentCategory(UploadDocument model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITDOCUMENTCATEGORY))
            {
                m_Database.AddInParameter(dbCommand, "@id", DbType.String, model.ID);
                m_Database.AddInParameter(dbCommand, "@CapId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@Name", DbType.String, model.Name);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.String, model.IsActive);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int UpdateDocumentCategory(UploadDocument model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITDOCUMENTCATEGORY))
            {
                m_Database.AddInParameter(dbCommand, "@id", DbType.String, model.ID);
                m_Database.AddInParameter(dbCommand, "@Name", DbType.String, model.Name);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.String, model.IsActive);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int FaqUpdateAndSubmit(FaqModel model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITFAQ))
            {
                m_Database.AddInParameter(dbCommand, "@id", DbType.String, model.Id);
                m_Database.AddInParameter(dbCommand, "@FaqQuestion", DbType.String, model.FaqQuestion);
                m_Database.AddInParameter(dbCommand, "@FaqAnswer", DbType.String, model.FaqAnswer);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.String, model.IsActive);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public List<FaqModel> GetFaqList()
        {
            List<FaqModel> ObjCategory = new List<FaqModel>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETFAQLIST))
            {
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ObjCategory.Add(GetFaqListFromDataReader(dataReader));
                    }
                }
            }
            return ObjCategory;
        }

        public int SystemLinkUpdateAndSubmit(SystemLinkModel model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITSYSTEMLINK))
            {
                m_Database.AddInParameter(dbCommand, "@id", DbType.String, model.Id);
                m_Database.AddInParameter(dbCommand, "@Capid", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@Title", DbType.String, model.Title);
                m_Database.AddInParameter(dbCommand, "@Url", DbType.String, model.Url);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.String, model.IsActive);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public List<SystemLinkModel> GetSystemLinkList()
        {
            List<SystemLinkModel> ObjSystemLinkModel = new List<SystemLinkModel>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETSYSTEMLINK))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ObjSystemLinkModel.Add(GetSystemLinkModelFromDataReader(dataReader));
                    }
                }
            }
            return ObjSystemLinkModel;
        }

        public int SubmitBulletin(HttpPostedFileBase FileUpload1, BulletinBoard BulBoard)
        {
            int success = 0;
            string filePath = FileUpload1.FileName;
            string filename1 = Path.GetFileName(filePath);
            string ext = Path.GetExtension(filename1);
            string type = String.Empty;
            switch (ext)
            {
                case ".png":
                    type = "image/png";
                    break;
                case ".jpg":
                    type = "image/jpeg";
                    break;
            }
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITBULLETINBOARD))
            {
                m_Database.AddInParameter(dbCommand, "@Id", DbType.String, BulBoard.Id);
                m_Database.AddInParameter(dbCommand, "@CapId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@Name", DbType.String, filename1);
                m_Database.AddInParameter(dbCommand, "@Title", DbType.String, BulBoard.Title);
                m_Database.AddInParameter(dbCommand, "@Description", DbType.String, BulBoard.Description);
                m_Database.AddInParameter(dbCommand, "@Article", DbType.String, BulBoard.Article);
                m_Database.AddInParameter(dbCommand, "@TYPE", DbType.String, type);
                // m_Database.AddInParameter(dbCommand, "@FilePath", DbType.String, filePath);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.String, BulBoard.IsActive);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public List<BulletinBoard> GetListOfBulletin()
        {
            List<BulletinBoard> objBulletinBoard = new List<BulletinBoard>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETBULLETINBOARD))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objBulletinBoard.Add(GetListOfBulletinFromDataReader(dataReader));
                    }
                }
            }
            return objBulletinBoard;
        }

        public List<Points> GetPoints(string UserNTID)
        {
            List<Points> objPoints = new List<Points>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETPOINT))
            {
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, UserNTID);

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objPoints.Add(GetPointsFromDataReader(dataReader));
                    }
                }
            }
            return objPoints;
        }

        public int TrainingUpdateAndSubmit(Training model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITTRAINING))
            {
                m_Database.AddInParameter(dbCommand, "@id", DbType.String, model.TrainingId);
                m_Database.AddInParameter(dbCommand, "@CapId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@Title", DbType.String, model.Title);
                m_Database.AddInParameter(dbCommand, "@Url", DbType.String, model.Link);
                m_Database.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, model.FromDate);
                m_Database.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, model.ToDate);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.String, model.IsActive);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public List<Training> GetTrainingLinkList()
        {
            List<Training> ObjTraining = new List<Training>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETTRAINING))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ObjTraining.Add(GetTrainingLinkFromDataReader(dataReader));
                    }
                }
            }
            return ObjTraining;
        }

        public int RssFeedUpdateAndSubmit(RSSFeed model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITRSSFEED))
            {
                m_Database.AddInParameter(dbCommand, "@id", DbType.String, model.Id);
                m_Database.AddInParameter(dbCommand, "@CapId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@Title", DbType.String, model.Title);
                m_Database.AddInParameter(dbCommand, "@Url", DbType.String, model.Url);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.String, model.IsActive);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public List<RSSFeed> GetRssFeedList()
        {
            List<RSSFeed> ObjRSSFeed = new List<RSSFeed>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETRSSFEED))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ObjRSSFeed.Add(GetRssFeedListFromDataReader(dataReader));
                    }
                }
            }

            return ObjRSSFeed;
        }

        public List<UploadDocument> GetUploadDocumentType()
        {
            List<UploadDocument> DocumentType = new List<UploadDocument>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITDOCUMENTTYPE))
            {
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        DocumentType.Add(GetUploadDocumentTypeFromDataReader(dataReader));
                    }
                }
            }
            return DocumentType;
        }

        public List<posts> GetpopularPost()
        {
            List<posts> objPost = new List<posts>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETPOPULERPOSTLIST))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objPost.Add(GetpopularPostFromDataReader(dataReader));
                    }
                }
            }
            return objPost;
        }

        public List<KnowledgeTreemModel> GetKnowledgeTree()
        {
            List<KnowledgeTreemModel> objKT = new List<KnowledgeTreemModel>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETKTLIST))
            {
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objKT.Add(GetKnowledgeTreeFromDataReader(dataReader));
                    }
                }
            }
            return objKT;
        }

        public List<Blog> GetpopularBlog()
        {
            List<Blog> objBlog = new List<Blog>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETPOPULERBLOGLIST))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objBlog.Add(GetpopularBlogFromDataReader(dataReader));
                    }
                }
            }
            return objBlog;
        }

        public List<QuizVM> GetQuiz()
        {
            List<QuizVM> ObjLob = new List<QuizVM>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETQUIZ))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ObjLob.Add(GetQuizFromDataReader(dataReader));
                    }
                }
            }
            return ObjLob;
        }

        public List<QuestionVM> GetQuizQuestions()
        {
            List<QuestionVM> ObjQuestionVM = new List<QuestionVM>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETQUIZQUESTIONS))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ObjQuestionVM.Add(GetQuizQuestionsFromDataReader(dataReader));
                    }
                }
            }
            return ObjQuestionVM;
        }

        public int SubmitQuiz(QuizVM model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITQUIZ))
            {
                m_Database.AddInParameter(dbCommand, "@QuizID", DbType.String, model.QuizID);
                m_Database.AddInParameter(dbCommand, "@QuizName", DbType.String, model.QuizName);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.String, model.IsActive);
                m_Database.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, model.FromDate);
                m_Database.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, model.ToDate);
                m_Database.AddInParameter(dbCommand, "@CapId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);
                foreach (var item in GetPointList())
                {
                    if (item.InteractionType == "CreatePKT")
                    {
                        m_Database.AddInParameter(dbCommand, "@Points", DbType.String, item.Point);
                    }
                }
                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int SubmitQuestion(QuestionVM model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITQUESTION))
            {
                m_Database.AddInParameter(dbCommand, "@QuizID", DbType.String, model.QuizID);
                m_Database.AddInParameter(dbCommand, "@QuestionID", DbType.String, model.QuestionID);
                m_Database.AddInParameter(dbCommand, "@QuestionText", DbType.String, model.QuestionText);
                m_Database.AddInParameter(dbCommand, "@ChoiceText1", DbType.String, model.ChoiceText1);
                m_Database.AddInParameter(dbCommand, "@ChoiceText2", DbType.String, model.ChoiceText2);
                if (model.ChoiceText3 != null)
                {
                    m_Database.AddInParameter(dbCommand, "@ChoiceText3", DbType.String, model.ChoiceText3);
                }
                else
                {
                    m_Database.AddInParameter(dbCommand, "@ChoiceText3", DbType.String, "!X");
                }
                if (model.ChoiceText4 != null)
                {
                    m_Database.AddInParameter(dbCommand, "@ChoiceText4", DbType.String, model.ChoiceText4);
                }
                else
                {
                    m_Database.AddInParameter(dbCommand, "@ChoiceText4", DbType.String, "!X");
                }
                m_Database.AddInParameter(dbCommand, "@ChoiceID1", DbType.String, model.ChoiceID1);
                m_Database.AddInParameter(dbCommand, "@ChoiceID2", DbType.String, model.ChoiceID2);
                m_Database.AddInParameter(dbCommand, "@ChoiceID3", DbType.String, model.ChoiceID3);
                m_Database.AddInParameter(dbCommand, "@ChoiceID4", DbType.String, model.ChoiceID4);
                m_Database.AddInParameter(dbCommand, "@AnswerText", DbType.String, model.AnswerText);
                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int SubmitQuesAnswer(int AnswerID, int QuestionID, string AnswerText)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITANSWER))
            {
                m_Database.AddInParameter(dbCommand, "@AnswerID", DbType.String, AnswerID);
                m_Database.AddInParameter(dbCommand, "@QuestionID", DbType.String, QuestionID);
                m_Database.AddInParameter(dbCommand, "@AnswerText", DbType.String, AnswerText);
                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int SubmitAnswer(QuizAnswersVM model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITANSWER))
            {
                m_Database.AddInParameter(dbCommand, "@AnswerID", DbType.String, model.AnswerID);
                m_Database.AddInParameter(dbCommand, "@QuestionID", DbType.String, model.QuestionID);
                m_Database.AddInParameter(dbCommand, "@AnswerText", DbType.String, model.AnswerText);
                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int SubmitChoice(ChoiceVM model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITCHOICES))
            {
                m_Database.AddInParameter(dbCommand, "@ChoiceID", DbType.String, model.ChoiceID);
                m_Database.AddInParameter(dbCommand, "@QuestionID", DbType.String, model.QuestionID);
                m_Database.AddInParameter(dbCommand, "@ChoiceText", DbType.String, model.ChoiceText);
                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int SubmitBadge(BadgeModel Badge, byte[] imageData)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITBADGE))
            {

                m_Database.AddInParameter(dbCommand, "@Id", DbType.String, Badge.Id);
                m_Database.AddInParameter(dbCommand, "@BadgeId", DbType.String, Badge.BadgeId);
                m_Database.AddInParameter(dbCommand, "@BadgeName", DbType.String, Badge.BadgeName);
                m_Database.AddInParameter(dbCommand, "@BadgePoint", DbType.String, Badge.BadgePoint);
                m_Database.AddInParameter(dbCommand, "@BadgePointTo", DbType.String, Badge.BadgePointTo);
                m_Database.AddInParameter(dbCommand, "@BadgeImage", DbType.Binary, imageData);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.String, Badge.IsActive);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@ModifiedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@ModifiedOn", DbType.DateTime, DateTime.Now);


                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public List<BadgeModel> GetListOfBadge()
        {
            List<BadgeModel> objBadge = new List<BadgeModel>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETBADGE))
            {

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objBadge.Add(GetListOfBadgeinFromDataReader(dataReader));
                    }
                }
            }
            return objBadge;
        }

        public List<QuizAnswersVM> GetQuizAnswer()
        {
            List<QuizAnswersVM> ObjAnswerVM = new List<QuizAnswersVM>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETQUIZANSWER))
            {

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ObjAnswerVM.Add(GetQuizAnswerFromDataReader(dataReader));
                    }
                }
            }
            return ObjAnswerVM;
        }

        public List<Followers> GetListFollower(string id)
        {
            List<Followers> objLisFollower = new List<Followers>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETLISTFOLLOWER))
            {
                m_Database.AddInParameter(dbCommand, "@Id", DbType.String, id);
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objLisFollower.Add(GetListFollowerFromDataReader(dataReader));
                    }
                }
            }
            return objLisFollower;
        }

        public List<Points> GetPointList()
        {
            List<Points> objPointList = new List<Points>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETINTERACTIONPOINT))
            {
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objPointList.Add(GetPointListFromDataReader(dataReader));
                    }
                }
            }
            return objPointList;
        }

        public int IsAttendWebExTraing(int TraingId, string TraingTitle)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITISATTENDWEBEX))
            {
                m_Database.AddInParameter(dbCommand, "@TraingId", DbType.String, TraingId);
                m_Database.AddInParameter(dbCommand, "@TraingTitle", DbType.String, TraingTitle);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public List<AssessmentAttend> GetListAssessmentAttend()
        {
            List<AssessmentAttend> objAssessmentAttend = new List<AssessmentAttend>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETLISTASSESSMENTATTEND))
            {
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objAssessmentAttend.Add(GetListAssessmentAttendFromDataReader(dataReader));
                    }
                }
            }
            return objAssessmentAttend;
        }

        public int SubmitAssessment(int QuizId, int Marks)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITASSESSMENT))
            {
                m_Database.AddInParameter(dbCommand, "@QuizId", DbType.String, QuizId);
                m_Database.AddInParameter(dbCommand, "@Marks", DbType.String, Marks);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);

                if (Marks > 90)
                {
                    foreach (var item in GetPointList())
                    {
                        if (item.InteractionType == "PKT>90")
                        {
                            m_Database.AddInParameter(dbCommand, "@Points", DbType.String, item.Point);
                        }
                    }
                }
                if (Marks > 84 && Marks < 91)
                {
                    foreach (var item in GetPointList())
                    {
                        if (item.InteractionType == "PKT>84<91")
                        {
                            m_Database.AddInParameter(dbCommand, "@Points", DbType.String, item.Point);
                        }
                    }
                }
                if (Marks > 79 && Marks < 85)
                {
                    foreach (var item in GetPointList())
                    {
                        if (item.InteractionType == "PKT>79<85")
                        {
                            m_Database.AddInParameter(dbCommand, "@Points", DbType.String, item.Point);
                        }
                    }
                }
                if (Marks < 80)
                {
                    foreach (var item in GetPointList())
                    {
                        if (item.InteractionType == "PKT<80")
                        {
                            m_Database.AddInParameter(dbCommand, "@Points", DbType.String, item.Point);
                        }
                    }
                }
                success = m_Database.ExecuteNonQuery(dbCommand);
            }

            return success;
        }

        public List<ChoiceVM> GetQuizChoice()
        {
            List<ChoiceVM> ObjChoiceVM = new List<ChoiceVM>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETQUIZCHOICE))
            {
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ObjChoiceVM.Add(GetQuizChoiceFromDataReader(dataReader));
                    }
                }
            }
            return ObjChoiceVM;
        }

        public List<ReportModel> GetUtilizationReport(int ReportId, string StartDate, string EndDate, string Criteria, string Capabilities, string CapText)
        {
            if (CapText == "-- ALL --")
            { CapText = "ALL"; }
            else
            { CapText = null; }

            string strProc = string.Empty;
            if (ReportId == Convert.ToInt32(ReportType.RegisteredUsers)) { strProc = DBConstant.PROCGETREGISTEREDUSERS; }
            else if (ReportId == Convert.ToInt32(ReportType.UserTraffic)) { strProc = DBConstant.PROCGETUSERTRAFFIC; }
            else if (ReportId == Convert.ToInt32(ReportType.Interactions)) { strProc = DBConstant.PROCGETINTERACTIONS; }
            else if (ReportId == Convert.ToInt32(ReportType.DocumentsUploaded)) { strProc = DBConstant.PROCGETDOCUMENTSUPLOADED; }
            else if (ReportId == Convert.ToInt32(ReportType.Blogs)) { strProc = DBConstant.PROCGETBLOGS; }
            else if (ReportId == Convert.ToInt32(ReportType.Posts)) { strProc = DBConstant.PROCGETPOSTS; }
            else if (ReportId == Convert.ToInt32(ReportType.FlaggedPosts)) { strProc = DBConstant.PROCGETFLAGGEDPOSTS; }
            List<ReportModel> objReport = new List<ReportModel>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(strProc))
            {
                m_Database.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, SafeTypeHandling.ConvertToDateTime(StartDate));
                m_Database.AddInParameter(dbCommand, "@EndDate", DbType.String, SafeTypeHandling.ConvertToDateTime(EndDate));
                //m_Database.AddInParameter(dbCommand, "@ReportId", DbType.Int32, ReportId);
                m_Database.AddInParameter(dbCommand, "@Criteria", DbType.String, Criteria);
                m_Database.AddInParameter(dbCommand, "@CapText", DbType.String, CapText);
                m_Database.AddInParameter(dbCommand, "@Capid", DbType.String, Capabilities);
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objReport.Add(GetUtilizationReportFromDataReader(dataReader));
                    }
                }
            }
            return objReport;
        }

        public int SubmitUserTraffic()
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCSUBMITUSERTRAFFIC))
            {
                m_Database.AddInParameter(dbCommand, "@CapId", DbType.String, 1);
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public string encrypt(string encryptString)
        {
            string EncryptionKey = "0123456789KMTOPTUMSHIV";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "0123456789KMTOPTUMSHIV";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public List<AssessmentAttend> GetAssessmentResult()
        {
            List<AssessmentAttend> ObjAssessmentResult = new List<AssessmentAttend>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstant.PROCGETASSESSMENTRESULT))

            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ObjAssessmentResult.Add(GetListAssessmentResultFromDataReader(dataReader));
                    }
                }
            }

            return ObjAssessmentResult;
        }

        public bool SendEmail(UserRegistrationModel objUser, string EmailCode)
        {
            bool mailSent = false;
            string mailBody = string.Empty, mailSubject = string.Empty, mailTo = string.Empty, mailCC = string.Empty,
                                                      mailFrom = string.Empty, CurrentItemURL = string.Empty, EmailBodyHTML = string.Empty, admins = string.Empty, SLA = string.Empty;

            EmailTemplates objEmailTemplate = GetEmailTemplates().Where(x => x.Title == EmailCode).FirstOrDefault();
            string val = objEmailTemplate.To;
            EmailBodyHTML = objEmailTemplate.Value;
            mailTo = objEmailTemplate.To.Replace("Employee", objUser.UserEmail);
            mailCC = objEmailTemplate.CC;
            mailSubject = objEmailTemplate.Subject.Replace("[ActionTaken]", objUser.Status == 6 ? "approved" : "rejected");

            if (val != "Employee")
            {
                List<UserRegistrationModel> listOfAdmins = GetListOfRegisteredUser().Where(x => x.IsActive == true && x.RoleId == Convert.ToInt32(RoleType.Admin)
                                                                             && x.Status == Convert.ToInt32(StatusType.Approve) && x.CapabilitiesId == objUser.CapabilitiesId).ToList();
                foreach (var admin in listOfAdmins)
                {
                    admins = admins + admin.UserEmail + ", ";
                }
                if (!string.IsNullOrEmpty(admins))
                {
                    admins = admins.Substring(0, admins.Length - 2);
                    mailTo = mailTo.Replace("Admin", admins);
                }
            }

            mailBody = EmailBodyHTML;
             string siteURL = GetSiteURL(SLA);
            mailBody = mailBody.Replace("[Employee]", objUser.UserName);
            mailBody = mailBody.Replace("[Admin]", objUser.UserName);
            mailBody = mailBody.Replace("[ActionTaken]", objUser.Status == 6 ? "approved" : "rejected");
            mailBody = mailBody.Replace("[ManageUser]", siteURL + "/Admin/ManageUser");
            mailBody = mailBody.Replace("[Link]", siteURL + "/Home/Index");
            mailBody = mailBody.Replace("[ManageBlog]", siteURL + "/Admin/ApproveBlogs");
            mailBody = mailBody.Replace("[ManageDoc]", siteURL + "/Admin/ApproveUploadDoc");
            mailBody = mailBody.Replace("[ManageFeedback]", siteURL + "/Admin/ManageFeedBack");
            mailBody = mailBody.Replace("[ManageFlagPost]", siteURL + "/Admin/ApprovePost");

            mailSent = SendToEmail(mailTo, mailCC, mailSubject, mailBody);
            return mailSent;
        }

        public bool SendEmail(int RoleId, string EmailCode)
        {
            bool mailSent = false;
            string mailBody = string.Empty, mailSubject = string.Empty, mailTo = string.Empty, mailCC = string.Empty, mailFrom = string.Empty,
                                                       CurrentItemURL = string.Empty, EmailBodyHTML = string.Empty, mailListTo = string.Empty, mailListCC = string.Empty, SLA = string.Empty;

            EmailTemplates objEmailTemplate = GetEmailTemplates().Where(x => x.Title == EmailCode).FirstOrDefault();
            EmailBodyHTML = objEmailTemplate.Value;
            mailTo = objEmailTemplate.To.Replace("Admin/Facilitator", Convert.ToString(HttpContext.Current.Session["UserEmailId"]));
            mailCC = objEmailTemplate.CC;
            mailSubject = objEmailTemplate.Subject.Replace("[CreationRole]", RoleId == Convert.ToInt32(RoleType.Admin) ? "admin" : "facilitator");

            List<UserRegistrationModel> objList = GetListOfRegisteredUser().Where(x => x.IsActive == true && x.Status == Convert.ToInt32(StatusType.Approve)
                                              && x.CapabilitiesId == Convert.ToInt32(HttpContext.Current.Session["CapabilitiesId"]) && (x.RoleId == Convert.ToInt32(RoleType.Admin) || x.RoleId == Convert.ToInt32(RoleType.Facilitator))).ToList();

            foreach (var itm in objList)
            {
                if (itm.UserCode != Convert.ToString(HttpContext.Current.Session["UserEmployeeId"]))
                    mailListCC = mailListCC + itm.UserEmail + ", ";
            }
            if (!string.IsNullOrEmpty(mailListCC))
            {
                mailListCC = mailListCC.Substring(0, mailListCC.Length - 2);
                mailCC = mailCC.Replace("OtherAdmin/Facilitator", mailListCC);
            }
            mailBody = EmailBodyHTML.Replace("[CreatedBy]", Convert.ToString(HttpContext.Current.Session["UserLastName"] + ", " + HttpContext.Current.Session["UserFirstName"]));
            mailSent = SendToEmail(mailTo, mailCC, mailSubject, mailBody);

            return mailSent;
        }

        public bool SendEmail(UserRegistrationModel model, string total, string EmailCode)
        {
            bool mailSent = false;
            string mailBody = string.Empty, mailSubject = string.Empty, mailTo = string.Empty, mailCC = string.Empty, mailFrom = string.Empty,
                                                       CurrentItemURL = string.Empty, EmailBodyHTML = string.Empty, mailListTo = string.Empty, mailListCC = string.Empty, SLA = string.Empty;
            List<UserRegistrationModel> objUserList = new List<UserRegistrationModel>();

            EmailTemplates objEmailTemplate = GetEmailTemplates().Where(x => x.Title == EmailCode).FirstOrDefault();
            string val = objEmailTemplate.To;
            EmailBodyHTML = objEmailTemplate.Value;
            mailTo = objEmailTemplate.To.Replace("Employee", model.UserEmail);
            mailCC = objEmailTemplate.CC;
            mailSubject = objEmailTemplate.Subject;

            if (val != "Employee")
                mailTo = objEmailTemplate.To.Replace("Admin", total);

            mailBody = EmailBodyHTML;
            mailBody = mailBody.Replace("[Employee]", model.UserName);
            mailBody = mailBody.Replace("[TotalScore]", total + "%");

            mailSent = SendToEmail(mailTo, mailCC, mailSubject, mailBody);
            return mailSent;
        }

        public bool SendToEmail(string mailTo, string mailCC, string mailSubject, string  mailBody)
        {
            using (var message = new MailMessage("shiva.chauhan@gmail.com", "doctorhub4u@gmail.com"))
            {
                message.Subject = "Message Subject test";
                message.Body = "Message body test at " + DateTime.Now;
                //message.Subject = mailSubject;
                //message.Body = mailBody;
                using (SmtpClient client = new SmtpClient
                {
                    EnableSsl = true,
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Credentials = new NetworkCredential("doctorhub4u@gmail.com", "hub@1234")
                })
                {
                    client.Send(message);
                }
            }
            return true;
        }

        public bool SendEmail(string UserEmail, string UserCode, string UserName, string EmailCode, String Bodytxt, int capid)
        {
            bool mailSent = false;
            string mailBody = string.Empty, mailSubject = string.Empty, mailTo = string.Empty, mailCC = string.Empty,
                                                      mailFrom = string.Empty, CurrentItemURL = string.Empty, EmailBodyHTML = string.Empty, admins = string.Empty, SLA = string.Empty;

            EmailTemplates objEmailTemplate = GetEmailTemplates().Where(x => x.Title == EmailCode).FirstOrDefault();
            string val = objEmailTemplate.To;
            EmailBodyHTML = objEmailTemplate.Value;
            mailTo = objEmailTemplate.To.Replace("Employee", UserEmail);
            mailCC = objEmailTemplate.CC;
            mailSubject = "Having trouble logging in";

            if (val != "Employee")
            {
                List<UserRegistrationModel> listOfAdmins = GetListOfRegisteredUser().Where(x => x.IsActive == true && x.RoleId == Convert.ToInt32(RoleType.Admin)
                                                                             && x.Status == Convert.ToInt32(StatusType.Approve) && x.CapabilitiesId == capid).ToList();
                foreach (var admin in listOfAdmins)
                {
                    admins = admins + admin.UserEmail + ", ";
                }
                if (!string.IsNullOrEmpty(admins))
                {
                    admins = admins.Substring(0, admins.Length - 2);
                    mailTo = mailTo.Replace("Admin", admins);
                }
            }

            mailBody = EmailBodyHTML;
            string siteURL = GetSiteURL(SLA);
            mailBody = mailBody.Replace("[Employee]", UserName);
            mailBody = mailBody.Replace("[EmployeeEmail]", UserEmail);
            mailBody = mailBody.Replace("[Admin]", UserName);
            mailBody = mailBody.Replace("[Message]", Bodytxt);

            mailSent = true;// SendMail.SendEmail(mailTo, mailCC, mailSubject, mailBody);
            return mailSent;
        }

        #endregion

        #region Private Method

        private Menu GetMenuDetailsFromDataReader(IDataReader datareader)
        {
            objMenu = new Menu();
            objMenu.MenuId = SafeTypeHandling.ConvertStringToInt32(datareader["MenuId"]);
            objMenu.MenuName = SafeTypeHandling.ConvertToString(datareader["MenuName"]);
            objMenu.MenuUrl = SafeTypeHandling.ConvertToString(datareader["MenuUrl"]);
            objMenu.ParentId = SafeTypeHandling.ConvertStringToInt32(datareader["ParentId"]);
            objMenu.IsActive = SafeTypeHandling.ConvertStringToBoolean(datareader["IsActive"]);

            return objMenu;
        }

        private GenericModel GetGenericTypeFromDataReader(IDataReader datareader)
        {
            objGenericModel = new GenericModel();
            objGenericModel.Id = SafeTypeHandling.ConvertStringToInt32(datareader["Id"]);
            objGenericModel.Name = SafeTypeHandling.ConvertToString(datareader["Name"]);

            return objGenericModel;
        }

        private UserRegistrationModel GetUserRegistrationDetailsFromDataReader(IDataReader datareader)
        {
            objUserRegistrationModel = new UserRegistrationModel();
            objUserRegistrationModel.Id = SafeTypeHandling.ConvertStringToInt32(datareader["Id"]);
            objUserRegistrationModel.UserCode = SafeTypeHandling.ConvertToString(datareader["UserCode"]);
            objUserRegistrationModel.UserEmail = SafeTypeHandling.ConvertToString(datareader["UserEmail"]);
            objUserRegistrationModel.UserName = SafeTypeHandling.ConvertToString(datareader["UserName"]);
            objUserRegistrationModel.UserNTID = SafeTypeHandling.ConvertToString(datareader["UserNTID"]);
            objUserRegistrationModel.ManagerCode = SafeTypeHandling.ConvertToString(datareader["ManagerCode"]);
            objUserRegistrationModel.ManagerEmail = SafeTypeHandling.ConvertToString(datareader["ManagerEmail"]);
            objUserRegistrationModel.ManagerName = SafeTypeHandling.ConvertToString(datareader["ManagerName"]);
            objUserRegistrationModel.ManagerNTID = SafeTypeHandling.ConvertToString(datareader["ManagerNTID"]);
            objUserRegistrationModel.RoleId = SafeTypeHandling.ConvertStringToInt32(datareader["RoleId"]);
            objUserRegistrationModel.LOBId = SafeTypeHandling.ConvertStringToInt32(datareader["LOBId"]);
            objUserRegistrationModel.Status = SafeTypeHandling.ConvertStringToInt32(datareader["UserStatus"]);
            objUserRegistrationModel.StatusType = SafeTypeHandling.ConvertToString(datareader["StatusType"]);
            objUserRegistrationModel.RoleName = SafeTypeHandling.ConvertToString(datareader["RoleName"]);
            objUserRegistrationModel.LOBName = SafeTypeHandling.ConvertToString(datareader["LOBName"]);
            objUserRegistrationModel.BusinessSegmentName = SafeTypeHandling.ConvertToString(datareader["BsName"]);
            objUserRegistrationModel.CapabilitiesName = SafeTypeHandling.ConvertToString(datareader["CapName"]);
            objUserRegistrationModel.IsActive = SafeTypeHandling.ConvertStringToBoolean(datareader["IsActive"]);
            objUserRegistrationModel.AboutMe = SafeTypeHandling.ConvertToString(datareader["AboutMe"]);
            objUserRegistrationModel.CapabilitiesId = SafeTypeHandling.ConvertStringToInt32(datareader["CapabilitiesId"]);
            objUserRegistrationModel.BusinessSegmentId = SafeTypeHandling.ConvertStringToInt32(datareader["BusinessSegmentId"]);
            objUserRegistrationModel.UserPhoto = (byte[])datareader["UserPhoto"];
            objUserRegistrationModel.ImgStatus = SafeTypeHandling.ConvertStringToInt32(datareader["ImgStatus"]);
            objUserRegistrationModel.IsAdmin = SafeTypeHandling.ConvertStringToBoolean(datareader["IsAdmin"]);

            return objUserRegistrationModel;
        }

        private Feedback GetUserFeedbackFromDataReader(IDataReader datareader)
        {
            objFeebback = new Feedback();
            objFeebback.Id = SafeTypeHandling.ConvertStringToInt32(datareader["Id"]);
            objFeebback.UserCode = SafeTypeHandling.ConvertToString(datareader["UserCode"]);
            objFeebback.UserEmail = SafeTypeHandling.ConvertToString(datareader["UserEmail"]);
            objFeebback.UserName = SafeTypeHandling.ConvertToString(datareader["UserName"]);
            objFeebback.UserNTID = SafeTypeHandling.ConvertToString(datareader["UserNTID"]);
            objFeebback.AdminCode = SafeTypeHandling.ConvertToString(datareader["AdminCode"]);
            objFeebback.AdminEmail = SafeTypeHandling.ConvertToString(datareader["AdminEmail"]);
            objFeebback.AdminName = SafeTypeHandling.ConvertToString(datareader["AdminName"]);
            objFeebback.AdminNTID = SafeTypeHandling.ConvertToString(datareader["AdminNTID"]);
            objFeebback.UserLOB = SafeTypeHandling.ConvertStringToInt32(datareader["UserLOB"]);
            objFeebback.UserLobName = SafeTypeHandling.ConvertToString(datareader["LOBName"]);
            objFeebback.FeedBackId = SafeTypeHandling.ConvertToString(datareader["FeedBackId"]);
            objFeebback.FeedbackQuestion = SafeTypeHandling.ConvertToString(datareader["UserFeedBack"]);
            objFeebback.AdminReply = SafeTypeHandling.ConvertToString(datareader["AdminReply"]);
            objFeebback.FeedbackQuestionDate = SafeTypeHandling.ConvertToString(datareader["CreatedOn"]);
            objFeebback.CreatedOn = SafeTypeHandling.ConvertToDateTime(datareader["CreatedOn"]);
            objFeebback.AdminReplyDate = SafeTypeHandling.ConvertToString(datareader["ModifiedOn"]);
            objFeebback.ModifiedOn = SafeTypeHandling.ConvertToDateTime(datareader["ModifiedOn"]);

            return objFeebback;
        }

        private Notification GetNotificationFromDataReader(IDataReader datareader)
        {
            objNotification = new Notification();
            objNotification.Id = SafeTypeHandling.ConvertStringToInt32(datareader["Id"]);
            objNotification.RoleId = SafeTypeHandling.ConvertToString(datareader["RoleId"]);
            objNotification.UserCode = SafeTypeHandling.ConvertToString(datareader["UserCode"]);
            objNotification.IsAdminFlag = SafeTypeHandling.ConvertStringToBoolean(datareader["IsAdminFlag"]);
            objNotification.IsUserFlag = SafeTypeHandling.ConvertStringToBoolean(datareader["IsUserFlag"]);
            objNotification.IsFacilitatorFlag = SafeTypeHandling.ConvertStringToBoolean(datareader["IsFacilitatorFlag"]);
            objNotification.Identifier = SafeTypeHandling.ConvertToString(datareader["Identifier"]);
            objNotification.AdminDescripation = SafeTypeHandling.ConvertToString(datareader["AdminDescripation"]);
            objNotification.UserDescripation = SafeTypeHandling.ConvertToString(datareader["UserDescripation"]);
            objNotification.CapabilityId = SafeTypeHandling.ConvertStringToInt32(datareader["CapabilityId"]);
            objNotification.CreatedOn = SafeTypeHandling.ConvertToDateTime(datareader["CreatedOn"]);

            return objNotification;
        }

        private EmailTemplates GetEmailTemplatesFromDataReader(IDataReader datareader)
        {
            objEmailTemplates = new EmailTemplates();
            objEmailTemplates.Title = SafeTypeHandling.ConvertToString(datareader["Title"]);
            objEmailTemplates.Value = SafeTypeHandling.ConvertToString(datareader["Value"]);
            objEmailTemplates.To = SafeTypeHandling.ConvertToString(datareader["To"]);
            objEmailTemplates.CC = SafeTypeHandling.ConvertToString(datareader["CC"]);
            objEmailTemplates.Subject = SafeTypeHandling.ConvertToString(datareader["Subject"]);

            return objEmailTemplates;
        }

        private posts GetListOfUserFlagPosFromDataReader(IDataReader datareader)
        {
            objPost = new posts();
            objPost.FlagCount = SafeTypeHandling.ConvertStringToInt32(datareader["FlagCount"]);
            objPost.PostId = SafeTypeHandling.ConvertStringToInt32(datareader["PostId"]);
            objPost.Message = SafeTypeHandling.ConvertToString(datareader["Message"]);
            objPost.PostedBy = SafeTypeHandling.ConvertToString(datareader["Postedby"]);
            objPost.PostedByName = SafeTypeHandling.ConvertToString(datareader["PostedByName"]);
            objPost.PostedDate = SafeTypeHandling.ConvertToString(datareader["PostedDate"]);
            objPost.Status = SafeTypeHandling.ConvertToString(datareader["Status"]);
            objPost.Remarks = SafeTypeHandling.ConvertToString(datareader["Remarks"]);

            return objPost;
        }

        private UploadDocument GetUserUploadDocFromDataReader(IDataReader datareader)
        {
            objUploadDocument = new UploadDocument();
            objUploadDocument.ID = SafeTypeHandling.ConvertStringToInt32(datareader["Id"]);
            objUploadDocument.Message = SafeTypeHandling.ConvertToString(datareader["Message"]);
            objUploadDocument.Title = SafeTypeHandling.ConvertToString(datareader["Title"]);
            objUploadDocument.Category = SafeTypeHandling.ConvertToString(datareader["Category"]);
            objUploadDocument.Name = SafeTypeHandling.ConvertToString(datareader["Name"]);
            objUploadDocument.UploadBy = SafeTypeHandling.ConvertToString(datareader["UploadBy"]);
            objUploadDocument.UserCode = SafeTypeHandling.ConvertToString(datareader["UserCode"]);
            objUploadDocument.UploadByName = SafeTypeHandling.ConvertToString(datareader["UploadByName"]);
            objUploadDocument.Status = SafeTypeHandling.ConvertStringToInt32(datareader["Status"]);
            objUploadDocument.type = SafeTypeHandling.ConvertToString(datareader["type"]);
            objUploadDocument.Remarks = SafeTypeHandling.ConvertToString(datareader["Remarks"]);
            objUploadDocument.UploadDate = SafeTypeHandling.ConvertToString(datareader["UploadDate"]);

            return objUploadDocument;
        }

        private AboutMe GetAboutMeFromDataReader(IDataReader datareader)
        {
            objAboutMe = new AboutMe();
            objAboutMe.AboutTxt = SafeTypeHandling.ConvertToString(datareader["AboutMe"]);

            return objAboutMe;
        }

        private Followers GetMyFollowerFromDataReader(IDataReader datareader)
        {
            objFollower = new Followers();
            objFollower.FollowingBy = SafeTypeHandling.ConvertStringToInt32(datareader["FollowingBy"]);
            objFollower.FollowerBy = SafeTypeHandling.ConvertStringToInt32(datareader["FollowerBy"]);

            return objFollower;
        }

        private Blog GetBlogListFromDataReader(IDataReader datareader)
        {
            objBlog = new Blog();
            objBlog.BlogId = SafeTypeHandling.ConvertStringToInt32(datareader["id"]);
            objBlog.Title = SafeTypeHandling.ConvertToString(datareader["title"]);
            objBlog.Message = SafeTypeHandling.ConvertToString(datareader["Message"]);
            objBlog.BlogerId = SafeTypeHandling.ConvertStringToInt32(datareader["Blogerid"]);
            objBlog.Status = SafeTypeHandling.ConvertStringToInt32(datareader["Status"]);
            objBlog.BlogBy = SafeTypeHandling.ConvertToString(datareader["BlogBy"]);
            objBlog.UserCode = SafeTypeHandling.ConvertToString(datareader["UserCode"]);
            objBlog.Remarks = SafeTypeHandling.ConvertToString(datareader["Remarks"]);
            objBlog.Blogdate = SafeTypeHandling.ConvertToString(datareader["BlogDate"]);
            objBlog.CreatedBy = SafeTypeHandling.ConvertToString(datareader["BlogCreatedBy"]);

            return objBlog;
        }

        private UserRegistrationModel GetProfileImgFromDataReader(IDataReader datareader)
        {
            objUserRegistrationModel = new UserRegistrationModel();
            objUserRegistrationModel.UserPhoto = (byte[])datareader["UserPhoto"];

            return objUserRegistrationModel;
        }

        private Followers GetStatusFollowerFromDataReader(IDataReader datareader)
        {
            objFollower = new Followers();
            objFollower.CountFollower = SafeTypeHandling.ConvertStringToInt32(datareader["count"]);

            return objFollower;
        }

        private UserRegistrationModel GetUserListFromDataReader(IDataReader datareader)
        {
            objUserRegistrationModel = new UserRegistrationModel();
            objUserRegistrationModel.Id = SafeTypeHandling.ConvertStringToInt32(datareader["Id"]);
            objUserRegistrationModel.UserCode = SafeTypeHandling.ConvertToString(datareader["UserCode"]);
            objUserRegistrationModel.ManagerName = SafeTypeHandling.ConvertToString(datareader["ManagerName"]);
            objUserRegistrationModel.UserEmail = SafeTypeHandling.ConvertToString(datareader["UserEmail"]);
            objUserRegistrationModel.UserNTID = SafeTypeHandling.ConvertToString(datareader["UserNTID"]);
            objUserRegistrationModel.UserName = SafeTypeHandling.ConvertToString(datareader["UserName"]);

            return objUserRegistrationModel;
        }

        private Blog GetDocblogListFromDataReader(IDataReader datareader)
        {
            objBlog = new Blog();
            objBlog.Id = SafeTypeHandling.ConvertStringToInt32(datareader["id"]);
            objBlog.Type = SafeTypeHandling.ConvertToString(datareader["type"]);
            objBlog.Title = SafeTypeHandling.ConvertToString(datareader["title"]);
            objBlog.Message = SafeTypeHandling.ConvertToString(datareader["Message"]);
            objBlog.UserName = SafeTypeHandling.ConvertToString(datareader["UserName"]);
            objBlog.UserCode = SafeTypeHandling.ConvertToString(datareader["UserCode"]);
            objBlog.Blogdate = SafeTypeHandling.ConvertToString(datareader["dt"]);

            return objBlog;
        }

        private UserRegistrationModel GetProfileImgListFromDataReader(IDataReader datareader)
        {
            objUserRegistrationModel = new UserRegistrationModel();
            objUserRegistrationModel.Id = SafeTypeHandling.ConvertStringToInt32(datareader["Id"]);
            objUserRegistrationModel.UserCode = SafeTypeHandling.ConvertToString(datareader["UserCode"]);
            objUserRegistrationModel.UserName = SafeTypeHandling.ConvertToString(datareader["UserName"]);
            objUserRegistrationModel.ManagerName = SafeTypeHandling.ConvertToString(datareader["ManagerName"]);
            objUserRegistrationModel.ImgStatus = SafeTypeHandling.ConvertStringToInt32(datareader["ImgStatus"]);
            objUserRegistrationModel.Remarks = SafeTypeHandling.ConvertToString(datareader["Remarks"]);
            objUserRegistrationModel.image = "data:image/png;base64," + Convert.ToBase64String((byte[])datareader["UserPhoto"]);

            return objUserRegistrationModel;
        }

        private posts GetLikeCountFromDataReader(IDataReader datareader)
        {
            objPost = new posts();
            objPost.PostId = SafeTypeHandling.ConvertStringToInt32(datareader["PostId"]);
            objPost.LikeBy = SafeTypeHandling.ConvertToString(datareader["LikeBy"]);
            objPost.Identifier = SafeTypeHandling.ConvertToString(datareader["Identifier"]);

            return objPost;
        }

        private posts GetPostFromDataReader(IDataReader datareader)
        {
            objPost = new posts();
            objPost.PostId = SafeTypeHandling.ConvertStringToInt32(datareader["PostId"]);
            objPost.Message = SafeTypeHandling.ConvertToString(datareader["Message"]);
            objPost.PostedBy = SafeTypeHandling.ConvertToString(datareader["Postedby"]);
            objPost.UserCode = SafeTypeHandling.ConvertToString(datareader["UserCode"]);
            objPost.PostedByName = SafeTypeHandling.ConvertToString(datareader["PostedByName"]);
            objPost.PostedDate = SafeTypeHandling.ConvertToString(datareader["PostedDate"]);
            objPost.Status = SafeTypeHandling.ConvertToString(datareader["Status"]);
            objPost.Remarks = SafeTypeHandling.ConvertToString(datareader["Remarks"]);

            return objPost;
        }

        private posts GetListOfPostFromDataReader(IDataReader datareader)
        {
            objPost = new posts();
            objPost.PostId = SafeTypeHandling.ConvertStringToInt32(datareader["PostId"]);
            objPost.PostedBy = SafeTypeHandling.ConvertToString(datareader["Postedby"]);

            return objPost;
        }

        private posts GetCommentListOnPostFromDataReader(IDataReader datareader)
        {
            objPost = new posts();
            objPost.CommentId = SafeTypeHandling.ConvertStringToInt32(datareader["CommentId"]);
            objPost.PostId = SafeTypeHandling.ConvertStringToInt32(datareader["PostId"]);
            objPost.Message = SafeTypeHandling.ConvertToString(datareader["Message"]);
            objPost.UserCode = SafeTypeHandling.ConvertToString(datareader["UserCode"]);
            objPost.CommentedBy = SafeTypeHandling.ConvertToString(datareader["CommentedBy"]);
            objPost.CommentedByName = SafeTypeHandling.ConvertToString(datareader["CommentedByName"]);
            objPost.CommentedDate = SafeTypeHandling.ConvertToString(datareader["CommentedDate"]);
            objPost.Identifier = SafeTypeHandling.ConvertToString(datareader["Identifier"]);

            return objPost;
        }

        private BusinessSegmentModel GetBusinessSegmentFromDataReader(IDataReader datareader)
        {
            BusinessSegmentModel objbs = new BusinessSegmentModel();
            objbs.Id = Convert.ToInt32(datareader["Id"]);
            objbs.Name = SafeTypeHandling.ConvertToString(datareader["Name"]);
            objbs.Identifier = SafeTypeHandling.ConvertToString(datareader["Identifier"]);
            objbs.IsActive = SafeTypeHandling.ConvertStringToBoolean(datareader["IsActive"]);
            objbs.CreatedBy = SafeTypeHandling.ConvertToString(datareader["CreatedBy"]);
            objbs.CreatedOn = SafeTypeHandling.ConvertToDateTime(datareader["CreatedOn"]);
            objbs.ModifiedBy = SafeTypeHandling.ConvertToString(datareader["ModifiedBy"]);
            objbs.ModifiedOn = SafeTypeHandling.ConvertToDateTime(datareader["ModifiedOn"]);

            return objbs;
        }

        private CapabilitiesModel GetCapabilitiesFromDataReader(IDataReader datareader)
        {
            CapabilitiesModel objcap = new CapabilitiesModel();
            objcap.Id = Convert.ToInt32(datareader["Id"]);
            objcap.BsId = Convert.ToInt32(datareader["BsId"]);
            objcap.BsName = SafeTypeHandling.ConvertToString(datareader["BsName"]);
            objcap.Name = SafeTypeHandling.ConvertToString(datareader["Name"]);
            objcap.Identifier = SafeTypeHandling.ConvertToString(datareader["Identifier"]);
            objcap.IsActive = SafeTypeHandling.ConvertStringToBoolean(datareader["IsActive"]);
            objcap.CreatedBy = SafeTypeHandling.ConvertToString(datareader["CreatedBy"]);
            objcap.CreatedOn = SafeTypeHandling.ConvertToDateTime(datareader["CreatedOn"]);
            objcap.ModifiedBy = SafeTypeHandling.ConvertToString(datareader["ModifiedBy"]);
            objcap.ModifiedOn = SafeTypeHandling.ConvertToDateTime(datareader["ModifiedOn"]);

            return objcap;
        }

        private LobModel GetLobFromDataReader(IDataReader datareader)
        {
            LobModel ObjLob = new LobModel();
            ObjLob.Id = Convert.ToInt32(datareader["Id"]);
            ObjLob.CapId = Convert.ToInt32(datareader["CapId"]);
            ObjLob.Name = SafeTypeHandling.ConvertToString(datareader["Name"]);
            ObjLob.BsName = SafeTypeHandling.ConvertToString(datareader["BsName"]);
            ObjLob.CapName = SafeTypeHandling.ConvertToString(datareader["CapName"]);
            ObjLob.Identifier = SafeTypeHandling.ConvertToString(datareader["Identifier"]);
            ObjLob.IsActive = SafeTypeHandling.ConvertStringToBoolean(datareader["IsActive"]);
            ObjLob.CreatedBy = SafeTypeHandling.ConvertToString(datareader["CreatedBy"]);
            ObjLob.CreatedOn = SafeTypeHandling.ConvertToDateTime(datareader["CreatedOn"]);
            ObjLob.ModifiedBy = SafeTypeHandling.ConvertToString(datareader["ModifiedBy"]);
            ObjLob.ModifiedOn = SafeTypeHandling.ConvertToDateTime(datareader["ModifiedOn"]);

            return ObjLob;
        }

        private UploadDocument GetDocumentCategoryFromDataReader(IDataReader datareader)
        {
            UploadDocument ObjCategory = new UploadDocument();

            ObjCategory.ID = Convert.ToInt32(datareader["Id"]);
            ObjCategory.Name = SafeTypeHandling.ConvertToString(datareader["Name"]);
            ObjCategory.IsActive = SafeTypeHandling.ConvertStringToBoolean(datareader["IsActive"]);
            ObjCategory.CreatedBy = SafeTypeHandling.ConvertToString(datareader["CreatedBy"]);
            ObjCategory.CreatedOn = SafeTypeHandling.ConvertToDateTime(datareader["CreatedOn"]);
            ObjCategory.ModifiedBy = SafeTypeHandling.ConvertToString(datareader["ModifiedBy"]);
            ObjCategory.ModifiedOn = SafeTypeHandling.ConvertToDateTime(datareader["ModifiedOn"]);

            return ObjCategory;
        }

        private FaqModel GetFaqListFromDataReader(IDataReader datareader)
        {
            FaqModel ObjFaq = new FaqModel();
            ObjFaq.Id = Convert.ToInt32(datareader["Id"]);
            ObjFaq.FaqQuestion = SafeTypeHandling.ConvertToString(datareader["FaqQuestion"]);
            ObjFaq.FaqAnswer = SafeTypeHandling.ConvertToString(datareader["FaqAnswer"]);
            ObjFaq.IsActive = SafeTypeHandling.ConvertStringToBoolean(datareader["IsActive"]);
            ObjFaq.CreatedBy = SafeTypeHandling.ConvertToString(datareader["CreatedBy"]);
            ObjFaq.CreatedOn = SafeTypeHandling.ConvertToDateTime(datareader["CreatedOn"]);
            ObjFaq.ModifiedBy = SafeTypeHandling.ConvertToString(datareader["ModifiedBy"]);
            ObjFaq.ModifiedOn = SafeTypeHandling.ConvertToDateTime(datareader["ModifiedOn"]);

            return ObjFaq;
        }

        private SystemLinkModel GetSystemLinkModelFromDataReader(IDataReader datareader)
        {
            SystemLinkModel SystemLink = new SystemLinkModel();
            SystemLink.Id = Convert.ToInt32(datareader["Id"]);
            SystemLink.Title = SafeTypeHandling.ConvertToString(datareader["Title"]);
            SystemLink.Url = SafeTypeHandling.ConvertToString(datareader["Url"]);
            SystemLink.IsActive = SafeTypeHandling.ConvertStringToBoolean(datareader["IsActive"]);
            SystemLink.CreatedBy = SafeTypeHandling.ConvertToString(datareader["CreatedBy"]);
            SystemLink.CreatedOn = SafeTypeHandling.ConvertToDateTime(datareader["CreatedOn"]);
            SystemLink.ModifiedBy = SafeTypeHandling.ConvertToString(datareader["ModifiedBy"]);
            SystemLink.ModifiedOn = SafeTypeHandling.ConvertToDateTime(datareader["ModifiedOn"]);

            return SystemLink;
        }

        private BulletinBoard GetListOfBulletinFromDataReader(IDataReader datareader)
        {
            objBulletinBoard = new BulletinBoard();
            objBulletinBoard.Id = SafeTypeHandling.ConvertStringToInt32(datareader["Id"]);
            objBulletinBoard.Name = SafeTypeHandling.ConvertToString(datareader["Name"]);
            objBulletinBoard.Title = SafeTypeHandling.ConvertToString(datareader["Title"]);
            objBulletinBoard.Description = SafeTypeHandling.ConvertToString(datareader["Description"]);
            objBulletinBoard.Article = SafeTypeHandling.ConvertToString(datareader["Article"]);
            objBulletinBoard.type = SafeTypeHandling.ConvertToString(datareader["type"]);
            objBulletinBoard.IsActive = SafeTypeHandling.ConvertStringToBoolean(datareader["IsActive"]);
            objBulletinBoard.CreatedBy = SafeTypeHandling.ConvertToString(datareader["CreatedBy"]);
            objBulletinBoard.CreatedOn = SafeTypeHandling.ConvertToDateTime(datareader["CreatedOn"]);
            objBulletinBoard.ModifiedBy = SafeTypeHandling.ConvertToString(datareader["ModifiedBy"]);
            objBulletinBoard.ModifiedOn = SafeTypeHandling.ConvertToDateTime(datareader["ModifiedOn"]);

            return objBulletinBoard;
        }

        private Points GetPointsFromDataReader(IDataReader datareader)
        {
            Points objPoints = new Points();
            objPoints.Point = SafeTypeHandling.ConvertStringToInt32(datareader["Point"]);
            objPoints.CreatedBy = SafeTypeHandling.ConvertToString(datareader["Userid"]);

            return objPoints;
        }

        private RSSFeed GetRssFeedListFromDataReader(IDataReader datareader)
        {
            RSSFeed RSSFeed = new RSSFeed();
            RSSFeed.Id = Convert.ToInt32(datareader["Id"]);
            RSSFeed.Title = SafeTypeHandling.ConvertToString(datareader["Title"]);
            RSSFeed.Url = SafeTypeHandling.ConvertToString(datareader["Url"]);
            RSSFeed.IsActive = SafeTypeHandling.ConvertStringToBoolean(datareader["IsActive"]);
            RSSFeed.CreatedBy = SafeTypeHandling.ConvertToString(datareader["CreatedBy"]);
            RSSFeed.CreatedOn = SafeTypeHandling.ConvertToDateTime(datareader["CreatedOn"]);
            RSSFeed.ModifiedBy = SafeTypeHandling.ConvertToString(datareader["ModifiedBy"]);
            RSSFeed.ModifiedOn = SafeTypeHandling.ConvertToDateTime(datareader["ModifiedOn"]);

            return RSSFeed;
        }

        private posts GetpopularPostFromDataReader(IDataReader datareader)
        {
            objPost = new posts();
            objPost.PostId = SafeTypeHandling.ConvertStringToInt32(datareader["PostId"]);
            objPost.Message = SafeTypeHandling.ConvertToString(datareader["Message"]);
            objPost.UserCode = SafeTypeHandling.ConvertToString(datareader["UserCode"]);
            objPost.PostedByName = SafeTypeHandling.ConvertToString(datareader["PostedByName"]);

            return objPost;
        }

        private Training GetTrainingLinkFromDataReader(IDataReader datareader)
        {
            Training ObjTrainingLink = new Training();
            ObjTrainingLink.TrainingId = Convert.ToInt32(datareader["Id"]);
            ObjTrainingLink.Title = SafeTypeHandling.ConvertToString(datareader["Title"]);
            ObjTrainingLink.Link = SafeTypeHandling.ConvertToString(datareader["Url"]);
            ObjTrainingLink.FromDate = SafeTypeHandling.ConvertToDateTime(datareader["FromDate"]);
            ObjTrainingLink.ToDate = SafeTypeHandling.ConvertToDateTime(datareader["ToDate"]);
            ObjTrainingLink.IsActive = SafeTypeHandling.ConvertStringToBoolean(datareader["IsActive"]);
            ObjTrainingLink.CreatedBy = SafeTypeHandling.ConvertToString(datareader["CreatedBy"]);
            ObjTrainingLink.CreatedOn = SafeTypeHandling.ConvertToDateTime(datareader["CreatedOn"]);
            ObjTrainingLink.ModifiedBy = SafeTypeHandling.ConvertToString(datareader["ModifiedBy"]);
            ObjTrainingLink.ModifiedOn = SafeTypeHandling.ConvertToDateTime(datareader["ModifiedOn"]);

            return ObjTrainingLink;
        }

        private UploadDocument GetUploadDocumentTypeFromDataReader(IDataReader datareader)
        {
            UploadDocument UploadDocumenType = new UploadDocument();
            UploadDocumenType.type = SafeTypeHandling.ConvertToString(datareader["DOCUTYPE"]);

            return UploadDocumenType;
        }

        //private ManagePoll GetManagePolls(IDataReader datareader)
        //{
        //    ManagePoll objManagePoll = new ManagePoll();
        // 
        //        objManagePoll.PollID = SafeTypeHandling.ConvertStringToInt32(datareader["PollID"]);
        //        objManagePoll.Questions = SafeTypeHandling.ConvertToString(datareader["Title"]);
        //        string splitString = SafeTypeHandling.ConvertToString(datareader["Options"]);
        //        foreach (var item in splitString.Replace("~", "<br/>"))
        //        {
        //            objManagePoll.Options += item.ToString();
        //        }
        //        objManagePoll.FromDate = SafeTypeHandling.ConvertToDateTime(datareader["FromDate"]);
        //        objManagePoll.ToDate = SafeTypeHandling.ConvertToDateTime(datareader["ToDate"]);
        //        objManagePoll.IsActive = SafeTypeHandling.ConvertStringToBoolean(datareader["IsActive"]);
        //   
        //    return objManagePoll;
        //}

        //private ManagePoll GetPolls(IDataReader datareader)
        //{
        //    ManagePoll objPolls = new ManagePoll();
        //           //        objPolls.Title = SafeTypeHandling.ConvertToString(datareader["Title"]);
        //        //string splitString = SafeTypeHandling.ConvertToString(datareader["Options"]);
        //        //foreach (var item in splitString.Replace("~", "<br/>"))
        //        //{
        //        //    objPolls.Options += item.ToString();
        //        //}
        //        objPolls.Options = SafeTypeHandling.ConvertToString(datareader["Options"]);
        //        objPolls.FromDate = SafeTypeHandling.ConvertToDateTime(datareader["FromDate"]);
        //        objPolls.ToDate = SafeTypeHandling.ConvertToDateTime(datareader["ToDate"]);
        //    
        //    return objPolls;
        //}

        //private Poll GetUserVoteResult(IDataReader datareader)
        //{
        //    Poll objPolls = new Poll();
        //    
        //        objPolls.Title = SafeTypeHandling.ConvertToString(datareader["Question"]);
        //        objPolls.Options = SafeTypeHandling.ConvertToString(datareader["Options"]);
        //    
        //    return objPolls;
        //}

        //private ManagePoll GetManagePollsByID(IDataReader datareader)
        //{
        //    ManagePoll objManagePoll = new ManagePoll();
        //   
        //        objManagePoll.PollID = SafeTypeHandling.ConvertStringToInt32(datareader["PollID"]);
        //        objManagePoll.Questions = SafeTypeHandling.ConvertToString(datareader["Title"]);
        //        string splitString = SafeTypeHandling.ConvertToString(datareader["Options"]);
        //        foreach (var item in splitString.Replace("~", "\n"))
        //        {
        //            objManagePoll.Options += item.ToString();
        //        }
        //        objManagePoll.FromDate = SafeTypeHandling.ConvertToDateTime(datareader["FromDate"]);
        //        objManagePoll.ToDate = SafeTypeHandling.ConvertToDateTime(datareader["ToDate"]);
        //        objManagePoll.IsActive = SafeTypeHandling.ConvertStringToBoolean(datareader["IsActive"]);
        //    
        //    return objManagePoll;
        //}

        //private Poll GetUserVoteResultCount(IDataReader datareader)
        //{
        //    Poll objPolls = new Poll();
        //    
        //        objPolls.Title = SafeTypeHandling.ConvertToString(datareader["Question"]);
        //        objPolls.Options = SafeTypeHandling.ConvertToString(datareader["Options"]);
        //        objPolls.VotePercentage = SafeTypeHandling.ConvertStringToInt32(datareader["VotePerc"]);
        //   
        //    return objPolls;
        //}

        private Blog GetpopularBlogFromDataReader(IDataReader datareader)
        {
            objBlog = new Blog();
            objBlog.BlogId = SafeTypeHandling.ConvertStringToInt32(datareader["Id"]);
            objBlog.Message = SafeTypeHandling.ConvertToString(datareader["Message"]);
            objBlog.UserCode = SafeTypeHandling.ConvertToString(datareader["UserCode"]);
            objBlog.BlogBy = SafeTypeHandling.ConvertToString(datareader["BlogBy"]);

            return objBlog;
        }

        private KnowledgeTreemModel GetKnowledgeTreeFromDataReader(IDataReader datareader)
        {
            KnowledgeTreemModel objKT = new KnowledgeTreemModel();
            objKT.DOCMENTID = SafeTypeHandling.ConvertStringToInt32(datareader["DOCMENTID"]);
            objKT.IMAGENAME = SafeTypeHandling.ConvertToString(datareader["IMAGENAME"]);
            objKT.DOCUTYPE = SafeTypeHandling.ConvertToString(datareader["DOCUTYPE"]);
            objKT.CATEGORYNAME = SafeTypeHandling.ConvertToString(datareader["CATEGORYNAME"]);
            objKT.LOBName = SafeTypeHandling.ConvertToString(datareader["LOBName"]);
            objKT.UserName = SafeTypeHandling.ConvertToString(datareader["UserName"]);
            objKT.UserCode = SafeTypeHandling.ConvertToString(datareader["UserCode"]);
            objKT.TITLE = SafeTypeHandling.ConvertToString(datareader["TITLE"]);
            objKT.Tags = SafeTypeHandling.ConvertToString(datareader["Tag"]);

            return objKT;
        }

        private QuizVM GetQuizFromDataReader(IDataReader datareader)
        {
            QuizVM ObjQuiz = new QuizVM();
            ObjQuiz.QuizID = Convert.ToInt32(datareader["QuizID"]);
            ObjQuiz.QuizName = SafeTypeHandling.ConvertToString(datareader["QuizName"]);
            ObjQuiz.FromDate = SafeTypeHandling.ConvertToDateTime(datareader["FromDate"]);
            ObjQuiz.ToDate = SafeTypeHandling.ConvertToDateTime(datareader["ToDate"]);
            ObjQuiz.IsActive = SafeTypeHandling.ConvertStringToBoolean(datareader["IsActive"]);
            ObjQuiz.CreatedBy = SafeTypeHandling.ConvertToString(datareader["CreatedBy"]);
            ObjQuiz.CreatedByMailId = SafeTypeHandling.ConvertToString(datareader["CreatedEmail"]);
            ObjQuiz.Assessment = SafeTypeHandling.ConvertToString(encrypt(datareader["QuizID"].ToString()));

            return ObjQuiz;
        }

        private QuestionVM GetQuizQuestionsFromDataReader(IDataReader datareader)
        {
            QuestionVM ObjQuestionVM = new QuestionVM();
            ObjQuestionVM.QuizID = Convert.ToInt32(datareader["QuizID"]);
            ObjQuestionVM.QuestionID = Convert.ToInt32(datareader["QuestionID"]);
            ObjQuestionVM.QuestionText = SafeTypeHandling.ConvertToString(datareader["QuestionText"]);
            ObjQuestionVM.QuizName = SafeTypeHandling.ConvertToString(datareader["QuizName"]);
            ObjQuestionVM.AnswerText = SafeTypeHandling.ConvertToString(datareader["AnswerText"]);
            ObjQuestionVM.ChoiceText = SafeTypeHandling.ConvertToString(datareader["ChoiceText"]);
            ObjQuestionVM.ChoicestringID = SafeTypeHandling.ConvertToString(datareader["ChoiceID"]);

            return ObjQuestionVM;
        }

        private ChoiceVM GetQuizChoiceFromDataReader(IDataReader datareader)
        {
            Random rnd = new Random();
            ChoiceVM ObjChoiceVM = new ChoiceVM();
            ObjChoiceVM.QuestionID = Convert.ToInt32(datareader["QuestionID"]);
            ObjChoiceVM.QuestionText = SafeTypeHandling.ConvertToString(datareader["QuestionText"]);
            int index = rnd.Next(Convert.ToInt32(datareader["ChoiceID"]));
            ObjChoiceVM.ChoiceID = index;// Convert.ToInt32(datareader["ChoiceID"]);
            ObjChoiceVM.ChoiceText = SafeTypeHandling.ConvertToString(datareader["ChoiceText"]);

            return ObjChoiceVM;
        }

        private QuizAnswersVM GetQuizAnswerFromDataReader(IDataReader datareader)
        {
            objQuizAnswers = new QuizAnswersVM();
            objQuizAnswers.QuestionID = Convert.ToInt32(datareader["QuestionID"]);
            objQuizAnswers.AnswerID = Convert.ToInt32(datareader["AnswerID"]);
            objQuizAnswers.AnswerText = SafeTypeHandling.ConvertToString(datareader["AnswerText"]);
            objQuizAnswers.QuestionText = SafeTypeHandling.ConvertToString(datareader["QuestionText"]);

            return objQuizAnswers;
        }

        private BadgeModel GetListOfBadgeinFromDataReader(IDataReader datareader)
        {
            BadgeModel objBadge = new BadgeModel();

            objBadge.Id = SafeTypeHandling.ConvertStringToInt32(datareader["Id"]);
            objBadge.BadgeId = SafeTypeHandling.ConvertStringToInt32(datareader["BadgeId"]);
            objBadge.BadgeName = SafeTypeHandling.ConvertToString(datareader["BadgeName"]);
            objBadge.BadgePoint = SafeTypeHandling.ConvertStringToInt32(datareader["BadgePoint"]);
            objBadge.BadgePointTo = SafeTypeHandling.ConvertStringToInt32(datareader["BadgePointTo"]);
            Byte[] bytes = (byte[])datareader["BadgeImage"];
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            objBadge.BImage = base64String;
            objBadge.IsActive = SafeTypeHandling.ConvertStringToBoolean(datareader["IsActive"]);
            objBadge.CreatedBy = SafeTypeHandling.ConvertToString(datareader["CreatedBy"]);
            objBadge.CreatedOn = SafeTypeHandling.ConvertToDateTime(datareader["CreatedOn"]);
            objBadge.ModifiedBy = SafeTypeHandling.ConvertToString(datareader["ModifiedBy"]);
            objBadge.ModifiedOn = SafeTypeHandling.ConvertToDateTime(datareader["ModifiedOn"]);

            return objBadge;
        }

        private Followers GetListFollowerFromDataReader(IDataReader datareader)
        {
            objFollower = new Followers();
            objFollower.Id = SafeTypeHandling.ConvertToString(datareader["ID"]);
            objFollower.UserName = SafeTypeHandling.ConvertToString(datareader["UserName"]);
            objFollower.UserCode = SafeTypeHandling.ConvertToString(datareader["UserCode"]);
            objFollower.status = SafeTypeHandling.ConvertToString(datareader["status"]);

            return objFollower;
        }

        private Points GetPointListFromDataReader(IDataReader datareader)
        {
            Points objPointList = new Points();
            objPointList.InteractionType = SafeTypeHandling.ConvertToString(datareader["InteractionType"]);
            objPointList.Point = SafeTypeHandling.ConvertStringToInt32(datareader["Point"]);

            return objPointList;
        }

        private AssessmentAttend GetListAssessmentAttendFromDataReader(IDataReader datareader)
        {
            AssessmentAttend objAssessmentAttend = new AssessmentAttend();
            objAssessmentAttend.status = SafeTypeHandling.ConvertToString(datareader["status"]);
            objAssessmentAttend.QuizID = SafeTypeHandling.ConvertStringToInt32(datareader["QuizId"]);
            objAssessmentAttend.AttendCount = SafeTypeHandling.ConvertStringToInt32(datareader["Attend"]);
            objAssessmentAttend.Marks = SafeTypeHandling.ConvertStringToInt32(datareader["Marks"]);

            return objAssessmentAttend;
        }

        private ReportModel GetUtilizationReportFromDataReader(IDataReader datareader)
        {
            objReport = new ReportModel();
            objReport.Interval = SafeTypeHandling.ConvertToString(datareader["Interval"]);
            objReport.Utilization = Convert.ToInt32(datareader["Utilization"]);

            return objReport;
        }

        private AssessmentAttend GetListAssessmentResultFromDataReader(IDataReader datareader)
        {
            AssessmentAttend objAssessmentResult = new AssessmentAttend();
            objAssessmentResult.UserName = SafeTypeHandling.ConvertToString(datareader["UserName"]);
            objAssessmentResult.QuizName = SafeTypeHandling.ConvertToString(datareader["QuizName"]);
            objAssessmentResult.Marks = SafeTypeHandling.ConvertStringToInt32(datareader["Marks"]);
            objAssessmentResult.status = SafeTypeHandling.ConvertToString(datareader["status"]);

            return objAssessmentResult;
        }

        #endregion
    }

}
