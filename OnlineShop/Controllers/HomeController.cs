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

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private artsDBContext db = new artsDBContext();
        public  ActionResult Index()
        {
            var cate = db.categories;
            return View(cate.ToList());
            
        }

        [Route("About")]
        public ActionResult About(){
            return View();
        }
    }
}