using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class Shipping
    {
        [ForeignKey("Order")]
        [Key]
        public int Shipping_id { get; set; }
        public int? AE_id { get; set; }
        public int Order_id { get; set; }
        public DateTime Shipping_start_date { get; set; }
        public string Shipping_status { get; set; }
        public DateTime? Received_date { get; set; }

        public virtual Order Order { get; set; }
        public virtual AdminEmployee AdminEmployee { get; set; }
    }
}