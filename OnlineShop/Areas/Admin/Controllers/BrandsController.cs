using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.DAL;
using OnlineShop.Models;
using PagedList;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BrandsController : BaseController
    {
        private artsDBContext db = new artsDBContext();

        // GET: Admin/Brands
        public ActionResult Index(int? page)
        {
            var brands = db.brands.ToList();
            var pageNumber = page ?? 1;
            var onePageOfProducts = brands.ToPagedList(pageNumber, 10);
            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View(brands);
        }

        // GET: Admin/Brands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }
        
        public JsonResult GetBrandSeachData(string searchStr)
        {
            //List<BrandSearch> brands = new List<BrandSearch>();

            var brands = db.brands.Select(b => new { brand_id = b.Brand_id, brand_name = b.Brand_name}).Where(b => b.brand_name.Contains(searchStr) || searchStr == null).ToList();
            return Json(brands,JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Brand_id,Brand_name")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.brands.Add(brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brand);
        }

        // GET: Admin/Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Admin/Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Brand_id,Brand_name")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        // GET: Admin/Brands/Delete/5

        public ActionResult Delete(int? id)
        {   
            try
            {
                var brand = db.brands.Find(id);
                db.brands.Remove(brand);
                db.SaveChanges();
                TempData["delete"] = "Success";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["delete"] = "Can not this delete this brand, it's was stick to the product. You can choose FORCE DELETE to delete this brand go with products";
                return RedirectToAction("Index");
                throw;
            }

        }

        // POST: Admin/Brands/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Brand brand = db.brands.Find(id);
        //    db.brands.Remove(brand);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public ActionResult ForceDelete(int? id)
        {
            try
            {
                var products = db.products.Where(p => p.Brand.Brand_id == id).ToList();
                foreach (var item in products)
                {
                    db.products.Remove(item);
                }
                var brand = db.brands.Find(id);
                db.brands.Remove(brand);
                db.SaveChanges();
                TempData["delete"] = "Success";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["delete"] = "Can not this delete this brand, it's was stick to the product. You can choose FORCE DELETE to delete this brand go with products";
                return RedirectToAction("Index");
                throw;
            }
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
