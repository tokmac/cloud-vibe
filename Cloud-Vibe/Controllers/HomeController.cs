using Cloud_Vibe.Data;
using Cloud_Vibe.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cloud_Vibe.Controllers
{
    [RequireHttps]
    public class HomeController : BaseController
    {
        public HomeController(ICloudVibeData data)
            : base(data)
        {

        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "User");
            }
            return View();
        }

        public ActionResult About(HttpPostedFileBase file)
        {
            //MemoryStream target = new MemoryStream();
            //file.InputStream.CopyTo(target);
            //byte[] data = target.ToArray();

            //Cloud_Vibe.Data.CloudVibeData context = new Data.CloudVibeData(Cloud_Vibe.Data.CloudVibeDbContex);
            //context.Albums.Add(new Album()
            //{
            //    Torrent = data;
            //})
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}