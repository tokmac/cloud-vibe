using AutoMapper;
using Cloud_Vibe.Data;
using Cloud_Vibe.Data.Models;
using Cloud_Vibe.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cloud_Vibe.Controllers
{
    public class AlbumController : BaseController
    {
        public AlbumController(ICloudVibeData data)
            :base(data)
        {

        }

        // GET: Album
        public ActionResult Details(string title)
        {
            Album album = data.Albums.All().FirstOrDefault(s => s.Title == title);

            Mapper.CreateMap<Album, AlbumDetailsViewModel>();
            AlbumDetailsViewModel albumToSee = new AlbumDetailsViewModel();
            Mapper.Map<Album, AlbumDetailsViewModel>(album, albumToSee);

            return View(albumToSee);
        }
    }
}