using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.DAL.AdminVM
{
    public class ProductVM
    {
        [Key]
        public int Product_id { get; set; }
        [Required]
        public int Brand_id { get; set; }
        [Required]
        public int Cate_id { get; set; }
        [Required]
        public string Product_name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime Date_created { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }
        public string Product_code { get; set; }
    }
}