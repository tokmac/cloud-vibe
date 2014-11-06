using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cloud_Vibe.Data.Models
{
    public class Message
    {
        public int ID { get; set; }
        public virtual AppUser Sender { get; set; }
        public string Text { get; set; }
        public virtual MessageStatusTypes MessageStatus { get; set; }
    }
}