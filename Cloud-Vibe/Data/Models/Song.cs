using Cloud_Vibe.Data.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Cloud_Vibe.Data.Models
{
    public class Song : DeletableEntity, IDownloadable
    {
        public Song()
        {
            this.Thanxies = new HashSet<Thanx>();
            this.UsersDownloaded = new HashSet<User>();
            this.Comments = new HashSet<Comment>();
        }

        public int ID { get; set; }

        [Display(Name = "Artist")]
        public virtual Artist Artist { get; set; }
        
        [Required(ErrorMessage="Cannot save song with empty title")]
        [Index(IsUnique = true)]
        [MaxLength(450)]
        public string Title { get; set; }

        public byte[] CoverArt { get; set; }

        public int Year { get; set; }

        public DateTime SharedOn { get; set; }

        public virtual User UserShared { get; set; }

        [Required(ErrorMessage="You need to add torrent file")]
        public byte[] Torrent { get; set; }

        public string VideoLink { get; set; }

        public int Views { get; set; }

        public int Downloads { get; set; }

        [InverseProperty("Song")]
        public virtual ICollection<Thanx> Thanxies { get; set; }

        public virtual ICollection<User> UsersDownloaded { get; set; }

        [InverseProperty("Song")]
        public virtual ICollection<Comment> Comments { get; set; }

        public string TypeMIME { get; set; }
    }
}
