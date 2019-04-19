using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class Brand
    {
        [Key]
        public int Brand_id { get; set; }
        [Required]
        [StringLength(50)]
        public string Brand_name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}