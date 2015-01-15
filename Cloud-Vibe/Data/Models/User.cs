using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Cloud_Vibe.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.SharedAlbums = new HashSet<Album>();
            this.DownloadedAlbums = new HashSet<Album>();
            this.SharedSongs = new HashSet<Song>();
            this.DownloadedSongs = new HashSet<Song>();
            this.SocialAccoutsLinks = new HashSet<SocialAccountLink>();
            this.Comments = new HashSet<Comment>();
            this.FavoriteArtists = new HashSet<Artist>();
            this.MessagesSent = new HashSet<Message>();
            this.MessagesRecieved = new HashSet<Message>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        //[Required]
        public string FirstName { get; set; }
        //[Required]
        public string LastName { get; set; }
        public Byte[] Avatar { get; set; }
        public bool HasLoggedWithSocial { get; set; }

        [InverseProperty("UserShared")]
        public virtual ICollection<Album> SharedAlbums { get; set; }
        public virtual ICollection<Album> DownloadedAlbums { get; set; }

        [InverseProperty("UserShared")]
        public virtual ICollection<Song> SharedSongs { get; set; }
        public virtual ICollection<Song> DownloadedSongs { get; set; }

        public virtual ICollection<SocialAccountLink> SocialAccoutsLinks { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Artist> FavoriteArtists { get; set; }
        [InverseProperty("Sender")]
        public virtual ICollection<Message> MessagesSent { get; set; }
        [InverseProperty("Receiver")]
        public virtual ICollection<Message> MessagesRecieved { get; set; }
    }
}