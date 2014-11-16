using Cloud_Vibe.Controllers;
using Cloud_Vibe.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cloud_Vibe.Areas.Administration.Controllers.Base
{
    //[Authorize(Roles="Admin")]
    public abstract class AdminController : BaseController
    {
        public AdminController(ICloudVibeData data)
            : base(data)
        {
        }
    }
}