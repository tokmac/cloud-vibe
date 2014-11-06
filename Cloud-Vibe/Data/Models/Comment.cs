using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cloud_Vibe.Data.Models
{
    public class Comment
    {
        public int ID { get; set; }

        public virtual AppUser User { get; set; }

        public string Text { get; set; }
    }
}