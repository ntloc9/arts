using OnlineShop.Areas.Extension;
using OnlineShop.DAL;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    //[RoutePrefix("user")]
    public class UsersDetailsController : Controller
    {
        private artsDBContext db = new artsDBContext();



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Details Users
        public ActionResult Details(int? id)
        {
            if (id.HasValue == false)
            {
                return RedirectToAction("Index", "Home");
            }

            User user = db.users.Where(p => p.User_id == id).FirstOrDefault();

            return View(user);

        }

        // GET: /Link/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.users.Find(id); // Find link id
            if (user == null)
            {
                return HttpNotFound(); // throw not found
            }
            return View(user); // Found and show data
        }

        // POST: /Link/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_id,User_name,User_password,User_email,User_address,User_phone_number")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified; // Change status
                db.SaveChanges(); // Save db
                return View(user);
            }
            return View(user);
        }

        //Order history
        public ActionResult OrderHistory(int id)
        {
            var orders = db.orders.Where(o => o.User_id == id).ToList();
            return View(orders);
        }

        //Order Detail History
        public ActionResult OrderDetail(int id)
        {
            var products = db.orderProducts.Where(o => o.Order_id == id).ToList();
            return View(products);
        }

    }
}