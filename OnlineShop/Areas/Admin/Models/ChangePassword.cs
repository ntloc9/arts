using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class ChangePassword
    {
        public string oldpass { get; set; }
        public string newpass { get; set; }
    }
}