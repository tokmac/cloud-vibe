﻿namespace Cloud_Vibe.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    
    using AutoMapper;
    using PagedList;

    using Cloud_Vibe.Data;
    using Cloud_Vibe.Data.Models;
    using Cloud_Vibe.Models.ViewModels;

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

        [HttpGet]
        public ActionResult AllSongs(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParam = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.DateSortParam = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.ArtistSortParam = sortOrder == "Artist" ? "artist_desc" : "Artist";
            ViewBag.DownloadsSortParam = sortOrder == "Downloads" ? "downloads_desc" : "Downloads";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var songs = data.Songs.All();

            if (!String.IsNullOrEmpty(searchString))
            {
                songs = songs.Where(s => s.Title.Contains(searchString)
                                       || s.Artist.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    songs = songs.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    songs = songs.OrderBy(s => s.SharedOn);
                    break;
                case "date_desc":
                    songs = songs.OrderByDescending(s => s.SharedOn);
                    break;
                case "Artist":
                    songs = songs.OrderBy(s => s.Artist.Name);
                    break;
                case "artist_desc":
                    songs = songs.OrderByDescending(s => s.Artist.Name);
                    break;
                case "Downloads":
                    songs = songs.OrderBy(s => s.Downloads);
                    break;
                case "downloads_desc":
                    songs = songs.OrderByDescending(s => s.Downloads);
                    break;
                default:  // Name ascending 
                    songs = songs.OrderBy(s => s.Title);
                    break;
            }

            var readySongs = songs.ToList();
            List<SongDetailsViewModel> allSongs = new List<SongDetailsViewModel>();
            if (readySongs.Count != 0)
            {
                foreach (var song in readySongs)
                {
                    allSongs.Add(Mapper.Map<SongDetailsViewModel>(song));
                }
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(allSongs.ToPagedList(pageNumber, pageSize));
        }
    }
}