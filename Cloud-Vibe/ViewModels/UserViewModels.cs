using Cloud_Vibe.Data.Models;
using Cloud_Vibe.Web.Infrastructure.Mapping;
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
        [Display(Name = "Torrent")]
        //[FileExtensions(Extensions="torrent", ErrorMessage="You can add only torrent file")]
        public HttpPostedFileBase Torrent { get; set; }

        [Display(Name = "Album Art")]
        public HttpPostedFileBase CoverArt { get; set; }

        [Display(Name = "YouTube")]
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
        [Required(ErrorMessage = "Artist Name is required!")]
        public string ArtistName { get; set; }

        [Required(ErrorMessage = "Title is required!")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Need to add cover!")]
        [Display(Name = "Cover picture")]
        public HttpPostedFileBase CoverArt { get; set; }

        [Display(Name = "Year of Release")]
        public int Year { get; set; }

        public DateTime SharedOn { get; set; }

        [Required(ErrorMessage = "You need to add torrent file")]
        [Display(Name = "Torrent")]
        //[FileExtensions(Extensions = ".torrent", ErrorMessage = "You can add only torrent file")]
        public HttpPostedFileBase Torrent { get; set; }

        [Display(Name = "YouTube")]
        public string VideoLink { get; set; }

    }

    public class AlbumDetailsViewModel : IMapFrom<Album>
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

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
        public string VideoLink { get; set; }

        [Display(Name = "Downloaded")]
        public int Downloads { get; set; }

        [Display(Name = "Viewed")]
        public int Views { get; set; }

        [Display(Name = "Comments")]
        public ICollection<Comment> Comments { get; set; }
    }

    public class SongDetailsViewModel : IMapFrom<Song>
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

    public class SearchViewModel : IMapFrom<Song>, IMapFrom<Album>
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

        [Display(Name = "Shared on")]
        public DateTime SharedOn { get; set; }

        [Display(Name = "Shared by")]
        public User UserShared { get; set; }

        [Display(Name = "Viewed")]
        public int Views { get; set; }

        [Display(Name = "Downloaded")]
        public int Downloads { get; set; }
    }

    public class CommentViewModel : IMapFrom<Comment>
    {
        public User User { get; set; }
        public string Text { get; set; }
        public DateTime SharedOn { get; set; }
    }
}