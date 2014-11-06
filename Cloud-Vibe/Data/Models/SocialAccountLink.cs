using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cloud_Vibe.Data.Models
{
    public class SocialAccountLink
    {
        public int ID { get; set; }

        public virtual SocialNetwork SocialNetwork { get; set; }
        public string AccountLink { get; set; }
    }
}