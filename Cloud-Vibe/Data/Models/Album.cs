using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cloud_Vibe.Data.Models
{
    public class Album
    {
        public Album()
        {
            this.VideosLinks = new List<string>();
            this.Thanxies = new HashSet<Thanx>();
            this.UsersDownloaded = new HashSet<User>();
            this.Comments = new HashSet<Comment>();
        }

        public int ID { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [MaxLength(450)]
        public string Title { get; set; }
        [Required]
        public virtual Artist Artist { get; set; }
        public int Year { get; set; }
        [Required]
        public DateTime SharedOn { get; set; }
        public virtual User UserShared { get; set; }
        [Required]
        public byte[] Torrent { get; set; }
        public byte[] CoverArt { get; set; }
        public List<string> VideosLinks { get; set; }
        public int Downloads { get; set; }
        public int Views { get; set; }
        [InverseProperty("Album")]
        public virtual ICollection<Thanx> Thanxies { get; set; }
        public virtual ICollection<User> UsersDownloaded { get; set; }
        [InverseProperty("Album")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}