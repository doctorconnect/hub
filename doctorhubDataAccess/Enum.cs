using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubDataAccess
{
    public enum StatusType 
    {
        Pending=5,
        Approve=6,
        Reject=7
    }

    public enum RoleType
    {
        Admin = 1,
        Facilitator =10,
        Contributor=11,
        User=12
    }

    public enum ReportType
    {
        RegisteredUsers = 17,
        UserTraffic = 18,
        Interactions = 19,
        DocumentsUploaded = 20,
        Blogs = 21,
        Posts = 22,
        FlaggedPosts = 23
    }
}
