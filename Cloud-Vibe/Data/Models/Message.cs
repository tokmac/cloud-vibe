using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cloud_Vibe.Data.Models
{
    public class Message
    {
        public int ID { get; set; }

        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
        [Required(ErrorMessage="Cannot send empty message")]
        public string Text { get; set; }
        public virtual MessageStatusTypes MessageStatus { get; set; }
    }
}