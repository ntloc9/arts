using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Extension
{
    [Serializable]
    public class AccountLoginSession
    {
        public string AccountName { get; set; }
        public int AccountID { get; set; }
    }
}