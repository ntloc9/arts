using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.VM
{
    public class OrderDetailVM
    {
        //[Key]
        //public int Order_id { get; set; }
        //public int User_id { get; set; }
        public string Order_status { get; set; }
        public DateTime Order_date { get; set; }
        public decimal Total { get; set; }
        public string Payment_method { get; set; }
        public string User_address { get; set; }
        public string User_phone { get; set; }
        public string User_name { get; set; }
    }
}