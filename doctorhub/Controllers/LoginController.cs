using doctorhubBusinessEntities;
using doctorhubDataAccess;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

            if ((modelval != null && modelval.EmailId != "") && (modelval.Password != null && modelval.Password != ""))
            {
                var s = objDirectoryDataAccess.UserExist(modelval.EmailId, modelval.Password);
                if (s == true)
                {

                    HttpContext.Session["UserNTID"] = objDirectoryDataAccess.GetNtidAndPass(modelval.EmailId).FirstOrDefault().UserNTID;
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    ViewBag.error = "UserName Or Passowrd is in correct !!";
                }

            }

            return View("Login");
        }
        public ActionResult UserRegistration()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UserRegistration(string name, string email, string mobile, string password)
        {
            if (name != "" && email != "" && mobile != "" && password != "")
            {
                var R = objDirectoryDataAccess.RegisterUserExist(email);
                if (R == false)
                {

                    UserRegistrationModel user = new UserRegistrationModel();
                    user.RoleId = 12;
                    user.UserCode = RandomDigits(10);
                    user.UserNTID = "HUB" + RandomDigits(5);
                    user.UserName = name;
                    user.UserEmail = email;
                    user.ManagerName = "INDIA";
                    user.ManagerNTID = password;
                    user.ManagerCode = password;
                    user.ManagerEmail = email;
                    user.BusinessSegmentId = 1;
                    user.CapabilitiesId = 1;
                    user.LOBId = 1;
                    user.Status = 6;
                    user.AboutMe = "Describe yourself in 140 characters";


                    var imagPath = Server.MapPath("~/images/logo.jpg");
                    Image img = Image.FromFile(imagPath);
                    byte[] imageDatabytes = (byte[])(new ImageConverter()).ConvertTo(img, typeof(byte[]));

                    try
                    {
                        int msg = 0;
                        msg = objDirectoryDataAccess.SubmitUserRequest(null, imageDatabytes, user);
                    }
                    catch (Exception ex)
                    {

                    }
                    ViewBag.success = "Register Sucessfully..!!";
                    return View();
                }
                ViewBag.Rerror = "Oppss User E-mail Id Allaeady Register !!";
                // return Json(new { success = true, responseText = " Sucessfully." }, JsonRequestBehavior.AllowGet);
                return View();
            }
            ViewBag.Rerror = "Plese fill your detail correctly Not balnk !!";
            // return Json(new { success = true, responseText = " Sucessfully." }, JsonRequestBehavior.AllowGet);
            return View();
        }

        public string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }

        public ActionResult ForgetPass()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ForgetPass(string email)
        {

            if ((email != null && email != ""))
            {

                var s = objDirectoryDataAccess.GetNtidAndPass(email).FirstOrDefault();
                if (s != null)
                { 
                if (s.UserNTID != "" && s.Password != "")
                {

                    string x = objDirectoryDataAccess.SendPasswordToEmail(email, s.UserNTID, s.Password, s.Password);
                    ViewBag.Psuccess = x;
                }

                else
                {
                    ViewBag.Perror = "Email-id is in correct !!";
                }
                }
                else
                {
                    ViewBag.Perror = "Email-id is not exist  !!";
                }

            }

            return View();
        }
    }
}