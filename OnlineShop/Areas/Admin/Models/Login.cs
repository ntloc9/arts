using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class Login
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Pass { get; set; }
    }
}