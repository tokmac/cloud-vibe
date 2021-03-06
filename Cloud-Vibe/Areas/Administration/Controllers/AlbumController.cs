﻿namespace Cloud_Vibe.Areas.Administration.Controllers
{
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using Cloud_Vibe.Areas.Administration.ViewModels;
    using Cloud_Vibe.Areas.Administration.Controllers.Base;
    using Cloud_Vibe.Controllers;
    using Cloud_Vibe.Data;
    using Cloud_Vibe.Data.Models;
    using System.Threading;
    using System.Globalization;
    using Cloud_Vibe.Areas.Administration.ViewModels.Album;

    public class AlbumController : AdminController
    {
        public AlbumController(ICloudVibeData data)
            : base(data)
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
            var albums = data.Albums.All().ToList();
            var resultAlbums = new List<AlbumViewModel>();

            foreach (var item in albums)
            {
                resultAlbums.Add(AutoMapper.Mapper.Map<AlbumViewModel>(item));
            }

            return Json(resultAlbums.ToDataSourceResult(request));
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
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, AlbumViewModel model)
        {
            if (model != null)
            {
                var album = this.data.Albums.GetById(model.ID);
                album.Title = model.Title;
                album.Artist.Name = model.Artist;
                album.VideoLink = model.VideoLink;
                this.data.SaveChanges();
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, AlbumViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.data.Albums.Delete(model.ID.Value);
                this.data.SaveChanges();
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }
}