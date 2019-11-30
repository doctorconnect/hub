using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubBusinessEntities
{
    public class Comments : Base
    {
        public string CommentedBy { get; set; }
        public string CommentedByName { get; set; }
        public string Message { get; set; }
        public int CommentId { get; set; }
        public string Identifier { get; set; }
        public string CommentedDate { get; set; }
    }

    public class posts: Comments
    {       
           public int FlagCount {get;set;} 
           public int PostId {get;set;} 
           public string PostedBy{get;set;} 
           public string PostedByName{get;set;} 
           public string PostedDate{get;set;} 
           public string Status {get;set;} 
           public string Remarks {get;set;}
        public string LikeBy { get; set; }

    }
}
