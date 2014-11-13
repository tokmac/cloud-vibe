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
    public class SongController : BaseController
    {

        public SongController(ICloudVibeData data)
            :base(data)
        {

        }
        // GET: Song
        public ActionResult Details(string title)
        {
            var song = data.Songs.All().FirstOrDefault(s => s.Title == title);
            song.Views = song.Views + 1;
            data.SaveChanges();

            Mapper.CreateMap<Song,SongDetailsViewModel>();

            SongDetailsViewModel songToSee = Mapper.Map<SongDetailsViewModel>(song);

            return View(songToSee);
        }
    }
}