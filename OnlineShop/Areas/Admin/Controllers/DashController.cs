using OnlineShop.Areas.Admin.Extension;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Areas.Extension;
using OnlineShop.DAL;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class DashController : BaseController
    {
        private artsDBContext db = new artsDBContext();

        // GET: Admin/Dash
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        //Change password
        [HttpPost]
        public ActionResult ChangePassword(string oldpass, string newpass)
        {
            //find current account session
            var session = (AccountLoginSession)Session[SessionCont.SessionAdmin];
            //get account info
            var getuser = new Helper().GetByName(session.AccountName);
            
            //compare password and apply change
            if (getuser.AE_password.Equals(EncrypPass.MD5Hash(oldpass)))
            {
                var user = new AdminEmployee() { AE_id = getuser.AE_id, AE_password = EncrypPass.MD5Hash(newpass) };
                db.adminEmployees.Attach(user);
                db.Entry(user).Property(u => u.AE_password).IsModified = true;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
            }
            return View("Index");
        }

        public ActionResult SearchProduct(string searchStr, int mode)
        {
            //search , 1 is by product code | 2 is by product name
            if (mode == 1)
            {
                var products = db.products.Where(p => p.Product_code == searchStr).ToList();
                return View(products);
            }
            else
            {
                var products = db.products.Where(p => p.Product_name.Contains(searchStr)).ToList();
                return View(products);
            }
        }

        public JsonResult GetTotalOrderByMonth()
        {
            var orders = db.orders.Select(o => o.Order_date.Month);
            return Json(orders, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchBrandCate(string searchStr, int mode)
        {
            //search , 1 is search brand | 2 is search cate
            if (mode == 1)
            {
                var brand = db.brands.Where(b => b.Brand_name.Contains(searchStr)).ToList();
                return View(brand);
            }
            else
            {
                var cate = db.categories.Where(c => c.Cate_name.Contains(searchStr)).ToList();
                return View(cate);
            }
        }
    }
}