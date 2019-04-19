using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace OnlineShop.Models
{
    public class OrderProduct
    {
        [Key]
        public int OrderProduct_id { get; set; }
        public int Order_id { get; set; }
        public int Product_id { get; set; }
        public string DeliOrderProduct_number { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}