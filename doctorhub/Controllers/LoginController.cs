using doctorhubBusinessEntities;
using doctorhubDataAccess;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doctorhub.Controllers
{
    public class LoginController : Controller
    {
        private DirectoryDataAccess objDirectoryDataAccess;
        public LoginController()
        {
            this.objDirectoryDataAccess = new DirectoryDataAccess();
        }
        // GET: Login
        public ActionResult Login()
        {

            return View("Login");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Login(UserLogin modelval)
        {
            if ((modelval.EmailId != null && modelval.EmailId != "") && (modelval.Password != null && modelval.Password != ""))
            {


            }
            return View(modelval);
        }

        [HttpPost]
        public JsonResult UserRegistration(string name, string email, string password)
        {
            UserRegistrationModel user = new UserRegistrationModel();
            user.RoleId = 1;
            user.UserCode = RandomDigits(10);
            user.UserNTID = "HUB"+name;
            user.UserName = name;
            user.UserEmail = email;
            user.ManagerName = "INDIA";
            user.ManagerNTID = password;
            user.ManagerCode = password;
            user.ManagerEmail = email;
            user.BusinessSegmentId = 1;
            user.CapabilitiesId = 1;
            user.LOBId = 1;
            user.AboutMe = "Describe yourself in 140 characters";
           
            
            var imagPath = Server.MapPath("~/images/logo.jpg");
            Image img = Image.FromFile(imagPath);
            byte[] imageDatabytes = (byte[])(new ImageConverter()).ConvertTo(img, typeof(byte[]));

            try
            {
                int msg = 0;
                msg = objDirectoryDataAccess.SubmitUserRequest(null, imageDatabytes,user);
            }
            catch(Exception ex)
            {

            }
            return Json(new { success = true, responseText = " Sucessfully." }, JsonRequestBehavior.AllowGet);
        }

        public string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
    }
}