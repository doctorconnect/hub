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
}
