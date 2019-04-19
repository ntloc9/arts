using OnlineShop.Areas.Admin.Extension;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Areas.Extension;
using OnlineShop.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private artsDBContext db = new artsDBContext();
        private Helper helper = new Helper();
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login login)
        {
            if (ModelState.IsValid)
            {
                var res = helper.LoginCheck(login.Name, EncrypPass.MD5Hash(login.Pass), true);
                if (res == 1)
                {
                    //SessionHelper.setSession(new Session() { UserName = login.Name });
                    var user = helper.GetByName(login.Name);
                    var accountSession = new AccountLoginSession();
                    accountSession.AccountID = user.AE_id;
                    accountSession.AccountName = user.AE_name;
                    Session.Add(SessionCont.SessionAdmin, accountSession);
                    return RedirectToAction("Index", "Dash");
                }
                else if (res == 0)
                {
                    ModelState.AddModelError("", "Account doesn't exist!");
                }
                else if (res == -1)
                {
                    ModelState.AddModelError("", "Wrong Password!");
                }
            }
            return View(login);
        }

        public ActionResult Logout()
        {
            Session.Remove(SessionCont.SessionAdmin);
            return RedirectToAction("Index", "Login");
        }

    }
}