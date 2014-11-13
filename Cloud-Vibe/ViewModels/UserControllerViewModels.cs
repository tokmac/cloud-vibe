using Cloud_Vibe.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cloud_Vibe.Models.ViewModels
{
    public class ShareAlbumViewModel
    {
        [Required]
        [Display(Name = "Album Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Artist Name")]
        public string ArtistName { get; set; }

        [Display(Name = "Year of Release")]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Torrent File")]
        public HttpPostedFileBase Torrent { get; set; }

        [Display(Name = "Album Art")]
        public HttpPostedFileBase CoverArt { get; set; }

        [Display(Name = "YouTube or Vimeo link")]
        public string VideoLink { get; set; }
    }

    public class AddArtistViewModel
    {
        [Required]
        [Display(Name = "Artist Name")]
        public string Name { get; set; }

        [Display(Name = "Artist Biography")]
        public string Biography { get; set; }

        [Required]
        [Display(Name = "Artist Picture")]
        public HttpPostedFileBase Picture { get; set; }

        [Display(Name = "Album Official Website")]
        public string WebSite { get; set; }

        [Required]
        [Display(Name = "Is the artist well known?")]
        public bool IsFamous { get; set; }
    }

    public class ShareSongViewModel
    {
        [Display(Name = "Artist Name")]
        public string ArtistName { get; set; }

        [Required(ErrorMessage = "Cannot save song with empty title")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Cover picture")]
        public HttpPostedFileBase CoverArt { get; set; }

        [Display(Name = "Year of Release")]
        public int Year { get; set; }

        public DateTime SharedOn { get; set; }

        [Required(ErrorMessage = "You need to add torrent file")]
        [Display(Name = "Add Torrent File")]
        public HttpPostedFileBase Torrent { get; set; }

        [Display(Name = "YouTube or Vimeo link")]
        public string VideoLink { get; set; }

    }

    public class AlbumDetailsViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int? ID { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Artist Name")]
        public Artist Artist { get; set; }

        [Display(Name = "Year of Release")]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Shared on")]
        public DateTime SharedOn { get; set; }

        [Display(Name = "User Shared the Album")]
        public User UserShared { get; set; }

        [Required]
        [Display(Name = "Torrent File")]
        public byte[] Torrent { get; set; }
        public string TypeMIME { get; set; }

        [Display(Name = "Cover Art")]
        public byte[] CoverArt { get; set; }

        [Display(Name = "Videos")]
        public List<string> VideosLinks { get; set; }

        [Display(Name = "Downloaded")]
        public int Downloads { get; set; }

        [Display(Name = "Viewed")]
        public int Views { get; set; }

        [Display(Name = "Comments")]
        public ICollection<Comment> Comments { get; set; }
    }

    public class SongDetailsViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Display(Name = "Artist")]
        public virtual Artist Artist { get; set; }

        [Required(ErrorMessage = "Cannot save song with empty title")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Cover Art")]
        public byte[] CoverArt { get; set; }

        [Display(Name = "Year of Release")]
        public int Year { get; set; }

        [Display(Name = "Shared on")]
        public DateTime SharedOn { get; set; }

        [Display(Name = "Shared by")]
        public User UserShared { get; set; }

        [Required(ErrorMessage = "You need to add torrent file")]
        [Display(Name = "Torrent file")]
        public byte[] Torrent { get; set; }
        public string TypeMIME { get; set; }

        [Display(Name = "Video")]
        public string VideoLink { get; set; }

        [Display(Name = "Viewed")]
        public int Views { get; set; }

        [Display(Name = "Downloaded")]
        public int Downloads { get; set; }
    }
}