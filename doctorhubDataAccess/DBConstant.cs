using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubDataAccess
{
    public class DBConstant
    {
        public const string Connectstring = "HubConnect";

        public const string PROCSAVEERROR = "uspSaveError";
        public const string PROCGETMENULIST = "uspGetMenuList";
        public const string PROCGETGENERICDETAILS = "uspGetGenericList";
        public const string PROCGETLISTOFREGISTEREDUSER = "uspGetListOfRegisteredUser";
        public const string PROCGETLISTOFUSERFEEDBACK = "uspGetListOfUserFeedback";
        public const string PROCGETLISTOFADMINFEEDBACK = "uspGetListOfAdminFeedback";
        public const string PROCSUBMITUSERREQUEST = "uspSubmitUserRequest";
        public const string PROCCREATEUSERPROFILE = "uspCreateUserProfile";
        public const string PROCSUBMITUSERFEEDBACK = "uspSubmitUserFeedback";
        public const string PROCUPDATEUSERDETAILS = "uspUpdateUserDetails";
        public const string PROCUPDATEUSERREQUESTSTATUS = "uspUpdateUserRequestStatus";
        public const string PROCUPDATEADMINFEEDBACK = "uspUpdateAdminFeedback";
        public const string PROCUPDATEADMINREPLY = "uspUpdateAdminReply";
        public const string PROCGETEMAILTEMPLATES = "uspGetEmailTemplates";
        public const string PROCGETLISTOFPOST = "uspGetListOfPost";


        public const string PROCGETLISTOFNOTIFICATION = "uspGetListOfNotification";
        public const string PROCUPDATENOTIFICATIONCOUNT = "uspUpdateNotificationCount";
        public const string PROCGETLISTOFNOTIFICATIONDETAILS = "uspGetListOfNotificationDetails";

        public const string PROCADDAPPROVER = "uspAddApprover";
        public const string PROCEDITAPPROVER = "uspEditApprover";
        public const string PROCGETAPPROVER = "uspGetApprover";

        //  Post  Proc
        public const string PROCSUBMITPOST = "uspSubmitPost";
        public const string PROCGETPOSTLIST = "uspGetPostList";
        public const string PROCDELETEPOST = "uspDeletePost";
        public const string PROCUNFLAGEPOST = "uspUnFlagFlagPost";
        public const string PROCGETPOSTLIKE = "uspGetLike";
        public const string PROCGETPOSTLIKECOUNT = "uspGetLikeCount";
        public const string PROCSUBMITPOSTFLAG = "uspSubmitPostFlag";
        // comment Proc and like proc
        public const string PROCSUBMITCOMMENT = "uspSubmitComment";
        public const string PROCSUBMITLIKE = "uspSubmitLike";


        public const string PROCGETCOMMENTLIST = "uspGetCommentList";
        public const string PROCGETCOMMENT = "uspGetComment";
        // About Me Proc
        public const string PROCUPDATEABOUTME = "uspUpdateAboutMe";
        public const string PROCGETABOUTME = "uspGetAboutMe";
        // followe and following Proc
        public const string PROCGETLISTFOLLOWER = "uspGetListfollower";
        public const string PROCGETMYFOLLOWER = "uspGetMyfollower";
        public const string PROCGETFOLLOWERCOUNT = "uspGetFollowerCount";
        public const string PROCUPDATEFOLLOWER = "uspUpdateFollowerCount";
        //Blog Proc
        public const string PROCSUBMITBLOGPOST = "uspSubmitBlogPost";
        public const string PROCGETBLOGLIST = "uspGetBlogList";
        public const string PROCDELETEBLOGPOST = "uspDeleteBlogPost";
        public const string PROUPDATEBLOGSSTATUS = "uspUpdateBlogStatus";




        //Profile Image proc
        public const string PROCGETPROFILEIMG = "uspGetUserProfileImg";
        public const string PROCUPDATEUSERPROFILE = "uspUpdateUserProfileImg";
        public const string PROCGETPROFILEIMGLIST = "uspGetUserProfileImgList";
        public const string PROUPDATEIMGSTATUS = "uspUpdateImgStatus";
        // User search result proc 
        public const string PROCGETSEARCHUSERLIST = "uspGetSearchUser";
        public const string PROCGETLISTFLAGPOSTLIST = "uspGetFlagPostList";
        // latest 7 doc and bolg post detail prc
        public const string PROCGETLISTOFPOSTDDOC = "uspGetLatestBlogDoc";
        // Document  uplod delete proc


        public const string PROCSUBMITDOCUMENT = "uspSubmitDocument";
        public const string PROCGETLISTOFUPLOADDOC = "uspGetListOfUploadDoc";
        public const string PROCDOWNLOADDOCUMENT = "uspDownloadDoument";
        public const string PROUPDATEUPLOADDOCSTATUS = "uspUpdateUploadDocStatus";


        // CreManageate BussinessSegment  --

        public const string PROCGETBUSINESSSEGMENT = "uspGetBusinessSegment";
        public const string PROCSUBMITBUSINESSSEGMENT = "uspSubmitBusinessSegment";

        // Manage CAPABILITIES  --

        public const string PROCGETCAPABILITIES = "uspGetCAPABILITIES";
        public const string PROCSUBMITCAPABILITIES = "uspSubmitKMTCapabilities";

        // Manage Lob  --

        public const string PROCGETLOB = "uspGetLob";
        public const string PROCSUBMITLOB = "uspSubmitKMTLob";

        //Added for poll
        public const string PROCSUBMITMANAEPOLL = "uspCreatePoll";
        public const string PROCGETManagePoll = "uspGetManagePolls";
        public const string PROCDELETEManagePoll = "uspDeleteManagePolls";
        public const string PROCGETPOLLS = "uspGetPolls";
        public const string PROCSUBMITVOTE = "uspSubmitVote";
        public const string PROCGETUSERVOTERESULT = "uspGetUserVoteResult";
        public const string PROCGETUSERVOTERESULTCOUNT = "uspVoteResultCount";
        public const string PROCGETManagePollBYID = "uspGetManagePollsByID";
        public const string PROCUPDATEManagePoll = "uspUpdateManagePolls";
        // End

        // Create DocumentCategory  --

        public const string PROCGETDOCUMENTCATEGORY = "uspGetDocumentCategory";
        public const string PROCSUBMITDOCUMENTCATEGORY = "uspSubmitDocumentCategory";
        public const string PROCSUBMITDOCUMENTTYPE = "uspGetDocumentType";

        // Manage FAQ  -

        public const string PROCGETFAQLIST = "uspGetFaqList";
        public const string PROCSUBMITFAQ = "uspSubmitFaq";

        // Manage SYSTEM LINK  -

        public const string PROCGETSYSTEMLINK = "uspGetSystemLink";
        public const string PROCSUBMITSYSTEMLINK = "uspSubmitSystemLink";


        // Manage BULLETIN BOARD  -

        public const string PROCGETBULLETINBOARD = "uspGetBulletinBoard";
        public const string PROCSUBMITBULLETINBOARD = "uspSubmitBulletinBoard";

        // get user access point  Links 
        public const string PROCGETPOINT = "uspGetPointList";

        // Manage  TRAINING  -

        public const string PROCGETTRAINING = "uspGetTraining";
        public const string PROCSUBMITTRAINING = "uspSubmitTraining";


        // Manage RSSFEED  -

        public const string PROCGETRSSFEED = "uspGetRSSFeed";
        public const string PROCSUBMITRSSFEED = "uspSubmitRSSFeed";

        // PopularPost AND Blog

        public const string PROCGETPOPULERPOSTLIST = "uspGetPopulerPostList";
        public const string PROCGETPOPULERBLOGLIST = "uspGetPopulerBlogList";
        public const string PROCGETKTLIST = "uspGetKnowledgeTree";


        //---///////////QUIZE  SYSTEM 
        public const string PROCSUBMITQUIZ = "uspSubmitQuiz";
        public const string PROCSUBMITQUESTION = "uspSubmitQuestion";
        public const string PROCSUBMITCHOICES = "uspSubmitChoices";
        public const string PROCSUBMITANSWER = "uspSubmitAnswer";

        public const string PROCGETQUIZ = "uspGetKMTQuiz";
        public const string PROCGETQUIZQUESTIONS = "uspGetQuizQuestions";
        public const string PROCGETQUIZCHOICE = "uspGetQuizChoices";
        public const string PROCGETQUIZANSWER = "uspGetQuizAnswer";


        // Manage Badge   -

        public const string PROCGETBADGE = "uspGetBadge";
        public const string PROCSUBMITBADGE = "uspSubmitBadge";

        public const string PROCGETINTERACTIONPOINT = "uspGetInteractionPoint";
        public const string PROCSUBMITINTERACTIONPOINT = "uspSubmitInteractionPoint";

        // Is Attend WebEx Capture Informtion 

        public const string PROCSUBMITISATTENDWEBEX = "uspSubmitIsAttendWebExTraing";
        public const string PROCGETLISTASSESSMENTATTEND = "uspGetListAssessmentAttend";
        public const string PROCSUBMITASSESSMENT = "uspSubmitAssessment";
        public const string PROCSUBMITUSERTRAFFIC = "uspSubmitUserTraffic";
        //Reports
        public const string PROCGETREGISTEREDUSERS = "uspGetRegisteredUsersReports";
        public const string PROCGETUSERTRAFFIC = "uspGetUserTrafficReports";
        public const string PROCGETINTERACTIONS = "uspGetInteractionsReports";
        public const string PROCGETDOCUMENTSUPLOADED = "uspGetDocumentsUploadedReport";
        public const string PROCGETBLOGS = "uspGetBlogsReports";
        public const string PROCGETPOSTS = "uspGetPostsReport";
        public const string PROCGETFLAGGEDPOSTS = "uspGetFlaggedPostsReports";
        public const string PROCGETASSESSMENTRESULT = "uspGetAssessmentResult";


        public const string PROCGETCBLOGININFO = "GetCBLoginInfo";
        public const string PROCGETLOGININFO = "GetLoginInfo";
        public const string PROCGETEXISTUSER = "GetExistUser";
        public const string PROCGETPASS = "Getpass";
    }
}
