using Cloud_Vibe.Data.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cloud_Vibe.Data.Models
{
    public class Album : DeletableEntity, IDownloadable
    {
        public Album()
        {
            this.Thanxies = new HashSet<Thanx>();
            this.UsersDownloaded = new HashSet<User>();
            this.Comments = new HashSet<Comment>();
        }

        public int ID { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [MaxLength(450)]
        public string Title { get; set; }
        public virtual Artist Artist { get; set; }
        public int Year { get; set; }
        public DateTime SharedOn { get; set; }
        public virtual User UserShared { get; set; }
        public byte[] Torrent { get; set; }
        public byte[] CoverArt { get; set; }
        public string VideoLink { get; set; }
        public int Downloads { get; set; }
        public int Views { get; set; }
        [InverseProperty("Album")]
        public virtual ICollection<Thanx> Thanxies { get; set; }
        public virtual ICollection<User> UsersDownloaded { get; set; }
        [InverseProperty("Album")]
        public virtual ICollection<Comment> Comments { get; set; }

        public string TypeMIME { get; set; }
    }
}