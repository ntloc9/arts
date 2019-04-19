using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class Product
    {
        [Key]
        public int Product_id { get; set; }
        [Required]
        public int Brand_id { get; set; }
        [Required]
        public int Cate_id { get; set; }
        [Required]
        [StringLength(250)]
        public string Product_name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime Date_created { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }
        public string Image { get; set; }
        [Required]
        [StringLength(7)]
        public string Product_code { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
    }
}