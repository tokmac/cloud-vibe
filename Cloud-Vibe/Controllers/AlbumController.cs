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
            if (title == null)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                var album = data.Albums.All().FirstOrDefault(s => s.Title == title);
                album.Views = album.Views + 1;
                data.SaveChanges();

                Mapper.CreateMap<Album, AlbumDetailsViewModel>();
                AlbumDetailsViewModel albumToSee = Mapper.Map<AlbumDetailsViewModel>(album);

                var albumsFrom = data.Albums.All().Where(s => s.Artist.Name.Contains(album.Artist.Name)).Take(5).ToList();
                var moreAlbums = new List<AlbumDetailsViewModel>();

                foreach (var alb in albumsFrom)
                {
                    moreAlbums.Add(Mapper.Map<AlbumDetailsViewModel>(alb));
                }

                var albumCurrentComments = data.Comments.All().Where(c => c.Album.ID == albumToSee.ID).ToList();
                var comments = new List<CommentViewModel>();

                foreach (var comment in albumCurrentComments)
                {
                    comments.Add(Mapper.Map<CommentViewModel>(comment));
                }

                ViewBag.MoreAlbums = moreAlbums;
                ViewBag.CommentsInfo = comments;

                return View(albumToSee);
            }
        }

        //[HttpGet]
        //public ActionResult AllAlbums(string sortOrder, string currentFilter, string searchString, int? page)
        //{
        //    ViewBag.CurrentSort = sortOrder;
        //    ViewBag.TitleSortParam = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        //    ViewBag.DateSortParam = sortOrder == "Date" ? "date_desc" : "Date";
        //    ViewBag.ArtistSortParam = sortOrder == "Artist" ? "artist_desc" : "Artist";
        //    ViewBag.DownloadsSortParam = sortOrder == "Downloads" ? "downloads_desc" : "Downloads";

        //    if (searchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }

        //    ViewBag.CurrentFilter = searchString;

        //    var albums = data.Albums.All();

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        albums = albums.Where(s => s.Title.Contains(searchString)
        //                               || s.Artist.Name.Contains(searchString));
        //    }

        //    switch (sortOrder)
        //    {
        //        case "title_desc":
        //            albums = albums.OrderByDescending(s => s.Title);
        //            break;
        //        case "Date":
        //            albums = albums.OrderBy(s => s.SharedOn);
        //            break;
        //        case "date_desc":
        //            albums = albums.OrderByDescending(s => s.SharedOn);
        //            break;
        //        case "Artist":
        //            albums = albums.OrderBy(s => s.Artist.Name);
        //            break;
        //        case "artist_desc":
        //            albums = albums.OrderByDescending(s => s.Artist.Name);
        //            break;
        //        case "Downloads":
        //            albums = albums.OrderBy(s => s.Downloads);
        //            break;
        //        case "downloads_desc":
        //            albums = albums.OrderByDescending(s => s.Downloads);
        //            break;
        //        default:  // Name ascending 
        //            albums = albums.OrderBy(s => s.Title);
        //            break;
        //    }

        //    var readyAlbums = albums.ToList();
        //    List<AlbumDetailsViewModel> allSongs = new List<AlbumDetailsViewModel>();

        //    if (readyAlbums.Count != 0)
        //    {
        //        foreach (var song in readyAlbums)
        //        {
        //            allSongs.Add(Mapper.Map<AlbumDetailsViewModel>(song));
        //        }
        //    }

        //    int pageSize = 5;
        //    int pageNumber = (page ?? 1);
        //    return View(allSongs.ToPagedList(pageNumber, pageSize));
        //}

        [HttpGet]
        public ActionResult AllAlbums(HttpPostedFileBase file)
        {
            ViewBag.Message = "Your application description page.";

            List<Album> allAlbumsDb = data.Albums.All().ToList();
            List<AlbumDetailsViewModel> allAlbums = new List<AlbumDetailsViewModel>();

            foreach (var album in allAlbumsDb)
            {
                allAlbums.Add(Mapper.Map<AlbumDetailsViewModel>(album));
            }


            return View(allAlbums);
        }
    }
}