using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class LoginUser
    {
        [Key]
        [Required(ErrorMessage = "Name is required")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { set; get; }
    }
}