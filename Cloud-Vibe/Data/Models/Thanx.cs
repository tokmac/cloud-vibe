using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cloud_Vibe.Data.Models
{
    public class Thanx
    {
        public int ID { get; set; }
        public virtual AppUser User { get; set; }

        public virtual Song Song { get; set; }
        public virtual Album Album { get; set; }
    }
}