using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace OnlineShop.Models
{
    public class Wishlist
    {
        [Key]
        public int Wishlist_id { get; set; }
        public int User_id { get; set; }
        public int Product_id { get; set; }

        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}