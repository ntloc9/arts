using OnlineShop.Areas.Extension;
using OnlineShop.DAL;
using OnlineShop.Models;
using OnlineShop.Models.Login;
using OnlineShop.Models.UserLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class RegistryController : Controller
    {
        private artsDBContext db = new artsDBContext();
        // GET: Registry
        public ActionResult Index()
        {
            return View();
        }

        //Registry Customers
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            if (db.users.Any(u => u.User_name == user.User_name))
            {
                ModelState.AddModelError("", "User name was taken!");
                return View(user);
            }
            try
            {
                user.User_password = EncrypPass.MD5Hash(user.User_password);
                db.users.Add(user);
                db.SaveChanges();
                TempData["regSuccess"] = "sign up success, login with your account";
                return RedirectToAction("Login", "Registry");
            }
            catch (Exception)
            {
                TempData["regSuccess"] = "sign up fail, please try again";
                return View(user);
            }
        }

        //User_Login_Customers

        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Login(string name, string pass)
        {
            try
            {
                var user = db.users.FirstOrDefault(u => u.User_name == name);
                if (user == null)
                {
                    ModelState.AddModelError("", "Account doesn't exist!");
                    return View();
                }
                else if (user.User_password == EncrypPass.MD5Hash(pass))
                {
                    var userSession = new UserSession();
                    userSession.name = user.User_name;
                    userSession.id = user.User_id;
                    Session.Add(Sessioncont.USER_SESSION, userSession);
                    return Redirect("/");

                }
                else
                {
                    TempData["log"] = "fail";
                    ModelState.AddModelError("", "Please!Check account/password doesn't exist!");
                    return View();
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
                throw;
            }
        }

        public ActionResult Logout()
        {
            Session.Remove(Sessioncont.USER_SESSION);
            return RedirectToAction("Index", "Home");
        }

    }
}