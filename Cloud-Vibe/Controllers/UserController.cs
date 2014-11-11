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
            IList<Album> allAlbumsOrdered = data.Albums.All().OrderByDescending(a => a.SharedOn).ToList();
            IList<Album> latestAlbums = new List<Album>();

            if (allAlbumsOrdered.Count == 0)
            {
                return View(latestAlbums);
            }
            else if (allAlbumsOrdered.Count < 10)
            {
                for (int i = 0; i < allAlbumsOrdered.Count; i++)
                {
                    latestAlbums.Add(allAlbumsOrdered[i]);
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    latestAlbums.Add(allAlbumsOrdered[i]);
                }
            }

            return View(latestAlbums);
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
                .ForMember(c => c.SharedOn, option => option.UseValue(DateTime.Now));


            var albumToSave = Mapper.Map<ShareAlbumViewModel, Album>(album);

            data.Albums.Add(albumToSave);
            artist.Albums.Add(albumToSave);
            user[0].SharedAlbums.Add(albumToSave);
            data.SaveChanges();

            TempData["success"] = String.Format("Succesfully added album {0}",album.ArtistName);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ShareSong(ShareSongViewModel song)
        {
            List<byte[]> box = new List<byte[]>();
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
                .ForMember(c => c.SharedOn, option => option.UseValue(DateTime.Now));


            var songToSave = Mapper.Map<ShareSongViewModel, Song>(song);

            user[0].SharedSongs.Add(songToSave);
            data.Songs.Add(songToSave);
            artist.Songs.Add(songToSave);
            data.SaveChanges();

            TempData["success"] = String.Format("Succesfully added album {0}", song.ArtistName);
            return RedirectToAction("Index");
        }
    }
}