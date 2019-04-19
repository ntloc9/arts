using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Areas.Extension;
using OnlineShop.DAL;
using OnlineShop.Models;
using OnlineShop.Models.VM;

namespace OnlineShop.Areas.Employee.Controllers
{
    public class OrdersController : BaseController
    {
        private artsDBContext db = new artsDBContext();

        // GET: Employee/Orders
        public ActionResult Index()
        {
            var orders = db.orders.Where(o => o.Order_status.Equals(DeliveryCons.Processing)).Include(o => o.User);
            return View(orders.ToList());
        }

        // GET: Employee/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.orders.Find(id);
            User user = db.users.Find(order.User_id);
            //OrderDetailVM ODv = new OrderDetailVM();
            //ODv.Order_date = order.Order_date;
            //ODv.Order_status = order.Order_status;
            //ODv.Payment_method = order.Payment_method;
            //ODv.Total = order.Total;
            //ODv.User_name = user.User_name;
            //ODv.User_address = user.User_address;
            //ODv.User_phone = user.User_phone_number;
            if (order == null || user == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        public ActionResult AcceptShipOrder(int empid, int orderid)
        {
            // Add shipping record with current employee
            Shipping shipping = new Shipping();
            shipping.AE_id = empid;
            shipping.Order_id = orderid;
            shipping.Shipping_start_date = DateTime.Now;
            shipping.Shipping_status = DeliveryCons.Shipping;
            db.shippings.Add(shipping);

            // Change order_status of table Order
            var order = new Order() { Order_id = orderid, Order_status = DeliveryCons.Shipping };
            db.orders.Attach(order);
            db.Entry(order).Property(u => u.Order_status).IsModified = true;
            db.Configuration.ValidateOnSaveEnabled = false;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ClientReceivedOrder(int shippingid)
        {
            //try
            //{
            var currentShipping = db.shippings.Find(shippingid);
            int orderId = currentShipping.Order_id;
            // Change table Shipping 
            //var shipping = new Shipping() { Shipping_id = shippingid,
            //                                AE_id = currentShipping.AE_id,
            //                                Shipping_start_date = currentShipping.Shipping_start_date,
            //                                Order_id = currentShipping.Order_id,
            //                                Shipping_status = DeliveryCons.Shipped,
            //                                Received_date = DateTime.Now };

            currentShipping.Shipping_status = DeliveryCons.Shipped;
            currentShipping.Received_date = DateTime.Now;
            db.SaveChanges();

            var currentOrder = db.orders.Find(orderId);
            //Change table Order
            currentOrder.Order_status = DeliveryCons.Received;
            db.SaveChanges();
            //}
            //catch (Exception)
            //{
            //    TempData["shipped"] = "no";
            //}
            
            return RedirectToAction("Index", "EmployeeDash");
        }

        public ActionResult OrdersCompleted()
        {
            var orders = db.orders.Where(o => o.Order_status.Equals(DeliveryCons.Received)).ToList();
            return View(orders);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
