using OnlineShop.Areas.Admin.Extension;
using OnlineShop.Areas.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // check UserLoginSession before continue to orther controller
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (AccountLoginSession)Session[SessionCont.SessionAdmin];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}