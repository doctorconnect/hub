﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doctorhub.Controllers
{
    public class QuizController : Controller
    {
        // GET: Quiz
        public ActionResult Index()
        {
            return View();
        }
    }
}