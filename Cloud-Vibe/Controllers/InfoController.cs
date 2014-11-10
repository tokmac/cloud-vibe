using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cloud_Vibe.Controllers
{
    public class InfoController : Controller
    {
        // GET: TermsOfAgree
        [HttpGet]
        public ActionResult TermsOfAgree()
        {
            return View();
        }

        // GET: ShareGuide
        [HttpGet]
        public ActionResult ShareGuide()
        {
            return View();
        }

        // GET: DownloadGuide
        [HttpGet]
        public ActionResult DownloadGuide()
        {
            return View();
        }
    }
}