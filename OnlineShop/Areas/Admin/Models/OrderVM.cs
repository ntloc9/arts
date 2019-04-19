using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class OrderVM
    {
        [Key]
        public int Order_id { get; set; }
        public int User_id { get; set; }
        public string Order_status { get; set; }
        public DateTime Order_date { get; set; }
        public decimal Total { get; set; }
        public string Payment_method { get; set; }
        public string Shipper { get; set; }
        public DateTime DateStartShip { get; set; }
        public DateTime DateReceived
        {
            get; set;
        }
    }
}