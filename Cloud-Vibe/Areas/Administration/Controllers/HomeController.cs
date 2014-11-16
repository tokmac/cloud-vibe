using Cloud_Vibe.Areas.Administration.Controllers.Base;
using Cloud_Vibe.Controllers;
using Cloud_Vibe.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cloud_Vibe.Areas.Administration.Controllers
{
    public class HomeController : AdminController
    {
        public HomeController(ICloudVibeData data)
            : base(data)
        {
        }


        // GET: Admininstration/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}