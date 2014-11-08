using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cloud_Vibe.Data.Models
{
    public class Artist
    {
        public Artist()
        {
            this.Albums = new HashSet<Album>();
            this.Songs = new HashSet<Song>();
            this.SocialAccounts = new HashSet<SocialAccountLink>();
        }

        public int ID { get; set; }
        [Required]
        [Index(IsUnique=true)]
        [MaxLength(450)]
        public string Name { get; set; }
        public string Biography { get; set; }
        [Required]
        public byte[] Picture { get; set; }
        [InverseProperty("Artist")]
        public virtual ICollection<Album> Albums { get; set; }
        [InverseProperty("Artist")]
        public virtual ICollection<Song> Songs { get; set; }
        public string WebSite { get; set; }
        [InverseProperty("Artist")]
        public virtual ICollection<SocialAccountLink> SocialAccounts { get; set; }
        public virtual ICollection<AppUser> UsersFavored { get; set; }
        public bool IsFamous { get; set; }
    }
}