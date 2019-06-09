using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Xml;
using System.Web;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.IO.Compression;

using System.Configuration;
using WeixinRoboot.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace WeixinRoboot
{
    public partial class StartForm : Form
    {
        EO.WinForm.WebControl wb_ballgame = null;

        EO.WinForm.WebControl wb_other = null;
        EO.WinForm.WebControl wb_refresh = null;

        EO.WinForm.WebControl wb_balllivepoint = null;


        EO.WinForm.WebControl wb_pointlog = null;

        EO.WinForm.WebControl wb_vrchongqing = null;

        public StartForm()
        {
            InitializeComponent();

            //DownLoadTest();
            //DownLoadTest2();
            RunnerF.StartF = this;
            RunnerF.Show();

            RunnerF.MembersSet_firstrun = true;


            //NetFramework.WindowsApi.EnumWindows(new NetFramework.WindowsApi.CallBack(EnumWinsCallBack), 0);



        }

        public string _uuid = "";
        public System.Net.CookieCollection cookie = new CookieCollection();

        public System.Net.CookieCollection cookieyixin = new CookieCollection();

        private void LoadBarCode()
        {
            this.Invoke(new Action(() => { lbl_msg.Text = "等待微信二维码"; }));


            string Result = NetFramework.Util_WEB.OpenUrl("https://login.weixin.qq.com/jslogin?appid=wx782c26e4c19acffb&fun=new&lang=zh_CN&_=" + JavaTimeSpan()
              , "", "", "GET", cookie);


            string UUID = Result.Substring(Result.IndexOf("uuid"));
            UUID = UUID.Substring(UUID.IndexOf("\"") + 1);
            UUID = UUID.Substring(0, UUID.Length - 2);
            _uuid = UUID;
            //登陆https://login.weixin.qq.com/qrcode/XXXXXX
            this.Invoke(new Action(() => { PicBarCode.ImageLocation = "https://login.weixin.qq.com/qrcode/" + UUID + "?t=" + JavaTimeSpan(); }));

        }

        public void SetMode(string Mode, string GameMode)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;

            switch (Mode)
            {
                case "Admin":
                    break;
                case "User":
                    MI_UserSetting.Visible = false;
                    break;
                case "EasyRobot":
                    PicBarCode_yixin.Visible = false;









                    break;
                default:
                    break;
            }
            if (Mode != "Admin" && Mode != "User")
            {

                if (GameMode != "")
                {
                    Linq.aspnet_UsersNewGameResultSend loadset = Util_Services.GetServicesSetting();



                    T_AoZhouCai.Checked = (GameMode == "澳彩" ? true : false);
                    T_chongqingshishicai.Checked = (GameMode == "重庆" ? true : false);
                    T_TengXunShiFen.Checked = (GameMode == "腾十" ? true : false);
                    T_TengXunShiFenXin.Checked = (GameMode == "腾十信" ? true : false);
                    T_TengXunWuFen.Checked = (GameMode == "腾五" ? true : false);
                    T_TengXunWuFenXin.Checked = (GameMode == "腾五信" ? true : false);
                    T_WuFenCai.Checked = (GameMode == "五分" ? true : false);
                    T_XinJiangShiShiCai.Checked = (GameMode == "新疆" ? true : false);
                    T_HeNeiWuFen.Checked = (GameMode == "河内" ? true : false);
                    T_VRChongQingShiShiCai.Checked = (GameMode == "VR" ? true : false);

                    T_AoZhouCai.Visible = (GameMode == "澳彩" ? true : false);
                    T_chongqingshishicai.Visible = (GameMode == "重庆" ? true : false);
                    T_TengXunShiFen.Visible = (GameMode == "腾十" ? true : false);
                    T_TengXunShiFenXin.Visible = (GameMode == "腾十信" ? true : false);
                    T_TengXunWuFen.Visible = (GameMode == "腾五" ? true : false);
                    T_TengXunWuFenXin.Visible = (GameMode == "腾五信" ? true : false);
                    T_WuFenCai.Visible = (GameMode == "五分" ? true : false);
                    T_XinJiangShiShiCai.Visible = (GameMode == "新疆" ? true : false);
                    T_HeNeiWuFen.Visible = (GameMode == "河五" ? true : false);
                    T_VRChongQingShiShiCai.Visible = (GameMode == "VR" ? true : false);


                    loadset.Thread_AoZhouCai = (GameMode == "澳彩" ? true : false);
                    loadset.Thread_ChongQingShiShiCai = (GameMode == "重庆" ? true : false);

                    loadset.Thread_HeNeiWuFen = (GameMode == "河五" ? true : false);
                    loadset.Thread_TengXunShiFen = (GameMode == "腾十" ? true : false);
                    loadset.Thread_TengXunShiFenXin = (GameMode == "腾十信" ? true : false);
                    loadset.Thread_TengXunWuFen = (GameMode == "腾五" ? true : false);
                    loadset.Thread_TengXunWuFenXin = (GameMode == "腾五信" ? true : false);

                    loadset.Thread_WuFen = (GameMode == "五分" ? true : false);
                    loadset.Thread_XinJiangShiShiCai = (GameMode == "新疆" ? true : false);
                    loadset.Thread_VRChongqing = (GameMode == "VR" ? true : false);








                    Util_Services.SaveServicesSetting(loadset);
                    RunnerF.SetMode(GameMode);
                }
            }
            //复制赔率和福利到本地

            RobootWeb.WebService usrws = new RobootWeb.WebService();
            if (db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.UserKey).Count() == 0)
            {




                Linq.Game_BasicRatio[] CopyRatio = (Linq.Game_BasicRatio[])JsonConvert.DeserializeObject(usrws.GetTemplateRatios(), typeof(Linq.Game_BasicRatio[]));

                if (CopyRatio.Count() != 0)
                {
                    foreach (var item in CopyRatio)
                    {
                        NetFramework.Console.WriteLine("正在复制默认赔率", true);
                        Linq.Game_BasicRatio newr = new Linq.Game_BasicRatio();
                        newr.aspnet_UserID = (GlobalParam.UserKey);
                        newr.BasicRatio = item.BasicRatio;
                        newr.BuyType = item.BuyType;
                        newr.BuyValue = item.BuyValue;
                        newr.GameType = item.GameType;
                        newr.IncludeMin = item.IncludeMin;
                        newr.MaxBuy = item.MaxBuy;
                        newr.MinBuy = item.MinBuy;
                        newr.BonusBuyValueCondition = item.BonusBuyValueCondition;
                        newr.WX_SourceType = item.WX_SourceType;
                        newr.Enable = item.Enable;


                        newr.OrderIndex = item.OrderIndex;
                        db.Game_BasicRatio.InsertOnSubmit(newr);

                    } 
                    db.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);
                }
            }

            if (db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.UserKey).Count() == 0)
            {

                Linq.WX_BounsConfig[] BounsConfig = (Linq.WX_BounsConfig[])JsonConvert.DeserializeObject(usrws.GetTemplateBonus(), typeof(Linq.WX_BounsConfig[]));

                if (BounsConfig.Count() != 0)
                {
                    foreach (var item in BounsConfig)
                    {
                        NetFramework.Console.WriteLine("正在复制默认反水", true);
                        Linq.WX_BounsConfig newr = new Linq.WX_BounsConfig();
                        newr.aspnet_UserID = (GlobalParam.UserKey);
                        newr.RowNumber = item.RowNumber;
                        newr.StartBuyPeriod = item.StartBuyPeriod;
                        newr.EndBuyPeriod = item.EndBuyPeriod;
                        newr.StartBuyAverage = item.StartBuyAverage;
                        newr.EndBuyAverage = item.EndBuyAverage;
                        newr.FixNumber = item.FixNumber;
                        newr.FlowPercent = item.FlowPercent;
                        newr.IfDivousPercent = item.IfDivousPercent;

                        db.WX_BounsConfig.InsertOnSubmit(newr);
                       
                    }
                    db.SubmitChanges();
                }
            }

        }


        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            //按快捷键
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 199:     //按下的是Shift+S

                            StopQQ = !StopQQ;
                            lbl_qqthread.Text = "(ALT+O)采集:" + (StopQQ == true ? "停止" : "运行");
                            //此处填写快捷键响应代码        
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        private void Start_Load(object sender, EventArgs e)
        {


            //注册热键Shift+S，Id号为100。HotKey.KeyModifiers.Shift也可以直接使用数字4来表示。

            HotKey.UnregisterHotKey(Handle, 199);

            bool regkey = HotKey.RegisterHotKey(Handle, 199, HotKey.KeyModifiers.Alt, Keys.O);
            if (regkey == false)
            {
                MessageBox.Show("注册热键失败,采集时间2秒");
            }
            else
            {
                SleepTime = 600;
            }




            Thread StartThread = new Thread(new ThreadStart(StartThreadDo));
            StartThread.Start();

            Thread StartThreadYixin = new Thread(new ThreadStart(StartThreadYixinDo));
            StartThreadYixin.Start();




            try
            {
                this.Text = "会员号:" + GlobalParam.UserName + "版本:" + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() + "数据源：" + GlobalParam.DataSourceName;
                RunnerF.Text = "会员号:" + GlobalParam.UserName + "版本:" + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() + "数据源：" + GlobalParam.DataSourceName;
            }
            catch (Exception)
            {

                this.Text = "会员号:" + GlobalParam.UserName + "版本:" + " 数据源：" + GlobalParam.DataSourceName;
                RunnerF.Text = "会员号:" + GlobalParam.UserName + "版本:" + " 数据源：" + GlobalParam.DataSourceName;
            }


            Thread EndNoticeBoss = new Thread(new ThreadStart(RepeatSendBossReport));
            EndNoticeBoss.Start();

            if (Util_Services.GetServicesSetting().SuperUser == true)
            {


                wb_ballgame = new EO.WinForm.WebControl();
                // wb_ballgame.ScriptErrorsSuppressed = true;
                wb_ballgame.WebView = new EO.WebBrowser.WebView();

                wb_ballgame.Dock = DockStyle.Fill;
                wb_ballgame.Name = "wb_football";

                gb_football.Controls.Add(wb_ballgame);














                wb_other = new EO.WinForm.WebControl();
                //wb_other.ScriptErrorsSuppressed = true;
                // wb_other.Dock = DockStyle.Fill;
                // wb_other.Name = "wb_other";
                wb_other.WebView = new EO.WebBrowser.WebView();
                gb_other.Controls.Add(wb_other);



                wb_refresh = new EO.WinForm.WebControl();
                wb_refresh.WebView = new EO.WebBrowser.WebView();
                //wb_refresh.ScriptErrorsSuppressed = true;
                wb_refresh.Dock = DockStyle.Fill;
                wb_refresh.Name = "wb_refresh";

                gb_refresh.Controls.Add(wb_refresh);






                wb_balllivepoint = new EO.WinForm.WebControl();
                wb_balllivepoint.WebView = new EO.WebBrowser.WebView();
                //wb_balllivepoint.ScriptErrorsSuppressed = true;
                wb_balllivepoint.Dock = DockStyle.Fill;
                wb_balllivepoint.Name = "wb_balllivepoint";

                gb_point.Controls.Add(wb_balllivepoint);




                wb_pointlog = new EO.WinForm.WebControl();
                wb_pointlog.WebView = new EO.WebBrowser.WebView();
                //wb_pointlog.ScriptErrorsSuppressed = true;
                wb_pointlog.Dock = DockStyle.Fill;
                wb_pointlog.Name = "wb_pointlog";

                gb_pointlog.Controls.Add(wb_pointlog);



            }
            wb_vrchongqing = new EO.WinForm.WebControl();
            wb_vrchongqing.WebView = new EO.WebBrowser.WebView();
            //wb_vrchongqing.ScriptErrorsSuppressed = true;
            wb_vrchongqing.Dock = DockStyle.Fill;
            wb_vrchongqing.Name = "wb_vrchongqing";

            gb_vrchongqingshishicai.Controls.Add(wb_vrchongqing);
            tm_refresh.Start();

        }





        private Int32 _tip = 1;

        public JObject InitResponse = null;
        public XmlDocument newridata = new XmlDocument();

        public string AsyncKey
        {
            get
            {
                string Result = "";
                if (synckeys == null)
                {
                    return "";
                }
                foreach (var keeyitem in synckeys["List"] as JArray)
                {
                    //if (keeyitem["Key"].ToString().StartsWith("11")==false)
                    {
                        Result += keeyitem["Key"] + "_" + keeyitem["Val"] + "|";
                    }

                }
                if (Result != "")
                {
                    Result = Result.Substring(0, Result.Length - 1);
                }
                return Result;

            }
        }

        string Uin = "";
        string Sid = "";
        string Skey = "";
        string _DeviceID = "";
        string DeviceID
        {
            get
            {
                if (_DeviceID == "")
                {

                    string ResultID = "e";
                    for (int i = 0; i < 4; i++)
                    {
                        ResultID += GlobalParam.UserKey.ToByteArray()[i].ToString("0000");
                    }
                    _DeviceID = ResultID;
                }
                return _DeviceID;

            }
        }
        string pass_ticket = "";


        string webhost = "";

        public JObject j_BaseRequest = null;
        JObject synckeys = null;



        public RunnerForm RunnerF = new RunnerForm();



        private string WX_MyInfo = "";
        public string MyUserName(string WX_SourceType)
        {
            if (WX_SourceType == "微")
            {
                return WX_MyInfo;
            }
            else if (WX_SourceType == "易")
            {
                return YiXin_MyInfo["1"].ToString();

            }
            else
            {
                return "不明来源" + WX_SourceType;
            }

        }

        private void StartThreadDo()
        {
            try
            {



                LoadBarCode();

                //使用get方法，查询地址：https://login.weixin.qq.com/cgi-bin/mmwebwx-bin/login?uuid=XXXXXX&tip=1&_=时间戳

                //这里的XXXXXX是我们刚才获取的uuid，时间戳同上。tip在第一次获取时应为1，这个数是每次查询要变的。

                //如果服务器返回：window.code=201，则说明此时用户在手机端已经完成扫描，但还没有点击确认，继续使用上面的地址查询，但tip要变成0；

                //如果服务器返回：

                //window.code=200
                //window.redirect_uri="XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
                this.Invoke(new Action(() => { lbl_msg.Text = "等待扫码"; }));
                KillThread.Add(WaitScanTHreadID, true);
                WaitScanTHreadID = Guid.NewGuid();
                Thread WaitScan = new Thread(new ParameterizedThreadStart(WaitScanThreadDo));

                WaitScan.Start(WaitScanTHreadID);
            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine(AnyError.Message, true);

                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
            }

        }
        Guid WaitScanTHreadID = Guid.NewGuid();
        Guid WaitScanTHreadYiXinID = Guid.NewGuid();
        private void StartThreadYixinDo()
        {
            try
            {
                cookieyixin = new CookieCollection();
                LoadYiXinBarCode();

                this.Invoke(new Action(() => { lbl_msg.Text = "等待易信扫码"; }));

                KillThread.Add(WaitScanTHreadYiXinID, true);
                WaitScanTHreadYiXinID = Guid.NewGuid();
                Thread WaitScan = new Thread(new ParameterizedThreadStart(WaitScanThreadYixinDo));
                WaitScan.Start(WaitScanTHreadYiXinID);

            }
            catch (Exception AnyError)
            {

                NetFramework.Console.WriteLine(AnyError.Message, true);

                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
            }


        }



        private void WaitScanThreadDo(Object ThreadID)
        {

            try
            {
                while (true)
                {

                    if (KillThread.ContainsKey((Guid)ThreadID))
                    {
                        return;
                    }

                    //使用get方法，查询地址：https://login.weixin.qq.com/cgi-bin/mmwebwx-bin/login?uuid=XXXXXX&tip=1&_=时间戳

                    //这里的XXXXXX是我们刚才获取的uuid，时间戳同上。tip在第一次获取时应为1，这个数是每次查询要变的。


                    //如果服务器返回：window.code=201，则说明此时用户在手机端已经完成扫描，但还没有点击确认，继续使用上面的地址查询，但tip要变成0；

                    //如果服务器返回：

                    //window.code=200
                    //window.redirect_uri="XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"

                    string CheckUrl = "https://login.weixin.qq.com/cgi-bin/mmwebwx-bin/login?uuid=" + _uuid + "&tip=" + _tip.ToString() + "&_=" + JavaTimeSpan();

                    string Result = NetFramework.Util_WEB.OpenUrl(CheckUrl
                   , "", "", "GET", cookie);

                    if (Result.Contains("window.code=201"))
                    {
                        _tip = 0;
                        this.Invoke(new Action(() => { lbl_msg.Text = "手机已扫码"; }));
                        this.Invoke(new Action(() => { PicBarCode.Visible = false; }));
                    }
                    else if (Result.Contains("window.code=200"))
                    {
                        ;
                        this.Invoke(new Action(() => { lbl_msg.Text = "手机已确认"; }));



                        // 用get方法，访问在上一步骤获得访问地址，并在参数后面加上：&fun=new，会返回一个xml格式的文本，类似这样：

                        //<error>
                        //    <ret>0</ret>
                        //    <message>OK</message>
                        //    <skey>xxx</skey>
                        //    <wxsid>xxx</wxsid>
                        //    <wxuin>xxx</wxuin>
                        //    <pass_ticket>xxx</pass_ticket>
                        //    <isgrayscale>1</isgrayscale>
                        //</error>

                        //把这里的wxuin，wxsid，skey，pass_ticket都记下来，这是重要数据。

                        this.Invoke(new Action(() => { lbl_msg.Text = "获取参数/Cookie"; }));



                        //                    5、微信初始化

                        //这个是很重要的一步，我在这个步骤折腾了很久。。。

                        //要使用POST方法，访问地址：https://"+webhost+"/cgi-bin/mmwebwx-bin/webwxinit?r=时间戳&lang=ch_ZN&pass_ticket=XXXXXX

                        //其中，时间戳不用解释，pass_ticket是我们在上面获取的一长串字符。

                        //POST的内容是个json串，{"BaseRequest":{"Uin":"XXXXXXXX","Sid":"XXXXXXXX","Skey":XXXXXXXXXXXXX","DeviceID":"e123456789012345"}}

                        //uin、sid、skey分别对应上面步骤4获取的字符串，DeviceID是e后面跟着一个15字节的随机数。

                        //程序里面要注意使用UTF8编码方式。
                        ReStartWeixin(true);

                        return;


                    }//200 code

                    if (_tip != 0)
                    {
                        this.Invoke(new Action(() => { lbl_msg.Text = "等待扫码"; }));
                        _tip += 1;
                    }
                    Thread.Sleep(300);

                }
            }
            catch (Exception AnyError)
            {
                MessageBox.Show(AnyError.Message + Environment.NewLine + AnyError.StackTrace);
            }




        }

        DateTime? RestartTime_WeiXin = null;
        Int32? RestartCount_WeiXin = 0;
        private void ReStartWeixin(Boolean SetDatabase = true)
        {
            NetFramework.Console.WriteLine("微信重启" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), false);
            if (RestartTime_WeiXin != null && (DateTime.Now - RestartTime_WeiXin.Value).TotalMinutes <= 2 && RestartCount_WeiXin > 3)
            {
                MessageBox.Show("微信频繁重启，可能已掉线");
                WeiXinOnLine = false;
                return;
            }

            RestartCount_WeiXin += 1;
            RestartTime_WeiXin = DateTime.Now;
            JObject Members = WXInit(SetDatabase);



            // 6、获取群成员列表

            //使用POST方法，访问：POST /cgi-bin/mmwebwx-bin/webwxbatchgetcontact?type=ex&r=1523261103230 

            //POST的内容为
            //                {
            //"BaseRequest": {
            //    "Uin": 2402981522,
            //    "Sid": "S144IRSNcchOBtSV",
            //    "Skey": "@crypt_bbd454c7_21f599999eb556f6ee3d4511e5c145a9",
            //    "DeviceID": "e850172353347767"
            //},
            //"Count": 13,
            //"List": [
            //    {
            //        "UserName": "@@7c353c65fdf44eab929e6d933b32352e40b8ef2c6b26b79f0b3d7cb2faf60513",
            //        "EncryChatRoomId": ""
            //    },

            // 成功则以JSON格式返回所有联系人的信息。格式类似：

            //////  JObject queryRoomMember = new JObject();
            //////  queryRoomMember.Add("BaseRequest", j_BaseRequest["BaseRequest"]);
            //////  Int32 Groupcount = 0;
            //////  JArray jaroom = new JArray();
            //////  foreach (var item in (Members["MemberList"]) as JArray)
            //////  {
            //////      string UserNametempID = (item["UserName"] as JValue).Value.ToString();
            //////      if (UserNametempID.StartsWith("@@"))
            //////      {
            //////          JObject newroom = new JObject();
            //////          newroom.Add("UserName", UserNametempID);
            //////          newroom.Add("EncryChatRoomId", "");
            //////          jaroom.Add(newroom);
            //////          Groupcount += 1;
            //////      }

            //////  }
            //////  queryRoomMember.Add("List", jaroom);
            //////  queryRoomMember.Add("Count", Groupcount);

            //////  string str_membroom = NetFramework.Util_WEB.OpenUrl("https://" + webhost + "/cgi-bin/mmwebwx-bin/webwxbatchgetcontact?type=ex&r=" + JavaTimeSpan()
            //////, "https://" + webhost + "/", queryRoomMember.ToString(), "POST", cookie, Encoding.UTF8);

            //////  JObject RoomMembers = JObject.Parse(str_membroom);







            //7、开启微信状态通知

            //用POST方法，访问：https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxstatusnotify

            //POST的内容是JSON串，格式：

            //{ 
            //     BaseRequest: { Uin: xxx, Sid: xxx, Skey: xxx, DeviceID: xxx }, 
            //     Code: 3, 
            //     FromUserName: 自己ID, 
            //     ToUserName: 自己ID, 
            //     ClientMsgId: 时间戳 
            //}

            // JObject State = new JObject();
            // State.Add("BaseRequest", j_BaseRequest["BaseRequest"]);
            // State.Add("Code", "3");
            // State.Add("FromUserName", MyUserName);
            // State.Add("ToUserName", MyUserName);
            // State.Add("ClientMsgId", JavaTimeSpan());

            // string str_state = NetFramework.Util_WEB.OpenUrl("https://" + webhost + "/cgi-bin/mmwebwx-bin/webwxstatusnotify"
            //, "https://" + webhost + "/", j_BaseRequest.ToString(), "POST", cookie);


            RestartTime_WeiXin = DateTime.Now;
            this.Invoke(new Action(() =>
            {
                WeiXinOnLine = true;
            }));
            KillThread.Add(Keepaliveid, true);
            Keepaliveid = Guid.NewGuid();

            Thread Keepalive = new Thread(new ParameterizedThreadStart(KeepAlieveDo));
            Keepalive.Start(Keepaliveid);
            RestartCount_WeiXin = 0;

        }




        string strfindurid = "";
        string strprefix = "";
        char[] Splits = Encoding.UTF8.GetChars(new byte[] { (byte)0xef, (byte)0xbf, (byte)0xbd });
        string MyUploadId = "";
        string MyUploadId2 = "";
        string MyUploadId3 = "";

        JObject qrresult_YiXin = null;

        private void WaitScanThreadYixinDo(Object ThreadID)
        {
            try
            {
                while (true)
                {
                    if (KillThread.ContainsKey((Guid)ThreadID))
                    {
                        return;
                    }

                    string Result = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im/check?qrcode=" + System.Web.HttpUtility.UrlEncode(yixinQrCodeData) + "&ts=" + JavaTimeSpan()
      , "https://web.yixin.im", "", "GET", cookieyixin, true, true);
                    if (Result == "")
                    {
                        Thread.Sleep(2000);
                        continue;

                    }
                    qrresult_YiXin = JObject.Parse(Result);




                    if (qrresult_YiXin["code"].Value<string>() == "202")
                    {

                        this.Invoke(new Action(() => { lbl_msg.Text = "等待易信确认"; }));
                    }
                    if (qrresult_YiXin["code"].Value<string>() == "200")
                    {
                        this.Invoke(new Action(() =>
                        {
                            lbl_msg.Text = "易信已确认";
                            PicBarCode_yixin.Visible = false;
                        }));

                        //扫码成功
                        RestartYiXin();
                        return;



                    }
                    Thread.Sleep(2000);
                }

            }
            catch (Exception AnyError)
            {

                NetFramework.Console.WriteLine(AnyError.Message, true);

                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
            }

        }

        DateTime? RestartTime_YiXin = null;
        Int32 RestartCount_YiXin = 0;
        private void RestartYiXin()
        {
            if (RestartTime_YiXin != null && (DateTime.Now - RestartTime_YiXin.Value).TotalMinutes < 1 && RestartCount_YiXin > 3)
            {
                MessageBox.Show("易信频繁重启，可能已掉线");
                YiXinOnline = false;
                return;
            }
            RestartCount_YiXin += 1;
            RestartTime_YiXin = DateTime.Now;
            string WSResult = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im:9092/socket.io/1/?t=" + JavaTimeSpan() + "&jsonp=0"
    , "https://web.yixin.im", "", "GET", cookieyixin, System.Text.Encoding.UTF8, true, true);


            //io.j[0]("3acbb645-ea2c-4fd9-bf55-9246184a7883:60:60:websocket,xhr-polling");
            Regex findurid = new Regex("\"((?!:)[\\S\\s])+:", RegexOptions.IgnoreCase);
            strfindurid = findurid.Match(WSResult).Value;
            strfindurid = strfindurid.Replace("\"", "");
            strfindurid = strfindurid.Replace(":", "");

            Regex prefix = new Regex(",((?!\")[\\S\\s])+\"", RegexOptions.IgnoreCase);
            strprefix = prefix.Match(WSResult).Value;
            strprefix = strprefix.Replace("\"", "");
            strprefix = strprefix.Replace(",", "");



            // /socket.io/1/xhr-polling/3acbb645-ea2c-4fd9-bf55-9246184a7883?t=1533087966302

            string HTMLRefresh = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im/"
, "https://web.yixin.im", "", "GET", cookieyixin, System.Text.Encoding.UTF8, true, true);







            string WSResultRepeat = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im:9092/socket.io/1/" + strprefix + "/" + strfindurid + "?t=" + JavaTimeSpan()
, "https://web.yixin.im", "", "GET", cookieyixin, System.Text.Encoding.UTF8, true, true);


            string[] Messages = WSResultRepeat.Split(Splits, StringSplitOptions.RemoveEmptyEntries);


            if (WSResultRepeat.StartsWith("1") == false)
            {
                this.Invoke(new Action(() => { lbl_msg.Text = "建立连接失败"; }));
                NetFramework.Console.WriteLine("建立连接失败", true);
                return;
            }


            string FirstSendBody = "3:::{\"SID\":90,\"CID\":34,\"Q\":[{\"t\":\"string\",\"v\""
                + " :\"" + System.Web.HttpUtility.UrlDecode(qrresult_YiXin["message"].Value<string>()) + "\"},{\"t\":\"property\",\"v\":{\"9\":\"80\",\"10\":\"100\",\"16\""
                + "  :\"" + (cookieyixin[" yxlkdeviceid"] == null ? "syl5faSRmgZ6bsMsFvo9" : cookieyixin[" yxlkdeviceid"].Value) + "\",\"24\":\"\"}},{\"t\":\"boolean\",\"v\":true}]}";


            WSResultRepeat = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im:9092/socket.io/1/" + strprefix + "/" + strfindurid + "?t=" + JavaTimeSpan()
          , "https://web.yixin.im", FirstSendBody, "POST", cookieyixin, System.Text.Encoding.UTF8, true, true);
            // WR_repeat = JObject.Parse(WSResultRepeat);




            WSResultRepeat = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im:9092/socket.io/1/" + strprefix + "/" + strfindurid + "?t=" + JavaTimeSpan()
           , "https://web.yixin.im", "", "GET", cookieyixin, System.Text.Encoding.UTF8, true, true);

            //myinfo?
            //                        3:::{
            //  "sid" : 90,
            //  "cid" : 34,
            //  "code" : 200,
            //  "r" : [ 168324356, "c0996c10-21fb-44a6-a642-45ffc3a60a82", "113.117.245.200", "f1321f0db626e643", 0, false ],
            //  "key" : 0,
            //  "ser" : 0
            //}

            Messages = WSResultRepeat.Split(Splits, StringSplitOptions.RemoveEmptyEntries);
            //3:::{"SID":96,"CID":4,"SER":1,"Q":[{"t":"long","v":"168367856"},{"t":"byte","v":1}]}
            //3:::{"SID":96,"CID":1,"SER":2,"Q":[{"t":"property","v":{"1":"168367856","2":"168324356","3":"FULL LOAD TEST","4":"1533087979.705","5":"0","6":"7527c77d8e7b78c9c6ab4f371de29696"}}]}

            if (Messages.Length == 1)
            {
                YiXinMessageProcess(Messages[0]);
            }//收到一个消息
            else
            {

                foreach (var item in Messages)
                {
                    try
                    {
                        Convert.ToDouble(item);
                    }
                    catch (Exception)
                    {
                        YiXinMessageProcess(item);
                    }//偶数的
                    ;
                }

            }//收到多消息


            string SecondBody = "3:::{\"SID\":93,\"CID\":1,\"Q\":[{\"t\":\"ByteIntMap\",\"v\":{\"1\":\"0\",\"2\":\"0\",\"3\":\"0\",\"5\":\"0\",\"10\":\"0\"}},{\"t\":\"LongIntMap\",\"v\":{}}]}";
            WSResultRepeat = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im:9092/socket.io/1/" + strprefix + "/" + strfindurid + "?t=" + JavaTimeSpan()
        , "https://web.yixin.im", SecondBody, "POST", cookieyixin, System.Text.Encoding.UTF8, true, true);
            // WR_repeat = JObject.Parse(WSResultRepeat);

            KillThread.Add(KeepaliveYiXInid, true);
            KeepaliveYiXInid = Guid.NewGuid();
            Thread keepaliveyexindo = new Thread(new ParameterizedThreadStart(KeepAlieveYixinDo));
            keepaliveyexindo.Start(KeepaliveYiXInid);
        }







        Guid Keepaliveid = Guid.NewGuid();
        Guid KeepaliveYiXInid = Guid.NewGuid();
        private void KeepAlieveDo(object ThreadID)
        {
            if (KillThread.ContainsKey((Guid)ThreadID))
            {
                return;
            }
            while (true)
            {
                if (KillThread.ContainsKey((Guid)ThreadID))
                {
                    return;
                }
                try
                {


                    #region "微信监听"

                MethodRestart:


                    this.Invoke(new Action(() => { lbl_msg.Text = "机器人监听中"; }));


                    //使用get方法，设置超时为60秒，访问：https://webpush."+webhost+"/cgi-bin/mmwebwx-bin/synccheck?sid=XXXXXX&uin=XXXXXX&synckey=XXXXXX&r=时间戳&skey=XXXXXX&deviceid=XXXXXX&_=时间戳

                    //其他几个参数不用解释，这里的synckey需要说一下，前面的步骤获取的json串中有多个key信息，需要把这些信息拼起来，key_val，中间用|分割，类似这样：

                    //1_652651920|2_652651939|3_652651904|1000_0

                    //服务器返回：window.synccheck={retcode:”0”,selector:”0”}

                    //retcode为0表示成功，selector为2和6表示有新信息。4表示公众号新信息。
                    string CheckUrl3 = "https://webpush." + webhost + "/cgi-bin/mmwebwx-bin/synccheck?sid=" + System.Web.HttpUtility.UrlEncode(Sid) + "&uin=" + System.Web.HttpUtility.UrlEncode(Uin) + "&synckey=" + System.Web.HttpUtility.UrlEncode(AsyncKey) + "&r=" + JavaTimeSpan() + "&skey=" + System.Web.HttpUtility.UrlEncode(Skey) + "&deviceid=" + System.Web.HttpUtility.UrlEncode(DeviceID) + "&_=" + JavaTimeSpan();
                    string Result3 = NetFramework.Util_WEB.OpenUrl(CheckUrl3
                      , "https://" + webhost + "/", "", "GET", cookie, false);
                    Result3 = Result3.Substring(Result3.IndexOf("=") + 1);
                    if (Result3 == "")
                    {
                        continue;
                    }
                    JObject Check = JObject.Parse(Result3);

                    if (Result3.Contains("selector:0") == false && Result3.Contains("selector:2") == false)
                    {
                        NetFramework.Console.WriteLine(GlobalParam.UserName + DateTime.Now.ToString(), false);
                        NetFramework.Console.WriteLine(GlobalParam.UserName + Result3, true);

                    }

                    if (Check["retcode"].ToString() == "1101")
                    {

                        //string CheckUrl4 = "https://" + webhost + "/cgi-bin/mmwebwx-bin/webwxsync?sid=" + Sid + "&skey=" + Skey;
                        //JObject body4 = new JObject();
                        //body4.Add("BaseRequest", j_BaseRequest["BaseRequest"]);
                        //body4.Add("SyncKey", synckeys);
                        //body4.Add("rr", JavaTimeSpan());
                        //string Result4 = NetFramework.Util_WEB.OpenUrl(CheckUrl4
                        //  , "https://" + webhost + "/", body4.ToString().Replace(Environment.NewLine, ""), "POST", cookie, Encoding.UTF8, false);

                        //JObject Newmsg = JObject.Parse(Result4);


                        //string AddMsgCount = Newmsg["AddMsgCount"].ToString();
                        //synckeys = (Newmsg["SyncKey"] as JObject);

                        NetFramework.Console.WriteLine(cookie.Count == 0 ? "" : cookie[0].Expires.ToString(), true);

                        //           string Result2 = NetFramework.Util_WEB.OpenUrl(RedirtURI
                        //, "", "", "GET", cookie, false);

                        //           newridata.LoadXml(Result2);
                        //goto MethodRestart;
                        ReStartWeixin(true);
                        return;

                    }
                    if (Check["retcode"].ToString() == "1100")
                    {
                        Thread.Sleep(50);
                        //            //ReStartWeixin();
                        //            string Result2 = NetFramework.Util_WEB.OpenUrl(RedirtURI
                        //, "", "", "GET", cookie, false);

                        //            newridata.LoadXml(Result2);
                        //goto MethodRestart;
                        ReStartWeixin(true);
                        return;
                    }
                    else if (
                     ((Result3.Contains("selector:\"7\"")) == true)
                         )
                    {
                        //goto MethodRestart;
                        //              string Result2 = NetFramework.Util_WEB.OpenUrl(RedirtURI
                        //, "", "", "GET", cookie, false);

                        //              newridata.LoadXml(Result2);
                        //ReStartWeixin();
                        //return;
                        ReStartWeixin(true);
                        return;

                    }

                    ////////////////////////////////////////////////

                    if
                        (
                        ((Result3.Contains("selector:\"0\"")) == false)
                        || (Check["retcode"].ToString() == "1101")
                        || (Check["retcode"].ToString() == "1100"))
                    {

                        this.Invoke(new Action(() => { lbl_msg.Text = Result3; }));


                        // 9、读取新信息

                        //检测到有信息以后，用POST方法，访问：https://"+webhost+"/cgi-bin/mmwebwx-bin/webwxsync?sid=XXXXXX&skey=XXXXXX

                        //POST的内容：

                        //{"BaseRequest" : {"DeviceID":"XXXXXX,"Sid":"XXXXXX", "Skey":"XXXXXX", "Uin":"XXXXXX"},"SyncKey" : {"Count":4,"List":[{"Key":1,"Val":652653204},{"Key":2,"Val":652653674},{"Key":3,"Val":652653544},{"Key":1000,"Val":0}]},"rr" :时间戳}

                        //注意这里的SyncKey格式，参考前面的说明。
                        string CheckUrl4 = "https://" + webhost + "/cgi-bin/mmwebwx-bin/webwxsync?sid=" + Sid + "&skey=" + Skey;
                        JObject body4 = new JObject();
                        body4.Add("BaseRequest", j_BaseRequest["BaseRequest"]);
                        body4.Add("SyncKey", synckeys);
                        body4.Add("rr", JavaTimeSpan());
                        string Result4 = NetFramework.Util_WEB.OpenUrl(CheckUrl4
                          , "https://" + webhost + "/", body4.ToString().Replace(Environment.NewLine, ""), "POST", cookie, Encoding.UTF8, false);

                        JObject Newmsg = JObject.Parse(Result4);


                        string AddMsgCount = Newmsg["AddMsgCount"].ToString();
                        synckeys = (Newmsg["SyncCheckKey"] as JObject);

                        if (AddMsgCount != "0")
                        {

                            foreach (var AddMsgList in (Newmsg["AddMsgList"] as JArray))
                            {
                                string FromUserNameTEMPID = AddMsgList["FromUserName"].ToString();
                                string ToUserNameTEMPID = AddMsgList["ToUserName"].ToString();

                                string Content = AddMsgList["Content"].ToString();
                                string msgTime = AddMsgList["CreateTime"].ToString();
                                string msgType = AddMsgList["MsgType"].ToString();
                                //Thread MessageThread = new Thread(new ParameterizedThreadStart(StartMessageThread));
                                //MessageThread.ApartmentState = ApartmentState.STA;
                                //MessageThread.Start(new object[] { "微", FromUserNameTEMPID, ToUserNameTEMPID, Content, msgTime, msgType, (FromUserNameTEMPID.StartsWith("@@")) });
                                DateTime EachStart = DateTime.Now;
                                MessageRobotDo("微", FromUserNameTEMPID, ToUserNameTEMPID, Content, msgTime, msgType, (FromUserNameTEMPID.StartsWith("@@")));
                                NetFramework.Console.WriteLine("收到消息,处理耗时:" + (DateTime.Now - EachStart).TotalSeconds.ToString(), true);

                                //MessageRobotDo("微", db, FromUserNameTEMPID, ToUserNameTEMPID, Content, msgTime, msgType, (FromUserNameTEMPID.StartsWith("@@")));

                            }//JSON消息循环
                        }//新消息数目不为0


                    }//有新消息


                    //                10、发送信息

                    //这个比较简单，用POST方法，访问：https://"+webhost+"/cgi-bin/mmwebwx-bin/webwxsendmsg

                    //POST的还是json格式，类似这样：

                    //{"Msg":{"Type":1,"Content":"测试信息","FromUserName":"XXXXXX","ToUserName":"XXXXXX","LocalID":"时间戳","ClientMsgId":"时间戳"},"BaseRequest":{"Uin":"XXXXXX","Sid":"XXXXXX","Skey":"XXXXXX","DeviceID":"XXXXXX"}}

                    //这里的Content是信息内容，LocalID和ClientMsgId都用当前时间戳。
                    #endregion

                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine(AnyError.Message, true);
                    NetFramework.Console.WriteLine(AnyError.StackTrace, true);

                }
                Thread.Sleep(500);

            }//不停循环

        }//KeepAliveDo






        private void KeepAlieveYixinDo(object ThreadID)
        {

            try
            {
                while (true)
                {
                    if (KillThread.ContainsKey((Guid)ThreadID))
                    {
                        return;
                    }
                    string WSResultRepeat = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im:9092/socket.io/1/" + strprefix + "/" + strfindurid + "?t=" + JavaTimeSpan()
                                  , "https://web.yixin.im", "", "GET", cookieyixin, System.Text.Encoding.UTF8, true, true);
                    // WR_repeat = JObject.Parse(WSResultRepeat);
                    string[] Messages = WSResultRepeat.Split(Splits, StringSplitOptions.RemoveEmptyEntries);
                    //3:::{"SID":96,"CID":4,"SER":1,"Q":[{"t":"long","v":"168367856"},{"t":"byte","v":1}]}
                    //3:::{"SID":96,"CID":1,"SER":2,"Q":[{"t":"property","v":{"1":"168367856","2":"168324356","3":"FULL LOAD TEST","4":"1533087979.705","5":"0","6":"7527c77d8e7b78c9c6ab4f371de29696"}}]}
                    if (WSResultRepeat.EndsWith(":::1+0"))
                    {
                        RestartYiXin();
                        return;
                    }
                    if (Messages.Length == 1)
                    {
                        YiXinMessageProcess(Messages[0]);
                    }//收到一个消息
                    else
                    {

                        foreach (var item in Messages)
                        {
                            try
                            {
                                Convert.ToDouble(item);
                            }
                            catch (Exception)
                            {
                                YiXinMessageProcess(item);
                            }//偶数的
                            ;
                        }

                    }//收到多消息
                }// while结束

            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine("易信消息处理错误" + AnyError.Message, true);
                NetFramework.Console.WriteLine("易信消息处理错误" + AnyError.StackTrace, true);
            }



        }//函数结束

        Int32 NextSer = 0;
        private void YiXinMessageProcess(string Rawitem)
        {
            if (Rawitem.Length <= 4)
            {
                return;
            }
            string item = Rawitem.Substring(4);
            try
            {


                JObject eachMessage = JObject.Parse(item);

                if (eachMessage["sid"].Value<string>() == "91")
                {
                    //我的好友简单列表
                    if (eachMessage["cid"].Value<string>() == "106")
                    {


                        //                    {
                        //    "cid": 106,
                        //    "code": 200,
                        //    "ser": 0,
                        //    "sid": 91,
                        //    "key": 0,
                        //    "r": [
                        //        [
                        //            {
                        //                "2": "168367856",
                        //"3" : "琦琦 备注",
                        //                "4": "1",
                        //                "6": "1532659457",
                        //                "7": "18",
                        //                "8": "0",
                        //                "9": "1532659457"
                        //            },
                        //            {
                        //                "2": "168625138",
                        //                "4": "1",
                        //                "6": "1533102447",
                        //                "7": "27",
                        //                "8": "0",
                        //                "9": "1533102447"
                        //            },
                        //            {
                        //                "2": "168803760",
                        //                "4": "1",
                        //                "6": "1533115147",
                        //                "7": "31",
                        //                "8": "0",
                        //                "9": "1533115147"
                        //            }
                        //        ],
                        //        1533115200
                        //    ]
                        //}

                        JArray r = (eachMessage["r"] as JArray);
                        JArray contacts = r[0] as JArray;
                        foreach (JObject contactitem in contacts)
                        {
                            if (contactitem["2"] == null)
                            {
                                continue;
                            }
                            YixinContact newc = new YixinContact();
                            newc.ContactType = "个人";
                            newc.ContactID = contactitem["2"].Value<string>();
                            newc.ContactRemarkName = (contactitem["3"] == null ? "" : contactitem["3"].Value<string>());
                            YixinContactlist.Add(newc);

                        }
                        RunnerF.SetYixinMembers(YixinContactlist, YixinContactInfolist);
                    }
                    //联系人具体信息
                    else if (eachMessage["cid"].Value<string>() == "102")
                    {
                        //                             {
                        //  "1" : "168367856",
                        //  "16" : "{\"hbmedal10ValidTime\":\"1533094133\",\"medalUpdateTime\":\"1533098499\"}",
                        //  "2" : "l13828081978",
                        //  "5" : "13828081978",
                        //  "6" : "琦琦",
                        //  "7" : "琦琦娱乐",
                        //  "8" : "http://nos.netease.com/yixinpublic/pr_jsjqja5ahwadbq0o8zfr3g==_1532616291_307707129",
                        //  "9" : "1",
                        //  "11" : "广东 江门",
                        //  "14" : "1533098499",
                        //  "15" : "1"
                        //}, 
                        JArray r = (eachMessage["r"] as JArray);
                        JArray contacts = r[0] as JArray;
                        foreach (JObject contactitem in contacts)
                        {
                            if (contactitem["1"] == null)
                            {
                                continue;
                            }
                            YixinContactInfo newi = new YixinContactInfo();
                            newi.ContactID = contactitem["1"].Value<string>();
                            newi.ContactName = (contactitem["6"] == null ? "" : contactitem["6"].Value<string>());
                            newi.ContactPhone = (contactitem["5"] == null ? "" : contactitem["5"].Value<string>());
                            newi.ContactSignName = (contactitem["7"] == null ? "" : contactitem["7"].Value<string>());
                            YixinContactInfolist.Add(newi);

                        }

                        RunnerF.SetYixinMembers(YixinContactlist, YixinContactInfolist);
                        this.Invoke(new Action(() =>
                        {
                            YiXinOnline = true;
                        }));
                    }
                    else if (eachMessage["cid"].Value<string>() == "101")
                    {
                        JArray r = (eachMessage["r"] as JArray);

                        JObject contactitem = r[0] as JObject;


                        YiXin_MyInfo = contactitem as JObject;


                        //:::{
                        //"key" : 0,
                        //"ser" : 0,
                        //"sid" : 91,
                        //"cid" : 101,
                        //"code" : 200,
                        //"r" : [ {
                        //  "1" : "168324356",
                        //  "16" : "{\"generation\":\"G80\"}",
                        //  "2" : "weisimin",
                        //  "5" : "18007603071",
                        //  "6" : "weisimin",
                        //  "9" : "1",
                        //  "13" : "1664",
                        //  "14" : "1532754908",
                        //  "15" : "1"
                        //}, 1533087966 ]
                        //}


                    }
                }//联系人消息类
                //我在的群简单列表
                //-------------------------------------------------------------------------------------------------------------------
                else if (eachMessage["sid"].Value<string>() == "94")
                {
                    if (eachMessage["cid"].Value<string>() == "104")
                    {
                        //                        {
                        //  "1" : "41900237",
                        //  "2" : "群名1",
                        //  "3" : "168324356",
                        //  "4" : "2018-08-01 09:59:54.0",
                        //  "5" : "1",
                        //  "6" : "1533088811",
                        //  "7" : "1",
                        //  "8" : "1",
                        //  "10" : "0",
                        //  "11" : "0",
                        //  "12" : "",
                        //  "14" : "0",
                        //  "15" : ""
                        //}, 


                        JArray r = (eachMessage["r"] as JArray);
                        JArray contacts = r[0] as JArray;
                        foreach (JObject contactitem in contacts)
                        {
                            YixinContact newc = new YixinContact();
                            newc.ContactType = "群";
                            newc.ContactID = contactitem["1"].Value<string>();
                            newc.ContactRemarkName = (contactitem["2"] == null ? "" : contactitem["2"].Value<string>());
                            YixinContactlist.Add(newc);

                        }
                        RunnerF.SetYixinMembers(YixinContactlist, YixinContactInfolist);

                    }
                    else if (eachMessage["cid"].Value<string>() == "111")
                    {
                        //群里面的成员
                        //                    :::{
                        //  "key" : 0,
                        //  "ser" : 0,
                        //  "sid" : 94,
                        //  "cid" : 111,
                        //  "code" : 200,
                        //  "r" : [ 41900236, [ {
                        //    "1" : "41900236",
                        //    "2" : "168367856",
                        //    "3" : "2",
                        //    "4" : "0",
                        //    "5" : "0",
                        //    "6" : "168324356",
                        //    "7" : "1533088766",
                        //    "8" : "1533088766",
                        //    "9" : "1",
                        //    "10" : "0",
                        //    "11" : "0",
                        //    "12" : "",
                        //    "13" : "",
                        //    "14" : "0"
                        //  }, {
                        //    "1" : "41900236",
                        //    "2" : "168324356",
                        //    "3" : "0",
                        //    "4" : "0",
                        //    "5" : "0",
                        //    "6" : "0",
                        //    "7" : "1533088766",
                        //    "8" : "1533088766",
                        //    "9" : "1",
                        //    "10" : "1",
                        //    "11" : "0",
                        //    "12" : "",
                        //    "13" : "",
                        //    "14" : "0"
                        //  } ], 1533174570 ]
                        //}

                    }

                }//群消息类
                //-------------------------------------------------------------------------------------------------------------------

                else if (eachMessage["sid"].Value<string>() == "94")
                {
                    //            3:::{
                    //  "cid" : 1,
                    //  "code" : 200,
                    //  "ser" : 0,
                    //  "sid" : 92,
                    //  "key" : 168367856,
                    //  "r" : [ 24347991369, {
                    //    "body" : [ {
                    //      "1" : "168324356",
                    //      "2" : "168367856",
                    //      "3" : "好的",
                    //      "4" : "1533115817",
                    //      "5" : "0",
                    //      "6" : "1b1a36856b1a4ee8bc7d055bfb4dada5",
                    //      "20002" : "49811",
                    //      "20001" : "202.68.200.157"
                    //    } ],
                    //    "headerPacket" : {
                    //      "sid" : 96,
                    //      "cid" : 1,
                    //      "key" : 168367856
                    //    }
                    //  } ]
                    //}

                }
                //收到别人发来的消息
                //-------------------------------------------------------------------------------------------------------------------

                else if (eachMessage["sid"].Value<string>() == "92")
                {
                    //            3:::{
                    //  "sid" : 92,
                    //  "cid" : 1,
                    //  "code" : 200,
                    //  "r" : [ 0, {
                    //    "body" : [ 53455438, {
                    //      "1" : "53455438", ToUserID
                    //      "2" : "168324356", FromUserID
                    //      "101" : "53455438",
                    //      "3" : "22488888", Contact
                    //      "4" : "1532944804",
                    //      "5" : "0",
                    //      "80" : "0",
                    //      "6" : "670ab1ae7cda46febfb3c7452e8c351b",
                    //      "7" : "3587333718802439",
                    //      "20002" : "12179",
                    //      "9" : "1532754908",
                    //      "20001" : "113.117.248.55"
                    //    } ],
                    //    "headerPacket" : {
                    //      "sid" : 94,
                    //      "cid" : -10,
                    //      "key" : 168324356
                    //    }
                    //  } ],
                    //  "key" : 168324356,
                    //  "ser" : 0
                    //}
                    if (eachMessage["cid"].Value<string>() == "1")
                    {
                        Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                        //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                        ////db.ObjectTrackingEnabled = false;
                        JObject AddMsgList = eachMessage["r"][1]["body"][0] as JObject;
                        if (AddMsgList == null)
                        {
                            AddMsgList = eachMessage["r"][1]["body"][1] as JObject;

                        }
                        if (AddMsgList == null)
                        {
                            return;

                        }
                        string FromUserNameTEMPID = AddMsgList["2"].ToString();
                        string ToUserNameTEMPID = AddMsgList["1"].ToString();
                        if (AddMsgList["3"] == null)
                        {
                            return;
                        }
                        string Content = AddMsgList["3"].ToString();
                        string msgTime = AddMsgList["4"].ToString();
                        string msgType = "未定义";

                        //Thread MessageThread = new Thread(new ParameterizedThreadStart(StartMessageThread));
                        //MessageThread.Start(new object[] { "易", FromUserNameTEMPID, ToUserNameTEMPID, Content, msgTime, msgType, (FromUserNameTEMPID.StartsWith("@@")) });
                        MessageRobotDo("易", FromUserNameTEMPID, ToUserNameTEMPID, Content, msgTime, msgType, (FromUserNameTEMPID.StartsWith("@@")));

                    }

                    //像是KEEPALIVE的更新包
                    else if (eachMessage["cid"].Value<string>() == "50")
                    {
                        if (eachMessage["ser"].Value<string>() != "0")
                        {
                            NextSer = Convert.ToInt32(eachMessage["ser"].Value<string>());

                        }
                        // string FirstSendBody = "3:::{\"SID\":96,\"CID\":4,\"SER\":" + YixinSer.ToString() + ",\"Q\":[{\"t\":\"long\",\"v\":\"" + MyInfo["1"].ToString() + "\"},{\"t\":\"byte\",\"v\":1}]}";
                        // YixinSer += 1;
                        // string WSResultRepeat = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im:9092/socket.io/1/" + strprefix + "/" + strfindurid + "?t=" + JavaTimeSpan()
                        //    , "https://web.yixin.im", FirstSendBody, "POST", cookieyixin, System.Text.Encoding.UTF8, true, true);
                        // // WR_repeat = JObject.Parse(WSResultRepeat);



                    }
                    //收到发来的消息
                }
                //-------------------------------------------------------------------------------------------------------
                else if (eachMessage["sid"].Value<string>() == "90")
                {
                    if (eachMessage["cid"].Value<string>() == "34")
                    {
                        MyUploadId = eachMessage["r"][1].ToString();
                    }
                    else if (eachMessage["cid"].Value<string>() == "-3")
                    {
                        MyUploadId2 = eachMessage["r"][1][0]["16"].ToString();
                        MyUploadId2 = MyUploadId2.Substring(0, 8) + "-" + MyUploadId2.Substring(8, 4) + "-" + MyUploadId2.Substring(12, 4) + "-" + MyUploadId2.Substring(16);
                        MyUploadId3 = eachMessage["r"][1][0]["13"].ToString();
                    }
                }
            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine("消息无法处理:" + Rawitem, true);
                NetFramework.Console.WriteLine(AnyError.Message, true);
                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
            }

        }

        private bool RobotStop = false;


        //private void StartMessageThread(object param)
        //{
        //    object[] Params = (object[])param;
        //    string SourceType = (string)Params[0];

        //    string FromUserNameTEMPID = (string)Params[1];
        //    string ToUserNameTEMPID = (string)Params[2];
        //    string Content = (string)Params[3];
        //    string msgTime = (string)Params[4];
        //    string msgType = (string)Params[5];
        //    bool IsTalkGroup = (bool)Params[6];

        //    MessageRobotDo(SourceType,

        //     FromUserNameTEMPID,
        //     ToUserNameTEMPID,
        //     Content,
        //     msgTime,
        //     msgType,
        //     IsTalkGroup);
        //}



        private void MessageRobotDo(

            string SourceType,
            string FromUserNameTEMPID,
            string ToUserNameTEMPID,
            string RawContent,
            string msgTime,
            string msgType,
            bool IsTalkGroup)
        {
            try
            {

                string Content = RawContent;
                Regex groupwhosay = new Regex("@((?!<br/>)[\\s\\S])+<br/>", RegexOptions.IgnoreCase);

                string str_groupwhosay = groupwhosay.Match(Content).Value;

                Content = Regex.Replace(Content, "@((?!<br/>)[\\s\\S])+<br/>", "", RegexOptions.IgnoreCase);

                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;




                #region 消息处理
                Linq.aspnet_UsersNewGameResultSend mysetting = Util_Services.GetServicesSetting();

                //string FromUserNameTEMPID = AddMsgList["FromUserName"].ToString();
                //string ToUserNameTEMPID = AddMsgList["ToUserName"].ToString();

                //string Content = AddMsgList["Content"].ToString();
                //string msgTime = AddMsgList["CreateTime"].ToString();
                //string msgType = AddMsgList["MsgType"].ToString();

                #region "转发"
                if (Content.Contains("上分") || Content.Contains("下分") || (msgType == "10000"))
                {

                    #region 转发设置

                    DataRow[] FromContacts = RunnerF.MemberSource.Select("User_ContactTEMPID='" + FromUserNameTEMPID
                        + "' and User_IsReceiveTransfer=true");
                    if (FromContacts.Length != 0)
                    {
                        string FromContactName = FromContacts[0].Field<string>("User_Contact");


                        var ReceiveTrans = db.WX_UserReply.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                            && t.IsAdmin == true);

                        foreach (var recitem in ReceiveTrans)
                        {
                            DataRow[] ToContact = RunnerF.MemberSource.Select("User_ContactID='" + recitem.WX_UserName.Replace("'", "''") + "' and User_SourceType='" + recitem.WX_SourceType + "'");
                            if (ToContact.Length != 0)
                            {
                                SendRobotContent(FromContactName + ":" + NetFramework.Util_WEB.CleanHtml(Content)
                                    , ToContact[0].Field<string>("User_ContactTEMPID")
                                     , ToContact[0].Field<string>("User_SourceType")
                                    );
                            }



                        }

                    }

                    #endregion
                }
                #endregion





                if (Content != "")
                {
                    var Tocontacts = RunnerF.MemberSource.Select("User_ContactTEMPID='" + (FromUserNameTEMPID == MyUserName(SourceType) ? ToUserNameTEMPID : FromUserNameTEMPID) + "'");
                    if (Tocontacts.Count() == 0)
                    {
                        NetFramework.Console.WriteLine("找不到联系人，消息无法处理" + (FromUserNameTEMPID == MyUserName(SourceType) ? ToUserNameTEMPID : FromUserNameTEMPID), true);
                        return;
                    }
                    DataRow userr = Tocontacts.First();
                    Linq.WX_UserReply checkreply = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == userr.Field<string>("User_ContactID") && t.WX_SourceType == userr.Field<string>("User_SourceType"));


                    if (RobotStop == false)
                    {



                        //自己发的，对方发的，自己在群发的，对方在群发的




                        DataRow[] FindGroupIsMember = RunnerF.MemberSource.Select("User_ContactTEMPID='" + str_groupwhosay.Replace(":<br/>", "") + "' and User_IsAdmin='True'");



                        #region "如果是自己发出的或会员发出的"



                        if (FromUserNameTEMPID == MyUserName(SourceType)
                            || FindGroupIsMember.Count() > 0
                            )
                        {


                            if (Content == "加")
                            {
                                if (SourceType == "微")
                                {
                                    SendRobotContent("开始刷新联系人", Tocontacts
                                       , SourceType
                                       );
                                    RepeatGetMembers(Skey, pass_ticket, true);
                                }
                                else if (SourceType == "易")
                                {
                                    RepeatGetMembersYiXin();
                                }

                            }



                            string MyOutResult = "";
                            try
                            {
                                //执行会员命令
                                MyOutResult = Linq.ProgramLogic.WX_UserReplyLog_MySendCreate(Content, Tocontacts[0], JavaSecondTime(Convert.ToInt64(msgTime)));
                                string[] Splits = Content.Replace("，", ",").Replace("，", ",")
                                           .Replace(".", ",").Replace("。", ",").Replace("。", ",")
                                           .Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                if (Splits.Count() != 1 && Linq.ProgramLogic.ShiShiCaiIsOrderContent(Content))
                                {
                                    DateTime Times = JavaSecondTime(Convert.ToInt64(msgTime));
                                    foreach (var Splititem in Splits)
                                    {
                                        Times.AddMilliseconds(10);
                                        String TmpMessage = Linq.ProgramLogic.WX_UserReplyLog_MySendCreate(Splititem, Tocontacts[0], Times);

                                        if (TmpMessage != "")
                                        {
                                            MyOutResult = TmpMessage;
                                        }
                                    }


                                }


                                //执行模拟下单,模拟下单内部切分
                                if (Tocontacts[0].Field<Boolean?>("User_IsReply") == true)
                                {

                                    String TmpMessage = NewWXContent(JavaSecondTime(Convert.ToInt64(msgTime)), Content, Tocontacts[0], "人工", true);
                                    if (TmpMessage != "")
                                    {
                                        MyOutResult = TmpMessage;
                                    }

                                }
                                //全部执行玩才输出
                                if (MyOutResult != "")
                                {
                                    string WX_UserName = Tocontacts[0].Field<string>("User_ContactID");
                                    var NoticeList = RunnerF.MemberSource.Select("User_ContactID='" + WX_UserName + "'");

                                    var NoticeContacts =
                                    SendRobotContent(MyOutResult, NoticeList
                                        , SourceType
                                        );

                                }





                            }
                            catch (Exception mysenderror)
                            {

                                NetFramework.Console.WriteLine(mysenderror.Message, true);
                                NetFramework.Console.WriteLine(mysenderror.StackTrace, true);
                            }





                            #region "对"
                            if (Content.Contains("对") || Content.ToUpper().Contains("VS"))
                            {

                                try
                                {

                                    Linq.ProgramLogic.FormatResultState State = Linq.ProgramLogic.FormatResultState.Initialize;
                                    Linq.ProgramLogic.FormatResultType StateType = Linq.ProgramLogic.FormatResultType.Initialize;
                                    string BuyType = "";
                                    string BuyMoney = "";
                                    string[] q_Teams = new string[] { };
                                    Linq.Game_FootBall_VS[] AllTeams = (Linq.Game_FootBall_VS[])Linq.ProgramLogic.ReceiveContentFormat(Content, out State, out StateType, Linq.ProgramLogic.FormatResultDirection.MemoryMatchList, out BuyType, out BuyMoney, out q_Teams);
                                    foreach (var matchitem in AllTeams)
                                    {

                                        if (StateType == Linq.ProgramLogic.FormatResultType.QueryImage)
                                        {
                                            if (File.Exists(Application.StartupPath + "\\output\\" + matchitem.GameKey + ".jpg"))
                                            {
                                                SendRobotImage(Application.StartupPath + "\\output\\" + matchitem.GameKey + ".jpg", (FromUserNameTEMPID == MyUserName(SourceType) ? ToUserNameTEMPID : FromUserNameTEMPID), SourceType);
                                            }
                                        }
                                        else if (StateType == Linq.ProgramLogic.FormatResultType.QueryTxt)
                                        {
                                            if (File.Exists(Application.StartupPath + "\\output\\" + matchitem.GameKey + ".txt"))
                                            {
                                                SendRobotTxtFile(Application.StartupPath + "\\output\\" + matchitem.GameKey + ".txt", (FromUserNameTEMPID == MyUserName(SourceType) ? ToUserNameTEMPID : FromUserNameTEMPID), SourceType);
                                            }
                                        }


                                    }
                                    if (StateType == Linq.ProgramLogic.FormatResultType.QueryResult)
                                    {
                                        var LastPoints = db.Game_ResultFootBall_Last.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                                             &&
                                             (
                                             (t.A_Team.Contains(q_Teams[0]) && t.B_Team.Contains(q_Teams[1]))
                                             || (t.A_Team.Contains(q_Teams[1]) && t.B_Team.Contains(q_Teams[0]))
                                             )
                                              && t.LiveBallLastSendTime >= DateTime.Now.AddDays(-2)
                                             );

                                        foreach (var points in LastPoints)
                                        {
                                            SendRobotContent(points.A_Team + "VS" + points.B_Team + ",现时比分" + points.LastPoint, (FromUserNameTEMPID == MyUserName(SourceType) ? ToUserNameTEMPID : FromUserNameTEMPID), SourceType);

                                        }

                                    }
                                }
                                catch (Exception AnyError)
                                {

                                    NetFramework.Console.WriteLine("查询联赛,解析" + Content + "失败", true);

                                    NetFramework.Console.WriteLine(AnyError.Message, true);

                                    NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                                }
                            }
                            #endregion

                            #region "发图"
                            //if (Content == ("图1") || (Content == ("图2")) || Content == "图3" || Content == "图4"
                            //    || (Content.Contains(Environment.NewLine) == false && Content.Contains("图"))
                            //            )
                            {
                                string GameType = "";
                                string PicType = "";
                                string SettingUserName = "";

                                string NewContent = "";
                                if (Content.StartsWith("01"))
                                {
                                    NewContent = "重庆发图";
                                }
                                else if (Content.StartsWith("02"))
                                {
                                    NewContent = "VR发图";
                                }
                                else if (Content.StartsWith("03"))
                                {
                                    NewContent = "澳彩发图";
                                }
                                else if (Content.StartsWith("50"))
                                {
                                    NewContent = "腾五发图";
                                }
                                else if (Content.StartsWith("51"))
                                {
                                    NewContent = "腾五信发图";
                                }
                                else if (Content.StartsWith("10"))
                                {
                                    NewContent = "腾十发图";
                                }
                                else if (Content.StartsWith("11"))
                                {
                                    NewContent = "腾十信发图";
                                }
                                else
                                {
                                    NewContent = Content;
                                }
                                Linq.ProgramLogic.ShiShiCaiPicKeepType KeepPic = Linq.ProgramLogic.ShiShiCaiPicTypeCaculate(NewContent, ref GameType, ref PicType, ref SettingUserName);
                                if (KeepPic != Linq.ProgramLogic.ShiShiCaiPicKeepType.UnKnown)
                                {


                                    DataRow[] Settingcontacts = RunnerF.MemberSource.Select("User_Contact='" + SettingUserName + "'");
                                    if (SettingUserName != "" && Settingcontacts.Length == 0)
                                    {
                                        SendRobotContent("找不到玩家：" + SettingUserName, Tocontacts, SourceType);
                                    }
                                    //图1，2，3，4使用
                                    SendChongqingResultPic(Linq.ProgramLogic.GetMode(SettingUserName == "" ? Tocontacts : Settingcontacts), Content, (FindGroupIsMember.Count() > 0 ? FromUserNameTEMPID : ToUserNameTEMPID));


                                    if (KeepPic == Linq.ProgramLogic.ShiShiCaiPicKeepType.Keep && (MyOutResult == ""))
                                    {
                                        if (Util_Services.GetServicesSetting().SuperUser == true)
                                        {

                                            MyOutResult = Linq.ProgramLogic.WX_UserReplyLog_MySendCreate(GameType + "模式", (SettingUserName == "" ? Tocontacts : Settingcontacts)[0], JavaSecondTime(Convert.ToInt64(msgTime)));
                                        }
                                        else
                                        {
                                            SendRobotContent((SettingUserName == "" ? "" : SettingUserName + "群") + "会员限制彩种,不能切换彩种", Tocontacts, SourceType);

                                        }
                                        MyOutResult = Linq.ProgramLogic.WX_UserReplyLog_MySendCreate(PicType + "发图", (SettingUserName == "" ? Tocontacts : Settingcontacts)[0], JavaSecondTime(Convert.ToInt64(msgTime)));
                                        SendRobotContent((SettingUserName == "" ? "" : SettingUserName + "群") + MyOutResult, Tocontacts, SourceType);

                                    }
                                    if (KeepPic == Linq.ProgramLogic.ShiShiCaiPicKeepType.Stop && (MyOutResult == ""))
                                    {
                                        MyOutResult = Linq.ProgramLogic.WX_UserReplyLog_MySendCreate(PicType + "停图", (SettingUserName == "" ? Tocontacts : Settingcontacts)[0], JavaSecondTime(Convert.ToInt64(msgTime)));
                                        SendRobotContent((SettingUserName == "" ? "" : SettingUserName + "群") + MyOutResult, Tocontacts, SourceType);
                                    }
                                    if (KeepPic == Linq.ProgramLogic.ShiShiCaiPicKeepType.SetTime && (MyOutResult == ""))
                                    {
                                        MyOutResult = Linq.ProgramLogic.WX_UserReplyLog_MySendCreate(GameType, (SettingUserName == "" ? Tocontacts : Settingcontacts)[0], JavaSecondTime(Convert.ToInt64(msgTime)));
                                        SendRobotContent((SettingUserName == "" ? "" : SettingUserName + "群") + MyOutResult, Tocontacts, SourceType);
                                    }
                                    if (KeepPic == Linq.ProgramLogic.ShiShiCaiPicKeepType.RestoreDefault && (MyOutResult == ""))
                                    {
                                        MyOutResult = Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("停止", (SettingUserName == "" ? Tocontacts : Settingcontacts)[0], JavaSecondTime(Convert.ToInt64(msgTime)));
                                        SendRobotContent((SettingUserName == "" ? "" : SettingUserName + "群") + MyOutResult, Tocontacts, SourceType);
                                    }
                                    if (KeepPic == Linq.ProgramLogic.ShiShiCaiPicKeepType.Keep || KeepPic == Linq.ProgramLogic.ShiShiCaiPicKeepType.Once)
                                    {
                                        string ToSendGameType = "";
                                        Linq.ProgramLogic.ShiShiCaiMode ToSendEnumType = Linq.ProgramLogic.ShiShiCaiMode.未知;
                                        switch (GameType)
                                        {
                                            case "重庆":
                                                ToSendEnumType = Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩;
                                                break;
                                            case "新疆":
                                                ToSendEnumType = Linq.ProgramLogic.ShiShiCaiMode.新疆时时彩;
                                                break;
                                            case "五分":
                                                ToSendEnumType = Linq.ProgramLogic.ShiShiCaiMode.五分彩;
                                                break;
                                            case "VR":
                                                ToSendEnumType = Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩;
                                                break;
                                            case "腾五":
                                                ToSendEnumType = Linq.ProgramLogic.ShiShiCaiMode.腾讯五分;
                                                break;
                                            case "腾十":
                                                ToSendEnumType = Linq.ProgramLogic.ShiShiCaiMode.腾讯十分;
                                                break;
                                            case "腾五信":
                                                ToSendEnumType = Linq.ProgramLogic.ShiShiCaiMode.腾五信;
                                                break;
                                            case "腾十信":
                                                ToSendEnumType = Linq.ProgramLogic.ShiShiCaiMode.腾十信;
                                                break;
                                            case "澳彩":
                                                ToSendEnumType = Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5;
                                                break;
                                            case "河五":
                                                ToSendEnumType = Linq.ProgramLogic.ShiShiCaiMode.河内五分;
                                                break;
                                            case "":

                                                ToSendEnumType = Linq.ProgramLogic.GetMode((SettingUserName == "" ? Tocontacts : Settingcontacts));
                                                break;
                                            default:
                                                break;
                                        }
                                        foreach (DataRow item in (SettingUserName == "" ? Tocontacts : Settingcontacts))
                                        {
                                            if (PicType == "")
                                            {

                                                Linq.WX_WebSendPICSetting webpcset = db.WX_WebSendPICSetting.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                              && t.WX_SourceType == item.Field<object>("User_SourceType").ToString()
                               && t.WX_UserName == item.Field<object>("User_ContactID").ToString()
                              );
                                                if (webpcset == null || webpcset.PreSendGameType == null || webpcset.PreSendGameType == "")
                                                {
                                                    PicType = "文本";
                                                }
                                                else
                                                {
                                                    PicType = webpcset.PreSendGameType;
                                                }

                                            }//空白取数
                                            string NoticeTempid = item.Field<string>("User_ContactTEMPID");
                                            SendChongqingResultPic(ToSendEnumType, PicType + "图", NoticeTempid);
                                        }
                                    }//不是未知发图模式
                                }
                            }

                            #endregion


                            if (checkreply.IsBallPIC == true)
                            {
                                #region 联赛查询

                                if (Content == "联赛")
                                {
                                    string Reply = "";
                                    var source = db.Game_FootBall_VS.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                                        // && (t.LastAliveTime == null || t.LastAliveTime >= DateTime.Today.AddDays(-3))
                                        && t.Jobid == GlobalParam.JobID
                                        );
                                    var classsource = (from ds in source
                                                       select new { ds.GameType, ds.MatchClass }).Distinct();
                                    foreach (var item in classsource)
                                    {
                                        Reply += item.GameType + "-" + item.MatchClass + Environment.NewLine;
                                    }
                                    SendRobotContent(Reply, MyUserName(SourceType) == FromUserNameTEMPID ? ToUserNameTEMPID : FromUserNameTEMPID, SourceType);
                                }


                                string[] Files = Directory.GetFiles(Application.StartupPath + "\\output");
                                //foreach (var item in Files)
                                //{
                                //    if (item.Contains(Content) && item.Contains("jpg") && Content != "" && Content != "联赛")
                                //    {

                                //        SendRobotImage(item, MyUserName == FromUserNameTEMPID ? ToUserNameTEMPID : FromUserNameTEMPID, SourceType);
                                //    }
                                //}

                                foreach (var item in Files)
                                {
                                    if (item.Contains(Content) && item.Contains("txt") && Content != "" && Content != "联赛" && Content.Length >= 2 && Regex.Replace(Content, "[0-9]+", "", RegexOptions.IgnoreCase) != "")
                                    {
                                        SendRobotTxtFile(item, MyUserName(SourceType) == FromUserNameTEMPID ? ToUserNameTEMPID : FromUserNameTEMPID, SourceType);
                                        //SendRobotImage(item, MyUserName == FromUserNameTEMPID ? ToUserNameTEMPID : FromUserNameTEMPID, SourceType);
                                    }

                                }
                            }
                                #endregion
                            return;
                        }//会员模式
                        #endregion




                        #region "发图"
                        //if (Content == ("图1") || (Content == ("图2")) || Content == "图3" || Content == "图4"
                        //   || (Content.Contains(Environment.NewLine) == false && Content.Contains("图"))


                        //    )
                        //{
                        //    string GameType = "";
                        //    string PicType = "";
                        //    string SettingUserName = "";
                        //    Linq.ProgramLogic.ShiShiCaiPicKeepType KeepPic = Linq.ProgramLogic.ShiShiCaiPicTypeCaculate(Content, ref GameType, ref PicType, ref SettingUserName);
                        //    if (KeepPic!= Linq.ProgramLogic.ShiShiCaiPicKeepType.UnKnown)
                        //        {
                        //            if (KeepPic == Linq.ProgramLogic.ShiShiCaiPicKeepType.Keep || KeepPic == Linq.ProgramLogic.ShiShiCaiPicKeepType.Once)
                        //            {
                        //                string ToSendGameType = "";
                        //                switch (GameType)
                        //                {
                        //                    case "重庆":
                        //                        ToSendGameType = (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩));

                        //                        break;
                        //                    case "新疆":
                        //                        ToSendGameType = (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.新疆时时彩));

                        //                        break;
                        //                    case "五分":
                        //                        ToSendGameType = (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.五分彩));

                        //                        break;
                        //                    case "VR":
                        //                        ToSendGameType = (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩));

                        //                        break;
                        //                    case "腾五":
                        //                        ToSendGameType = (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾讯五分));

                        //                        break;
                        //                    case "腾十":
                        //                        ToSendGameType = (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾讯十分));

                        //                        break;
                        //                    case "澳彩":
                        //                        ToSendGameType = (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5));

                        //                        break;


                        //                    default:
                        //                        break;
                        //                }

                        //                SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.新疆时时彩, PicType + "图", FromUserNameTEMPID, ToSendGameType);
                        //            }//不是未知发图模式
                        //    }
                        //}

                        #endregion

                        #region "联赛查询"
                        if (Content.Contains("对") || Content.ToUpper().Contains("VS"))
                        {

                            try
                            {

                                Linq.ProgramLogic.FormatResultState State = Linq.ProgramLogic.FormatResultState.Initialize;
                                Linq.ProgramLogic.FormatResultType StateType = Linq.ProgramLogic.FormatResultType.Initialize;
                                string BuyType = "";
                                string BuyMoney = "";
                                string[] q_Teams = new string[] { };
                                Linq.Game_FootBall_VS[] AllTeams = (Linq.Game_FootBall_VS[])Linq.ProgramLogic.ReceiveContentFormat(Content, out State, out StateType, Linq.ProgramLogic.FormatResultDirection.MemoryMatchList, out BuyType, out BuyMoney, out q_Teams);
                                foreach (var matchitem in AllTeams)
                                {

                                    if (StateType == Linq.ProgramLogic.FormatResultType.QueryImage)
                                    {
                                        if (File.Exists(Application.StartupPath + "\\output\\" + matchitem.GameKey + ".jpg"))
                                        {
                                            SendRobotImage(Application.StartupPath + "\\output\\" + matchitem.GameKey + ".jpg", (FromUserNameTEMPID == MyUserName(SourceType) ? ToUserNameTEMPID : FromUserNameTEMPID), SourceType);
                                        }
                                        else
                                        {
                                            RefreshotherV2(matchitem.GameKey);
                                            if (File.Exists(Application.StartupPath + "\\output\\" + matchitem.GameKey + ".jpg"))
                                            {
                                                SendRobotImage(Application.StartupPath + "\\output\\" + matchitem.GameKey + ".jpg", (FromUserNameTEMPID == MyUserName(SourceType) ? ToUserNameTEMPID : FromUserNameTEMPID), SourceType);
                                            }
                                            SendRobotContent("赛事存在，但数据未下载，稍后在试", (FromUserNameTEMPID == MyUserName(SourceType) ? ToUserNameTEMPID : FromUserNameTEMPID), SourceType);

                                        }
                                    }
                                    else if (StateType == Linq.ProgramLogic.FormatResultType.QueryTxt)
                                    {
                                        if (File.Exists(Application.StartupPath + "\\output\\" + matchitem.GameKey + ".txt"))
                                        {
                                            SendRobotTxtFile(Application.StartupPath + "\\output\\" + matchitem.GameKey + ".txt", (FromUserNameTEMPID == MyUserName(SourceType) ? ToUserNameTEMPID : FromUserNameTEMPID), SourceType);
                                        }
                                        else
                                        {
                                            RefreshotherV2(matchitem.GameKey);
                                            if (File.Exists(Application.StartupPath + "\\output\\" + matchitem.GameKey + ".txt"))
                                            {
                                                SendRobotTxtFile(Application.StartupPath + "\\output\\" + matchitem.GameKey + ".txt", (FromUserNameTEMPID == MyUserName(SourceType) ? ToUserNameTEMPID : FromUserNameTEMPID), SourceType);
                                            }
                                            else
                                            {
                                                SendRobotContent("赛事存在，但数据准备失败", (FromUserNameTEMPID == MyUserName(SourceType) ? ToUserNameTEMPID : FromUserNameTEMPID), SourceType);
                                            }
                                        }
                                    }


                                }
                                if (StateType == Linq.ProgramLogic.FormatResultType.QueryResult)
                                {
                                    var LastPoints = db.Game_ResultFootBall_Last.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                                         &&
                                         (
                                         (t.A_Team.Contains(q_Teams[0]) && t.B_Team.Contains(q_Teams[1]))
                                         || (t.A_Team.Contains(q_Teams[1]) && t.B_Team.Contains(q_Teams[0]))
                                         )
                                         && t.LiveBallLastSendTime >= DateTime.Now.AddDays(-2)

                                         );

                                    foreach (var points in LastPoints)
                                    {
                                        SendRobotContent(points.A_Team + "VS" + points.B_Team + ",现时比分" + points.LastPoint, (FromUserNameTEMPID == MyUserName(SourceType) ? ToUserNameTEMPID : FromUserNameTEMPID), SourceType);

                                    }

                                }
                            }
                            catch (Exception AnyError)
                            {

                                NetFramework.Console.WriteLine("查询联赛,解析" + Content + "失败", true);

                                NetFramework.Console.WriteLine(AnyError.Message, true);

                                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                            }
                        }
                        #endregion




                        //if (Content.StartsWith("@"))
                        //{
                        //    Regex FindTmpUserID = new Regex(("@[0-9a-zA-Z]+"), RegexOptions.IgnoreCase);
                        //    string FindSayUserID = FindTmpUserID.Match(Content).Value;
                        //    // DataRow sayuserr = runnerf.MemberSource.Select("User_ContactTEMPID='" + FindSayUserID + "'").First();
                        //}

                        RunnerF.Invoke(new Action(() =>
                        {

                            DataRow newr = RunnerF.ReplySource.NewRow();
                            newr.SetField("Reply_Contact", userr.Field<string>("User_Contact"));
                            newr.SetField("Reply_ContactID", userr.Field<string>("User_ContactID"));
                            newr.SetField("Reply_SourceType", userr.Field<string>("User_SourceType"));
                            newr.SetField("Reply_ContactTEMPID", userr.Field<string>("User_ContactTEMPID"));
                            newr.SetField("Reply_ReceiveContent", Content);
                            newr.SetField("Reply_ReceiveTime", JavaSecondTime(Convert.ToInt64(msgTime)));
                            RunnerF.ReplySource.Rows.Add(newr);
                        }));





                        #region "玩家回复检查是否启用自动跟踪"

                        if (checkreply.IsReply == true)
                        {
                            ////群不下单
                            //if (IsTalkGroup)
                            //{
                            //    return;
                            //}
                            //授权不处理订单
                            if (mysetting.IsReceiveOrder != true)
                            {
                                return;
                            }
                            String OutMessage = "";
                            try
                            {
                                OutMessage = NewWXContent(JavaSecondTime(Convert.ToInt64(msgTime)), Content, userr, SourceType, false);
                            }
                            catch (Exception mysenderror)
                            {

                                NetFramework.Console.WriteLine(mysenderror.Message, true);
                                NetFramework.Console.WriteLine(mysenderror.StackTrace, true);
                            }
                            if (OutMessage != "")
                            {
                                string WX_UserName = Tocontacts[0].Field<string>("User_ContactID");
                                var NoticeList = RunnerF.MemberSource.Select("User_ContactID='" + WX_UserName + "'");

                                SendRobotContent(OutMessage, NoticeList
                                     , userr.Field<string>("User_SourceType")

                                    );
                            }
                        }
                        #endregion
                        //if (checkreply.IsBallPIC == true)
                        {
                            #region 联赛查询

                            if (Content == "联赛")
                            {
                                string Reply = "";
                                var source = db.Game_FootBall_VS.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                                    //&& (t.LastAliveTime == null || t.LastAliveTime >= DateTime.Today.AddDays(-3))
                                     && t.Jobid == GlobalParam.JobID
                                    );
                                var classsource = (from ds in source
                                                   select new { ds.GameType, ds.MatchClass }).Distinct();
                                foreach (var item in classsource)
                                {
                                    Reply += item.GameType + "-" + item.MatchClass + Environment.NewLine;
                                }
                                SendRobotContent(Reply, MyUserName(SourceType) == FromUserNameTEMPID ? ToUserNameTEMPID : FromUserNameTEMPID, SourceType);
                            }


                            string[] Files = Directory.GetFiles(Application.StartupPath + "\\output");
                            ////foreach (var item in Files)
                            ////{
                            ////    if (item.Contains(Content) && item.Contains("jpg") && Content != "" && Content != "联赛")
                            ////    {

                            ////        SendRobotImage(item, MyUserName == FromUserNameTEMPID ? ToUserNameTEMPID : FromUserNameTEMPID, SourceType);
                            ////    }
                            ////}

                            foreach (var item in Files)
                            {
                                if (item.Contains(Content) && item.Contains("txt")
                                    && Content != "" && Content != "联赛"
                                    && Content.Length >= 2
                                    && Regex.Replace(Content, "[0-9]+", "", RegexOptions.IgnoreCase) != ""
                                    && item.Contains("联赛")
                                    )
                                {
                                    SendRobotTxtFile(item, MyUserName(SourceType) == FromUserNameTEMPID ? ToUserNameTEMPID : FromUserNameTEMPID, SourceType);
                                    //SendRobotImage(item, MyUserName == FromUserNameTEMPID ? ToUserNameTEMPID : FromUserNameTEMPID, SourceType);
                                }

                            }

                            #endregion







                        }
                    }//机器人不停止


                    #region 会员并且是私聊
                    if (checkreply.IsAdmin == true)
                    {
                        if (Content == "00" && RobotStop == false)
                        {
                            foreach (DataRow UserRow in RunnerF.MemberSource.Rows)
                            {
                                string Row_WX_UserName = (UserRow == null ? "" : UserRow.Field<string>("User_ContactID"));
                                string Row_WX_SourceType = (UserRow == null ? "" : UserRow.Field<string>("User_SourceType"));

                                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                                    && t.WX_UserName == Row_WX_UserName
                                    && t.WX_SourceType == Row_WX_SourceType);
                                toupdate.IsReply = false;
                                UserRow.SetField("User_IsReply", false);
                                db.SubmitChanges();
                            }
                            string WX_UserName = Tocontacts[0].Field<string>("User_ContactID");
                            var NoticeList = RunnerF.MemberSource.Select("User_ContactID='" + WX_UserName + "'");
                            SendRobotContent("已全取消自动", NoticeList
                                 , userr.Field<string>("User_SourceType"));
                        }
                        else if (Content == "99" && RobotStop == false)
                        {
                            foreach (DataRow UserRow in RunnerF.MemberSource.Rows)
                            {
                                string Row_WX_UserName = (UserRow == null ? "" : UserRow.Field<string>("User_ContactID"));
                                string Row_WX_SourceType = (UserRow == null ? "" : UserRow.Field<string>("User_SourceType"));

                                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                                    && t.WX_UserName == Row_WX_UserName
                                    && t.WX_SourceType == Row_WX_SourceType);
                                toupdate.IsReply = true;
                                UserRow.SetField("User_IsReply", true);
                                db.SubmitChanges();

                            }
                            string WX_UserName = Tocontacts[0].Field<string>("User_ContactID");
                            var NoticeList = RunnerF.MemberSource.Select("User_ContactID='" + WX_UserName + "'");
                            SendRobotContent("已全勾上自动", NoticeList
                                 , userr.Field<string>("User_SourceType"));
                        }
                        else if (Content == "88" && RobotStop == false)
                        {
                            foreach (DataRow UserRow in RunnerF.MemberSource.Rows)
                            {
                                string Row_WX_UserName = (UserRow == null ? "" : UserRow.Field<string>("User_ContactID"));
                                string Row_WX_SourceType = (UserRow == null ? "" : UserRow.Field<string>("User_SourceType"));

                                Linq.WX_WebSendPICSetting webpcset = db.WX_WebSendPICSetting.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                                    && t.WX_SourceType == Row_WX_SourceType
                                     && t.WX_UserName == Row_WX_UserName
                                    );

                                webpcset.IsSendPIC = false;
                                UserRow.SetField("User_IsSendPic", false);
                                db.SubmitChanges();
                            }
                            string WX_UserName = Tocontacts[0].Field<string>("User_ContactID");
                            var NoticeList = RunnerF.MemberSource.Select("User_ContactID='" + WX_UserName + "'");
                            SendRobotContent("已全取消发图", NoticeList
                                 , userr.Field<string>("User_SourceType"));
                        }
                        else if (Content == "66")
                        {
                            this.Invoke(new Action(() =>
                            {
                                cb_robotstop.Checked = true;

                            }));
                            RobotStop = true;
                            string WX_UserName = Tocontacts[0].Field<string>("User_ContactID");
                            var NoticeList = RunnerF.MemberSource.Select("User_ContactID='" + WX_UserName + "'");
                            SendRobotContent("机器人已停止", NoticeList
                                 , userr.Field<string>("User_SourceType"));
                        }
                        else if (Content == "77")
                        {
                            foreach (DataRow UserRow in RunnerF.MemberSource.Rows)
                                this.Invoke(new Action(() =>
                                {
                                    cb_robotstop.Checked = false;

                                }));
                            RobotStop = false;
                            string WX_UserName = Tocontacts[0].Field<string>("User_ContactID");
                            var NoticeList = RunnerF.MemberSource.Select("User_ContactID='" + WX_UserName + "'");
                            SendRobotContent("机器人已回复", NoticeList
                               , userr.Field<string>("User_SourceType"));
                        }


                    }
                    #endregion


                #endregion
                }//内容非空白


            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine("消息处理异常" + AnyError.Message, true);
                NetFramework.Console.WriteLine("消息处理异常" + AnyError.StackTrace, true);
                ;
            }
        }

        List<YixinContact> YixinContactlist = new List<YixinContact>();
        public class YixinContact
        {
            public string ContactID = "";
            public string ContactType = "";
            public string ContactRemarkName = "";
        }
        List<YixinContactInfo> YixinContactInfolist = new List<YixinContactInfo>();
        public class YixinContactInfo
        {
            public string ContactID = "";
            public string ContactName = "";
            public string ContactPhone = "";
            public string ContactSignName = "";
        }

        JObject YiXin_MyInfo = null;

        private String CmdRun(string Path, string ExeAndParam)
        {

            //Process.Start(new ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase) { UseShellExecute = true, Verb = "runas" });


            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是false否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口

            p.StartInfo.Verb = "runas";
            p.Start();//启动程序





            //StreamWrite(p.StandardInput.BaseStream,"chcp 65001"+Environment.NewLine,Encoding.UTF8);
            StreamWrite(p.StandardInput.BaseStream, "cd " + Path + Environment.NewLine, Encoding.GetEncoding("GB2312"));
            if (Path.Length > 2)
            {
                StreamWrite(p.StandardInput.BaseStream, Path.Substring(0, 2) + Environment.NewLine, Encoding.GetEncoding("GB2312"));
            }


            StreamWrite(p.StandardInput.BaseStream, ExeAndParam + Environment.NewLine, Encoding.GetEncoding("GB2312"));
            //向cmd窗口发送输入信息
            //string str = Console.ReadLine();
            StreamWrite(p.StandardInput.BaseStream, "exit" + Environment.NewLine, Encoding.GetEncoding("GB2312"));



            //p.StandardInput.WriteLine("exit");
            //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令



            //获取cmd窗口的输出信息


            //StreamReader reader = new StreamReader(p.StandardOutput.BaseStream, Encoding.GetEncoding("GB2312"));




            p.WaitForExit();//等待程序执行完退出进程  

            // byte[] bytes = new byte[ p.StandardOutput.BaseStream.Length];
            //p.StandardOutput.BaseStream.Read(bytes, 0, bytes.Length);

            // String Outputs = p.StandardOutput.ReadToEnd();
            String Outputs = "";
            p.Close();
            NetFramework.Console.WriteLine("#########################################################", false);
            NetFramework.Console.WriteLine(ExeAndParam, false);

            return Outputs;

            //NetFramework.Console.WriteLine(Outputs);

        }


        private void StreamWrite(Stream BaseStream, string Data, Encoding codee)
        {
            byte[] bytes = codee.GetBytes(Data);
            BaseStream.Write(bytes, 0, bytes.Length);
            BaseStream.Flush();
        }

        public string SendRobotContent(string Content, DataRow[] ToUsers, string WX_SourceType)
        {
            string Result = "";
            foreach (DataRow item in ToUsers)
            {
                Result = SendRobotContent(Content, item.Field<string>("User_ContactTEMPID"), WX_SourceType);
            }
            return Result;
        }

        public string SendRobotContent(string Content, string TempToUserID, string WX_SourceType)
        {

            switch (WX_SourceType)
            {
                case "易":
                    return SendYiXinContent(Content, TempToUserID);

                case "微":
                    return SendWXContent(Content, TempToUserID);
                default:
                    //ldconsole.exe action --name *** --key call.input --value ***
                    //NoxConsole.exe action –name *** –key call.input –value ***
                    // var findenum = InjectWins.Where(t => t.WX_UserTMPID == TempToUserID);

                    //if (findenum != null && cb_adbnoxmode.Checked == true && WX_SourceType == Enum.GetName(typeof(PCSourceType), PCSourceType.雷电))
                    //{
                    //    CmdRun(tb_Leidian.Text, " ldconsole.exe " + "–name " + findenum.WX_UserName + " –key call.input –value " + Content);
                    //}
                    //if (findenum != null && cb_adbnoxmode.Checked == true && WX_SourceType == Enum.GetName(typeof(PCSourceType), PCSourceType.夜神))
                    //{
                    //    CmdRun(tb_NoxPath.Text, " NoxConsole.exe " + "–name " + findenum.WX_UserName + " –key call.input –value " + Content);
                    //}
                    //else if (WX_SourceType == Enum.GetName(typeof(PCSourceType), PCSourceType.PCQ))
                    {
                        try
                        {
                            hwndSendText(Content, new IntPtr(Convert.ToInt32(TempToUserID)));
                        }
                        catch (Exception AnyError)
                        {

                            NetFramework.Console.WriteLine(AnyError.Message, true);
                            NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                        }

                    }

                    return "";

            }

        }

        public string SendRobotTxtFile(string TXTFile, string TempToUserID, string WX_SourceType)
        {
            FileStream fs = new FileStream(TXTFile, FileMode.OpenOrCreate);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
            string Content = Encoding.UTF8.GetString(bytes);
            switch (WX_SourceType)
            {
                case "易":
                    return SendYiXinContent(Content, TempToUserID);

                case "微":
                    return SendWXContent(Content, TempToUserID);


                default:
                    return "";

            }

        }

        public string SendRobotLink(string Title, string Link, string TempToUserID, string WX_SourceType)
        {
            switch (WX_SourceType)
            {
                case "易":
                    SendYiXinContent(Title, TempToUserID);
                    return SendYiXinContent(Link, TempToUserID);

                case "微":
                    SendWXContent(Title, TempToUserID);
                    return SendWXContent(Link, TempToUserID);


                default:
                    return "";

            }

        }

        public void SendRobotImage(string ImageFile, string TempToUserID, string WX_SourceType)
        {
            Thread SnedImageThread = new Thread(new ParameterizedThreadStart(ThreadSendRobotImage));
            SnedImageThread.Start(new object[] { ImageFile, TempToUserID, WX_SourceType });
        }

        public void SendRobotImageByte(byte[] ImageFile, string TempToUserID, string WX_SourceType)
        {
            Thread SnedImageThread = new Thread(new ParameterizedThreadStart(ThreadSendRobotImageByte));
            SnedImageThread.Start(new object[] { ImageFile, TempToUserID, WX_SourceType });
        }
        public void ThreadSendRobotImage(object param)
        {
            try
            {
                string ImageFile = (string)(param as object[])[0];
                string TempToUserID = (string)(param as object[])[1];
                string WX_SourceType = (string)(param as object[])[2]; ;

                switch (WX_SourceType)
                {
                    case "易":
                        SendYiXinImage(ImageFile, TempToUserID);
                        break;
                    case "微":
                        SendWXImage(ImageFile, TempToUserID);
                        break;
                    default:
                        if (WX_SourceType.Contains("QQ"))
                        {

                            hwndSendImageFile(ImageFile, new IntPtr(Convert.ToInt32(TempToUserID)));
                        }
                        return;

                }
            }
            catch (Exception AnyEror)
            {

                NetFramework.Console.WriteLine("图片发送失败," + AnyEror.Message, true);
                NetFramework.Console.WriteLine("图片发送失败," + AnyEror.StackTrace, true);
                return;
            }
        }
        public void ThreadSendRobotImageByte(object param)
        {
            try
            {
                byte[] ImageFile = (byte[])(param as object[])[0];
                MemoryStream ms = new MemoryStream(ImageFile);
                Image tosave = Image.FromStream(ms);
                string NewFileName = Application.StartupPath + "\\EmuFile\\" + Guid.NewGuid().ToString() + ".jpg";
                tosave.Save(NewFileName);
                ms.Close();
                ms.Dispose();
                tosave.Dispose();

                string TempToUserID = (string)(param as object[])[1];
                string WX_SourceType = (string)(param as object[])[2]; ;

                switch (WX_SourceType)
                {
                    case "易":
                        SendYiXinImage(NewFileName, TempToUserID);
                        break;
                    case "微":
                        SendWXImage(NewFileName, TempToUserID);
                        break;
                    default:
                        if (WX_SourceType.Contains("QQ"))
                        {
                            hwndSendImageFileByte(ImageFile, new IntPtr(Convert.ToInt32(TempToUserID)));
                        }
                        return;

                }
            }
            catch (Exception AnyEror)
            {

                NetFramework.Console.WriteLine("图片发送失败," + AnyEror.Message, true);
                NetFramework.Console.WriteLine("图片发送失败," + AnyEror.StackTrace, true);
                return;
            }
        }

        public string SendWXContent(string Content, string TempToUserID)
        {
            Int32 TestCount = 1;
            // ReDo:
            TestCount += 1;
            if (TestCount >= 3)
            {
                NetFramework.Console.WriteLine("文字发送失败" + Content, true);
                return "";
            }
            Thread.Sleep(500);
            try
            {
                //10、发送信息

                //这个比较简单，用POST方法，访问：https://"+webhost+"/cgi-bin/mmwebwx-bin/webwxsendmsg


                //POST的还是json格式，类似这样：

                //{"Msg":{"Type":1,"Content":"测试信息","FromUserName":"XXXXXX","ToUserName":"XXXXXX","LocalID":"时间戳","ClientMsgId":"时间戳"},"BaseRequest":{"Uin":"XXXXXX","Sid":"XXXXXX","Skey":"XXXXXX","DeviceID":"XXXXXX"}}
                //?sid=QfLp+Z+FePzvOFoG&r=1377482079876
                //这里的Content是信息内容，LocalID和ClientMsgId都用当前时间戳。
                string CheckUrl4 = "https://" + webhost + "/cgi-bin/mmwebwx-bin/webwxsendmsg?sid=" + Sid + "&r_=" + JavaTimeSpan();
                JObject body4 = new JObject();
                body4.Add("BaseRequest", j_BaseRequest["BaseRequest"]);
                JObject Msg = new JObject();
                Msg.Add("Type", "1");
                Msg.Add("Content", Content);
                Msg.Add("FromUserName", MyUserName("微"));
                Msg.Add("ToUserName", TempToUserID);
                string timespan = JavaTimeSpanFFFF();
                Msg.Add("LocalID", timespan);
                Msg.Add("ClientMsgId", timespan);

                body4.Add("Msg", Msg);

                string Result4 = NetFramework.Util_WEB.OpenUrl(CheckUrl4
                         , "https://" + webhost + "/", body4.ToString().Replace(Environment.NewLine, ""), "POST", cookie, Encoding.GetEncoding("UTF-8"), true);

                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;
                Linq.WX_PicErrorLog log = new Linq.WX_PicErrorLog();
                log.aspnet_Userid = GlobalParam.UserKey;
                log.SendTime = DateTime.Now;
                log.UploadResult = Content;
                log.SendResult = Result4;
                log.WX_SourceType = "微";
                db.WX_PicErrorLog.InsertOnSubmit(log);
                db.SubmitChanges();

                if (Result4.Contains("\"Ret\": 0,") == false)
                {

                    //if (TestCount >= 3)
                    //{
                    //    NetFramework.Console.WriteLine("文字发送失败" + Content, true);
                    //    return Result4;
                    //}
                    //else
                    //{
                    //    NetFramework.Console.WriteLine("文字发送失败" + Content, true);
                    //    goto ReDo;

                    //}
                }
                return Result4;
            }
            catch
            {
                //goto ReDo;
            }
            return "";
        }

        public string SendWXContent(JObject weixinmsg, string TempToUserID)
        {
            Int32 TestCount = 1;
        ReDo:
            TestCount += 1;
            if (TestCount >= 3)
            {
                NetFramework.Console.WriteLine("JSON发送失败", true);
                return "";
            }
            Thread.Sleep(500);
            try
            {
                //10、发送信息

                //这个比较简单，用POST方法，访问：https://"+webhost+"/cgi-bin/mmwebwx-bin/webwxsendmsg

                //POST的还是json格式，类似这样：

                //{"Msg":{"Type":1,"Content":"测试信息","FromUserName":"XXXXXX","ToUserName":"XXXXXX","LocalID":"时间戳","ClientMsgId":"时间戳"},"BaseRequest":{"Uin":"XXXXXX","Sid":"XXXXXX","Skey":"XXXXXX","DeviceID":"XXXXXX"}}
                //?sid=QfLp+Z+FePzvOFoG&r=1377482079876
                //这里的Content是信息内容，LocalID和ClientMsgId都用当前时间戳。
                string CheckUrl4 = "https://" + webhost + "/cgi-bin/mmwebwx-bin/webwxsendmsg?sid=" + Sid + "&r_=" + JavaTimeSpan();
                JObject body4 = new JObject();
                body4.Add("BaseRequest", j_BaseRequest["BaseRequest"]);


                body4.Add("Msg", weixinmsg);

                string Result4 = NetFramework.Util_WEB.OpenUrl(CheckUrl4
                         , "https://" + webhost + "/", body4.ToString().Replace(Environment.NewLine, ""), "POST", cookie, Encoding.GetEncoding("UTF-8"), true);

                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;
                Linq.WX_PicErrorLog log = new Linq.WX_PicErrorLog();
                log.aspnet_Userid = GlobalParam.UserKey;
                log.SendTime = DateTime.Now;
                log.UploadResult = weixinmsg.ToString();
                log.SendResult = Result4;
                log.WX_SourceType = "微";
                db.WX_PicErrorLog.InsertOnSubmit(log);
                db.SubmitChanges();

                if (Result4.Contains("\"Ret\": 0,") == false)
                {

                    if (TestCount >= 3)
                    {
                        NetFramework.Console.WriteLine("文字发送失败" + weixinmsg.ToString(), true);
                        return Result4;
                    }
                    else
                    {
                        NetFramework.Console.WriteLine("文字发送失败" + weixinmsg.ToString(), true);
                        goto ReDo;

                    }
                }
                return Result4;
            }
            catch
            {
                goto ReDo;
            }

        }

        public string SendWXImage(string ImageFile, string TEMPUserName)
        {
            Int32 TestCount = 1;
        ReDo:
            TestCount += 1;
            if (TestCount >= 3)
            {
                NetFramework.Console.WriteLine("图片发送失败", true);
                return "";
            }
            Thread.Sleep(500);
            try
            {
                string UpLoadResult2 = NetFramework.Util_WEB.UploadWXImage(ImageFile, MyUserName("微"), TEMPUserName, JavaTimeSpan(), cookie, j_BaseRequest, webhost);

                //{
                //"BaseResponse": {
                //"Ret": 0,
                //"ErrMsg": ""
                //}
                //,
                //"MediaId": "@crypt_33344d6e_b33557427c4a251b699847e345597efabd85640cc9f3f3be2b26f7c0c7050c991c632d52dabdb7bf064836b75bcf83af1e7e68389581d4ea8b7d2ab4b1e8ee197c29f34f687b17fb5d65e4f53533314ff10306498b37e6eaa180b774a2d969b2b3c2a4dbed6091d831022d2ac5aa957921890346cdfd76f59309655ea52b4745bb0a753627ec2589075ca5fc5b43c5e0e9da6f7bc073f98a16b445d8d5c904739ee7c139d78c347ed06cc33d228b60e1d86ccfdc8d449f5ebc41675165012e1f6e971cd545870ee19392dec805928a33828a54c12f6e90d41bd9b67dcc57c437a8d9ffe4b93c5f1af2b7cc0bf5a865bb292c46db7bf18b2f2c135917ac5c4b0451051ebcb324b7a22c434358d083382bcc74adf467d894f649f5b6612bc4b4e4ec9fdcadcd001d524dd8651d26eea05ed19b5676f14328ae5d3aa055e30051c1",
                //"StartPos": 80114,
                //"CDNThumbImgHeight": 100,
                //"CDNThumbImgWidth": 74,
                //"EncryFileName": "Data%2Ejpg"
                //}

                JObject returnupload2 = JObject.Parse(UpLoadResult2);
                string MediaID2 = (returnupload2["MediaId"].ToString());
                //POST /cgi-bin/mmwebwx-bin/webwxsendmsgimg?fun=async&f=json HTTP/1.1
                //Host: "+webhost+"
                //{
                //"BaseRequest": {
                //    "Uin": 2402981522,
                //    "Sid": "wBFSJu8HTfMyOOvw",
                //    "Skey": "@crypt_bbd454c7_2a7fbf7dd30ef13f7f52d08edeb74c8a",
                //    "DeviceID": "e679329170898983"
                //},
                //"Msg": {
                //    "Type": 3,
                //    "MediaId": "@crypt_33344d6e_b33557427c4a251b699847e345597efabd85640cc9f3f3be2b26f7c0c7050c991c632d52dabdb7bf064836b75bcf83af1e7e68389581d4ea8b7d2ab4b1e8ee197c29f34f687b17fb5d65e4f53533314ff10306498b37e6eaa180b774a2d969b2b3c2a4dbed6091d831022d2ac5aa957921890346cdfd76f59309655ea52b4745bb0a753627ec2589075ca5fc5b43c5e0e9da6f7bc073f98a16b445d8d5c904739ee7c139d78c347ed06cc33d228b60e1d86ccfdc8d449f5ebc41675165012e1f6e971cd545870ee19392dec805928a33828a54c12f6e90d41bd9b67dcc57c437a8d9ffe4b93c5f1af2b7cc0bf5a865bb292c46db7bf18b2f2c135917ac5c4b0451051ebcb324b7a22c434358d083382bcc74adf467d894f649f5b6612bc4b4e4ec9fdcadcd001d524dd8651d26eea05ed19b5676f14328ae5d3aa055e30051c1",
                //    "Content": "",
                //    "FromUserName": "@a57d4ad282cdf68368ff9fc32f00aa49e0390d71e304a6a15727d45c540b4239",
                //    "ToUserName": "@@8bfae6c8f3731e7ca57fec7f6af9901c08bba6c6d5a72dce7b8da1c91d98a8bf",
                //    "LocalID": "15233296619040196",
                //    "ClientMsgId": "15233296619040196"
                //},
                //"Scene": 0
                //}
                string CheckUrl2 = "https://" + webhost + "/cgi-bin/mmwebwx-bin/webwxsendmsgimg?fun=async&f=json";
                JObject body2 = new JObject();
                body2.Add("BaseRequest", j_BaseRequest["BaseRequest"]);
                JObject Msg2 = new JObject();
                Msg2.Add("Type", 3);
                Msg2.Add("MediaId", MediaID2);
                Msg2.Add("Content", "");
                Msg2.Add("FromUserName", MyUserName("微"));
                Msg2.Add("ToUserName", TEMPUserName);

                String Time2 = JavaTimeSpanFFFF();

                Msg2.Add("LocalID", Time2);
                Msg2.Add("ClientMsgId", Time2);

                body2.Add("Msg", Msg2);
                body2.Add("Scene", 0);

                NetFramework.Console.WriteLine("正在发图" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), false);

                string Result2 = NetFramework.Util_WEB.OpenUrl(CheckUrl2
                  , "https://" + webhost + "/", body2.ToString().Replace(Environment.NewLine, "").Replace(" ", ""), "POST", cookie, Encoding.UTF8, false);


                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;
                Linq.WX_PicErrorLog log = new Linq.WX_PicErrorLog();
                log.aspnet_Userid = GlobalParam.UserKey;
                log.SendTime = DateTime.Now;
                log.UploadResult = UpLoadResult2;
                log.SendResult = Result2;
                log.WX_SourceType = "微";
                db.WX_PicErrorLog.InsertOnSubmit(log);
                db.SubmitChanges();


                NetFramework.Console.WriteLine("发送结果" + Result2 + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), false);

                if (Result2.Contains("\"Ret\": 0,") == false)
                {

                    if (TestCount >= 3)
                    {
                        NetFramework.Console.WriteLine("图片发送失败", true);
                        return Result2;
                    }
                    else
                    {
                        NetFramework.Console.WriteLine("重试图片发送", true);
                        goto ReDo;

                    }
                }
                return Result2;
            }
            catch
            {
                goto ReDo;
            }



        }

        Int32 YixinSer = 0;
        public string SendYiXinContent(string Content, string TempToUserID)
        {

            if (YixinSer != NextSer - 1)
            {
                string FirstSendBody = "3:::{\"SID\":96,\"CID\":4,\"SER\":" + YixinSer.ToString() + ",\"Q\":[{\"t\":\"long\",\"v\":\"" + MyUserName("易") + "\"},{\"t\":\"byte\",\"v\":1}]}";
                YixinSer += 1;
                string WSResultRepeat = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im:9092/socket.io/1/" + strprefix + "/" + strfindurid + "?t=" + JavaTimeSpan()
                   , "https://web.yixin.im", FirstSendBody, "POST", cookieyixin, System.Text.Encoding.UTF8, true, true);
                // WR_repeat = JObject.Parse(WSResultRepeat);


            }



            //3:::{"SID":96,"CID":1,"SER":2,"Q":[{"t":"property","v":{"1":"168367856","2":"168324356","3":"发个消息给我","4":"1533115796.337","5":"0","6":"903a263c8e5dd101474290243a289a9e"}}]}

            string ContactType = "";
            DataRow UserRoow = RunnerF.MemberSource.Select("User_ContactID='" + TempToUserID.Replace("'", "''") + "' AND User_SourceType='易'")[0];
            ContactType = UserRoow["User_ContactType"].ToString();

            JObject BodyJson = null;
            if (ContactType == "个人")
            {
                BodyJson = JObject.Parse("{\"SID\":96,\"CID\":1,\"SER\":" + YixinSer.ToString() + ",\"Q\":[{\"t\":\"property\",\"v\":{\"1\":\"168367856\",\"2\":\"168324356\",\"3\":\"发个消息给我\",\"4\":\"1533115796.337\",\"5\":\"0\",\"6\":\"903a263c8e5dd101474290243a289a9e\"}}]}");
                BodyJson["SER"] = YixinSer.ToString();
                BodyJson["Q"][0]["v"]["1"] = TempToUserID;
                BodyJson["Q"][0]["v"]["2"] = MyUserName("易");
                BodyJson["Q"][0]["v"]["3"] = Content;
                BodyJson["Q"][0]["v"]["4"] = (Convert.ToInt64(JavaTimeSpan()) / 1000).ToString();
                BodyJson["Q"][0]["v"]["6"] = Guid.NewGuid().ToString().Replace("-", "");

            }
            else
            {

                BodyJson = JObject.Parse("{\"SID\":94,\"CID\":10,\"SER\":" + YixinSer.ToString() + ",\"Q\":[{\"t\":\"long\",\"v\":\"41900238\"},{\"t\":\"property\",\"v\":{\"1\":\"41900238\",\"2\":\"168324356\",\"3\":\"群消息000\",\"4\":\"1533802681.846\",\"5\":\"0\",\"6\":\"529077973a8fbdf842a89a42fe7cc881\"}}]}");
                BodyJson["SER"] = YixinSer.ToString();
                BodyJson["Q"][0]["v"] = TempToUserID;
                BodyJson["Q"][1]["v"]["1"] = TempToUserID;
                BodyJson["Q"][1]["v"]["2"] = MyUserName("易");
                BodyJson["Q"][1]["v"]["3"] = Content;
                BodyJson["Q"][1]["v"]["4"] = (Convert.ToInt64(JavaTimeSpan()) / 1000).ToString();
                BodyJson["Q"][1]["v"]["6"] = Guid.NewGuid().ToString().Replace("-", "");
            }
            string SendBody = "3:::" + BodyJson.ToString();
            YixinSer += 1;


            string WSResult = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im:9092/socket.io/1/" + strprefix + "/" + strfindurid + "?t=" + JavaTimeSpan()
               , "https://web.yixin.im", SendBody, "POST", cookieyixin, System.Text.Encoding.UTF8, true, true);
            // WR_repeat = JObject.Parse(WSResultRepeat);


            // WSResult = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im:9092/socket.io/1/" + strprefix + "/" + strfindurid + "?t=" + JavaTimeSpan()
            //, "https://web.yixin.im", "", "GET", cookieyixin, System.Text.Encoding.UTF8, true, true);
            // WR_repeat = JObject.Parse(WSResultRepeat);

            //数据不一定立即返回，不能同步
            //string[]  Messages = WSResult.Split(Splits);
            //  foreach (var messageitem in Messages)
            //  {
            //      JObject Retrn = JObject.Parse(messageitem);
            //      if (Retrn["code"].ToString() != "200")
            //      {
            //          NetFramework.Console.WriteLine("发送失败");
            //          NetFramework.Console.WriteLine(messageitem);
            //      }
            //  }

            //如果发送成功
            //            3:::{
            //  "cid" : 1,
            //  "code" : 200,
            //  "ser" : 0,
            //  "sid" : 92,
            //  "key" : 168367856,
            //  "r" : [ 24347991201, {
            //    "body" : [ {
            //      "1" : "168324356",
            //      "2" : "168367856",
            //      "4" : "1533115807",
            //      "5" : "5",
            //      "6" : "903a263c8e5dd101474290243a289a9e",
            //      "20002" : "49811",
            //      "20001" : "202.68.200.157"
            //    } ],
            //    "headerPacket" : {
            //      "sid" : 96,
            //      "cid" : 1,
            //      "key" : 168367856
            //    }
            //  } ]
            //}

            return "";

        }

        public string SendYiXinImage(string ImageFile, string TEMPUserName)
        {
            //3:::{"SID":96,"CID":1,"SER":3,"Q":[{"t":"property","v":{"1":"168367856","2":"168324356","3":"图片","4":"1533116272.971","5":"1","6":"07cc609372652b4b10d11b963c977897","51":"ec6a2cd8d5a4f885945620450f2b9df4","53":"http://nos-yx.netease.com/yixinpublic/pr_fj7cturlagjpcf_hyq8q9q==_1533116270_19288624","56":"image/jpeg","58":"0"}}]}
            //strfindurid
            //https://nos-hz.yixin.im/nos/webbatchupload?uid=168324356&sid=43930d23-5e36-4440-af37-d2cce3998e4b&size=1&type=0&limit=15
            //https://nos-hz.yixin.im/nos/webbatchupload?uid=168324356&sid=43930d23-5e36-4440-af37-d2cce3998e4b&size=1&type=0&limit=15
            string Uploadresult = NetFramework.Util_WEB.UploadYixinImage(ImageFile, cookieyixin, MyUserName("易"), MyUploadId);
            // {"result":[{"bucket":"yixinpublic","object":"pr_paqpkc5hwiekxvz5n3dbhg==_1533169024_30997221","etag":"3f28b61cad5b2f0807c647e7df401f34","fileName":"simple.png","fileSize":425136,"uploadCode":0}],"code":"200"}

            if (Uploadresult.Contains("code\":\"200\"") == false)
            {
                NetFramework.Console.WriteLine("图片上传失败", true);
                NetFramework.Console.WriteLine(Uploadresult, true);
                return Uploadresult;
            }
            else
            {



                if (YixinSer != NextSer - 1)
                {
                    string FirstSendBody = "3:::{\"SID\":96,\"CID\":4,\"SER\":" + YixinSer.ToString() + ",\"Q\":[{\"t\":\"long\",\"v\":\"" + MyUserName("易") + "\"},{\"t\":\"byte\",\"v\":1}]}";
                    YixinSer += 1;
                    string WSResultRepeat = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im:9092/socket.io/1/" + strprefix + "/" + strfindurid + "?t=" + JavaTimeSpan()
                       , "https://web.yixin.im", FirstSendBody, "POST", cookieyixin, System.Text.Encoding.UTF8, true, true);
                    // WR_repeat = JObject.Parse(WSResultRepeat);


                }

                if (Uploadresult == "")
                {
                    NetFramework.Console.WriteLine("图片发送失败，服务器超时", true);
                    return "";
                }
                JObject JUploadResult = JObject.Parse(Uploadresult);

                string ContactType = "";
                DataRow UserRoow = RunnerF.MemberSource.Select("User_ContactID='" + TEMPUserName.Replace("'", "''") + "' AND User_SourceType='易'")[0];
                ContactType = UserRoow["User_ContactType"].ToString();
                JObject BodyJson = null;
                if (ContactType == "个人")
                {


                    BodyJson = JObject.Parse("{\"SID\":96,\"CID\":1,\"SER\":" + YixinSer.ToString() + ",\"Q\":[{\"t\":\"property\",\"v\":{\"1\":\"168367856\",\"2\":\"168324356\",\"3\":\"图片\",\"4\":\"1533116272.971\",\"5\":\"1\",\"6\":\"07cc609372652b4b10d11b963c977897\",\"51\":\"ec6a2cd8d5a4f885945620450f2b9df4\",\"53\":\"http://nos-yx.netease.com/yixinpublic/pr_fj7cturlagjpcf_hyq8q9q==_1533116270_19288624\",\"56\":\"image/jpeg\",\"58\":\"0\"}}]}");
                    BodyJson["SER"] = YixinSer.ToString();
                    BodyJson["Q"][0]["v"]["1"] = TEMPUserName;
                    BodyJson["Q"][0]["v"]["2"] = MyUserName("易");
                    BodyJson["Q"][0]["v"]["3"] = "图片";
                    BodyJson["Q"][0]["v"]["4"] = (Convert.ToInt64(JavaTimeSpan()) / 1000).ToString();
                    BodyJson["Q"][0]["v"]["6"] = Guid.NewGuid().ToString().Replace("-", "");


                    BodyJson["Q"][0]["v"]["51"] = JUploadResult["result"][0]["etag"].ToString();
                    BodyJson["Q"][0]["v"]["53"] = "http://nos-yx.netease.com/yixinpublic/" + JUploadResult["result"][0]["object"].ToString();

                }
                else
                {
                    BodyJson = JObject.Parse("{\"SID\":94,\"CID\":10,\"SER\":" + YixinSer.ToString() + ",\"Q\":[{\"t\":\"long\",\"v\":\"41900238\"},{\"t\":\"property\",\"v\":{\"1\":\"41900238\",\"2\":\"168324356\",\"3\":\"群消息000\",\"4\":\"1533802681.846\",\"5\":\"1\",\"6\":\"529077973a8fbdf842a89a42fe7cc881\"}}]}");
                    BodyJson["SER"] = YixinSer.ToString();
                    BodyJson["Q"][0]["v"] = TEMPUserName;
                    BodyJson["Q"][1]["v"]["1"] = TEMPUserName;
                    BodyJson["Q"][1]["v"]["2"] = MyUserName("易");
                    BodyJson["Q"][1]["v"]["3"] = "图片";
                    BodyJson["Q"][1]["v"]["4"] = (Convert.ToInt64(JavaTimeSpan()) / 1000).ToString();
                    BodyJson["Q"][1]["v"]["6"] = Guid.NewGuid().ToString().Replace("-", "");


                    BodyJson["Q"][1]["v"]["51"] = JUploadResult["result"][0]["etag"].ToString();
                    BodyJson["Q"][1]["v"]["53"] = "http://nos-yx.netease.com/yixinpublic/" + JUploadResult["result"][0]["object"].ToString();

                }


                string SendBody = "3:::" + BodyJson.ToString();
                YixinSer += 1;
                string WSResult = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im:9092/socket.io/1/" + strprefix + "/" + strfindurid + "?t=" + JavaTimeSpan()
                   , "https://web.yixin.im", SendBody, "POST", cookieyixin, System.Text.Encoding.UTF8, true, true);
                // WR_repeat = JObject.Parse(WSResultRepeat);


                // WSResult = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im:9092/socket.io/1/" + strprefix + "/" + strfindurid + "?t=" + JavaTimeSpan()
                //, "https://web.yixin.im", "", "GET", cookieyixin, System.Text.Encoding.UTF8, true, true);
                // // WR_repeat = JObject.Parse(WSResultRepeat);
                // string[] Messages = WSResult.Split(Splits);
                // foreach (var messageitem in Messages)
                // {

                //     try
                //     {
                //         JObject Retrn = JObject.Parse(messageitem);
                //         NetFramework.Console.WriteLine("发送图片返回:" + messageitem);
                //     }
                //     catch (Exception)
                //     {

                //         NetFramework.Console.WriteLine("发送图片返回:" + WSResult);
                //     }


                // }

                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;
                Linq.WX_PicErrorLog log = new Linq.WX_PicErrorLog();
                log.aspnet_Userid = GlobalParam.UserKey;
                log.SendTime = DateTime.Now;
                log.UploadResult = Uploadresult;
                log.SendResult = WSResult;
                log.WX_SourceType = "易";
                db.WX_PicErrorLog.InsertOnSubmit(log);
                db.SubmitChanges();
                return WSResult;

            }

        }


        bool _WeiXinOnLine = false;
        bool _YiXinOnline = false;

        public bool WeiXinOnLine
        {
            get { return _WeiXinOnLine; }
            set
            {
                _WeiXinOnLine = value; MI_GameLogManulDeal.Enabled = _YiXinOnline || _WeiXinOnLine; MI_Bouns_Manul.Enabled = _YiXinOnline || _WeiXinOnLine;

                #region 启动实时比分

                if (RunnerF.MembersSet_firstrun == true)
                {
                    System.Threading.Thread st_rcp = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadRepeatCheckPoint));
                    st_rcp.Start();
                }
                RunnerF.MembersSet_firstrun = false;
                #endregion


            }
        }
        public bool YiXinOnline
        {
            get { return _YiXinOnline; }
            set
            {
                _YiXinOnline = value; MI_GameLogManulDeal.Enabled = _YiXinOnline || _WeiXinOnLine; ; MI_Bouns_Manul.Enabled = _YiXinOnline || _WeiXinOnLine;


                #region 启动实时比分

                if (RunnerF.MembersSet_firstrun == true)
                {
                    System.Threading.Thread st_rcp = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadRepeatCheckPoint));
                    st_rcp.Start();
                }
                RunnerF.MembersSet_firstrun = false;
                #endregion
            }
        }



        public void ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode subm, bool IgoreDataSettingSend = true, bool IgoreMemberGroup = false)
        {



            NetFramework.Console.WriteLine("正在开奖" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), false);
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            Linq.aspnet_UsersNewGameResultSend checkus = Util_Services.GetServicesSetting();
            string LastPeriod = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                && t.GameName == subm.ToString()

                ).OrderByDescending(t => t.GamePeriod).First().GamePeriod;

            if ((checkus != null && checkus.IsNewSend == true) || (IgoreDataSettingSend == true))
            {


                #region "发送余额"
                var noticeChangelist = db.WX_UserGameLog.Where(t => t.Result_HaveProcess == false
                    && t.aspnet_UserID == GlobalParam.UserKey
                    && ((_WeiXinOnLine == true && t.WX_SourceType == "微")
                      || (_YiXinOnline == true && t.WX_SourceType == "易")
                      || (t.WX_SourceType != "微" && t.WX_SourceType != "易")
                      && (string.Compare(t.GamePeriod, (t.OpenMode == "澳洲幸运5" || t.OpenMode == "VR重庆时时彩" ? "" : "20") + LastPeriod) <= 0)
                      )

                    ).Select(t => new { t.WX_UserName, t.WX_SourceType, t.MemberGroupName, t.GamePeriod }).Distinct().ToArray();

                var lst_membergroup = (from dssub in
                                           (from ds in noticeChangelist
                                            select new { ds.MemberGroupName, ds.WX_SourceType }).Distinct()
                                       select new TMP_MemberGroup(dssub.MemberGroupName, dssub.WX_SourceType)).ToArray();



                foreach (var notice_item in noticeChangelist)
                {

                    Int32 TotalChanges = Linq.ProgramLogic.WX_UserGameLog_Deal(this, notice_item.WX_UserName, notice_item.WX_SourceType);
                    if (TotalChanges == 0)
                    {
                        continue;
                    }

                    decimal? ReminderMoney = Linq.ProgramLogic.WXUserChangeLog_GetRemainder(notice_item.WX_UserName, notice_item.WX_SourceType);

                    var Rows = RunnerF.MemberSource.Select("User_ContactID='" + notice_item.WX_UserName.Replace("'", "''") + "' and User_SourceType='" + notice_item.WX_SourceType + "'");
                    if (Rows.Count() < 1)
                    {
                        NetFramework.Console.WriteLine("找不到联系人，发不出", true);
                        continue;
                    }
                    string TEMPUserName = Rows.First().Field<string>("User_ContactTEMPID");
                    string SourceType = Rows.First().Field<string>("User_SourceType");

                    if (notice_item.MemberGroupName != "")
                    {
                        var memmbertmp = lst_membergroup.SingleOrDefault(t => t.MemberGroupName == notice_item.MemberGroupName);
                        memmbertmp.TMPID = TEMPUserName;
                    }


                    #region "PC端不一个个的发"
                    //if ((notice_item.WX_SourceType == "微" || notice_item.WX_SourceType == "易") || IgoreMemberGroup)
                    {

                        if (IgoreMemberGroup == true && notice_item.WX_SourceType != "微" && notice_item.WX_SourceType != "易")
                        {

                            var sets = db.WX_PCSendPicSetting.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                              && t.WX_SourceType == notice_item.WX_SourceType
                              && t.WX_UserName == notice_item.MemberGroupName
                              );
                            foreach (var setitem in sets)
                            {
                                setitem.NextSendString = ("@" + notice_item.WX_UserName + "##") + "余" + (ReminderMoney.HasValue ? ReminderMoney.Value.ToString("N0") : "");
                            }
                            db.SubmitChanges();
                        }
                        else if (notice_item.WX_SourceType == "微" || notice_item.WX_SourceType == "易")
                        {
                            foreach (var noticeitem in Rows)
                            {
                                string noticeTEMPUserName = noticeitem.Field<string>("User_ContactTEMPID");
                                String ContentResult = SendRobotContent("已开奖，可继续下注，余" + (ReminderMoney.HasValue ? ReminderMoney.Value.ToString("N0") : ""), noticeTEMPUserName, notice_item.WX_SourceType);

                            }

                        }
                    }
                    #endregion
                    var updatechangelog = db.WX_UserChangeLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == notice_item.WX_UserName && t.WX_SourceType == notice_item.WX_SourceType && t.NeedNotice == false);
                    db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, updatechangelog);
                    foreach (var updatechangeitem in updatechangelog)
                    {
                        updatechangeitem.HaveNotice = true;
                    }


                    db.SubmitChanges();

                }//循环开奖

                #region 群整点发


                foreach (var memberite in lst_membergroup)
                {
                    if (memberite.MemberGroupName == "")
                    {
                        continue;
                    }
                    var sets = InjectWins.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                          && t.WX_SourceType == memberite.WX_SourceType
                          && t.WX_UserName == memberite.MemberGroupName
                          );
                    string ReturnSend = "##" + Environment.NewLine;

                    string GameFullPeriod = "";
                    string GameFullLocalPeriod = "";
                    string NextSubPeriod = "";



                    bool ShiShiCaiSuccess = false;
                    string ShiShiCaiErrorMessage = "";
                    // Linq.ProgramLogic.ShiShiCaiMode subm = Linq.ProgramLogic.GetMode(sets.First());

                    Linq.ProgramLogic.ChongQingShiShiCaiCaculatePeriod(DateTime.Now, "", db, "", "", out GameFullPeriod, out GameFullLocalPeriod, true, out ShiShiCaiSuccess, out ShiShiCaiErrorMessage, subm);

                    NextSubPeriod = GameFullPeriod.Substring(GameFullPeriod.Length - 3, 3);
                    ReturnSend += "战斗胜负数据如下：" + Environment.NewLine;

                    var buyusers = noticeChangelist.Where(t => t.MemberGroupName == memberite.MemberGroupName && t.WX_SourceType == memberite.WX_SourceType).Select(t => new { t.WX_UserName, t.WX_SourceType, t.GamePeriod }).Distinct();
                    foreach (var useritem in buyusers)
                    {


                        decimal? ReminderMoney = Linq.ProgramLogic.WXUserChangeLog_GetRemainder(useritem.WX_UserName, useritem.WX_SourceType);
                        ReturnSend += "[" + useritem.WX_UserName + "]本期盈亏:"
                            + Linq.ProgramLogic.GetUserPeriodInOut(useritem.GamePeriod, useritem.WX_UserName, useritem.WX_SourceType, db).ToString("N0")
                            + ",总分:" + (ReminderMoney.HasValue ? ReminderMoney.Value.ToString() : "0") + Environment.NewLine;
                        ReturnSend += "---------------" + Environment.NewLine;



                    }

                    ReturnSend += "====================" + Environment.NewLine
                        + Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm) + "【" + NextSubPeriod + "回合】" + Environment.NewLine
                                + "开战！" + Environment.NewLine
                                + "请各指挥官开始进攻！" + Environment.NewLine
                                + "====================" + Environment.NewLine;

                    if (lst_membergroup.Count() != 0)
                    {
                        //String ContentResult = SendRobotContent(ReturnSend, memberite.TMPID, memberite.WX_SourceType);
                        foreach (var setitem in sets)
                        {
                            setitem.NextSendString = ReturnSend;
                        }
                        //winsdb.SubmitChanges();

                    }
                }
                #endregion


                //var tonotice = db.Logic_WX_UserGameLog_Deal(GlobalParam.Key);
                //foreach (var item in tonotice)
                //{
                //    DataRow[] user = RunnerF.MemberSource.Select("User_ContactID='" + item.WX_UserName + "' and User_SourceType='"+item.WX_SourceType+"'");
                //    if (user.Length == 0)
                //    {
                //        continue;
                //    }
                //    SendWXContent((item.Remainder.HasValue ? item.Remainder.Value.ToString("N0") : "0"), user[0].Field<string>("User_ContactID"));
                //}

                #endregion

                NetFramework.Console.WriteLine("开奖完成" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), false);
            }
        }

        private class TMP_MemberGroup
        {
            public TMP_MemberGroup(string _MemberGroupName, string _WX_SourceType)
            {

                MemberGroupName = _MemberGroupName;
                WX_SourceType = _WX_SourceType;
            }

            public string MemberGroupName { get; set; }

            public string WX_SourceType { get; set; }

            public string TMPID { get; set; }

            public List<TMP_BuyUserPeriod> BuyUserInfos { get; set; }
        }
        private class TMP_BuyUserPeriod
        {
            public string WX_UserName { get; set; }
            public string WX_SourceType { get; set; }

            public string GameFullPeriod { get; set; }
        }



        public static object Proccesing = false;
        public string NewWXContent(DateTime ReceiveTime, string ReceiveContent, DataRow userr, string SourceType, bool adminmode = false, Int32 ReceiveIndex = 1, bool ReturnPreMessage = true, string MemberGroupName = "")
        {
            lock (Proccesing)
            {
                Proccesing = true;
                string NewContent = ReceiveContent;

                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;
                Linq.WX_UserReplyLog log = db.WX_UserReplyLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                                              && t.WX_UserName == userr.Field<string>("User_ContactID")
                                              && t.WX_SourceType == userr.Field<string>("User_SourceType")
                                              && t.ReceiveTime == ReceiveTime
                                              && t.SourceType == SourceType
                                              && t.ReceiveIndex == ReceiveIndex
                                              );
                if (log == null)
                {

                    Linq.WX_UserReplyLog newlogr = new Linq.WX_UserReplyLog();
                    newlogr.aspnet_UserID = GlobalParam.UserKey;
                    newlogr.WX_UserName = userr.Field<string>("User_ContactID");
                    newlogr.WX_SourceType = userr.Field<string>("User_SourceType");
                    newlogr.ReceiveContent = ReceiveContent;
                    newlogr.ReceiveTime = ReceiveTime;
                    newlogr.SourceType = SourceType;
                    newlogr.ReceiveIndex = ReceiveIndex;
                    newlogr.ReplyContent = "";
                    newlogr.HaveDeal = false;
                    db.WX_UserReplyLog.InsertOnSubmit(newlogr);
                    db.SubmitChanges();

                    #region "老板查询"
                    if (ReceiveContent.Length == 8 || ReceiveContent.Length == 17)
                    {
                        try
                        {
                            Linq.WX_UserReply testu = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_SourceType == newlogr.WX_SourceType
                                && t.WX_UserName == newlogr.WX_UserName);
                            if (testu.IsBoss == true)
                            {
                                NetFramework.Console.WriteLine("准备老板查询发图", false);
                                DataTable Result2 = WeixinRoboot.Linq.ProgramLogic.GetBossReportSource(newlogr.WX_SourceType, ReceiveContent);
                                DrawDataTable(Result2);

                                SendRobotImage(Application.StartupPath + "\\Data" + GlobalParam.UserName + "老板查询.jpg", userr.Field<string>("User_ContactTEMPID"), userr.Field<string>("User_SourceType"));


                                NetFramework.Console.WriteLine("准备老板查询发图完毕", false);
                                Linq.PIC_EndSendLog bsl = new Linq.PIC_EndSendLog();
                                bsl.WX_BossID = newlogr.WX_UserName;
                                bsl.WX_SourceType = newlogr.WX_SourceType;
                                bsl.WX_SendDate = DateTime.Now;
                                bsl.WX_UserName = newlogr.WX_UserName;
                                bsl.aspnet_UserID = GlobalParam.UserKey;
                                db.PIC_EndSendLog.InsertOnSubmit(bsl);


                                Linq.PIC_EndSendLog findbsl = db.PIC_EndSendLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                                    && t.WX_SourceType == newlogr.WX_SourceType
                                     && t.WX_BossID == newlogr.WX_UserName

                                    );
                                if (findbsl == null)
                                {
                                    db.SubmitChanges();
                                }



                            }
                        }
                        catch (Exception AnyError)
                        {
                            NetFramework.Console.WriteLine(AnyError.StackTrace, true);

                        }

                    }
                    #endregion


                    bool ISCancel = false;
                    if (NewContent.Contains("取消"))
                    {
                        ISCancel = true;
                        NewContent = NewContent.Replace("取消", "");
                    }

                    Linq.ProgramLogic.GameMode gm = Linq.ProgramLogic.GameMode.非玩法;
                    Linq.ProgramLogic.ShiShiCaiMode subm = Linq.ProgramLogic.ShiShiCaiMode.未知;


                    subm = Linq.ProgramLogic.GetMode(userr);

                    if (ReceiveContent.Contains("期"))
                    {
                        ShiShiCaiDealGameLogAndNotice(subm, true, false);

                    }

                    if (NewContent.StartsWith("六"))
                    {
                        gm = Linq.ProgramLogic.GameMode.六合彩;
                        NewContent = NewContent.Substring(1);
                    }
                    else if (ReceiveContent.Contains("-")
                        || ReceiveContent.Contains("主")
                        || ReceiveContent.Contains("客")
                        || ReceiveContent.Contains("大球")
                        || ReceiveContent.Contains("小球")

                         || ReceiveContent.Contains("主客")
                         || ReceiveContent.Contains("主和")
                         || ReceiveContent.Contains("主主")
                         || ReceiveContent.Contains("和客")
                         || ReceiveContent.Contains("和主")
                         || ReceiveContent.Contains("和和")
                         || ReceiveContent.Contains("客客")
                         || ReceiveContent.Contains("客和")
                         || ReceiveContent.Contains("客主")

                        )
                    {
                        gm = Linq.ProgramLogic.GameMode.球赛;
                    }
                    else if (ReceiveContent == "查")
                    {
                        gm = Linq.ProgramLogic.GameMode.时时彩;
                    }
                    else
                    {
                        gm = Linq.ProgramLogic.GameMode.非玩法;
                    }

                    string RequestPeriod = "";

                    if (ReceiveContent.Contains("期"))
                    {


                        RequestPeriod = NewContent.Split("期".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];


                        NewContent = NewContent.Replace(RequestPeriod, "").Replace("期", "");


                    }


                    #region 期号全取消,logcreate实现
                    //if (ISCancel == true && RequestPeriod != "" && NewContent == "")
                    //{


                    //    //string GameFullPeriod = "";
                    //    //string GameFullLocalPeriod = "";
                    //    //bool ShiShiCaiSuccess = false;
                    //    //string ShiShiCaiErrorMessage = "";
                    //    //Linq.ProgramLogic.ChongQingShiShiCaiCaculatePeriod(ReceiveTime, RequestPeriod, db, userr.Field<string>("User_ContactID"), userr.Field<string>("User_SourceType"), out GameFullPeriod, out GameFullLocalPeriod, adminmode, out ShiShiCaiSuccess, out ShiShiCaiErrorMessage);

                    //    //if (gm == Linq.ProgramLogic.GameMode.时时彩)
                    //    //{


                    //    //    var periodscancel = db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                    //    //        && t.GamePeriod == GameFullPeriod
                    //    //        && t.WX_UserName == userr.Field<string>("User_ContactID")
                    //    //        && t.WX_SourceType == userr.Field<string>("User_SourceType")
                    //    //        );
                    //    //    foreach (var item in periodscancel)
                    //    //    {
                    //    //        NewContent += item.Buy_Value + Convert.ToInt32(item.Buy_Point).ToString() + ",";
                    //    //    }
                    //    //    if (NewContent.EndsWith(","))
                    //    //    {
                    //    //        NewContent = NewContent.Substring(0, NewContent.Length - 1);
                    //    //    }
                    //    //}
                    //    //else if (gm == Linq.ProgramLogic.GameMode.六合彩)
                    //    //{
                    //    //    var periodscancel = db.WX_UserGameLog_HKSix.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                    //    //          && t.GamePeriod == GameFullPeriod
                    //    //          && t.WX_UserName == userr.Field<string>("User_ContactID")
                    //    //          && t.WX_SourceType == userr.Field<string>("User_SourceType")
                    //    //          );
                    //    //    foreach (var item in periodscancel)
                    //    //    {
                    //    //        NewContent += item.BuyValue+"-" + Convert.ToInt32(item.BuyMoney).ToString() + ",";
                    //    //    }
                    //    //    if (NewContent.EndsWith(","))
                    //    //    {
                    //    //        NewContent = NewContent.Substring(0, NewContent.Length - 1);
                    //    //    }
                    //    //}

                    //}
                    #endregion





                    #region /或\玩法

                    string[] SubModeContents = NewContent.Replace("，", ",").Replace("，", ",")
                                                            .Replace(".", ",").Replace("。", ",").Replace("。", ",").Replace(" ", "")
                        .Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    string TotalNewContent = "";

                    foreach (var subitem in SubModeContents)
                    {
                        string SubGameContent = subitem.Replace("/", "\\");

                        string[] suborders = SubGameContent.Split("\\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                        string OutGameContent = "";

                        if (suborders.Length == 2)
                        {
                            char[] buynums = suborders[0].ToCharArray();
                            foreach (var sublogicitem in buynums)
                            {
                                string tmp_numc = sublogicitem.ToString();
                                tmp_numc = tmp_numc.Replace("0", "零");
                                tmp_numc = tmp_numc.Replace("1", "一");
                                tmp_numc = tmp_numc.Replace("2", "二");
                                tmp_numc = tmp_numc.Replace("3", "三");
                                tmp_numc = tmp_numc.Replace("4", "四");
                                tmp_numc = tmp_numc.Replace("5", "五");
                                tmp_numc = tmp_numc.Replace("6", "六");
                                tmp_numc = tmp_numc.Replace("7", "七");
                                tmp_numc = tmp_numc.Replace("8", "八");
                                tmp_numc = tmp_numc.Replace("9", "九");


                                OutGameContent += (ISCancel == true ? "取消" : "") + "全" + tmp_numc + suborders[1] + ",";
                            }
                        }
                        else if (suborders.Length == 3)
                        {
                            suborders[0] = suborders[0].Replace("1", "万");
                            suborders[0] = suborders[0].Replace("2", "千");
                            suborders[0] = suborders[0].Replace("3", "百");
                            suborders[0] = suborders[0].Replace("4", "十");
                            suborders[0] = suborders[0].Replace("5", "个");

                            char[] suborderprefix = suborders[0].ToCharArray();

                            char[] buynums = suborders[1].ToCharArray();

                            foreach (var prefixitem in suborderprefix)
                            {
                                foreach (var numsitem in buynums)
                                {
                                    string tmp_numc = numsitem.ToString();
                                    tmp_numc = tmp_numc.Replace("0", "零");
                                    tmp_numc = tmp_numc.Replace("1", "一");
                                    tmp_numc = tmp_numc.Replace("2", "二");
                                    tmp_numc = tmp_numc.Replace("3", "三");
                                    tmp_numc = tmp_numc.Replace("4", "四");
                                    tmp_numc = tmp_numc.Replace("5", "五");
                                    tmp_numc = tmp_numc.Replace("6", "六");
                                    tmp_numc = tmp_numc.Replace("7", "七");
                                    tmp_numc = tmp_numc.Replace("8", "八");
                                    tmp_numc = tmp_numc.Replace("9", "九");

                                    OutGameContent += (ISCancel == true ? "取消" : "") + prefixitem + tmp_numc + suborders[2] + ",";
                                }
                            }


                        }
                        else
                        {
                            OutGameContent = subitem + ",";
                        }

                        TotalNewContent += OutGameContent;

                    }

                    if (TotalNewContent.EndsWith(","))
                    {
                        TotalNewContent = TotalNewContent.Substring(0, TotalNewContent.Length - 1);
                    }
                    NewContent = TotalNewContent;
                    #endregion


                    #region  六连单玩法
                    if (gm == Linq.ProgramLogic.GameMode.六合彩)
                    {

                        string NewResultContent = "";
                        string NewToChangeContent = NewContent.Replace("，", ",").Replace("，", ",")
                                           .Replace(".", ",").Replace("。", ",").Replace("。", ",").Replace(" ", "")
                                           .Replace("大", "大?").Replace("??", "?")
                                           .Replace("小", "小?").Replace("??", "?")
                                           .Replace("单", "单?").Replace("??", "?")
                                           .Replace("双", "双?").Replace("??", "?")
                                           .Replace("鼠", "鼠?").Replace("??", "?")
                                           .Replace("牛", "牛?").Replace("??", "?")
                                           .Replace("虎", "虎?").Replace("??", "?")
                                           .Replace("兔", "兔?").Replace("??", "?")
                                           .Replace("龙", "龙?").Replace("??", "?")
                                           .Replace("蛇", "蛇?").Replace("??", "?")
                                           .Replace("马", "马?").Replace("??", "?")
                                           .Replace("羊", "羊?").Replace("??", "?")
                                           .Replace("猴", "猴?").Replace("??", "?")
                                           .Replace("鸡", "鸡?").Replace("??", "?")
                                           .Replace("狗", "狗?").Replace("??", "?")
                                           .Replace("猪", "猪?").Replace("??", "?")
                                           .Replace("?", "-").Replace("-,", ",")
                                           ;

                        if (NewToChangeContent.EndsWith("-"))
                        {
                            NewToChangeContent = NewToChangeContent.Substring(0, NewToChangeContent.Length - 1);
                        }
                        Regex FindMoney = new Regex("-[0-9]+", RegexOptions.IgnoreCase);
                        int indf = NewToChangeContent.IndexOf("-");
                        while (indf > 0)
                        {

                            string PreFix = NewToChangeContent.Substring(0, indf);
                            string EndPreFix = NewToChangeContent.Substring(indf);

                            string[] BuyTypes = PreFix.Split(",".ToCharArray());
                            string strmoney = FindMoney.Match(EndPreFix).Value;
                            if (strmoney.Length > 1)
                            {
                                strmoney = strmoney.Substring(1);
                            }
                            foreach (var Buyitem in BuyTypes)
                            {
                                try
                                {
                                    Convert.ToDecimal(strmoney);
                                }
                                catch (Exception)
                                {

                                    continue;
                                }
                                NewResultContent += Buyitem + "-" + strmoney + ",";
                            }
                            Int32 NextStart = EndPreFix.IndexOf(",");
                            NewToChangeContent = EndPreFix.Substring(NextStart + 1);
                            indf = NewToChangeContent.IndexOf("-");



                        }//循环查找-的位置
                        if (NewResultContent.EndsWith(","))
                        {
                            NewResultContent = NewResultContent.Substring(0, NewResultContent.Length - 1);
                        }
                        NewContent = NewResultContent == "" ? NewToChangeContent : NewResultContent;
                    }//六合彩模式才生效



                    #endregion



                    string ReturnSend = "";
                    try
                    {
                        if (Linq.ProgramLogic.ShiShiCaiIsOrderContent(NewContent))
                        {
                            gm = Linq.ProgramLogic.GameMode.时时彩;
                        }
                        ReturnSend = Linq.ProgramLogic.WX_UserReplyLog_Create(userr.Table, gm, subm, RequestPeriod, ReceiveTime, (ISCancel == true ? "取消" : "") + NewContent, userr.Field<string>("User_ContactID"), userr.Field<string>("User_SourceType"), adminmode, MemberGroupName);
                    }
                    catch (Exception AnyError2)
                    {

                        NetFramework.Console.WriteLine(AnyError2.Message, true);
                        NetFramework.Console.WriteLine(AnyError2.StackTrace, true);
                    }




                    string[] Splits = NewContent.Replace("，", ",").Replace("，", ",")
                                                            .Replace(".", ",").Replace("。", ",").Replace("。", ",").Replace(" ", "")
                        .Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    //修正多语句 
                    if (gm != Linq.ProgramLogic.GameMode.非玩法)
                    {
                        bool CheckSuccess = true;
                        foreach (var subitem in Splits)
                        {
                            if (Linq.ProgramLogic.ShiShiCaiIsOrderContent(subitem) == false)
                            {
                                CheckSuccess = false;
                                break;
                            }
                        }
                        if (CheckSuccess)
                        {
                            gm = Linq.ProgramLogic.GameMode.时时彩;
                        }

                    }
                    if (Splits.Count() != 1)
                    {
                        DateTime Times = ReceiveTime;

                        foreach (var Splititem in Splits)
                        {

                            String TmpMessage = "";
                            try
                            {
                                TmpMessage = Linq.ProgramLogic.WX_UserReplyLog_Create(userr.Table, gm, subm, RequestPeriod, ReceiveTime, ((ISCancel == true ? "取消" : "") + Splititem), userr.Field<string>("User_ContactID"), userr.Field<string>("User_SourceType"), adminmode, MemberGroupName);

                            }
                            catch (Exception AnyError2)
                            {

                                NetFramework.Console.WriteLine(AnyError2.Message, true);
                                NetFramework.Console.WriteLine(AnyError2.StackTrace, true);
                            }

                            if (TmpMessage != "")
                            {
                                ReturnSend = TmpMessage;
                            }
                        }


                    }



                    newlogr.ReplyContent = ReturnSend;
                    newlogr.ReplyTime = DateTime.Now;
                    newlogr.HaveDeal = false;
                    try
                    {
                        db.SubmitChanges();
                    }
                    catch (Exception AnyError2)
                    {

                        NetFramework.Console.WriteLine(AnyError2.Message, true);
                        NetFramework.Console.WriteLine(AnyError2.StackTrace, true);
                    }



                    if (NewContent.Contains("期"))
                    {
                        if (gm == Linq.ProgramLogic.GameMode.时时彩)
                        {
                            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩, true, true);
                        }
                        else if (gm == Linq.ProgramLogic.GameMode.六合彩)
                        {


                        }


                    }

                    return ReturnSend;




                }
                else
                {
                    NetFramework.Console.WriteLine("下单记录已存在", true);
                    return ReturnPreMessage == true ? log.ReplyContent : "";

                }
            }//lock代码
            //string Message = "";
            //Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[ GlobalParam.DataSourceName].ConnectionString);
            ////db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            //db.Logic_WX_UserReplyLog_Create(ReceiveContent, userr.Field<string>("User_ContactID"), userr.Field<string>("User_SourceType"), GlobalParam.Key, ReceiveTime, ref Message, SourceType);

            //return Message;

        }//新消息

        string RedirtURI = "";
        private JObject WXInit(bool SetDataBase = false)
        {
            Int32 RetryCount = 1;
        retry:
            RetryCount += 1;
            if (RetryCount >= 5)
            {
                MessageBox.Show("微信无法登陆");
                return null;
            }
            try
            {

                string Result = "";



                cookie = new CookieCollection();
                Result = NetFramework.Util_WEB.OpenUrl("https://login.weixin.qq.com/cgi-bin/mmwebwx-bin/login?loginicon=true&uuid=" + _uuid + "&tip=" + _tip.ToString() + "&_=" + JavaTimeSpan()
                     , "", "", "GET", cookie);


                NetFramework.Console.WriteLine(Result, true);
                string Redirect = Result.Substring(Result.IndexOf("redirect_uri"));
                Redirect = Redirect.Substring(Redirect.IndexOf("\"") + 1);
                Redirect = Redirect.Substring(0, Redirect.Length - 2);
                string CheckUrl2 = Redirect;
                RedirtURI = CheckUrl2;
                webhost = CheckUrl2.Substring(CheckUrl2.IndexOf("//") + 2);
                webhost = webhost.Substring(0, webhost.IndexOf("/"));

                //CheckUrl2 = CheckUrl2.Replace(webhost, "wechat.qq.com");
                //webhost = "wechat.qq.com";

                string Result2 = NetFramework.Util_WEB.OpenUrl(CheckUrl2
               , "", "", "GET", cookie, false);

                newridata.LoadXml(Result2);

                if (newridata.SelectSingleNode("error/message") != null)
                {

                    if (newridata.SelectSingleNode("error/message").InnerText != "")
                    {
                        MessageBox.Show(newridata.SelectSingleNode("error/message").InnerText);
                        Environment.Exit(0);
                    }
                }

                pass_ticket = newridata.SelectSingleNode("error/pass_ticket").InnerText;
                Uin = newridata.SelectSingleNode("error/wxuin").InnerText;
                Sid = newridata.SelectSingleNode("error/wxsid").InnerText;
                Skey = newridata.SelectSingleNode("error/skey").InnerText;
                this.Invoke(new Action(() => { lbl_msg.Text = "初始化"; }));




                //Thread KeepUpdateContactThread = new Thread(new ParameterizedThreadStart(KeepUpdateContactThreadDo));
                //KeepUpdateContactThread.Start(new object[]{ KeepUpdateContactThreadID,Skey,pass_ticket});

                return RepeatGetMembers(Skey, pass_ticket, SetDataBase);

            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine(AnyError.Message, true);
                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                goto retry;
            }
        }

        Int32 GetMembersCount = 1;

        private JObject RepeatGetMembers(string Skey, string pass_ticket, bool SetDataBase = true)
        {
            GetMembersCount += 1;
            if (GetMembersCount > 10)
            {
                MessageBox.Show("获取联系人超过10次");
                return null; ;
            }

            string CheckUrl3 = "https://" + webhost + "/cgi-bin/mmwebwx-bin/webwxinit?r=" + JavaTimeSpan() + "&pass_ticket=" + pass_ticket;

            j_BaseRequest = new JObject();
            j_BaseRequest.Add("BaseRequest", "");

            JObject bcc = new JObject();
            bcc.Add("Uin", Uin);
            bcc.Add("Sid", Sid);
            bcc.Add("Skey", Skey);
            // DeviceID = "e" + ( Convert.ToInt64( JavaTimeSpan()) .ToString("000000000000000"));
            bcc.Add("DeviceID", DeviceID);

            j_BaseRequest["BaseRequest"] = bcc;
            //Cookie guid = new Cookie("__guid", "16776304.2514178305917694000.1522594008310.6897", "/");
            //guid.Domain = cookie[0].Domain;
            //cookie.Add(guid);



            //Cookie freq = new Cookie("MM_WX_NOTIFY_STATE", "1");
            //Cookie last_wxuin = new Cookie("MM_WX_SOUND_STATE", "1");

            //freq.Domain = cookie[0].Domain;
            //cookie.Add(freq);
            //last_wxuin.Domain = cookie[0].Domain;
            //cookie.Add(last_wxuin);


            string Result3 = NetFramework.Util_WEB.OpenUrl(CheckUrl3
           , "https://" + webhost + "/", j_BaseRequest.ToString().Replace(Environment.NewLine, "").Replace(" ", ""), "POST", cookie, false);


            InitResponse = JObject.Parse(Result3);

            synckeys = (InitResponse["SyncKey"] as JObject);



            WX_MyInfo = InitResponse["User"]["UserName"].ToString();

            // 6、获取好友列表

            //使用POST方法，访问：https://"+webhost+"/cgi-bin/mmwebwx-bin/webwxgetcontact?r=时间戳

            //POST的内容为空。成功则以JSON格式返回所有联系人的信息。格式类似：
            string str_memb = NetFramework.Util_WEB.OpenUrl("https://" + webhost + "/cgi-bin/mmwebwx-bin/webwxgetcontact?r=" + JavaTimeSpan() + "&seq=0&skey=" + Skey
        , "https://" + webhost + "/", "", "GET", cookie);

            JObject Members = JObject.Parse(str_memb);



            if (InitResponse["ContactList"] != null)
            {
                (Members["MemberList"] as JArray).Add((InitResponse["ContactList"].ToArray()));
            }

            ;

            if (SetDataBase == true)
            {
                RunnerF.MembersSet(Members);
            }

            return Members;



        }

        private void RepeatGetMembersYiXin()
        {

            string FirstSendBody = "3:::{\"SID\":90,\"CID\":34,\"Q\":[{\"t\":\"string\",\"v\""
                + " :\"" + System.Web.HttpUtility.UrlDecode(qrresult_YiXin["message"].Value<string>()) + "\"},{\"t\":\"property\",\"v\":{\"9\":\"80\",\"10\":\"100\",\"16\""
                + "  :\"" + (cookieyixin[" yxlkdeviceid"] == null ? "syl5faSRmgZ6bsMsFvo9" : cookieyixin[" yxlkdeviceid"].Value) + "\",\"24\":\"\"}},{\"t\":\"boolean\",\"v\":true}]}";


            string WSResultRepeat = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im:9092/socket.io/1/" + strprefix + "/" + strfindurid + "?t=" + JavaTimeSpan()
           , "https://web.yixin.im", FirstSendBody, "POST", cookieyixin, System.Text.Encoding.UTF8, true, true);
            // WR_repeat = JObject.Parse(WSResultRepeat);




            WSResultRepeat = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im:9092/socket.io/1/" + strprefix + "/" + strfindurid + "?t=" + JavaTimeSpan()
           , "https://web.yixin.im", "", "GET", cookieyixin, System.Text.Encoding.UTF8, true, true);

            //myinfo?
            //                        3:::{
            //  "sid" : 90,
            //  "cid" : 34,
            //  "code" : 200,
            //  "r" : [ 168324356, "c0996c10-21fb-44a6-a642-45ffc3a60a82", "113.117.245.200", "f1321f0db626e643", 0, false ],
            //  "key" : 0,
            //  "ser" : 0
            //}

            string[] Messages = WSResultRepeat.Split(Splits, StringSplitOptions.RemoveEmptyEntries);
            //3:::{"SID":96,"CID":4,"SER":1,"Q":[{"t":"long","v":"168367856"},{"t":"byte","v":1}]}
            //3:::{"SID":96,"CID":1,"SER":2,"Q":[{"t":"property","v":{"1":"168367856","2":"168324356","3":"FULL LOAD TEST","4":"1533087979.705","5":"0","6":"7527c77d8e7b78c9c6ab4f371de29696"}}]}

            if (Messages.Length == 1)
            {
                YiXinMessageProcess(Messages[0]);
            }//收到一个消息
            else
            {

                foreach (var item in Messages)
                {
                    try
                    {
                        Convert.ToDouble(item);
                    }
                    catch (Exception)
                    {
                        YiXinMessageProcess(item);
                    }//偶数的
                    ;
                }

            }//收到多消息

        }



        Dictionary<Guid, Boolean> KillThread = new Dictionary<Guid, bool>();



        private void DownLoad163ThreadDo_Vr()
        {
            while (true)
            {
                try
                {
                    DownloadResult_Vr(false);
                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception anyerror)
                {


                    System.Threading.Thread.Sleep(1000);
                }


            }
        }
        private void DownLoad163ThreadDo_VrHK()
        {
            while (true)
            {
                try
                {
                    DownloadResult_VrHK(false);
                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception anyerror)
                {


                    System.Threading.Thread.Sleep(1000);
                }


            }
        }
        private void DownLoad163ThreadDo_Aoz()
        {
            while (true)
            {
                try
                {
                    DownloadResult_Aoz(false);
                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception anyerror)
                {


                    System.Threading.Thread.Sleep(1000);
                }


            }
        }
        private void DownLoad163ThreadDo_tengshi()
        {
            while (true)
            {
                try
                {
                    DownloadResult_tengshi(false);
                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception anyerror)
                {


                    System.Threading.Thread.Sleep(1000);
                }


            }
        }
        private void DownLoad163ThreadDo_tengshi_xin()
        {
            while (true)
            {
                try
                {
                    DownloadResult_tengshi_xin(false);
                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception anyerror)
                {


                    System.Threading.Thread.Sleep(1000);
                }


            }
        }
        private void DownLoad163ThreadDo_tengwu()
        {
            while (true)
            {
                try
                {
                    DownloadResult_tengwu(false);
                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception anyerror)
                {


                    System.Threading.Thread.Sleep(1000);
                }


            }
        }

        private void DownLoad163ThreadDo_tengwu_xin()
        {
            while (true)
            {
                try
                {
                    DownloadResult_tengwu_xin(false);
                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception anyerror)
                {


                    System.Threading.Thread.Sleep(1000);
                }


            }
        }

        private void DownLoad163ThreadDo_wufen()
        {
            while (true)
            {
                try
                {
                    DownloadResult_wufen(false);
                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception anyerror)
                {


                    System.Threading.Thread.Sleep(1000);
                }


            }
        }
        private void DownLoad163ThreadDo_xiangjiang()
        {
            while (true)
            {
                try
                {
                    DownloadResult_xiangjiang(false);
                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception anyerror)
                {


                    System.Threading.Thread.Sleep(1000);
                }


            }
        }
        private void DownLoad163ThreadDo_chongqing()
        {
            while (true)
            {
                try
                {
                    DownloadResult_chongqing(false);
                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception anyerror)
                {


                    System.Threading.Thread.Sleep(1000);
                }


            }
        }

        private void DownLoad163ThreadDo_HeNeiWuFen()
        {
            while (true)
            {
                try
                {
                    DownloadResult_HeNeiWuFen(false);
                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception anyerror)
                {


                    System.Threading.Thread.Sleep(1000);
                }


            }
        }


        private void DownloadResult_Aoz(bool IsOpwnNow)
        {



            try
            {

                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;
                Linq.aspnet_UsersNewGameResultSend loadset = Util_Services.GetServicesSetting();

                if (loadset.Thread_AoZhouCai == true)
                {
                    Boolean TmpCheck = false;
                    //DownLoad163CaiPiaoV_aozc(ref TmpCheck, DateTime.Today, false, IsOpwnNow, true);
                    try
                    {
                        DownLoad163CaiPiaoV_aozc888888(ref TmpCheck, DateTime.Today, false, IsOpwnNow, true);
                    }
                    catch (Exception AnyError)
                    {
                        NetFramework.Console.WriteLine(AnyError.Message, true);
                        NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                    } try
                    {
                        DownLoad163CaiPiaoV_aozcAPI168(ref TmpCheck, DateTime.Today, false, IsOpwnNow, true);
                    }
                    catch (Exception AnyError)
                    {
                        NetFramework.Console.WriteLine(AnyError.Message, true);
                        NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                    }
                    if (TmpCheck)
                    {
                        DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5);
                        SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5);
                    }

                }






            }
            catch (Exception AnyError)
            {
                ; NetFramework.Console.WriteLine(AnyError.Message, true);
                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
            }

        }

        private void DownloadResult_Vr(bool IsOpwnNow)
        {





            try
            {
                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;
                Linq.aspnet_UsersNewGameResultSend loadset = Util_Services.GetServicesSetting();

                if (loadset.Thread_VRChongqing == true)
                {
                    Boolean TmpCheck = false;

                    {
                        try
                        {
                            DownLoad163CaiPiaoV_vrchongqingcaislimapp(ref TmpCheck, DateTime.Today.AddDays(1), false, IsOpwnNow);
                        }

                        catch (Exception AnyError)
                        {
                            NetFramework.Console.WriteLine(AnyError.Message, true);

                        }

                        try
                        {
                            DownLoad163CaiPiaoV_vrchongqingcais_CopyFromOtherUsers(ref TmpCheck, false);
                        }

                        catch (Exception AnyError)
                        {
                            NetFramework.Console.WriteLine(AnyError.Message, true);
                        }

                        if (TmpCheck)
                        {
                            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);
                            SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);
                        }
                    }



                    //DownLoadTest();
                }

            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine(AnyError.Message, true);
                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
            }






        }
        private void DownloadResult_VrHK(bool IsOpwnNow)
        {





            try
            {
                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;
                Linq.aspnet_UsersNewGameResultSend loadset = Util_Services.GetServicesSetting();

                if (loadset.Thread_VRChongqing == true)
                {
                    Boolean TmpCheck = false;

                    {

                        try
                        {
                            DownLoad163CaiPiaoV_vrchongqingcai(ref TmpCheck, DateTime.Today.AddDays(1), false, IsOpwnNow);

                        }
                        catch (Exception AnyError)
                        {
                            NetFramework.Console.WriteLine(AnyError.Message, true);

                        }
                        try
                        {
                            DownLoad163CaiPiaoV_vrchongqingcaislim(ref TmpCheck, DateTime.Today.AddDays(1), false, IsOpwnNow);

                        }
                        catch (Exception AnyError)
                        {
                            NetFramework.Console.WriteLine(AnyError.Message, true);

                        }

                        try
                        {
                            DownLoad163CaiPiaoV_vrchongqingcais_CopyFromOtherUsers(ref TmpCheck, false);
                        }

                        catch (Exception AnyError)
                        {
                            NetFramework.Console.WriteLine(AnyError.Message, true);
                        }

                        if (TmpCheck)
                        {
                            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);
                            SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);
                        }
                    }



                    //DownLoadTest();
                }

            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine(AnyError.Message, true);
                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
            }






        }
        private void DownloadResult_tengshi(bool IsOpwnNow)
        {



            try
            {

                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;
                Linq.aspnet_UsersNewGameResultSend loadset = Util_Services.GetServicesSetting();
                try
                {
                    if (loadset.Thread_TengXunShiFen == true)
                    {
                        Boolean TmpCheck = false;
                        try
                        {
                            DownLoad163CaiPiaoV_tengxunshifen(ref TmpCheck, DateTime.Today, false, IsOpwnNow);
                        }
                        catch (Exception)
                        {


                        }

                        try
                        {
                            DownLoad163CaiPiaoV_tengxunshifen_gf4(ref TmpCheck, DateTime.Today, false, IsOpwnNow);
                        }
                        catch (Exception)
                        {


                        }


                        if (TmpCheck)
                        {
                            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);
                            SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);
                        }
                    }
                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine(AnyError.Message, true);
                    NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                }



            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine(AnyError.Message, true);
                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
            }

        }

        private void DownloadResult_tengshi_xin(bool IsOpwnNow)
        {



            try
            {

                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;
                Linq.aspnet_UsersNewGameResultSend loadset = Util_Services.GetServicesSetting();
                try
                {
                    if (loadset.Thread_TengXunShiFenXin == true)
                    {
                        Boolean TmpCheck = false;
                        try
                        {
                            DownLoad163CaiPiaoV_tengxunshifen_xin_gf4(ref TmpCheck, DateTime.Today, false, IsOpwnNow);

                        }
                        catch (Exception)
                        {


                        }
                        try
                        {
                            DownLoad163CaiPiaoV_tengxunshifen_xin_188(ref TmpCheck, DateTime.Today, false, IsOpwnNow);

                        }
                        catch (Exception)
                        {


                        }

                        if (TmpCheck)
                        {
                            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾十信);
                            SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.腾十信);
                        }
                    }
                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine(AnyError.Message, true);
                    NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                }



            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine(AnyError.Message, true);
                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
            }

        }
        private void DownloadResult_tengwu(bool IsOpwnNow)
        {



            try
            {

                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;
                Linq.aspnet_UsersNewGameResultSend loadset = Util_Services.GetServicesSetting();

                try
                {
                    if (loadset.Thread_TengXunWuFen == true)
                    {
                        Boolean TmpCheck = false;
                        try
                        {
                            DownLoad163CaiPiaoV_tengxunwufenbyhuayu(ref TmpCheck, DateTime.Today, false, IsOpwnNow);

                        }
                        catch (Exception)
                        {


                        }
                        try
                        {

                            //DownLoad163CaiPiaoV_tengxunwufenbyhuayu(ref TmpCheck, DateTime.Today, false, IsOpwnNow);

                            DownLoad163CaiPiaoV_tengxunwufenbyhuayulishi(ref TmpCheck, DateTime.Today, false, IsOpwnNow);

                        }
                        catch (Exception)
                        {


                        }
                        try
                        {

                            //DownLoad163CaiPiaoV_tengxunwufenbyhuayu(ref TmpCheck, DateTime.Today, false, IsOpwnNow);

                            DownLoad163CaiPiaoV_tengxunwufen_gf4(ref TmpCheck, DateTime.Today, false, IsOpwnNow);

                        }
                        catch (Exception)
                        {


                        }

                        if (TmpCheck)
                        {
                            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾讯五分);
                            SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.腾讯五分);
                        }
                    }
                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine(AnyError.Message, true);
                    NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                }




            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine(AnyError.Message, true);
                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
            }

        }

        private void DownloadResult_tengwu_xin(bool IsOpwnNow)
        {



            try
            {

                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;
                Linq.aspnet_UsersNewGameResultSend loadset = Util_Services.GetServicesSetting();

                try
                {
                    if (loadset.Thread_TengXunWuFenXin == true)
                    {
                        Boolean TmpCheck = false;
                        try
                        {
                            DownLoad163CaiPiaoV_tengxunwufen_xin_188(ref TmpCheck, DateTime.Today, false, IsOpwnNow);

                        }
                        catch (Exception)
                        {


                        }
                        try
                        {
                            DownLoad163CaiPiaoV_tengxunwufen_xin_gf4(ref TmpCheck, DateTime.Today, false, IsOpwnNow);

                        }
                        catch (Exception)
                        {


                        }

                        if (TmpCheck)
                        {
                            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾五信);
                            SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.腾五信);
                        }
                    }
                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine(AnyError.Message, true);
                    NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                }




            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine(AnyError.Message, true);
                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
            }

        }

        private void DownloadResult_wufen(bool IsOpwnNow)
        {



            try
            {

                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;
                Linq.aspnet_UsersNewGameResultSend loadset = Util_Services.GetServicesSetting();

                try
                {

                    if (loadset.Thread_WuFen == true)
                    {
                        bool TmpCheck = false;

                        DownLoad163CaiPiaoV_wufencai(ref TmpCheck, DateTime.Now, false, IsOpwnNow);


                        DownLoad163CaiPiaoV_wufencai(ref TmpCheck, DateTime.Now.AddDays(-1), false, IsOpwnNow);

                        if (TmpCheck)
                        {
                            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.五分彩);
                            SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.五分彩);
                        }
                    }
                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine(AnyError.Message, true);
                    NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                }





            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine(AnyError.Message, true);
                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
            }

        }
        private void DownloadResult_xiangjiang(bool IsOpwnNow)
        {



            try
            {

                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;
                Linq.aspnet_UsersNewGameResultSend loadset = Util_Services.GetServicesSetting();

                try
                {
                    Boolean TmpCheck = false;
                    if (loadset.Thread_XinJiangShiShiCai == true)
                    {
                        DownLoad163CaiPiaoV_xinjiangshishicai(ref TmpCheck, DateTime.Now.AddDays(-1), false, IsOpwnNow, true);

                        if (TmpCheck)
                        {
                            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.新疆时时彩);
                            SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.新疆时时彩);
                        }
                    }
                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine(AnyError.Message, true);
                    NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                }


            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine(AnyError.Message, true);
                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
            }

        }
        private void DownloadResult_chongqing(bool IsOpwnNow)
        {



            try
            {

                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;
                Linq.aspnet_UsersNewGameResultSend loadset = Util_Services.GetServicesSetting();


                try
                {

                    if (loadset.Thread_ChongQingShiShiCai == true)
                    {
                        Boolean TmpCheck = false;
                        DownLoad163CaiPiaoV_13322(ref TmpCheck, DateTime.Today, false, IsOpwnNow);
                        if (TmpCheck)
                        {
                            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
                            SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
                        }
                    }

                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine(AnyError.Message, true);
                    NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                }

                //try
                //{

                //    if (loadset.Thread_ChongQingShiShiCai == true)
                //    {
                //        Boolean TmpCheck = false;
                //        DownLoad163CaiPiaoV_kaijiangwang1332Fast(ref TmpCheck, DateTime.Today, false, IsOpwnNow);

                //    }

                //}
                //catch (Exception AnyError)
                //{ NetFramework.Console.WriteLine(AnyError.Message); NetFramework.Console.WriteLine(AnyError.StackTrace); }

                //try
                //{

                //    if (loadset.Thread_ChongQingShiShiCai == true)
                //    {
                //        Boolean TmpCheck = false;



                //        DownLoad163CaiPiaoV_kaijiangwang(ref TmpCheck, DateTime.Today, false, IsOpwnNow);




                //    }

                //}
                //catch (Exception AnyError)
                //{ NetFramework.Console.WriteLine(AnyError.Message); NetFramework.Console.WriteLine(AnyError.StackTrace); }

            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine(AnyError.Message, true);
                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
            }

        }

        private void DownloadResult_HeNeiWuFen(bool IsOpwnNow)
        {



            try
            {

                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;
                Linq.aspnet_UsersNewGameResultSend loadset = Util_Services.GetServicesSetting();


                try
                {

                    if (loadset.Thread_HeNeiWuFen == true)
                    {
                        Boolean TmpCheck = false;
                        DownLoad163CaiPiaoV_henei_188(ref TmpCheck, DateTime.Today, false, IsOpwnNow);
                        if (TmpCheck)
                        {
                            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.河内五分);
                            SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.河内五分);
                        }
                    }

                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine(AnyError.Message, true);
                    NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                }


            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine(AnyError.Message, true);
                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
            }

        }





        public void SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode FilterSubmode, string Mode = "All", string ToUserID = "")
        {
            if (RobotStop == true)
            {
                return;
            }
            NetFramework.Console.WriteLine(GlobalParam.UserName + "开始发送图片" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine, false);


            #region
            try
            {
                SendPicEnumWins(FilterSubmode);
            }
            catch (Exception AnyError)
            {

                NetFramework.Console.WriteLine(AnyError.Message, true);
                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
            }


            #endregion

            #region "有新的就通知,以及处理结果"

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            Linq.aspnet_UsersNewGameResultSend myconfig = Util_Services.GetServicesSetting();
            if (
                (DateTime.Now.Hour >= myconfig.SendImageStart && DateTime.Now.Hour <= myconfig.SendImageEnd)
                || (DateTime.Now.Hour >= myconfig.SendImageStart2 && DateTime.Now.Hour <= myconfig.SendImageEnd2)
                || (DateTime.Now.Hour >= myconfig.SendImageStart3 && DateTime.Now.Hour <= myconfig.SendImageEnd3)
                || (DateTime.Now.Hour >= myconfig.SendImageStart4 && DateTime.Now.Hour <= myconfig.SendImageEnd4)
                || (ToUserID != "")

                )
            {

                NetFramework.Console.WriteLine("正在发图" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine, false);

                // var users = db.WX_UserReply.Where(t => t.IsReply == true && t.aspnet_UserID == GlobalParam.Key);
                //筛选内存中勾了跟踪的
                var users = RunnerF.MemberSource.Select((ToUserID == "" ? "User_IsSendPic=1 " : " 1=1 ") + (ToUserID == "" ? "" : " and User_ContactTEMPID='" + ToUserID + "'"));
                foreach (var item in users)
                {

                    DataRow[] dr = RunnerF.MemberSource.Select("User_ContactTEMPID='" + item.Field<object>("User_ContactTEMPID").ToString() + "'");
                    if (dr.Length == 0)
                    {
                        continue;
                    }
                    if (Linq.ProgramLogic.GetMode(dr) != FilterSubmode)
                    {
                        continue;
                    }

                    string TEMPUserName = dr[0].Field<string>("User_ContactTEMPID");
                    string SourceType = dr[0].Field<string>("User_SourceType");
                    Linq.aspnet_UsersNewGameResultSend myset = Util_Services.GetServicesSetting();
                    if (!myset.IsSendPIC == true && ToUserID == "")
                    {
                        continue;
                    }
                    Linq.WX_WebSendPICSetting webpcset = db.WX_WebSendPICSetting.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                        && t.WX_SourceType == item.Field<object>("User_SourceType").ToString()
                         && t.WX_UserName == item.Field<object>("User_ContactID").ToString()
                        );
                    if (webpcset == null)
                    {
                        webpcset = new Linq.WX_WebSendPICSetting();

                        webpcset.aspnet_UserID = GlobalParam.UserKey;

                        webpcset.WX_SourceType = item.Field<object>("User_SourceType").ToString();
                        webpcset.WX_UserName = item.Field<object>("User_ContactID").ToString();

                        webpcset.ballinterval = 120;
                        webpcset.footballPIC = false;
                        webpcset.bassketballpic = false;
                        webpcset.balluclink = false;

                        webpcset.card = false;
                        webpcset.cardname = "";
                        webpcset.shishicailink = false;
                        webpcset.NumberPIC = false;
                        webpcset.dragonpic = false;
                        webpcset.numericlink = false;
                        webpcset.dragonlink = false;

                        webpcset.IsSendPIC = false;
                        webpcset.NiuNiuPic = false;
                        webpcset.NoBigSmallSingleDoublePIC = false;
                        webpcset.NumberDragonTxt = true;
                        webpcset.NumberPIC = false;
                        webpcset.dragonpic = false;
                        db.WX_WebSendPICSetting.InsertOnSubmit(webpcset);
                        db.SubmitChanges();

                    }
                    if (Linq.ProgramLogic.TimeInDuring(webpcset.PIC_StartHour, webpcset.PIC_StartMinute, webpcset.PIC_EndHour, webpcset.Pic_EndMinute) == false)
                    {
                        continue;
                    }
                    string ToSendGameName = (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), FilterSubmode));
                    //只发勾了的群或指定的人
                    //if ((dr[0].Field<string>("User_ContactType") == "群" && ToUserID == "") || (TEMPUserName == ToUserID))
                    if ((ToUserID == "") || (TEMPUserName == ToUserID))
                    {
                        if (webpcset.IsSendPIC == false)
                        {
                            continue;
                        }


                        if ((Mode == "All" && webpcset.dragonpic == true) || (Mode == "图2图") || (Mode == "龙虎图"))
                        {
                            if (dr[0].Field<string>("User_SourceType") == "易")
                            {
                                SendRobotContent(StartForm.ReadVirtualFile("Data3" + GlobalParam.UserName + "_" + ToSendGameName + ".txt", db), TEMPUserName, SourceType);
                            }
                            if (dr[0].Field<string>("User_SourceType") == "微")
                            {
                                SendRobotContent(StartForm.ReadVirtualFile("Data3_yixin" + GlobalParam.UserName + "_" + ToSendGameName + ".txt", db), TEMPUserName, SourceType);
                            }
                            webpcset.PreSendGameType = "龙虎";

                        }
                        Thread.Sleep(500);

                        if ((Mode == "All" && webpcset.NumberPIC == true) || (Mode == "图1图"))
                        {
                            SendRobotImageByte(StartForm.ReadVirtualFilebyte("Data" + GlobalParam.UserName + "_" + ToSendGameName + ".jpg", db), TEMPUserName, SourceType);
                            webpcset.PreSendGameType = "图1";
                        }

                        //if ((Mode == "All" && webpcset.NumberAndDragonPIC == true) || (Mode == "图3"))
                        //{
                        //    SendRobotImage(Application.StartupPath + "\\Data" + GlobalParam.UserName + "_v3.jpg", TEMPUserName, SourceType);
                        //}

                        if ((Mode == "All" && webpcset.NumberAndDragonPIC == true) || (Mode == "图4图"))
                        {
                            SendRobotContent(StartForm.ReadVirtualFile("Data数字龙虎" + GlobalParam.UserName + "_" + ToSendGameName + ".txt", db), TEMPUserName, SourceType);
                            webpcset.PreSendGameType = "图4";
                        }

                        if (Mode == "All" && webpcset.shishicailink == true)
                        {
                            SendRobotLink("查询开奖网地址", "https://h5.13322.com/kaijiang/ssc_cqssc_history_dtoday.html", TEMPUserName, SourceType);
                        }


                        if ((Mode == "All" && webpcset.NiuNiuPic == true) || (Mode == "牛牛图"))
                        {
                            if (dr[0].Field<string>("User_SourceType") == "易")
                            {
                                SendRobotContent(StartForm.ReadVirtualFile("Data数字龙虎dingding_五分龙虎Vr牛牛" + GlobalParam.UserName + "_" + ToSendGameName + ".txt", db), TEMPUserName, SourceType);
                            }
                            if (dr[0].Field<string>("User_SourceType") == "微")
                            {
                                SendRobotContent(StartForm.ReadVirtualFile("Data数字龙虎_五分龙虎Vr牛牛" + GlobalParam.UserName + "_" + ToSendGameName + ".txt", db), TEMPUserName, SourceType);
                            }
                            webpcset.PreSendGameType = "牛牛";
                        }
                        if ((Mode == "All" && webpcset.NoBigSmallSingleDoublePIC == true) || (Mode == "独龙虎图"))
                        {
                            if (dr[0].Field<string>("User_SourceType") == "易")
                            {
                                SendRobotContent(StartForm.ReadVirtualFile("ata数字龙虎dingding_五分龙虎" + GlobalParam.UserName + "_" + ToSendGameName + ".txt", db), TEMPUserName, SourceType);
                            }
                            if (dr[0].Field<string>("User_SourceType") == "微")
                            {
                                SendRobotContent(StartForm.ReadVirtualFile("Data数字龙虎_五分龙虎" + GlobalParam.UserName + "_" + ToSendGameName + ".txt", db), TEMPUserName, SourceType);
                            }
                            webpcset.PreSendGameType = "独龙虎";
                        }

                        if ((Mode == "All" && webpcset.NumberDragonTxt == true) || (Mode == "文本图"))
                        {
                            if (dr[0].Field<string>("User_SourceType") == "易")
                            {
                                SendRobotContent(StartForm.ReadVirtualFile("Data数字龙虎dingding" + GlobalParam.UserName + "_" + ToSendGameName + ".txt", db), TEMPUserName, SourceType);
                            }
                            if (dr[0].Field<string>("User_SourceType") == "微")
                            {
                                SendRobotContent(StartForm.ReadVirtualFile("Data数字龙虎" + GlobalParam.UserName + "_" + ToSendGameName + ".txt", db), TEMPUserName, SourceType);
                            }
                            webpcset.PreSendGameType = "文本";
                        }



                        db.SubmitChanges();








                    }//向监听的群或目标ID发送图片

                }//设置为自动监听的用户

            }//时间段范围的才发
            else
            {
                NetFramework.Console.WriteLine("不在发图时间段:" + DateTime.Now.Hour
               + (Object2Str(myconfig.SendImageStart) + "-" + Object2Str(myconfig.SendImageEnd))
               + "或" + (Object2Str(myconfig.SendImageStart2) + "-" + Object2Str(myconfig.SendImageEnd2))
                + "或" + (Object2Str(myconfig.SendImageStart3) + "-" + Object2Str(myconfig.SendImageEnd3))
                + "或" + (Object2Str(myconfig.SendImageStart4) + "-" + Object2Str(myconfig.SendImageEnd4))
                , true);
            }

            #endregion

            NetFramework.Console.WriteLine(GlobalParam.UserName + "发送图片完毕" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine, false);
        }

        private string Object2Str(object param)
        {
            if (param == null)
            {
                return "";
            }
            else
            {
                return param.ToString();
            }
        }

        private Int32 Object2Int(object param)
        {
            try
            {
                return Convert.ToInt32(param);
            }
            catch (Exception)
            {

                return 0;
            }
        }
        private Decimal Object2Decimal(object param)
        {
            try
            {
                return Convert.ToDecimal(param);
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public void DownLoad163CaiPiao_V163(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://caipiao.163.com/award/cqssc/20180413.html


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩)

                ).Count();




            string URL = "http://caipiao.163.com/award/cqssc/";


            URL += SelectDate.ToString("yyyyMMdd") + ".html";
            // NetFramework.Console.WriteLine("正在刷新163网页" + DateTime.Now.ToString("HH:mm:ss fff"));
            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie163);
            Regex FindTableData = new Regex("<table width=\"100%\" border=\"0\" cellspacing=\"0\" class=\"awardList\">((?!</table>)[\\S\\s])+</table>", RegexOptions.IgnoreCase);
            string str_json = FindTableData.Match(Result).Value;
            //<td class="start" data-win-number="5 3 9 7 3" data-period="180404002">
            //<td class="award-winNum">
            Regex FindPeriod = new Regex("<td class=\"start\"((?!</td>)[\\S\\s])+</td>", RegexOptions.IgnoreCase);

            DateTime PreTime = DateTime.Now;
            foreach (Match item in FindPeriod.Matches(str_json))
            {
                Regex FindWin = new Regex("data-win-number='((?!')[\\S\\s])+'", RegexOptions.IgnoreCase);
                Regex Finddataperiod = new Regex("data-period=\"((?!\")[\\S\\s])+\"", RegexOptions.IgnoreCase);

                Match dataperiod = Finddataperiod.Match(item.Value);
                Match Win = FindWin.Match(item.Value);

                if (Win.Value != "")
                {
                    string str_dataperiod = dataperiod.Value;
                    str_dataperiod = str_dataperiod.Substring(str_dataperiod.IndexOf("\"") + 1);
                    str_dataperiod = str_dataperiod.Substring(0, str_dataperiod.Length - 1);

                    string str_Win = Win.Value;
                    str_Win = str_Win.Substring(str_Win.IndexOf("'") + 1);
                    str_Win = str_Win.Substring(0, str_Win.Length - 1);



                    Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
                    //if (Newdb || IsOpenNow)
                    //{


                    //}


                }//已开奖励
            }//每行处理
            // NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------");
            //NetFramework.Console.WriteLine("163下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"));
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);

            //Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //    && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩)
            //    ).Count();
            //if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //{
            //    NewResult = true;

            //    DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            //}
            //if (LocalGameResultCount != AfterCheckCount)
            //{
            //    SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            //}


            #endregion

        }


        public void DownLoad163CaiPiaoV_zhcw(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {

            //http://m.zhcw.com/kaijiang/place_info.jsp?id=572

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://caipiao.163.com/award/cqssc/20180413.html


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩)

                ).Count();

            string URL = "http://m.zhcw.com/clienth5.do?czId=572&pageNo=1&pageSize=20&transactionType=300306&src=0000100001%7C6000003060";
            NetFramework.Console.WriteLine("正在刷新中彩网网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie163);

            //{"czname":"重庆时时彩","pageNo":"1","pageSize":"20","totalPage":"5549","dataList":[{"kjIssue":"20180508058","kjdate":"2018/05/08","kjznum":"4 2 6 5 1","kjtnum":"--"},{"kjIssue":"20180508057","kjdate":"2018/05/08","kjznum":"7 2 4 1 9","kjtnum":"--"},{"kjIssue":"20180508056","kjdate":"2018/05/08","kjznum":"5 7 1 7 0","kjtnum":"--"},{"kjIssue":"20180508055","kjdate":"2018/05/08","kjznum":"0 9 8 1 5","kjtnum":"--"},{"kjIssue":"20180508054","kjdate":"2018/05/08","kjznum":"9 0 4 3 8","kjtnum":"--"},{"kjIssue":"20180508053","kjdate":"2018/05/08","kjznum":"7 5 5 0 0","kjtnum":"--"},{"kjIssue":"20180508052","kjdate":"2018/05/08","kjznum":"2 7 6 3 6","kjtnum":"--"},{"kjIssue":"20180508051","kjdate":"2018/05/08","kjznum":"5 1 6 7 6","kjtnum":"--"},{"kjIssue":"20180508050","kjdate":"2018/05/08","kjznum":"8 3 3 7 8","kjtnum":"--"},{"kjIssue":"20180508049","kjdate":"2018/05/08","kjznum":"7 7 0 7 1","kjtnum":"--"},{"kjIssue":"20180508048","kjdate":"2018/05/08","kjznum":"2 5 2 0 5","kjtnum":"--"},{"kjIssue":"20180508047","kjdate":"2018/05/08","kjznum":"1 3 6 7 7","kjtnum":"--"},{"kjIssue":"20180508046","kjdate":"2018/05/08","kjznum":"6 5 2 8 2","kjtnum":"--"},{"kjIssue":"20180508045","kjdate":"2018/05/08","kjznum":"0 3 7 2 7","kjtnum":"--"},{"kjIssue":"20180508044","kjdate":"2018/05/08","kjznum":"8 0 4 8 2","kjtnum":"--"},{"kjIssue":"20180508043","kjdate":"2018/05/08","kjznum":"2 8 6 5 5","kjtnum":"--"},{"kjIssue":"20180508042","kjdate":"2018/05/08","kjznum":"2 1 9 4 4","kjtnum":"--"},{"kjIssue":"20180508041","kjdate":"2018/05/08","kjznum":"8 2 3 4 3","kjtnum":"--"},{"kjIssue":"20180508040","kjdate":"2018/05/08","kjznum":"6 8 9 5 7","kjtnum":"--"},{"kjIssue":"20180508039","kjdate":"2018/05/08","kjznum":"4 8 9 6 1","kjtnum":"--"}]}



            DateTime PreTime = DateTime.Now;

            JArray Periods = JObject.Parse(Result)["dataList"] as JArray;


            foreach (JObject item in Periods)
            {

                string str_dataperiod = (item["kjIssue"] as JValue).Value.ToString();
                str_dataperiod = str_dataperiod.Substring(2);


                string str_Win = (item["kjznum"] as JValue).Value.ToString(); ;

                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
                //if (Newdb || IsOpenNow)
                //{


                //}

            }//每行处理
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("中彩网下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩)
                ).Count();
            //if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //{
            //    NewResult = true;


            //    DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            //}
            //if (LocalGameResultCount != AfterCheckCount)
            //{
            //    SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            //}
            #endregion

        }

        public void DownLoad163CaiPiaoV_500(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://m.500.com/info/kaijiang/ssc/2018-05-11.shtml


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩)

                ).Count();




            string URL = "http://m.500.com/info/kaijiang/ssc/";


            URL += SelectDate.ToString("yyyy-MM-dd") + ".shtml";
            NetFramework.Console.WriteLine("正在刷新500网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie163);
            Regex FindTableData = new Regex("<div class=\"lcbqc-info-tb\">((?!</div>)[\\S\\s])+</div>", RegexOptions.IgnoreCase);
            string str_json = FindTableData.Match(Result).Value;
            //<td class="start" data-win-number="5 3 9 7 3" data-period="180404002">
            //<td class="award-winNum">
            Regex FindPeriod = new Regex("<ul class=\"l-flex-row\">((?!</ul>)[\\S\\s])+</ul>", RegexOptions.IgnoreCase);
            DateTime PreTime = DateTime.Now;

            foreach (Match item in FindPeriod.Matches(str_json))
            {
                Regex li = new Regex("<li((?!</li>)[\\S\\s])+<", RegexOptions.IgnoreCase);

                Match dataperiod = li.Matches(item.Value)[0];
                Match Win = li.Matches(item.Value)[2];

                if (Win.Value != "")
                {
                    string str_dataperiod = dataperiod.Value;
                    str_dataperiod = str_dataperiod.Substring(str_dataperiod.IndexOf(">") + 1);
                    str_dataperiod = str_dataperiod.Substring(0, str_dataperiod.Length - 1);

                    string str_Win = Win.Value;
                    str_Win = str_Win.Substring(str_Win.IndexOf(">") + 1);
                    str_Win = str_Win.Substring(0, str_Win.Length - 1);
                    str_Win = str_Win.Replace(",", " ").Replace("\t", "").Replace("\r\n", "");

                    try
                    {
                        Convert.ToInt64(str_dataperiod);
                    }
                    catch (Exception)
                    {
                        continue;

                    }

                    Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod.Substring(2), ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
                    //if (Newdb || IsOpenNow)
                    //{

                    //}


                }//已开奖励
            }//每行处理
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("500彩票网下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩)
).Count();
            //if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //{
            //    NewResult = true;


            //    DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            //}
            //if (LocalGameResultCount != AfterCheckCount)
            //{
            //    SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            //}


            #endregion

        }
        //http://pay4.hbcchy.com/lotterytrend/getsscchart
        public void DownLoad163CaiPiaoV_tengxunshifen(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://pay4.hbcchy.com/lotterytrend/getsscchart


            //            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾讯十分)
            //).Count();




            string URL = "http://pay4.hbcchy.com/lotterytrend/getsscchart";

            NetFramework.Console.WriteLine("正在刷新腾讯十分网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);

            string Result = NetFramework.Util_WEB.OpenUrl(URL, "http://pay4.hbcchy.com/lotterytrend/chart/19/", "id=19&pnum=30", "POST", wufencai, true, true, "application/x-www-form-urlencoded; charset=UTF-8");
            DateTime PreTime = DateTime.Now;
            JObject jresult = JObject.Parse(Result);



            JArray periods = (JArray)jresult["data"];
            Int32 FullCount = 0;
            foreach (JToken trdata in periods.Reverse())
            {

                string Oldstr_Win = NetFramework.Util_WEB.CleanHtml(
                    ((JArray)trdata)[1].ToString()
                    );

                string str_dataperiod = NetFramework.Util_WEB.CleanHtml(
                     ((JArray)trdata)[0].ToString()

                    );
                Oldstr_Win = Oldstr_Win.Replace(Environment.NewLine, ",");
                string str_Win = "";
                if (Oldstr_Win.Length >= 5)
                {
                    str_Win = Oldstr_Win.Substring(0, 1) + ","
                        + Oldstr_Win.Substring(1, 1) + ","
                         + Oldstr_Win.Substring(2, 1) + ","
                          + Oldstr_Win.Substring(3, 1) + ","
                           + Oldstr_Win.Substring(4, 1) + ",";
                }
                str_Win = str_Win.Replace(" ", "").Replace("\t", "").Replace(",", " ");
                str_Win = str_Win.Substring(0, 9);
                //str_dataperiod = str_dataperiod.Substring(2, 9);

                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);

            }
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("腾讯十分下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);



            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾讯十分)
            //).Count();
            //if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //{
            //    NewResult = true;


            //    DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);

            //}
            //if (LocalGameResultCount != AfterCheckCount)
            //{
            //    SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);
            //}


            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }

        //http://gf4.006632.com:90/Result/GetLotteryResultList?gameID=246&pageSize=288&pageIndex=1&_=1557140016391
        public void DownLoad163CaiPiaoV_tengxunshifen_xin_gf4(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://pay4.hbcchy.com/lotterytrend/getsscchart


            //            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾十信)
            //).Count();




            string URL = "http://gf4.006632.com:90/Result/GetLotteryResultList?gameID=246&pageSize=288&pageIndex=1&_=1557140016391";

            NetFramework.Console.WriteLine("正在刷新腾讯十分网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);

            string Result = NetFramework.Util_WEB.OpenUrl(URL, "http://gf4.006632.com:90/Result/Index?game=246", "", "GET", wufencai, true, true, "application/x-www-form-urlencoded; charset=UTF-8");
            DateTime PreTime = DateTime.Now;
            JObject jresult = JObject.Parse(Result);



            JArray periods = (JArray)jresult["list"];
            Int32 FullCount = 0;
            foreach (JToken trdata in periods)
            {

                string Oldstr_Win = NetFramework.Util_WEB.CleanHtml(
                    ((JObject)trdata)["result"].ToString()
                    );

                string str_dataperiod = NetFramework.Util_WEB.CleanHtml(
                     ((JObject)trdata)["period"].ToString()

                    );
                string str_Win = Oldstr_Win.Replace(Environment.NewLine, ",");

                str_Win = str_Win.Replace(" ", "").Replace("\t", "").Replace(",", "");

                //str_dataperiod = str_dataperiod.Substring(2, 9);
                string GameTime = ((JObject)trdata)["date"].ToString();
                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.腾十信, GameTime);

            }
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("腾讯十分信下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.腾十信);



            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾十信)
            //).Count();
            //if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //{
            //    NewResult = true;


            //    DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);

            //}
            //if (LocalGameResultCount != AfterCheckCount)
            //{
            //    SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);
            //}


            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }

        public void DownLoad163CaiPiaoV_tengxunshifen_gf4(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://pay4.hbcchy.com/lotterytrend/getsscchart


            //            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾十信)
            //).Count();




            string URL = "http://gf4.006632.com:90/Result/GetLotteryResultList?gameID=247&pageSize=288&pageIndex=1&_=1557140016391";

            NetFramework.Console.WriteLine("正在刷新腾讯十分网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);

            string Result = NetFramework.Util_WEB.OpenUrl(URL, "http://gf4.006632.com:90/Result/Index?game=247", "", "GET", wufencai, true, true, "application/x-www-form-urlencoded; charset=UTF-8");
            DateTime PreTime = DateTime.Now;
            JObject jresult = JObject.Parse(Result);



            JArray periods = (JArray)jresult["list"];
            Int32 FullCount = 0;
            foreach (JToken trdata in periods)
            {

                string Oldstr_Win = NetFramework.Util_WEB.CleanHtml(
                    ((JObject)trdata)["result"].ToString()
                    );

                string str_dataperiod = NetFramework.Util_WEB.CleanHtml(
                     ((JObject)trdata)["period"].ToString()

                    );
                string str_Win = Oldstr_Win.Replace(Environment.NewLine, ",");

                str_Win = str_Win.Replace(" ", "").Replace("\t", "").Replace(",", "");

                //str_dataperiod = str_dataperiod.Substring(2, 9);
                string GameTime = ((JObject)trdata)["date"].ToString();
                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.腾讯十分, GameTime);

            }
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("腾讯十分信下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);



            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾十信)
            //).Count();
            //if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //{
            //    NewResult = true;


            //    DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);

            //}
            //if (LocalGameResultCount != AfterCheckCount)
            //{
            //    SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);
            //}


            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }


        public void DownLoad163CaiPiaoV_tengxunshifen_xin_188(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://pay4.hbcchy.com/lotterytrend/getsscchart


            //            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾十信)
            //).Count();
            //            //http://gf4.006632.com:90/Result/Index?game=244


            string URL = "http://www.188kaijiang.wang/api.php?param=CQShiCai/getBaseCQShiCaiList.do?date=&lotCode=txsfc";
            //string URL = "http://www.188kaijiang.wang/api.php?param=CQShiCai/getBaseCQShiCaiList.do?date=&lotCode=txsfc";

            NetFramework.Console.WriteLine("正在刷新腾讯十分信188网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);

            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", wufencai, true, true, "application/x-www-form-urlencoded; charset=UTF-8");
            DateTime PreTime = DateTime.Now;
            JObject jresult = JObject.Parse(Result);


            Int32 FullCount = 0;
            JArray periods = (JArray)jresult["result"]["data"];
            foreach (JToken trdata in periods)
            {

                string str_Win = trdata["preDrawCode"].ToString();

                string str_dataperiod = trdata["preDrawIssue"].ToString();

                str_Win = str_Win.Replace(" ", "").Replace("\t", "").Replace(",", "");
                // str_Win = str_Win.Substring(0, 9);

                string GameTime = trdata["preDrawTime"].ToString();
                //str_dataperiod = str_dataperiod.Substring(2, 9);

                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.腾十信, GameTime);

            }
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("腾讯十分信188下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.腾十信);


            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾讯五分)
            //).Count();
            //            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //            {
            //                NewResult = true;


            //                DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾讯五分);

            //            }

            //            if (LocalGameResultCount != AfterCheckCount)
            //            {
            //                SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.腾讯五分);
            //            }

            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }


        public void DownLoad163CaiPiaoV_henei_188(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://pay4.hbcchy.com/lotterytrend/getsscchart


            //            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾十信)
            //).Count();
            //            //http://gf4.006632.com:90/Result/Index?game=244


            string URL = "http://www.188kaijiang.wang/api.php?param=CQShiCai/getBaseCQShiCaiList.do?date=&lotCode=hn5fc";
            //string URL = "http://www.188kaijiang.wang/api.php?param=CQShiCai/getBaseCQShiCaiList.do?date=&lotCode=hn5fc";

            NetFramework.Console.WriteLine("正在刷新河内五分网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);

            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", wufencai, true, true, "application/x-www-form-urlencoded; charset=UTF-8");
            DateTime PreTime = DateTime.Now;
            JObject jresult = JObject.Parse(Result);


            Int32 FullCount = 0;
            JArray periods = (JArray)jresult["result"]["data"];
            foreach (JToken trdata in periods)
            {

                string str_Win = trdata["preDrawCode"].ToString();

                string str_dataperiod = trdata["preDrawIssue"].ToString();

                str_Win = str_Win.Replace(" ", "").Replace("\t", "").Replace(",", "");
                // str_Win = str_Win.Substring(0, 9);

                string GameTime = trdata["preDrawTime"].ToString();
                str_dataperiod = str_dataperiod.Substring(0, 8) + str_dataperiod.Substring(9, 3);


                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.河内五分, GameTime);

            }
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("腾讯河内五分下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.河内五分);


            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾讯五分)
            //).Count();
            //            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //            {
            //                NewResult = true;


            //                DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾讯五分);

            //            }

            //            if (LocalGameResultCount != AfterCheckCount)
            //            {
            //                SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.腾讯五分);
            //            }

            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }


        //https://api.honze88.com/api/v1/lotteries/17/issuos
        public void DownLoad163CaiPiaoV_tengxunwufenbyhuayu(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //https://api.honze88.com/api/v1/lotteries/17/issuos


            //            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾讯五分)
            //).Count();



            string URL = "https://api.honze88.com/api/v1/lotteries/17/issuos";
            //string URL = "http://www.188kaijiang.wang/api.php?param=CQShiCai/getBaseCQShiCaiList.do?date=&lotCode=tx5fc";

            NetFramework.Console.WriteLine("正在刷新腾讯五分网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);

            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", HuaYuCookie, true, true, "application/x-www-form-urlencoded; charset=UTF-8"
                   , "JWT " + HuaYuToken);
            //JObject jr = JObject.Parse(Result);
            DateTime PreTime = DateTime.Now;
            //JObject jresult = JObject.Parse(Result);


            Int32 FullCount = 0;
            JArray periods = JArray.Parse(Result);
            foreach (JToken trdata in periods)
            {

                string str_Win = trdata["opencode"].ToString();
                if (str_Win == "")
                {
                    continue;
                }
                string str_dataperiod = trdata["issuo"].ToString();

                str_Win = str_Win.Replace(" ", "").Replace("\t", "").Replace(",", " ");
                //str_Win = str_Win.Substring(0, 9);

                string GameTime = trdata["openedAt"].ToString();
                str_dataperiod = str_dataperiod.Replace("-", "");
                //20190328286
                str_dataperiod = str_dataperiod.Substring(0, 8) + str_dataperiod.Substring(9, 3);

                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.腾讯五分, GameTime);

            }
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("腾讯五分下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.腾讯五分);


            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾讯五分)
            //).Count();
            //if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //{
            //    NewResult = true;


            //    DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾讯五分);

            //}

            //if (LocalGameResultCount != AfterCheckCount)
            //{
            //    SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.腾讯五分);
            //}

            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }
        CookieCollection HuaYuCookie = new CookieCollection();
        public void DownLoad163CaiPiaoV_tengxunwufenbyhuayulishi(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //https://api.honze88.com/api/v1/lotteries/17/issuos
            //https://api.honze88.com/api/v1/lotteries/17/opencodes?limit=288

            //            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾讯五分)
            //).Count();



            //string URL = "https://api.honze88.com/api/v1/lotteries/17/opencodes?limit=50";
            string URL = "https://api.honze88.com/api/v1/lotteries/17/issuos";
            // string URL = "http://www.188kaijiang.wang/api.php?param=CQShiCai/getBaseCQShiCaiList.do?date=&lotCode=tx5fc";

            NetFramework.Console.WriteLine("正在刷新腾讯五分网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);

            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", HuaYuCookie, true, true, "application/x-www-form-urlencoded; charset=UTF-8"
                   , "JWT " + HuaYuToken);

            //JObject jr = JObject.Parse(Result);
            DateTime PreTime = DateTime.Now;
            //JObject jresult = JObject.Parse(Result);


            Int32 FullCount = 0;
            JArray periods = JArray.Parse(Result);
            foreach (JToken trdata in periods)
            {

                string str_Win = trdata["opencode"].ToString();
                if (str_Win == "")
                {
                    continue;
                }
                string str_dataperiod = trdata["issuo"].ToString();

                str_Win = str_Win.Replace(" ", "").Replace("\t", "").Replace(",", " ");
                //str_Win = str_Win.Substring(0, 9);


                str_dataperiod = str_dataperiod.Replace("-", "");
                //20190328286
                str_dataperiod = str_dataperiod.Substring(0, 8) + str_dataperiod.Substring(9, 3);

                string GameTime = trdata["openedAt"].ToString();
                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.腾讯五分, GameTime);

            }
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("腾讯五分下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.腾讯五分);


            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾讯五分)
).Count();
            //if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //{
            //    NewResult = true;


            //    DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾讯五分);

            //}

            //if (LocalGameResultCount != AfterCheckCount)
            //{
            //    SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.腾讯五分);
            //}

            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }


        public void DownLoad163CaiPiaoV_tengxunwufen_xin_188(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://pay4.hbcchy.com/lotterytrend/getsscchart


            //            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾五信)
            //).Count();
            //http://gf4.006632.com:90/Result/Index?game=244


            string URL = "http://www.188kaijiang.wang/api.php?param=CQShiCai/getBaseCQShiCaiList.do?date=&lotCode=tx5fc";
            //string URL = "http://www.188kaijiang.wang/api.php?param=CQShiCai/getBaseCQShiCaiList.do?date=&lotCode=tx5fc";

            NetFramework.Console.WriteLine("正在刷新腾讯五分网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);

            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", wufencai, true, true, "application/x-www-form-urlencoded; charset=UTF-8");
            DateTime PreTime = DateTime.Now;
            JObject jresult = JObject.Parse(Result);


            Int32 FullCount = 0;
            JArray periods = (JArray)jresult["result"]["data"];
            foreach (JToken trdata in periods)
            {

                string str_Win = trdata["preDrawCode"].ToString();

                string str_dataperiod = trdata["preDrawIssue"].ToString();

                str_Win = str_Win.Replace(" ", "").Replace("\t", "").Replace(",", "");
                // str_Win = str_Win.Substring(0, 9);

                string GameTime = trdata["preDrawTime"].ToString();
                //str_dataperiod = str_dataperiod.Substring(2, 9);

                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.腾五信, GameTime);

            }
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("腾讯五分188下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.腾五信);


            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾讯五分)
            //).Count();
            //            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //            {
            //                NewResult = true;


            //                DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾讯五分);

            //            }

            //            if (LocalGameResultCount != AfterCheckCount)
            //            {
            //                SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.腾讯五分);
            //            }

            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }

        public void DownLoad163CaiPiaoV_tengxunwufen_xin_gf4(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://pay4.hbcchy.com/lotterytrend/getsscchart


            //            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾五信)
            //).Count();




            string URL = "http://gf4.006632.com:90/Result/GetLotteryResultList?gameID=244&pageSize=288&pageIndex=1&_=1557140016391";

            NetFramework.Console.WriteLine("正在刷新腾五信网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);

            string Result = NetFramework.Util_WEB.OpenUrl(URL, "http://gf4.006632.com:90/Result/Index?game=244", "", "GET", wufencai, true, true, "application/x-www-form-urlencoded; charset=UTF-8");
            DateTime PreTime = DateTime.Now;
            JObject jresult = JObject.Parse(Result);



            JArray periods = (JArray)jresult["list"];
            Int32 FullCount = 0;
            foreach (JToken trdata in periods)
            {

                string Oldstr_Win = NetFramework.Util_WEB.CleanHtml(
                    ((JObject)trdata)["result"].ToString()
                    );

                string str_dataperiod = NetFramework.Util_WEB.CleanHtml(
                     ((JObject)trdata)["period"].ToString()

                    );
                string str_Win = Oldstr_Win.Replace(Environment.NewLine, ",");

                str_Win = str_Win.Replace(" ", "").Replace("\t", "").Replace(",", "");

                //str_dataperiod = str_dataperiod.Substring(2, 9);
                string GameTime = ((JObject)trdata)["date"].ToString();
                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.腾五信, GameTime);

            }
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("腾五信下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.腾五信);



            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾五信)
            //).Count();
            //if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //{
            //    NewResult = true;


            //    DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);

            //}
            //if (LocalGameResultCount != AfterCheckCount)
            //{
            //    SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);
            //}


            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }

        public void DownLoad163CaiPiaoV_tengxunwufen_gf4(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://pay4.hbcchy.com/lotterytrend/getsscchart


            //            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾五信)
            //).Count();




            string URL = "http://gf4.006632.com:90/Result/GetLotteryResultList?gameID=245&pageSize=288&pageIndex=1&_=1557140016391";

            NetFramework.Console.WriteLine("正在刷新腾五信网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);

            string Result = NetFramework.Util_WEB.OpenUrl(URL, "http://gf4.006632.com:90/Result/Index?game=245", "", "GET", wufencai, true, true, "application/x-www-form-urlencoded; charset=UTF-8");
            DateTime PreTime = DateTime.Now;
            JObject jresult = JObject.Parse(Result);



            JArray periods = (JArray)jresult["list"];
            Int32 FullCount = 0;
            foreach (JToken trdata in periods)
            {

                string Oldstr_Win = NetFramework.Util_WEB.CleanHtml(
                    ((JObject)trdata)["result"].ToString()
                    );

                string str_dataperiod = NetFramework.Util_WEB.CleanHtml(
                     ((JObject)trdata)["period"].ToString()

                    );
                string str_Win = Oldstr_Win.Replace(Environment.NewLine, ",");

                str_Win = str_Win.Replace(" ", "").Replace("\t", "").Replace(",", "");

                //str_dataperiod = str_dataperiod.Substring(2, 9);
                string GameTime = ((JObject)trdata)["date"].ToString();
                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.腾讯五分, GameTime);

            }
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("腾五信下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.腾讯五分);



            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾五信)
            //).Count();
            //if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //{
            //    NewResult = true;


            //    DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);

            //}
            //if (LocalGameResultCount != AfterCheckCount)
            //{
            //    SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);
            //}


            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }



        //http://www.188kaijiang.wang/api.php?param=CQShiCai/getBaseCQShiCai.do?issue=&lotCode=tx5fc
        public void DownLoad163CaiPiaoV_beijingsaichepk10(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://pay4.hbcchy.com/lotterytrend/getsscchart


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾讯十分)
).Count();




            string URL = "http://pay4.hbcchy.com/lotterytrend/getsscchart";

            NetFramework.Console.WriteLine("正在刷新腾讯十分网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);

            string Result = NetFramework.Util_WEB.OpenUrl(URL, "http://pay4.hbcchy.com/lotterytrend/chart/19/", "id=19&pnum=30", "POST", wufencai, true, true, "application/x-www-form-urlencoded; charset=UTF-8");
            DateTime PreTime = DateTime.Now;
            JObject jresult = JObject.Parse(Result);



            JArray periods = (JArray)jresult["data"];
            foreach (JToken trdata in periods)
            {

                string Oldstr_Win = NetFramework.Util_WEB.CleanHtml(
                    ((JArray)trdata)[1].ToString()
                    );

                string str_dataperiod = NetFramework.Util_WEB.CleanHtml(
                     ((JArray)trdata)[0].ToString()

                    );
                Oldstr_Win = Oldstr_Win.Replace(Environment.NewLine, ",");
                string str_Win = "";
                if (Oldstr_Win.Length >= 5)
                {
                    str_Win = Oldstr_Win.Substring(0, 1) + ","
                        + Oldstr_Win.Substring(1, 1) + ","
                         + Oldstr_Win.Substring(2, 1) + ","
                          + Oldstr_Win.Substring(3, 1) + ","
                           + Oldstr_Win.Substring(4, 1) + ",";
                }
                str_Win = str_Win.Replace(" ", "").Replace("\t", "").Replace(",", " ");
                str_Win = str_Win.Substring(0, 9);
                //str_dataperiod = str_dataperiod.Substring(2, 9);

                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);
                //if (Newdb || IsOpenNow)
                //{

                //}
            }
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("腾讯十分下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);


            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.腾讯十分)
            //).Count();
            //            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //            {
            //                NewResult = true;


            //                DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);

            //            }

            //            if (LocalGameResultCount != AfterCheckCount)
            //            {
            //                SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);
            //            }

            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }


        CookieCollection cc_vrchongqing = new CookieCollection();
        public void DownLoad163CaiPiaoV_vrchongqingcai(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://pay4.hbcchy.com/lotterytrend/getsscchart


            Int32 LocalGameResultCount = db.Game_Result.Where(t =>
                t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩)
).Count();




            string URL = "http://numbers.videoracing.com/open_13_2.aspx";

            NetFramework.Console.WriteLine("正在刷新VR重庆时时彩网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);

            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", wufencai, true, true, "application/x-www-form-urlencoded; charset=UTF-8");

            // "<div class="css_table">"

            DateTime PreTime = DateTime.Now;

            Regex Finddiv = new Regex("<div class=\"css_table\"[^>]*>((?<mm><div[^>]*>)+|(?<-mm></div>)|[\\s\\S])*?(?(mm)(?!))</div>", RegexOptions.IgnoreCase);

            Regex FindRow = new Regex("<div class=\"css_tr[^>]*>((?<mm><div[^>]*>)+|(?<-mm></div>)|[\\s\\S])*?(?(mm)(?!))</div>", RegexOptions.IgnoreCase);

            Regex Findcell = new Regex("<div class=\"css_td[^>]*>((?<mm><div[^>]*>)+|(?<-mm></div>)|[\\s\\S])*?(?(mm)(?!))</div>", RegexOptions.IgnoreCase);

            Int32 FullCount = 0;
            MatchCollection Divs = Finddiv.Matches(Result);
            foreach (Match divdata in Divs)
            {
                MatchCollection trs = FindRow.Matches(divdata.Value.Replace("'", "\""));
                foreach (Match tritem in trs)
                #region 每期处理
                {
                    MatchCollection tds = Findcell.Matches(tritem.Value);
                    if (NetFramework.Util_WEB.CleanHtml(
                         tds[0].Value

                        ) == "开奖日期")
                    {
                        continue;
                    }
                    string Oldstr_Win =
                         NetFramework.Util_WEB.CleanHtml(
                         tds[2].Value
                        )// + ","
                        +
                         NetFramework.Util_WEB.CleanHtml(
                         tds[3].Value

                        ) //+ ","
                         +
                         NetFramework.Util_WEB.CleanHtml(
                         tds[4].Value

                        ) //+ ","
                         +
                         NetFramework.Util_WEB.CleanHtml(
                         tds[5].Value

                        ) //+ ","
                         +
                         NetFramework.Util_WEB.CleanHtml(
                         tds[6].Value

                        );


                    string str_dataperiod =
                         NetFramework.Util_WEB.CleanHtml(
                         tds[0].Value

                        ).Replace("/", "") +
                        NetFramework.Util_WEB.CleanHtml(
                         tds[1].Value

                        );
                    Oldstr_Win = Oldstr_Win.Replace(Environment.NewLine, ",");
                    string str_Win = Oldstr_Win;
                    //if (Oldstr_Win.Length >= 5)
                    //{
                    //    str_Win = Oldstr_Win.Substring(0, 1) + ","
                    //        + Oldstr_Win.Substring(1, 1) + ","
                    //         + Oldstr_Win.Substring(2, 1) + ","
                    //          + Oldstr_Win.Substring(3, 1) + ","
                    //           + Oldstr_Win.Substring(4, 1) + ",";
                    //}
                    //str_Win = str_Win.Replace(" ", "").Replace("\t", "").Replace(",", " ");
                    ////str_Win = str_Win.Substring(0, 9);
                    //str_dataperiod = str_dataperiod.Substring(2, 9);

                    Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);

                }
                #endregion
            }
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("VR重庆时时彩下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);


            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩)
            //).Count();
            //            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //            {
            //                NewResult = true;


            //                DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);

            //            }

            //            if (LocalGameResultCount != AfterCheckCount)
            //            {
            //                SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);
            //            }

            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }
        public void DownLoad163CaiPiaoV_vrchongqingcaislim(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://pay4.hbcchy.com/lotterytrend/getsscchart


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩)
).Count();




            string URL = "http://numbers.videoracing.com/open_13_1.aspx";

            NetFramework.Console.WriteLine("正在刷新VR重庆时时彩网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);

            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", wufencai, true, true, "application/x-www-form-urlencoded; charset=UTF-8");

            // "<div class="css_table">"

            DateTime PreTime = DateTime.Now;

            Regex FindPeriod = new Regex("<div class=\"lottimenum\"[^>]*>((?<mm><div[^>]*>)+|(?<-mm></div>)|[\\s\\S])*?(?(mm)(?!))</div>", RegexOptions.IgnoreCase);

            Regex FindRow = new Regex("<div class=\"mainnums\"((?<mm><div[^>]*>)+|(?<-mm></div>)|[\\s\\S])*?(?(mm)(?!))</div>", RegexOptions.IgnoreCase);

            Regex Findli = new Regex("<li((?<mm><li[^>]*>)+|(?<-mm></li>)|[\\s\\S])*?(?(mm)(?!))</li>", RegexOptions.IgnoreCase);


            Match Divs = FindPeriod.Match(Result);
            Int32 FullCount = 0;
            {
                Match trs = FindRow.Match(Result);
                Match lidata = Findli.Match(trs.Value);
                #region 每期处理
                {


                    string Oldstr_Win =
                         NetFramework.Util_WEB.CleanHtml(
                        trs.Value
                        );
                    Oldstr_Win = Oldstr_Win.Replace("当期奖号", "").Replace(Environment.NewLine, "").Replace("\t", "").Replace(" ", "");

                    string str_dataperiod =
                         NetFramework.Util_WEB.CleanHtml(
                        Divs.Value

                        );
                    str_dataperiod = str_dataperiod.Replace("第", "").Replace("期号码", "");
                    Oldstr_Win = Oldstr_Win.Replace(Environment.NewLine, ",");
                    string str_Win = Oldstr_Win;
                    //if (Oldstr_Win.Length >= 5)
                    //{
                    //    str_Win = Oldstr_Win.Substring(0, 1) + ","
                    //        + Oldstr_Win.Substring(1, 1) + ","
                    //         + Oldstr_Win.Substring(2, 1) + ","
                    //          + Oldstr_Win.Substring(3, 1) + ","
                    //           + Oldstr_Win.Substring(4, 1) + ",";
                    //}
                    //str_Win = str_Win.Replace(" ", "").Replace("\t", "").Replace(",", " ");
                    ////str_Win = str_Win.Substring(0, 9);
                    //str_dataperiod = str_dataperiod.Substring(2, 9);

                    Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);

                }
                #endregion
            }
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("VR重庆时时彩下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);


            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩)
            //).Count();
            //            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //            {
            //                NewResult = true;


            //                DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);

            //            }

            //            if (LocalGameResultCount != AfterCheckCount)
            //            {
            //                SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);
            //            }

            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }


        //      //WebRequest LoginPage = HttpWebRequest.Create("https://api.honze88.com/api/v1/lotteries/4/issuos");
        //    "JWT eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MzQxMjA4LCJuYW1lIjoieGwxMjM0NTYiLCJpYXQiOjE1NTM5MTkyODYsImV4cCI6MTU1NTEyODg4Nn0.hM6rvGXgkIPZjqef18hvld550zYLzSWX0nyTX6f9AII"
        CookieCollection appcc = new CookieCollection();
        public void DownLoad163CaiPiaoV_vrchongqingcaislimapp(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://pay4.hbcchy.com/lotterytrend/getsscchart


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩)
).Count();

            // http://huy.vrbetapi.com//Account/LoginValidate?id=HUY&version=1.0&data=lVrOheZew4NAFwFkJmkk6HTItGzRs4S1jzGjGJy%2BSlwUkFsZeDyb%2BtJQGJrYcktDAcsWPtsUvFj2q4JVMzlOmh4IB7Buvhxjty1JuwpgUW1GGh1ug2a9RHK84d1upuA%2F 


            string URL = "https://api.honze88.com/api/v1/lotteries/42/issuos";



            NetFramework.Console.WriteLine("正在刷新VR重庆时时彩网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);

            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", wufencai, true, true, "application/x-www-form-urlencoded; charset=UTF-8"
                , "JWT eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MzQxMjA4LCJuYW1lIjoieGwxMjM0NTYiLCJpYXQiOjE1NTM5MTkyODYsImV4cCI6MTU1NTEyODg4Nn0.hM6rvGXgkIPZjqef18hvld550zYLzSWX0nyTX6f9AII");

            // "<div class="css_table">"

            DateTime PreTime = DateTime.Now;

            Regex FindPeriod = new Regex("<div class=\"lottimenum\"[^>]*>((?<mm><div[^>]*>)+|(?<-mm></div>)|[\\s\\S])*?(?(mm)(?!))</div>", RegexOptions.IgnoreCase);

            Regex FindRow = new Regex("<div class=\"mainnums\"((?<mm><div[^>]*>)+|(?<-mm></div>)|[\\s\\S])*?(?(mm)(?!))</div>", RegexOptions.IgnoreCase);

            Regex Findli = new Regex("<li((?<mm><li[^>]*>)+|(?<-mm></li>)|[\\s\\S])*?(?(mm)(?!))</li>", RegexOptions.IgnoreCase);


            Match Divs = FindPeriod.Match(Result);
            Int32 FullCount = 0;
            {
                Match trs = FindRow.Match(Result);
                Match lidata = Findli.Match(trs.Value);
                #region 每期处理
                {


                    string Oldstr_Win =
                         NetFramework.Util_WEB.CleanHtml(
                        trs.Value
                        );
                    Oldstr_Win = Oldstr_Win.Replace("当期奖号", "").Replace(Environment.NewLine, "").Replace("\t", "").Replace(" ", "");

                    string str_dataperiod =
                         NetFramework.Util_WEB.CleanHtml(
                        Divs.Value

                        );
                    str_dataperiod = str_dataperiod.Replace("第", "").Replace("期号码", "");
                    Oldstr_Win = Oldstr_Win.Replace(Environment.NewLine, ",");
                    string str_Win = Oldstr_Win;
                    //if (Oldstr_Win.Length >= 5)
                    //{
                    //    str_Win = Oldstr_Win.Substring(0, 1) + ","
                    //        + Oldstr_Win.Substring(1, 1) + ","
                    //         + Oldstr_Win.Substring(2, 1) + ","
                    //          + Oldstr_Win.Substring(3, 1) + ","
                    //           + Oldstr_Win.Substring(4, 1) + ",";
                    //}
                    //str_Win = str_Win.Replace(" ", "").Replace("\t", "").Replace(",", " ");
                    ////str_Win = str_Win.Substring(0, 9);
                    //str_dataperiod = str_dataperiod.Substring(2, 9);

                    Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);

                }
                #endregion
            }
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("VR重庆时时彩下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);


            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩)
            //).Count();
            //            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //            {
            //                NewResult = true;


            //                DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);

            //            }

            //            if (LocalGameResultCount != AfterCheckCount)
            //            {
            //                SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);
            //            }

            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }



        //        public void DownLoad163CaiPiaoV_vrchongqingcaislimWebDOM(ref Boolean NewResult, bool ReDrawGdi)
        //        {
        //            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
        //            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
        //            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
        //               && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩)
        //).Count();
        //            DateTime PreTime = DateTime.Now;

        //            this.Invoke(new Action(() =>
        //            {
        //                if (wb_vrchongqing == null)
        //                {
        //                    return;
        //                }
        //                if (wb_vrchongqing.Url == null || wb_vrchongqing.Url.AbsoluteUri.Contains("Bet/Index/42") == false)
        //                {
        //                    NetFramework.Console.WriteLine("未登陆不能使用WEB采集", true);
        //                    return;
        //                }


        //            }));
        //            //Regex FindPeriod = new Regex("<span((?!>)[\\s\\S])+id=\"lastissue\"((?!</span>)[\\s\\S])+</span>", RegexOptions.IgnoreCase);
        //            //Regex FindWin = new Regex("<ul class=\"win_numbers font-white\"[^>]*>((?<mm><ul[^>]*>)+|(?<-mm></ul>)|[\\s\\S])*?(?(mm)(?!))</ul>", RegexOptions.IgnoreCase);
        //            //#region 补充历史开奖
        //            //Regex FindHistory = new Regex("<ul class=\"text-left\">[^>]*>((?<mm><ul[^>]*>)+|(?<-mm></ul>)|[\\s\\S])*?(?(mm)(?!))</ul>", RegexOptions.IgnoreCase); ;
        //            //Regex Finddats = new Regex("<li[^>]*>((?<mm><li[^>]*>)+|(?<-mm></li>)|[\\s\\S])*?(?(mm)(?!))</li>", RegexOptions.IgnoreCase);
        //            //MatchCollection mc = FindHistory.Matches(Source);
        //            //foreach (Match item in mc)
        //            //{
        //            //    if (item.Value.Contains("封盘"))
        //            //    {
        //            //        continue;
        //            //    }
        //            //    MatchCollection dats = Finddats.Matches(item.Value);
        //            //    string lst_dataperiod = NetFramework.Util_WEB.CleanHtml(dats[0].Value);
        //            //    string lst_wins = NetFramework.Util_WEB.CleanHtml(dats[1].Value)
        //            //        + NetFramework.Util_WEB.CleanHtml(dats[2].Value)
        //            //        + NetFramework.Util_WEB.CleanHtml(dats[3].Value)
        //            //           + NetFramework.Util_WEB.CleanHtml(dats[4].Value)
        //            //               + NetFramework.Util_WEB.CleanHtml(dats[5].Value);

        //            //    Linq.ProgramLogic.NewGameResult(lst_wins, lst_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);


        //            //}

        //            //#endregion

        //            //WebKit.DOM.Node issuenode = wb_vrchongqing.Document.GetElementById("lastissue");
        //            string str_dataperiod = wb_vrchongqing.Document.GetElementById("lastissue").TextContent;
        //            if (str_dataperiod == "")
        //            {
        //                NetFramework.Console.WriteLine("网页模拟登陆APP失败", true);
        //                return;
        //            }
        //            string str_Win = issuenode.ChildNodes[0].TextContent;
        //            str_Win = str_Win.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\n", "");
        //            if (str_Win.Contains("?") == false && str_Win != "")
        //            {

        //                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);

        //            }

        //            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
        //            NetFramework.Console.WriteLine("VR重庆时时彩下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
        //            ShiShiCaiDealGameLogAndNotice();


        //            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
        //            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩)
        //            //).Count();
        //            //            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
        //            //            {
        //            //                NewResult = true;


        //            //                DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);

        //            //            }
        //            //            if (LocalGameResultCount != AfterCheckCount)
        //            //            {
        //            //                SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);
        //            //            }
        //        }
        public void DownLoad163CaiPiaoV_vrchongqingcaislimWeb(ref Boolean NewResult, bool ReDrawGdi)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
               && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩)
).Count();
            DateTime PreTime = DateTime.Now;
            string Source = "";
            this.Invoke(new Action(() =>
            {
                if (wb_vrchongqing == null)
                {
                    return;
                }
                if (wb_vrchongqing == null || wb_vrchongqing.WebView == null || wb_vrchongqing.WebView.Url.Contains("Bet/Index/42") == false)
                {
                    NetFramework.Console.WriteLine("未登陆不能使用WEB采集", true);
                    return;
                }
                try
                {
                    Source = wb_vrchongqing.WebView.GetHtml();


                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine(AnyError.Message, true);
                    NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                }
            }));
            Regex FindPeriod = new Regex("<span((?!>)[\\s\\S])+id=\"lastissue\"((?!</span>)[\\s\\S])+</span>", RegexOptions.IgnoreCase);
            Regex FindWin = new Regex("<ul class=\"win_numbers font-white\"[^>]*>((?<mm><ul[^>]*>)+|(?<-mm></ul>)|[\\s\\S])*?(?(mm)(?!))</ul>", RegexOptions.IgnoreCase);
            #region 补充历史开奖
            Regex FindHistory = new Regex("<ul class=\"text-left\">[^>]*>((?<mm><ul[^>]*>)+|(?<-mm></ul>)|[\\s\\S])*?(?(mm)(?!))</ul>", RegexOptions.IgnoreCase); ;
            Regex Finddats = new Regex("<li[^>]*>((?<mm><li[^>]*>)+|(?<-mm></li>)|[\\s\\S])*?(?(mm)(?!))</li>", RegexOptions.IgnoreCase);
            MatchCollection mc = FindHistory.Matches(Source);
            foreach (Match item in mc)
            {
                if (item.Value.Contains("封盘"))
                {
                    continue;
                }
                MatchCollection dats = Finddats.Matches(item.Value);
                string lst_dataperiod = NetFramework.Util_WEB.CleanHtml(dats[0].Value);
                string lst_wins = NetFramework.Util_WEB.CleanHtml(dats[1].Value)
                    + NetFramework.Util_WEB.CleanHtml(dats[2].Value)
                    + NetFramework.Util_WEB.CleanHtml(dats[3].Value)
                       + NetFramework.Util_WEB.CleanHtml(dats[4].Value)
                           + NetFramework.Util_WEB.CleanHtml(dats[5].Value);

                Linq.ProgramLogic.NewGameResult(lst_wins, lst_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);


            }

            #endregion
            string str_dataperiod = NetFramework.Util_WEB.CleanHtml(FindPeriod.Match(Source).Value);
            if (str_dataperiod == "")
            {
                NetFramework.Console.WriteLine("网页模拟登陆APP失败", true);
                return;
            }
            string str_Win = NetFramework.Util_WEB.CleanHtml(FindWin.Match(Source).Value);
            str_Win = str_Win.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\n", "");
            if (str_Win.Contains("?") == false && str_Win != "")
            {

                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);

            }

            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("VR重庆时时彩下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);


            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩)
            //).Count();
            //            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //            {
            //                NewResult = true;


            //                DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);

            //            }
            //            if (LocalGameResultCount != AfterCheckCount)
            //            {
            //                SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);
            //            }
        }
        public void DownLoad163CaiPiaoV_vrchongqingcais_CopyFromOtherUsers(ref Boolean NewResult, bool ReDrawGdi)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            var OtherUsers = db.Game_Result.Where(t => t.GameName == "VR重庆时时彩").OrderByDescending(t => t.GamePeriod);

            #region 补充历史开奖
            Int32 TakeIndex = 0;
            foreach (Linq.Game_Result item in OtherUsers)
            {
                TakeIndex += 1;
                if (TakeIndex >= 80)
                {
                    break;
                }

                Linq.ProgramLogic.NewGameResult(item.GameResult, item.GamePeriod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);


            }

            #endregion
            NetFramework.Console.WriteLine("VR重庆时时彩下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);


            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩)
            //).Count();
            //            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //            {
            //                NewResult = true;


            //                DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);

            //            }
            //            if (LocalGameResultCount != AfterCheckCount)
            //            {
            //                SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);
            //            }
        }

        public void DownLoad163CaiPiaoV_Taohua(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            NetFramework.Console.WriteLine("开始下载桃花中间表" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), false);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://m.500.com/info/kaijiang/ssc/2018-05-11.shtml

            NetFramework.Console.WriteLine("正在刷新桃花" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩)

                ).Count();


            var taohuaresult = db.TaoHua_GameResult.OrderByDescending(t => t.GamePeriod).Take(120);

            DateTime PreTime = DateTime.Now;

            foreach (Linq.TaoHua_GameResult item in taohuaresult)
            {



                string str_dataperiod = item.GamePeriod;


                string str_Win = item.GameResult;
                str_Win = str_Win.Substring(0, 1) + " " + str_Win.Substring(1, 1) + " " + str_Win.Substring(2, 1) + " " + str_Win.Substring(3, 1) + " " + str_Win.Substring(4, 1);


                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);

                //if (Newdb || IsOpenNow)
                //{

                //}


            }//每行处理
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("桃花下载器下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩)
            //).Count();
            //            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //            {
            //                NewResult = true;

            //                DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            //            }


            //            if (LocalGameResultCount != AfterCheckCount)
            //            {
            //                SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            //            }
            #endregion

            NetFramework.Console.WriteLine("下载桃花中间表完成" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), false);
        }


        public void DownLoad163CaiPiaoV_kaijiangwang(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow, Boolean Lasturl = false)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //https://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=2018-05-24&lotCode=10002


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩)
).Count();



            string URL = "http://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=";

            //if (Lasturl == true)
            //{
            //    URL = "http://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?lotCode=10002";
            //}
            //else
            //{
            //    URL += SelectDate.ToString("yyyy-MM-dd") + "&lotCode=10002";
            //}
            URL += SelectDate.ToString("yyyy-MM-dd") + "&lotCode=10002";
            //string URL = "http://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=";


            //URL += SelectDate.ToString("yyyy-MM-dd") + "&lotCode=10002";

            NetFramework.Console.WriteLine("正在刷新开奖网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie163);

            DateTime PreTime = DateTime.Now;
            JObject Resultfull = JObject.Parse(Result);


            foreach (JObject item in Resultfull["result"]["data"])
            {



                string str_dataperiod = (item["preDrawIssue"] as JValue).Value.ToString();
                str_dataperiod = str_dataperiod.Substring(2);

                string str_Win = (item["preDrawCode"] as JValue).Value.ToString();
                str_Win = str_Win.Replace(",", " ");

                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);



            }//每行处理
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("开奖网下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩)
            //).Count();
            //            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //            {
            //                NewResult = true;


            //                DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);

            //            }

            //            if (LocalGameResultCount != AfterCheckCount)
            //            {
            //                SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            //            }

            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }
        //https://kj.13322.com/trend/lottery.findLotteryIssue.do //lottery=cqssc
        //https://kj.13322.com/ssc_cqssc_history_dtoday.html

        public void DownLoad163CaiPiaoV_kaijiangwang1332Fast(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow, Boolean Lasturl = false)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //https://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=2018-05-24&lotCode=10002


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩)
).Count();



            String URL = "http://kj.13322.com/trend/lottery.findLotteryIssue.do";



            //string URL = "http://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=";


            //URL += SelectDate.ToString("yyyy-MM-dd") + "&lotCode=10002";

            NetFramework.Console.WriteLine("正在刷新开奖网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            string Result = NetFramework.Util_WEB.OpenUrl(URL, "http://kj.13322.com/ssc_cqssc_history_dtoday.html", "lottery=cqssc", "POST", cookie163);

            DateTime PreTime = DateTime.Now;
            JObject Resultfull = JObject.Parse(Result);


            foreach (JObject item in Resultfull["result"]["data"])
            {



                string str_dataperiod = (Resultfull["preDrawIssue"] as JValue).Value.ToString();
                str_dataperiod = str_dataperiod.Substring(2);

                string str_Win = (Resultfull["preDrawCode"] as JValue).Value.ToString();
                str_Win = str_Win.Replace(",", " ");

                string Str_Time = (Resultfull["preDrawTime"] as JValue).Value.ToString();



                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩, Str_Time);



            }//每行处理
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("13322下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩)
            //).Count();
            //            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //            {
            //                NewResult = true;


            //                DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);

            //            }

            //            if (LocalGameResultCount != AfterCheckCount)
            //            {
            //                SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            //            }

            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }




        public void DownLoad163CaiPiaoV_xinjiangshishicai(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow, Boolean Lasturl = false)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //https://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=2018-05-24&lotCode=10004


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.新疆时时彩)
).Count();



            string URL = "http://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=";

            if (Lasturl == true)
            {
                URL = "http://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?lotCode=10004";
            }
            else
            {
                URL += SelectDate.ToString("yyyy-MM-dd") + "&lotCode=10004";
            }

            //string URL = "http://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=";


            //URL += SelectDate.ToString("yyyy-MM-dd") + "&lotCode=10002";

            NetFramework.Console.WriteLine("正在刷新新疆时时彩网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie163);

            DateTime PreTime = DateTime.Now;
            JObject Resultfull = JObject.Parse(Result);


            foreach (JObject item in Resultfull["result"]["data"])
            {



                string str_dataperiod = (item["preDrawIssue"] as JValue).Value.ToString();
                str_dataperiod = str_dataperiod.Substring(2);

                string str_Win = (item["preDrawCode"] as JValue).Value.ToString();
                str_Win = str_Win.Replace(",", " ");

                string preDrawTime = (item["preDrawTime"] as JValue).Value.ToString();


                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.新疆时时彩, preDrawTime);



            }//每行处理
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("新疆时时彩下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.新疆时时彩);
            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.新疆时时彩)
            //).Count();
            //            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //            {
            //                NewResult = true;


            //                DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.新疆时时彩);

            //            }

            //            if (LocalGameResultCount != AfterCheckCount)
            //            {
            //                SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.新疆时时彩);
            //            }

            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }


        public static string MaxAozcPeriod = "";
        public static string MaxAozcTime = "";
        //https://88888kai.com/History/HisList.aspx?id=10076&date=2019-04-05&_=0.5912288907898384
        public void DownLoad163CaiPiaoV_aozcAPI168(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow, bool Lasturl = false)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://ssc.zzk3.cn/index.php?s=/home/record/index/yes/1.html


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
).Count();

            DateTime PreTime = DateTime.Now;


            // string URL = "http://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=";
            string URL = "https://www.cp8200.com/CQShiCai/getBaseCQShiCaiList.do?date=&lotCode=lucky5ball";

            if (Lasturl == true)
            {
                URL = "http://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?lotCode=10010";
                //https://cp8200.com/CQShiCai/getBaseCQShiCaiList.do?lotCode=lucky5ball
                //https://cp8200.com/CQShiCai/getBaseCQShiCai.do?issue=50567194&lotCode=lucky5ball

                //URL = "https://www.cp8200.com/CQShiCai/getBaseCQShiCaiList.do?date=&lotCode=lucky5ball";
                // URL = "https://cp8200.com/CQShiCai/getBaseCQShiCai.do?lotCode=lucky5ball" + (MaxAozcPeriod == "" ? "" : ("&issuse=" + (Convert.ToInt32(MaxAozcPeriod) + 1).ToString()));
                NetFramework.Console.WriteLine("正在刷澳洲幸运5彩网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);
                string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie163);
                if (Result == "")
                {
                    return;
                }

                JObject Resultfull = JObject.Parse(Result);
                // JToken item = Resultfull["result"]["data"];
                foreach (JToken item in Resultfull["result"]["data"])
                {


                    string str_dataperiod = (item["preDrawIssue"] as JValue).Value.ToString();
                    string StrTime = (item["preDrawTime"] as JValue).Value.ToString();
                    //StrTime = Convert.ToDateTime(StrTime).AddMinutes(-150).ToString("yyyy-MM-dd HH:mm:ss");

                    if (MaxAozcPeriod == "")
                    {
                        MaxAozcPeriod = str_dataperiod;
                        MaxAozcTime = StrTime;
                    }
                    try
                    {
                        if (Convert.ToInt32(str_dataperiod) > Convert.ToInt32(MaxAozcPeriod))
                        {
                            MaxAozcPeriod = str_dataperiod;
                            MaxAozcTime = StrTime;

                        }
                    }
                    catch (Exception)
                    {


                    }
                    string str_Win = (item["preDrawCode"] as JValue).Value.ToString();
                    str_Win = str_Win.Replace(",", " ");



                    Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5, StrTime);

                }
            }


            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("澳洲幸运5下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5);


            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
            //).Count();
            //            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //            {
            //                NewResult = true;


            //                DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5);

            //            }



            //            if (LocalGameResultCount != AfterCheckCount)
            //            {
            //                SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5);
            //            }
            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }

        public void DownLoad163CaiPiaoV_aozc(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow, bool Lasturl = false)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://ssc.zzk3.cn/index.php?s=/home/record/index/yes/1.html


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
).Count();

            DateTime PreTime = DateTime.Now;


            // string URL = "http://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=";
            string URL = "https://www.cp8200.com/CQShiCai/getBaseCQShiCaiList.do?date=&lotCode=lucky5ball";
            if (Lasturl == true)
            {
                // URL = "http://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?lotCode=10010";
                //https://cp8200.com/CQShiCai/getBaseCQShiCaiList.do?lotCode=lucky5ball
                //https://cp8200.com/CQShiCai/getBaseCQShiCai.do?issue=50567194&lotCode=lucky5ball

                //URL = "https://www.cp8200.com/CQShiCai/getBaseCQShiCaiList.do?date=&lotCode=lucky5ball";
                URL = "https://cp8200.com/CQShiCai/getBaseCQShiCai.do?lotCode=lucky5ball" + (MaxAozcPeriod == "" ? "" : ("&issuse=" + (Convert.ToInt32(MaxAozcPeriod) + 1).ToString()));
                NetFramework.Console.WriteLine("正在刷澳洲幸运5彩网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);
                string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie163);
                if (Result == "")
                {
                    return;
                }
                JObject Resultfull = JObject.Parse(Result);
                JToken item = Resultfull["result"]["data"];
                string str_dataperiod = (item["preDrawIssue"] as JValue).Value.ToString();

                if (MaxAozcPeriod == "")
                {
                    MaxAozcPeriod = str_dataperiod;
                    MaxAozcTime = (item["preDrawTime"] as JValue).Value.ToString();
                }
                try
                {
                    if (Convert.ToInt32(str_dataperiod) > Convert.ToInt32(MaxAozcPeriod))
                    {
                        MaxAozcPeriod = str_dataperiod;
                        MaxAozcTime = (item["preDrawTime"] as JValue).Value.ToString();
                    }
                }
                catch (Exception)
                {


                }
                string str_Win = (item["preDrawCode"] as JValue).Value.ToString();
                str_Win = str_Win.Replace(",", " ");



                string StrTime = (item["preDrawTime"] as JValue).Value.ToString();
                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5, StrTime);

            }
            else
            {
                URL = "https://www.cp8200.com/CQShiCai/getBaseCQShiCaiList.do?date=&lotCode=lucky5ball";

                // URL += SelectDate.ToString("yyyy-MM-dd") + "&lotCode=10010";
                NetFramework.Console.WriteLine("正在刷澳洲幸运5彩网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);
                string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie163);
                if (Result == "")
                {
                    return;
                }
                JObject Resultfull = JObject.Parse(Result);


                foreach (JObject item in Resultfull["result"]["data"])
                {
                    string str_dataperiod = (item["preDrawIssue"] as JValue).Value.ToString();

                    if (MaxAozcPeriod == "")
                    {
                        MaxAozcPeriod = str_dataperiod;
                        MaxAozcTime = (item["preDrawTime"] as JValue).Value.ToString();
                    }
                    try
                    {
                        if (Convert.ToInt32(str_dataperiod) > Convert.ToInt32(MaxAozcPeriod))
                        {
                            MaxAozcPeriod = str_dataperiod;
                            MaxAozcTime = (item["preDrawTime"] as JValue).Value.ToString();
                        }
                    }
                    catch (Exception)
                    {


                    }

                    string str_Win = (item["preDrawCode"] as JValue).Value.ToString();
                    str_Win = str_Win.Replace(",", " ");



                    string StrTime = (item["preDrawTime"] as JValue).Value.ToString();
                    Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5, StrTime);

                }
            }

            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("澳洲幸运5下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5);


            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
            //).Count();
            //            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //            {
            //                NewResult = true;


            //                DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5);

            //            }
            //            if (LocalGameResultCount != AfterCheckCount)
            //            {
            //                SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5);
            //            }


            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }
        public void DownLoad163CaiPiaoV_aozc888888(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow, bool Lasturl = false)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://ssc.zzk3.cn/index.php?s=/home/record/index/yes/1.html


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
).Count();

            DateTime PreTime = DateTime.Now;


            // string URL = "http://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=";
            //string URL = "https://www.cp8200.com/CQShiCai/getBaseCQShiCaiList.do?date=&lotCode=lucky5ball";
            string URL = "https://88888kai.com/Open/CurrentOpen.aspx?code=10076&_=0.19714537356157447";

            {

                // URL += SelectDate.ToString("yyyy-MM-dd") + "&lotCode=10010";
                NetFramework.Console.WriteLine("正在刷澳洲幸运5彩网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);
                string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie163);
                if (Result == "")
                {
                    return;
                }
                JObject Resultfull = JObject.Parse(Result);


                foreach (JObject item in Resultfull["list"])
                {
                    string str_dataperiod = (item["c_t"] as JValue).Value.ToString();

                    string StrTime = (item["c_d"] as JValue).Value.ToString();
                    string[] Times = StrTime.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    StrTime = DateTime.Today.Year + "-" + Times[0].Replace("/", "-") + " " + Times[2];
                    //StrTime = Convert.ToDateTime(StrTime).AddMinutes(-150).ToString("yyyy-MM-dd HH:mm:ss");

                    if (MaxAozcPeriod == "")
                    {
                        MaxAozcPeriod = str_dataperiod;
                        MaxAozcTime = StrTime;
                    }
                    try
                    {
                        if (Convert.ToInt32(str_dataperiod) > Convert.ToInt32(MaxAozcPeriod))
                        {
                            MaxAozcPeriod = str_dataperiod;
                            MaxAozcTime = StrTime;

                        }
                    }
                    catch (Exception)
                    {


                    }

                    string str_Win = (item["c_r"] as JValue).Value.ToString();
                    str_Win = str_Win.Replace(",", " ");





                    Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5, StrTime);

                }
            }

            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("澳洲幸运5下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5);


            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
            //).Count();
            //            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //            {
            //                NewResult = true;
            //                DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5);
            //            }

            //            if (LocalGameResultCount != AfterCheckCount)
            //            {
            //                SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5);
            //            }

            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }



        //cp222789.com
        public void DownLoad163CaiPiaoV_cp222789(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载cp222789网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //https://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=2018-05-24&lotCode=10002


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩)
).Count();




            string URL = "https://www.cp222789.com/data/cqssc/lotteryList/";


            URL += SelectDate.ToString("yyyy-MM-dd") + ".json?DPP64KALP77Z8697L9UY";
            NetFramework.Console.WriteLine("正在刷新彩票22798网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie163);
            DateTime PreTime = DateTime.Now;

            JObject Resultfull = JObject.Parse("{DownData:" + Result + "}");




            foreach (JObject item in (Resultfull["DownData"] as JArray))
            {



                string str_dataperiod = (item["issue"] as JValue).Value.ToString();
                str_dataperiod = str_dataperiod.Substring(2);




                string str_Win = "";
                foreach (object openitem in item["openNum"] as JArray)
                {
                    str_Win += openitem.ToString().Replace("{", "").Replace("}", "");
                }


                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
                //if (Newdb || IsOpenNow)
                //{

                //}



            }//每行处理
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("cp22789下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩)
).Count();
            //if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //{
            //    NewResult = true;


            //    DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);

            //}

            //if (LocalGameResultCount != AfterCheckCount)
            //{
            //    SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            //}

            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }


        //https://kj.13322.com/ssc_cqssc_history_d20180830.html
        //13322.com
        public void DownLoad163CaiPiaoV_13322(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载13322.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //https://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=2018-05-24&lotCode=10002


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩)
).Count();




            string URL = "https://kj.13322.com/ssc_cqssc_history_d";


            URL += SelectDate.ToString("yyyyMMdd") + ".html";
            NetFramework.Console.WriteLine("正在刷新13322网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie163);
            DateTime PreTime = DateTime.Now;
            Regex FindTable = new Regex("<table id=\"trend_table\"((?!</table></div>)[\\s\\S])+</table></div>", RegexOptions.IgnoreCase);

            string TableHtml = FindTable.Match(Result.Replace(Environment.NewLine, "")).Value;
            Regex FinrR = new Regex("td class=\"tdbbs tdbrs\"((?!<td class=\"tdbb\">)[\\S\\s])+ <td class=\"tdbb\">", RegexOptions.IgnoreCase);



            foreach (Match item in FinrR.Matches(TableHtml))
            {

                if (item.Value.Contains("ssc.drawDate") || item.Value.Contains("开奖日期"))
                {
                    continue;
                }


                Regex FindCols = new Regex("class=\"tdbbs tdbrs\"((?!</td>)[\\S\\s])+</td>", RegexOptions.IgnoreCase);
                MatchCollection dat_cols = FindCols.Matches(item.Value);


                string str_dataperiod = dat_cols[1].Value.Replace("</td>", "");
                str_dataperiod = str_dataperiod.Substring(str_dataperiod.IndexOf(">") + 1);
                str_dataperiod = str_dataperiod.Substring(2);




                Regex FindNums = new Regex("class=\"Ballsc_blue\"((?!</td>)[\\s\\S])+</td>", RegexOptions.IgnoreCase);

                string str_Win = "";
                foreach (Match openitem in FindNums.Matches(item.Value))
                {

                    string NumIndex = openitem.Value.Replace("</td>", "");
                    NumIndex = NumIndex.Substring(NumIndex.IndexOf(">") + 1);
                    str_Win += NumIndex;
                }


                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);




            }//每行处理
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("1322下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            //            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
            //                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩)
            //).Count();
            //            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //            {
            //                NewResult = true;


            //                DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);

            //            }

            //            if (LocalGameResultCount != AfterCheckCount)
            //            {
            //                SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            //            }

            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }



        CookieCollection cc1395 = new CookieCollection();
        public void DownLoad163CaiPiaoV_1395p(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //https://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=2018-05-24&lotCode=10002


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩)
).Count();




            string URL = "https://m.1395p.com/cqssc/getawarddata?t=0.8210487342419222";



            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cc1395);
            NetFramework.Console.WriteLine("正在刷新1395p网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            DateTime PreTime = DateTime.Now;
            JObject newr = JObject.Parse(Result);

            string str_Win = newr["current"]["award"].Value<string>();

            str_Win = str_Win.Replace(",", "");

            string str_dataperiod = newr["current"]["date"].Value<string>() + newr["current"]["period"].Value<Int32>().ToString("000");
            str_dataperiod = str_dataperiod.Replace("-", "");
            str_dataperiod = str_dataperiod.Substring(2);

            Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            //if (Newdb || IsOpenNow)
            //{

            //}
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("1395p下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);



            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩)
).Count();
            //if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //{
            //    NewResult = true;


            //    DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);

            //}

            //if (LocalGameResultCount != AfterCheckCount)
            //{
            //    SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            //}

            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }

        CookieCollection xianggshishic = new CookieCollection();
        public void DownLoad163CaiPiaoV_xianggangshishicai(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://ssc.zzk3.cn/index.php?s=/home/record/index/yes/1.html


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.香港时时彩)
).Count();




            string URL = "http://ssc.zzk3.cn/index.php?s=/home/record/index/yes/" + (DateTime.Today - SelectDate).TotalDays.ToString() + ".html";


            NetFramework.Console.WriteLine("正在刷新香港时时彩网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", xianggshishic);
            DateTime PreTime = DateTime.Now;
            Regex findhead = new Regex("<tbody id=\"reslist\"((?!</table>)[\\S\\s])+</table>", RegexOptions.IgnoreCase); ;

            Regex findtr = new Regex("<tr((?!</tr>)[\\S\\s])+</tr>", RegexOptions.IgnoreCase); ;

            Regex findtd = new Regex("<td((?!</td>)[\\S\\s])+</td>", RegexOptions.IgnoreCase); ;

            string strFindTable = findhead.Match(Result).Value;

            MatchCollection trs = findtr.Matches(strFindTable);
            foreach (Match trdata in trs)
            {
                MatchCollection tds = findtd.Matches(trdata.Value);
                string str_Win = NetFramework.Util_WEB.CleanHtml(tds[2].Value);
                str_Win = str_Win.Replace(Environment.NewLine, ",");
                str_Win = str_Win.Replace(" ", "").Replace("\t", "").Replace(",", " ");
                string str_dataperiod = NetFramework.Util_WEB.CleanHtml(tds[1].Value);
                str_dataperiod = str_dataperiod.Substring(2);

                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.香港时时彩);
                //if (Newdb || IsOpenNow)
                //{

                //}
            }
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("香港时时彩下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.香港时时彩);


            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.香港时时彩)
).Count();
            //if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //{
            //    NewResult = true;


            //    DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.香港时时彩);

            //}
            //if (LocalGameResultCount != AfterCheckCount)
            //{
            //    SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.香港时时彩);
            //}


            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }


        CookieCollection wufencai = new CookieCollection();
        public void DownLoad163CaiPiaoV_wufencai(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi, bool IsOpenNow)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 下载彩票结果
            //http://cp98881.com/draw-wfc-20190216.html


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.五分彩)
).Count();




            string URL = "http://cp98881.com/draw-wfc-" + SelectDate.ToString("yyyyMMdd") + ".html";

            NetFramework.Console.WriteLine("正在刷新五分彩网页" + DateTime.Now.ToString("HH:mm:ss fff"), false);

            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", wufencai);
            //new Regex(@"<td[^>]*>((?<mm><td[^>]*>)+|(?<-mm></td>)|[\s\S])*?(?(mm)(?!))</td>", RegexOptions.IgnoreCase);

            Regex findhead = new Regex("id=\"table-wfc\"[^>]*>((?<mm><table[^>]*>)+|(?<-mm></table>)|[\\s\\S])*?(?(mm)(?!))</table>", RegexOptions.IgnoreCase); ;

            Regex findtr = new Regex("<tr((?!</tr>)[\\S\\s])+</tr>", RegexOptions.IgnoreCase); ;

            Regex findtd = new Regex("<td((?!</td>)[\\S\\s])+</td>", RegexOptions.IgnoreCase); ;
            DateTime PreTime = DateTime.Now;
            string strFindTable = findhead.Match(Result).Value;

            MatchCollection trs = findtr.Matches(strFindTable);
            foreach (Match trdata in trs)
            {
                MatchCollection tds = findtd.Matches(trdata.Value);
                if (tds.Count == 0)
                {
                    continue;
                }
                string str_Win = NetFramework.Util_WEB.CleanHtml(tds[1].Value);

                string str_dataperiod = NetFramework.Util_WEB.CleanHtml(tds[0].Value);
                str_Win = str_Win.Replace(Environment.NewLine, ",");
                str_Win = str_Win.Replace(" ", "").Replace("\t", "").Replace(",", " ");
                str_Win = str_Win.Substring(0, 9);
                str_dataperiod = str_dataperiod.Substring(2, 9);

                Linq.ProgramLogic.NewGameResult(str_Win, str_dataperiod, ref NewResult, Linq.ProgramLogic.ShiShiCaiMode.五分彩);

            }
            NetFramework.Console.WriteLine("处理用时间" + (DateTime.Now - PreTime).TotalSeconds.ToString() + "-----------------------------------------------", false);
            NetFramework.Console.WriteLine("五分彩下载完成，准备开奖" + DateTime.Now.ToString("HH:mm:ss fff"), false);
            ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.五分彩);


            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                 && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), Linq.ProgramLogic.ShiShiCaiMode.五分彩)
).Count();
            //if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            //{
            //    NewResult = true;


            //    DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.五分彩);

            //}
            //if (LocalGameResultCount != AfterCheckCount)
            //{
            //    SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.五分彩);
            //}


            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }


        public object FileLock = false;
        public void DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode subm)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            DateTime Localday = DateTime.Now;

            if (subm == Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩 == false)
            {
                if (subm == Linq.ProgramLogic.ShiShiCaiMode.腾讯五分 || subm == Linq.ProgramLogic.ShiShiCaiMode.腾讯十分 || subm == Linq.ProgramLogic.ShiShiCaiMode.腾五信 || subm == Linq.ProgramLogic.ShiShiCaiMode.腾十信)
                {
                    if (Localday.Hour * 60 + Localday.Minute < 5)
                    {
                        Localday = Localday.AddDays(-1);
                    }
                }
                else if (Localday.Hour < 8)
                {
                    Localday = Localday.AddDays(-1);
                }
            }

            else
            {



                if (Localday.Hour < 7)
                {
                    Localday = Localday.AddDays(-1);
                }


            }


            //string GameFullPeriod = "";
            //string GameFullLocalPeriod = "";
            //bool ShiShiCaiSuccess = false;
            //string ShiShiCaiErrorMessage = "";

            //DateTime TestTime = DateTime.Now.AddMinutes(-2);
            //Linq.ProgramLogic.ChongQingShiShiCaiCaculatePeriod(TestTime, "", db, "", "", out GameFullPeriod, out GameFullLocalPeriod, true, out ShiShiCaiSuccess, out ShiShiCaiErrorMessage, subm, true);

            //Localday = Convert.ToDateTime(GameFullLocalPeriod.Substring(0, 4) + "-" + GameFullLocalPeriod.Substring(4, 2) + "-" + GameFullLocalPeriod.Substring(6, 2));


            NetFramework.Console.WriteLine(GlobalParam.UserName + "准备发图" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine, false);

            string Sql = @"select top 288 GamePeriod as 期号,GameTime as 时间,GameResult as 开奖号码,NumTotal as 和数,BigSmall as 大小,SingleDouble as 单双,DragonTiger as 龙虎 from Game_Result where "
               ;
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩 || subm == Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩)
            {

                Sql += "   GamePrivatePeriod like '" + Localday.ToString("yyyyMMdd")
                 + "%' and ";
            }
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.腾讯十分 || subm == Linq.ProgramLogic.ShiShiCaiMode.腾讯五分 || subm == Linq.ProgramLogic.ShiShiCaiMode.腾十信 || subm == Linq.ProgramLogic.ShiShiCaiMode.腾五信)
            {

                Sql += "   GamePeriod like '" + Localday.ToString("yyyyMMdd")
                 + "%' and ";
            }
            Sql += " aspnet_Userid='" + GlobalParam.UserKey.ToString()
            + "' and GameName='" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm) + "' order by GamePeriod desc");



            DataTable PrivatePerios = NetFramework.Util_Sql.RunSqlDataTable(GlobalParam.DataSourceName
                , Sql);
            DataView dv = PrivatePerios.AsDataView();

            ;
            dv.Sort = "期号";
            DataTable dtCopy = dv.ToTable();

            //GDI准备图片

            #region 画龙虎图表
            string Datatextplain = "";
            //Datatextplain += Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm) + Environment.NewLine;
            ////DataRow fir_currow = dtCopy.Rows[dtCopy.Rows.Count - 1];
            //if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
            //{
            //    Datatextplain += "本地时间与官网时差150分钟" + Environment.NewLine;
            //}
            Int32 TigerindexV1 = 0;
            Int32 TotalIndexV1 = 288;
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5 || subm == Linq.ProgramLogic.ShiShiCaiMode.五分彩)
            {
                TotalIndexV1 = 60;
            }

            foreach (DataRow datetextitem in dtCopy.Rows)
            {
                TigerindexV1 += 1;
                if (TigerindexV1 < dtCopy.Rows.Count - TotalIndexV1)
                {
                    continue;
                }
                string tigerordragon = datetextitem.Field<string>("龙虎");
                switch (tigerordragon)
                {
                    case "龙":
                        Datatextplain = Linq.ProgramLogic.Dragon + Datatextplain;
                        break;
                    case "虎":
                        Datatextplain = Linq.ProgramLogic.Tiger + Datatextplain;
                        break;
                    case "合":
                        Datatextplain = Linq.ProgramLogic.OK + Datatextplain;
                        break;
                    default:
                        break;
                }

                //if (datapindex == 11)
                //{
                //    Datatextplain += Environment.NewLine;
                //    datapindex = 1;
                //}

            }//行循环
            SaveVirtualFile("Data3" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", Datatextplain, db);

            #endregion


            #region 画龙虎图表 易信
            string Datatextplain_yixin = "";
            //Datatextplain_yixin += Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm) + Environment.NewLine;
            ////DataRow fir_currow = dtCopy.Rows[dtCopy.Rows.Count - 1];
            //if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
            //{
            //    Datatextplain_yixin += "本地时间与官网时差150分钟" + Environment.NewLine;
            //}
            Int32 TigerindexV2 = 0;
            Int32 TotalIndexV2 = 288;
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5 || subm == Linq.ProgramLogic.ShiShiCaiMode.五分彩)
            {
                TotalIndexV2 = 60;
            }
            foreach (DataRow datetextitem in dtCopy.Rows)
            {
                TigerindexV2 += 1;
                if (TigerindexV2 < dtCopy.Rows.Count - TotalIndexV2)
                {
                    continue;
                }

                string tigerordragon = datetextitem.Field<string>("龙虎");
                switch (tigerordragon)
                {
                    case "龙":
                        Datatextplain_yixin += Linq.ProgramLogic.Dragon_yixin;
                        break;
                    case "虎":
                        Datatextplain_yixin += Linq.ProgramLogic.Tiger_yixin;
                        break;
                    case "合":
                        Datatextplain_yixin += Linq.ProgramLogic.OK_yixin;
                        break;
                    default:
                        break;
                }
                //if (datapindex == 11)
                //{
                //    Datatextplain += Environment.NewLine;
                //    datapindex = 1;
                //}

            }//行循环
            SaveVirtualFile("Data3_yixin" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", Datatextplain_yixin, db);

            #endregion

            #region 画龙虎图表QQ
            string Datatextplain_QQ = "";
            //Datatextplain_dingding += Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm) + Environment.NewLine;
            ////DataRow fir_currow = dtCopy.Rows[dtCopy.Rows.Count - 1];
            //if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
            //{
            //    Datatextplain_dingding += "本地时间与官网时差150分钟" + Environment.NewLine;
            //}
            Int32 TigerindexVQQ = 0;
            Int32 TotalIndexVQQ = 288;
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5 || subm == Linq.ProgramLogic.ShiShiCaiMode.五分彩)
            {
                TotalIndexVQQ = 60;
            }
            foreach (DataRow datetextitem in dtCopy.Rows)
            {
                TigerindexVQQ += 1;
                if (TigerindexVQQ < dtCopy.Rows.Count - TotalIndexVQQ)
                {
                    continue;
                }

                string tigerordragon = datetextitem.Field<string>("龙虎");
                switch (tigerordragon)
                {
                    case "龙":
                        Datatextplain_QQ += Linq.ProgramLogic.Dragon_yixin;
                        break;
                    case "虎":
                        Datatextplain_QQ += Linq.ProgramLogic.Tiger_yixin;
                        break;
                    case "合":
                        Datatextplain_QQ += Linq.ProgramLogic.OK_yixin;
                        break;
                    default:
                        break;
                }

                //if (datapindex == 11)
                //{
                //    Datatextplain += Environment.NewLine;
                //    datapindex = 1;
                //}

            }//行循环
            SaveVirtualFile("Data3_QQ" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", Datatextplain_QQ, db);

            #endregion

            #region 画龙虎图表钉钉
            string Datatextplain_dingding = "";
            //Datatextplain_dingding += Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm) + Environment.NewLine;
            ////DataRow fir_currow = dtCopy.Rows[dtCopy.Rows.Count - 1];
            //if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
            //{
            //    Datatextplain_dingding += "本地时间与官网时差150分钟" + Environment.NewLine;
            //}
            Int32 TigerindexV3 = 0;
            Int32 TotalIndexV3 = 288;
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5 || subm == Linq.ProgramLogic.ShiShiCaiMode.五分彩)
            {
                TotalIndexV3 = 60;
            }
            foreach (DataRow datetextitem in dtCopy.Rows)
            {
                TigerindexV3 += 1;
                if (TigerindexV3 < dtCopy.Rows.Count - TotalIndexV3)
                {
                    continue;
                }

                string tigerordragon = datetextitem.Field<string>("龙虎");
                switch (tigerordragon)
                {
                    case "龙":
                        Datatextplain_dingding += Linq.ProgramLogic.Dragon_yixin;
                        break;
                    case "虎":
                        Datatextplain_dingding += Linq.ProgramLogic.Tiger_yixin;
                        break;
                    case "合":
                        Datatextplain_dingding += Linq.ProgramLogic.OK_yixin;
                        break;
                    default:
                        break;
                }

                //if (datapindex == 11)
                //{
                //    Datatextplain += Environment.NewLine;
                //    datapindex = 1;
                //}

            }//行循环
            SaveVirtualFile("Data3_dingding" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", Datatextplain_dingding, db);

            #endregion



            #region 画龙虎合
            //Int32 TotalRow = dtCopy.Rows.Count / 10;
            //Bitmap img2 = new Bitmap(303, (TotalRow + 2) * 30);
            //Graphics g2 = Graphics.FromImage(img2);
            //Brush bg = new SolidBrush(Color.White);
            //g2.FillRectangle(bg, new Rectangle(0, 0, img2.Width, img2.Height));

            //Image img_tiger = Bitmap.FromFile(Application.StartupPath + "\\tiger.png");
            //Image img_dragon = Bitmap.FromFile(Application.StartupPath + "\\dragon.png");
            //Image img_ok = Bitmap.FromFile(Application.StartupPath + "\\ok.png");

            //Int32 RowIndex = 0;
            //Int32 ResultIndex = 0;
            //Int32 Reminder = 0;
            //foreach (DataRow item in dtCopy.Rows)
            //{
            //    RowIndex = ResultIndex / 10;
            //    Reminder = ResultIndex % 10;

            //    switch (item.Field<string>("龙虎"))
            //    {
            //        case "龙":
            //            g2.DrawImageUnscaled(img_dragon, Reminder * 30 + 3, RowIndex * 30 + 3, 25, 25);
            //            break;
            //        case "虎":
            //            g2.DrawImageUnscaled(img_tiger, Reminder * 30 + 3, RowIndex * 30 + 3, 25, 25);
            //            break;
            //        case "合":
            //            g2.DrawImageUnscaled(img_ok, Reminder * 30 + 3, RowIndex * 30 + 3, 25, 25);
            //            break;
            //        default:
            //            break;
            //    }
            //    ResultIndex += 1;
            //}

            //if (System.IO.File.Exists(Application.StartupPath + "\\Data2" + GlobalParam.UserName + ".jpg"))
            //{
            //    System.IO.File.Delete(Application.StartupPath + "\\Data2" + GlobalParam.UserName + ".jpg");
            //}
            //img_tiger.Dispose();
            //img_dragon.Dispose();
            //img_ok.Dispose();

            //img2.Save(Application.StartupPath + "\\Data2" + GlobalParam.UserName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //img2.Dispose();

            //g2.Dispose();

            #endregion

            #region 画表格数字图
            Bitmap img = new Bitmap(472, 780);
            Graphics g = Graphics.FromImage(img);




            Linq.aspnet_UsersNewGameResultSend myset = Util_Services.GetServicesSetting();

            for (int i = 0; i <= 25; i++)
            {
                Int32 DrawHight = (i) * 30;
                if (i % 2 == 0)
                {
                    Rectangle r = new Rectangle(0, DrawHight, img.Width, 30);
                    Brush BGB = new SolidBrush(Color.FromArgb(236, 236, 236));
                    g.FillRectangle(BGB, r);
                }
                else
                {
                    Rectangle r = new Rectangle(0, DrawHight, img.Width, 30);
                    Brush BGB = new SolidBrush(Color.FromArgb(255, 255, 255));
                    g.FillRectangle(BGB, r);
                }
                Int32 MarginTop = 5;
                Int32 MarginLeft = 5;
                if (i == 0)
                {
                    Font sf = new Font("微软雅黑", 15);
                    Brush br = new SolidBrush(Color.Black);
                    g.DrawString(myset.ImageTopText, sf, br, new PointF(MarginLeft, MarginTop + i * 30));
                    g.DrawString(subm.ToString(), sf, br, new PointF(MarginLeft + 300, MarginTop + i * 30));

                }
                else if (i == 1)
                {

                    Font sf = new Font("微软雅黑", 15);
                    Brush br = new SolidBrush(Color.Red);
                    g.DrawString("期号", sf, br, new PointF(MarginLeft, MarginTop + i * 30));
                    g.DrawString("时间", sf, br, new PointF(MarginLeft + 50, MarginTop + i * 30));
                    g.DrawString("开奖号码", sf, br, new PointF(MarginLeft + 145, MarginTop + i * 30));
                    g.DrawString("和数", sf, br, new PointF(MarginLeft + 275, MarginTop + i * 30));
                    g.DrawString("大小", sf, br, new PointF(MarginLeft + 325, MarginTop + i * 30));
                    g.DrawString("单双", sf, br, new PointF(MarginLeft + 375, MarginTop + i * 30));
                    g.DrawString("龙虎", sf, br, new PointF(MarginLeft + 420, MarginTop + i * 30));
                }
                else if (i <= 24 && i > 1)
                {
                    if (dtCopy.Rows.Count - i + 1 < 0)
                    {
                        continue;
                    }
                    DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - i + 1];
                    Font sf = new Font("微软雅黑", 15);
                    Brush br_g = new SolidBrush(Color.FromArgb(96, 96, 96));
                    Brush br_black = new SolidBrush(Color.FromArgb(0, 0, 0));
                    Brush br_pinkblue = new SolidBrush(Color.FromArgb(172, 204, 236));
                    Brush br_purple = new SolidBrush(Color.FromArgb(232, 47, 205));
                    Brush br_blue = new SolidBrush(Color.FromArgb(48, 34, 245));
                    Brush br_green = new SolidBrush(Color.FromArgb(30, 118, 35));

                    Pen pe_pinkblue = new Pen(br_pinkblue, 2);

                    string ShortPeriod = currow.Field<string>("期号");
                    ShortPeriod = ShortPeriod.Substring(ShortPeriod.Length - 3, 3);
                    g.DrawString(ShortPeriod, sf, br_g, new PointF(MarginLeft, MarginTop + i * 30));//期号
                    DateTime? GameTime = currow.Field<DateTime?>("时间");
                    if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5 && GameTime != null)
                    {
                        GameTime = GameTime.Value.AddMinutes(0);
                    }
                    g.DrawString((GameTime.HasValue ? GameTime.Value.ToString("HH:mm") : "")
                    , sf, br_g, new PointF(MarginLeft + 50, MarginTop + i * 30));//时间

                    string OpenResult = currow.Field<string>("开奖号码");
                    string NewResult = "";
                    if (OpenResult != "")
                    {
                        NewResult = OpenResult.Substring(0, 1) + " "
                            + OpenResult.Substring(1, 1) + " "
                              + OpenResult.Substring(2, 1) + " "
                                + OpenResult.Substring(3, 1) + " "
                                  + OpenResult.Substring(4, 1);
                    }
                    g.DrawString(NewResult, new Font("微软雅黑", 19), br_black, new PointF(MarginLeft + 145, i * 30));//开奖号码

                    g.DrawEllipse(pe_pinkblue, 150, i * 30 + MarginTop, 22, 25);
                    g.DrawEllipse(pe_pinkblue, 172, i * 30 + MarginTop, 22, 25);
                    g.DrawEllipse(pe_pinkblue, 194, i * 30 + MarginTop, 22, 25);
                    g.DrawEllipse(pe_pinkblue, 216, i * 30 + MarginTop, 22, 25);
                    g.DrawEllipse(pe_pinkblue, 238, i * 30 + MarginTop, 22, 25);


                    g.DrawString(currow.Field<Int32>("和数").ToString(), sf, br_purple, new PointF(MarginLeft + 275, MarginTop + i * 30));//和数
                    string 大小 = currow.Field<string>("大小");
                    if (大小 == "大")
                    {
                        g.DrawString(大小, sf, br_purple, new PointF(MarginLeft + 325, MarginTop + i * 30));//大小
                    }
                    else if (大小 == "小")
                    {
                        g.DrawString(大小, sf, br_blue, new PointF(MarginLeft + 325, MarginTop + i * 30));//大小

                    }
                    else
                    {
                        g.DrawString(大小, sf, br_green, new PointF(MarginLeft + 325, MarginTop + i * 30));//大小
                    }


                    string 单双 = currow.Field<string>("单双");
                    if (单双 == "单")
                    {
                        g.DrawString(单双, sf, br_blue, new PointF(MarginLeft + 375, MarginTop + i * 30));//单双

                    }
                    else
                    {
                        g.DrawString(单双, sf, br_purple, new PointF(MarginLeft + 375, MarginTop + i * 30));//单双

                    }

                    string 龙虎 = currow.Field<string>("龙虎");
                    if (龙虎 == "龙")
                    {
                        g.DrawString(龙虎, sf, br_purple, new PointF(MarginLeft + 420, MarginTop + i * 30));//龙虎
                    }
                    else if (龙虎 == "虎")
                    {
                        g.DrawString(龙虎, sf, br_blue, new PointF(MarginLeft + 420, MarginTop + i * 30));//龙虎

                    }
                    else
                    {
                        g.DrawString(龙虎, sf, br_green, new PointF(MarginLeft + 420, MarginTop + i * 30));//龙虎

                    }
                }//数据
                else if (i == 25)
                {
                    Font sf = new Font("微软雅黑", 15);
                    Brush br = new SolidBrush(Color.Black);
                    g.DrawString(myset.ImageEndText, sf, br, new PointF(MarginLeft, MarginTop + i * 30));

                }



            }//每行画图

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            SaveVirtualFile("Data" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".jpg"
           , ms.GetBuffer(), db);
            img.Dispose();
            g.Dispose();


            #endregion




            #region 画表格+龙虎合
            Bitmap img_3 = new Bitmap(472, 780 + 180);
            Graphics g_3 = Graphics.FromImage(img_3);



            Linq.aspnet_UsersNewGameResultSend myset_3 = Util_Services.GetServicesSetting();

            for (int i = 0; i <= 25; i++)
            {
                Int32 DrawHight = (i) * 30;
                if (i % 2 == 0)
                {
                    Rectangle r = new Rectangle(0, DrawHight, img_3.Width, 30);
                    Brush BGB = new SolidBrush(Color.FromArgb(236, 236, 236));
                    g_3.FillRectangle(BGB, r);
                }
                else
                {
                    Rectangle r = new Rectangle(0, DrawHight, img_3.Width, 30);
                    Brush BGB = new SolidBrush(Color.FromArgb(255, 255, 255));
                    g_3.FillRectangle(BGB, r);
                }
                Int32 MarginTop = 5;
                Int32 MarginLeft = 5;
                if (i == 0)
                {
                    Font sf = new Font("微软雅黑", 15);
                    Brush br = new SolidBrush(Color.Black);
                    g_3.DrawString(myset_3.ImageTopText, sf, br, new PointF(MarginLeft, MarginTop + i * 30));

                }
                else if (i == 1)
                {

                    Font sf = new Font("微软雅黑", 15);
                    Brush br = new SolidBrush(Color.Red);
                    g_3.DrawString("期号", sf, br, new PointF(MarginLeft, MarginTop + i * 30));
                    g_3.DrawString("时间", sf, br, new PointF(MarginLeft + 50, MarginTop + i * 30));
                    g_3.DrawString("开奖号码", sf, br, new PointF(MarginLeft + 145, MarginTop + i * 30));
                    g_3.DrawString("和数", sf, br, new PointF(MarginLeft + 275, MarginTop + i * 30));
                    g_3.DrawString("大小", sf, br, new PointF(MarginLeft + 325, MarginTop + i * 30));
                    g_3.DrawString("单双", sf, br, new PointF(MarginLeft + 375, MarginTop + i * 30));
                    g_3.DrawString("龙虎", sf, br, new PointF(MarginLeft + 420, MarginTop + i * 30));
                }
                else if (i <= 24 && i > 1)
                {
                    if (dtCopy.Rows.Count - i + 1 < 0)
                    {
                        continue;
                    }
                    DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - i + 1];
                    Font sf = new Font("微软雅黑", 15);
                    Brush br_g = new SolidBrush(Color.FromArgb(96, 96, 96));
                    Brush br_black = new SolidBrush(Color.FromArgb(0, 0, 0));
                    Brush br_pinkblue = new SolidBrush(Color.FromArgb(172, 204, 236));
                    Brush br_purple = new SolidBrush(Color.FromArgb(232, 47, 205));
                    Brush br_blue = new SolidBrush(Color.FromArgb(48, 34, 245));
                    Brush br_green = new SolidBrush(Color.FromArgb(30, 118, 35));

                    Pen pe_pinkblue = new Pen(br_pinkblue, 2);
                    string ShortPeriod = currow.Field<string>("期号");
                    ShortPeriod = ShortPeriod.Substring(ShortPeriod.Length - 3, 3);
                    g_3.DrawString(ShortPeriod, sf, br_g, new PointF(MarginLeft, MarginTop + i * 30));//期号
                    g_3.DrawString((currow.Field<DateTime?>("时间").HasValue ? currow.Field<DateTime?>("时间").Value.ToString("HH:mm") : "")
                    , sf, br_g, new PointF(MarginLeft + 50, MarginTop + i * 30));//时间

                    string OpenResult = currow.Field<string>("开奖号码");
                    string NewResult = "";
                    if (OpenResult != "")
                    {
                        NewResult = OpenResult.Substring(0, 1) + " "
                            + OpenResult.Substring(1, 1) + " "
                              + OpenResult.Substring(2, 1) + " "
                                + OpenResult.Substring(3, 1) + " "
                                  + OpenResult.Substring(4, 1);
                    }
                    g_3.DrawString(NewResult, new Font("微软雅黑", 19), br_black, new PointF(MarginLeft + 145, i * 30));//开奖号码

                    g_3.DrawEllipse(pe_pinkblue, 150, i * 30 + MarginTop, 22, 25);
                    g_3.DrawEllipse(pe_pinkblue, 172, i * 30 + MarginTop, 22, 25);
                    g_3.DrawEllipse(pe_pinkblue, 194, i * 30 + MarginTop, 22, 25);
                    g_3.DrawEllipse(pe_pinkblue, 216, i * 30 + MarginTop, 22, 25);
                    g_3.DrawEllipse(pe_pinkblue, 238, i * 30 + MarginTop, 22, 25);


                    g_3.DrawString(currow.Field<Int32>("和数").ToString(), sf, br_purple, new PointF(MarginLeft + 275, MarginTop + i * 30));//和数
                    string 大小 = currow.Field<string>("大小");
                    if (大小 == "大")
                    {
                        g_3.DrawString(大小, sf, br_purple, new PointF(MarginLeft + 325, MarginTop + i * 30));//大小
                    }
                    else if (大小 == "小")
                    {
                        g_3.DrawString(大小, sf, br_blue, new PointF(MarginLeft + 325, MarginTop + i * 30));//大小

                    }
                    else
                    {
                        g_3.DrawString(大小, sf, br_green, new PointF(MarginLeft + 325, MarginTop + i * 30));//大小
                    }


                    string 单双 = currow.Field<string>("单双");
                    if (单双 == "单")
                    {
                        g_3.DrawString(单双, sf, br_blue, new PointF(MarginLeft + 375, MarginTop + i * 30));//单双

                    }
                    else
                    {
                        g_3.DrawString(单双, sf, br_purple, new PointF(MarginLeft + 375, MarginTop + i * 30));//单双

                    }

                    string 龙虎 = currow.Field<string>("龙虎");
                    if (龙虎 == "龙")
                    {
                        g_3.DrawString(龙虎, sf, br_purple, new PointF(MarginLeft + 420, MarginTop + i * 30));//龙虎
                    }
                    else if (龙虎 == "虎")
                    {
                        g_3.DrawString(龙虎, sf, br_blue, new PointF(MarginLeft + 420, MarginTop + i * 30));//龙虎

                    }
                    else
                    {
                        g_3.DrawString(龙虎, sf, br_green, new PointF(MarginLeft + 420, MarginTop + i * 30));//龙虎

                    }
                }//数据
                else if (i == 25)
                {
                    Font sf = new Font("微软雅黑", 15);
                    Brush br = new SolidBrush(Color.Black);
                    g_3.DrawString(myset_3.ImageEndText, sf, br, new PointF(MarginLeft, MarginTop + i * 30));

                }



            }//每行画图
            #region 追加小图

            Brush bg = new SolidBrush(Color.White);
            g_3.FillRectangle(bg, new Rectangle(0, 780, 472, 180));

            Image img_tiger_V3 = Bitmap.FromFile(Application.StartupPath + "\\tiger.png");
            Image img_dragon_V3 = Bitmap.FromFile(Application.StartupPath + "\\dragon.png");
            Image img_ok_V3 = Bitmap.FromFile(Application.StartupPath + "\\ok.png");

            Int32 TotalRow_V3 = dtCopy.Rows.Count / 18;

            Int32 RowIndex_V3 = 0;
            Int32 ResultIndex_V3 = 0;
            Int32 Reminder_V3 = 0;



            foreach (DataRow item in dtCopy.Rows)
            {
                RowIndex_V3 = ResultIndex_V3 / 18;
                Reminder_V3 = ResultIndex_V3 % 18;

                switch (item.Field<string>("龙虎"))
                {
                    case "龙":
                        g_3.DrawImageUnscaled(img_dragon_V3, Reminder_V3 * 25 + 3, RowIndex_V3 * 24 + 1 + 780, 23, 23);
                        break;
                    case "虎":
                        g_3.DrawImageUnscaled(img_tiger_V3, Reminder_V3 * 25 + 3, RowIndex_V3 * 24 + 1 + 780, 23, 23);
                        break;
                    case "合":
                        g_3.DrawImageUnscaled(img_ok_V3, Reminder_V3 * 25 + 3, RowIndex_V3 * 24 + 1 + 780, 23, 23);
                        break;
                    default:
                        break;
                }
                ResultIndex_V3 += 1;
            }






            #endregion

            MemoryStream ms3 = new MemoryStream();
            img_3.Save(ms3, System.Drawing.Imaging.ImageFormat.Jpeg);

            SaveVirtualFile("Data" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm)) + "_v3.jpg"
           , ms3.GetBuffer(), db);
            img_3.Dispose();
            g_3.Dispose();


            #endregion

            #region 数字文本
            string DatatextplainV7 = "";

            DatatextplainV7 += Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm) + Environment.NewLine;
            //DataRow fir_currow = dtCopy.Rows[dtCopy.Rows.Count - 1];
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
            {
                DatatextplainV7 += "本地时间与官网时差150分钟" + Environment.NewLine;
            }
            DatatextplainV7 += "期数  时间     号码     结果" + Environment.NewLine;
            for (int rev_index = 0; rev_index <= 10; rev_index++)
            {
                if ((dtCopy.Rows.Count - rev_index - 1) < 0
                    )
                {
                    continue;
                }
                DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - rev_index - 1];


                DatatextplainV7 += CaculateNumberAndDragon(subm, currow, false);
            }
            DatatextplainV7 += Environment.NewLine;

            SaveVirtualFile("Data数字龙虎" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm)) + "V7.txt", DatatextplainV7, db);

            #endregion

            #region 龙虎加数字文本有大小单双有牛牛
            #region 龙虎加数字文本
            string DatatextplainV22 = "";
            DatatextplainV22 += Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm) + Environment.NewLine;
            //DataRow fir_currow = dtCopy.Rows[dtCopy.Rows.Count - 1];
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
            {
                DatatextplainV22 += "本地时间与官网时差150分钟" + Environment.NewLine;
            }
            DatatextplainV22 += "期数  时间     号码     结果" + Environment.NewLine;

            //DatatextplainV5 += CaculateNumberAndDragon(fir_currow) + Environment.NewLine;
            Int32 NumIndexV22 = 10;


            for (int rev_index = 0; rev_index < NumIndexV22; rev_index++)
            {
                //if ((dtCopy.Rows.Count - 16 + rev_index - 1) < 0
                //    )
                //{
                //    continue;
                //}
                //DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - 16 + rev_index - 1];
                if (dtCopy.Rows.Count - 1 - rev_index < 0)
                {
                    continue;
                }
                DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - 1 - rev_index];
                DatatextplainV22 += CaculateNumberAndDragon(subm, currow, false, true, true);
            }
            DatatextplainV22 += Environment.NewLine;
            Int32 TigerindexV22 = 0;
            Int32 TotalIndexV22 = 288;
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5 || subm == Linq.ProgramLogic.ShiShiCaiMode.五分彩)
            {
                TotalIndexV22 = 60;
            }
            foreach (DataRow datetextitem in dtCopy.Rows)
            {
                TigerindexV22 += 1;
                if (TigerindexV22 < dtCopy.Rows.Count - TotalIndexV22)
                {
                    continue;
                }

                string tigerordragon = datetextitem.Field<string>("龙虎");
                switch (tigerordragon)
                {
                    case "龙":
                        DatatextplainV22 += Linq.ProgramLogic.Dragon;
                        break;
                    case "虎":
                        DatatextplainV22 += Linq.ProgramLogic.Tiger;
                        break;
                    case "合":
                        DatatextplainV22 += Linq.ProgramLogic.OK;
                        break;
                    default:
                        break;
                }


            }//行循环
            SaveVirtualFile("Data数字龙虎_五分龙虎Vr牛牛" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", DatatextplainV22, db);

            #endregion


            #region 龙虎加数字文本QQ
            string DatatextplainV23 = "";

            //DataRow fir_currow6 = dtCopy.Rows[dtCopy.Rows.Count - 1];


            //DatatextplainV6 += CaculateNumberAndDragon(fir_currow6) + Environment.NewLine;
            DatatextplainV23 += Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm) + Environment.NewLine;
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
            {
                DatatextplainV23 += "本地时间与官网时差150分钟" + Environment.NewLine;
            }
            Int32 NumIndexV23 = 10;
            DatatextplainV23 += "期数  时间     号码     结果" + Environment.NewLine;

            for (int rev_index = 0; rev_index < NumIndexV23; rev_index++)
            {
                //if ((dtCopy.Rows.Count - 16 + rev_index - 1) < 0
                //    )
                //{
                //    continue;
                //}
                //DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - 16 + rev_index - 1];

                if (dtCopy.Rows.Count - 1 - rev_index < 0)
                {
                    continue;
                }
                DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - 1 - rev_index];
                DatatextplainV23 += CaculateNumberAndDragon(subm, currow, false, true, true);
            }
            DatatextplainV23 += Environment.NewLine;
            Int32 TigerIndexV23 = 0;
            Int32 TotalIndexV23 = 288;
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5 || subm == Linq.ProgramLogic.ShiShiCaiMode.五分彩)
            {
                TotalIndexV23 = 60;
            }
            //最新在前
            foreach (DataRow datetextitem in dtCopy.Rows)
            {
                TigerIndexV23 += 1;
                if (TigerIndexV23 < dtCopy.Rows.Count - TotalIndexV23)
                {
                    continue;
                }


                string tigerordragon = datetextitem.Field<string>("龙虎");
                switch (tigerordragon)
                {
                    case "龙":
                        DatatextplainV23 = Linq.ProgramLogic.Dragon_yixin + DatatextplainV23;
                        break;
                    case "虎":
                        DatatextplainV23 = Linq.ProgramLogic.Tiger_yixin + DatatextplainV23;
                        break;
                    case "合":
                        DatatextplainV23 = Linq.ProgramLogic.OK_yixin + DatatextplainV23;
                        break;
                    default:
                        break;
                }


            }//行循环

            SaveVirtualFile("Data数字龙虎qq_五分龙虎Vr牛牛" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", DatatextplainV23, db);

            #endregion



            #region 龙虎加数字文本钉钉
            string DatatextplainV24 = "";
            DatatextplainV24 += Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm) + Environment.NewLine;
            //DataRow fir_currow6 = dtCopy.Rows[dtCopy.Rows.Count - 1];
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
            {
                DatatextplainV24 += "本地时间与官网时差150分钟" + Environment.NewLine;
            }
            DatatextplainV24 += "期数  时间     号码     结果" + Environment.NewLine;

            //DatatextplainV6 += CaculateNumberAndDragon(fir_currow6) + Environment.NewLine;

            Int32 NumIndexV24 = 12;

            for (int rev_index = 0; rev_index < NumIndexV24; rev_index++)
            {
                //if ((dtCopy.Rows.Count - 16 + rev_index - 1) < 0
                //    )
                //{
                //    continue;
                //}
                //DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - 16 + rev_index - 1];

                if (dtCopy.Rows.Count - 1 - rev_index < 0)
                {
                    continue;
                }
                DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - 1 - rev_index];
                DatatextplainV24 += CaculateNumberAndDragon(subm, currow, false, true, true);
            }
            DatatextplainV24 += Environment.NewLine;
            Int32 TigerIndexV24 = 0;
            Int32 TotalIndexV24 = 288;
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5 || subm == Linq.ProgramLogic.ShiShiCaiMode.五分彩)
            {
                TotalIndexV24 = 60;
            }
            foreach (DataRow datetextitem in dtCopy.Rows)
            {
                TigerIndexV24 += 1;

                if (TigerIndexV24 < dtCopy.Rows.Count - TotalIndexV24)
                {
                    continue;
                }
                string tigerordragon = datetextitem.Field<string>("龙虎");
                switch (tigerordragon)
                {
                    case "龙":
                        DatatextplainV24 = DatatextplainV24 + Linq.ProgramLogic.Dragon_yixin;
                        break;
                    case "虎":
                        DatatextplainV24 = DatatextplainV24 + Linq.ProgramLogic.Tiger_yixin;
                        break;
                    case "合":
                        DatatextplainV24 = DatatextplainV24 + Linq.ProgramLogic.OK_yixin;
                        break;
                    default:
                        break;
                }


            }//行循环
            SaveVirtualFile("Data数字龙虎dingding_五分龙虎Vr牛牛" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", DatatextplainV24, db);

            #endregion
            #endregion


            #region 龙虎加数字文本无大小单双
            #region 龙虎加数字文本
            string DatatextplainV10 = "";
            DatatextplainV10 += Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm) + Environment.NewLine;
            //DataRow fir_currow = dtCopy.Rows[dtCopy.Rows.Count - 1];
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
            {
                DatatextplainV10 += "本地时间与官网时差150分钟" + Environment.NewLine;
            }
            DatatextplainV10 += "期数  时间     号码     结果" + Environment.NewLine;

            //DatatextplainV5 += CaculateNumberAndDragon(fir_currow) + Environment.NewLine;
            Int32 NumIndexV10 = 10;


            for (int rev_index = 0; rev_index < NumIndexV10; rev_index++)
            {
                //if ((dtCopy.Rows.Count - 16 + rev_index - 1) < 0
                //    )
                //{
                //    continue;
                //}
                //DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - 16 + rev_index - 1];
                if (dtCopy.Rows.Count - 1 - rev_index < 0)
                {
                    continue;
                }
                DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - 1 - rev_index];
                DatatextplainV10 += CaculateNumberAndDragon(subm, currow, false, true);
            }
            DatatextplainV10 += Environment.NewLine;
            Int32 TigerindexV10 = 0;
            Int32 TotalIndexV10 = 288;
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5 || subm == Linq.ProgramLogic.ShiShiCaiMode.五分彩)
            {
                TotalIndexV10 = 60;
            }
            foreach (DataRow datetextitem in dtCopy.Rows)
            {
                TigerindexV10 += 1;
                if (TigerindexV10 < dtCopy.Rows.Count - TotalIndexV10)
                {
                    continue;
                }

                string tigerordragon = datetextitem.Field<string>("龙虎");
                switch (tigerordragon)
                {
                    case "龙":
                        DatatextplainV10 += Linq.ProgramLogic.Dragon;
                        break;
                    case "虎":
                        DatatextplainV10 += Linq.ProgramLogic.Tiger;
                        break;
                    case "合":
                        DatatextplainV10 += Linq.ProgramLogic.OK;
                        break;
                    default:
                        break;
                }


            }//行循环
            SaveVirtualFile("Data数字龙虎_五分龙虎" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", DatatextplainV10, db);

            #endregion


            #region 龙虎加数字文本QQ
            string DatatextplainV11 = "";

            //DataRow fir_currow6 = dtCopy.Rows[dtCopy.Rows.Count - 1];


            //DatatextplainV6 += CaculateNumberAndDragon(fir_currow6) + Environment.NewLine;
            DatatextplainV11 += Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm) + Environment.NewLine;
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
            {
                DatatextplainV11 += "本地时间与官网时差150分钟" + Environment.NewLine;
            }
            Int32 NumIndexV11 = 10;
            DatatextplainV11 += "期数  时间     号码     结果" + Environment.NewLine;

            for (int rev_index = 0; rev_index < NumIndexV11; rev_index++)
            {
                //if ((dtCopy.Rows.Count - 16 + rev_index - 1) < 0
                //    )
                //{
                //    continue;
                //}
                //DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - 16 + rev_index - 1];

                if (dtCopy.Rows.Count - 1 - rev_index < 0)
                {
                    continue;
                }
                DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - 1 - rev_index];
                DatatextplainV11 += CaculateNumberAndDragon(subm, currow, false, true);
            }
            DatatextplainV11 += Environment.NewLine;
            Int32 TigerIndexV11 = 0;
            Int32 TotalIndexV11 = 288;
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5 || subm == Linq.ProgramLogic.ShiShiCaiMode.五分彩)
            {
                TotalIndexV11 = 60;
            }
            //最新在前
            foreach (DataRow datetextitem in dtCopy.Rows)
            {
                TigerIndexV11 += 1;
                if (TigerIndexV11 < dtCopy.Rows.Count - TotalIndexV11)
                {
                    continue;
                }


                string tigerordragon = datetextitem.Field<string>("龙虎");
                switch (tigerordragon)
                {
                    case "龙":
                        DatatextplainV11 = Linq.ProgramLogic.Dragon_yixin + DatatextplainV11;
                        break;
                    case "虎":
                        DatatextplainV11 = Linq.ProgramLogic.Tiger_yixin + DatatextplainV11;
                        break;
                    case "合":
                        DatatextplainV11 = Linq.ProgramLogic.OK_yixin + DatatextplainV11;
                        break;
                    default:
                        break;
                }


            }//行循环
            SaveVirtualFile("Data数字龙虎qq_五分龙虎" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", DatatextplainV11, db);

            #endregion



            #region 龙虎加数字文本钉钉
            string DatatextplainV12 = "";
            DatatextplainV12 += Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm) + Environment.NewLine;
            //DataRow fir_currow6 = dtCopy.Rows[dtCopy.Rows.Count - 1];
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
            {
                DatatextplainV12 += "本地时间与官网时差150分钟" + Environment.NewLine;
            }
            DatatextplainV12 += "期数  时间     号码     结果" + Environment.NewLine;

            //DatatextplainV6 += CaculateNumberAndDragon(fir_currow6) + Environment.NewLine;

            Int32 NumIndexV12 = 12;

            for (int rev_index = 0; rev_index < NumIndexV12; rev_index++)
            {
                //if ((dtCopy.Rows.Count - 16 + rev_index - 1) < 0
                //    )
                //{
                //    continue;
                //}
                //DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - 16 + rev_index - 1];

                if (dtCopy.Rows.Count - 1 - rev_index < 0)
                {
                    continue;
                }
                DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - 1 - rev_index];
                DatatextplainV12 += CaculateNumberAndDragon(subm, currow, false, true);
            }
            DatatextplainV12 += Environment.NewLine;
            Int32 TigerIndexV12 = 0;
            Int32 TotalIndexV12 = 288;
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5 || subm == Linq.ProgramLogic.ShiShiCaiMode.五分彩)
            {
                TotalIndexV12 = 60;
            }
            foreach (DataRow datetextitem in dtCopy.Rows)
            {
                TigerIndexV12 += 1;

                if (TigerIndexV12 < dtCopy.Rows.Count - TotalIndexV12)
                {
                    continue;
                }
                string tigerordragon = datetextitem.Field<string>("龙虎");
                switch (tigerordragon)
                {
                    case "龙":
                        DatatextplainV12 = DatatextplainV12 + Linq.ProgramLogic.Dragon_yixin;
                        break;
                    case "虎":
                        DatatextplainV12 = DatatextplainV12 + Linq.ProgramLogic.Tiger_yixin;
                        break;
                    case "合":
                        DatatextplainV12 = DatatextplainV12 + Linq.ProgramLogic.OK_yixin;
                        break;
                    default:
                        break;
                }


            }//行循环
            SaveVirtualFile("Data数字龙虎dingding_五分龙虎" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", DatatextplainV12, db);

            #endregion
            #endregion


            #region 龙虎加数字文本

            #region 龙虎加数字文本
            string DatatextplainV5 = "";
            DatatextplainV5 += Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm) + Environment.NewLine;
            //DataRow fir_currow = dtCopy.Rows[dtCopy.Rows.Count - 1];
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
            {
                DatatextplainV5 += "本地时间与官网时差150分钟" + Environment.NewLine;
            }
            DatatextplainV5 += "期数  时间     号码     结果" + Environment.NewLine;

            //DatatextplainV5 += CaculateNumberAndDragon(fir_currow) + Environment.NewLine;
            Int32 NumIndexV5 = 10;


            for (int rev_index = 0; rev_index < NumIndexV5; rev_index++)
            {
                //if ((dtCopy.Rows.Count - 16 + rev_index - 1) < 0
                //    )
                //{
                //    continue;
                //}
                //DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - 16 + rev_index - 1];
                if (dtCopy.Rows.Count - 1 - rev_index < 0)
                {
                    continue;
                }
                DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - 1 - rev_index];
                DatatextplainV5 += CaculateNumberAndDragon(subm, currow);
            }
            DatatextplainV5 += Environment.NewLine;
            Int32 TigerindexV5 = 0;
            Int32 TotalIndexV5 = 288;
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5 || subm == Linq.ProgramLogic.ShiShiCaiMode.五分彩)
            {
                TotalIndexV5 = 60;
            }
            foreach (DataRow datetextitem in dtCopy.Rows)
            {
                TigerindexV5 += 1;
                if (TigerindexV5 < dtCopy.Rows.Count - TotalIndexV5)
                {
                    continue;
                }

                string tigerordragon = datetextitem.Field<string>("龙虎");
                switch (tigerordragon)
                {
                    case "龙":
                        DatatextplainV5 += Linq.ProgramLogic.Dragon;
                        break;
                    case "虎":
                        DatatextplainV5 += Linq.ProgramLogic.Tiger;
                        break;
                    case "合":
                        DatatextplainV5 += Linq.ProgramLogic.OK;
                        break;
                    default:
                        break;
                }


            }//行循环
            SaveVirtualFile("Data数字龙虎" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", DatatextplainV5, db);

            #endregion


            #region 龙虎加数字文本QQ
            string DatatextplainV8 = "";

            //DataRow fir_currow6 = dtCopy.Rows[dtCopy.Rows.Count - 1];


            //DatatextplainV6 += CaculateNumberAndDragon(fir_currow6) + Environment.NewLine;
            DatatextplainV8 += Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm) + Environment.NewLine;
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
            {
                DatatextplainV8 += "本地时间与官网时差150分钟" + Environment.NewLine;
            }
            Int32 NumIndexV8 = 10;
            DatatextplainV8 += "期数  时间     号码     结果" + Environment.NewLine;

            for (int rev_index = 0; rev_index < NumIndexV8; rev_index++)
            {
                //if ((dtCopy.Rows.Count - 16 + rev_index - 1) < 0
                //    )
                //{
                //    continue;
                //}
                //DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - 16 + rev_index - 1];

                if (dtCopy.Rows.Count - 1 - rev_index < 0)
                {
                    continue;
                }
                DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - 1 - rev_index];
                DatatextplainV8 += CaculateNumberAndDragon(subm, currow);
            }
            DatatextplainV8 += Environment.NewLine;
            Int32 TigerIndexV8 = 0;
            Int32 TotalIndexV8 = 288;
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5 || subm == Linq.ProgramLogic.ShiShiCaiMode.五分彩)
            {
                TotalIndexV8 = 60;
            }
            //最新在前
            foreach (DataRow datetextitem in dtCopy.Rows)
            {
                TigerIndexV8 += 1;
                if (TigerIndexV8 < dtCopy.Rows.Count - TotalIndexV8)
                {
                    continue;
                }


                string tigerordragon = datetextitem.Field<string>("龙虎");
                switch (tigerordragon)
                {
                    case "龙":
                        DatatextplainV8 = Linq.ProgramLogic.Dragon_yixin + DatatextplainV8;
                        break;
                    case "虎":
                        DatatextplainV8 = Linq.ProgramLogic.Tiger_yixin + DatatextplainV8;
                        break;
                    case "合":
                        DatatextplainV8 = Linq.ProgramLogic.OK_yixin + DatatextplainV8;
                        break;
                    default:
                        break;
                }


            }//行循环
            SaveVirtualFile("Data数字龙虎qq" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", DatatextplainV8, db);

            #endregion



            #region 龙虎加数字文本钉钉
            string DatatextplainV6 = "";
            DatatextplainV6 += Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm) + Environment.NewLine;
            //DataRow fir_currow6 = dtCopy.Rows[dtCopy.Rows.Count - 1];
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
            {
                DatatextplainV6 += "本地时间与官网时差150分钟" + Environment.NewLine;
            }
            DatatextplainV6 += "期数  时间     号码     结果" + Environment.NewLine;

            //DatatextplainV6 += CaculateNumberAndDragon(fir_currow6) + Environment.NewLine;

            Int32 NumIndexV6 = 10;

            for (int rev_index = 0; rev_index < NumIndexV6; rev_index++)
            {
                //if ((dtCopy.Rows.Count - 16 + rev_index - 1) < 0
                //    )
                //{
                //    continue;
                //}
                //DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - 16 + rev_index - 1];

                if (dtCopy.Rows.Count - 1 - rev_index < 0)
                {
                    continue;
                }
                DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - 1 - rev_index];
                DatatextplainV6 += CaculateNumberAndDragon(subm, currow);
            }
            DatatextplainV6 += Environment.NewLine;
            Int32 TigerIndexV6 = 0;
            Int32 TotalIndexV6 = 288;
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5 || subm == Linq.ProgramLogic.ShiShiCaiMode.五分彩)
            {
                TotalIndexV6 = 60;
            }
            foreach (DataRow datetextitem in dtCopy.Rows)
            {
                TigerIndexV6 += 1;

                if (TigerIndexV6 < dtCopy.Rows.Count - TotalIndexV6)
                {
                    continue;
                }
                string tigerordragon = datetextitem.Field<string>("龙虎");
                switch (tigerordragon)
                {
                    case "龙":
                        DatatextplainV6 = DatatextplainV6 + Linq.ProgramLogic.Dragon_yixin;
                        break;
                    case "虎":
                        DatatextplainV6 = DatatextplainV6 + Linq.ProgramLogic.Tiger_yixin;
                        break;
                    case "合":
                        DatatextplainV6 = DatatextplainV6 + Linq.ProgramLogic.OK_yixin;
                        break;
                    default:
                        break;
                }


            }//行循环
            SaveVirtualFile("Data数字龙虎dingding" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", DatatextplainV6, db);

            #endregion


            #endregion

            NetFramework.Console.WriteLine(GlobalParam.UserName + "准备完毕发图" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine, false);
        }

        private static void SaveVirtualFile(string FileName, string Context, Linq.dbDataContext db)
        {
            var data = db.aspnet_UserVirtualFile.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.FileID == FileName);
            if (data != null)
            {
                db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, data);
                data.FileContenxt = Convert.ToBase64String(Encoding.UTF8.GetBytes(Context));
                db.SubmitChanges();
            }
            else
            {
                Linq.aspnet_UserVirtualFile newf = new Linq.aspnet_UserVirtualFile();
                newf.aspnet_UserID = GlobalParam.UserKey;
                newf.FileID = FileName;
                newf.FileContenxt = Convert.ToBase64String(Encoding.UTF8.GetBytes(Context));
                db.aspnet_UserVirtualFile.InsertOnSubmit(newf);
                db.SubmitChanges();
            }
        }

        private static void SaveVirtualFile(string FileName, byte[] Context, Linq.dbDataContext db)
        {
            var data = db.aspnet_UserVirtualFile.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.FileID == FileName);
            if (data != null)
            {
                db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, data);
                data.FileContenxt = Convert.ToBase64String(Context); ;
                db.SubmitChanges();
            }
            else
            {
                Linq.aspnet_UserVirtualFile newf = new Linq.aspnet_UserVirtualFile();
                newf.aspnet_UserID = GlobalParam.UserKey;
                newf.FileID = FileName;
                newf.FileContenxt = Convert.ToBase64String(Context);
                db.aspnet_UserVirtualFile.InsertOnSubmit(newf);
                db.SubmitChanges();
            }
        }

        public static string ReadVirtualFile(string FileName, Linq.dbDataContext db)
        {
            var load = db.aspnet_UserVirtualFile.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.FileID == FileName);
            if (load == null)
            {
                NetFramework.Console.WriteLine(FileName + "没有找到", true);
            }
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, load);

            return load == null ? "" : Encoding.UTF8.GetString(Convert.FromBase64String(load.FileContenxt));
        }
        private static byte[] ReadVirtualFilebyte(string FileName, Linq.dbDataContext db)
        {
            var load = db.aspnet_UserVirtualFile.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.FileID == FileName);
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, load);
            if (load == null)
            {
                NetFramework.Console.WriteLine(FileName + "没有找到", true);
            }

            return load == null ? null : Convert.FromBase64String(load.FileContenxt);
        }

        private string CaculateNumberAndDragon(Linq.ProgramLogic.ShiShiCaiMode subm, DataRow currow, bool AddTotal = true, bool NoBigSmallSingleDouble = false, bool CaculateNiuNiu = false)
        {
            string DatatextplainV5 = "";
            string ShortPeriod = currow.Field<string>("期号");
            if (subm == Linq.ProgramLogic.ShiShiCaiMode.新疆时时彩)
            {
                ShortPeriod = "0" + ShortPeriod.Substring(ShortPeriod.Length - 2, 2);
            }
            else
            {
                ShortPeriod = ShortPeriod.Substring(ShortPeriod.Length - 3, 3);
            }
            string 期号 = ShortPeriod;//期号
            DateTime? gametime = currow.Field<DateTime?>("时间");

            string 时间 = (currow.Field<DateTime?>("时间").HasValue ? currow.Field<DateTime?>("时间").Value.ToString("HH:mm") : "");//时间
            string 实时 = (currow.Field<DateTime?>("时间").HasValue ? currow.Field<DateTime?>("时间").Value.AddMinutes(-0).ToString("HH:mm") : "");//时间

            string OpenResult = currow.Field<string>("开奖号码");
            string 合数 = currow.Field<Int32>("和数").ToString();//和数
            string 大小 = currow.Field<string>("大小");
            string 单双 = currow.Field<string>("单双");
            this.Invoke(new Action(() =>
            {
                bool TwoTreeNotSingle = cb_TwoTreeNotSingle.Checked;
                if (TwoTreeNotSingle == true && currow.Field<Int32>("和数") == 23)
                {
                    单双 = "和";
                }


            }));
            string 龙虎 = currow.Field<string>("龙虎");

            string 牛牛 = Linq.ProgramLogic.CaculateNiuNiu(OpenResult);

            if (subm == Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5)
            {

                //                xxx期   10:00    实时:11:00
                //88803         大单龙         27

                //DatatextplainV5 += 期号 + "期  "
                //   + 时间 + "  " + "实时:" + 实时 + Environment.NewLine
                //   + OpenResult + "    "
                //   + 大小 + 单双 + 龙虎

                //   + (AddTotal == false ? "" : ("    " + 合数))
                //   + Environment.NewLine;
                //return DatatextplainV5;
                DatatextplainV5 += 期号 + "  "
                    + 实时 + "  "
                    + OpenResult + "  "
                    + (NoBigSmallSingleDouble == true ? "" : (大小 + 单双)) + 龙虎 + (CaculateNiuNiu == false ? "" : " " + 牛牛)

                    + (AddTotal == false ? "" : ("" + 合数))
                    + Environment.NewLine;
                return DatatextplainV5;

            }
            else
            {
                DatatextplainV5 += 期号 + "  "
                    + 时间 + "  "
                    + OpenResult + "  "
                     + (NoBigSmallSingleDouble == true ? "" : (大小 + 单双)) + 龙虎 + (CaculateNiuNiu == false ? "" : " " + 牛牛)

                    + (AddTotal == false ? "" : ("" + 合数))
                    + Environment.NewLine;
                return DatatextplainV5;
            }
        }

        public void DrawDataTable(DataTable datasource)
        {


            #region 画表格
            Bitmap img = new Bitmap(840, (datasource.Rows.Count + 4) * 30);
            Graphics g = Graphics.FromImage(img);


            for (int i = 0; i <= datasource.Rows.Count + 4; i++)
            {
                Int32 DrawHight = (i) * 30;
                if (i % 2 == 0)
                {
                    Rectangle r = new Rectangle(0, DrawHight, img.Width, 30);
                    Brush BGB = new SolidBrush(Color.FromArgb(236, 236, 236));
                    g.FillRectangle(BGB, r);
                }
                else
                {
                    Rectangle r = new Rectangle(0, DrawHight, img.Width, 30);
                    Brush BGB = new SolidBrush(Color.FromArgb(255, 255, 255));
                    g.FillRectangle(BGB, r);
                }
                Int32 MarginTop = 5;
                Int32 MarginLeft = 5;
                if (i == 0)
                {
                    Font sf = new Font("微软雅黑", 15);
                    Brush br = new SolidBrush(Color.Black);

                }
                else if (i == 1)
                {

                    Font sf = new Font("微软雅黑", 15);
                    Brush br = new SolidBrush(Color.Black);

                    Font sfl = new Font("微软雅黑", 12);
                    Int32 WriteWidth = 0;
                    for (int ci = 0; ci < datasource.Columns.Count; ci++)
                    {
                        if (ci == 1)
                        {
                            WriteWidth += 180;
                        }
                        else if (ci > 1)
                        {
                            WriteWidth += 100;
                        }
                        g.DrawString(datasource.Columns[ci].ColumnName, (ci == 0 ? sfl : sf), br, new PointF(MarginLeft + WriteWidth, MarginTop + i * 30));

                    }


                }
                else
                {
                    if (i - 2 - datasource.Rows.Count >= 0)
                    {
                        continue;
                    }
                    DataRow currow = datasource.Rows[i - 2];


                    Font sf = new Font("微软雅黑", 15);
                    Brush br = new SolidBrush(Color.Black);
                    Font sfl = new Font("微软雅黑", 12);
                    Int32 WriteWidth = 0;
                    for (int ci = 0; ci < datasource.Columns.Count; ci++)
                    {
                        if (ci == 1)
                        {
                            WriteWidth += 180;
                        }
                        else if (ci > 1)
                        {
                            WriteWidth += 100;
                        }
                        g.DrawString(currow.Field<object>(ci).ToString(), (ci == 0 ? sfl : sf), br, new PointF(MarginLeft + WriteWidth, MarginTop + i * 30));



                    }



                }//具体数据结束
            }//每行画图
            if (System.IO.File.Exists(Application.StartupPath + "\\Data" + GlobalParam.UserName + "老板查询.jpg"))
            {
                System.IO.File.Delete(Application.StartupPath + "\\Data" + GlobalParam.UserName + "老板查询.jpg");
            }
            img.Save(Application.StartupPath + "\\Data" + GlobalParam.UserName + "老板查询.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            img.Dispose();
            g.Dispose();

            #endregion

        }



        private System.Net.CookieCollection cookie163 = new CookieCollection();


        public static string JavaTimeSpanFFFF()
        {

            double result = 0;
            DateTime startdate = new DateTime(1970, 1, 1, 8, 0, 0);
            TimeSpan seconds = DateTime.Now - startdate;
            result = Math.Round(seconds.TotalMilliseconds, 0);
            return result.ToString() + DateTime.Now.ToString("ffff");

        }


        public static string JavaTimeSpan()
        {

            double result = 0;
            DateTime startdate = new DateTime(1970, 1, 1, 8, 0, 0);
            TimeSpan seconds = DateTime.Now - startdate;
            result = Math.Round(seconds.TotalMilliseconds, 0);
            return result.ToString();

        }
        public static string JavaSecondTimeSpan()
        {

            double result = 0;
            DateTime startdate = new DateTime(1970, 1, 1, 8, 0, 0);
            TimeSpan seconds = DateTime.Now - startdate;
            result = Math.Round(seconds.TotalSeconds, 0);
            return result.ToString();

        }

        public static DateTime JavaTime(Int64 time)
        {

            DateTime startdate = new DateTime(1970, 1, 1, 8, 0, 0);
            return startdate.AddMilliseconds(time);
        }

        public static DateTime JavaSecondTime(Int64 time)
        {

            DateTime startdate = new DateTime(1970, 1, 1, 8, 0, 0);
            return startdate.AddSeconds(time);
        }
        private void btn_resfresh_Click(object sender, EventArgs e)
        {

            PicBarCode.Visible = true;
            MI_GameLogManulDeal.Enabled = false;

            KillThread.Add(Keepaliveid, true);
            Keepaliveid = Guid.NewGuid();

            WeiXinOnLine = false;



            Thread StartThread = new Thread(new ThreadStart(StartThreadDo));
            StartThread.Start();


        }


        static bool ReloadWX = false;



        bool FirstRun = true;
        DateTime? VR_PreTime = DateTime.Now;
        private void tm_refresh_Tick(object sender, EventArgs e)
        {

            try
            {
                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;
                lbl_six.Text = "六下期：" + (GetNextPreriodHKSix(db) == null ? "" : GetNextPreriodHKSix(db).GamePeriod);
                lbl_qqthread.Text = "(ALT+O)采集:" + (StopQQ == true ? "停止" : "运行");
                tm_refresh.Enabled = false;


                if (FirstRun == true)
                {
                    if (Util_Services.GetServicesSetting().SuperUser == true)
                    {
                        wb_ballgame.WebView.LoadUrl("http://odds.gooooal.com/company.html?type=1001");
                        wb_balllivepoint.WebView.LoadUrl("http://live.gooooal.com");

                    }

                    Thread startdo = new Thread(new ThreadStart(ThreadStartGetBallRatioV2));
                    startdo.Start();

                    #region 启动联赛循环发图
                    Thread strcs = new Thread(new ThreadStart(ThreadRepeatCheckSend));
                    strcs.Start();
                    #endregion

                    Thread trhksix = new Thread(new ThreadStart(ThreadGetHKSix));
                    trhksix.Start();

                    Thread checkqq = new Thread(new ThreadStart(Thread_CheckQQ));
                    checkqq.SetApartmentState(ApartmentState.STA);
                    checkqq.Start();

                    ReloadWebApp();

                    FirstRun = false;
                }

                //string Source = NetFramework.Util_CEF.JoinQueueAndWait(NewUrl, wb_vrchongqing);

                try
                {
                    if (wb_vrchongqing.WebView.Url == null)
                    {
                        ReloadWebApp();
                    }
                    if (wb_vrchongqing.WebView.Url.Contains("/Bet/Index/42") == false && wb_vrchongqing.WebView.Url.Contains("/Bet/Index") == true)
                    {
                        // wb_vrchongqing.Load("hfttp://huy.vrbetapi.com/Bet/Index/42");
                        wb_vrchongqing.WebView.LoadUrl("http://huy.vrbetapi.com/Bet/Index/42");
                    }
                    if (wb_vrchongqing.WebView.Url.Contains("ErrorHandle/Timeout")
                        )
                    {
                        ReloadWebApp();
                    }
                    String DocSource = wb_vrchongqing.WebView.GetHtml();
                    //StartGetDoc(wb_vrchongqing.DocumentAsHTMLDocument);
                    if (DocSource.Contains("时间逾期") || DocSource.Contains("重新登陆"))
                    {

                        ReloadWebApp();

                    }
                    //if ((DateTime.Now - VR_PreTime.Value).TotalSeconds > 60)
                    //{
                    //    ReloadWebApp();
                    //}
                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine(AnyError.Message, true);
                    NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                }
                SI_url.Text = NetFramework.Util_WEB.CurrentUrl;
                SI_url.ToolTipText = SI_url.Text;





                if (ReloadWX == true)
                {
                    btn_resfresh.Visible = true;
                    PicBarCode.Visible = true;
                    ReloadWX = false;
                }
                #region 显示模拟器
                //this.Invoke(new Action(() =>
                //{

                if (cb_adbnoxmode.Checked == true && tb_NoxPath.Text != "")
                {
                    CmdRun(tb_LeidianPath.Text, "adb devices");
                    string cmdr = CmdRun(tb_NoxPath.Text, "noxconsole.exe list");
                    ShowEnumtors(cmdr, SourceNoxList, "noxconsole.exe list");
                }
                if (cb_adbleidianmode.Checked == true && tb_LeidianPath.Text != "")
                {
                    CmdRun(tb_LeidianPath.Text, "adb devices");
                    string cmdr = CmdRun(tb_LeidianPath.Text, "ldconsole.exe list2");
                    ShowEnumtors(cmdr, SourceLeidianList, "ldconsole.exe list2");
                }
                //}));

                #endregion
            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine(AnyError.Message, true);
                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
            }
            //foreach (TabPage item in tc_wb.TabPages)
            //{
            //    tc_wb.SelectedTab=item;
            //} 





            tm_refresh.Enabled = true;


        }


        private string HuaYuToken = "";
        private bool TskRuning = false;
        private void ReloadWebApp()
        {
            try
            {

                System.Threading.Tasks.Task tsk = new System.Threading.Tasks.Task(new Action(() =>
                {
                    if (TskRuning == true)
                    {
                        return;
                    }
                    TskRuning = true;
                    try
                    {


                        VR_PreTime = DateTime.Now;
                        string LoginUrl = "https://api.honze88.com/api/v1/login";
                        string loginResult = NetFramework.Util_WEB.OpenUrl(LoginUrl, "", "{\"memberName\":\"xl1234567\",\"loginPasswd\":\"123456\"}", "POST", wufencai, true, true, "application/json; charset=utf-8"
                           , "");

                        string tOKEN = JObject.Parse(loginResult)["token"].ToString();
                        HuaYuToken = tOKEN;
                        string URL = "https://api.honze88.com/api/v1/yules/vr/lobbies/601/landing";

                        string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", wufencai, true, true, "application/x-www-form-urlencoded; charset=UTF-8"
                            , "JWT " + tOKEN);
                        JObject jr = JObject.Parse(Result);
                        //JWT eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MzQ0MzYxLCJuYW1lIjoieGwxMjM0NTY3IiwiaWF0IjoxNTU1MjEwNzMzLCJleHAiOjE1NTY0MjAzMzN9.6c9VVDQZkiCAd9Up_6MoqhooDGL_uL3ZSSAYE9j8YVc
                        string NewuRL = jr["url"].ToString();
                        this.Invoke(
                            new Action(() =>
                            {


                                NetFramework.Util_CEF.JoinQueueAndWait(NewuRL, wb_vrchongqing);
                            })

                            );//Invoke 
                    }
                    catch (Exception AnyError)
                    {
                        NetFramework.Console.WriteLine(AnyError.Message, true);
                        NetFramework.Console.WriteLine(AnyError.StackTrace, true);

                    }
                    TskRuning = false;
                }));

                tsk.Start();


            }
            catch (Exception anyerror)
            {

                NetFramework.Console.WriteLine("---------------------------------自动登录失败--------------------------", true);
                NetFramework.Console.WriteLine(anyerror.Message, true);
                NetFramework.Console.WriteLine(anyerror.StackTrace, true);
            }



        }
        private void ShowEnumtors(string Output, BindingList<EnumListRow> sourcebuf, string Command)
        {
            string[] Lines = Output.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            sourcebuf.Clear();
            bool NextLineIsCommand = false;
            foreach (var item in Lines)
            {
                if (item.EndsWith(Command))
                {
                    NextLineIsCommand = true;
                    continue;
                }
                if (NextLineIsCommand == false)
                {
                    continue;
                }
                if (item.Contains(","))
                {
                    string[] cells = item.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    EnumListRow newr = new EnumListRow();
                    newr.EnumName = cells[1];
                    newr.EnumState = cells[2] == "0" ? "未启动" : "启动";
                    sourcebuf.Add(newr);
                }
            }

        }


        private BindingList<EnumListRow> SourceNoxList = new BindingList<EnumListRow>();
        private BindingList<EnumListRow> SourceLeidianList = new BindingList<EnumListRow>();
        private class EnumListRow
        {
            public string EnumName { get; set; }
            public string EnumState { get; set; }
        }
        private void MI_MyData_Click(object sender, EventArgs e)
        {
            UserSetting us = new UserSetting();
            us.fd_username.Text = GlobalParam.UserName;
            us.SetMode("MyData");
            us.sf = this;
            us.Show();
        }

        private void MI_NewUser_Click(object sender, EventArgs e)
        {
            UserSetting us = new UserSetting();
            us.SetMode("New");
            us.sf = this;
            us.Show();
        }

        private void MI_UserSetting_Click(object sender, EventArgs e)
        {
            UserSetting us = new UserSetting();
            us.SetMode("Modify");
            us.sf = this;
            us.Show();
        }

        private void MI_Ratio_Setting_Click(object sender, EventArgs e)
        {
            F_Game_BasicRatio fm = new F_Game_BasicRatio();
            fm.Show();
        }

        private void StartForm_FormClosing(object sender, FormClosingEventArgs e)
        {



            try
            {


                //wb_ballgame.Stop();

                //wb_other.Stop();
                //wb_refresh.Stop();

                //wb_balllivepoint.Stop();


            }
            catch (Exception)
            {


            }

            wb_ballgame.Dispose();

            wb_other.Dispose();
            wb_refresh.Dispose();

            wb_balllivepoint.Dispose();

            EO.Base.Runtime.Shutdown();


            //CefSharp.Cef.Shutdown();
            //GC.Collect();
            Application.Exit();
            Environment.Exit(0);

        }

        private void MI_GameLogManulDeal_Click(object sender, EventArgs e)
        {
            Download163AndDeal d163 = new Download163AndDeal();
            d163.StartF = this;
            d163.RunnerF = this.RunnerF;
            d163.Show();
        }

        private void MI_OpenQuery_Click(object sender, EventArgs e)
        {
            OpenQuery oq = new OpenQuery();
            oq.RunnerF = this.RunnerF;
            oq.Show();
        }

        private void MI_Bouns_Manul_Click(object sender, EventArgs e)
        {
            SendBouns sb = new SendBouns();
            sb.StartF = this;
            sb.Show();
        }

        private void MI_Bouns_Setting_Click(object sender, EventArgs e)
        {
            F_WX_BounsRatio br = new F_WX_BounsRatio();
            br.Show();
        }

        private void BtnDrawGdi_Click(object sender, EventArgs e)
        {
            //Linq.dbDataContext db = new Linq.dbDataContext();
            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.五分彩);
            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.香港时时彩);
            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5);
            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);

            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾讯五分);
            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.北京赛车PK10);
            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);
            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.新疆时时彩);

            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾十信);
            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾五信);

        }

        private void Btn_StartDownLoad_Click(object sender, EventArgs e)
        {
            while (true)
            {
                try
                {
                    DownloadResult_Vr(true);

                    DownloadResult_Aoz(true);
                    DownloadResult_tengshi(true);
                    DownloadResult_tengwu(true);
                    DownloadResult_wufen(true);
                    DownloadResult_xiangjiang(true);
                    DownloadResult_chongqing(true);
                    DownloadResult_tengshi_xin(true);
                    DownloadResult_tengwu_xin(true);
                    DownloadResult_HeNeiWuFen(true);
                    DownloadResult_VrHK(true);
                }
                catch (Exception)
                {

                }

                Thread.Sleep(1000);
            }
        }

        private void btn_TestOrder_Click(object sender, EventArgs e)
        {




        }

        private void OpenBlack_Click(object sender, EventArgs e)
        {
            LogForm lf = new LogForm();
            lf.Show();



        }


        string yixinQrCodeData = "";
        string yixinQrUrl = "";
        private void LoadYiXinBarCode()
        {

            this.Invoke(new Action(() =>
            {
                string Result = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im"
       , "", "", "GET", cookieyixin);

                Regex findqrcode = new Regex("qrCode:'((?!')[\\S\\s])+'", RegexOptions.IgnoreCase);
                yixinQrCodeData = findqrcode.Match(Result).Value;
                yixinQrCodeData = yixinQrCodeData.Replace("'", "").Replace("qrCode:", "");

                Regex findqrurl = new Regex("qrUrl:'((?!')[\\S\\s])+'", RegexOptions.IgnoreCase);

                yixinQrUrl = findqrurl.Match(Result).Value;
                yixinQrUrl = yixinQrUrl.Replace("'", "").Replace("qrUrl:", "");


                ///dimen-login/qr/4789ebdae32b47de9d098e75c57f59c0?c=y%2FqZHisaL4rnfPzN5uSTWg%3D%3D
                PicBarCode_yixin.ImageLocation = yixinQrUrl;
            }));


        }

        private void btn_refreshyixin_Click(object sender, EventArgs e)
        {
            PicBarCode_yixin.Visible = true;
            MI_GameLogManulDeal.Enabled = false;
            YiXinOnline = false;

            KillThread.Add(KeepaliveYiXInid, true);
            KeepaliveYiXInid = Guid.NewGuid();

            Thread StartThread = new Thread(new ThreadStart(StartThreadYixinDo));
            StartThread.Start();
        }

        private void btn_bossreport_Click(object sender, EventArgs e)
        {
            DataTable Result1 = WeixinRoboot.Linq.ProgramLogic.GetBossReportSource("微", "20180905");
            DataTable Result2 = WeixinRoboot.Linq.ProgramLogic.GetBossReportSource("微", "20180112.20190630");

            DrawDataTable(Result2);
        }


        private void RepeatSendBossReport()
        {
            while (true)
            {


                try
                {



                    if (DateTime.Now.Hour * 60 + DateTime.Now.Minute >= 7 * 60 + 57)
                    {


                        Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                        //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                        ////db.ObjectTrackingEnabled = false;
                        var Rows = RunnerF.MemberSource.Select("User_IsBoss='true'");
                        foreach (var Bossitem in Rows)
                        {



                            var findsendlog = db.PIC_EndSendLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                                && t.WX_BossID == Bossitem.Field<string>("User_ContactID")
                                && t.WX_SendDate == DateTime.Today
                                && t.WX_SourceType == Bossitem.Field<string>("User_SourceType")
                                );
                            if (findsendlog == null)
                            {
                                NetFramework.Console.WriteLine("准备老板查询发图", false);
                                DataTable Result2 = WeixinRoboot.Linq.ProgramLogic.GetBossReportSource(Bossitem.Field<string>("User_SourceType")
                                    , DateTime.Today.AddDays(-1).ToString("yyyyMMdd"));
                                DrawDataTable(Result2);

                                SendRobotImage(Application.StartupPath + "\\Data" + GlobalParam.UserName + "老板查询.jpg"
                                    , Bossitem.Field<string>("User_ContactTEMPID")
                                    , Bossitem.Field<string>("User_SourceType"));




                                NetFramework.Console.WriteLine("老板查询发图完毕", false);
                                Linq.PIC_EndSendLog bsl = new Linq.PIC_EndSendLog();
                                bsl.WX_BossID = Bossitem.Field<string>("User_ContactID");
                                bsl.WX_SourceType = Bossitem.Field<string>("User_SourceType");
                                bsl.WX_SendDate = DateTime.Now;
                                bsl.WX_UserName = Bossitem.Field<string>("User_ContactID");
                                bsl.aspnet_UserID = GlobalParam.UserKey;
                                db.PIC_EndSendLog.InsertOnSubmit(bsl);
                                db.SubmitChanges();
                            }







                        }
                    }//封盘时间才发
                }//try 结束
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine("定时发送老板查询异常" + AnyError.StackTrace, true);

                }
                Thread.Sleep(60 * 1000);



            }//检查时间循环
        }

        private void btn_InjectAndDo_Click(object sender, EventArgs e)
        {

            InjectWins.Clear();

            NetFramework.WindowsApi.EnumWindows(new NetFramework.WindowsApi.CallBack(EnumWinsCallBack), 0);
            MI_GameLogManulDeal.Enabled = true;
            MI_Bouns_Manul.Enabled = true;
        }
        public BindingList<Linq.WX_PCSendPicSetting> InjectWins = new BindingList<Linq.WX_PCSendPicSetting>();
        public enum PCSourceType { PC微, PC钉, 雷电, PCQ, 夜神, 未知 }
        private PCSourceType WindowclassToSourceType(string windowsclass)
        {

            if (windowsclass == "ChatWnd")
            {
                return PCSourceType.PC微;
            }
            else if (windowsclass == "StandardFrame_DingTalk")
            {
                return PCSourceType.PC钉;
            }
            else if (windowsclass == "LDPlayerMainFrame")
            {
                return PCSourceType.雷电;
            }
            else if (windowsclass == "TXGuiFoundation")
            {
                return PCSourceType.PCQ;
            }
            else if (windowsclass == "Qt5QWindowIcon")
            {
                return PCSourceType.夜神;
            }
            else
            {
                return PCSourceType.未知;
            }
        }
        public Linq.dbDataContext winsdb = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);

        public bool EnumWinsCallBack(IntPtr hwnd, int lParam)
        {

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            ////db.ObjectTrackingEnabled = false;
            StringBuilder sb = new StringBuilder(512);
            NetFramework.WindowsApi.GetClassNameW(hwnd, sb, sb.Capacity);
            if (sb.ToString() == "ChatWnd" || (sb.ToString() == "StandardFrame_DingTalk") || (sb.ToString() == "LDPlayerMainFrame") || (sb.ToString() == "TXGuiFoundation")
                || (sb.ToString() == "Qt5QWindowIcon")

                )
            {
                StringBuilder RAW = new StringBuilder(512);
                NetFramework.WindowsApi.GetWindowText(hwnd, RAW, 512);





                string NewTitle = "智能发图" + RAW.ToString().Replace("智能发图", "");
                // NetFramework.WindowsApi.SetWindowText(hwnd, NewTitle);

                NetFramework.WindowsApi.Rect rec = new NetFramework.WindowsApi.Rect();
                NetFramework.WindowsApi.GetWindowRect(hwnd, out rec);
                if (rec.Right - rec.Left > 0 && NewTitle != "智能发图" && NewTitle != "智能发图QQ"
                    && NewTitle != "智能发图TXMenuWindow"
                     && NewTitle.EndsWith("的资料") == false
                     && NewTitle.EndsWith("验证消息") == false
                     && NewTitle.Contains("桌面快捷清理") == false
                     && NewTitle.Contains("MEmuConsole") == false
                    )
                {
                    if (db.WX_PCSendPicSetting.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
               && t.WX_UserName == NewTitle.Replace("智能发图", "")
               && t.WX_SourceType == Enum.GetName(typeof(PCSourceType), WindowclassToSourceType(sb.ToString()))
               ) == null)
                    {
                        Linq.WX_PCSendPicSetting newws = new Linq.WX_PCSendPicSetting();
                        newws.aspnet_UserID = GlobalParam.UserKey;
                        newws.WX_SourceType = Enum.GetName(typeof(PCSourceType), WindowclassToSourceType(sb.ToString()));
                        newws.WX_UserName = NewTitle.Replace("智能发图", "");

                        newws.OpenResult = false;
                        newws.NumberDragonTxt = false;
                        newws.Is_Reply = false;
                        newws.OpenPIC = false;
                        newws.OpenHour = 7;
                        newws.OpenMinue = 10;
                        newws.BounsTimeStopPIC = false;
                        newws.EndPIC = false;
                        newws.EndHour = 3;
                        newws.EndMinute = 10;
                        newws.DuringFiveMinute = false;
                        newws.OpenPlease = false;
                        newws.DragonTigerOne = false;
                        newws.WechatCard = false;
                        newws.LastPeriod = false;
                        newws.Text1 = "";
                        newws.Text1Minute = 120;
                        newws.Text2 = "";
                        newws.Text2Minute = 120;

                        newws.Text3 = "";
                        newws.Text3Minute = 120;
                        newws.NumberPIC = false;
                        newws.DragonPIC = false;
                        newws.FootballPIC = false;
                        newws.BasketballPIC = false;
                        newws.BallLink = false;
                        newws.BallInterval = 120;


                        newws.WX_UserTMPID = hwnd.ToString();
                        newws.Can_Use = true;

                        newws.ChongqingMode = true;
                        newws.FiveMinuteMode = false;
                        newws.HkMode = false;
                        newws.AozcMode = false;
                        newws.Tengxunshifen = false;
                        newws.RefreshBill = false;


                        db.WX_PCSendPicSetting.InsertOnSubmit(newws);


                        db.SubmitChanges();



                    }//数据库不存在

                    if (InjectWins.SingleOrDefault(t =>
                                 (t.WX_UserName == NewTitle.Replace("智能发图", "")
                        && t.WX_SourceType == Enum.GetName(typeof(PCSourceType), WindowclassToSourceType(sb.ToString()))
                                 )

                        ) == null)
                    {
                        Linq.WX_PCSendPicSetting loadset = winsdb.WX_PCSendPicSetting.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                          && t.WX_UserName == NewTitle.Replace("智能发图", "")
                          && t.WX_SourceType == Enum.GetName(typeof(PCSourceType), WindowclassToSourceType(sb.ToString())));
                        if (loadset != null)
                        {


                            loadset.WX_UserTMPID = hwnd.ToString();
                            loadset.Can_Use = true;

                            loadset.TMP_Text1Time = DateTime.Today;
                            loadset.TMP_Text2Time = DateTime.Today;
                            loadset.TMP_Text3Time = DateTime.Today;

                            loadset.TMP_Football = DateTime.Today;
                            loadset.TMP_Basketball = DateTime.Today;


                            winsdb.SubmitChanges();



                            InjectWins.Add(loadset);

                            if (loadset.GroupOwner != null && loadset.GroupOwner != "")
                            {
                                QqWindowHelper qh = new QqWindowHelper(hwnd, "", false);
                                qh.MainUI = this;
                                qh.ReloadMembers(loadset.GroupOwner, RunnerF.MemberSource, loadset.WX_SourceType, db, hwnd);
                            }
                        }



                    }//内存对象无
                    else
                    {


                    }
                }//剔除无效的窗口







            }//符合条件的窗口
            return true;
        }

        private void SendPicThreadDo()
        {
            while (true)
            {
                try
                {
                    //NetFramework.WindowsApi.EnumWindows(new NetFramework.WindowsApi.CallBack(SendPicEnumWinsCallBack), 0);

                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine(AnyError.Message, true);

                }
            }

        }
        public bool SendPicEnumWins(Linq.ProgramLogic.ShiShiCaiMode csubm)
        {
            try
            {
                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;
                //foreach (Linq.WX_PCSendPicSetting wins in InjectWins)
                //{
                //    StringBuilder RAW = new StringBuilder(512);
                //    NetFramework.WindowsApi.GetWindowText(new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)), RAW, 512);
                //    if (RAW.ToString() == "")
                //    {
                //        wins.Can_Use = false;
                //    }
                //}

                //var todelete = InjectWins.Where(t => t.Can_Use == false).ToArray();
                //foreach (var item in todelete)
                //{
                //    InjectWins.Remove(item);
                //}
                foreach (Linq.WX_PCSendPicSetting wins in InjectWins)
                {
                    StringBuilder RAW = new StringBuilder(512);
                    NetFramework.WindowsApi.GetWindowText(new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)), RAW, 512);
                    //if (RAW.ToString() == "")
                    //{
                    //    continue;
                    //}
                    //NetFramework.WindowsApi.SetWindowText(new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)), "智能发图" + RAW.ToString().Replace("智能发图", ""));
                    if (
                        //(DateTime.Now.Hour * 60 + DateTime.Now.Minute + (DateTime.Now.Hour <= 8 ? 60 * 24 : 0) > wins.EndHour * 60 + wins.EndMinute + 2
                        //      + (wins.EndHour <= 4 ? 60 * 24 : 0)
                        //      )

                        //||
                        //(DateTime.Now.Hour * 60 + DateTime.Now.Minute + (DateTime.Now.Hour <= 8 ? 60 * 24 : 0) < wins.OpenHour * 60 + wins.OpenMinue
                        //      + (wins.OpenHour <= 6 ? 60 * 24 : 0)

                        //)
                        Linq.ProgramLogic.TimeInDuring(wins.OpenHour, wins.OpenMinue, wins.EndHour, wins.EndMinute) == false
                        // Linq.ProgramLogic.TimeCanUse(7, 0, 3, 15) == false
                        )
                    {
                        NetFramework.Console.WriteLine("不在时间:" + wins.OpenHour.ToString() + ":" + wins.OpenMinue.ToString() + "-" + wins.EndHour.ToString() + ":" + wins.EndMinute.ToString(), true);
                        continue;

                    }
                    if (wins.OpenResult == false)
                    {
                        continue;
                    }
                    switch (csubm)
                    {
                        case WeixinRoboot.Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩:
                            if (wins.ChongqingMode == false || wins.ChongqingMode == null)
                            {
                                continue;
                            }
                            break;
                        case WeixinRoboot.Linq.ProgramLogic.ShiShiCaiMode.五分彩:
                            if (wins.FiveMinuteMode == false || wins.FiveMinuteMode == null)
                            {
                                continue;
                            }
                            break;
                        case WeixinRoboot.Linq.ProgramLogic.ShiShiCaiMode.香港时时彩:
                            if (wins.HkMode == false || wins.HkMode == null)
                            {
                                continue;
                            }
                            break;
                        case WeixinRoboot.Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5:
                            if (wins.AozcMode == false || wins.AozcMode == null)
                            {
                                continue;
                            }
                            break;
                        case WeixinRoboot.Linq.ProgramLogic.ShiShiCaiMode.腾讯十分:
                            if (wins.Tengxunshifen == false || wins.Tengxunshifen == null)
                            {
                                continue;
                            }
                            break;

                        case WeixinRoboot.Linq.ProgramLogic.ShiShiCaiMode.腾讯五分:
                            if (wins.Tengxunwufen == false || wins.Tengxunwufen == null)
                            {
                                continue;
                            }
                            break;

                        case WeixinRoboot.Linq.ProgramLogic.ShiShiCaiMode.北京赛车PK10:
                            if (wins.BeijingsaichePK10 == false || wins.BeijingsaichePK10 == null)
                            {
                                continue;
                            }
                            break;

                        case WeixinRoboot.Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩:
                            if (wins.VRChongqing == false || wins.VRChongqing == null)
                            {
                                continue;
                            }
                            break;
                        case WeixinRoboot.Linq.ProgramLogic.ShiShiCaiMode.新疆时时彩:
                            if (wins.XinJiangMode == false || wins.XinJiangMode == null)
                            {
                                continue;
                            }
                            break;
                        case WeixinRoboot.Linq.ProgramLogic.ShiShiCaiMode.腾十信:
                            if (wins.TengxunshifenXin == false || wins.TengxunshifenXin == null)
                            {
                                continue;
                            }
                            break;
                        case WeixinRoboot.Linq.ProgramLogic.ShiShiCaiMode.腾五信:
                            if (wins.TengxunwufenXin == false || wins.TengxunwufenXin == null)
                            {
                                continue;
                            }
                            break;
                        default:
                            continue;
                            break;
                    }


                    if (wins.NumberDragonTxt == true)
                    {
                        if (((PCSourceType)Enum.Parse(typeof(PCSourceType), wins.WX_SourceType)) == PCSourceType.PC微)
                        {
                            hwndSendText(ReadVirtualFile("Data数字龙虎" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), csubm)) + ".txt", winsdb), new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)));


                        }
                        else if (((PCSourceType)Enum.Parse(typeof(PCSourceType), wins.WX_SourceType)) == PCSourceType.PCQ)
                        {
                            hwndSendText(ReadVirtualFile("Data数字龙虎qq" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), csubm)) + ".txt", winsdb), new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)));


                        }
                        else
                        {
                            hwndSendText(ReadVirtualFile("Data数字龙虎dingding" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), csubm)) + ".txt", winsdb), new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)));
                        }
                    }


                    if (wins.NiuNiuPic == true)
                    {
                        if (((PCSourceType)Enum.Parse(typeof(PCSourceType), wins.WX_SourceType)) == PCSourceType.PC微)
                        {
                            hwndSendText(ReadVirtualFile("Data数字龙虎_五分龙虎Vr牛牛" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), csubm)) + ".txt", winsdb), new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)));


                        }
                        else if (((PCSourceType)Enum.Parse(typeof(PCSourceType), wins.WX_SourceType)) == PCSourceType.PCQ)
                        {
                            hwndSendText(ReadVirtualFile("Data数字龙虎qq_五分龙虎Vr牛牛" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), csubm)) + ".txt", winsdb), new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)));


                        }
                        else
                        {
                            hwndSendText(ReadVirtualFile("Data数字龙虎dingding_五分龙虎Vr牛牛" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), csubm)) + ".txt", winsdb), new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)));
                        }
                    }
                    if (wins.NoBigSmallSingleDoublePIC == true)
                    {
                        if (((PCSourceType)Enum.Parse(typeof(PCSourceType), wins.WX_SourceType)) == PCSourceType.PC微)
                        {
                            hwndSendText(ReadVirtualFile("Data数字龙虎_五分龙虎" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), csubm)) + ".txt", winsdb), new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)));


                        }
                        else if (((PCSourceType)Enum.Parse(typeof(PCSourceType), wins.WX_SourceType)) == PCSourceType.PCQ)
                        {
                            hwndSendText(ReadVirtualFile("Data数字龙虎qq_五分龙虎" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), csubm)) + ".txt", winsdb), new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)));


                        }
                        else
                        {
                            hwndSendText(ReadVirtualFile("Data数字龙虎dingding_五分龙虎" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), csubm)) + ".txt", winsdb), new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)));
                        }
                    }


                    if (wins.NumberText == true)
                    {
                        hwndSendText(ReadVirtualFile("Data数字龙虎" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), csubm)) + "V7.txt", winsdb), new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)));
                    }



                    Thread.Sleep(200);
                    //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

                    if (wins.DragonTigerOne == true)
                    {
                        Linq.ProgramLogic.ShiShiCaiMode subm = Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩;
                        if (wins.ChongqingMode == true)
                        {
                            subm = Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩;
                        }
                        else if (wins.FiveMinuteMode == true)
                        {
                            subm = Linq.ProgramLogic.ShiShiCaiMode.五分彩;
                        }
                        else if (wins.HkMode == true)
                        {
                            subm = Linq.ProgramLogic.ShiShiCaiMode.香港时时彩;
                        }
                        else if (wins.AozcMode == true)
                        {
                            subm = Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5;
                        }

                        else if (wins.Tengxunshifen == true)
                        {
                            subm = Linq.ProgramLogic.ShiShiCaiMode.腾讯十分;
                        }
                        else if (wins.Tengxunwufen == true)
                        {
                            subm = Linq.ProgramLogic.ShiShiCaiMode.腾讯五分;
                        }
                        else if (wins.VRChongqing == true)
                        {
                            subm = Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩;
                        }



                        Linq.Game_Result gr = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                            && t.GameName == Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), subm)
                            )
                            .OrderByDescending(t => t.GamePeriod).First();
                        if (gr.DragonTiger == "龙")
                        {
                            hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\龙.gif", new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)));
                        }
                        else if (gr.DragonTiger == "虎")
                        {
                            hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\虎.gif", new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)));
                        }
                        else if (gr.DragonTiger == "合")
                        {
                            hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\合.gif", new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)));
                        }
                    }


                    if (wins.OpenPlease == true)
                    {
                        Thread.Sleep(2000);

                        hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\开始下注.gif", new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)));

                    }
                    if (wins.NumberPIC == true)
                    {

                        lock (FileLock)
                        {
                            FileLock = !Convert.ToBoolean(FileLock);
                            byte[] R = ReadVirtualFilebyte("Data" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), csubm)) + ".jpg", db);
                            MemoryStream ms = new MemoryStream(R);
                            Image image = Image.FromStream(ms);
                            if (File.Exists(Application.StartupPath + "\\0.jpg"))
                            {
                                File.Delete(Application.StartupPath + "\\0.jpg");
                            }
                            image.Save(Application.StartupPath + "\\0.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                        hwndSendImageFile(Application.StartupPath + "\\0.jpg", new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)));
                    }



                    Thread.Sleep(200);


                    if (wins.DragonPIC == true)
                    {
                        if (((PCSourceType)Enum.Parse(typeof(PCSourceType), wins.WX_SourceType)) == PCSourceType.PC微)
                        {
                            hwndSendText(ReadVirtualFile("Data3" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), csubm)) + ".txt", db), new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)));
                        }
                        else if (((PCSourceType)Enum.Parse(typeof(PCSourceType), wins.WX_SourceType)) == PCSourceType.PCQ)
                        {
                            hwndSendText(ReadVirtualFile("Data3_qq" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), csubm)) + ".txt", db), new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)));

                        }
                        else //if ((wins.WinClassName == "ChatWnd" || wins.WinClassName == "LDPlayerMainFrame") )
                        {
                            hwndSendText(ReadVirtualFile("Data3_dingding" + GlobalParam.UserName + "_" + (Enum.GetName(typeof(Linq.ProgramLogic.ShiShiCaiMode), csubm)) + ".txt", db), new IntPtr(Convert.ToInt32(wins.WX_UserTMPID)));

                        }
                    }
                    Thread.Sleep(200);



                }
            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine(AnyError.Message, true);

            }
            return true;
        }

        public bool FindChildsCallBack(IntPtr hwnd, int lParam)
        {
            return false;
        }


        private void hwndDragFile(string FileImage, IntPtr hwnd)
        {
            // lock (GlobalParam.KeyBoardLocking)
            {
                try
                {



                    Clipboard.Clear();

                    StringBuilder RAW = new StringBuilder(512);

                    Int32 winstate = NetFramework.WindowsApi.GetWindowText(hwnd, RAW, 512);
                    if (winstate == 0)
                    {
                        return;
                    }


                    System.Collections.Specialized.StringCollection sc = new System.Collections.Specialized.StringCollection();
                    sc.Add(FileImage);
                    Clipboard.SetFileDropList(sc);


                    NetFramework.WindowsApi.ShowWindow(hwnd, 1);
                    NetFramework.WindowsApi.SwitchToThisWindow(hwnd, true);
                    NetFramework.WindowsApi.SetForegroundWindow(hwnd);





                    Thread.Sleep(500);

                    NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_MENU, 0, 0, 0);
                    NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_S, 0, 0, 0);

                    Thread.Sleep(10);
                    Application.DoEvents();
                    NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_S, 0, 2, 0);
                    NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_MENU, 0, 2, 0);


                    NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 0, 0);
                    NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 0, 0);

                    Thread.Sleep(10);

                    NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 2, 0);
                    NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 2, 0);

                    Thread.Sleep(500);

                    NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_RETURN, 0, 0, 0);

                    Thread.Sleep(10);

                    NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_RETURN, 0, 2, 0);



                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine("复制失败" + AnyError.Message, true);

                }

            }

        }
        private object AdbFileLock = false;
        private void hwndSendTextFile(string FileText, IntPtr hwnd)
        {
            // lock (GlobalParam.KeyBoardLocking)
            {
                try
                {
                    FileStream fs = new FileStream(FileText, System.IO.FileMode.OpenOrCreate);
                    byte[] ToSend = new byte[fs.Length];
                    fs.Read(ToSend, 0, ToSend.Length);
                    fs.Close();
                    fs.Dispose();
                    string SendText = Encoding.UTF8.GetString(ToSend);
                    var findenums = InjectWins.Where(t => t.WX_UserTMPID == hwnd.ToString());
                    foreach (var findenum in findenums)
                    {
                        if (findenum != null && cb_adbleidianmode.Checked == true && findenum.WX_SourceType == Enum.GetName(typeof(PCSourceType), PCSourceType.雷电))
                        {
                            AdbSendText(findenum.WX_UserName, PCSourceType.雷电, FileText);
                            return;

                        }
                        if (findenum != null && cb_adbnoxmode.Checked == true && findenum.WX_SourceType == Enum.GetName(typeof(PCSourceType), PCSourceType.夜神))
                        {
                            AdbSendText(findenum.WX_UserName, PCSourceType.夜神, FileText);
                            return;
                        }
                        #region "复制发图"

                        {
                            this.Invoke(new Action(() =>
                            {

                                Clipboard.Clear();
                                StringBuilder RAW = new StringBuilder(512);
                                Int32 winstate = NetFramework.WindowsApi.GetWindowText(hwnd, RAW, 512);
                                if (winstate == 0)
                                {
                                    return;
                                }

                                NetFramework.WindowsApi.ShowWindow(hwnd, 1);
                                NetFramework.WindowsApi.SwitchToThisWindow(hwnd, true);
                                NetFramework.WindowsApi.SetForegroundWindow(hwnd);


                                Thread.Sleep(1000);
                                //Thread.Sleep(200);

                                //NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_MENU, 0, 0, 0);
                                //NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_S, 0, 0, 0);

                                //Thread.Sleep(50);
                                //Application.DoEvents();
                                //NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_S, 0, 2, 0);
                                //NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_MENU, 0, 2, 0);

                                #region
                                //Int32 CurIndex = 0;

                                //byte[] Dragon = (new byte[] { 240, 159, 144 });
                                //byte[] Ok = (new byte[] { 240, 159, 136 });
                                //byte[] Tiger = (new byte[] { 238, 129, 144 });

                                //while (CurIndex < ToSend.Length)
                                //{
                                //    byte[] test = (new byte[] { ToSend[CurIndex], ToSend[CurIndex + 1], ToSend[CurIndex + 2] });

                                //    Application.DoEvents();

                                //    if (bytecompare(test, Dragon))
                                //    {
                                //        Clipboard.Clear();
                                //        Clipboard.SetText(Linq.DataLogic.Dragon, TextDataFormat.UnicodeText);
                                //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 0, 0);
                                //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 0, 0);
                                //        Thread.Sleep(50);
                                //        Application.DoEvents();
                                //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 2, 0);
                                //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 2, 0);

                                //        CurIndex += 4;
                                //    }
                                //    else if (bytecompare(test, Ok))
                                //    {
                                //        Clipboard.Clear();
                                //        Clipboard.SetText(Linq.DataLogic.OK, TextDataFormat.UnicodeText);
                                //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 0, 0);
                                //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 0, 0);
                                //        Thread.Sleep(50);
                                //        Application.DoEvents();
                                //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 2, 0);
                                //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 2, 0);

                                //        CurIndex += 4;
                                //    }
                                //    else if (bytecompare(test, Tiger))
                                //    {
                                //        Clipboard.Clear();

                                //        StringBuilder RAW = new StringBuilder(512);
                                //        NetFramework.WindowsApi.GetWindowText(hwnd, RAW, 512);

                                //        if (RAW.ToString().Contains("钉钉"))
                                //        {
                                //            Clipboard.SetText(Linq.DataLogic.Tiger_dingding, TextDataFormat.UnicodeText);
                                //        }
                                //        else
                                //        {
                                //            Clipboard.SetText(Linq.DataLogic.Tiger, TextDataFormat.Text);
                                //        }

                                //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 0, 0);
                                //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 0, 0);
                                //        Thread.Sleep(50);
                                //        Application.DoEvents();
                                //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 2, 0);
                                //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 2, 0);

                                //        CurIndex += 3;
                                //    }
                                //    else
                                //    {
                                //        Clipboard.Clear();
                                //        Clipboard.SetText(Encoding.UTF8.GetString(ToSend, CurIndex, ToSend.Length - CurIndex), TextDataFormat.UnicodeText);
                                //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 0, 0);
                                //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 0, 0);
                                //        Thread.Sleep(100);
                                //        Application.DoEvents();
                                //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 2, 0);
                                //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 2, 0);

                                //        CurIndex = ToSend.Length;
                                //    }



                                //}
                                #endregion


                                Clipboard.SetText((SendText.StartsWith(Linq.ProgramLogic.Tiger) ? " " : "") + SendText);
                                Thread.Sleep(50);
                                #region 模拟点击
                                //NetFramework.WindowsApi.Rect pos = new NetFramework.WindowsApi.Rect();
                                //NetFramework.WindowsApi.GetWindowRect(hwnd, out pos);

                                //NetFramework.WindowsApi.SetCursorPos(
                                //     pos.Right - 150
                                //    , pos.Bottom - 20
                                //    );//移动到需要点击的位置
                                //Thread.Sleep(10);
                                //NetFramework.WindowsApi.mouse_event(NetFramework.WindowsApi.MOUSEEVENTF_LEFTDOWN | NetFramework.WindowsApi.MOUSEEVENTF_ABSOLUTE
                                //    , 0
                                //    , 0
                                //    , 0, 0);//点击
                                //Thread.Sleep(10);
                                //NetFramework.WindowsApi.mouse_event(NetFramework.WindowsApi.MOUSEEVENTF_LEFTUP | NetFramework.WindowsApi.MOUSEEVENTF_ABSOLUTE
                                //     , 0
                                //    , 0
                                //    , 0, 0);//抬起

                                //Thread.Sleep(100);

                                #endregion


                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 0, 0);
                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 0, 0);
                                Thread.Sleep(10);

                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 2, 0);
                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 2, 0);
                                Thread.Sleep(50);

                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_RETURN, 0, 0, 0);

                                Thread.Sleep(10);

                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_RETURN, 0, 2, 0);
                            }));// invoke 结束
                        }
                        #endregion
                    }




                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine("复制失败" + AnyError.Message, true);

                }
            }
        }
        private void hwndSendText(string SendText, IntPtr hwnd)
        {
            if (SendText == "" || SendText == null)
            {
                return;
            }
            //System.Threading.Tasks.Task tak = new System.Threading.Tasks.Task(new Action(() =>
            //{





            // lock (GlobalParam.KeyBoardLocking)
            {
                try
                {


                    this.Invoke(new Action(() =>
                    {

                        //if (Setting == true)
                        //{
                        //    return;
                        //}

                        StringBuilder RAW = new StringBuilder(512);
                        //Int32 winstate = NetFramework.WindowsApi.GetWindowText(hwnd, RAW, 512);
                        //if (winstate == 0)
                        //{
                        //    return;
                        //}


                        NetFramework.WindowsApi.ShowWindow(hwnd, 1);

                        NetFramework.WindowsApi.SwitchToThisWindow(hwnd, true);
                        NetFramework.WindowsApi.SetForegroundWindow(hwnd);


                        Thread.Sleep(50);

                        //NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_MENU, 0, 0, 0);
                        //NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_S, 0, 0, 0);

                        //Thread.Sleep(50);
                        //Application.DoEvents();
                        //NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_S, 0, 2, 0);
                        //NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_MENU, 0, 2, 0);
                        #region

                        //byte[] ToSend = Encoding.UTF8.GetBytes(SendText);
                        //Int32 CurIndex = 0;

                        //byte[] Dragon = (new byte[] { 240, 159, 144 });
                        //byte[] Ok = (new byte[] { 240, 159, 136 });
                        //byte[] Tiger = (new byte[] { 238, 129, 144 });


                        //while (CurIndex < ToSend.Length)
                        //{
                        //    byte[] test = (new byte[] { ToSend[CurIndex], ToSend[CurIndex + 1], ToSend[CurIndex + 2] });

                        //    Application.DoEvents();

                        //    if (bytecompare(test, Dragon))
                        //    {
                        //        Clipboard.Clear();
                        //        Clipboard.SetText(Linq.DataLogic.Dragon, TextDataFormat.UnicodeText);
                        //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 0, 0);
                        //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 0, 0);
                        //        Thread.Sleep(50);
                        //        Application.DoEvents();
                        //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 2, 0);
                        //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 2, 0);

                        //        CurIndex += 4;
                        //    }
                        //    else if (bytecompare(test, Ok))
                        //    {
                        //        Clipboard.Clear();
                        //        Clipboard.SetText(Linq.DataLogic.OK, TextDataFormat.UnicodeText);
                        //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 0, 0);
                        //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 0, 0);
                        //        Thread.Sleep(50);
                        //        Application.DoEvents();
                        //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 2, 0);
                        //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 2, 0);

                        //        CurIndex += 4;
                        //    }
                        //    else if (bytecompare(test, Tiger))
                        //    {
                        //        Clipboard.Clear();
                        //        StringBuilder RAW = new StringBuilder(512);
                        //        NetFramework.WindowsApi.GetWindowText(hwnd, RAW, 512);

                        //        if (RAW.ToString().Contains("钉钉"))
                        //        {
                        //            Clipboard.SetText(Linq.DataLogic.Tiger_dingding, TextDataFormat.UnicodeText);
                        //        }
                        //        else
                        //        {
                        //            Clipboard.SetText(Linq.DataLogic.Tiger, TextDataFormat.Text);
                        //        }
                        //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 0, 0);
                        //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 0, 0);
                        //        Thread.Sleep(50);
                        //        Application.DoEvents();
                        //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 2, 0);
                        //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 2, 0);

                        //        CurIndex += 3;
                        //    }
                        //    else
                        //    {
                        //        Clipboard.Clear();
                        //        Clipboard.SetText(Encoding.UTF8.GetString(ToSend, CurIndex, ToSend.Length - CurIndex), TextDataFormat.UnicodeText);
                        //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 0, 0);
                        //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 0, 0);
                        //        Thread.Sleep(100);
                        //        Application.DoEvents();
                        //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 2, 0);
                        //        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 2, 0);

                        //        CurIndex = ToSend.Length;
                        //    }



                        //}
                        #endregion



                        #region adb发送
                        #region 保存成文件
                        //lock (AdbFileLock)
                        {
                            //AdbFileLock = !(Convert.ToBoolean(AdbFileLock));
                            if (Directory.Exists(Application.StartupPath + "\\EmuFile") == false)
                            {
                                Directory.CreateDirectory(Application.StartupPath + "\\EmuFile");
                            }

                            Guid NewFileName = Guid.NewGuid();
                            //FileStream fs = new FileStream(Application.StartupPath + "\\EmuFile" + "\\" + NewFileName + ".txt", FileMode.OpenOrCreate);
                            //Byte[] tosend = Encoding.UTF8.GetBytes(SendText);
                            //fs.Write(tosend, 0, tosend.Length);


                            //fs.Flush();
                            //fs.Close();

                            //fs.Dispose();

                            File.WriteAllText(Application.StartupPath + "\\EmuFile" + "\\" + NewFileName + ".txt", SendText);
                            Thread.Sleep(500);
                        #endregion
                            var findenums = InjectWins.Where(t => t.WX_UserTMPID == hwnd.ToString());
                            foreach (var findenum in findenums)
                            {
                                if (findenum != null && cb_adbleidianmode.Checked == true && findenum.WX_SourceType == Enum.GetName(typeof(PCSourceType), PCSourceType.雷电))
                                {
                                    AdbSendText(findenum.WX_UserName, PCSourceType.雷电, Application.StartupPath + "\\EmuFile" + "\\" + NewFileName + ".txt");

                                    return;

                                }
                                if (findenum != null && cb_adbnoxmode.Checked == true && findenum.WX_SourceType == Enum.GetName(typeof(PCSourceType), PCSourceType.夜神))
                                {
                                    AdbSendText(findenum.WX_UserName, PCSourceType.夜神, Application.StartupPath + "\\EmuFile" + "\\" + NewFileName + ".txt");

                                    return;
                                }
                            }
                        #endregion
                        }//lock

                        Clipboard.Clear();
                        Clipboard.SetText((SendText.StartsWith(Linq.ProgramLogic.Tiger) ? " " : "") + SendText);

                        Thread.Sleep(50);
                        #region 模拟点击
                        //NetFramework.WindowsApi.Rect pos = new NetFramework.WindowsApi.Rect();
                        //NetFramework.WindowsApi.GetWindowRect(hwnd, out pos);

                        //NetFramework.WindowsApi.SetCursorPos(
                        //     pos.Right - 150
                        //    , pos.Bottom - 20
                        //    );//移动到需要点击的位置
                        //Thread.Sleep(10);
                        //NetFramework.WindowsApi.mouse_event(NetFramework.WindowsApi.MOUSEEVENTF_LEFTDOWN | NetFramework.WindowsApi.MOUSEEVENTF_ABSOLUTE
                        //    , 0
                        //    , 0
                        //    , 0, 0);//点击
                        //Thread.Sleep(10);
                        //NetFramework.WindowsApi.mouse_event(NetFramework.WindowsApi.MOUSEEVENTF_LEFTUP | NetFramework.WindowsApi.MOUSEEVENTF_ABSOLUTE
                        //     , 0
                        //    , 0
                        //    , 0, 0);//抬起

                        //Thread.Sleep(100);

                        #endregion
                        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 0, 0);
                        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 0, 0);
                        Thread.Sleep(10);

                        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 2, 0);
                        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 2, 0);
                        Thread.Sleep(200);


                        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_RETURN, 0, 0, 0);

                        Thread.Sleep(10);

                        NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_RETURN, 0, 2, 0);

                    }));// invoke 结束

                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine("复制失败" + AnyError.Message, true);
                }
            }//lock
            //}));//action
            //tak.Start();
        }

        private void AdbSendText(string EnumName, PCSourceType EnumType, string PCFilePath)
        {
            System.IO.FileStream FS = new FileStream(PCFilePath, FileMode.OpenOrCreate);
            if (FS.Length == 0)
            {
                return;
            }

            if (EnumType == PCSourceType.雷电)
            {


                //ldconsole.exe action --name 高三二班重庆彩 --key call.input --value 中国123
                //ldconsole.exe adb --name 高三二班重庆彩  --command "shell input tap 332 616 "
                //ldconsole.exe adb --name 高三二班重庆彩  --command " shell uiautomator dump /sdcard/1.xml"

                // adb shell rm /sdcard/0.txt
                //adb push D:\file.txt system/
                // adb shell am broadcast -a ADB_INPUT_SF --es msg 0.txt

                //CmdRun(tb_LeidianPath.Text, " ldconsole.exe adb --name " + EnumName + "  --command \"  shell ime set com.android.adbkeyboard/.AdbIME \" ");


                CmdRun(tb_LeidianPath.Text, "ldconsole.exe " + "adb --name " + EnumName + " --command  \"  shell rm /sdcard/0.txt  \"");




                CmdRun(tb_LeidianPath.Text, "ldconsole.exe " + "adb --name " + EnumName + " --command  \"  push " + PCFilePath + " /sdcard/0.txt  \"");

                //CmdRun(tb_LeidianPath.Text, "ldconsole.exe adb --name " + EnumName + "  --command \"shell input tap 90 440 \"");


                CmdRun(tb_LeidianPath.Text, "ldconsole.exe " + "adb --name " + EnumName + " --command  \" shell am broadcast -a ADB_INPUT_SF --es msg  0.txt \"");
                //CmdRun(tb_LeidianPath.Text, "ldconsole.exe adb --name " + EnumName + "  --command \"shell input tap 163 425 \"");



            }
            if (EnumType == PCSourceType.夜神)
            {
                //NoxConsole.exe action -name:夜神模拟器 -key:call.input -value:"中国123"
                //NoxConsole.exe adb -name:夜神模拟器 -command:" shell input tap 332 616"
                //NoxConsole.exe adb -name:夜神模拟器 -command:" shell uiautomator dump /sdcard/1.xml"

                //CmdRun(tb_LeidianPath.Text, "ldconsole.exe " + "adb -name:" + EnumName + " --command: \" shell am broadcast -a ADB_INPUT_CHARS --eia chars '" + ToAdb + "'\"");
                File.Delete(PCFilePath);
            }


        }

        private void AdbSendImage(string EnumName, PCSourceType EnumType, string PCFilePath)
        {
            System.IO.FileStream FS = new FileStream(PCFilePath, FileMode.OpenOrCreate);
            if (FS.Length == 0)
            {
                return;
            }
            if (EnumType == PCSourceType.雷电)
            {
                //ldconsole.exe action --name 高三二班重庆彩 --key call.input --value 中国123
                //ldconsole.exe adb --name 高三二班重庆彩  --command "shell input tap 332 616 "
                //ldconsole.exe adb --name 高三二班重庆彩  --command " shell uiautomator dump /sdcard/1.xml"

                // adb shell rm /sdcard/0.txt
                //adb push D:\file.txt system/
                // adb shell am broadcast -a ADB_INPUT_SF --es msg 0.txt

                CmdRun(tb_LeidianPath.Text, " ldconsole.exe adb --name " + EnumName + "  --command \"  shell ime set com.android.adbkeyboard/.AdbIME \" ");


                CmdRun(tb_LeidianPath.Text, "ldconsole.exe " + "adb --name " + EnumName + " --command  \"  shell rm /sdcard/0.jpg  \"");




                CmdRun(tb_LeidianPath.Text, "ldconsole.exe " + "adb --name " + EnumName + " --command  \"  push " + PCFilePath + " /sdcard/0.jpg  \"");

                CmdRun(tb_LeidianPath.Text, "ldconsole.exe " + "adb --name " + EnumName + " --command  \"   shell am broadcast -a android.intent.action.MEDIA_MOUNTED -d file:///sdcard/  \"");

                CmdRun(tb_LeidianPath.Text, "ldconsole.exe adb --name " + EnumName + "  --command \"shell input tap 90 452 \"");
                Thread.Sleep(50);
                CmdRun(tb_LeidianPath.Text, "ldconsole.exe adb --name " + EnumName + "  --command \"shell input tap 163 429 \"");
                Thread.Sleep(50);
                CmdRun(tb_LeidianPath.Text, "ldconsole.exe adb --name " + EnumName + "  --command \"shell input tap 37 368 \"");
                Thread.Sleep(50);
                CmdRun(tb_LeidianPath.Text, "ldconsole.exe adb --name " + EnumName + "  --command \"shell input tap 27 71 \"");
                Thread.Sleep(50);
                CmdRun(tb_LeidianPath.Text, "ldconsole.exe adb --name " + EnumName + "  --command \"shell input tap 155 30 \"");
            }
            if (EnumType == PCSourceType.夜神)
            {
                //NoxConsole.exe action -name:夜神模拟器 -key:call.input -value:"中国123"
                //NoxConsole.exe adb -name:夜神模拟器 -command:" shell input tap 332 616"
                //NoxConsole.exe adb -name:夜神模拟器 -command:" shell uiautomator dump /sdcard/1.xml"

                //CmdRun(tb_LeidianPath.Text, "ldconsole.exe " + "adb -name:" + EnumName + " --command: \" shell am broadcast -a ADB_INPUT_CHARS --eia chars '" + ToAdb + "'\"");

            }


        }
        private void hwndSendImageFile(string FileImage, IntPtr hwnd)
        {
            // lock (GlobalParam.KeyBoardLocking)
            {
                try
                {
                    var findenums = InjectWins.Where(t => t.WX_UserTMPID == hwnd.ToString());
                    foreach (var findenum in findenums)
                    {
                        if (findenum != null && cb_adbleidianmode.Checked == true && findenum.WX_SourceType == Enum.GetName(typeof(PCSourceType), PCSourceType.雷电))
                        {
                            AdbSendImage(findenum.WX_UserName, PCSourceType.雷电, FileImage);
                        }
                        if (findenum != null && cb_adbnoxmode.Checked == true && findenum.WX_SourceType == Enum.GetName(typeof(PCSourceType), PCSourceType.夜神))
                        {
                            AdbSendImage(findenum.WX_UserName, PCSourceType.雷电, FileImage);

                        }
                        if (findenum.WX_SourceType != "雷电" && findenum.WX_SourceType != "夜神")
                        {

                            this.Invoke(new Action(() =>
                            {

                                Clipboard.Clear();
                                StringBuilder RAW = new StringBuilder(512);
                                Int32 winstate = NetFramework.WindowsApi.GetWindowText(hwnd, RAW, 512);
                                if (winstate == 0)
                                {
                                    return;
                                }

                                //System.Collections.Specialized.StringCollection sc = new System.Collections.Specialized.StringCollection();
                                //sc.Add(FileImage);
                                //Clipboard.SetFileDropList(sc);
                                Image tocpyimage = Image.FromFile(FileImage);
                                System.Drawing.Bitmap bp = new Bitmap(tocpyimage);
                                Clipboard.SetData("System.Drawing.Bitmap", bp);


                                NetFramework.WindowsApi.ShowWindow(hwnd, 1);
                                NetFramework.WindowsApi.SetForegroundWindow(hwnd);
                                NetFramework.WindowsApi.SetActiveWindow(hwnd);
                                NetFramework.WindowsApi.SwitchToThisWindow(hwnd, true);

                                Thread.Sleep(50);

                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 0, 0);
                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 0, 0);

                                Thread.Sleep(10);

                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 2, 0);
                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 2, 0);

                                bp.Dispose();

                                tocpyimage.Dispose();
                                bp = null;
                                tocpyimage = null;


                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_RETURN, 0, 0, 0);

                                Thread.Sleep(10);

                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_RETURN, 0, 2, 0);



                            }));// invoke 结束
                        }
                    }

                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine("复制失败" + AnyError.Message, true);
                }
            }
        }
        private void hwndSendImageFileByte(byte[] FileImageByte, IntPtr hwnd)
        {
            // lock (GlobalParam.KeyBoardLocking)
            {
                try
                {
                    var findenums = InjectWins.Where(t => t.WX_UserTMPID == hwnd.ToString());
                    foreach (var findenum in findenums)
                    {
                        MemoryStream ms = new MemoryStream(FileImageByte);
                        Image tosave = Image.FromStream(ms);
                        string NewFileName = Application.StartupPath + "\\EmuFile\\" + Guid.NewGuid().ToString() + ".jpg";
                        tosave.Save(NewFileName);
                        ms.Close();
                        ms.Dispose();
                        tosave.Dispose();

                        if (findenum != null && cb_adbleidianmode.Checked == true && findenum.WX_SourceType == Enum.GetName(typeof(PCSourceType), PCSourceType.雷电))
                        {
                            AdbSendImage(findenum.WX_UserName, PCSourceType.雷电, NewFileName);
                        }
                        if (findenum != null && cb_adbnoxmode.Checked == true && findenum.WX_SourceType == Enum.GetName(typeof(PCSourceType), PCSourceType.夜神))
                        {
                            AdbSendImage(findenum.WX_UserName, PCSourceType.雷电, NewFileName);

                        }
                        if (findenum.WX_SourceType != "雷电" && findenum.WX_SourceType != "夜神")
                        {

                            this.Invoke(new Action(() =>
                            {

                                Clipboard.Clear();
                                StringBuilder RAW = new StringBuilder(512);
                                Int32 winstate = NetFramework.WindowsApi.GetWindowText(hwnd, RAW, 512);
                                if (winstate == 0)
                                {
                                    return;
                                }

                                //System.Collections.Specialized.StringCollection sc = new System.Collections.Specialized.StringCollection();
                                //sc.Add(FileImage);
                                //Clipboard.SetFileDropList(sc);
                                Image tocpyimage = Image.FromFile(NewFileName);
                                System.Drawing.Bitmap bp = new Bitmap(tocpyimage);
                                Clipboard.SetData("System.Drawing.Bitmap", bp);


                                NetFramework.WindowsApi.ShowWindow(hwnd, 1);
                                NetFramework.WindowsApi.SetForegroundWindow(hwnd);
                                NetFramework.WindowsApi.SetActiveWindow(hwnd);
                                NetFramework.WindowsApi.SwitchToThisWindow(hwnd, true);

                                Thread.Sleep(50);

                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 0, 0);
                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 0, 0);

                                Thread.Sleep(10);

                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 2, 0);
                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 2, 0);

                                bp.Dispose();

                                tocpyimage.Dispose();
                                bp = null;
                                tocpyimage = null;


                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_RETURN, 0, 0, 0);

                                Thread.Sleep(10);

                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_RETURN, 0, 2, 0);



                            }));// invoke 结束
                        }
                    }


                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine("复制失败" + AnyError.Message, true);
                }
            }
        }


        private void CheckTimeSend()
        {
            while (true)
            {
                try
                {

                    //开盘
                    //if (DateTime.Now.Hour == 9 && DateTime.Now.Minute >= 50)
                    {

                        foreach (Linq.WX_PCSendPicSetting sendwins in InjectWins)
                        {
                            StringBuilder RAW = new StringBuilder(512);
                            NetFramework.WindowsApi.GetWindowText(new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)), RAW, 512);

                            if (RAW.ToString() == "" || sendwins.OpenPIC == false
                                || Linq.ProgramLogic.TimeInDuring(sendwins.OpenHour, sendwins.OpenMinue, sendwins.EndHour, sendwins.EndMinute) == false)
                            {
                                continue;

                            }
                            if (GlobalParam.HaveSend.ContainsKey(DateTime.Today.ToString("yyyyMMdd") + new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)).ToString() + "开盘") == false)
                            {
                                hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\开盘啦.png", new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)));

                                hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\开盘啦.png", new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)));

                                hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\开盘啦.png", new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)));

                                //hwndDragFile(Application.StartupPath + "\\PCGIFS\\财源滚滚来.gif", sendwins.hwnd);

                                //hwndDragFile(Application.StartupPath + "\\PCGIFS\\开始下注.gif", sendwins.hwnd);

                                GlobalParam.HaveSend.Add(DateTime.Today.ToString("yyyyMMdd") + new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)).ToString() + "开盘", true);
                            }

                        }
                    }

                    //封盘
                    //if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour <= 5)
                    {

                        foreach (Linq.WX_PCSendPicSetting sendwins in InjectWins)
                        {
                            StringBuilder RAW = new StringBuilder(512);
                            NetFramework.WindowsApi.GetWindowText(new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)), RAW, 512);

                            if
                                ((RAW.ToString() == "" || sendwins.EndPIC == false)
                                || ((DateTime.Now.Hour * 60 + DateTime.Now.Minute + (DateTime.Now.Hour <= 8 ? 24 * 60 : 0) < sendwins.EndHour * 60 + sendwins.EndMinute + 2 + (sendwins.EndHour <= 4 ? 24 * 60 : 0)))
                                || ((DateTime.Now.Hour * 60 + DateTime.Now.Minute + (DateTime.Now.Hour <= 8 ? 24 * 60 : 0) > sendwins.EndHour * 60 + sendwins.EndMinute + 2 + (sendwins.EndHour <= 4 ? 24 * 60 : 0) + 5))


                                )
                            {
                                //不勾或没到时间不发
                                continue;

                            }
                            if (GlobalParam.HaveSend.ContainsKey(DateTime.Today.ToString("yyyyMMdd") + new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)).ToString() + "封盘") == false)
                            {
                                hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\封盘.png", new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)));
                                Thread.Sleep(50);
                                hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\封盘.png", new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)));
                                Thread.Sleep(50);
                                hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\封盘.png", new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)));
                                Thread.Sleep(50);
                                hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\正在结算.gif", new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)));
                                Thread.Sleep(50);
                                hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\正在结算.gif", new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)));
                                Thread.Sleep(50);
                                hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\正在结算.gif", new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)));
                                GlobalParam.HaveSend.Add(DateTime.Today.ToString("yyyyMMdd") + new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)).ToString() + "封盘", true);
                            }

                        }
                    }
                    //下注加
                    //if (true)
                    //{

                    //}
                    //整点停止下注
                    Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                    //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                    ////db.ObjectTrackingEnabled = false;
                    Linq.Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == DateTime.Now.ToString("HH:mm"));
                    if (testmin != null && GlobalParam.HaveSend.ContainsKey(DateTime.Today.ToString("yyyyMMdd") + testmin.PeriodIndex) == false)
                    {
                        foreach (Linq.WX_PCSendPicSetting sendwins in InjectWins)
                        {
                            StringBuilder RAW = new StringBuilder(512);
                            NetFramework.WindowsApi.GetWindowText(new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)), RAW, 512);

                            if (RAW.ToString() == "" || sendwins.BounsTimeStopPIC == false)
                            {
                                continue;

                            }
                            hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\停止下注.gif", new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)));
                        }
                        GlobalParam.HaveSend.Add(DateTime.Today.ToString("yyyyMMdd") + testmin.PeriodIndex, true);

                    }




                    //if ( HaveSend.ContainsKey(DateTime.Today.ToString("yyyyMMdd") + "最后一期") == false)
                    //{
                    foreach (Linq.WX_PCSendPicSetting sendwins in InjectWins)
                    {
                        StringBuilder RAW = new StringBuilder(512);
                        NetFramework.WindowsApi.GetWindowText(new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)), RAW, 512);

                        if (RAW.ToString() == "" || sendwins.LastPeriod == false
                            || ((DateTime.Now.Hour * 60 + DateTime.Now.Minute + (sendwins.EndHour * 60 + sendwins.EndMinute >= 600 && sendwins.EndHour * 60 + sendwins.EndMinute < 1320 ? 7 : 2) + (DateTime.Now.Hour <= 8 ? 24 * 60 : 0) < sendwins.EndHour * 60 + sendwins.EndMinute + (sendwins.EndHour <= 4 ? 24 * 60 : 0)))
                            )
                        {
                            continue;

                        }
                        if (GlobalParam.HaveSend.ContainsKey(DateTime.Today.ToString("yyyyMMdd") + new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)).ToString() + "最后一期") == false)
                        {

                            hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\最后一期.gif", new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)));
                            Thread.Sleep(200);
                            hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\最后一期.gif", new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)));
                            Thread.Sleep(200);
                            hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\最后一期.gif", new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)));

                            GlobalParam.HaveSend.Add(DateTime.Today.ToString("yyyyMMdd") + new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)).ToString() + "最后一期", true);

                        }

                    }


                    //}




                    //5分钟，10点到2点,新时间不区分
                    //if (DateTime.Now.Hour <= 1 || DateTime.Now.Hour >= 22)
                    //{
                    //    if (GlobalParam.HaveSend.ContainsKey(DateTime.Today.ToString("yyyyMMdd") + "5分钟") == false)
                    //    {
                    //        foreach (Linq.WX_PCSendPicSetting sendwins in InjectWins)
                    //        {
                    //            StringBuilder RAW = new StringBuilder(512);
                    //            NetFramework.WindowsApi.GetWindowText(new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)), RAW, 512);

                    //            if (RAW.ToString() == "" || sendwins.DuringFiveMinute == false)
                    //            {
                    //                continue;

                    //            }
                    //            if (GlobalParam.HaveSend.ContainsKey(DateTime.Today.ToString("yyyyMMdd") + new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)).ToString() + "5分钟") == false)
                    //            {
                    //                hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\5分钟.gif", new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)));
                    //                Thread.Sleep(500);
                    //                hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\5分钟.gif", new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)));
                    //                Thread.Sleep(500);
                    //                hwndSendImageFile(Application.StartupPath + "\\PCGIFS\\5分钟.gif", new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)));

                    //                GlobalParam.HaveSend.Add(DateTime.Today.ToString("yyyyMMdd") + new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)) + "5分钟", true);
                    //            }
                    //        }




                    //    }

                    //}
                    //文字1，2，3
                    foreach (Linq.WX_PCSendPicSetting CheckSendWin in InjectWins.Where(t => t.aspnet_UserID == GlobalParam.UserKey))
                    {
                        StringBuilder RAW = new StringBuilder(512);
                        NetFramework.WindowsApi.GetWindowText(new IntPtr(Convert.ToInt32(CheckSendWin.WX_UserTMPID)), RAW, 512);

                        if (RAW.ToString() == "")
                        {
                            continue;

                        }
                        if (CheckSendWin.TMP_Text1Time.Value.AddMinutes(CheckSendWin.Text1Minute.Value) <= DateTime.Now && CheckSendWin.Text1 != "" && CheckSendWin.Text1 != null)
                        {
                            if (CheckSendWin.Text1PicPath != "" && CheckSendWin.Text1PicPath != null)
                            {
                                hwndSendImageFile(CheckSendWin.Text1PicPath, new IntPtr(Convert.ToInt32(CheckSendWin.WX_UserTMPID)));
                            }
                            hwndSendText(CheckSendWin.Text1, new IntPtr(Convert.ToInt32(CheckSendWin.WX_UserTMPID)));
                            CheckSendWin.TMP_Text1Time = DateTime.Now;
                        }


                        if (CheckSendWin.TMP_Text2Time.Value.AddMinutes(CheckSendWin.Text2Minute.Value) <= DateTime.Now && CheckSendWin.Text2 != "" && CheckSendWin.Text2 != null)
                        {

                            hwndSendText(CheckSendWin.Text2, new IntPtr(Convert.ToInt32(CheckSendWin.WX_UserTMPID)));
                            CheckSendWin.TMP_Text2Time = DateTime.Now;
                        }

                        if (CheckSendWin.TMP_Text3Time.Value.AddMinutes(CheckSendWin.Text3Minute.Value) <= DateTime.Now && CheckSendWin.Text3 != "" && CheckSendWin.Text3 != null)
                        {

                            hwndSendText(CheckSendWin.Text3, new IntPtr(Convert.ToInt32(CheckSendWin.WX_UserTMPID)));
                            CheckSendWin.TMP_Text3Time = DateTime.Now;
                        }


                    }
                    //球赛图片
                    foreach (Linq.WX_PCSendPicSetting sendwins in InjectWins)
                    {
                        StringBuilder RAW = new StringBuilder(512);
                        NetFramework.WindowsApi.GetWindowText(new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)), RAW, 512);

                        if (RAW.ToString() == "")
                        {
                            continue;

                        }
                        if (sendwins.TMP_Football.Value.AddMinutes(sendwins.BallInterval.Value) <= DateTime.Now && sendwins.FootballPIC == true)
                        {
                            hwndSendImageFile(Application.StartupPath + "\\output\\" + BallType.篮球 + ".jpg", new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)));
                            sendwins.TMP_Football = DateTime.Now;
                            if (sendwins.BallLink == true)
                            {

                            }
                        }
                        if (sendwins.TMP_Basketball.Value.AddMinutes(sendwins.BallInterval.Value) <= DateTime.Now && sendwins.BasketballPIC == true)
                        {
                            hwndSendImageFile(Application.StartupPath + "\\output\\" + BallType.足球 + ".jpg", new IntPtr(Convert.ToInt32(sendwins.WX_UserTMPID)));
                            sendwins.TMP_Basketball = DateTime.Now;
                            if (sendwins.BallLink == true)
                            {

                            }
                        }

                    }

                }
                catch (Exception AnyError)
                {

                    NetFramework.Console.WriteLine(AnyError.Message, true);
                    NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                }
                Thread.Sleep(1000);

            }

        }


        bool Setting = false;

        private void MI_PCWechatSendSetting_Click(object sender, EventArgs e)
        {
            PCWeChatSendImageSetting pcs = new PCWeChatSendImageSetting();
            pcs.SF = this;
            Setting = true;
            pcs.ShowDialog();
            Setting = false;

        }

        private void Btn_ManulSend_Click(object sender, EventArgs e)
        {
            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.五分彩);
            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.香港时时彩);
            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5);
            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);

            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾讯五分);
            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.北京赛车PK10);
            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);
            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.新疆时时彩);

            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾十信);
            DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾五信);
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;

            Linq.aspnet_UsersNewGameResultSend loadset = Util_Services.GetServicesSetting();

            if (loadset.Thread_ChongQingShiShiCai == true)
            {
                SendPicEnumWins(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
            }


            if (loadset.Thread_WuFen == true)
            {
                SendPicEnumWins(Linq.ProgramLogic.ShiShiCaiMode.五分彩);
            }



            if (loadset.Thread_AoZhouCai == true)
            {
                SendPicEnumWins(Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5);
            }

            if (loadset.Thread_TengXunShiFen == true)
            {
                SendPicEnumWins(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);
            }

            if (loadset.Thread_TengXunWuFen == true)
            {
                SendPicEnumWins(Linq.ProgramLogic.ShiShiCaiMode.腾讯五分);
            }

            if (loadset.Thread_TengXunShiFenXin == true)
            {
                SendPicEnumWins(Linq.ProgramLogic.ShiShiCaiMode.腾十信);
            }

            if (loadset.Thread_TengXunWuFenXin == true)
            {
                SendPicEnumWins(Linq.ProgramLogic.ShiShiCaiMode.腾五信);
            }


            if (loadset.Thread_VRChongqing == true)
            {
                SendPicEnumWins(Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩);
            }

            if (loadset.Thread_XinJiangShiShiCai == true)
            {
                SendPicEnumWins(Linq.ProgramLogic.ShiShiCaiMode.新疆时时彩);
            }
        }
        private static bool StopQQ = false;
        private static int SleepTime = 2000;
        private void Thread_CheckQQ()
        {
            while (true)
            {
                if (StopQQ == false)
                {


                    try
                    {

                        var wins = InjectWins.Where(t => t.Is_Reply == true);
                        foreach (var item in wins)
                        {
                            String winTitle = "消息";
                            NetFramework.Console.WriteLine("----------------------------------------", false);
                            NetFramework.Console.WriteLine("开始采集" + DateTime.Now.ToString("HH:mm:ss fffF"), false);
                            WeixinRoboot.QqWindowHelper helper = new WeixinRoboot.QqWindowHelper(new IntPtr(Convert.ToInt32(item.WX_UserTMPID)), winTitle);
                            helper.MainUI = this;
                            NetFramework.Console.WriteLine("采集完成" + DateTime.Now.ToString("HH:mm:ss fffF"), false);
                            string Context = helper.GetContent();
                            NetFramework.Console.WriteLine("复制完成" + DateTime.Now.ToString("HH:mm:ss fffF"), false);
                            CheckQQConMessage(Context, new IntPtr(Convert.ToInt32(item.WX_UserTMPID)), helper.WindowName);
                            NetFramework.Console.WriteLine("解析完成" + DateTime.Now.ToString("HH:mm:ss fffF"), false);
                            NetFramework.Console.WriteLine("休眠" + (SleepTime / 1000.0m).ToString() + "秒", false);
                        }

                    }
                    catch (Exception AnyError)
                    {

                        NetFramework.Console.WriteLine(AnyError.Message, true);
                        NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                    }
                }
                Thread.Sleep(SleepTime);

            }
        }




        private void btn_runtest_Click(object sender, EventArgs e)
        {



            WeixinRoboot.QqWindowHelper helper = new WeixinRoboot.QqWindowHelper(new IntPtr(Convert.ToInt32(InjectWins[0].WX_UserTMPID)), "消息");
            helper.MainUI = this;
            string Context = helper.GetContent();
            CheckQQConMessage(Context, new IntPtr(Convert.ToInt32(InjectWins[0].WX_UserTMPID)), helper.WindowName);





            return;
            RefreshballV2(wb_ballgame, "data_main", BallType.足球, Linq.ProgramLogic.BallCompanyType.皇冠);
            RefreshballV2(wb_ballgame, "data_main", BallType.足球, Linq.ProgramLogic.BallCompanyType.澳彩);


            GetAndSetPoint(wb_balllivepoint, BallType.足球);
            GetAndSetPoint(wb_balllivepoint, BallType.篮球);

            NetFramework.Console.WriteLine("测试跑完", false);

            return;



            string TEMPUserName = "";




            DataRow[] dr = RunnerF.MemberSource.Select("User_IsReply='1'");

            TEMPUserName = dr[0].Field<string>("User_ContactTEMPID");
            string SourceType = dr[0].Field<string>("User_SourceType");






            FileStream fs = new FileStream(Application.StartupPath + "\\Template_shishicai.json", System.IO.FileMode.OpenOrCreate);
            byte[] bs = new byte[fs.Length];
            fs.Read(bs, 0, bs.Length);
            fs.Close();
            fs.Dispose();
            string Newmsgtxt = Encoding.UTF8.GetString(bs, 3, bs.Length - 3);
            Newmsgtxt = Newmsgtxt.Replace("#微信号#", "weisimin1986");

            JObject body2 = new JObject();
            body2.Add("BaseRequest", j_BaseRequest["BaseRequest"]);

            JObject Msg2 = JObject.Parse(Newmsgtxt);


            Msg2.Add("FromUserName", MyUserName("微"));
            Msg2.Add("ToUserName", TEMPUserName);

            String Time2 = JavaTimeSpanFFFF();

            Msg2.Add("LocalID", Time2);
            Msg2.Add("ClientMsgId", Time2);

            body2.Add("Msg", Msg2);



            SendWXContent(Msg2, TEMPUserName);











        }

        private void CheckQQConMessage(string Messages, IntPtr hwnd, string WindowName)
        {

            Regex FindTime = new Regex("[0-9]+:[0-9]+:[0-9]+", RegexOptions.IgnoreCase);
            Regex FindDate = new Regex("[0-9][0-9][0-9][0-9]/[0-9]+/[0-9]+", RegexOptions.IgnoreCase);
            string[] Lines = Messages.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);


            List<Linq.WX_PCSendPicSetting> toremovepset = new List<Linq.WX_PCSendPicSetting>();
            foreach (var testitem in InjectWins)
            {
                StringBuilder RAW = new StringBuilder(512);
                NetFramework.WindowsApi.GetWindowText(hwnd, RAW, 512);
                if (RAW.ToString() == "")
                {
                    toremovepset.Add(testitem);
                }
            }
            foreach (var removeitem in toremovepset)
            {
                InjectWins.Remove(removeitem);
            }


            var pcsets = InjectWins.Where(t => t.WX_UserTMPID == hwnd.ToString());
            if (pcsets.Count() == 0)
            {
                return;
            }




            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            #region 整点发送下单情况和余下分数



            #region 整点计算
            string GameFullPeriod = "";
            string GameFullLocalPeriod = "";
            bool ShiShiCaiSuccess = false;
            string ShiShiCaiErrorMessage = "";
            Linq.ProgramLogic.ShiShiCaiMode subm = Linq.ProgramLogic.GetMode(pcsets.First());

            Linq.ProgramLogic.ChongQingShiShiCaiCaculatePeriod((DateTime.Now.AddSeconds(-30)), "", db, "", "", out GameFullPeriod, out GameFullLocalPeriod, false, out ShiShiCaiSuccess, out ShiShiCaiErrorMessage, subm, true);

            if (ShiShiCaiSuccess == false)
            {

                hwndSendText(ShiShiCaiErrorMessage, hwnd);
            }
            #endregion

            if ((pcsets.First().PreSendPeriod != GameFullPeriod || pcsets.First() == null) && (GameFullPeriod != ""))
            {
                string ReturnSend = "##" + Environment.NewLine
                        + "=======休战=======" + Environment.NewLine
                        + "本轮攻击" + (pcsets.First().PreSendPeriod == null ? "" : (pcsets.First().PreSendPeriod.Length >= 3 ? pcsets.First().PreSendPeriod.Substring(pcsets.First().PreSendPeriod.Length - 3, 3) : pcsets.First().PreSendPeriod)) + "结束" + Environment.NewLine
                        + "请各参战玩家等待" + Environment.NewLine
                        + "下一回合的开始" + Environment.NewLine
                        + "====================" + Environment.NewLine
                        + "攻击核对：" + Environment.NewLine;

                var buys = db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                    && t.MemberGroupName == WindowName
                    && t.WX_SourceType == Enum.GetName(typeof(PCSourceType), PCSourceType.PCQ)
                    && t.Result_HaveProcess == false
                    && t.Buy_Point != 0
                    );
                var users = buys.Select(t => t.WX_UserName).Distinct();
                foreach (var useritem in users)
                {
                    Linq.ProgramLogic.TotalResult tr = Linq.ProgramLogic.BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                          && t.WX_UserName == useritem
                          && t.WX_SourceType == Enum.GetName(typeof(PCSourceType), PCSourceType.PCQ)
                          && t.Result_HaveProcess == false
                          && t.Buy_Point != 0).ToList()
                      , RunnerF.MemberSource);
                    ReturnSend += tr.ToSlimStringV4();

                }
                ReturnSend += "====================" + Environment.NewLine
                            + "如果有误，请私聊管理" + Environment.NewLine
                            + "====================" + Environment.NewLine;

                if (users.Count() != 0)
                {
                    SendRobotContent(ReturnSend, hwnd.ToString(), Enum.GetName(typeof(PCSourceType), PCSourceType.PCQ));
                }
                pcsets.First().PreSendPeriod = GameFullPeriod;
                //winsdb.SubmitChanges();

            }//发送结束
            #endregion

            #region 如果开奖队列有数据


            if (pcsets.First() != null && pcsets.First().NextSendString != null && pcsets.First().NextSendString != "")
            {
                SendRobotContent(pcsets.First().NextSendString, hwnd.ToString(), Enum.GetName(typeof(PCSourceType), PCSourceType.PCQ));
                pcsets.First().NextSendString = "";
                //winsdb.SubmitChanges();
            }
            #endregion


            string prewhosay = "";
            string strfinddate = "";
            string strfindtime = "";

            DataRow usr = null;
            Int32 MessageIndex = 0;

            Boolean ReplyMode = false;


            foreach (var lineitem in Lines)
            {

                try
                {



                    string tmp_strfinddate = FindDate.Match(lineitem).Value;
                    string tmp_strfindtime = FindTime.Match(lineitem).Value;


                    if (lineitem.EndsWith(tmp_strfindtime) && tmp_strfindtime != "")
                    {
                        strfinddate = tmp_strfinddate.Replace("/", "-");
                        strfindtime = tmp_strfindtime;

                        prewhosay = lineitem.Replace(tmp_strfindtime, "");
                        if (tmp_strfinddate != "")
                        {
                            prewhosay = prewhosay.Replace(tmp_strfinddate, "").Replace(" ", "");
                        }
                        prewhosay = prewhosay.Replace(" ", "");

                        prewhosay = Regex.Replace(prewhosay, "\\((?!\\))[\\s\\S]+\\)", "");
                        prewhosay = Regex.Replace(prewhosay, "<(?!>)[\\s\\S]+>", "");


                        MessageIndex = 0;

                        ReplyMode = false;


                        Linq.WX_UserReply userreply = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                                                                                  && t.WX_UserName == prewhosay
                                                                                  && t.WX_SourceType == Enum.GetName(typeof(PCSourceType), PCSourceType.PCQ)
                                                                                  );





                        usr = RunnerF.MemberSource.AsEnumerable().SingleOrDefault
                               (t => t.Field<object>("User_ContactID").ToString() == prewhosay
                               && t.Field<object>("User_SourceType").ToString() == pcsets.First().WX_SourceType
                               );


                        if (userreply == null)
                        {
                            Linq.WX_UserReply newr = new Linq.WX_UserReply();
                            newr.aspnet_UserID = GlobalParam.UserKey;
                            newr.WX_SourceType = pcsets.First().WX_SourceType;
                            newr.WX_UserName = prewhosay;
                            db.WX_UserReply.InsertOnSubmit(newr);
                            db.SubmitChanges();


                        }
                        if (usr == null && userreply == null)
                        {
                            DataRow newset = RunnerF.MemberSource.NewRow();
                            newset.SetField("User_ContactID", prewhosay);
                            newset.SetField("User_ContactTEMPID", hwnd.ToString());
                            newset.SetField("User_SourceType", pcsets.First().WX_SourceType);

                            #region 拉取注入设置的模式

                            newset.SetField("User_ChongqingMode", pcsets.First().ChongqingMode);
                            newset.SetField("User_FiveMinuteMode", pcsets.First().FiveMinuteMode);
                            newset.SetField("User_HkMode", pcsets.First().HkMode);
                            newset.SetField("User_AozcMode", pcsets.First().AozcMode);

                            #endregion
                            newset.SetField("User_Contact", prewhosay);

                            RunnerF.MemberSource.Rows.Add(newset);
                            usr = newset;
                        }
                        else if (usr == null && userreply != null)
                        {
                            DataRow newset = RunnerF.MemberSource.NewRow();
                            newset.SetField("User_ContactID", prewhosay);
                            newset.SetField("User_ContactTEMPID", hwnd.ToString());
                            newset.SetField("User_SourceType", pcsets.First().WX_SourceType);
                            newset.SetField("User_Contact", prewhosay);

                            newset.SetField("User_IsAdmin", userreply.IsAdmin);


                            #region 拉取注入设置的模式

                            newset.SetField("User_ChongqingMode", pcsets.First().ChongqingMode);
                            newset.SetField("User_FiveMinuteMode", pcsets.First().FiveMinuteMode);
                            newset.SetField("User_HkMode", pcsets.First().HkMode);
                            newset.SetField("User_AozcMode", pcsets.First().AozcMode);

                            #endregion

                            RunnerF.MemberSource.Rows.Add(newset);
                            usr = newset;
                        }







                        continue;
                    }
                    MessageIndex += 1;

                    if (usr == null)
                    {
                        continue;
                    }

                    DateTime RequestTime = Convert.ToDateTime(strfinddate == "" ? (DateTime.Today.ToString("yyyy-MM-dd ") + strfindtime) : (strfinddate + " " + strfindtime));
                    if (RequestTime > DateTime.Now)
                    {
                        RequestTime = RequestTime.AddDays(-1);

                    }
                    string NewContent = lineitem;
                    NewContent = NewContent.Trim();


                    //一般正常下单
                    //会员代下单@XX+-*#
                    //机器人回复@xx##

                    if (NewContent == "")
                    {
                        continue;
                    }

                    Linq.WX_UserReply newloadreply = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                                                                                 && t.WX_UserName == prewhosay
                                                                                 && t.WX_SourceType == Enum.GetName(typeof(PCSourceType), PCSourceType.PCQ)
                                                                                 );
                    if (NewContent.Contains("##") && MessageIndex == 1)
                    {
                        ReplyMode = true;
                    }
                    if (NewContent == "加")
                    {
                        QqWindowHelper qh = new QqWindowHelper(hwnd, "", false);
                        qh.MainUI = this;
                        var loadset = db.WX_PCSendPicSetting.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                            && t.WX_SourceType == Enum.GetName(typeof(PCSourceType), PCSourceType.PCQ)
                            && t.WX_UserName == WindowName
                            );
                        if (loadset != null)
                        {
                            qh.ReloadMembers(loadset.GroupOwner, RunnerF.MemberSource, Enum.GetName(typeof(PCSourceType), PCSourceType.PCQ), db, hwnd);

                        }

                    }
                    if (NewContent == "群取消" && newloadreply.IsAdmin == true)
                    {
                        var buys = db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                            && t.Result_HaveProcess == false
                            && t.MemberGroupName == WindowName
                            ).Select(t => new { t.WX_UserName, t.WX_SourceType }).Distinct();
                        foreach (var cancelitem in buys)
                        {

                            DataRow canusr = RunnerF.MemberSource.AsEnumerable().SingleOrDefault
                                    (t => t.Field<object>("User_ContactID").ToString() == cancelitem.WX_UserName
                                    && t.Field<object>("User_SourceType").ToString() == cancelitem.WX_SourceType
                                    );
                            try
                            {
                                string subReturnSend =
                                NewWXContent(DateTime.Now, "取消", canusr,
                                cancelitem.WX_SourceType, newloadreply.IsAdmin.HasValue ? newloadreply.IsAdmin.Value : false);
                                if (subReturnSend != "")
                                {
                                    hwndSendText("@" + cancelitem.WX_UserName + " ##" + subReturnSend, hwnd);
                                }
                            }
                            catch (Exception suberror)
                            {

                                NetFramework.Console.WriteLine(suberror.Message, true);
                                NetFramework.Console.WriteLine(suberror.StackTrace, true);
                            }

                        }

                    }


                    Linq.WX_UserReply checkl = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                                           && t.WX_SourceType == pcsets.First().WX_SourceType
                                           && t.WX_UserName == prewhosay
                                           );

                    //会员模式，NEWWXCONTENT自动记录聊天日志
                    if (NewContent.StartsWith("@") == false && ReplyMode == false)
                    {

                        #region 会员代下单不走这分支
                        string Return = NewWXContent(
                             RequestTime
                             , NewContent
                             , usr, pcsets.First().WX_SourceType
                             , (newloadreply.IsAdmin.HasValue ? newloadreply.IsAdmin.Value : false)
                             , MessageIndex, false, WindowName
                             );
                        if (Return != "")
                        {
                            Return = "@" + prewhosay + "##" + Return;
                            hwndSendText(Return, hwnd);


                        }
                        #endregion
                    }
                    else if (newloadreply.IsAdmin == true && NewContent.StartsWith("@")
                        && NewContent.Contains("##") == false
                        && ReplyMode == false
                        && (NewContent.Contains("#") == true
                        || NewContent.Contains("+") == true
                        || NewContent.Contains("*") == true
                         || NewContent.Contains("-") == true
                         || NewContent.Contains(" ") == true
                        ))
                    {
                        #region

                        NewContent = NewContent.Replace("+", "#").Replace("*", "#").Replace("-", "#").Replace(" ", "#");

                        string[] ReplaceWhoSayAndContent = NewContent.Split("#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                        if (ReplaceWhoSayAndContent.Length >= 2)
                        {
                            string ReplaceWhoSay = ReplaceWhoSayAndContent[0].Replace("@", "");
                            string ReplaceNewContent = ReplaceWhoSayAndContent[1];

                            var replylog = db.WX_UserReplyLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                                && t.WX_SourceType == pcsets.First().WX_SourceType
                                && t.WX_UserName == prewhosay
                                && t.ReceiveTime == RequestTime
                                && t.ReceiveIndex == MessageIndex
                                );
                            if (replylog == null)
                            {
                                Linq.WX_UserReplyLog newl = new Linq.WX_UserReplyLog();
                                newl.aspnet_UserID = GlobalParam.UserKey;
                                newl.WX_UserName = prewhosay;
                                newl.WX_SourceType = pcsets.First().WX_SourceType;
                                newl.SourceType = "人工";
                                newl.ReceiveTime = RequestTime;
                                newl.ReceiveIndex = MessageIndex;
                                newl.ReceiveContent = NewContent;
                                db.WX_UserReplyLog.InsertOnSubmit(newl);

                                replylog = newl;
                            }
                            else
                            {
                                continue;
                            }


                            var rows = RunnerF.MemberSource.AsEnumerable().Where
                                   (t => t.Field<string>("User_ContactID") == ReplaceWhoSay
                                   && t.Field<string>("User_SourceType") == pcsets.First().WX_SourceType
                                   );
                            DataRow ref_usr = null;
                            if (rows.Count() > 0)
                            {
                                ref_usr = rows.First();
                            }
                            if (ref_usr == null)
                            {
                                continue;
                            }
                            string ReturnSend = "";
                            //先走一遍会员消息
                            if (checkl.IsAdmin == true)
                            {
                                ReturnSend = Linq.ProgramLogic.WX_UserReplyLog_MySendCreate(ReplaceNewContent, ref_usr, RequestTime);
                                if (ReturnSend != "")
                                {
                                    ReturnSend = "@" + ReplaceWhoSay + "##" + ReturnSend;
                                    hwndSendText(ReturnSend, hwnd);

                                }//不为空的才发出
                                replylog.ReplyTime = DateTime.Now;
                                replylog.ReplyContent = ReturnSend;
                                db.SubmitChanges();
                            }


                            //再走一遍代下单
                            ReturnSend = NewWXContent(RequestTime
                                , ReplaceNewContent, ref_usr, "人工"
                                , newloadreply.IsAdmin.HasValue ? newloadreply.IsAdmin.Value : false
                                , MessageIndex, false, WindowName);



                            if (ReturnSend != "")
                            {
                                ReturnSend = "@" + ReplaceWhoSay + "##" + ReturnSend;
                                hwndSendText(ReturnSend, hwnd);

                            }//不为空的才发出
                            replylog.ReplyTime = DateTime.Now;
                            replylog.ReplyContent = ReturnSend;
                            db.SubmitChanges();



                        #endregion

                        }//@内容不是空白





                    }//会员代下单模式
                }//try结束
                catch (Exception anyerror)
                {
                    NetFramework.Console.WriteLine(anyerror.Message, true);
                    NetFramework.Console.WriteLine(anyerror.StackTrace, true);
                    continue;
                }
            }//行循环
        }





        private void mi_reminderquery_Click(object sender, EventArgs e)
        {
            RemindQuery rq = new RemindQuery();
            rq.Show();
        }


        //private void Refreshball(CefSharp.WinForms.ChromiumCefSharp.WinForms.ChromiumEO.WinForm.WebControl wb, string idname, string balltype)
        //{
        //    this.Invoke(new Action(() =>
        //    {

        //        if (wb.IsBrowserInitialized == false)
        //        {
        //            NetFramework.Console.WriteLine("控件" + wb.Name + "尚未初始化");
        //            return;
        //        }


        //        System.Threading.Tasks.Task<string> task = wb.GetBrowser().MainFrame.GetSourceAsync();
        //        task.Wait();

        //        string html = task.Result;



        //        Regex findtable = new Regex("<div id=\"" + idname + "((?!(/div))[\\s\\S])+/div>", RegexOptions.IgnoreCase);
        //        Regex findmaintr = new Regex(@"<tr[^>]*>((?<mm><tr[^>]*>)+|(?<-mm></tr>)|[\s\S])*?(?(mm)(?!))</tr>", RegexOptions.IgnoreCase);
        //        Regex findtds = new Regex(@"<td[^>]*>((?<mm><td[^>]*>)+|(?<-mm></td>)|[\s\S])*?(?(mm)(?!))</td>", RegexOptions.IgnoreCase);

        //        string total = findtable.Match(html).Value;

        //        #region 全部赛事保存成图片

        //        string NewHtml = Regex.Replace(html, "<body((?!</body)[\\s\\S]+)</body>", "<body>" + total + "</body>", RegexOptions.IgnoreCase);
        //        NewHtml = Regex.Replace(NewHtml, "<script((?!</script>)[\\s\\S])+</script>", "", RegexOptions.IgnoreCase);
        //        NewHtml = Regex.Replace(NewHtml, "<iframe((?!</iframe>)[\\s\\S])+</iframe>", "", RegexOptions.IgnoreCase);
        //        if (File.Exists(Application.StartupPath + "\\output\\" + wb.Name + ".htm") == false)
        //        {
        //            FileStream fc = File.Create(Application.StartupPath + "\\output\\" + wb.Name + ".htm");
        //            fc.Flush();
        //            fc.Close();
        //        }

        //        FileStream fswb = new FileStream(Application.StartupPath + "\\output\\" + wb.Name + ".htm", FileMode.Truncate);
        //        byte[] wbs = Encoding.UTF8.GetBytes(NewHtml);

        //        fswb.Write(wbs, 0, wbs.Length);
        //        fswb.Flush();
        //        fswb.Close();

        //        System.Threading.Tasks.Task<CefSharp.JavascriptResponse> gst = wb.GetBrowser().MainFrame.EvaluateScriptAsync("$('#" + idname + "').height()");
        //        gst.Wait();
        //        string jsheight = gst.Result.Result == null ? "1024" : gst.Result.Result.ToString();
        //        Int32 njsheighy = Convert.ToInt32(jsheight);


        //        Bitmap wbOutputbitmap = WebSnapshotsHelper.GetWebSiteThumbnail("file:///" + Application.StartupPath + "\\output\\" + wb.Name + ".htm", 840, njsheighy, 840, njsheighy); //宽高根据要获取快照的网页决定
        //        wbOutputbitmap.Save(Application.StartupPath + "\\output\\" + wb.Name + ".jpg"
        //            , System.Drawing.Imaging.ImageFormat.Jpeg
        //            );
        //        wbOutputbitmap.Dispose();

        //        #endregion

        //        Regex findHead = new Regex("<div id=\"data_top\">((?!</div>)[\\s\\S])+</div>", RegexOptions.IgnoreCase);

        //        string headstr = findHead.Match(html).Value;

        //        headstr = headstr.Replace("欧洲盘", "");
        //        headstr = Regex.Replace(headstr, "<td", "<td style=\"font-size:16px!important\"", RegexOptions.IgnoreCase);
        //        headstr = Regex.Replace(headstr, "width=\"42\"", "wifth=\"60\"", RegexOptions.IgnoreCase);


        //        MatchCollection rows = findmaintr.Matches(total);
        //        foreach (Match Rowitem in rows)
        //        {
        //            Regex findid = new Regex("id=\"((?!\")[\\s\\S])+\"");

        //            Regex findtables = new Regex(@"<table[^>]*>((?<mm><table[^>]*>)+|(?<-mm></table>)|[\s\S])*?(?(mm)(?!))</table>", RegexOptions.IgnoreCase);
        //            MatchCollection tabs = findtables.Matches(Rowitem.Value);
        //            if (tabs.Count == 0)
        //            {
        //                continue;
        //            }
        //            string NewRowItem = Rowitem.Value;
        //            try
        //            {

        //                NewRowItem = Regex.Replace(NewRowItem, "<br>", "", RegexOptions.IgnoreCase);
        //                NewRowItem = Regex.Replace(NewRowItem, "<input((?!>)[\\S\\s])+>", "", RegexOptions.IgnoreCase);

        //                XmlDocument doc = new XmlDocument();
        //                doc.LoadXml(NewRowItem);

        //                var trs = doc.SelectNodes("tr/td[2]/table/tbody/tr");
        //                foreach (XmlNode tritem in trs)
        //                {
        //                    var tds = tritem.SelectNodes("td");
        //                    tds[4].InnerXml = "-";
        //                    tds[5].InnerXml = "-";
        //                    tds[6].InnerXml = "-";
        //                }
        //                NewRowItem = doc.OuterXml;
        //                NewRowItem = Regex.Replace(NewRowItem, "<td", "<td style=\"font-size:16px!important\"", RegexOptions.IgnoreCase);
        //                NewRowItem = Regex.Replace(NewRowItem, "<font", "<font style=\"font-size:16px!important\"", RegexOptions.IgnoreCase);
        //                NewRowItem = Regex.Replace(NewRowItem, "width=\"44\"", "width=\"60\"", RegexOptions.IgnoreCase);
        //            }
        //            catch (Exception AnyError)
        //            {
        //                NetFramework.Console.WriteLine("删除欧洲盘失败");
        //            }

        //            string vs = findtds.Matches(tabs[0].Value)[1].Value;
        //            string Key = findid.Match(Rowitem.Value).Value;
        //            Key = Key.Replace("\"", "").Replace("id=", "");
        //            vs = vs.Replace("<br>", "@#@#");
        //            string reg = @"[<].*?[>]";
        //            vs = Regex.Replace(vs, reg, "");


        //            Linq.ProgramLogic.c_vs toupdate = Linq.ProgramLogic.GameMatches.SingleOrDefault(t => t.Key == Key && t.GameType == balltype);
        //            if (toupdate == null)
        //            {
        //                Linq.ProgramLogic.c_vs newr = new Linq.ProgramLogic.c_vs();
        //                newr.Key = Key;

        //                newr.A_Team = "(主)"+vs.Split("@#@#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
        //                newr.B_Team = "(客)"+vs.Split("@#@#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1];
        //                newr.GameType = balltype;
        //                newr.RowData = NewRowItem;
        //                newr.HeadDiv = headstr;

        //                Linq.ProgramLogic.GameMatches.Add(newr);

        //                foreach (Match rratio in findmaintr.Matches(tabs[1].Value))
        //                {

        //                    MatchCollection ratios = findtds.Matches(rratio.Value);

        //                    Linq.ProgramLogic.c_rario currentr = new Linq.ProgramLogic.c_rario();

        //                    currentr.rowtypeindex = newr.CurRatioCount;
        //                    newr.ratios.Add(currentr);
        //                    newr.CurRatioCount += 1;

        //                    currentr.RatioType = (Regex.Replace(ratios[0].Value, reg, ""));
        //                    currentr.A_WIN = (Regex.Replace(ratios[1].Value, reg, ""));
        //                    currentr.Winless = Regex.Replace(ratios[2].Value, reg, "");
        //                    currentr.B_Win = (Regex.Replace(ratios[3].Value, reg, ""));

        //                    if (balltype == "足球")
        //                    {




        //                        currentr.BigWin = (Regex.Replace(ratios[7].Value, reg, ""));
        //                        currentr.Total = Regex.Replace(ratios[8].Value, reg, "");
        //                        currentr.SmallWin = (Regex.Replace(ratios[9].Value, reg, ""));


        //                    }
        //                    else
        //                    {
        //                        currentr.BigWin = (Regex.Replace(ratios[6].Value, reg, ""));
        //                        currentr.Total = Regex.Replace(ratios[7].Value, reg, "");
        //                        currentr.SmallWin = (Regex.Replace(ratios[8].Value, reg, ""));
        //                    }







        //                }//行循环盘类别

        //            }//无比赛
        //            else
        //            {
        //                foreach (Match rratio in findmaintr.Matches(tabs[1].Value))
        //                {




        //                    MatchCollection ratios = findtds.Matches(rratio.Value);

        //                    Linq.ProgramLogic.c_rario findcr = toupdate.ratios.SingleOrDefault(t => t.RatioType == (Regex.Replace(ratios[0].Value, reg, "")));

        //                    if (findcr == null)
        //                    {



        //                        Linq.ProgramLogic.c_rario currentr = new Linq.ProgramLogic.c_rario();

        //                        currentr.rowtypeindex = toupdate.CurRatioCount;
        //                        toupdate.ratios.Add(currentr);
        //                        toupdate.CurRatioCount += 1;


        //                        currentr.RatioType = (Regex.Replace(ratios[0].Value, reg, ""));
        //                        currentr.A_WIN = (Regex.Replace(ratios[1].Value, reg, ""));
        //                        currentr.Winless = Regex.Replace(ratios[2].Value, reg, "");
        //                        currentr.B_Win = (Regex.Replace(ratios[3].Value, reg, ""));

        //                        if (balltype == "足球")
        //                        {
        //                            currentr.BigWin = (Regex.Replace(ratios[7].Value, reg, ""));
        //                            currentr.Total = Regex.Replace(ratios[8].Value, reg, "");
        //                            currentr.SmallWin = (Regex.Replace(ratios[9].Value, reg, ""));
        //                        }
        //                        else
        //                        {
        //                            currentr.BigWin = (Regex.Replace(ratios[6].Value, reg, ""));
        //                            currentr.Total = Regex.Replace(ratios[7].Value, reg, "");
        //                            currentr.SmallWin = (Regex.Replace(ratios[8].Value, reg, ""));
        //                        }
        //                    }//找到当前盘或初始盘
        //                    else
        //                    {
        //                        findcr.RatioType = (Regex.Replace(ratios[0].Value, reg, ""));
        //                        findcr.A_WIN = (Regex.Replace(ratios[1].Value, reg, ""));
        //                        findcr.Winless = Regex.Replace(ratios[2].Value, reg, "");
        //                        findcr.B_Win = (Regex.Replace(ratios[3].Value, reg, ""));

        //                        if (balltype == "足球")
        //                        {
        //                            findcr.BigWin = (Regex.Replace(ratios[7].Value, reg, ""));
        //                            findcr.Total = Regex.Replace(ratios[8].Value, reg, "");
        //                            findcr.SmallWin = (Regex.Replace(ratios[9].Value, reg, ""));

        //                        }
        //                        else
        //                        {
        //                            findcr.BigWin = (Regex.Replace(ratios[6].Value, reg, ""));
        //                            findcr.Total = Regex.Replace(ratios[7].Value, reg, "");
        //                            findcr.SmallWin = (Regex.Replace(ratios[8].Value, reg, ""));
        //                        }

        //                    }//盘行数不一样
        //                }//循环
        //            }//有比赛
        //        }

        //    }));//ACTION结束
        //}
        //private void Refreshother(CefSharp.WinForms.ChromiumCefSharp.WinForms.ChromiumEO.WinForm.WebControl wb)
        //{
        //    this.Invoke(new Action(() =>
        //    {
        //        if (wb.IsBrowserInitialized == false)
        //        {
        //            NetFramework.Console.WriteLine("控件" + wb.Name + "尚未初始化");
        //            return;
        //        }


        //        System.Threading.Tasks.Task<string> task = wb.GetBrowser().MainFrame.GetSourceAsync();
        //        task.Wait();
        //        string otherhtml = task.Result;
        //        otherhtml = Regex.Replace(otherhtml, "<script((?!</script>)[\\s\\S])+</script>", "", RegexOptions.IgnoreCase);
        //        otherhtml = Regex.Replace(otherhtml, "<iframe((?!</iframe>)[\\s\\S])+</iframe>", "", RegexOptions.IgnoreCase);
        //        Regex FindOthers = new Regex("<a href=\"((?!\")[\\s\\S])+\" target=\"_blank\"><span>其他玩法</span></a>", RegexOptions.IgnoreCase);
        //        //var findothersres= FindOthers.Matches(otherhtml);
        //        List<string> findothersres = new List<string>();
        //        foreach (Match item in FindOthers.Matches(otherhtml))
        //        {
        //            findothersres.Add(item.Value);
        //        }


        //        foreach (string item in findothersres)
        //        {
        //            Application.DoEvents();
        //            string NewUrl = "http://odds.gooooal.com/" + item.Replace("<a href=\"", "").Replace("\" target=\"_blank\"><span>其他玩法</span></a>", "").Replace("&amp;", "&");
        //            //NewUrl = "http://odds.gooooal.com/singlefield.html?mid=1394090&type=5";
        //            wb_refresh.Load(NewUrl);

        //            DateTime pretime = DateTime.Now;
        //            while ((DateTime.Now - pretime).TotalMilliseconds < 3000)
        //            {
        //                Application.DoEvents();
        //                Thread.Sleep(100);
        //                if (ExitKill)
        //                {
        //                    return;
        //                }
        //            }
        //            wb_refresh.GetBrowser().StopLoad();


        //            System.Threading.Tasks.Task<string> refreshtask = wb_refresh.GetBrowser().MainFrame.GetSourceAsync();
        //            refreshtask.Wait();
        //            string refreshhtml = refreshtask.Result;

        //            Regex FindShow = FindShow = new Regex(@"<div\s*id=""data_main5""[^>]*>((?<mm><div[^>]*>)+|(?<-mm></div>)|[\s\S])*?(?(mm)(?!))</div>", RegexOptions.IgnoreCase);
        //            string showstr = FindShow.Match(refreshhtml).Value;
        //            showstr = Regex.Replace(showstr, "<td", "<td style=\"font-size:16px!important;\"", RegexOptions.IgnoreCase);
        //            String NewOutput = "";
        //            refreshhtml = Regex.Replace(refreshhtml, "<script((?!</script>)[\\s\\S])+</script>", "", RegexOptions.IgnoreCase);
        //            refreshhtml = Regex.Replace(refreshhtml, "<iframe((?!</iframe>)[\\s\\S])+</iframe>", "", RegexOptions.IgnoreCase);



        //            Linq.ProgramLogic.c_vs gamem = Linq.ProgramLogic.GameMatches.SingleOrDefault(t => t.Key == "m_" + item.Replace("<a href=\"singlefield.html?mid=", "").Replace("\" target=\"_blank\"><span>其他玩法</span></a>", "").Replace("&amp;type=5", ""));
        //            if (gamem != null)
        //            {
        //                NewOutput = Regex.Replace(refreshhtml, "<body>((?!</body>)[\\s\\S])+</body>", "<body style=\"width:840px\">"
        //                    + gamem.HeadDiv
        //                    + "<table>"
        //                    + gamem.RowData
        //                    + "</table>"
        //                   + showstr + "</body>", RegexOptions.IgnoreCase);

        //            }
        //            else
        //            {
        //                NewOutput = Regex.Replace(refreshhtml, "<body>((?!</body>)[\\s\\S])+</body>", "<body style=\"width:840px\">" + showstr + "</body>", RegexOptions.IgnoreCase);

        //            }
        //            if (File.Exists(Application.StartupPath + "\\tmp.htm") == false)
        //            {
        //                FileStream fsc = File.Create(Application.StartupPath + "\\tmp.htm");
        //                fsc.Flush();
        //                fsc.Close();
        //                fsc.Dispose();
        //            }
        //            FileStream fs = new FileStream(Application.StartupPath + "\\tmp.htm", FileMode.Truncate);
        //            byte[] bsource = Encoding.UTF8.GetBytes(NewOutput);
        //            fs.Write(bsource, 0, bsource.Length);
        //            fs.Close();


        //            if (Directory.Exists(Application.StartupPath + "\\output") == false)
        //            {
        //                Directory.CreateDirectory(Application.StartupPath + "\\output");
        //            }

        //            Bitmap Outputbitmap = WebSnapshotsHelper.GetWebSiteThumbnail("file:///" + Application.StartupPath + "\\tmp.htm", 840, 450, 840, 450); //宽高根据要获取快照的网页决定
        //            Outputbitmap.Save(Application.StartupPath + "\\output\\" + "m_" + item.Replace("<a href=\"singlefield.html?mid=", "").Replace("\" target=\"_blank\"><span>其他玩法</span></a>", "").Replace("&amp;type=5", "") + ".jpg"
        //                , System.Drawing.Imaging.ImageFormat.Jpeg
        //                );
        //            Outputbitmap.Dispose();


        //        }//循环抓数结束

        //        #region 无其他玩法图片
        //        foreach (var gamem in Linq.ProgramLogic.GameMatches)
        //        {
        //            string[] picfiles = Directory.GetFiles((Application.StartupPath + "\\output"));
        //            if (picfiles.Where(t => t.Contains(gamem.Key)).Count() == 0)
        //            {
        //                string NewOutput = "";
        //                NewOutput = Regex.Replace(otherhtml, "<body>((?!</body>)[\\s\\S])+</body>", "<body style=\"width:840px\">"
        //                      + gamem.HeadDiv
        //                      + "<table>"
        //                      + gamem.RowData
        //                      + "</table>"
        //                      + "</body>", RegexOptions.IgnoreCase);

        //                if (File.Exists(Application.StartupPath + "\\tmp.htm") == false)
        //                {
        //                    FileStream fsc = File.Create(Application.StartupPath + "\\tmp.htm");
        //                    fsc.Flush();
        //                    fsc.Close();
        //                    fsc.Dispose();
        //                }
        //                FileStream fs = new FileStream(Application.StartupPath + "\\tmp.htm", FileMode.Truncate);
        //                byte[] bsource = Encoding.UTF8.GetBytes(NewOutput);
        //                fs.Write(bsource, 0, bsource.Length);
        //                fs.Close();


        //                if (Directory.Exists(Application.StartupPath + "\\output") == false)
        //                {
        //                    Directory.CreateDirectory(Application.StartupPath + "\\output");
        //                }

        //                Bitmap Outputbitmap = WebSnapshotsHelper.GetWebSiteThumbnail("file:///" + Application.StartupPath + "\\tmp.htm", 840, 450, 840, 450); //宽高根据要获取快照的网页决定
        //                Outputbitmap.Save(Application.StartupPath + "\\output\\" + gamem.Key + ".jpg"
        //                    , System.Drawing.Imaging.ImageFormat.Jpeg
        //                    );
        //                Outputbitmap.Dispose();

        //            }


        //        }
        //        #endregion

        //    }));//invode action结束
        //}
        //private void StartGetBallRatio()
        //{
        //    while (true)
        //    {
        //        if (ExitKill)
        //        {
        //            return;
        //        }

        //        try
        //        {
        //            Refreshball(wb_football, "data_main", "足球 ");
        //            Refreshball(wb_basketball, "mm_content", "篮球");
        //            Refreshother(wb_other);


        //        }
        //        catch (Exception AnyError)
        //        {

        //            NetFramework.Console.WriteLine(AnyError.Message);
        //            NetFramework.Console.WriteLine(AnyError.StackTrace);
        //            return;
        //        }
        //        Thread.Sleep(5000);
        //    }

        //}


        public enum BallType { 足球, 篮球 }

        private object LockLoad = false;
        private void RefreshballV2(EO.WinForm.WebControl ballgame, string idname, BallType DoBallType, Linq.ProgramLogic.BallCompanyType PcompanyType)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;






            string html = "";
            this.Invoke(new Action(() =>
            {
                ((GroupBox)ballgame.Parent).Text = Enum.GetName(typeof(BallType), DoBallType) + Enum.GetName(typeof(Linq.ProgramLogic.BallCompanyType), PcompanyType) + "正在采集赛事";

                if (DoBallType == BallType.足球)
                {

                    //ballgame.GetBrowser().StopLoad();
                    //ballgame.GetBrowser().MainFrame.ExecuteJavaScriptAsync("lication.href='" + "http://odds.gooooal.com/company.html?type=" + (PcompanyType == Linq.ProgramLogic.c_rario.CompanyType.皇冠 ? "1001" : "1") + "'");
                    html = NetFramework.Util_CEF.JoinQueueAndWait("http://odds.gooooal.com/company.html?type=" + (PcompanyType == Linq.ProgramLogic.BallCompanyType.皇冠 ? "1001" : "1"), ballgame);
                }
                else if (DoBallType == BallType.篮球)
                {
                    //ballgame.GetBrowser().StopLoad();
                    //ballgame.GetBrowser().MainFrame.ExecuteJavaScriptAsync("lication.href='" + "http://odds.gooooal.com/bkscompany.html?type=" + (PcompanyType == Linq.ProgramLogic.c_rario.CompanyType.皇冠 ? "1001" : "1") + "'");
                    // ballgame.GetBrowser().CloseBrowser(true);
                    html = NetFramework.Util_CEF.JoinQueueAndWait("http://odds.gooooal.com/bkscompany.html?type=" + (PcompanyType == Linq.ProgramLogic.BallCompanyType.皇冠 ? "1001" : "1"), ballgame);
                }




            }));


            Regex findtable = new Regex("<div id=\"" + idname + "((?!(/div))[\\s\\S])+/div>", RegexOptions.IgnoreCase);
            Regex findmaintr = new Regex(@"<tr[^>]*>((?<mm><tr[^>]*>)+|(?<-mm></tr>)|[\s\S])*?(?(mm)(?!))</tr>", RegexOptions.IgnoreCase);
            Regex findtds = new Regex(@"<td[^>]*>((?<mm><td[^>]*>)+|(?<-mm></td>)|[\s\S])*?(?(mm)(?!))</td>", RegexOptions.IgnoreCase);

            string total = findtable.Match(html).Value;

            Regex findDatatop = new Regex("<div id=\"data_top((?!(/div))[\\s\\S])+/div>", RegexOptions.IgnoreCase);
            string strdatatop = findDatatop.Match(html).Value;

            Regex findtablestart = new Regex("<div id=\"" + idname + "((?!(<tbody>))[\\s\\S])+<tbody>", RegexOptions.IgnoreCase);
            string strtablestart = findtablestart.Match(html).Value;

            #region 全部赛事保存成图片

            //string NewHtml = Regex.Replace(html, "<body((?!</body)[\\s\\S]+)</body>", "<body style=\"font-family:微软雅黑;\">" + total + "</body>", RegexOptions.IgnoreCase);
            //NewHtml = Regex.Replace(NewHtml, "<script((?!</script>)[\\s\\S])+</script>", "", RegexOptions.IgnoreCase);
            //NewHtml = Regex.Replace(NewHtml, "<iframe((?!</iframe>)[\\s\\S])+</iframe>", "", RegexOptions.IgnoreCase);
            //if (File.Exists(Application.StartupPath + "\\output\\" + GetMatchWB.Name + ".htm") == false)
            //{
            //    FileStream fc = File.Create(Application.StartupPath + "\\output\\" + GetMatchWB.Name + ".htm");
            //    fc.Flush();
            //    fc.Close();
            //}

            //FileStream fswb = new FileStream(Application.StartupPath + "\\output\\" + GetMatchWB.Name + ".htm", FileMode.Truncate);
            //byte[] wbs = Encoding.UTF8.GetBytes(NewHtml);
            //fswb.Write(new byte[] { 0xEF, 0XBB, 0XBF }, 0, 3);
            //fswb.Write(wbs, 0, wbs.Length);
            //fswb.Flush();
            //fswb.Close();

            //System.Threading.Tasks.Task<CefSharp.JavascriptResponse> gst = GetMatchWB.GetBrowser().MainFrame.EvaluateScriptAsync("$('#" + idname + "').height()");
            //gst.Wait();
            //string jsheight = gst.Result.Result == null ? "1024" : gst.Result.Result.ToString();
            //Int32 njsheighy = Convert.ToInt32(jsheight);


            //Bitmap wbOutputbitmap = WebSnapshotsHelper.GetWebSiteThumbnail("file:///" + Application.StartupPath + "\\output\\" + wb.Name + ".htm", 840 + 50, njsheighy + 50, 840 + 50, njsheighy + 50); //宽高根据要获取快照的网页决定
            //wbOutputbitmap.Save(Application.StartupPath + "\\output\\" + GetMatchWB.Name + ".jpg"
            //    , System.Drawing.Imaging.ImageFormat.Jpeg
            //    );
            //wbOutputbitmap.Dispose();

            #endregion







            Regex findHead = new Regex("<div id=\"data_top\">((?!</div>)[\\s\\S])+</div>", RegexOptions.IgnoreCase);

            string headstr = findHead.Match(html).Value;

            headstr = headstr.Replace("欧洲盘", "");
            headstr = Regex.Replace(headstr, "<td", "<td style=\"font-size:16px!important\"", RegexOptions.IgnoreCase);
            headstr = Regex.Replace(headstr, "width=\"42\"", "wifth=\"60\"", RegexOptions.IgnoreCase);


            MatchCollection rows = findmaintr.Matches(total);



            foreach (Match Rowitem in rows)
            {

                Regex findid = new Regex("id=\"((?!\")[\\s\\S])+\"");

                Regex findtables = new Regex(@"<table[^>]*>((?<mm><table[^>]*>)+|(?<-mm></table>)|[\s\S])*?(?(mm)(?!))</table>", RegexOptions.IgnoreCase);
                MatchCollection tabs = findtables.Matches(Rowitem.Value);
                if (tabs.Count == 0)
                {
                    continue;
                }


                string vs = findtds.Matches(tabs[0].Value)[1].Value;
                string Key = findid.Match(Rowitem.Value).Value;
                Key = Key.Replace("\"", "").Replace("id=", "");
                vs = vs.Replace("<br>", "@#@#");
                string reg = @"[<].*?[>]";
                vs = Regex.Replace(vs, reg, "");

                string TypeAndTime = findtds.Matches(tabs[0].Value)[0].Value;
                TypeAndTime = TypeAndTime.Replace("<br>", "$#$#");
                TypeAndTime = NetFramework.Util_WEB.CleanHtml(TypeAndTime);



                Linq.Game_FootBall_VS nextwrite = null;




                Linq.Game_FootBall_VS toupdate = db.Game_FootBall_VS.SingleOrDefault(t => t.GameKey == Key
                    && t.GameType == Enum.GetName(typeof(BallType), DoBallType)
                    && t.aspnet_UserID == GlobalParam.UserKey
                    );
                if (toupdate == null)
                {
                    Linq.Game_FootBall_VS newvs = new Linq.Game_FootBall_VS();
                    newvs.GameKey = Key;
                    newvs.aspnet_UserID = GlobalParam.UserKey;

                    newvs.A_Team = "(主)" + (vs.Split("@#@#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0].Replace("(主)", ""));
                    newvs.B_Team = "(客)" + vs.Split("@#@#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1];

                    newvs.A_Team = Regex.Replace(newvs.A_Team, "\\[[0-9]+\\]", "", RegexOptions.IgnoreCase);
                    newvs.B_Team = Regex.Replace(newvs.B_Team, "\\[[0-9]+\\]", "", RegexOptions.IgnoreCase);

                    newvs.GameType = Enum.GetName(typeof(BallType), DoBallType);
                    newvs.RowData = tabs[0].Value;
                    newvs.RowDataWithName = Rowitem.Value;
                    newvs.HeadDiv = headstr;


                    newvs.GameTime = TypeAndTime.Split("$#$#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1];
                    newvs.MatchClass = TypeAndTime.Split("$#$#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
                    newvs.GameVS = vs.Replace("@#@#", "VS");

                    newvs.GameVS = Regex.Replace(newvs.GameVS, "\\[[0-9]+\\]", "", RegexOptions.IgnoreCase);
                    newvs.Jobid = GlobalParam.JobID;
                    newvs.LastAliveTime = DateTime.Now;


                    db.Game_FootBall_VS.InsertOnSubmit(newvs);

                    //没比赛就没盘，直接加
                    foreach (Match rratio in findmaintr.Matches(tabs[1].Value))
                    {

                        MatchCollection ratios = findtds.Matches(rratio.Value);

                        Linq.Game_FootBall_VSRatios currentr = new Linq.Game_FootBall_VSRatios();

                        currentr.aspnet_UserID = newvs.aspnet_UserID;
                        currentr.GameKey = newvs.GameKey;

                        IQueryable<Linq.Game_FootBall_VSRatios> DbRatios = Linq.ProgramLogic.GameVSGetRatios(db, newvs);

                        currentr.RatioIndex = (DbRatios.Count() == 0 ? 0 : DbRatios.Max(t => t.RatioIndex));
                        currentr.RatioIndex += 1;



                        currentr.RatioType = (Regex.Replace(ratios[0].Value, reg, "").Replace("澳门", "").Replace("皇冠", ""));
                        currentr.A_WIN = (Regex.Replace(ratios[1].Value, reg, ""));
                        currentr.Winless = Regex.Replace(ratios[2].Value, reg, "");
                        currentr.B_WIN = (Regex.Replace(ratios[3].Value, reg, ""));
                        currentr.RCompanyType = Enum.GetName(typeof(Linq.ProgramLogic.BallCompanyType), PcompanyType);
                        if (DoBallType == BallType.足球)
                        {
                            currentr.BIGWIN = (Regex.Replace(ratios[7].Value, reg, ""));
                            currentr.Total = Regex.Replace(ratios[8].Value, reg, "");
                            currentr.SMALLWIN = (Regex.Replace(ratios[9].Value, reg, ""));


                        }
                        else if (DoBallType == BallType.篮球)
                        {
                            currentr.BIGWIN = (Regex.Replace(ratios[6].Value, reg, ""));
                            currentr.Total = Regex.Replace(ratios[7].Value, reg, "");
                            currentr.SMALLWIN = (Regex.Replace(ratios[8].Value, reg, ""));
                        }
                        db.Game_FootBall_VSRatios.InsertOnSubmit(currentr);
                        db.SubmitChanges();
                    }//行循环盘类别
                    db.SubmitChanges();
                    nextwrite = newvs;
                    //存起来先准备好联赛，再循环跑
                }//无比赛
                else
                {

                    foreach (Match rratio in findmaintr.Matches(tabs[1].Value))
                    {
                        MatchCollection htmratios = findtds.Matches(rratio.Value);
                        IQueryable<Linq.Game_FootBall_VSRatios> DbRatios = Linq.ProgramLogic.GameVSGetRatios(db, toupdate);
                        Linq.Game_FootBall_VSRatios findcr = DbRatios.SingleOrDefault(t =>
                            t.aspnet_UserID == toupdate.aspnet_UserID
                            && t.GameKey == toupdate.GameKey
                            && t.RatioType == (Regex.Replace(htmratios[0].Value, reg, "").Replace("澳门", "").Replace("皇冠", "")));
                        if (findcr == null)
                        {

                            Linq.Game_FootBall_VSRatios currentr = new Linq.Game_FootBall_VSRatios();

                            currentr.aspnet_UserID = toupdate.aspnet_UserID;
                            currentr.GameKey = toupdate.GameKey;


                            currentr.RatioIndex = (DbRatios.Count() == 0 ? 0 : DbRatios.Max(t => t.RatioIndex));
                            currentr.RatioIndex += 1;



                            currentr.RatioType = (Regex.Replace(htmratios[0].Value, reg, "").Replace("澳门", "").Replace("皇冠", ""));
                            currentr.A_WIN = (Regex.Replace(htmratios[1].Value, reg, ""));
                            currentr.Winless = Regex.Replace(htmratios[2].Value, reg, "");
                            currentr.B_WIN = (Regex.Replace(htmratios[3].Value, reg, ""));
                            currentr.RCompanyType = Enum.GetName(typeof(Linq.ProgramLogic.BallCompanyType), PcompanyType);
                            if (DoBallType == BallType.足球)
                            {
                                currentr.BIGWIN = (Regex.Replace(htmratios[7].Value, reg, ""));
                                currentr.Total = Regex.Replace(htmratios[8].Value, reg, "");
                                currentr.SMALLWIN = (Regex.Replace(htmratios[9].Value, reg, ""));
                            }
                            else if (DoBallType == BallType.篮球)
                            {
                                currentr.BIGWIN = (Regex.Replace(htmratios[6].Value, reg, ""));
                                currentr.Total = Regex.Replace(htmratios[7].Value, reg, "");
                                currentr.SMALLWIN = (Regex.Replace(htmratios[8].Value, reg, ""));
                            }
                            db.Game_FootBall_VSRatios.InsertOnSubmit(currentr);
                            db.SubmitChanges();
                        }//找到当前盘或初始盘
                        //采集的是皇冠或采集和要更新的是同一名字的
                        else if (PcompanyType == Linq.ProgramLogic.BallCompanyType.皇冠 ||
                            ((Linq.ProgramLogic.BallCompanyType)Enum.Parse(typeof(Linq.ProgramLogic.BallCompanyType), findcr.RCompanyType)) == PcompanyType
                            )
                        {

                            findcr.RatioType = (Regex.Replace(htmratios[0].Value, reg, "").Replace("澳门", "").Replace("皇冠", ""));
                            findcr.A_WIN = (Regex.Replace(htmratios[1].Value, reg, ""));
                            findcr.Winless = Regex.Replace(htmratios[2].Value, reg, "");
                            findcr.B_WIN = (Regex.Replace(htmratios[3].Value, reg, ""));

                            if (DoBallType == BallType.足球)
                            {
                                findcr.BIGWIN = (Regex.Replace(htmratios[7].Value, reg, ""));
                                findcr.Total = Regex.Replace(htmratios[8].Value, reg, "");
                                findcr.SMALLWIN = (Regex.Replace(htmratios[9].Value, reg, ""));

                            }
                            else
                            {
                                findcr.BIGWIN = (Regex.Replace(htmratios[6].Value, reg, ""));
                                findcr.Total = Regex.Replace(htmratios[7].Value, reg, "");
                                findcr.SMALLWIN = (Regex.Replace(htmratios[8].Value, reg, ""));
                            }

                            db.SubmitChanges();



                        }//盘行数不一样

                    }//循环
                    toupdate.Jobid = GlobalParam.JobID;
                    toupdate.LastAliveTime = DateTime.Now;

                    db.SubmitChanges();

                    nextwrite = toupdate;
                }//有比赛


                #region toupdate 写入数据库



                Linq.Game_Football_LastRatio lr = db.Game_Football_LastRatio.SingleOrDefault(t => t.GameKey == nextwrite.GameKey
                    && t.aspnet_UserID == GlobalParam.UserKey);
                if (lr == null)
                {
                    Linq.Game_Football_LastRatio newr = new Linq.Game_Football_LastRatio();

                    newr.aspnet_UserID = GlobalParam.UserKey;
                    newr.GameKey = nextwrite.GameKey;
                    newr.GameTime = nextwrite.GameTime;
                    newr.GameVS = nextwrite.GameVS;
                    newr.A_Team = nextwrite.A_Team;
                    newr.B_Team = nextwrite.B_Team;

                    Linq.Game_FootBall_VSRatios lrr = Linq.ProgramLogic.VSGetCurRatio(nextwrite, db);
                    if (lrr != null)
                    {
                        newr.A_WIN = lrr.A_WIN;
                        newr.Winless = lrr.Winless;
                        newr.B_Win = lrr.B_WIN;
                        newr.BIGWIN = lrr.BIGWIN;
                        newr.Total = lrr.Total;
                        newr.SMALLWIN = lrr.SMALLWIN;

                        newr.R1_0_A = lrr.R1_0_A;
                        newr.R2_0_A = lrr.R1_0_A;
                        newr.R2_1_A = lrr.R1_0_A;
                        newr.R3_0_A = lrr.R1_0_A;
                        newr.R3_1_A = lrr.R1_0_A;
                        newr.R3_2_A = lrr.R1_0_A;
                        newr.R4_0_A = lrr.R1_0_A;
                        newr.R4_1_A = lrr.R1_0_A;
                        newr.R4_2_A = lrr.R1_0_A;
                        newr.R4_3_A = lrr.R1_0_A;

                        newr.R1_0_B = lrr.R1_0_B;
                        newr.R2_0_B = lrr.R1_0_B;
                        newr.R2_1_B = lrr.R1_0_B;
                        newr.R3_0_B = lrr.R1_0_B;
                        newr.R3_1_B = lrr.R1_0_B;
                        newr.R3_2_B = lrr.R1_0_B;
                        newr.R4_0_B = lrr.R1_0_B;
                        newr.R4_1_B = lrr.R1_0_B;
                        newr.R4_2_B = lrr.R1_0_B;
                        newr.R4_3_B = lrr.R1_0_B;


                        newr.R0_0 = lrr.R0_0;
                        newr.R1_1 = lrr.R1_1;
                        newr.R2_2 = lrr.R2_2;
                        newr.R3_3 = lrr.R3_3;
                        newr.R4_4 = lrr.R4_4;
                        newr.ROTHER = lrr.ROTHER;

                        newr.R_A_A = lrr.R_A_A;
                        newr.R_A_SAME = lrr.R_A_SAME;
                        newr.R_A_B = lrr.R_A_B;
                        newr.R_SAME_A = lrr.R_SAME_A;
                        newr.R_SAME_SAME = lrr.R_SAME_SAME;
                        newr.R_SAME_B = lrr.R_SAME_B;

                        newr.R_B_A = lrr.R_B_A;
                        newr.R_B_SAME = lrr.R_B_SAME;
                        newr.R_B_SAME = lrr.R_B_SAME;


                    }

                    db.Game_Football_LastRatio.InsertOnSubmit(newr);
                    db.SubmitChanges();
                }//没群模拟下单
                else
                {
                    lr.GameTime = nextwrite.GameTime;
                    lr.GameVS = nextwrite.GameVS;
                    lr.A_Team = nextwrite.A_Team;
                    lr.B_Team = nextwrite.B_Team;
                    Linq.Game_FootBall_VSRatios lrr = Linq.ProgramLogic.VSGetCurRatio(nextwrite, db);
                    if (lrr != null)
                    {
                        lr.A_WIN = lrr.A_WIN;
                        lr.Winless = lrr.Winless;
                        lr.B_Win = lrr.B_WIN;
                        lr.BIGWIN = lrr.BIGWIN;
                        lr.Total = lrr.Total;
                        lr.SMALLWIN = lrr.SMALLWIN;

                        lr.R1_0_A = lrr.R1_0_A;
                        lr.R2_0_A = lrr.R1_0_A;
                        lr.R2_1_A = lrr.R1_0_A;
                        lr.R3_0_A = lrr.R1_0_A;
                        lr.R3_1_A = lrr.R1_0_A;
                        lr.R3_2_A = lrr.R1_0_A;
                        lr.R4_0_A = lrr.R1_0_A;
                        lr.R4_1_A = lrr.R1_0_A;
                        lr.R4_2_A = lrr.R1_0_A;
                        lr.R4_3_A = lrr.R1_0_A;

                        lr.R1_0_B = lrr.R1_0_B;
                        lr.R2_0_B = lrr.R1_0_B;
                        lr.R2_1_B = lrr.R1_0_B;
                        lr.R3_0_B = lrr.R1_0_B;
                        lr.R3_1_B = lrr.R1_0_B;
                        lr.R3_2_B = lrr.R1_0_B;
                        lr.R4_0_B = lrr.R1_0_B;
                        lr.R4_1_B = lrr.R1_0_B;
                        lr.R4_2_B = lrr.R1_0_B;
                        lr.R4_3_B = lrr.R1_0_B;


                        lr.R0_0 = lrr.R0_0;
                        lr.R1_1 = lrr.R1_1;
                        lr.R2_2 = lrr.R2_2;
                        lr.R3_3 = lrr.R3_3;
                        lr.R4_4 = lrr.R4_4;
                        lr.ROTHER = lrr.ROTHER;

                        lr.R_A_A = lrr.R_A_A;
                        lr.R_A_SAME = lrr.R_A_SAME;
                        lr.R_A_B = lrr.R_A_B;
                        lr.R_SAME_A = lrr.R_SAME_A;
                        lr.R_SAME_SAME = lrr.R_SAME_SAME;
                        lr.R_SAME_B = lrr.R_SAME_B;

                        lr.R_B_A = lrr.R_B_A;
                        lr.R_B_SAME = lrr.R_B_SAME;
                        lr.R_B_SAME = lrr.R_B_SAME;


                    }

                    db.SubmitChanges();



                }




                #endregion

            }//皇冠盘循环







        }

        private void ThreadRefreshOther()
        {

            while (true)
            {

                try
                {
                    if (ExitKill)
                    {
                        return;
                    }
                    Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                    //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                    ////db.ObjectTrackingEnabled = false;
                    var source = db.Game_FootBall_VS.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                        // && (t.LastAliveTime == null || t.LastAliveTime >= DateTime.Today.AddDays(-3))
                         && t.Jobid == GlobalParam.JobID
                        && t.GameType == Enum.GetName(typeof(BallType), BallType.足球));
                    foreach (string gamekey in source.Select(t => t.GameKey))
                    {
                        RefreshotherV2(gamekey);
                        Application.DoEvents();
                        Thread.Sleep(200);
                    }

                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine(AnyError.Message, true);
                    NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                }
                Thread.Sleep(5000);
            }
        }
        private void PrepareClassData()
        {


            StreamReader sr = new StreamReader(Application.StartupPath + "\\FullSource.html");
            string html = sr.ReadToEnd();
            sr.Close();
            Regex findDatatop = new Regex("<div id=\"data_top((?!(/div))[\\s\\S])+/div>", RegexOptions.IgnoreCase);
            string strdatatop = findDatatop.Match(html).Value;

            Regex findtablestart = new Regex("<div id=\"" + "data_main" + "((?!(<tbody>))[\\s\\S])+<tbody>", RegexOptions.IgnoreCase);
            string strtablestart = findtablestart.Match(html).Value;


            try
            {

                #region 分联赛发图
                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                ////db.ObjectTrackingEnabled = false;
                var source = db.Game_FootBall_VS.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                    // && (t.LastAliveTime == null || t.LastAliveTime >= DateTime.Today.AddDays(-3))
                     && t.Jobid == GlobalParam.JobID
                    );
                var GameMatcheClasss = (from t in source
                                        select new { t.GameType, t.MatchClass }

                                ).Distinct().ToArray();
                //Linq.ProgramLogic.GameMatcheClasss.Clear();

                foreach (var matchclassicitem in GameMatcheClasss)
                {
                    Application.DoEvents();
                    var Marges = source.Where(t => t.MatchClass == matchclassicitem.MatchClass


                        && t.Jobid == GlobalParam.JobID

                        );

                    #region 准备存文本的格式
                    string outputtxt = "";
                    Int32 MaxTeamCount = 0;
                    Int32 GroupIndexCount = 0;
                    foreach (var matchitem in Marges)
                    {
                        outputtxt += Linq.ProgramLogic.GetMatchGameString(matchitem, db, false);
                        MaxTeamCount += 1;
                        if (MaxTeamCount >= 8)
                        {
                            if (File.Exists(Application.StartupPath + "\\联赛_" + matchclassicitem.GameType + matchclassicitem.MatchClass + "_" + (GroupIndexCount.ToString()) + ".txt") == false)
                            {
                                FileStream fsc = File.Create(Application.StartupPath + "\\Output\\联赛_" + matchclassicitem.GameType + matchclassicitem.MatchClass + "_" + (GroupIndexCount.ToString()) + ".txt");
                                fsc.Flush();
                                fsc.Close();
                            }
                            FileStream fstxt_sub = new FileStream(Application.StartupPath + "\\Output\\联赛_" + matchclassicitem.GameType + matchclassicitem.MatchClass + "_" + (GroupIndexCount.ToString()) + ".txt", FileMode.OpenOrCreate);
                            byte[] writeintxt_sub = Encoding.UTF8.GetBytes(outputtxt);
                            fstxt_sub.Write(writeintxt_sub, 0, writeintxt_sub.Length);
                            fstxt_sub.Flush();
                            fstxt_sub.Close();
                            outputtxt = "";

                            MaxTeamCount = 0;
                            GroupIndexCount += 1;
                        }
                    }
                    #endregion
                    if (File.Exists(Application.StartupPath + "\\联赛_" + matchclassicitem.GameType + matchclassicitem.MatchClass + "_" + (GroupIndexCount.ToString()) + ".txt") == false)
                    {
                        FileStream fsc = File.Create(Application.StartupPath + "\\Output\\联赛_" + matchclassicitem.GameType + matchclassicitem.MatchClass + "_" + (GroupIndexCount.ToString()) + ".txt");
                        fsc.Flush();
                        fsc.Close();
                    }
                    FileStream fstxt = new FileStream(Application.StartupPath + "\\Output\\联赛_" + matchclassicitem.GameType + matchclassicitem.MatchClass + "_" + (GroupIndexCount.ToString()) + ".txt", FileMode.OpenOrCreate);
                    byte[] writeintxt = Encoding.UTF8.GetBytes(outputtxt);
                    fstxt.Write(writeintxt, 0, writeintxt.Length);
                    fstxt.Flush();
                    fstxt.Close();



                    #region 保存JPG，不用了
                    continue;
                    string TypeString = "";
                    foreach (var matchitem in Marges)
                    {
                        //Body上加上原来的TR数据
                        TypeString += matchitem.RowDataWithName;
                    }



                    string NewTotalHtml = Regex.Replace(html, "<body((?!</body)[\\s\\S]+)</body>", "<body style=\"font-family:微软雅黑; min-width:850px;\">" + strdatatop + strtablestart + TypeString + "</tbody></table></div>" + "</body>", RegexOptions.IgnoreCase);
                    NewTotalHtml = Regex.Replace(NewTotalHtml, "<script((?!</script>)[\\s\\S])+</script>", "", RegexOptions.IgnoreCase);
                    NewTotalHtml = Regex.Replace(NewTotalHtml, "<iframe((?!</iframe>)[\\s\\S])+</iframe>", "", RegexOptions.IgnoreCase);

                    NewTotalHtml = Regex.Replace(NewTotalHtml, "</head>", "<script src=\"../jquery-1.2.6.pack.js\" type=\"text/javascript\"></script></head>", RegexOptions.IgnoreCase);

                    if (File.Exists(Application.StartupPath + "\\output\\" + matchclassicitem.GameType + matchclassicitem.MatchClass + ".htm") == false)
                    {
                        FileStream fc = File.Create(Application.StartupPath + "\\output\\" + matchclassicitem.GameType + matchclassicitem.MatchClass + ".htm");
                        fc.Flush();
                        fc.Close();
                    }

                    FileStream Newfswb = new FileStream(Application.StartupPath + "\\output\\" + matchclassicitem.GameType + matchclassicitem.MatchClass + ".htm", FileMode.OpenOrCreate);
                    byte[] Newwbs = Encoding.UTF8.GetBytes(NewTotalHtml);
                    Newfswb.Write(new byte[] { 0xEF, 0XBB, 0XBF }, 0, 3);
                    Newfswb.Write(Newwbs, 0, Newwbs.Length);
                    Newfswb.Flush();
                    Newfswb.Close();
                    object gst = null;
                    this.Invoke(new Action(() =>
                    {
                        //lock (NetFramework.Util_CEF.LockLoad)
                        {
                            NetFramework.Util_CEF.LockLoad = !((bool)NetFramework.Util_CEF.LockLoad);
                            wb_other.WebView.LoadUrl("file:///" + Application.StartupPath + "\\output\\" + matchclassicitem.GameType + matchclassicitem.MatchClass + ".htm");







                            try
                            {


                                gst = wb_other.WebView.EvalScript("$('body').height()");
                            }
                            catch (Exception AnyError)
                            {
                                NetFramework.Console.WriteLine(AnyError.Message, true);
                                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                            }
                        }
                    }));
                    string jsheight = gst == null ? "1024" : gst.ToString();
                    Int32 njsheighy = Convert.ToInt32(jsheight);
                    Bitmap wbOutputbitmap = null;
                    this.Invoke(new Action(() =>
                    {



                        wbOutputbitmap = WebSnapshotsHelper.GetWebSiteThumbnail("file:///" + Application.StartupPath + "\\output\\" + matchclassicitem.GameType + matchclassicitem.MatchClass + ".htm", 840 + 50, njsheighy + 50, 840 + 50, njsheighy + 50); //宽高根据要获取快照的网页决定

                    }));
                    if (File.Exists(Application.StartupPath + "\\output\\" + "联赛_" + matchclassicitem.GameType + matchclassicitem.MatchClass + ".jpg"))
                    {
                        File.Delete(Application.StartupPath + "\\output\\" + "联赛_" + matchclassicitem.GameType + matchclassicitem.MatchClass + ".jpg");
                    }
                    NetFramework.Console.WriteLine("正在保存" + Application.StartupPath + "\\output\\" + "联赛_" + matchclassicitem.GameType + matchclassicitem.MatchClass + ".jpg", false);
                    wbOutputbitmap.Save(Application.StartupPath + "\\output\\" + "联赛_" + matchclassicitem.GameType + matchclassicitem.MatchClass + ".jpg"
                        , System.Drawing.Imaging.ImageFormat.Jpeg
                        );
                    wbOutputbitmap.Dispose();
                    #endregion
                    Application.DoEvents();

                }//每个联赛分类



                #endregion





            }
            catch (Exception AnyError)
            {

                NetFramework.Console.WriteLine(AnyError.Message, true);
                NetFramework.Console.WriteLine(AnyError.StackTrace, true);



            }
        }
        private void RefreshotherV2(string GameKey)
        {

            string NewUrl = "http://odds.gooooal.com/singlefield.html?mid=" + GameKey.Replace("m_", "") + "&Type=5";
            //NewUrl = "http://odds.gooooal.com/singlefield.html?mid=1394090&type=5";

            string refreshhtml = "";
            this.Invoke(new Action(() =>
            {
                refreshhtml = NetFramework.Util_CEF.JoinQueueAndWait(NewUrl, wb_refresh);
            }));


            Regex FindShow = FindShow = new Regex(@"<div\s*id=""data_main5""[^>]*>((?<mm><div[^>]*>)+|(?<-mm></div>)|[\s\S])*?(?(mm)(?!))</div>", RegexOptions.IgnoreCase);
            string showstr = FindShow.Match(refreshhtml).Value;
            showstr = Regex.Replace(showstr, "<td", "<td style=\"font-size:16px!important;\"", RegexOptions.IgnoreCase);

            refreshhtml = Regex.Replace(refreshhtml, "<script((?!</script>)[\\s\\S])+</script>", "", RegexOptions.IgnoreCase);
            refreshhtml = Regex.Replace(refreshhtml, "<iframe((?!</iframe>)[\\s\\S])+</iframe>", "", RegexOptions.IgnoreCase);


            #region 波胆解析
            Regex findtbls = new Regex(@"<table[^>]*>((?<mm><table[^>]*>)+|(?<-mm></table>)|[\s\S])*?(?(mm)(?!))</table>", RegexOptions.IgnoreCase);
            MatchCollection tables = findtbls.Matches(showstr);
            Regex findmaintr = new Regex(@"<tr[^>]*>((?<mm><tr[^>]*>)+|(?<-mm></tr>)|[\s\S])*?(?(mm)(?!))</tr>", RegexOptions.IgnoreCase);
            Regex findtds = new Regex(@"<td[^>]*>((?<mm><td[^>]*>)+|(?<-mm></td>)|[\s\S])*?(?(mm)(?!))</td>", RegexOptions.IgnoreCase);
            if (tables.Count < 2)
            {
                return;
            }
            MatchCollection companys = findmaintr.Matches(tables[1].Value);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            foreach (Match cmpitem in companys)
            {
                MatchCollection data_td = findtds.Matches(cmpitem.Value);
                if (data_td.Count >= 2)
                {
                    if (NetFramework.Util_WEB.CleanHtml(data_td[0].Value).Contains("澳彩"))
                    {
                        var source = db.Game_FootBall_VS.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                            // && (t.LastAliveTime == null || t.LastAliveTime >= DateTime.Today.AddDays(-3))
                             && t.Jobid == GlobalParam.JobID
                            );
                        Linq.Game_FootBall_VS gamem_u = source.SingleOrDefault(t => t.GameKey == GameKey);
                        if (gamem_u != null)
                        {

                            Linq.Game_FootBall_VSRatios ratio_u = Linq.ProgramLogic.VSGetCurRatio(gamem_u, db);
                            if (ratio_u != null)
                            {
                                ratio_u.R_A_A = NetFramework.Util_WEB.CleanHtml(data_td[1].Value);

                                ratio_u.R_A_SAME = NetFramework.Util_WEB.CleanHtml(data_td[2].Value);

                                ratio_u.R_A_B = NetFramework.Util_WEB.CleanHtml(data_td[3].Value);

                                ratio_u.R_SAME_A = NetFramework.Util_WEB.CleanHtml(data_td[4].Value);

                                ratio_u.R_SAME_SAME = NetFramework.Util_WEB.CleanHtml(data_td[5].Value);

                                ratio_u.R_SAME_B = NetFramework.Util_WEB.CleanHtml(data_td[6].Value);

                                ratio_u.R_B_A = NetFramework.Util_WEB.CleanHtml(data_td[7].Value);

                                ratio_u.R_B_SAME = NetFramework.Util_WEB.CleanHtml(data_td[8].Value);

                                ratio_u.R_B_B = NetFramework.Util_WEB.CleanHtml(data_td[8].Value);
                            }

                        }
                    }

                }



            }
            if (tables.Count >= 4)
            {


                MatchCollection companys_p = findmaintr.Matches(tables[3].Value);
                Int32 TRINDEX = 0;
                foreach (Match cmpitem in companys_p)
                {

                    MatchCollection subtbls = findtbls.Matches(findtds.Matches(cmpitem.Value)[0].Value);
                    if (subtbls.Count == 0)
                    {
                        continue;
                    }
                    MatchCollection subtrs = findmaintr.Matches(subtbls[0].Value);

                    MatchCollection data_A_td = findtds.Matches(subtrs[0].Value);

                    if (data_A_td.Count >= 2)
                    {
                        if (data_A_td[0].Value.Contains("澳彩"))
                        {
                            MatchCollection data_B_td = findtds.Matches(subtrs[1].Value);
                            var source = db.Game_FootBall_VS.Where(t =>
                                t.aspnet_UserID == GlobalParam.UserKey
                                    // && (t.LastAliveTime == null || t.LastAliveTime >= DateTime.Today.AddDays(-3))
                                 && t.Jobid == GlobalParam.JobID
                                );
                            Linq.Game_FootBall_VS gamem_u = source.SingleOrDefault(t => t.GameKey == GameKey);


                            if (gamem_u != null)
                            {

                                Linq.Game_FootBall_VSRatios ratio_u = Linq.ProgramLogic.VSGetCurRatio(gamem_u, db);
                                if (ratio_u != null)
                                {
                                    ratio_u.R1_0_A = NetFramework.Util_WEB.CleanHtml(data_A_td[2].Value);
                                    ratio_u.R1_0_B = NetFramework.Util_WEB.CleanHtml(data_B_td[1].Value);

                                    ratio_u.R2_0_A = NetFramework.Util_WEB.CleanHtml(data_A_td[3].Value);
                                    ratio_u.R2_0_B = NetFramework.Util_WEB.CleanHtml(data_B_td[2].Value);

                                    ratio_u.R2_1_A = NetFramework.Util_WEB.CleanHtml(data_A_td[4].Value);
                                    ratio_u.R2_1_B = NetFramework.Util_WEB.CleanHtml(data_B_td[3].Value);

                                    ratio_u.R3_0_A = NetFramework.Util_WEB.CleanHtml(data_A_td[5].Value);
                                    ratio_u.R3_0_B = NetFramework.Util_WEB.CleanHtml(data_B_td[4].Value);

                                    ratio_u.R3_1_A = NetFramework.Util_WEB.CleanHtml(data_A_td[6].Value);
                                    ratio_u.R3_1_B = NetFramework.Util_WEB.CleanHtml(data_B_td[5].Value);

                                    ratio_u.R3_2_A = NetFramework.Util_WEB.CleanHtml(data_A_td[7].Value);
                                    ratio_u.R3_2_B = NetFramework.Util_WEB.CleanHtml(data_B_td[6].Value);



                                    ratio_u.R4_0_A = NetFramework.Util_WEB.CleanHtml(data_A_td[8].Value);
                                    ratio_u.R4_0_B = NetFramework.Util_WEB.CleanHtml(data_B_td[7].Value);

                                    ratio_u.R4_1_A = NetFramework.Util_WEB.CleanHtml(data_A_td[9].Value);
                                    ratio_u.R4_1_B = NetFramework.Util_WEB.CleanHtml(data_B_td[8].Value);

                                    ratio_u.R4_2_A = NetFramework.Util_WEB.CleanHtml(data_A_td[10].Value);
                                    ratio_u.R4_2_B = NetFramework.Util_WEB.CleanHtml(data_B_td[9].Value);

                                    ratio_u.R4_3_A = NetFramework.Util_WEB.CleanHtml(data_A_td[11].Value);
                                    ratio_u.R4_3_B = NetFramework.Util_WEB.CleanHtml(data_B_td[10].Value);

                                    ratio_u.R0_0 = NetFramework.Util_WEB.CleanHtml(data_A_td[12].Value);

                                    ratio_u.R1_1 = NetFramework.Util_WEB.CleanHtml(data_A_td[13].Value);

                                    ratio_u.R2_2 = NetFramework.Util_WEB.CleanHtml(data_A_td[14].Value);

                                    ratio_u.R3_3 = NetFramework.Util_WEB.CleanHtml(data_A_td[15].Value);

                                    ratio_u.R4_4 = NetFramework.Util_WEB.CleanHtml(data_A_td[16].Value);

                                    ratio_u.ROTHER = NetFramework.Util_WEB.CleanHtml(data_A_td[17].Value);

                                    db.SubmitChanges();

                                }
                            }
                        }


                    }

                    TRINDEX += 1;

                }

            }


            #endregion


            var sourcegame = db.Game_FootBall_VS.Where(t =>
                t.aspnet_UserID == GlobalParam.UserKey
                    //&& (t.LastAliveTime == null || t.LastAliveTime >= DateTime.Today.AddDays(-3))
                 && t.Jobid == GlobalParam.JobID
                );
            Linq.Game_FootBall_VS gamem = sourcegame.SingleOrDefault(t => t.GameKey == GameKey);






            NetFramework.Console.WriteLine("正在准备图片" + gamem.GameVS, false);

            StreamReader sr = new StreamReader(Application.StartupPath + "\\Template.Htm");
            string temps = sr.ReadToEnd();
            sr.Close();


            if (Directory.Exists(Application.StartupPath + "\\output") == false)
            {
                Directory.CreateDirectory(Application.StartupPath + "\\output");
            }

            List<Linq.Game_FootBall_VSRatios> DbRatios_gamem = Linq.ProgramLogic.GameVSGetRatios(db, gamem).ToList();

            Linq.Game_FootBall_VSRatios curratio = Linq.ProgramLogic.VSGetCurRatio(gamem, db);
            #region 保存成图片

            string Saves = temps;
            Saves = Saves.Replace("{A_Team}", gamem.A_Team);
            Saves = Saves.Replace("{B_Team}", gamem.B_Team);
            Saves = Saves.Replace("{比赛时间}", Convert.ToDateTime("2020-" + gamem.GameTime).ToString("MM月dd日 HH:mm"));
            Saves = Saves.Replace("{比赛类型}", gamem.MatchClass);

            for (int i = 0; i < DbRatios_gamem.Count(); i++)
            {
                Saves = Saves.Replace("{盘类型" + i.ToString() + "}", DbRatios_gamem[i].RatioType);
                Saves = Saves.Replace("{A赢" + i.ToString() + "}", DbRatios_gamem[i].A_WIN);
                Saves = Saves.Replace("{让球" + i.ToString() + "}", DbRatios_gamem[i].Winless);
                Saves = Saves.Replace("{B赢" + i.ToString() + "}", DbRatios_gamem[i].B_WIN);
                Saves = Saves.Replace("{大球" + i.ToString() + "}", DbRatios_gamem[i].BIGWIN);
                Saves = Saves.Replace("{总球" + i.ToString() + "}", DbRatios_gamem[i].Total);
                Saves = Saves.Replace("{小球" + i.ToString() + "}", DbRatios_gamem[i].SMALLWIN);
            }

            Saves = Saves.Replace("{IsHide3}", DbRatios_gamem.Count >= 3 ? "" : "display:none");

            Saves = Saves.Replace("{IsHide2}", DbRatios_gamem.Count >= 2 ? "" : "display:none");
            if (curratio != null)
            {


                Saves = Saves.Replace("{R1_0_A}", curratio.R1_0_A);
                Saves = Saves.Replace("{R2_0_A}", curratio.R2_0_A);
                Saves = Saves.Replace("{R2_1_A}", curratio.R2_1_A);
                Saves = Saves.Replace("{R3_0_A}", curratio.R3_0_A);
                Saves = Saves.Replace("{R3_1_A}", curratio.R3_1_A);
                Saves = Saves.Replace("{R3_2_A}", curratio.R3_2_A);
                Saves = Saves.Replace("{R4_0_A}", curratio.R4_0_A);
                Saves = Saves.Replace("{R4_1_A}", curratio.R4_1_A);
                Saves = Saves.Replace("{R4_2_A}", curratio.R4_2_A);
                Saves = Saves.Replace("{R4_3_A}", curratio.R4_3_A);

                Saves = Saves.Replace("{R1_0_B}", curratio.R1_0_B);
                Saves = Saves.Replace("{R2_0_B}", curratio.R2_0_B);
                Saves = Saves.Replace("{R2_1_B}", curratio.R2_1_B);
                Saves = Saves.Replace("{R3_0_B}", curratio.R3_0_B);
                Saves = Saves.Replace("{R3_1_B}", curratio.R3_1_B);
                Saves = Saves.Replace("{R3_2_B}", curratio.R3_2_B);
                Saves = Saves.Replace("{R4_0_B}", curratio.R4_0_B);
                Saves = Saves.Replace("{R4_1_B}", curratio.R4_1_B);
                Saves = Saves.Replace("{R4_2_B}", curratio.R4_2_B);
                Saves = Saves.Replace("{R4_3_B}", curratio.R4_3_B);


                Saves = Saves.Replace("{R0_0}", curratio.R0_0);
                Saves = Saves.Replace("{R1_1}", curratio.R1_1);
                Saves = Saves.Replace("{R2_2}", curratio.R2_2);
                Saves = Saves.Replace("{R3_3}", curratio.R3_3);
                Saves = Saves.Replace("{R4_4}", curratio.R4_4);

                Saves = Saves.Replace("{Rother}", curratio.ROTHER);


                Saves = Saves.Replace("{R_A_A}", curratio.R_A_A);
                Saves = Saves.Replace("{R_A_SAME}", curratio.R_A_SAME);
                Saves = Saves.Replace("{R_A_B}", curratio.R_A_B);
                Saves = Saves.Replace("{R_SAME_A}", curratio.R_SAME_A);
                Saves = Saves.Replace("{R_SAME_SAME}", curratio.R_SAME_SAME);
                Saves = Saves.Replace("{R_SAME_B}", curratio.R_SAME_B);
                Saves = Saves.Replace("{R_B_A}", curratio.R_B_A);
                Saves = Saves.Replace("{R_B_SAME}", curratio.R_B_SAME);
                Saves = Saves.Replace("{R_B_B}", curratio.R_B_B);
            }
            if (File.Exists(Application.StartupPath + "\\tmp.htm") == false)
            {
                FileStream fsc = File.Create(Application.StartupPath + "\\tmp.htm");
                fsc.Flush();
                fsc.Close();
                fsc.Dispose();
            }
            FileStream fs = new FileStream(Application.StartupPath + "\\tmp.htm", FileMode.OpenOrCreate);
            byte[] bsource = Encoding.UTF8.GetBytes(Saves);
            fs.Write(new byte[] { 0xEF, 0XBB, 0XBF }, 0, 3);
            fs.Write(bsource, 0, bsource.Length);
            fs.Close();

            Object gst = null;
            this.Invoke(new Action(() =>
                       {

                           //lock (NetFramework.Util_CEF.LockLoad)
                           {
                               NetFramework.Util_CEF.LockLoad = !((bool)NetFramework.Util_CEF.LockLoad);
                               wb_other.WebView.LoadUrl("file:///" + Application.StartupPath + "\\tmp.htm");


                           }
                           try
                           {


                               (wb_other).WebView.EvalScript("$('#datatable').height()");
                           }
                           catch (Exception anyerror)
                           {
                               NetFramework.Console.WriteLine(anyerror.Message, true);
                               NetFramework.Console.WriteLine(anyerror.StackTrace, true);

                           }
                       }));



            string jsheight = gst == null ? "1024" : gst.ToString();
            Int32 njsheight = Convert.ToInt32(jsheight);
            try
            {


                gst = (wb_other).WebView.EvalScript("$('#datatable').width()");
            }
            catch (Exception anyerror)
            {
                NetFramework.Console.WriteLine(anyerror.Message, true);
                NetFramework.Console.WriteLine(anyerror.StackTrace, true);

            }
            string jshwidth = gst == null ? "1024" : gst.ToString();
            Int32 njswdith = Convert.ToInt32(jshwidth);


            if (Directory.Exists(Application.StartupPath + "\\output") == false)
            {
                Directory.CreateDirectory(Application.StartupPath + "\\output");
            }
            Bitmap Outputbitmap = null;
            this.Invoke(new Action(() =>
            {
                Outputbitmap = WebSnapshotsHelper.GetWebSiteThumbnail("file:///" + Application.StartupPath + "\\tmp.htm", 743 + 50, njsheight + 50, 743 + 50, njsheight + 50); //宽高根据要获取快照的网页决定

            }));
            if (File.Exists(Application.StartupPath + "\\output\\" + gamem.GameKey + ".jpg"))
            {
                File.Delete(Application.StartupPath + "\\output\\" + gamem.GameKey + ".jpg");
            }
            Outputbitmap.Save(Application.StartupPath + "\\output\\" + gamem.GameKey + ".jpg"
                , System.Drawing.Imaging.ImageFormat.Jpeg
                );
            Outputbitmap.Dispose();



            #endregion


            #region 保存成文本
            string SendKeyGame = Linq.ProgramLogic.GetMatchGameString(gamem, db);

            if (File.Exists(Application.StartupPath + "\\output\\" + gamem.GameKey + ".txt") == false)
            {
                FileStream fsc = File.Create(Application.StartupPath + "\\output\\" + gamem.GameKey + ".txt");
                fsc.Flush();
                fsc.Close();
            }
            FileStream fsw = new FileStream(Application.StartupPath + "\\output\\" + gamem.GameKey + ".txt", FileMode.OpenOrCreate);
            byte[] b_SendKeyGame = Encoding.UTF8.GetBytes(SendKeyGame);
            fsw.Write(b_SendKeyGame, 0, b_SendKeyGame.Length);
            fsw.Flush();
            fsw.Close();
            #endregion

        }
        private bool ExitKill = false;
        private bool otherstart = false;
        private void ThreadStartGetBallRatioV2()
        {
            while (true)
            {

                try
                {
                    if (ExitKill)
                    {
                        return;
                    }
                    if (IsRefreshBall == false)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    RefreshballV2(wb_ballgame, "mm_content", BallType.篮球, Linq.ProgramLogic.BallCompanyType.皇冠);
                    RefreshballV2(wb_ballgame, "data_main", BallType.足球, Linq.ProgramLogic.BallCompanyType.皇冠);
                    RefreshballV2(wb_ballgame, "mm_content", BallType.篮球, Linq.ProgramLogic.BallCompanyType.澳彩);
                    RefreshballV2(wb_ballgame, "data_main", BallType.足球, Linq.ProgramLogic.BallCompanyType.澳彩);

                    PrepareClassData();
                    if (otherstart == false)
                    {
                        Thread ThreadClass = new Thread(new ThreadStart(ThreadRefreshOther));
                        ThreadClass.Start();
                        otherstart = true;
                    }

                }
                catch (Exception AnyError)
                {

                    NetFramework.Console.WriteLine(AnyError.Message, true);
                    NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                    return;
                }
                Thread.Sleep(5000);
            }

        }
        private bool CheckHasStart = false;
        private void ThreadRepeatCheckPoint()
        {
            if (CheckHasStart == true)
            {
                return;
            }

            CheckHasStart = true;
            while (true)
            {

                try
                {
                    if (ExitKill)
                    {
                        return;
                    }
                    if (IsRefreshBall == false)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    GetAndSetPoint(wb_balllivepoint, BallType.足球);
                    GetAndSetPoint(wb_balllivepoint, BallType.篮球);




                }
                catch (Exception anyError)
                {
                    NetFramework.Console.WriteLine(anyError.Message, true);
                    NetFramework.Console.WriteLine(anyError.StackTrace, true);

                }

                Thread.Sleep(5000);
            }
        }

        private void GetAndSetPoint(EO.WinForm.WebControl balllivepoint, BallType p_balltype)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            string html = "";

            this.Invoke(new Action(() =>
            {
                ((GroupBox)balllivepoint.Parent).Text = Enum.GetName(typeof(BallType), p_balltype) + "正在采集比分";

                if (p_balltype == BallType.足球)
                {
                    html = NetFramework.Util_CEF.JoinQueueAndWait("http://live.gooooal.com", balllivepoint);
                }
                else if (p_balltype == BallType.篮球)
                {
                    html = NetFramework.Util_CEF.JoinQueueAndWait("http://live.gooooal.com/live_bks_new.html", balllivepoint);
                }
            }));


            Regex findtables = new Regex("<table id=\"" + (p_balltype == BallType.足球 ? "tb_data" : "h2") + "\"[^>]*>((?<mm><table[^>]*>)+|(?<-mm></table>)|[\\s\\S])*?(?(mm)(?!))</table>", RegexOptions.IgnoreCase);
            string str_datatable = findtables.Match(html).Value;

            Regex findmaintr = new Regex("<tr id=\"" + (p_balltype == BallType.足球 ? "m" : "r") + "_[^>]*>((?<mm><tr[^>]*>)+|(?<-mm></tr>)|[\\s\\S])*?(?(mm)(?!))</tr>", RegexOptions.IgnoreCase);
            Regex findtds = new Regex(@"<td[^>]*>((?<mm><td[^>]*>)+|(?<-mm></td>)|[\s\S])*?(?(mm)(?!))</td>", RegexOptions.IgnoreCase);

            var trs = findmaintr.Matches(str_datatable);

            Regex findkey = new Regex(

               p_balltype == BallType.足球 ? "m_[0-9]+" : "r_[0-9]+"

                , RegexOptions.IgnoreCase);









            foreach (Match item in trs)
            {

                Application.DoEvents();
                string str_key = findkey.Match(item.Value).Value;

                str_key = str_key.StartsWith("r_") ? "tr_mhd_" + str_key.Substring(2) : str_key;


                Linq.Game_ResultFootBall_Last newsendr = null;



                MatchCollection datas = findtds.Matches(item.Value);




                if (p_balltype == BallType.足球)
                {
                    if (datas.Count >= 6)
                    {
                        Linq.Game_ResultFootBall_Last grl = db.Game_ResultFootBall_Last.SingleOrDefault(t => t.GameKey == str_key && t.aspnet_UserID == GlobalParam.UserKey);
                        if (grl == null)
                        {
                            grl = new Linq.Game_ResultFootBall_Last();
                            grl.aspnet_UserID = GlobalParam.UserKey;
                            grl.GameKey = str_key;

                            grl.A_Team = NetFramework.Util_WEB.CleanHtml(datas[6].Value);

                            grl.A_Team = Regex.Replace(grl.A_Team, "\\[[0-9]+\\]", "", RegexOptions.IgnoreCase);

                            grl.B_Team = NetFramework.Util_WEB.CleanHtml(datas[8].Value);

                            grl.B_Team = Regex.Replace(grl.B_Team, "\\[[0-9]+\\]", "", RegexOptions.IgnoreCase);


                            grl.LastPoint = NetFramework.Util_WEB.CleanHtml(datas[7].Value);

                            grl.HalfPoint = NetFramework.Util_WEB.CleanHtml(datas[9].Value);
                            grl.EndState = NetFramework.Util_WEB.CleanHtml(datas[4].Value);

                            grl.MatchBallType = "足球";
                            grl.MatchClassName = NetFramework.Util_WEB.CleanHtml(datas[2].Value);

                            db.Game_ResultFootBall_Last.InsertOnSubmit(grl);
                            db.SubmitChanges();
                            if (grl.LastPoint.ToUpper() != "VS")
                            {
                                newsendr = grl;
                                GetPointLog(newsendr, db);
                            }

                            if (grl.EndState == "完")
                            {
                                newsendr = grl;
                                GetPointLog(newsendr, db);
                            }


                        }//没记录
                        else
                        {
                            grl.A_Team = NetFramework.Util_WEB.CleanHtml(datas[6].Value);

                            grl.A_Team = Regex.Replace(grl.A_Team, "\\[[0-9]+\\]", "", RegexOptions.IgnoreCase);


                            grl.B_Team = NetFramework.Util_WEB.CleanHtml(datas[8].Value);

                            grl.B_Team = Regex.Replace(grl.B_Team, "\\[[0-9]+\\]", "", RegexOptions.IgnoreCase);



                            if (grl.LastPoint != NetFramework.Util_WEB.CleanHtml(datas[7].Value)
                                && NetFramework.Util_WEB.CleanHtml(datas[7].Value) != "VS")
                            {
                                newsendr = grl;
                                GetPointLog(newsendr, db);
                            }

                            if (grl.EndState != NetFramework.Util_WEB.CleanHtml(datas[4].Value)
                                && NetFramework.Util_WEB.CleanHtml(datas[4].Value) == "完")
                            {
                                newsendr = grl;
                                GetPointLog(newsendr, db);

                            }

                            grl.MatchBallType = "足球";
                            grl.MatchClassName = NetFramework.Util_WEB.CleanHtml(datas[2].Value);

                            grl.LastPoint = NetFramework.Util_WEB.CleanHtml(datas[7].Value);
                            grl.EndState = NetFramework.Util_WEB.CleanHtml(datas[4].Value);

                            grl.HalfPoint = NetFramework.Util_WEB.CleanHtml(datas[9].Value);

                            db.SubmitChanges();




                        }
                    }
                }//足球的
                else if (p_balltype == BallType.篮球)
                {

                    Regex findtable = new Regex("<table[^>]*>((?<mm><table[^>]*>)+|(?<-mm></table>)|[\\s\\S])*?(?(mm)(?!))</table>", RegexOptions.IgnoreCase);
                    if (datas.Count == 0)
                    {
                        return;
                    }
                    string basketballtable = findtable.Match(datas[0].Value).Value;

                    Regex findsubtr = new Regex("<tr[^>]*>((?<mm><tr[^>]*>)+|(?<-mm></tr>)|[\\s\\S])*?(?(mm)(?!))</tr>", RegexOptions.IgnoreCase);

                    Regex findsubth = new Regex("<th[^>]*>((?<mm><th[^>]*>)+|(?<-mm></th>)|[\\s\\S])*?(?(mm)(?!))</th>", RegexOptions.IgnoreCase);


                    MatchCollection subtrs = findsubtr.Matches(basketballtable);

                    if (subtrs.Count >= 3)
                    {
                        MatchCollection rowstd1 = findtds.Matches(subtrs[1].Value);
                        MatchCollection rowstd2 = findtds.Matches(subtrs[2].Value);
                        MatchCollection towwth0 = findsubth.Matches(subtrs[0].Value);


                        if (rowstd1.Count >= 4)
                        {
                            Linq.Game_ResultFootBall_Last grl = db.Game_ResultFootBall_Last.SingleOrDefault(t => t.GameKey == str_key);
                            if (grl == null)
                            {
                                grl = new Linq.Game_ResultFootBall_Last();
                                grl.aspnet_UserID = GlobalParam.UserKey;
                                grl.GameKey = str_key;

                                grl.A_Team = NetFramework.Util_WEB.CleanHtml(rowstd1[1].Value);
                                grl.A_Team = Regex.Replace(grl.A_Team, "\\[[0-9]+\\]", "", RegexOptions.IgnoreCase);

                                grl.B_Team = NetFramework.Util_WEB.CleanHtml(rowstd2[0].Value);
                                grl.B_Team = Regex.Replace(grl.B_Team, "\\[[0-9]+\\]", "", RegexOptions.IgnoreCase);



                                grl.LastPoint = NetFramework.Util_WEB.CleanHtml(rowstd1[3].Value) + "-" + NetFramework.Util_WEB.CleanHtml(rowstd2[2].Value);

                                grl.HalfPoint = NetFramework.Util_WEB.CleanHtml(rowstd1[2].Value) + "-" + NetFramework.Util_WEB.CleanHtml(rowstd2[1].Value);

                                grl.MatchBallType = "篮球";
                                grl.MatchClassName = NetFramework.Util_WEB.CleanHtml(towwth0[1].Value);

                                grl.EndState = NetFramework.Util_WEB.CleanHtml(towwth0[2].Value);

                                db.Game_ResultFootBall_Last.InsertOnSubmit(grl);
                                db.SubmitChanges();
                                if (grl.LastPoint.ToUpper() != "-")
                                {
                                    newsendr = grl;
                                }
                            }//没记录
                            else
                            {

                                grl.A_Team = NetFramework.Util_WEB.CleanHtml(rowstd1[1].Value);
                                grl.A_Team = Regex.Replace(grl.A_Team, "\\[[0-9]+\\]", "", RegexOptions.IgnoreCase);

                                grl.B_Team = NetFramework.Util_WEB.CleanHtml(rowstd2[0].Value);
                                grl.B_Team = Regex.Replace(grl.B_Team, "\\[[0-9]+\\]", "", RegexOptions.IgnoreCase);



                                if (grl.LastPoint != NetFramework.Util_WEB.CleanHtml(rowstd1[3].Value) + "-" + NetFramework.Util_WEB.CleanHtml(rowstd2[2].Value)
                                    && NetFramework.Util_WEB.CleanHtml(rowstd1[3].Value) + "-" + NetFramework.Util_WEB.CleanHtml(rowstd2[2].Value) != "-"
                                    )
                                {
                                    newsendr = grl;

                                }
                                if (grl.EndState != NetFramework.Util_WEB.CleanHtml(towwth0[2].Value)
                                    && NetFramework.Util_WEB.CleanHtml(towwth0[2].Value) == "完")
                                {
                                    newsendr = grl;

                                }
                                grl.EndState = NetFramework.Util_WEB.CleanHtml(towwth0[2].Value);

                                grl.LastPoint = NetFramework.Util_WEB.CleanHtml(rowstd1[3].Value) + "-" + NetFramework.Util_WEB.CleanHtml(rowstd2[2].Value);

                                grl.HalfPoint = NetFramework.Util_WEB.CleanHtml(rowstd1[2].Value) + "-" + NetFramework.Util_WEB.CleanHtml(rowstd2[1].Value);

                                grl.MatchBallType = "篮球";
                                grl.MatchClassName = NetFramework.Util_WEB.CleanHtml(towwth0[1].Value);

                                db.SubmitChanges();


                            }
                        }
                    }//
                }//篮球的



                #region 发到群上
                if (newsendr != null)
                {

                    DataRow[] rows = this.RunnerF.MemberSource.Select();



                    foreach (DataRow rowitem in rows)
                    {
                        string tmpid = rowitem.Field<object>("User_ContactTEMPID").ToString();
                        string UserName = rowitem.Field<object>("User_ContactID").ToString();
                        string SourceType = rowitem.Field<object>("User_SourceType").ToString();

                        Linq.WX_WebSendPICSetting sets = db.WX_WebSendPICSetting.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                            && t.WX_UserName == UserName
                            && t.WX_SourceType == SourceType
                            );

                        Linq.WX_WebSendPICSettingMatchClass subset = db.WX_WebSendPICSettingMatchClass.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                            && t.WX_UserName == UserName
                            && t.WX_SourceType == SourceType
                            && t.MatchBallType == newsendr.MatchBallType
                            && t.MatchClassName == newsendr.MatchClassName
                            );
                        var GameCount = db.WX_UserGameLog_Football.Where(t =>

                              t.aspnet_UserID == GlobalParam.UserKey
                              && t.WX_UserName == UserName
                              && t.WX_SourceType == SourceType
                              && t.GameKey == newsendr.GameKey
                              );

                        //开赛
                        if (sets != null && (sets.ballstart == true || (GameCount.Count() > 0))
                            && (newsendr.LastPoint == "0-0" || newsendr.LastPoint == "-")
                            && Convert.ToBoolean(rowitem.Field<object>("User_IsBallPIC")) == true
                            )
                        {
                            SendRobotContent(
                               newsendr.A_Team + "VS" + newsendr.B_Team + "已开赛"
                               , tmpid, SourceType
                               );
                            newsendr.LiveBallLastSendTime = DateTime.Now;
                            db.SubmitChanges();
                        }
                        //即时比分
                        if
                           (
                                (
                            //勾了的足球
                               (sets != null && sets.balllivepoint == true && (p_balltype == BallType.篮球) == false)
                            //买过的足球或篮球每10分钟
                               || (GameCount.Count() > 0 && (p_balltype == BallType.足球 ||
                               ((p_balltype == BallType.篮球) == true && (newsendr.LiveBallLastSendTime == null || (DateTime.Now - newsendr.LiveBallLastSendTime.Value).TotalMinutes > 10))
                               ))
                            //勾了的篮球间隔10分钟
                               || (sets != null && sets.balllivepoint == true && (p_balltype == BallType.篮球) == true && (newsendr.LiveBallLastSendTime == null || (DateTime.Now - newsendr.LiveBallLastSendTime.Value).TotalMinutes > 10))
                                )
                               &&
                                   (
                            //不是0-0且不是完结的
                                newsendr.LastPoint != "0-0"
                                && newsendr.EndState != "完"
                                   )
                           )
                        {
                            string PointLog = Linq.ProgramLogic.GetPointLog(db, newsendr.GameKey);
                            SendRobotContent(
                                newsendr.A_Team + "VS" + newsendr.B_Team + (PointLog == "" ? "" : (Environment.NewLine + PointLog)) + " ，现时比分" + newsendr.LastPoint
                                , tmpid, SourceType
                                );
                            newsendr.LiveBallLastSendTime = DateTime.Now;
                            db.SubmitChanges();
                        }
                        //完结
                        if (newsendr.EndState == "完")
                        {

                            string[] FrontHalf = newsendr.HalfPoint.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            string[] FullHalf = newsendr.LastPoint.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                            Int32 FrontHalfA = Object2Int(FrontHalf[0]);

                            Int32 FrontHalfB = Object2Int(FrontHalf[1]);

                            Int32 EndHalfA = Object2Int(FullHalf[0]) - FrontHalfA;

                            Int32 EndHalfB = Object2Int(FullHalf[1]) - FrontHalfB;

                            if (
                                (sets != null && sets.ballend == true || (GameCount.Count() > 0))
                                && Convert.ToBoolean(rowitem.Field<object>("User_IsBallPIC")) == true
                                )
                            {





                                string TMP_CheckWhoWin = "";

                                string CheckWhoWin = "";
                                Linq.Game_Football_LastRatio lr = db.Game_Football_LastRatio.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                                            && t.GameKey == newsendr.GameKey
                                            );



                                if (lr != null)
                                {
                                    Linq.ProgramLogic.CaculateATemBTeamBigSmallWinless(
                                    Linq.ProgramLogic.BallBuyType.A_WIN
                                     , lr.Winless, lr.Total,
                                     1, FrontHalfA, FrontHalfB, EndHalfA, EndHalfB, out TMP_CheckWhoWin);

                                    CheckWhoWin += TMP_CheckWhoWin;
                                    Linq.ProgramLogic.CaculateATemBTeamBigSmallWinless(
                                    Linq.ProgramLogic.BallBuyType.BIGWIN
                                     , lr.Winless, lr.Total,
                                     1, FrontHalfA, FrontHalfB, EndHalfA, EndHalfB, out TMP_CheckWhoWin);
                                    CheckWhoWin += "，" + TMP_CheckWhoWin;
                                }




                                SendRobotContent(
                                  newsendr.A_Team + "VS" + newsendr.B_Team + "已完结，赛果" + newsendr.LastPoint + "，" + CheckWhoWin
                                  , tmpid, SourceType
                                  );
                                newsendr.LiveBallLastSendTime = DateTime.Now;
                                db.SubmitChanges();
                            }




                            var toopen = db.WX_UserGameLog_Football.Where(t => t.GameKey == newsendr.GameKey
                                && t.aspnet_UserID == GlobalParam.UserKey
                                && t.HaveOpen == false
                                && t.WX_UserName == UserName
                                && t.WX_SourceType == SourceType
                                );
                            foreach (var openitem in toopen)
                            {

                                string Send = Linq.ProgramLogic.OpenBallGameLog(openitem, db, FrontHalfA, FrontHalfB, EndHalfA, EndHalfB);
                                SendRobotContent(
                                   Send
                                    , tmpid, SourceType
                                    );

                            }


                        }


                    }
                }
                #endregion




            }//赛事循环



        }
        private void GetPointLog(Linq.Game_ResultFootBall_Last toget, Linq.dbDataContext db)
        {
            string html = "";
            this.Invoke(new Action(() =>
            {
                ((GroupBox)wb_pointlog.Parent).Text = toget.A_Team + " VS " + toget.B_Team + "正在入球时间";

                string Key = toget.GameKey.Replace("m_", "");
                string Key5 = "";
                if (Key.Length > 5)
                {
                    Key5 = Key.Substring(0, 5);
                }
                NetFramework.Util_CEF.JoinQueueAndWait("http://www.gooooal.com/analysis/event/" + Key5 + "/event_cn_" + Key + ".html", wb_pointlog);

            }));


            Regex FindLogList = new Regex("<div id=\"div_logList\"[^>]*>((?<mm><div[^>]*>)+|(?<-mm></div>)|[\\s\\S])*?(?(mm)(?!))</div>", RegexOptions.IgnoreCase);
            Regex PointATeam = new Regex("<div id=\"block_l2\"[^>]*>((?<mm><div[^>]*>)+|(?<-mm></div>)|[\\s\\S])*?(?(mm)(?!))</div>", RegexOptions.IgnoreCase);
            Regex PointBTeam = new Regex("<div id=\"block_r2\"[^>]*>((?<mm><div[^>]*>)+|(?<-mm></div>)|[\\s\\S])*?(?(mm)(?!))</div>", RegexOptions.IgnoreCase);


            Regex PoinTimes = new Regex("<p tim=[0-9_]+\"[^>]*>((?<mm><p[^>]*>)+|(?<-mm></p>)|[\\s\\S])*?(?(mm)(?!))</p>", RegexOptions.IgnoreCase);



            string str_loglist = FindLogList.Match(html).Value;
            string str_loaga = PointATeam.Match(str_loglist).Value;
            string str_loagb = PointBTeam.Match(str_loglist).Value;

            MatchCollection pointsa = PoinTimes.Matches(str_loaga);
            MatchCollection pointsb = PoinTimes.Matches(str_loagb);

            SavePointLog(toget, pointsa, db);
            SavePointLog(toget, pointsb, db);


        }
        private void SavePointLog(Linq.Game_ResultFootBall_Last toget, MatchCollection PointTimes, Linq.dbDataContext db)
        {
            Regex PointMinute = new Regex("<em>\\[[0-9]+'\\]</em>", RegexOptions.IgnoreCase);
            Regex PointTimeID = new Regex("tim=[0-9_]+", RegexOptions.IgnoreCase);
            foreach (Match item in PointTimes)
            {
                string TimeValue = PointTimeID.Match(item.Value).Value;
                TimeValue = TimeValue.Replace("tim=", "");
                string Minute = PointMinute.Match(item.Value).Value;
                Linq.Game_ResultFootBallPointLog_Last lasttime = db.Game_ResultFootBallPointLog_Last.SingleOrDefault(t => t.aspnet_UserID == toget.aspnet_UserID
                    && t.GameKey == toget.GameKey
                    && t.PointIndex == TimeValue
                    );
                Minute = NetFramework.Util_WEB.CleanHtml(Minute);
                if (lasttime == null)
                {
                    Linq.Game_ResultFootBallPointLog_Last pl = new Linq.Game_ResultFootBallPointLog_Last();
                    pl.aspnet_UserID = toget.aspnet_UserID;
                    pl.GameKey = toget.GameKey;
                    pl.PointIndex = TimeValue;
                    pl.PointTime = Minute;
                    db.Game_ResultFootBallPointLog_Last.InsertOnSubmit(pl);
                    db.SubmitChanges();
                }


            }
        }
        private void ThreadRepeatCheckSend()
        {
            while (true)
            {
                if (IsRefreshBall == false)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                try
                {
                    Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                    //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                    ////db.ObjectTrackingEnabled = false;
                    DataRow[] rows = new DataRow[] { };
                    this.Invoke(new Action(() =>
                    {
                        rows = this.RunnerF.MemberSource.Select("User_IsBallPIC='True'");
                    }));
                    foreach (DataRow rowitem in rows)
                    {

                        string tmpid = rowitem.Field<object>("User_ContactTEMPID").ToString();
                        string SourceType = rowitem.Field<object>("User_SourceType").ToString();
                        string WX_USERNAME = rowitem.Field<object>("User_ContactID").ToString();


                        Linq.WX_WebSendPICSetting findset = db.WX_WebSendPICSetting.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                            && t.WX_UserName == WX_USERNAME
                            && t.WX_SourceType == SourceType
                            );
                        if (findset == null)
                        {
                            continue;
                        }
                        if (findset.LastBallSendTime == null ||
                            (DateTime.Now - findset.LastBallSendTime.Value).TotalMinutes > findset.ballinterval)
                        {

                            string[] Files = Directory.GetFiles(Application.StartupPath + "\\output");

                            //循环发联赛图
                            foreach (var item in Files)
                            {
                                FileInfo fi = new FileInfo(item);
                                if (fi.Name.Contains("联赛_足球") && findset.footballPIC == true && fi.Name.Contains("txt"))
                                {


                                    //SendRobotImage(item, MyUserName == FromUserNameTEMPID ? ToUserNameTEMPID : FromUserNameTEMPID, SourceType);
                                    SendRobotTxtFile(item, tmpid, SourceType);


                                    findset.LastBallSendTime = DateTime.Now;
                                    db.SubmitChanges();
                                }
                                if (fi.Name.Contains("联赛_篮球") && findset.bassketballpic == true && fi.Name.Contains("txt"))
                                {


                                    //SendRobotImage(item, MyUserName == FromUserNameTEMPID ? ToUserNameTEMPID : FromUserNameTEMPID, SourceType);
                                    SendRobotTxtFile(item, tmpid, SourceType);
                                    findset.LastBallSendTime = DateTime.Now;
                                    db.SubmitChanges();
                                }



                            }//文件循环
                            if (findset.balluclink == true)
                            {
                                //http://odds.gooooal.com/company.html?type=1001
                                //http://odds.gooooal.com/bkscompany.html?type=1001

                                SendRobotLink("雪缘园足球赛事查看", "http://odds.gooooal.com/company.html?type=1001", tmpid, SourceType);
                                SendRobotLink("雪缘园篮球赛事查看", "http://odds.gooooal.com/bkscompany.html?type=1001", tmpid, SourceType);
                                findset.LastBallSendTime = DateTime.Now;
                                db.SubmitChanges();
                            }

                        }

                        Application.DoEvents();

                    }



                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine(AnyError.Message, true);
                    NetFramework.Console.WriteLine(AnyError.StackTrace, true);

                }
                Thread.Sleep(5000);
            }

        }




        private void StartForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ExitKill = true;

            //CefSharp.Cef.Shutdown();
            Environment.Exit(0);
        }

        private void MI_BallOpenManul_Click(object sender, EventArgs e)
        {
            BallOpen bo = new BallOpen();
            bo.sf = this;
            bo.Show();
        }

        private void MI_BallGames_Click(object sender, EventArgs e)
        {
            BallGames bg = new BallGames();
            bg.Show();
        }

        public bool IsRefreshBall = false;


        public static void SetNextPreriodHKSix(string Period, DateTime NextTime, Linq.dbDataContext db)
        {
            var toupd = db.Game_TimeHKSix.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                && t.GamePeriod == Period
                );
            if (toupd == null)
            {
                Linq.Game_TimeHKSix newthk = new Linq.Game_TimeHKSix();
                newthk.aspnet_UserID = GlobalParam.UserKey;
                newthk.GamePeriod = Period;
                newthk.OpenTime = NextTime;
                db.Game_TimeHKSix.InsertOnSubmit(newthk);
                db.SubmitChanges();
            }
            else
            {
                return;
            }
        }
        public static Linq.Game_TimeHKSix GetNextPreriodHKSix(Linq.dbDataContext db)
        {
            var canbuys = db.Game_TimeHKSix.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                && t.OpenTime >= DateTime.Now.AddMinutes(2)
                ).OrderBy(t => t.OpenTime);
            if (canbuys.Count() > 0)
            {
                return canbuys.First();
            }
            else
            {
                return null;
            }
        }


        private void ThreadGetHKSix()
        {

            while (true)
            {
                try
                {
                    Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                    //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                    ////db.ObjectTrackingEnabled = false;
                    CookieCollection cookhk = new CookieCollection();
                    //https://1680660.com/smallSix/findSmallSixInfo.do
                    //只支持HTTPS
                    string Result = NetFramework.Util_WEB.OpenUrl("https://1680660.com/smallSix/findSmallSixInfo.do", "", "", "GET", cookhk, Encoding.GetEncoding("UTF-8"));
                    JObject j_result = JObject.Parse(Result);
                    string lastperiod = j_result["result"]["data"]["preDrawIssue"].ToString();
                    string Number = j_result["result"]["data"]["preDrawCode"].ToString();

                    SetNextPreriodHKSix(j_result["result"]["data"]["drawIssue"].ToString()
                      , Convert.ToDateTime(j_result["result"]["data"]["drawTime"].ToString()), db);

                    string[] Numbers = Number.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (Numbers.Length >= 7)
                    {


                        Linq.Game_ResultHKSix gr = db.Game_ResultHKSix.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                              && t.GamePeriod == lastperiod
                              );

                        Linq.Game_ResultHKSix processr = null;
                        if (gr == null)
                        {
                            Linq.Game_ResultHKSix newgr = new Linq.Game_ResultHKSix();
                            newgr.aspnet_UserID = GlobalParam.UserKey;
                            newgr.GamePeriod = lastperiod;
                            newgr.Num1 = Convert.ToInt32(Numbers[0]);
                            newgr.Num2 = Convert.ToInt32(Numbers[1]);
                            newgr.Num3 = Convert.ToInt32(Numbers[2]);
                            newgr.Num4 = Convert.ToInt32(Numbers[3]);
                            newgr.Num5 = Convert.ToInt32(Numbers[4]);
                            newgr.Num6 = Convert.ToInt32(Numbers[5]);
                            newgr.NumSpec = Convert.ToInt32(Numbers[6]);
                            newgr.GameTime = Convert.ToDateTime(j_result["result"]["data"]["preDrawTime"].ToString());
                            newgr.AnmialSpec = Linq.ProgramLogic.NumberToAnmial(newgr.NumSpec.Value);

                            newgr.Anmial1 = Linq.ProgramLogic.NumberToAnmial(newgr.Num1.Value);
                            newgr.Anmial2 = Linq.ProgramLogic.NumberToAnmial(newgr.Num2.Value);
                            newgr.Anmial3 = Linq.ProgramLogic.NumberToAnmial(newgr.Num3.Value);
                            newgr.Anmial4 = Linq.ProgramLogic.NumberToAnmial(newgr.Num4.Value);
                            newgr.Anmial5 = Linq.ProgramLogic.NumberToAnmial(newgr.Num5.Value);
                            newgr.Anmial6 = Linq.ProgramLogic.NumberToAnmial(newgr.Num6.Value);

                            db.Game_ResultHKSix.InsertOnSubmit(newgr);
                            db.SubmitChanges();
                            processr = newgr;

                            var users = RunnerF.MemberSource.Select("User_IsReply=1 ");
                            foreach (var item in users)
                            {

                                DataRow[] dr = RunnerF.MemberSource.Select("User_ContactTEMPID='" + item.Field<object>("User_ContactTEMPID").ToString() + "'");
                                if (dr.Length == 0)
                                {
                                    continue;
                                }
                                string TEMPUserName = dr[0].Field<string>("User_ContactTEMPID");
                                string SourceType = dr[0].Field<string>("User_SourceType");
                                string WX_UserName = dr[0].Field<string>("User_ContactID");


                                Linq.WX_WebSendPICSetting myset = db.WX_WebSendPICSetting.SingleOrDefault(t =>
                                    t.aspnet_UserID == GlobalParam.UserKey

                                    );
                                if (myset != null)
                                {
                                    if (myset.HKSixResult == true)
                                    {
                                        SendRobotContent(Linq.ProgramLogic.GetHKSixLast16(), TEMPUserName, SourceType);
                                    }
                                }
                            }
                        }
                        else
                        {
                            gr.GameTime = Convert.ToDateTime(j_result["result"]["data"]["preDrawTime"].ToString());

                            gr.AnmialSpec = Linq.ProgramLogic.NumberToAnmial(gr.NumSpec.Value);
                            gr.Anmial1 = Linq.ProgramLogic.NumberToAnmial(gr.Num1.Value);
                            gr.Anmial2 = Linq.ProgramLogic.NumberToAnmial(gr.Num2.Value);
                            gr.Anmial3 = Linq.ProgramLogic.NumberToAnmial(gr.Num3.Value);
                            gr.Anmial4 = Linq.ProgramLogic.NumberToAnmial(gr.Num4.Value);
                            gr.Anmial5 = Linq.ProgramLogic.NumberToAnmial(gr.Num5.Value);
                            gr.Anmial6 = Linq.ProgramLogic.NumberToAnmial(gr.Num6.Value);


                            db.SubmitChanges();
                            processr = gr;
                        }
                        var ToOpens = db.WX_UserGameLog_HKSix.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                            && t.HaveOpen == false
                             && t.GamePeriod == processr.GamePeriod
                            );
                        var ToOpenUsers = (from ds in ToOpens
                                           select new { ds.WX_UserName, ds.WX_SourceType }).Distinct();



                        foreach (var useritem in ToOpenUsers)
                        {

                            var opensubs = ToOpens.Where(t => t.WX_UserName == useritem.WX_UserName && t.WX_SourceType == useritem.WX_SourceType);

                            foreach (var perioditem in opensubs)
                            {
                                Linq.ProgramLogic.OpenHKSix(perioditem, db, processr);
                            }


                            decimal Reminder = Linq.ProgramLogic.WXUserChangeLog_GetRemainder(useritem.WX_UserName, useritem.WX_SourceType);

                            DataRow[] sendinfo = new DataRow[] { };
                            this.Invoke(new Action(() =>
                            {
                                sendinfo = RunnerF.MemberSource.Select("User_ContactID='" + useritem.WX_UserName.Replace("'", "''") + "'");
                                for (int i = 0; i < sendinfo.Length; i++)
                                {
                                    SendRobotContent((useritem.WX_SourceType == "PCQ" ? ("@" + useritem.WX_UserName + "##") : "") + "余" + Reminder.ToString("N0"), sendinfo[i].Field<string>("User_ContactTEMPID")
                                             , sendinfo[i].Field<string>("User_SourceType"));
                                }

                            }));

                        }

                    }//结果不是空白的

                    DownloadHKSixYear(db, DateTime.Today.Year);
                    DownloadHKSixYear(db, DateTime.Today.Year - 1);


                }
                catch (Exception anyerror)
                {
                    NetFramework.Console.WriteLine(anyerror.Message, true);
                    NetFramework.Console.WriteLine(anyerror.StackTrace, true);

                }




                Thread.Sleep(5000);
            }

        }


        private void DownloadHKSixYear(Linq.dbDataContext db, Int32 takeYear)
        {
            #region 下载全年历史
            CookieCollection coohkk = new CookieCollection();
            //https://1680660.com/smallSix/findSmallSixHistory.do
            string Result_year = NetFramework.Util_WEB.OpenUrl("http://1680660.com/smallSix/findSmallSixHistory.do", "https://6hch.com/", "year=" + takeYear.ToString() + "&type=1", "POST", coohkk, Encoding.UTF8, false, false, "application/x-www-form-urlencoded; charset=UTF-8");
            JObject j_result_year = JObject.Parse(Result_year);

            JArray prs = (JArray)j_result_year["result"]["data"]["bodyList"];
            foreach (var item in prs)
            {
                string perios = item["preDrawDate"].ToString().Substring(0, 4) + Convert.ToInt32(item["issue"].ToString()).ToString("000");

                string Number_year = item["preDrawCode"].ToString();
                string[] Numbers_year = Number_year.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (Number_year.Length >= 7)
                {


                    Linq.Game_ResultHKSix gr_year = db.Game_ResultHKSix.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                     && t.GamePeriod == perios
                     );

                    Linq.Game_ResultHKSix processr_year = null;
                    if (gr_year == null)
                    {
                        Linq.Game_ResultHKSix newgr = new Linq.Game_ResultHKSix();
                        newgr.aspnet_UserID = GlobalParam.UserKey;
                        newgr.GamePeriod = perios;
                        newgr.Num1 = Convert.ToInt32(Numbers_year[0]);
                        newgr.Num2 = Convert.ToInt32(Numbers_year[1]);
                        newgr.Num3 = Convert.ToInt32(Numbers_year[2]);
                        newgr.Num4 = Convert.ToInt32(Numbers_year[3]);
                        newgr.Num5 = Convert.ToInt32(Numbers_year[4]);
                        newgr.Num6 = Convert.ToInt32(Numbers_year[5]);
                        newgr.NumSpec = Convert.ToInt32(Numbers_year[6]);

                        newgr.AnmialSpec = Linq.ProgramLogic.NumberToAnmial(newgr.NumSpec.Value);
                        newgr.Anmial1 = Linq.ProgramLogic.NumberToAnmial(newgr.Num1.Value);
                        newgr.Anmial2 = Linq.ProgramLogic.NumberToAnmial(newgr.Num2.Value);
                        newgr.Anmial3 = Linq.ProgramLogic.NumberToAnmial(newgr.Num3.Value);
                        newgr.Anmial4 = Linq.ProgramLogic.NumberToAnmial(newgr.Num4.Value);
                        newgr.Anmial5 = Linq.ProgramLogic.NumberToAnmial(newgr.Num5.Value);
                        newgr.Anmial6 = Linq.ProgramLogic.NumberToAnmial(newgr.Num6.Value);


                        newgr.GameTime = Convert.ToDateTime(item["preDrawDate"].ToString());
                        db.SubmitChanges();
                        db.Game_ResultHKSix.InsertOnSubmit(newgr);
                        db.SubmitChanges();
                        processr_year = newgr;
                    }
                    else
                    {
                        gr_year.GameTime = Convert.ToDateTime(item["preDrawDate"].ToString());
                        gr_year.AnmialSpec = Linq.ProgramLogic.NumberToAnmial(gr_year.NumSpec.Value);
                        gr_year.Anmial1 = Linq.ProgramLogic.NumberToAnmial(gr_year.Num1.Value);
                        gr_year.Anmial2 = Linq.ProgramLogic.NumberToAnmial(gr_year.Num2.Value);
                        gr_year.Anmial3 = Linq.ProgramLogic.NumberToAnmial(gr_year.Num3.Value);
                        gr_year.Anmial4 = Linq.ProgramLogic.NumberToAnmial(gr_year.Num4.Value);
                        gr_year.Anmial5 = Linq.ProgramLogic.NumberToAnmial(gr_year.Num5.Value);
                        gr_year.Anmial6 = Linq.ProgramLogic.NumberToAnmial(gr_year.Num6.Value);

                        processr_year = gr_year;
                        db.SubmitChanges();
                    }
                    var ToOpens = db.WX_UserGameLog_HKSix.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                        && t.GamePeriod == processr_year.GamePeriod
                        && t.HaveOpen == false
                                 );
                    var ToOpenUsers = (from ds in ToOpens
                                       select new { ds.WX_UserName, ds.WX_SourceType }).Distinct();



                    foreach (var useritem in ToOpenUsers)
                    {

                        var opensubs = ToOpens.Where(t => t.WX_UserName == useritem.WX_UserName && t.WX_SourceType == useritem.WX_SourceType);

                        foreach (var perioditem in opensubs)
                        {
                            Linq.ProgramLogic.OpenHKSix(perioditem, db, processr_year);
                        }


                        decimal Reminder = Linq.ProgramLogic.WXUserChangeLog_GetRemainder(useritem.WX_UserName, useritem.WX_SourceType);

                        DataRow[] sendinfo = new DataRow[] { };
                        this.Invoke(new Action(() =>
                        {
                            sendinfo = RunnerF.MemberSource.Select("User_ContactID='" + useritem.WX_UserName.Replace("'", "''") + "'");
                            for (int i = 0; i < sendinfo.Length; i++)
                            {
                                SendRobotContent((useritem.WX_SourceType == "PCQ" ? ("@" + useritem.WX_UserName + "##") : "") + "余" + Reminder.ToString("N0"), sendinfo[i].Field<string>("User_ContactTEMPID")
                                         , sendinfo[i].Field<string>("User_SourceType"));
                            }

                        }));

                    }
                }
            }


            #endregion
        }
        private void MI_RatioHKSix_Click(object sender, EventArgs e)
        {

        }

        private void TopMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            Linq.aspnet_UsersNewGameResultSend loadset = Util_Services.GetServicesSetting();
            try
            {
                loadset.BlockStartHour = Convert.ToInt32(tb_StartHour.Text);
                loadset.BlockStartMinute = Convert.ToInt32(tb_StartMinute.Text);
                loadset.BlockEndHour = Convert.ToInt32(tb_EndHour.Text);
                loadset.BlockEndMinute = Convert.ToInt32(tb_EndMinute.Text);
                loadset.NoxPath = tb_NoxPath.Text;
                loadset.LeiDianPath = tb_LeidianPath.Text;
                loadset.AdbNoxMode = cb_adbnoxmode.Checked;
                loadset.AdbLeidianMode = cb_adbleidianmode.Checked;
                loadset.NoxSharePath = tb_noxsharepath.Text;
                loadset.LeiDianSharePath = tb_leidiansharepath.Text;
                loadset.TwoTreeNotSingle = cb_TwoTreeNotSingle.Checked;
                loadset.FuliRatio = Convert.ToDecimal(tb_fuliratio.Text);
                loadset.LiuShuiRatio = Convert.ToDecimal(tb_liushuiratio.Text);

                loadset.Thread_AoZhouCai = T_AoZhouCai.Checked;
                loadset.Thread_VRChongqing = T_VRChongQingShiShiCai.Checked;
                loadset.Thread_TengXunShiFen = T_TengXunShiFen.Checked;
                loadset.Thread_TengXunWuFen = T_TengXunWuFen.Checked;
                loadset.Thread_WuFen = T_WuFenCai.Checked;
                loadset.Thread_XinJiangShiShiCai = T_XinJiangShiShiCai.Checked;
                loadset.Thread_ChongQingShiShiCai = T_chongqingshishicai.Checked;

                loadset.Thread_TengXunShiFenXin = T_TengXunShiFenXin.Checked;
                loadset.Thread_TengXunWuFenXin = T_TengXunWuFenXin.Checked;
                loadset.Thread_HeNeiWuFen = T_HeNeiWuFen.Checked;


                Linq.Util_Services.SaveServicesSetting(loadset);

                if (sender != null)
                {
                    MessageBox.Show("保存成功");
                }

                //if (loadset.AdbNoxMode == true && loadset.NoxPath != "")
                //{
                //    CmdRun(loadset.NoxPath, "adb devices");
                //}
                //if (loadset.AdbLeidianMode == true && loadset.LeiDianPath != "")
                //{
                //    CmdRun(loadset.LeiDianPath, "adb devices");
                //}
                if (cb_adbnoxmode.Checked == true && tb_NoxPath.Text != "")
                {
                    string cmdr = CmdRun(tb_NoxPath.Text, "noxconsole.exe list");
                    ShowEnumtors(cmdr, SourceNoxList, "noxconsole.exe list");
                }
                if (cb_adbleidianmode.Checked == true && tb_leidiansharepath.Text != "")
                {
                    string cmdr = CmdRun(tb_LeidianPath.Text, "ldconsole.exe list2");
                    ShowEnumtors(cmdr, SourceLeidianList, "ldconsole.exe list2");
                }


            }
            catch (Exception anyerror)
            {

                NetFramework.Console.WriteLine(anyerror.Message, false);
                NetFramework.Console.WriteLine(anyerror.StackTrace, false);

                MessageBox.Show("保存封盘时段失败");

            }


        }

        private void btn_Redownload_Click(object sender, EventArgs e)
        {
            System.Threading.Tasks.Task task = new System.Threading.Tasks.Task(() =>
            {

                DownloadResult_Vr(true);
                DownloadResult_VrHK(true);
                DownloadResult_Aoz(true);
                DownloadResult_tengshi(true);
                DownloadResult_tengwu(true);
                DownloadResult_wufen(true);
                DownloadResult_xiangjiang(true);
                DownloadResult_chongqing(true);

                DownloadResult_tengshi_xin(true);
                DownloadResult_tengwu_xin(true);

                DownloadResult_HeNeiWuFen(true);


                MessageBox.Show("全下载已完成");
            });

            task.Start();
            MessageBox.Show("全下载已启动");


        }


        private void AdbRefreshUpPoint()
        {
            while (true)
            {
                foreach (var injitem in InjectWins.Where(t => t.RefreshBill == true))
                {
                    if (cb_adbnoxmode.Checked == true)
                    {
                        CmdRun(tb_NoxPath.Text, " ldconsole.exe adb --name 高三二班重庆彩  --command \"shell  input swipe\" ");
                    }
                    if (cb_adbleidianmode.Checked == true)
                    {
                        CmdRun(tb_LeidianPath.Text, " ldconsole.exe adb --name " + injitem.WX_UserName + "  --command \"shell  input swipe 181 246 181 510\" ");
                        CmdRun(tb_LeidianPath.Text, " ldconsole.exe adb --name " + injitem.WX_UserName + "  --command \" shell uiautomator dump /sdcard/bill.xml\" ");
                        CmdRun(tb_LeidianPath.Text, " ldconsole.exe adb --name " + injitem.WX_UserName + "  --command \" shell uiautomator dump /sdcard/bill.xml\" ");

                    }



                }
                Thread.Sleep(5000);
            }
        }

        private void btn_relogin_Click(object sender, EventArgs e)
        {
            //http://huy.vrbetapi.com//Account/LoginValidate?id=HUY&version=1.0&data=lVrOheZew4NAFwFkJmkk6HTItGzRs4S1jzGjGJy%2BSlwUkFsZeDyb%2BtJQGJrYcktDMBLvVjNEISyWd1fdO6gYGzEl1Ymwj%2FxCI4H5HwpZ3G3DJHjHvshxLWpiZdHdVzgK
            //https://huy.vrbetapi.com//Account/LoginValidate?id=HUY&version=1.0&data=lVrOheZew4NAFwFkJmkk6HTItGzRs4S1jzGjGJy%2BSlwUkFsZeDyb%2BtJQGJrYcktD8m9x3h2APc9WRr7jB2V6DwpxdVVlphmiDzNf%2B7ch2PPDJHjHvshxLWpiZdHdVzgK
            // wb_vrchongqing.Load("https://huy.vrbetapi.com//Account/LoginValidate?id=HUY&version=1.0&data=lVrOheZew4NAFwFkJmkk6HTItGzRs4S1jzGjGJy%2BSlwUkFsZeDyb%2BtJQGJrYcktD8m9x3h2APc9WRr7jB2V6DwpxdVVlphmiDzNf%2B7ch2PPDJHjHvshxLWpiZdHdVzgK");
            ReloadWebApp();
        }

        private void btn_installapk_Click(object sender, EventArgs e)
        {
            //System.Threading.Tasks.Task task = new System.Threading.Tasks.Task(() =>
            //{

            foreach (var item in InjectWins)
            {
                if (item.OpenResult == false)
                {
                    continue;
                }
                if (item.WX_SourceType == "雷电")
                {
                    CmdRun(tb_LeidianPath.Text, " ldconsole.exe adb --name " + item.WX_UserName + "  --command \"  shell ime set com.android.adbkeyboard/.AdbIME \" ");

                    CmdRun(tb_LeidianPath.Text, "dnconsole uninstallapp  --name " + item.WX_UserName + " --packagename com.android.adbkeyboard");
                    CmdRun(tb_LeidianPath.Text, "dnconsole installapp --name  " + item.WX_UserName + " --filename " + Application.StartupPath + "\\apk\\keyboardservice.apk");
                    //adb shell ime set com.android.adbkeyboard/.AdbIME  
                }
                else if (item.WX_SourceType == "夜神")
                {

                }
            }
            MessageBox.Show("安装已完成");
            //});

            //task.Start();
            //MessageBox.Show("安装已启动");
        }

        private void btn_active_Click(object sender, EventArgs e)
        {
            foreach (var item in InjectWins)
            {
                if (item.OpenResult == false)
                {
                    continue;
                }
                if (item.WX_SourceType == "雷电")
                {

                    //ldconsole.exe action --name 高三二班重庆彩 --key call.input --value 中国123
                    //ldconsole.exe adb --name 高三二班重庆彩  --command "shell input tap 332 616 "
                    //ldconsole.exe adb --name 高三二班重庆彩  --command " shell uiautomator dump /sdcard/1.xml"

                    // adb shell rm /sdcard/0.txt
                    //adb push D:\file.txt system/
                    // adb shell am broadcast -a ADB_INPUT_SF --es msg 0.txt
                    CmdRun(tb_LeidianPath.Text, " ldconsole.exe adb --name " + item.WX_UserName + "  --command \"  shell ime enable com.android.adbkeyboard/.AdbIME \" ");
                    CmdRun(tb_LeidianPath.Text, " ldconsole.exe adb --name " + item.WX_UserName + "  --command \"  shell ime set com.android.adbkeyboard/.AdbIME \" ");
                    //adb shell ime set com.android.adbkeyboard/.AdbIME  
                }
                else if (item.WX_SourceType == "夜神")
                {

                }
            }
            MessageBox.Show("安装已完成");
        }

        private void Btn_Restore_Click(object sender, EventArgs e)
        {
            foreach (var item in InjectWins)
            {
                if (item.OpenResult == false)
                {
                    continue;
                }
                if (item.WX_SourceType == "雷电")
                {

                    //ldconsole.exe action --name 高三二班重庆彩 --key call.input --value 中国123
                    //ldconsole.exe adb --name 高三二班重庆彩  --command "shell input tap 332 616 "
                    //ldconsole.exe adb --name 高三二班重庆彩  --command " shell uiautomator dump /sdcard/1.xml"

                    // adb shell rm /sdcard/0.txt
                    //adb push D:\file.txt system/
                    // adb shell am broadcast -a ADB_INPUT_SF --es msg 0.txt
                    CmdRun(tb_LeidianPath.Text, " ldconsole.exe adb --name " + item.WX_UserName + "  --command \"shell ime set  com.android.emu.inputservice/.InputService \" ");

                    //adb shell ime set com.android.adbkeyboard/.AdbIME  
                }
                else if (item.WX_SourceType == "夜神")
                {

                }
            }
            MessageBox.Show("安装已完成");

        }

        private void btn_query_Click(object sender, EventArgs e)
        {
            string R = "";
            if (cb_adbleidianmode.Checked == true)
            {

                //ldconsole.exe action --name 高三二班重庆彩 --key call.input --value 中国123
                //ldconsole.exe adb --name 高三二班重庆彩  --command "shell input tap 332 616 "
                //ldconsole.exe adb --name 高三二班重庆彩  --command " shell uiautomator dump /sdcard/1.xml"

                // adb shell rm /sdcard/0.txt
                //adb push D:\file.txt system/
                // adb shell am broadcast -a ADB_INPUT_SF --es msg 0.txt
                R = CmdRun(tb_LeidianPath.Text, "  adb devices ");

                //adb shell ime set com.android.adbkeyboard/.AdbIME  
            }
            else if (cb_adbnoxmode.Checked == true)
            {
                R = CmdRun(tb_LeidianPath.Text, "  Nox_adb devices ");
            }
            MessageBox.Show("模拟器在线状态：" + Environment.NewLine + R);
        }

        private void cb_refreshball_CheckedChanged(object sender, EventArgs e)
        {
            IsRefreshBall = cb_refreshball.Checked;
        }

        private void cb_robotstop_CheckedChanged(object sender, EventArgs e)
        {
            RobotStop = cb_robotstop.Checked;
        }
        bool FirstShow = true;
        private void StartForm_Shown(object sender, EventArgs e)
        {
            if (FirstShow)
            {
                Thread DownLoad163_VrHK = new Thread(new ThreadStart(DownLoad163ThreadDo_VrHK));
                DownLoad163_VrHK.SetApartmentState(ApartmentState.STA);
                DownLoad163_VrHK.Start();

                Thread DownLoad163_Vr = new Thread(new ThreadStart(DownLoad163ThreadDo_Vr));
                DownLoad163_Vr.SetApartmentState(ApartmentState.STA);
                DownLoad163_Vr.Start();


                Thread DownLoad163_Aoz = new Thread(new ThreadStart(DownLoad163ThreadDo_Aoz));
                DownLoad163_Aoz.SetApartmentState(ApartmentState.STA);
                DownLoad163_Aoz.Start();

                Thread DownLoad163_tengshi2 = new Thread(new ThreadStart(DownLoad163ThreadDo_tengshi_xin));
                DownLoad163_tengshi2.SetApartmentState(ApartmentState.STA);
                DownLoad163_tengshi2.Start();


                Thread DownLoad163_tengshi = new Thread(new ThreadStart(DownLoad163ThreadDo_tengshi));
                DownLoad163_tengshi.SetApartmentState(ApartmentState.STA);
                DownLoad163_tengshi.Start();

                Thread DownLoad163_tengwu = new Thread(new ThreadStart(DownLoad163ThreadDo_tengwu));
                DownLoad163_tengwu.SetApartmentState(ApartmentState.STA);
                DownLoad163_tengwu.Start();

                Thread DownLoad163_tengwu2 = new Thread(new ThreadStart(DownLoad163ThreadDo_tengwu_xin));
                DownLoad163_tengwu2.SetApartmentState(ApartmentState.STA);
                DownLoad163_tengwu2.Start();

                Thread DownLoad163_wufen = new Thread(new ThreadStart(DownLoad163ThreadDo_wufen));
                DownLoad163_wufen.SetApartmentState(ApartmentState.STA);
                DownLoad163_wufen.Start();

                Thread DownLoad163_xiangjiang = new Thread(new ThreadStart(DownLoad163ThreadDo_xiangjiang));
                DownLoad163_xiangjiang.SetApartmentState(ApartmentState.STA);
                DownLoad163_xiangjiang.Start();


                Thread DownLoad163_chongqing = new Thread(new ThreadStart(DownLoad163ThreadDo_chongqing));
                DownLoad163_chongqing.SetApartmentState(ApartmentState.STA);
                DownLoad163_chongqing.Start();

                Thread DownLoad163_HeNeiWuFen = new Thread(new ThreadStart(DownLoad163ThreadDo_HeNeiWuFen));
                DownLoad163_HeNeiWuFen.SetApartmentState(ApartmentState.STA);
                DownLoad163_HeNeiWuFen.Start();




                Thread CheckTimeSendThread = new Thread(new ThreadStart(CheckTimeSend));
                CheckTimeSendThread.SetApartmentState(ApartmentState.STA);
                CheckTimeSendThread.Start();


                ////db.ObjectTrackingEnabled = false;

                gv_NoxEnums.DataSource = SourceNoxList;
                gv_LeidianEnums.DataSource = SourceLeidianList;

                #region 任务管理器
                Process[] FindNoxList = Process.GetProcessesByName("nox");
                if (FindNoxList.Length > 0)
                {
                    tb_NoxPath.Text = FindNoxList[0].MainModule.FileName;
                    tb_NoxPath.Text = tb_NoxPath.Text.Substring(0, tb_NoxPath.Text.LastIndexOf("\\"));
                }

                Process[] FindLeiDianList = Process.GetProcessesByName("dnplayer");
                if (FindLeiDianList.Length > 0)
                {
                    tb_LeidianPath.Text = FindLeiDianList[0].MainModule.FileName;
                    tb_LeidianPath.Text = tb_LeidianPath.Text.Substring(0, tb_LeidianPath.Text.LastIndexOf("\\"));
                }
                #endregion


                LoadUserSetting();

                //if (loadset.AdbNoxMode == true && loadset.NoxPath != "")
                //{
                //    CmdRun(loadset.NoxPath, "adb.exe devices");
                //}
                //if (loadset.AdbLeidianMode == true && loadset.LeiDianPath != "")
                //{
                //    CmdRun(loadset.LeiDianPath, "adb.exe devices");
                //}

            }//第一次显示
            FirstShow = false;

        }
        public void LoadUserSetting()
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            Linq.aspnet_UsersNewGameResultSend loadset = Util_Services.GetServicesSetting();
            tb_StartHour.Text = loadset.BlockStartHour.HasValue ? loadset.BlockStartHour.Value.ToString() : "";
            tb_StartMinute.Text = loadset.BlockStartMinute.HasValue ? loadset.BlockStartMinute.Value.ToString() : "";
            tb_EndHour.Text = loadset.BlockEndHour.HasValue ? loadset.BlockEndHour.Value.ToString() : "";
            tb_EndMinute.Text = loadset.BlockEndMinute.HasValue ? loadset.BlockEndMinute.Value.ToString() : "";
            tb_NoxPath.Text = loadset.NoxPath;
            tb_LeidianPath.Text = loadset.LeiDianPath;
            tb_leidiansharepath.Text = loadset.LeiDianSharePath;
            tb_noxsharepath.Text = loadset.NoxSharePath;
            cb_adbnoxmode.Checked = loadset.AdbNoxMode.HasValue ? loadset.AdbNoxMode.Value : false;
            cb_adbleidianmode.Checked = loadset.AdbLeidianMode.HasValue ? loadset.AdbLeidianMode.Value : false;
            cb_TwoTreeNotSingle.Checked = loadset.TwoTreeNotSingle.HasValue ? loadset.TwoTreeNotSingle.Value : false;

            tb_liushuiratio.Text = loadset.LiuShuiRatio.HasValue ? loadset.LiuShuiRatio.Value.ToString("0.000") : "0.024";
            tb_fuliratio.Text = loadset.FuliRatio.HasValue ? loadset.FuliRatio.Value.ToString("0.000") : "0.02";


            T_AoZhouCai.Checked = loadset.Thread_AoZhouCai.HasValue ? loadset.Thread_AoZhouCai.Value : true;
            T_VRChongQingShiShiCai.Checked = loadset.Thread_VRChongqing.HasValue ? loadset.Thread_VRChongqing.Value : true;
            T_TengXunShiFen.Checked = loadset.Thread_TengXunShiFen.HasValue ? loadset.Thread_TengXunShiFen.Value : true;
            T_TengXunWuFen.Checked = loadset.Thread_TengXunWuFen.HasValue ? loadset.Thread_TengXunWuFen.Value : true;
            T_WuFenCai.Checked = loadset.Thread_WuFen.HasValue ? loadset.Thread_WuFen.Value : true;
            T_XinJiangShiShiCai.Checked = loadset.Thread_XinJiangShiShiCai.HasValue ? loadset.Thread_XinJiangShiShiCai.Value : true;
            T_chongqingshishicai.Checked = loadset.Thread_ChongQingShiShiCai.HasValue ? loadset.Thread_ChongQingShiShiCai.Value : true;

            T_TengXunShiFenXin.Checked = loadset.Thread_TengXunShiFenXin.HasValue ? loadset.Thread_TengXunShiFenXin.Value : true;
            T_TengXunWuFenXin.Checked = loadset.Thread_TengXunWuFenXin.HasValue ? loadset.Thread_TengXunWuFenXin.Value : true;

            T_HeNeiWuFen.Checked = loadset.Thread_HeNeiWuFen.HasValue ? loadset.Thread_HeNeiWuFen.Value : true;


            if (loadset.LeiDianPath == null)
            {
                loadset.LeiDianPath = "";
            }
            if (loadset.LeiDianSharePath == null)
            {
                loadset.LeiDianSharePath = "";
            } if (loadset.NoxPath == null)
            {
                loadset.NoxPath = "";
            } if (loadset.NoxSharePath == null)
            {
                loadset.NoxSharePath = "";
            }
            if (loadset.AdbNoxMode == null)
            {
                loadset.AdbNoxMode = false; ;
            }
            if (loadset.AdbLeidianMode == null)
            {
                loadset.AdbLeidianMode = false; ;
            }
            if (loadset.TwoTreeNotSingle == null)
            {
                loadset.TwoTreeNotSingle = false;
            }
            if (loadset.LiuShuiRatio == null)
            {
                loadset.LiuShuiRatio = 0.024M;
            }
            if (loadset.FuliRatio == null)
            {
                loadset.FuliRatio = 0.02M; ;
            }

            if (loadset.Thread_AoZhouCai == null)
            {
                loadset.Thread_AoZhouCai = true; ;
            }

            if (loadset.Thread_VRChongqing == null)
            {
                loadset.Thread_VRChongqing = true; ;
            }
            if (loadset.Thread_TengXunShiFen == null)
            {
                loadset.Thread_TengXunShiFen = true; ;
            }
            if (loadset.Thread_TengXunWuFen == null)
            {
                loadset.Thread_TengXunWuFen = true; ;
            }
            if (loadset.Thread_TengXunShiFenXin == null)
            {
                loadset.Thread_TengXunShiFenXin = true; ;
            }
            if (loadset.Thread_TengXunWuFenXin == null)
            {
                loadset.Thread_TengXunWuFenXin = true; ;
            }
            if (loadset.Thread_WuFen == null)
            {
                loadset.Thread_WuFen = true; ;
            }
            if (loadset.Thread_XinJiangShiShiCai == null)
            {
                loadset.Thread_XinJiangShiShiCai = true; ;
            }
            if (loadset.Thread_ChongQingShiShiCai == null)
            {
                loadset.Thread_ChongQingShiShiCai = true; ;
            }
            if (loadset.Thread_HeNeiWuFen == null)
            {
                loadset.Thread_HeNeiWuFen = true; ;
            }

            db.SubmitChanges();
        }







    }
}
