using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace OnlineShop.Models
{
    public class Category
    { 
        [Key]
        public int Cate_id { get; set; }
        [Required]
        [StringLength(50)]
        public string Cate_name { get; set; }
        public string Cate_image { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}