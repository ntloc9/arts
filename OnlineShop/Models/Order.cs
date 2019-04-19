using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace OnlineShop.Models
{
    public class Order
    {
        [Key]
        public int Order_id { get; set; }
        public int User_id { get; set; }
        public string Order_status { get; set; }
        public DateTime Order_date { get; set; }
        public decimal Total { get; set; }
        public string Payment_method { get; set; }

        public virtual User User { get; set; }
        public virtual Shipping Shipping { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

    }
}