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


    public class SongController : AdminController
    {
        public SongController(ICloudVibeData data)
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
            var songs = data.Songs.All().ToList();
            var resultSongs = new List<SongViewModel>();

            foreach (var item in songs)
            {
                resultSongs.Add(AutoMapper.Mapper.Map<SongViewModel>(item));
            }

            return Json(resultSongs.ToDataSourceResult(request));
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
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, SongViewModel model)
        {
            if (model != null)
            {
                var song = this.data.Songs.GetById(model.ID);
                song.Title = model.Title;
                song.Artist.Name = model.Artist;
                song.VideoLink = model.VideoLink;
                this.data.SaveChanges();
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, SongViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.data.Songs.Delete(model.ID.Value);
                this.data.SaveChanges();
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }
}