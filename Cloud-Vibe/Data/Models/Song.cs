using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Cloud_Vibe.Data.Models
{
    public class Song
    {
        public Song()
        {
            this.Thanxies = new HashSet<Thanx>();
        }

        public int ID { get; set; }

        [Display(Name = "Artist")]
        public virtual Artist Artist { get; set; }
        
        public string Title { get; set; }

        public byte CoverArt { get; set; }

        public int Year { get; set; }

        public DateTime SharedOn { get; set; }
        [Display(Name = "User Shared")]
        public virtual AppUser UserShared { get; set; }

        public byte[] Torrent { get; set; }

        public string VideoLink { get; set; }

        public int Views { get; set; }

        public int Downloads { get; set; }

        [Display(Name = "Thanxies")]
        public virtual ICollection<Thanx> Thanxies { get; set; }
    }
}
