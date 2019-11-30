using doctorhubBusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace doctorhubDataAccess
{
    [Serializable]
    public class ActiveDirectoryUser
    {
        public void CreateUserSession(UserRegistrationModel user)
        {
            HttpContext.Current.Session["UserNTID"] = user.UserNTID;
            HttpContext.Current.Session["Adminstrator"] = "0";
            HttpContext.Current.Session["CapabilitiesId"] = user.CapabilitiesId;
            HttpContext.Current.Session["UserFirstName"] = user.UserName;

        }
    }
}
