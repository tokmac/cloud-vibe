using Cloud_Vibe.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cloud_Vibe.Models.ViewModels
{
    public class AlbumViewModels
    {
        public class AlbumDetailsViewModel
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
            public byte[] Torrent { get; set; }

            [Display(Name = "Album Art")]
            public byte[] CoverArt { get; set; }

            [Display(Name = "User")]
            public User UserShared { get; set; }

            [Display(Name = "Shared on")]
            public DateTime SharedOn { get; set; }

            [Display(Name = "YouTube or Vimeo link")]
            public string VideoLink { get; set; }

            [Display(Name = "Downloaded")]
            public int Downloads { get; set; }

            [Display(Name = "Viewed")]
            public int Views { get; set; }

            [Display(Name = "Thanxies")]
            public int Thanxies { get; set; }

            [Display(Name = "Comments")]
            public ICollection<Comment> Comments { get; set; }
        }
    }
}