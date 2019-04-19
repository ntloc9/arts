using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using OnlineShop.DAL;
using OnlineShop.Models;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CategoriesController : BaseController
    {
        private artsDBContext db = new artsDBContext();

        // GET: Admin/Categories
        public ActionResult Index()
        {
            return View(db.categories.ToList());
        }

        // GET: Admin/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cate_id,Cate_name")] Category category, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                db.categories.Add(category);
                db.SaveChanges();

                var id = category.Cate_id;
                #region Image Upload

                // Create necessary directories
                var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));

                var pathString1 = Path.Combine(originalDirectory.ToString(), "Categories");
                var pathString2 = Path.Combine(originalDirectory.ToString(), "Categories\\" + id.ToString());
                var pathString3 = Path.Combine(originalDirectory.ToString(), "Categories\\" + id.ToString() + "\\Thumbs");

                if (!Directory.Exists(pathString1))
                    Directory.CreateDirectory(pathString1);

                if (!Directory.Exists(pathString2))
                    Directory.CreateDirectory(pathString2);

                if (!Directory.Exists(pathString3))
                    Directory.CreateDirectory(pathString3);

                // Check if a file was uploaded
                if (file != null && file.ContentLength > 0)
                {
                    // Get file extension
                    string ext = file.ContentType.ToLower();

                    // Verify extension
                    if (ext != "image/jpg" &&
                        ext != "image/jpeg" &&
                        ext != "image/pjpeg" &&
                        ext != "image/gif" &&
                        ext != "image/x-png" &&
                        ext != "image/png")
                    {
                        ModelState.AddModelError("", "The image was not uploaded - wrong image extension.");
                        return View(category);
                    }

                    // Init image name
                    string imageName = file.FileName;

                    // Save image name to DTO
                    Category dto = db.categories.Find(id);
                    dto.Cate_image = imageName;

                    db.SaveChanges();

                    // Set original and thumb image paths
                    var path = string.Format("{0}\\{1}", pathString2, imageName);
                    var path2 = string.Format("{0}\\{1}", pathString3, imageName);

                    // Save original
                    file.SaveAs(path);

                    // Create and save thumb
                    WebImage img = new WebImage(file.InputStream);
                    img.Resize(200, 200);
                    img.Save(path2);
                }

                #endregion
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cate_id,Cate_name")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var category = db.categories.Find(id);
            db.categories.Remove(category);
            db.SaveChanges();
            TempData["delete"] = "Success";
            return RedirectToAction("Index");
        }

        // POST: Admin/Categories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Category category = db.categories.Find(id);
        //    db.categories.Remove(category);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
