using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Extension
{
    public class LoginModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Pass { get; set; }
    }
}