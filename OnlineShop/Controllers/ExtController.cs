using OnlineShop.DAL;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ExtController : Controller
    {
        // GET: Ext
        public ActionResult CategoryMenuPartial()
        {
            artsDBContext db = new artsDBContext();
            // Declare list of CategoryVM
            List<Category> categoryVMList;

            // Init the list
            categoryVMList = db.categories.ToArray().OrderBy(x => x.Cate_name).ToList();

            // Return partial with list
            return PartialView(categoryVMList);
        }

    }
}