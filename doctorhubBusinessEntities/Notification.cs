using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubBusinessEntities
{
    public class Notification
    {
           public int  Id {get;set;} 
           public string  RoleId {get;set;} 
           public string  UserCode{get;set;} 
           public bool  IsAdminFlag {get;set;} 
           public bool  IsUserFlag {get;set;} 
           public bool  IsFacilitatorFlag {get;set;} 
           public string  Identifier {get;set;} 
           public string  AdminDescripation {get;set;} 
           public string  UserDescripation {get;set;} 
           public int  CapabilityId {get;set;} 
           public DateTime  CreatedOn{get;set;}

    }
}
