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
    using Cloud_Vibe.Models;
    using Cloud_Vibe.Models.ViewModels;

    [Authorize]
    public class UserController : BaseController
    {
        public UserController(ICloudVibeData data)
            : base(data)
        {

        }

        // GET: User
        public ActionResult Index()
        {
            Mapper.CreateMap<Album, AlbumDetailsViewModel>();
            Mapper.CreateMap<Song, SongDetailsViewModel>();

            IEnumerable<Album> allAlbumsOrdered = data.Albums.All().OrderByDescending(a => a.SharedOn).ToList().Take(10);
            IEnumerable<Song> allSongsOrdered = data.Songs.All().OrderByDescending(a => a.SharedOn).ToList().Take(10);

            List<AlbumDetailsViewModel> latestAlbums = new List<AlbumDetailsViewModel>();
            List<SongDetailsViewModel> latestSongs = new List<SongDetailsViewModel>();

            if (allAlbumsOrdered.Count() >= 1)
            {
                foreach (var album in allAlbumsOrdered)
                {
                    AlbumDetailsViewModel currentAlbum = new AlbumDetailsViewModel();
                    Mapper.Map<Album, AlbumDetailsViewModel>(album, currentAlbum);
                    latestAlbums.Add(currentAlbum);
                }
            }

            if (allSongsOrdered.Count() >= 1)
            {
                foreach (var song in allSongsOrdered)
                {
                    SongDetailsViewModel currentSong = new SongDetailsViewModel();
                    Mapper.Map<Song, SongDetailsViewModel>(song, currentSong);
                    latestSongs.Add(currentSong);
                }
            }

            ViewData["latestAlbums"] = latestAlbums;
            ViewData["latestSongs"] = latestSongs;

            return View();
        }

        // GET: User
        [HttpGet]
        public ActionResult Share()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ShareAlbum(ShareAlbumViewModel album)
        {
            string fileType = album.CoverArt.ContentType;
            List<byte[]> box = new List<byte[]>();
            if (album.CoverArt != null)
            {
                box.Add(Utilities.FilesByteUtility.HttpPostedFileToByteArray(album.CoverArt));
            }
            else
            {
                box.Add(Utilities.FilesByteUtility.FileFromPathToByteArray(HttpContext.Server.MapPath("~/Content/images/no_user.jpg")));
            }

            var coverArt = box[0];
            var artist = data.Artists.All().FirstOrDefault(a => a.Name == album.ArtistName);

            if (artist == null)
            {
                artist = new Artist { Name = album.ArtistName, Picture = coverArt };
                //TempData["need-artist"] = "true";
                //return View("Share");
                data.Artists.Add(artist);
                data.SaveChanges();
            }

            var torrent = Utilities.FilesByteUtility.HttpPostedFileToByteArray(album.Torrent);
            var user = data.Users.All().Where(u => u.UserName == User.Identity.Name).ToArray();

            Mapper.CreateMap<ShareAlbumViewModel, Album>()
                .ForMember(c => c.CoverArt, option => option.UseValue(coverArt))
                .ForMember(c => c.Torrent, option => option.UseValue(torrent))
                .ForMember(c => c.Artist, option => option.UseValue(artist))
                .ForMember(c => c.UserShared, option => option.UseValue(user[0]))
                .ForMember(c => c.SharedOn, option => option.UseValue(DateTime.Now))
                .ForMember(c => c.TypeMIME, option => option.UseValue(fileType));


            var albumToSave = Mapper.Map<ShareAlbumViewModel, Album>(album);

            data.Albums.Add(albumToSave);
            artist.Albums.Add(albumToSave);
            user[0].SharedAlbums.Add(albumToSave);
            data.SaveChanges();

            TempData["success"] = String.Format("Succesfully added album {0}", album.ArtistName);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ShareSong(ShareSongViewModel song)
        {
            List<byte[]> box = new List<byte[]>();
            string fileType = song.CoverArt.ContentType;
            if (song.CoverArt != null)
            {
                box.Add(Utilities.FilesByteUtility.HttpPostedFileToByteArray(song.CoverArt));
            }
            else
            {
                box.Add(Utilities.FilesByteUtility.FileFromPathToByteArray(HttpContext.Server.MapPath("~/Content/images/no_user.jpg")));
            }
            var coverArt = box[0];


            var artist = data.Artists.All().FirstOrDefault(a => a.Name == song.ArtistName);
            if (artist == null)
            {
                artist = new Artist { Name = song.ArtistName, Picture = coverArt };
                //TempData["need-artist"] = "true";
                //return View("Share");
                data.Artists.Add(artist);
                data.SaveChanges();
            }

            var torrent = Utilities.FilesByteUtility.HttpPostedFileToByteArray(song.Torrent);

            var user = data.Users.All().Where(u => u.UserName == User.Identity.Name).ToArray();

            Mapper.CreateMap<ShareSongViewModel, Song>()
                .ForMember(c => c.CoverArt, option => option.UseValue(coverArt))
                .ForMember(c => c.Torrent, option => option.UseValue(torrent))
                .ForMember(c => c.Artist, option => option.UseValue(artist))
                .ForMember(c => c.UserShared, option => option.UseValue(user[0]))
                .ForMember(c => c.SharedOn, option => option.UseValue(DateTime.Now))
                .ForMember(c => c.TypeMIME, option => option.UseValue(fileType));


            var songToSave = Mapper.Map<ShareSongViewModel, Song>(song);


            data.Songs.Add(songToSave);
            data.SaveChanges();

            artist.Songs.Add(songToSave);
            data.SaveChanges();

            user[0].SharedSongs.Add(songToSave);
            data.SaveChanges();

            TempData["success"] = String.Format("Succesfully added album {0}", song.ArtistName);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DownloadFile(int id, string name, string item)
        {
            var model = GetModel(id, item);
            var cd = new System.Net.Mime.ContentDisposition
            {
                // for example foo.bak
                FileName = name,

                // always prompt the user for downloading, set to true if you want 
                // the browser to try to show the file inline
                Inline = false,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());

            model.Downloads = model.Downloads + 1;
            data.SaveChanges();

            return File(model.Torrent, "application/x-bittorrent ");
        }



        private IDownloadable GetModel(int id, string type)
        {
            if (type == "song")
            {
                return data.Songs.All().FirstOrDefault(s => s.ID == id);
            }

            return data.Albums.All().FirstOrDefault(a => a.ID == id);
        }

        [HttpGet]
        public ActionResult Search(string sortOrder, string searchString,string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParam = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.DateSortParam = sortOrder == "date_desc" ? "Date" : "date_desc";
            ViewBag.ArtistSortParam = sortOrder == "Artist" ? "artist_desc" : "Artist";
            ViewBag.DownloadsSortParam = sortOrder == "downloads_desc" ? "Downloads" : "downloads_desc";
            ViewBag.SearchString = searchString;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var songs = data.Songs.All();
            var albums = data.Albums.All();

            if (!String.IsNullOrEmpty(searchString))
            {
                songs = songs.Where(s => s.Title.Contains(searchString)
                                       || s.Artist.Name.Contains(searchString));

                albums =albums.Where(s => s.Title.Contains(searchString)
                                       || s.Artist.Name.Contains(searchString));
            }

            var readySongs = songs.ToList();
            List<SearchViewModel> allSongs = new List<SearchViewModel>();
            if (readySongs.Count != 0)
            {
                foreach (var song in readySongs)
                {
                    allSongs.Add(Mapper.Map<SearchViewModel>(song));
                }
            }

            var readyAlbums = albums.ToList();
            List<SearchViewModel> allAlbums = new List<SearchViewModel>();
            if (readyAlbums.Count != 0)
            {
                foreach (var album in readyAlbums)
                {
                    allAlbums.Add(Mapper.Map<SearchViewModel>(album));
                }
            }

            IList<SearchViewModel> results = new List<SearchViewModel>();

            foreach (var song in allSongs)
            {
                results.Add(song);
            }
            foreach (var album in allAlbums)
            {
                results.Add(album);
            }

            switch (sortOrder)
            {
                case "title_desc":
                    results = results.OrderByDescending(s => s.Title).ToList();
                    break;
                case "Date":
                    results = results.OrderBy(a => a.SharedOn).ToList();
                    break;
                case "date_desc":
                    results = results.OrderByDescending(s => s.SharedOn).ToList();
                    break;
                case "Artist":
                    results = results.OrderBy(s => s.Artist.Name).ToList();
                    break;
                case "artist_desc":
                    results = results.OrderByDescending(s => s.Artist.Name).ToList();
                    break;
                case "Downloads":
                    results = results.OrderBy(s => s.Downloads).ToList();
                    break;
                case "downloads_desc":
                    results = results.OrderByDescending(s => s.Downloads).ToList();
                    break;
                default:  // Name ascending 
                    results = results.OrderBy(s => s.Title).ToList();
                    break;
            }


            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(results.ToPagedList(pageNumber, pageSize));
        }
    }
}