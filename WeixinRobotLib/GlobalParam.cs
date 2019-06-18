using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Diagnostics;


using System.Reflection;
using System.Net;
using System.Xml;

namespace WeixinRobotLib
{
    public class UserParam
    {
        public string UserName;
        public string Password;
        public string ASPXAUTH;
        public bool LogInSuccess = false;
        public CookieContainer LoginCookie = new CookieContainer();

        public Guid UserKey = Guid.Empty;
        public Guid JobID = Guid.Empty;


        public string DataSourceName = "";

        public static string MemberSourceode { get; set; }



    }





    public class MatchTypeHTMLFormat
    {
        public string MatchType { get; set; }
        public string HtmlData { get; set; }
    }

}
