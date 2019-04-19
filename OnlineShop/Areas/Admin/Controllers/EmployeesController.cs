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

namespace OnlineShop.Areas.Admin.Controllers
{
    public class EmployeesController : BaseController
    {
        private artsDBContext db = new artsDBContext();
        private readonly string emp = "emp";

        // GET: Admin/AdminEmployees
        public ActionResult Index()
        {
            return View(db.adminEmployees.Where(e => e.AE_permission.Equals(emp)).ToList());
        }

        // GET: Admin/AdminEmployees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminEmployee adminEmployee = db.adminEmployees.Find(id);
            if (adminEmployee == null)
            {
                return HttpNotFound();
            }
            return View(adminEmployee);
        }

        // GET: Admin/AdminEmployees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AE_id,AE_name,AE_password,AE_phone")] AdminEmployee adminEmployee)
        {
            if (ModelState.IsValid)
            {
                adminEmployee.AE_permission = "emp";
                adminEmployee.AE_password = EncrypPass.MD5Hash(adminEmployee.AE_password);
                db.adminEmployees.Add(adminEmployee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adminEmployee);
        }

        // GET: Admin/AdminEmployees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminEmployee adminEmployee = db.adminEmployees.Find(id);
            if (adminEmployee == null)
            {
                return HttpNotFound();
            }
            return View(adminEmployee);
        }

        // POST: Admin/AdminEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AE_id,AE_name,AE_password,AE_phone,AE_permission")] AdminEmployee adminEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminEmployee);
        }

        // GET: Admin/AdminEmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminEmployee adminEmployee = db.adminEmployees.Find(id);
            if (adminEmployee == null)
            {
                return HttpNotFound();
            }
            return View(adminEmployee);
        }

        // POST: Admin/AdminEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdminEmployee adminEmployee = db.adminEmployees.Find(id);
            db.adminEmployees.Remove(adminEmployee);
            db.SaveChanges();
            return RedirectToAction("Index");
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
