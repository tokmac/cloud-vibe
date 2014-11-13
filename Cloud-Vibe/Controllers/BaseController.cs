using Cloud_Vibe.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cloud_Vibe.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ICloudVibeData data;

        public BaseController(ICloudVibeData data)
        {
            this.data = data;
        }
    }
}