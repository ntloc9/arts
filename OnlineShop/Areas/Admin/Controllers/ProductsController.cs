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
using PagedList;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductsController : BaseController
    {
        private artsDBContext db = new artsDBContext();

        // GET: Admin/Products
        public ActionResult Index(int? page, int? cateID)
        {
            // Set page number
            var pageNumber = page ?? 1;

            var products = db.products.Where(x => cateID == null || cateID == 0 || x.Cate_id == cateID).Include(p => p.Brand).Include(p => p.Category).ToList();

            ViewBag.Cate_id = new SelectList(db.categories, "Cate_id", "Cate_name");

            // Set selected category
            ViewBag.SelectedCat = cateID.ToString();

            // Set pagination
            var onePageOfProducts = products.ToPagedList(pageNumber, 10);
            ViewBag.OnePageOfProducts = onePageOfProducts;


            return View(products);
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            
            Product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.Brand_id = new SelectList(db.brands, "Brand_id", "Brand_name");
            ViewBag.Cate_id = new SelectList(db.categories, "Cate_id", "Cate_name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct([Bind(Include = "Product_code,Brand_id,Cate_id,Product_name,Price,Date_created,Quantity,Detail,Image")] Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brand_id = new SelectList(db.brands, "Brand_id", "Brand_name", product.Brand_id);
                ViewBag.Cate_id = new SelectList(db.categories, "Cate_id", "Cate_name", product.Cate_id);
                return View("Create", product);
            }

            if (db.products.Any(p => p.Product_name == product.Product_name))
            {
                ViewBag.Brand_id = new SelectList(db.brands, "Brand_id", "Brand_name", product.Brand_id);
                ViewBag.Cate_id = new SelectList(db.categories, "Cate_id", "Cate_name", product.Cate_id);
                ModelState.AddModelError("", "That product name was used!");
                return View("Create", product);
            }

            //Product proDTO = new Product();
            //proDTO.Brand_id = product.Brand_id;
            //proDTO.Cate_id = product.Cate_id;
            //proDTO.Product_name = product.Product_name;
            //proDTO.Price = product.Price;
            //proDTO.Date_created = product.Date_created;
            //proDTO.Quantity = product.Quantity;
            //proDTO.Detail = product.Detail;
            //proDTO.Image = product.Image;

            //int maxID = db.products.Max(p => p.Product_id);

            //string productCode = product.Cate_id.ToString() + "" + string.Format("{0:00000}", maxID + 1);
            //product.Product_code = productCode;

            db.products.Add(product);
            db.SaveChanges();

            var id = product.Product_id;

            TempData["SaveProduct"] = "You have added a product!";

            #region Upload Image

            // Create necessary directories
            var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));

            var pathString1 = Path.Combine(originalDirectory.ToString(), "Products");
            var pathString2 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString());
            var pathString3 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Thumbs");
            var pathString4 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Gallery");
            var pathString5 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Gallery\\Thumbs");

            if (!Directory.Exists(pathString1))
                Directory.CreateDirectory(pathString1);

            if (!Directory.Exists(pathString2))
                Directory.CreateDirectory(pathString2);

            if (!Directory.Exists(pathString3))
                Directory.CreateDirectory(pathString3);

            if (!Directory.Exists(pathString4))
                Directory.CreateDirectory(pathString4);

            if (!Directory.Exists(pathString5))
                Directory.CreateDirectory(pathString5);

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
                    ViewBag.Brand_id = new SelectList(db.brands, "Brand_id", "Brand_name", product.Brand_id);
                    ViewBag.Cate_id = new SelectList(db.categories, "Cate_id", "Cate_name", product.Cate_id);
                    ModelState.AddModelError("", "The image was not uploaded - wrong image extension.");
                    return View(product);
                }

                // Init image name
                string imageName = file.FileName;

                // Save image name to DTO
                Product dto = db.products.Find(id);
                dto.Image = imageName;

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
            return RedirectToAction("Index", "Products");

        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.Brand_id = new SelectList(db.brands, "Brand_id", "Brand_name", product.Brand_id);
            ViewBag.Cate_id = new SelectList(db.categories, "Cate_id", "Cate_name", product.Cate_id);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_id, Brand_id,Cate_id,Product_name,Price,Date_created,Quantity,Detail, Product_code")] Product product, HttpPostedFileBase file)
        {
            ViewBag.Brand_id = new SelectList(db.brands, "Brand_id", "Brand_name", product.Brand_id);
            ViewBag.Cate_id = new SelectList(db.categories, "Cate_id", "Cate_name", product.Cate_id);

            var id = product.Product_id;
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            if (product.Image == null)
            {
                var imagename = db.products.Select(p => new { product_id = p.Product_id, image = p.Image }).FirstOrDefault(p => p.product_id == id);
                product.Image = imagename.image;
            }

            //if (db.products.Any(p => p.Product_name == product.Product_name))
            //{
            //    ViewBag.Brand_id = new SelectList(db.brands, "Brand_id", "Brand_name", product.Brand_id);
            //    ViewBag.Cate_id = new SelectList(db.categories, "Cate_id", "Cate_name", product.Cate_id);
            //    ModelState.AddModelError("", "That product name is taken!");
            //    return View(product);
            //}


            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();


            #region Image Upload

            // Check for file upload
            if (file != null && file.ContentLength > 0)
            {

                // Get extension
                string ext = file.ContentType.ToLower();

                // Verify extension
                if (ext != "image/jpg" &&
                    ext != "image/jpeg" &&
                    ext != "image/pjpeg" &&
                    ext != "image/gif" &&
                    ext != "image/x-png" &&
                    ext != "image/png")
                {
                    ViewBag.Brand_id = new SelectList(db.brands, "Brand_id", "Brand_name", product.Brand_id);
                    ViewBag.Cate_id = new SelectList(db.categories, "Cate_id", "Cate_name", product.Cate_id);
                    ModelState.AddModelError("", "The image was not uploaded - wrong image extension.");
                    return View(product);
                }

                // Set uplpad directory paths
                var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));

                var pathString1 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString());
                var pathString2 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Thumbs");

                // Delete files from directories

                DirectoryInfo di1 = new DirectoryInfo(pathString1);
                DirectoryInfo di2 = new DirectoryInfo(pathString2);

                foreach (FileInfo file2 in di1.GetFiles())
                    file2.Delete();

                foreach (FileInfo file3 in di2.GetFiles())
                    file3.Delete();

                // Save image name

                string imageName = file.FileName;
                if (imageName != null)
                {
                    Product dto = db.products.Find(id);
                    dto.Image = imageName;

                    db.SaveChanges();


                    // Save original and thumb images

                    var path = string.Format("{0}\\{1}", pathString1, imageName);
                    var path2 = string.Format("{0}\\{1}", pathString2, imageName);

                    file.SaveAs(path);

                    WebImage img = new WebImage(file.InputStream);
                    img.Resize(200, 200);
                    img.Save(path2);
                }
            }

            #endregion

            return RedirectToAction("Index", "Products");
        }

        

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                var product = db.products.Find(id);
                db.products.Remove(product);
                db.SaveChanges();
                TempData["deleteProducts"] = "Success";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["deleteProducts"] = "delete fail, please try again";
                return RedirectToAction("Index");
            }
        }

        //// POST: Admin/Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Product product = db.products.Find(id);
        //    db.products.Remove(product);
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
