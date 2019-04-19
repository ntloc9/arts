using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop.Areas.Extension
{
    public class AreaAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        private readonly string area;

        public AreaAuthorizeAttribute(string area)
        {
            this.area = area;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string loginUrl = "";

            if (area == "Admin")
            {
                loginUrl = "~/Admin/Login";
            }
            else if (area == "Members")
            {
                loginUrl = "~/Members/Login";
            }

            filterContext.Result = new RedirectResult(loginUrl + "?returnUrl=" + filterContext.HttpContext.Request.Url.PathAndQuery);
        }
    }
}