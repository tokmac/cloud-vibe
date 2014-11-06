using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cloud_Vibe.Data.Models
{
    public class SocialNetwork
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}