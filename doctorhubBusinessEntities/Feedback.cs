using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubBusinessEntities
{
    public class FeedBackList :Base
    {
        public string FeedBackId { get; set; }
        public string UserFeedback { get; set; }
        public string FeedbackQuestionDate { get; set; }
        public string AdminReply { get; set; }
        public string AdminReplyDate { get; set; }
       


    }

    public class Feedback:FeedBackList
    {
          public int Id {get; set;} 
          public string  UserCode {get; set;}
          public string  UserEmail {get; set;}
          public string  UserName {get; set;}
          public string  UserNTID {get; set;}
          public string  AdminCode {get; set;}
          public string  AdminEmail {get; set;}
          public string  AdminName {get; set;}
          public string  AdminNTID {get; set;}
          public int  UserLOB {get; set;}
          public string  UserLobName {get; set;}
          public string FeedbackQuestion { get; set; }
          public List<FeedBackList> UserFeedBackList { get; set; }
        
     
    }
}
