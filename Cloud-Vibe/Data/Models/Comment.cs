using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cloud_Vibe.Data.Models
{
    public class Comment
    {
        public int ID { get; set; }

        [Required]
        public virtual User User { get; set; }

        [Required(ErrorMessage="Cannot post empty comment")]
        public string Text { get; set; }

        public virtual Song Song { get; set; }

        public virtual Album Album { get; set; }
    }
}