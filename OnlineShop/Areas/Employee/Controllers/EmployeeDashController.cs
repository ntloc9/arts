using OnlineShop.Areas.Admin.Extension;
using OnlineShop.Areas.Extension;
using OnlineShop.DAL;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Employee.Controllers
{
    //[RoutePrefix("employee")]
    public class EmployeeDashController : BaseController
    {
        artsDBContext db = new artsDBContext();
        // GET: Employee/EmployeeDash
        //[Route("orders")]
        public ActionResult Index()
        {
            var session = (AccountLoginSession)Session[SessionCont.SessionEmployee];
            var orders = db.shippings.Where(o => o.AE_id == session.AccountID && o.Shipping_status.Equals(DeliveryCons.Shipping)).ToList();

            return View(orders);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldpass, string newpass)
        {
            //find current account session
            var session = (AccountLoginSession)Session[SessionCont.SessionEmployee];
            //get account info
            var getuser = new Helper().GetByName(session.AccountName);

            //compare password and apply change
            if (getuser.AE_password.Equals(EncrypPass.MD5Hash(oldpass)))
            {
                var user = new AdminEmployee() { AE_id = getuser.AE_id, AE_password = EncrypPass.MD5Hash(newpass) };
                db.adminEmployees.Attach(user);
                db.Entry(user).Property(u => u.AE_password).IsModified = true;
                db.Configuration.ValidateOnSaveEnabled = false;
                TempData["Changepass"] = "Change password successfully";
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        
    }
}