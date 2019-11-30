using doctorhubBusinessEntities;
using doctorhubDataAccess;
using System;
using System.Collections.Generic;
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
            UserRegistration user = new UserRegistration();
            user.UserName = name;
            user.EmailId = email;
            user.Password = password;
            try
            {
                int msg = 0;
                msg = objDirectoryDataAccess.SubmitUserRequest(user);
            }
            catch(Exception ex)
            {

            }
            return Json(new { success = true, responseText = " Sucessfully." }, JsonRequestBehavior.AllowGet);
        }
    }
}