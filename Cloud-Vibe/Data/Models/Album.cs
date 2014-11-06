using System;
using System.Collections.Generic;
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
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public virtual Artist Artist { get; set; }
        public int Year { get; set; }
        public DateTime SharedOn { get; set; }
        public virtual AppUser UserShared { get; set; }
        public byte Torrent { get; set; }
        public byte CoverArt { get; set; }
        public List<string> VideosLinks { get; set; }
        public int Downloads { get; set; }
        public int Views { get; set; }
        public virtual ICollection<Thanx> Thanxies { get; set; }
    }
}