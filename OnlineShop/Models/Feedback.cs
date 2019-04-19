using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace OnlineShop.Models
{
    public class Feedback
    {
        [Key]
        public int Fb_id { get; set; }
        public int User_id { get; set; }
        public string Fb_title { get; set; }
        public string Fb_detail { get; set; }

        public virtual User User { get; set; }
    }
}