using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Login
{
    [Serializable]
    public class UserLogin
    {
        [Key]
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Please! Enter your username")]
        public string name { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please! Enter your password")]
        public string pass { get; set; }
    }
}