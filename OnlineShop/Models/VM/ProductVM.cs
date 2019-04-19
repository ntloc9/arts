using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.VM
{
    public class ProductVM
    {
        public ProductVM()
        {
        }

        public ProductVM(Product p)
        {
            Product_id = p.Product_id;
            Product_name = p.Product_name;
            Brand_id = p.Brand_id;
            Cate_id = p.Cate_id;
            Price = p.Price;
            Date_created = p.Date_created;
            Quantity = p.Quantity;
            Detail = p.Detail;
            Image = p.Image;
            Product_code = p.Product_code;
        }

        public int Product_id { get; set; }
        public int Brand_id { get; set; }
        public int Cate_id { get; set; }
        public string Product_name { get; set; }
        public decimal Price { get; set; }
        public DateTime Date_created { get; set; }
        public int Quantity { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }
        public string Product_code { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }

    }
}