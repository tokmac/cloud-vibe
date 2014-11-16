﻿

namespace Cloud_Vibe.Areas.Administration.Controllers
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

    public class AlbumController : AdminController
    {
        public AlbumController(ICloudVibeData data)
            : base(data)
        {
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

            var alb = new List<AlbumViewModel>() { new AlbumViewModel() { Title = "Test" } };

            var test = new List<AlbumViewModel>();

            foreach (var item in albums)
            {
                test.Add(AutoMapper.Mapper.Map<AlbumViewModel>(item));
            }

            return Json(test.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, AlbumViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = Mapper.Map<Album>(model);
                this.data.Albums.Add(dbModel);
                this.data.SaveChanges();
                model.ID = dbModel.ID;
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, AlbumViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var album = this.data.Albums.Find(model.ID);
                Mapper.Map(model.ID, album);
                this.data.SaveChanges();
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, AlbumViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var album = this.data.Albums.Find(model.ID);
                album.IsDeleted = true;
                album.DeletedOn = DateTime.Now;
                this.data.SaveChanges();
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }
}