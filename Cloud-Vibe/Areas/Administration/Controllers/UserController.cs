//using Cloud_Vibe.Areas.Administration.Controllers.Base;
//using Cloud_Vibe.Areas.Administration.ViewModels;
//using Cloud_Vibe.Data;
//using Kendo.Mvc.UI;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Threading;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Security;

//namespace Cloud_Vibe.Areas.Administration.Controllers
//{
//    public class UserController : AdminController
//    {
//        public UserController(ICloudVibeData data)
//            :base(data)
//        {
//            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
//        }

//        // GET: Administration/User
//        public ActionResult Index()
//        {
//            return View();
//        }

//        [HttpPost]
//        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
//        {
//            var users = data.Users.All().ToList();
//            var resultUsers = new List<UserViewModel>();

//            foreach (var user in users)
//            {
//                var userToAdd = new UserViewModel
//                {
//                    ID = user.Id,
//                    FirstName = user.FirstName,
//                    LastName = user.LastName,
//                    Role = Roles.GetRolesForUser(user.UserName),
//                    Username = user.UserName,
//                    Email = user.Email
//                };

//                resultUsers.Add(userToAdd);
//            }

//            return Json(resultUsers.ToDataSourceResult(request));
//        }

//        [HttpPost]
//        public ActionResult Update([DataSourceRequest]DataSourceRequest request, UserViewModel model)
//        {
//            if (model != null)
//            {
//                var user = this.data.Users.GetById(model.ID);
//                user.UserName = model.Username;
//                //UserManager.AddToRole(model.Id, "Superusers");
//                this.data.SaveChanges();
//            }

//            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
//        }

//        [HttpPost]
//        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, UserViewModel model)
//        {
//            if (model != null && ModelState.IsValid)
//            {
//                var currentUser = data.Users.GetById(model.ID);
//                this.data.Users.Delete(currentUser);
//                this.data.SaveChanges();
//            }

//            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
//        }
//    }
//}