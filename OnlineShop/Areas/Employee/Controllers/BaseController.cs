using OnlineShop.Areas.Admin.Extension;
using OnlineShop.Areas.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop.Areas.Employee.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (AccountLoginSession)Session[SessionCont.SessionEmployee];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Employee" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}