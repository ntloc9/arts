using OnlineShop.DAL;
using OnlineShop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    [RoutePrefix("shop")]
    public class ShopController : Controller
    {
        artsDBContext db = new artsDBContext();
        // GET: Shop

        [Route("cate")]
        public ActionResult AllCate()
        {
            var cates = db.categories.ToList();
            return View(cates);
        }

        [Route("cate/{cate_id}")]
        public ActionResult ProductByCate(int? cate_id)
        {
            var products = db.products.Where(p => p.Cate_id == cate_id).ToList();
            return View(products);
        }

        [Route("brand/{brand_id}")]
        public ActionResult ProductByBrand(int? brand_id)
        {
            var products = db.products.Where(p => p.Brand_id == brand_id).ToList();
            return View(products);
        }

        [Route("products")]
        public ActionResult Products(int? page)
        {
            var products = db.products.ToList();
            var pageNumber = page ?? 1;
            var onePageOfProducts = products.ToPagedList(pageNumber, 10);
            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View(products);
        }

        [Route("{id}")]
        public ActionResult Product(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.products.Find(id);
            ViewBag.Brand_id = new SelectList(db.brands, "Brand_id", "Brand_name", product.Brand_id);
            ViewBag.Cate_id = new SelectList(db.categories, "Cate_id", "Cate_name", product.Cate_id);


            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}