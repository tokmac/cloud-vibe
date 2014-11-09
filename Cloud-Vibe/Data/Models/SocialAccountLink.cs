using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cloud_Vibe.Data.Models
{
    public class SocialAccountLink
    {
        public int ID { get; set; }

        public virtual SocialNetwork SocialNetwork { get; set; }
        [Required(ErrorMessage="Cannot save empty social link")]
        public string AccountLink { get; set; }
        public virtual User User { get; set; }
        public virtual Artist Artist { get; set; }
    }
}