using System;
using System.Collections.Generic;
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
        public string Name { get; set; }
        public string Biography { get; set; }
        public string Picture { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
        public string WebSite { get; set; }
        public virtual ICollection<SocialAccountLink> SocialAccounts { get; set; }
        public bool IsFamous { get; set; }
    }
}