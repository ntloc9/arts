using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class Register
    {
        [Key]
        public long ID { set; get; }

        [Required(ErrorMessage = "Name is required")]
        public string UserName { set; get; }

        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password at least 6 word.")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { set; get; }

        [Compare("Password", ErrorMessage = "Password is not right.")]
        public string ConfirmPassword { set; get; }

        public string Address { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập email")]
        public string Email { set; get; }

        public string Phone { set; get; }
    }
}