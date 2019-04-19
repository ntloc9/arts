using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class AdminEmployee
    {
        [Key]
        public int AE_id { get; set; }
        public string AE_name { get; set; }
        public string AE_password { get; set; }
        public string AE_phone { get; set; }
        public string AE_permission { get; set; }
    }
}