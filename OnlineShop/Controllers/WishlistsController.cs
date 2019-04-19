using OnlineShop.DAL;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class WishlistsController : Controller
    {
        private artsDBContext db = new artsDBContext();

        // GET: Wishlists
        public ActionResult Index(int user_id)
        {
            var wishlist = db.wishlists.Where(w => w.User_id == user_id).ToList();
            return View(wishlist);
        }
     

        public ActionResult Add(int product_id, int user_id)
        {
            Wishlist wishlist = new Wishlist();
            wishlist.Product_id = product_id;
            wishlist.User_id = user_id;

            var wish = db.wishlists.Add(wishlist);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        

        // POST: /Link/Delete/5
       
        public ActionResult Remove(int id, int user_id)
        {
            Wishlist wishlist = db.wishlists.Find(id);
            db.wishlists.Remove(wishlist);
            db.SaveChanges();
            return RedirectToAction("Index", new { user_id });
        }
    }
}