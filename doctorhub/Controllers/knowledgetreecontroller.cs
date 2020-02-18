using doctorhubBusinessEntities;
using doctorhubDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doctorhub.Controllers
{
    public class KnowledgeTreeController : Controller
    {
        private DirectoryDataAccess objDirectoryDataAccess;

        public KnowledgeTreeController()
        {
            objDirectoryDataAccess = new DirectoryDataAccess();
        }

        public ActionResult KnowledgeTree()
        {
            string categories = Request.QueryString["categories"];
            string doctypes = Request.QueryString["doctypes"];
            string lobs = Request.QueryString["lobs"];
            List<KnowledgeTreemModel> listOfKnowledgeTreemModel = new List<KnowledgeTreemModel>();
            IEnumerable<KnowledgeTreemModel> listOfKtdata = new List<KnowledgeTreemModel>();
            listOfKnowledgeTreemModel = objDirectoryDataAccess.GetKnowledgeTree();
            ViewBag.listOfKnowledgeTreemModel = listOfKnowledgeTreemModel;
            listOfKtdata = listOfKnowledgeTreemModel;
            if (!string.IsNullOrEmpty(categories))
            {
                List<KnowledgeTreemModel> listOfSelectcategories = new List<KnowledgeTreemModel>();
                List<string> checkedcategories = categories.Split(',').ToList();
                ViewBag.checkedcategories = checkedcategories;
                for (int i = 0; i < checkedcategories.Count(); i++)
                {
                    listOfSelectcategories.AddRange(listOfKtdata.Where(m => m.CATEGORYNAME == checkedcategories[i]));
                }
                listOfKtdata = listOfSelectcategories;
            }
            if (!string.IsNullOrEmpty(doctypes))
            {
                List<KnowledgeTreemModel> listOfKtdatadoctypes = new List<KnowledgeTreemModel>();
                List<string> checkeddoctypes = doctypes.Split(',').ToList();
                ViewBag.checkeddoctypes = checkeddoctypes;
                for (int j = 0; j < checkeddoctypes.Count(); j++)
                {
                    listOfKtdatadoctypes.AddRange(listOfKtdata.Where(m => m.DOCUTYPE == checkeddoctypes[j]));
                }
                listOfKtdata = listOfKtdatadoctypes;
            }
            if (!string.IsNullOrEmpty(lobs))
            {
                List<KnowledgeTreemModel> listOfKtdatalobs = new List<KnowledgeTreemModel>();
                List<string> checkedlobs = lobs.Split(',').ToList();
                ViewBag.checkedlobs = checkedlobs;
                for (int k = 0; k < checkedlobs.Count(); k++)
                {
                    listOfKtdatalobs.AddRange(listOfKtdata.Where(m => m.LOBName == checkedlobs[k]));
                }
                listOfKtdata = listOfKtdatalobs;
            }
            ViewBag.listOfKtdata = listOfKtdata;

            return View();
        }
    }
}
