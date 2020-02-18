using doctorhubBusinessEntities;
using doctorhubBusinessEntities.viewModels;
using doctorhubDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doctorhub.Controllers
{
    public class QuizController : Controller
    {
        private DirectoryDataAccess objDirectoryDataAccess;

        public QuizController()
        {
            this.objDirectoryDataAccess = new DirectoryDataAccess();
        }

        public JsonResult GetQuiz()
        {
            return Json(objDirectoryDataAccess.GetQuiz(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageQuiz()
        {
            string keyId = Request.QueryString["key"];

            if (!string.IsNullOrEmpty(keyId))
            {
                ViewBag.KeyId = keyId;
                var details = objDirectoryDataAccess.GetQuiz().Where(x => x.QuizID == Convert.ToInt32(keyId)).FirstOrDefault();
                return View(details);
            }

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ManageQuiz(QuizVM model)
        {
            ModelState.Remove("QuizID");
            if (ModelState.IsValid)
            {
                int msg = 0;
                if (model.QuizID == 0)
                {
                    msg = objDirectoryDataAccess.SubmitQuiz(model);
                    if (msg > 0)
                    {
                        objDirectoryDataAccess.SendEmail(Convert.ToInt32(HttpContext.Session["RoleId"]), "KMT24");
                        TempData["success"] = "Quiz Added Successfully";
                    }
                    else
                        TempData["error"] = "Some Error Occured. Please Contact Admin";
                }
                else
                {
                    msg = objDirectoryDataAccess.SubmitQuiz(model);
                    if (msg > 0)
                    {
                        objDirectoryDataAccess.SendEmail(Convert.ToInt32(HttpContext.Session["RoleId"]), "KMT25");
                        TempData["success"] = "Quiz Updated Successfully";
                    }
                    else
                        TempData["error"] = "Some Error Occured. Please Contact Admin";
                }
            }

            ModelState.Clear();
            return RedirectToAction("ManageQuiz");
        }

        public JsonResult GetQuestion()
        {
            return Json(objDirectoryDataAccess.GetQuizQuestions(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageQuestion()
        {
            QuizVM quiz = new doctorhubBusinessEntities.viewModels.QuizVM();
            List<SelectListItem> Quiz = new List<SelectListItem>();
            var QuizList = objDirectoryDataAccess.GetQuiz().Where(q => q.IsActive == true).Select(l => new { l.QuizID, l.QuizName });
            foreach (var item in QuizList)
            {
                Quiz.Add(new SelectListItem
                {
                    Text = item.QuizName,
                    Value = item.QuizID.ToString(),
                });
            }
            ViewBag.BSList = Quiz.ToList();
            string keyId = Request.QueryString["key"];
            if (!string.IsNullOrEmpty(keyId))
            {
                ViewBag.KeyId = keyId;
                var details = objDirectoryDataAccess.GetQuizQuestions().Where(x => x.QuestionID == Convert.ToInt32(keyId)).FirstOrDefault();
                string[] arr = details.ChoiceText.ToString().Split('/');
                string[] arrChoiceId = details.ChoicestringID.ToString().Trim().Split('/');
                if (arr.Length == 4)
                {
                    details.ChoiceID1 = arrChoiceId[0].Trim();
                    details.ChoiceID2 = arrChoiceId[1].Trim();
                    details.ChoiceID3 = arrChoiceId[2].Trim();
                    details.ChoiceID4 = arrChoiceId[3].Trim();
                    details.ChoiceText2 = arr[1];
                    details.ChoiceText3 = arr[2].Replace("!X", "");
                    details.ChoiceText4 = arr[3].Replace("!X", "");
                    details.ChoiceText1 = arr[0];
                    details.ChoiceText2 = arr[1];
                    details.ChoiceText3 = arr[2].Replace("!X", "");
                    details.ChoiceText4 = arr[3].Replace("!X", "");
                    if (arr[0].Trim() == details.AnswerText.Trim())
                    {
                        details.IsAnswer1 = true;
                    }
                    if (arr[1].Trim() == details.AnswerText.Trim())
                    {
                        details.IsAnswer2 = true;
                    }
                    if (arr[2].Trim() == details.AnswerText.Trim())
                    {
                        details.IsAnswer3 = true;
                    }
                    if (arr[3].Trim() == details.AnswerText.Trim())
                    {
                        details.IsAnswer4 = true;
                    }
                }
                if (arr.Length == 3)
                {
                    details.ChoiceID1 = arrChoiceId[0].Trim();
                    details.ChoiceID2 = arrChoiceId[1].Trim();
                    details.ChoiceID3 = arrChoiceId[2].Trim();
                    details.ChoiceText1 = arr[0];
                    details.ChoiceText2 = arr[1];
                    details.ChoiceText3 = arr[2];
                    if (arr[0].Trim() == details.AnswerText.Trim())
                    {
                        details.IsAnswer1 = true;
                    }
                    if (arr[1].Trim().ToString() == details.AnswerText.Trim().ToString())
                    {
                        details.IsAnswer2 = true;
                    }
                    if (arr[2].Trim() == details.AnswerText.Trim())
                    {
                        details.IsAnswer3 = true;
                    }
                }
                if (arr.Length == 2)
                {
                    details.ChoiceID1 = arrChoiceId[0].Trim();
                    details.ChoiceID2 = arrChoiceId[1].Trim();
                    details.ChoiceText1 = arr[0];
                    details.ChoiceText2 = arr[1];
                    if (arr[0].Trim() == details.AnswerText.Trim())
                    {
                        details.IsAnswer1 = true;
                    }
                    if (arr[1].Trim() == details.AnswerText.Trim())
                    {
                        details.IsAnswer2 = true;
                    }
                }
                return View(details);
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ManageQuestion(QuestionVM model)
        {
            ModelState.Remove("QuestionID");
            if (ModelState.IsValid)
            {
                if (model.IsAnswer1 == true)
                {
                    model.AnswerText = model.ChoiceText1;
                }
                if (model.IsAnswer2 == true)
                {
                    model.AnswerText = model.ChoiceText2;
                }
                if (model.IsAnswer3 == true)
                {
                    model.AnswerText = model.ChoiceText3;
                }
                if (model.IsAnswer4 == true)
                {
                    model.AnswerText = model.ChoiceText4;
                }
                int msg = 0;
                if (model.QuestionID == 0)
                {
                    msg = objDirectoryDataAccess.SubmitQuestion(model);
                    if (msg > 0)
                        TempData["success"] = "Question Added Successfully";
                    else
                        TempData["error"] = "Some Error Occured. Please Contact Admin";
                }
                else
                {
                    msg = objDirectoryDataAccess.SubmitQuestion(model);
                    if (msg > 0)
                        TempData["success"] = "Question Updated Successfully";
                    else
                        TempData["error"] = "Some Error Occured. Please Contact Admin";
                }
            }

            ModelState.Clear();
            return RedirectToAction("ManageQuestion");
        }

        public JsonResult GetAnswer()
        {
            return Json(objDirectoryDataAccess.GetQuizAnswer(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageAnswer()
        {
            QuizVM quiz = new doctorhubBusinessEntities.viewModels.QuizVM();
            List<SelectListItem> Quiz = new List<SelectListItem>();
            var QuizList = objDirectoryDataAccess.GetQuiz().Select(l => new { l.QuizID, l.QuizName });
            foreach (var item in QuizList)
            {
                Quiz.Add(new SelectListItem
                {
                    Text = item.QuizName,
                    Value = item.QuizID.ToString(),
                });
            }
            ViewBag.BSList = Quiz.ToList();
            List<SelectListItem> Ques = new List<SelectListItem>();
            var QuesList = objDirectoryDataAccess.GetQuizQuestions().Select(l => new { l.QuestionID, l.QuestionText });
            foreach (var item in QuesList)
            {
                Ques.Add(new SelectListItem
                {
                    Text = item.QuestionText,
                    Value = item.QuestionID.ToString(),
                });
            }
            ViewBag.QuesList = Ques.ToList();
            string keyId = Request.QueryString["key"];
            if (!string.IsNullOrEmpty(keyId))
            {


                ViewBag.KeyId = keyId;
                var details = objDirectoryDataAccess.GetQuizAnswer().Where(x => x.AnswerID == Convert.ToInt32(keyId)).FirstOrDefault();
                return View(details);
            }
            return View();
        }

        [HttpPost]
        public ActionResult ManageAnswer(QuizAnswersVM model, int? QuizID)
        {
            ModelState.Remove("AnswerID");
            if (ModelState.IsValid)
            {
                int msg = 0;
                if (model.AnswerID == 0)
                {
                    msg = objDirectoryDataAccess.SubmitAnswer(model);
                    if (msg > 0)
                        TempData["success"] = "Answer Added Successfully";
                    else
                        TempData["error"] = "Some Error Occured. Please Contact Admin";
                }
                else
                {
                    msg = objDirectoryDataAccess.SubmitAnswer(model);
                    if (msg > 0)
                        TempData["success"] = "Answer Updated Successfully";
                    else
                        TempData["error"] = "Some Error Occured. Please Contact Admin";
                }
            }

            ModelState.Clear();
            return RedirectToAction("ManageAnswer");
        }

        public JsonResult GetQuizChoice()
        {
            return Json(objDirectoryDataAccess.GetQuizChoice(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageQuizChoice()
        {
            QuizVM quiz = new doctorhubBusinessEntities.viewModels.QuizVM();
            List<SelectListItem> Ques = new List<SelectListItem>();
            var QuesList = objDirectoryDataAccess.GetQuizQuestions().Select(l => new { l.QuestionID, l.QuestionText });
            foreach (var item in QuesList)
            {
                Ques.Add(new SelectListItem
                {
                    Text = item.QuestionText,
                    Value = item.QuestionID.ToString(),
                });
            }
            ViewBag.QuesList = Ques.ToList();

            string keyId = Request.QueryString["key"];
            if (!string.IsNullOrEmpty(keyId))
            {
                ViewBag.KeyId = keyId;
                var details = objDirectoryDataAccess.GetQuizChoice().Where(x => x.ChoiceID == Convert.ToInt32(keyId)).FirstOrDefault();
                return View(details);
            }

            return View();
        }

        [HttpPost]
        public ActionResult ManageQuizChoice(ChoiceVM model)
        {
            ModelState.Remove("ChoiceID");
            if (ModelState.IsValid)
            {
                int msg = 0;
                if (model.ChoiceID == 0)
                {
                    if (model.IsAnswer == true)
                    {
                        msg = objDirectoryDataAccess.SubmitQuesAnswer(0, model.QuestionID, model.ChoiceText);
                        msg = objDirectoryDataAccess.SubmitChoice(model);
                    }
                    else
                    {
                        msg = objDirectoryDataAccess.SubmitChoice(model);
                    }

                    if (msg > 0)
                        TempData["success"] = "Choice Added Successfully";
                    else
                        TempData["error"] = "Some Error Occured. Please Contact Admin";
                }
                else
                {
                    msg = objDirectoryDataAccess.SubmitChoice(model);
                    if (msg > 0)
                        TempData["success"] = "Choice Updated Successfully";
                    else
                        TempData["error"] = "Some Error Occured. Please Contact Admin";
                }
            }

            ModelState.Clear();
            return RedirectToAction("ManageQuizChoice");
        }

        [HttpGet]
        public ActionResult SelectQuiz()
        {
            string userNTId = HttpContext.Session["UserNTID"].ToString();
            var userDetails = objDirectoryDataAccess.GetListOfRegisteredUser().Where(x => x.UserNTID == userNTId && x.IsActive == true).FirstOrDefault();
            if (userDetails.Status == Convert.ToInt32(StatusType.Approve))
            {
                ViewBag.UserName = userDetails.UserName;
                ViewBag.LOBName = userDetails.LOBName;
                ViewBag.BusinessSegment = userDetails.BusinessSegmentName;
                ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(userDetails.UserPhoto, 0, userDetails.UserPhoto.Length);
            }
            QuizVM quiz = new doctorhubBusinessEntities.viewModels.QuizVM();
            List<SelectListItem> Quiz = new List<SelectListItem>();
            var QuizList = objDirectoryDataAccess.GetQuiz().Select(l => new { l.QuizID, l.QuizName });
            foreach (var item in QuizList)
            {
                Quiz.Add(new SelectListItem
                {
                    Text = item.QuizName,
                    Value = item.QuizID.ToString(),
                });
            }
            quiz.ListOfQuiz = Quiz.ToList();

            return View(quiz);
        }

        [HttpGet]
        public ActionResult QuizTest(QuizVM quiz)
        {
            return View();
        }

        [HttpPost]
        public ActionResult QuizTest(List<QuizAnswersVM> resultQuiz)
        {
            List<QuizAnswersVM> finalResultQuiz = new List<QuizAnswersVM>();

            foreach (QuizAnswersVM answser in resultQuiz)
            {
                QuizAnswersVM result = objDirectoryDataAccess.GetQuizAnswer().Where(x => x.QuestionID == answser.QuestionID).Select(a => new QuizAnswersVM
                {
                    QuestionID = a.QuestionID,
                    AnswerQ = a.AnswerText,
                    isCorrect = (answser.AnswerQ.ToLower().Equals(a.AnswerText.ToLower()))
                }).FirstOrDefault();
                finalResultQuiz.Add(result);
            }
            return Json(new { result = finalResultQuiz }, JsonRequestBehavior.AllowGet);
        }
    }

}