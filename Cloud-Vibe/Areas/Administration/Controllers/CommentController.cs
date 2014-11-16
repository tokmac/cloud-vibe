namespace Cloud_Vibe.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using Cloud_Vibe.Areas.Administration.Controllers.Base;
    using Cloud_Vibe.Areas.Administration.ViewModels.Song;
    using Cloud_Vibe.Controllers;
    using Cloud_Vibe.Data;
    using Cloud_Vibe.Areas.Administration.ViewModels.Comment;

    public class CommentController : AdminController
    {
        public CommentController(ICloudVibeData data)
            :base(data)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        [HttpGet]
        public ActionResult All()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var comments = data.Comments.All().ToList();
            var relatedComments = new List<CommentViewModel>();

            foreach (var item in comments)
            {
                relatedComments.Add(AutoMapper.Mapper.Map<CommentViewModel>(item));
            }

            return Json(relatedComments.ToDataSourceResult(request));
        }

        //[HttpPost]
        //public ActionResult Create([DataSourceRequest]DataSourceRequest request, AlbumViewModel model)
        //{
        //    if (model != null && ModelState.IsValid)
        //    {
        //        Album dbModel = new Album()
        //        {
        //            Artist = new Artist() { Name = model.UserShared },
        //            Title = model.Title,
        //            SharedOn = model.SharedOn,

        //        };
        //        this.data.Albums.Add(dbModel);
        //        this.data.SaveChanges();
        //        model.ID = dbModel.ID;
        //    }

        //    return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        //}

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, CommentViewModel model)
        {
            if (model != null)
            {
                var comment = this.data.Comments.GetById(model.ID);
                comment.Text = model.Text;

                this.data.SaveChanges();
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, CommentViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.data.Comments.Delete(model.ID.Value);
                this.data.SaveChanges();
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }
}