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
            var album = data.Songs.All().FirstOrDefault(s => s.Title == title);
            var thanxies = album.Thanxies.Count;

            Mapper.CreateMap<Album, AlbumDetailsViewModel>();

            var albumToSee = Mapper.Map<AlbumDetailsViewModel>(album);
            


            return View();
        }
    }
}