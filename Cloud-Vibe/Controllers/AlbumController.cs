namespace Cloud_Vibe.Controllers
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

        [HttpGet]
        public ActionResult AllAlbums(string sortOrder, string currentFilter, string searchString, int? page)
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

            var albums = data.Albums.All();

            if (!String.IsNullOrEmpty(searchString))
            {
                albums = albums.Where(s => s.Title.Contains(searchString)
                                       || s.Artist.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    albums = albums.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    albums = albums.OrderBy(s => s.SharedOn);
                    break;
                case "date_desc":
                    albums = albums.OrderByDescending(s => s.SharedOn);
                    break;
                case "Artist":
                    albums = albums.OrderBy(s => s.Artist.Name);
                    break;
                case "artist_desc":
                    albums = albums.OrderByDescending(s => s.Artist.Name);
                    break;
                case "Downloads":
                    albums = albums.OrderBy(s => s.Downloads);
                    break;
                case "downloads_desc":
                    albums = albums.OrderByDescending(s => s.Downloads);
                    break;
                default:  // Name ascending 
                    albums = albums.OrderBy(s => s.Title);
                    break;
            }

            var readyAlbums = albums.ToList();
            List<AlbumDetailsViewModel> allSongs = new List<AlbumDetailsViewModel>();

            if (readyAlbums.Count != 0)
            {
                foreach (var song in readyAlbums)
                {
                    allSongs.Add(Mapper.Map<AlbumDetailsViewModel>(song));
                }
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(allSongs.ToPagedList(pageNumber, pageSize));
        }
    }
}