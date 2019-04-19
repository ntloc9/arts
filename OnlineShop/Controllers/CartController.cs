using OnlineShop.DAL;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        artsDBContext db = new artsDBContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Buy(int id)
        {
            Product product = new Product();
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Product = getbyID(id), Quantity = 1 });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Product = getbyID(id), Quantity = 1 });
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult Remove(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        public Product getbyID(int ID)
        {
            var pro = db.products.Find(ID);
            return pro;
        }
        private int isExist(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.Product_id.Equals(id))
                    return i;
            return -1;
        }

        public ActionResult Checkout(int userid, decimal total, string payment)
        {
            var order = new Order();
            order.Order_date = DateTime.Now;
            order.User_id = userid;
            order.Total = total;
            order.Order_status = "proccessing";
            order.Payment_method = payment;
            db.orders.Add(order);
            db.SaveChanges();
            return RedirectToAction("Success", "Cart");
        }

        public ActionResult Success()
        {
            Session["cart"] = null;
            return View();
        }
    }
}