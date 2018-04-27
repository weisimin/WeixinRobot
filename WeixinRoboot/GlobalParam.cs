using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

using System.Reflection;
using System.Net;
using System.Xml;
using System.Web.Security;
namespace WeixinRoboot
{
    public class GlobalParam
    {
        public static string UserName;
        public static bool LogInSuccess = false;
        public static Guid Key = Guid.Empty;

        public static Linq.dbDataContext db {
            get { return _db; }
            set { _db = value;
            }
        }
        private static Linq.dbDataContext _db = null;     

    }

}
