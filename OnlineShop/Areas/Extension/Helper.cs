using OnlineShop.DAL;
using OnlineShop.Models;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Extension
{
    public class Helper
    {
        private artsDBContext db = new artsDBContext();

        //check account, return 1 is admin, return 2 is employee
        public int LoginCheck(string Aname, string Apass, bool isAdmin = false)
        {
            var res = db.adminEmployees.FirstOrDefault(x => x.AE_name == Aname);
            if (res == null)
            {
                return 0;
            }
            else if (isAdmin == true && res.AE_permission.Equals("admin"))
            {
                if (res.AE_password == Apass) 
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            else 
            {
                if (res.AE_password == Apass)
                {
                    return 2;
                }
                else
                {
                    return -2;
                }
            }
           
        }
        
        // Check username was duplicate?
        public bool CheckName(string userName)
        {
            return db.users.Count(x => x.User_name == userName) > 0;
        }

        // Check email was duplicate?
        public bool CheckEmail(string email)
        {
            return db.users.Count(x => x.User_email == email) > 0;
        }

        // Get info of account by name
        public AdminEmployee GetByName(string Name)
        {
            return db.adminEmployees.SingleOrDefault(x => x.AE_name == Name);
        }

        // Get info of account by id
        public AdminEmployee ViewDetail(int id)
        {
            return db.adminEmployees.Find(id);
        }
        // sign up
        public long Insert(User entity)
        {
            db.users.Add(entity);
            db.SaveChanges();
            return entity.User_id;
        }

        public string GenDeProNum(string payment, int user_id, string product_code)
        {
            string res;
            string payStr ="";
            string proStr = product_code;
            string UDAY = string.Format("{0:0000}", user_id) + DateTime.Now.ToString("MMyy");
            if (payment.Contains("Card"))
            {
                payStr = "1"; 
            }
            if (payment.Contains("VPP"))
            {
                payStr = "2";
            }
            else
            {
                payStr = "3";
            }
            res = payStr + proStr + UDAY;
            return res;
        }
    }   
}