
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace OnlineShop.Models
{
    public class User
    {
        [Key]
        public int User_id { get; set; }
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Username is required")]
        public string User_name { get; set; }
        [Display(Name = "Password")]
        public string User_password { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        [Required(ErrorMessage = "Email is required")]
        public string User_email { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string User_address { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string User_phone_number { get; set; }

        public virtual ICollection<Wishlist> Wishlists { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}

