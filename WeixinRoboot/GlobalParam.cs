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
        public static Guid UserKey = Guid.Empty;

        public static Dictionary<string, Boolean> HaveSend = new Dictionary<string, bool>();

        private static Guid _JobID = Guid.Empty;

        public static Guid JobID
        {
            get {
                if (_JobID==Guid.Empty)
                {
                    _JobID = Guid.NewGuid();
                }

                return _JobID;
            
            }
        }


    }


    public class WinSends
    {
        public WinSends()
        {
            足球时间 = DateTime.MinValue;
            篮球时间 = DateTime.MinValue;
            足球图片 = false;
            篮球图片 = false;
            开奖结果 = true;

        }
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



        public bool 足球图片 { get; set; }
        public bool 篮球图片 { get; set; }

        private bool _球赛链接 = false;

        public bool 球赛链接 { get { return _球赛链接; } set { _球赛链接 = value; } }



        private Int32 _球赛间隔 = 240;
        public Int32 球赛间隔
        {
            get { return _球赛间隔; }
            set { _球赛间隔 = value; }
        }

        private Int32 _封盘小时 = 1;
        private Int32 _封盘分钟 = 55;

        private Int32 _开盘小时 = 10;
        private Int32 _开盘分钟 = 00;

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

        public string WinClassName { get; set; }

        public string 窗口名字 { get; set; }

        public bool 开盘 { get { return _开盘; } set { _开盘 = value; } }
        public bool 整点停止 { get { return _整点停止; } set { _整点停止 = value; } }
        public bool 封盘 { get { return _封盘; } set { _封盘 = value; } }
        public bool 高速期 { get { return _高速期; } set { _高速期 = value; } }
        public bool 开奖后请下注 { get { return _开奖后请下注; } set { _开奖后请下注 = value; } }
        public bool 龙虎单图
        {
            get { return _龙虎单图; }
            set { _龙虎单图 = value; }
        }
        public bool 名片 { get { return _名片; } set { _名片 = value; } }
        public bool 最后一期 { get { return _最后一期; } set { _最后一期 = value; } }



        public Int32 封盘小时
        {
            get { return _封盘小时; }
            set
            {
                _封盘小时 = value;
                var todel = GlobalParam.HaveSend.Where(t => t.Key.Contains(hwnd.ToString() + "封盘")).ToArray();
                foreach (var item in todel)
                {
                    GlobalParam.HaveSend.Remove(item.Key);
                }


                todel = GlobalParam.HaveSend.Where(t => t.Key.Contains(hwnd.ToString() + "最后一期")).ToArray();
                foreach (var item in todel)
                {
                    GlobalParam.HaveSend.Remove(item.Key);
                }
            }

        }
        public Int32 封盘分钟
        {
            get { return _封盘分钟; }
            set
            {
                _封盘分钟 = value;
                var todel = GlobalParam.HaveSend.Where(t => t.Key.Contains(hwnd.ToString() + "封盘")).ToArray();
                foreach (var item in todel)
                {
                    GlobalParam.HaveSend.Remove(item.Key);
                }
                todel = GlobalParam.HaveSend.Where(t => t.Key.Contains(hwnd.ToString() + "最后一期")).ToArray();
                foreach (var item in todel)
                {
                    GlobalParam.HaveSend.Remove(item.Key);
                }

            }
        }

        public Int32 开盘小时
        {
            get { return _开盘小时; }
            set
            {
                _开盘小时 = value;
                var todel = GlobalParam.HaveSend.Where(t => t.Key.Contains(hwnd.ToString() + "开盘")).ToArray();
                foreach (var item in todel)
                {
                    GlobalParam.HaveSend.Remove(item.Key);
                }




            }
        }
        public Int32 开盘分钟
        {
            get { return _开盘分钟; }
            set
            {
                _开盘分钟 = value;

                var todel = GlobalParam.HaveSend.Where(t => t.Key.Contains(hwnd.ToString() + "开盘")).ToArray();
                foreach (var item in todel)
                {
                    GlobalParam.HaveSend.Remove(item.Key);
                }
            }
        }

        private string _文字1 = "";
        private string _文字2 = "";
        private string _文字3 = "";


        public string 文字1 { get { return _文字1; } set { _文字1 = value; } }
        public string 文字2 { get { return _文字2; } set { _文字2 = value; } }
        public string 文字3 { get { return _文字3; } set { _文字3 = value; } }



        private Int32 _文字1间隔 = 30;
        private Int32 _文字2间隔 = 30;
        private Int32 _文字3间隔 = 30;


        public Int32 文字1间隔 { get { return _文字1间隔; } set { _文字1间隔 = value; } }
        public Int32 文字2间隔 { get { return _文字2间隔; } set { _文字2间隔 = value; } }
        public Int32 文字3间隔 { get { return _文字3间隔; } set { _文字3间隔 = value; } }


        public DateTime 文字1时间 = DateTime.Now;
        public DateTime 文字2时间 = DateTime.Now;
        public DateTime 文字3时间 = DateTime.Now;


        public DateTime 足球时间 { get; set; }

        public DateTime 篮球时间 { get; set; }

        public bool 开奖结果 { get; set; }

        public bool 龙虎图 { get; set; }
        public bool 数字图 { get; set; }

        public bool 数字龙虎文字 { get; set; }
    }


    public class MatchTypeHTMLFormat
    {
    public string MatchType{get;set;}
    public string HtmlData { get; set; }
    }

}
