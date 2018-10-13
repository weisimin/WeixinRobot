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



    }

    public class WinSends
    {
        private IntPtr _hwnd = new IntPtr();
        private string _微信 = "";
        private bool _开盘 = false;
        private bool _整点停止 = false;
        private bool _封盘 = false;
        private bool _高速期 = false;
        private bool _开奖后请下注 = false;
        private bool _龙虎单图 = false;
        private bool _名片 = false;
        private bool _最后一期 = false;

        private Int32 _最后小时 = 1;
        private Int32 _最后分钟 = 50;

        public IntPtr hwnd
        {
            get { return _hwnd; }
            set
            {
                _hwnd = value;

                StringBuilder RAW = new StringBuilder(512);
                NetFramework.WindowsApi.GetWindowText(value, RAW, 512);
                _微信 = RAW.ToString();

            }
        }
        public string 微信 { get { return _微信; } set { _微信 = value; } }
        public bool 开盘 { get { return _开盘; } set { _开盘 = value; } }
        public bool 整点停止 { get { return _整点停止; } set { _整点停止 = value; } }
        public bool 封盘 { get { return _封盘; } set { _封盘 = value; } }
        public bool 高速期 { get { return _高速期; } set { _高速期 = value; } }
        public bool 开奖后请下注 { get { return _开奖后请下注; } set { _开奖后请下注 = value; } }
        public bool 龙虎单图 { get { return _龙虎单图; } set { _龙虎单图 = value; } }
        public bool 名片 { get { return _名片; } set { _名片 = value; } }
        public bool 最后一期 { get { return _最后一期; } set { _最后一期 = value; } }

        public Int32 最后小时 { get { return _最后小时; } set { _最后小时 = value; } }
        public Int32 最后分钟 { get { return _最后分钟; } set { _最后分钟 = value; } }
    }

}
