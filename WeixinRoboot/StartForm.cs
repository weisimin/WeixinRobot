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

namespace WeixinRoboot
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();


            RunnerF.StartF = this;
            RunnerF.Show();

            //NetFramework.WindowsApi.EnumWindows(new NetFramework.WindowsApi.CallBack(EnumWinsCallBack), 0);


            Thread DownLoad163 = new Thread(new ParameterizedThreadStart(DownLoad163ThreadDo));
            DownLoad163.ApartmentState = ApartmentState.STA;
            DownLoad163.Start(Download163ThreadID);

            Thread CheckTimeSendThread = new Thread(new ThreadStart(CheckTimeSend));
            CheckTimeSendThread.ApartmentState = ApartmentState.STA;
            CheckTimeSendThread.Start();


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

        public void SetMode(string Mode)
        {
            switch (Mode)
            {
                case "Admin":
                    break;
                case "User":
                    MI_UserSetting.Visible = false;
                    break;
                default:
                    break;
            }
        }


        private void Start_Load(object sender, EventArgs e)
        {
            tm_refresh.Start();
            Thread StartThread = new Thread(new ThreadStart(StartThreadDo));
            StartThread.Start();

            Thread StartThreadYixin = new Thread(new ThreadStart(StartThreadYixinDo));
            StartThreadYixin.Start();


            try
            {
                this.Text = "会员号:" + GlobalParam.UserName + "版本:" + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                RunnerF.Text = "会员号:" + GlobalParam.UserName + "版本:" + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch (Exception)
            {

                this.Text = "会员号:" + GlobalParam.UserName + "版本:";
                RunnerF.Text = "会员号:" + GlobalParam.UserName + "版本:";
            }


            Thread EndNoticeBoss = new Thread(new ThreadStart(RepeatSendBossReport));
            EndNoticeBoss.Start();

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
                        ResultID += GlobalParam.Key.ToByteArray()[i].ToString("0000");
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





        public string MyUserName = "";

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
                NetFramework.Console.Write(AnyError.Message);

                NetFramework.Console.Write(AnyError.StackTrace);
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

                NetFramework.Console.Write(AnyError.Message);

                NetFramework.Console.Write(AnyError.StackTrace);
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
                        ReStartWeixin();

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
        private void ReStartWeixin()
        {
            NetFramework.Console.WriteLine("微信重启" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            if (RestartTime_WeiXin != null && (DateTime.Now - RestartTime_WeiXin.Value).TotalMinutes <= 2 && RestartCount_WeiXin > 3)
            {
                MessageBox.Show("微信频繁重启，可能已掉线");
                WeiXinOnLine = false;
                return;
            }

            RestartCount_WeiXin += 1;
            RestartTime_WeiXin = DateTime.Now;
            JObject Members = WXInit();



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
                    Thread.Sleep(200);
                }

            }
            catch (Exception AnyError)
            {

                NetFramework.Console.Write(AnyError.Message);

                NetFramework.Console.Write(AnyError.StackTrace);
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
                NetFramework.Console.WriteLine("建立连接失败");
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
                MessageProcess(Messages[0]);
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
                        MessageProcess(item);
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
            while (true)
            {
                try
                {


                    #region "微信监听"




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


                    NetFramework.Console.WriteLine(GlobalParam.UserName + DateTime.Now.ToString());
                    NetFramework.Console.WriteLine(GlobalParam.UserName + Result3);

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

                        NetFramework.Console.WriteLine(cookie.Count == 0 ? "" : cookie[0].Expires.ToString());



                        ReStartWeixin();
                        return;
                    }
                    if (Check["retcode"].ToString() == "1100")
                    {
                        Thread.Sleep(1500);
                        ReStartWeixin();
                        return;

                    }
                    else if (
                     ((Result3.Contains("selector:\"7\"")) == true)
                         )
                    {


                        ReStartWeixin();
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
                        synckeys = (Newmsg["SyncKey"] as JObject);

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

                                MessageRobotDo("微", FromUserNameTEMPID, ToUserNameTEMPID, Content, msgTime, msgType, (FromUserNameTEMPID.StartsWith("@@")));

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
                    NetFramework.Console.Write(AnyError.Message);
                    NetFramework.Console.Write(AnyError.StackTrace);

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
                        MessageProcess(Messages[0]);
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
                                MessageProcess(item);
                            }//偶数的
                            ;
                        }

                    }//收到多消息
                }// while结束
                Thread.Sleep(500);
            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine("易信消息处理错误" + AnyError.Message);
                NetFramework.Console.WriteLine("易信消息处理错误" + AnyError.StackTrace);
            }



        }//函数结束

        Int32 NextSer = 0;
        private void MessageProcess(string Rawitem)
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


                        MyInfo = contactitem as JObject;


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
                        Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
                        db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

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
                Console.WriteLine("消息无法处理:" + Rawitem);

            }

        }


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
            string Content,
            string msgTime,
            string msgType,
            bool IsTalkGroup)
        {
            try
            {

                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
                db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

                #region 消息处理
                Linq.aspnet_UsersNewGameResultSend mysetting = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key);

                //string FromUserNameTEMPID = AddMsgList["FromUserName"].ToString();
                //string ToUserNameTEMPID = AddMsgList["ToUserName"].ToString();

                //string Content = AddMsgList["Content"].ToString();
                //string msgTime = AddMsgList["CreateTime"].ToString();
                //string msgType = AddMsgList["MsgType"].ToString();

                #region "转发"
                if (Content.Contains("上分") || Content.Contains("下分") || (msgType == "10000"))
                {

                    #region 转发设置

                    DataRow[] FromContacts = RunnerF.MemberSource.Select("User_ContactTEMPID='" + FromUserNameTEMPID + "'");
                    if (FromContacts.Length != 0)
                    {
                        string FromContactName = FromContacts[0].Field<string>("User_Contact");

                        var ReceiveTrans = db.WX_UserReply.Where(t => t.aspnet_UserID == GlobalParam.Key && t.IsReceiveTransfer == true);

                        foreach (var recitem in ReceiveTrans)
                        {
                            DataRow[] ToContact = RunnerF.MemberSource.Select("User_ContactID='" + recitem.WX_UserName + "' and User_SourceType='" + recitem.WX_SourceType + "'");
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
                    #region "如果是自己发出的"
                    if (Content == "加")
                    {
                        if (SourceType == "微")
                        {
                            RepeatGetMembers(Skey, pass_ticket);
                        }
                        else if (SourceType == "易")
                        {
                            RepeatGetMembersYiXin();
                        }
                    }
                    #region
                    if (FromUserNameTEMPID == MyUserName || (MyInfo != null && MyInfo["1"] != null && FromUserNameTEMPID == MyInfo["1"].ToString()))
                    {
                        #region "发图"
                        if (Content == ("图1") || (Content == ("图2")))
                        {
                            SendChongqingResult(Content, ToUserNameTEMPID);
                        }

                        #endregion


                        var tocontacts = RunnerF.MemberSource.Select("User_ContactTEMPID='" + ToUserNameTEMPID + "'");
                        if (tocontacts.Count() == 0)
                        {
                            return;
                        }



                        string MyOutResult = "";
                        try
                        {
                            //执行会员命令
                            MyOutResult = Linq.DataLogic.WX_UserReplyLog_MySendCreate(Content, tocontacts[0], JavaSecondTime(Convert.ToInt64(msgTime)));
                            string[] Splits = Content.Replace("，", ",").Replace("，", ",")
                                       .Replace(".", ",").Replace("。", ",").Replace("。", ",")
                                       .Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            if (Splits.Count() != 1 && Linq.DataLogic.IsOrderContent(Content))
                            {
                                DateTime Times = JavaSecondTime(Convert.ToInt64(msgTime));
                                foreach (var Splititem in Splits)
                                {
                                    Times.AddMilliseconds(10);
                                    String TmpMessage = Linq.DataLogic.WX_UserReplyLog_MySendCreate(Splititem, tocontacts[0], Times);

                                    if (TmpMessage != "")
                                    {
                                        MyOutResult = TmpMessage;
                                    }
                                }


                            }


                            //执行模拟下单,模拟下单内部切分
                            if (IsTalkGroup == false && tocontacts[0].Field<Boolean?>("User_IsReply") == true)
                            {

                                String TmpMessage = NewWXContent(JavaSecondTime(Convert.ToInt64(msgTime)), Content, tocontacts[0], "人工", true);
                                if (TmpMessage != "")
                                {
                                    MyOutResult = TmpMessage;
                                }

                            }
                            //全部执行玩才输出
                            if (MyOutResult != "")
                            {
                                SendRobotContent(MyOutResult, tocontacts[0].Field<string>("User_ContactTEMPID")
                                    , tocontacts[0].Field<string>("User_SourceType")
                                    );

                            }

                            if (Content.StartsWith("20"))
                            {
                                DealGameLogAndNotice();
                            }



                        }
                        catch (Exception mysenderror)
                        {

                            NetFramework.Console.Write(mysenderror.Message);
                            NetFramework.Console.Write(mysenderror.StackTrace);
                        }








                        //string MyOutResult = "";

                        //db.Logic_WX_UserReplyLog_MySendCreate(Content, tocontacts[0].Field<string>("User_ContactID"),ocontacts[0].Field<string>("User_SourceType"), GlobalParam.Key, JavaTime(Convert.ToInt64(msgTime)), ref MyOutResult);
                        //if (MyOutResult != "")
                        //{
                        //    SendWXContent(MyOutResult, tocontacts[0].Field<string>("User_ContactTEMPID"));

                        //}


                        return;
                    }
                    #endregion "自发自处理部分"

                    else
                    {
                        #region "发图"
                        if (Content == ("图1") || (Content == ("图2")))
                        {
                            SendChongqingResult(Content, FromUserNameTEMPID);
                        }

                        #endregion
                    }
                    #endregion

                    var contacts = RunnerF.MemberSource.Select("User_ContactTEMPID='" + FromUserNameTEMPID + "'");
                    if (contacts.Count() == 0)
                    {
                        NetFramework.Console.WriteLine("找不到联系人，消息无法处理" + FromUserNameTEMPID);
                        return;
                    }
                    DataRow userr = contacts.First();

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





                    #region "检查是否启用自动跟踪"

                    Linq.WX_UserReply checkreply = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == userr.Field<string>("User_ContactID") && t.WX_SourceType == userr.Field<string>("User_SourceType"));
                    if (checkreply.IsReply == true)
                    {
                        //群不下单
                        if (IsTalkGroup)
                        {
                            return;
                        }
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

                            NetFramework.Console.Write(mysenderror.Message);
                            NetFramework.Console.Write(mysenderror.StackTrace);
                        }
                        if (OutMessage != "")
                        {

                            SendRobotContent(OutMessage, userr.Field<string>("User_ContactTEMPID")
                                 , userr.Field<string>("User_SourceType")

                                );
                        }
                    }

                    #endregion
                }//内容非空白

                #endregion
            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine("消息处理异常" + AnyError.Message);
                NetFramework.Console.WriteLine("消息处理异常" + AnyError.StackTrace);
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

        JObject MyInfo = null;

        public string SendRobotContent(string Content, string TempToUserID, string WX_SourceType)
        {
            switch (WX_SourceType)
            {
                case "易":
                    return SendYiXinContent(Content, TempToUserID);
                    break;
                case "微":
                    return SendWXContent(Content, TempToUserID);
                    break;

                default:
                    return "";
                    break;
            }

        }
        public void SendRobotImage(string ImageFile, string TempToUserID, string WX_SourceType)
        {
            Thread SnedImageThread = new Thread(new ParameterizedThreadStart(ThreadSendRobotImage));
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
                        return;
                        break;
                }
            }
            catch (Exception AnyEror)
            {

                NetFramework.Console.WriteLine("图片发送失败," + AnyEror.Message);
                NetFramework.Console.WriteLine("图片发送失败," + AnyEror.StackTrace);
                return;
            }
        }


        public string SendWXContent(string Content, string TempToUserID)
        {
            Int32 TestCount = 1;
        ReDo:
            TestCount += 1;
            if (TestCount >= 3)
            {
                NetFramework.Console.WriteLine("文字发送失败" + Content);
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
                Msg.Add("FromUserName", MyUserName);
                Msg.Add("ToUserName", TempToUserID);
                string timespan = JavaTimeSpan();
                Msg.Add("LocalID", timespan);
                Msg.Add("ClientMsgId", JavaTimeSpan());

                body4.Add("Msg", Msg);

                string Result4 = NetFramework.Util_WEB.OpenUrl(CheckUrl4
                         , "https://" + webhost + "/", body4.ToString().Replace(Environment.NewLine, ""), "POST", cookie, Encoding.GetEncoding("UTF-8"), true);

                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
                db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

                Linq.WX_PicErrorLog log = new Linq.WX_PicErrorLog();
                log.aspnet_Userid = GlobalParam.Key;
                log.SendTime = DateTime.Now;
                log.UploadResult = Content;
                log.SendResult = Result4;
                log.WX_SourceType = "微";
                db.WX_PicErrorLog.InsertOnSubmit(log);
                db.SubmitChanges();

                if (Result4.Contains("\"Ret\": 0,") == false)
                {

                    if (TestCount >= 3)
                    {
                        NetFramework.Console.WriteLine("文字发送失败" + Content);
                        return Result4;
                    }
                    else
                    {
                        NetFramework.Console.WriteLine("文字发送失败" + Content);
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
                NetFramework.Console.WriteLine("图片发送失败");
                return "";
            }
            Thread.Sleep(500);
            try
            {
                string UpLoadResult2 = NetFramework.Util_WEB.UploadWXImage(ImageFile, MyUserName, TEMPUserName, JavaTimeSpan(), cookie, j_BaseRequest, webhost);
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
                string MediaID2 = returnupload2["MediaId"].ToString();
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
                Msg2.Add("FromUserName", MyUserName);
                Msg2.Add("ToUserName", TEMPUserName);

                String Time2 = JavaTimeSpan();

                Msg2.Add("LocalID", Time2);
                Msg2.Add("ClientMsgId", Time2);

                body2.Add("Msg", Msg2);
                body2.Add("Scene", 0);

                NetFramework.Console.WriteLine("正在发图" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"));

                string Result2 = NetFramework.Util_WEB.OpenUrl(CheckUrl2
                  , "https://" + webhost + "/", body2.ToString().Replace(Environment.NewLine, ""), "POST", cookie, Encoding.UTF8, false);


                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
                db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

                Linq.WX_PicErrorLog log = new Linq.WX_PicErrorLog();
                log.aspnet_Userid = GlobalParam.Key;
                log.SendTime = DateTime.Now;
                log.UploadResult = UpLoadResult2;
                log.SendResult = Result2;
                log.WX_SourceType = "微";
                db.WX_PicErrorLog.InsertOnSubmit(log);
                db.SubmitChanges();


                NetFramework.Console.WriteLine("发送结果" + Result2 + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"));

                if (Result2.Contains("\"Ret\": 0,") == false)
                {

                    if (TestCount >= 3)
                    {
                        NetFramework.Console.WriteLine("图片发送失败");
                        return Result2;
                    }
                    else
                    {
                        NetFramework.Console.WriteLine("重试图片发送");
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
                string FirstSendBody = "3:::{\"SID\":96,\"CID\":4,\"SER\":" + YixinSer.ToString() + ",\"Q\":[{\"t\":\"long\",\"v\":\"" + MyInfo["1"].ToString() + "\"},{\"t\":\"byte\",\"v\":1}]}";
                YixinSer += 1;
                string WSResultRepeat = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im:9092/socket.io/1/" + strprefix + "/" + strfindurid + "?t=" + JavaTimeSpan()
                   , "https://web.yixin.im", FirstSendBody, "POST", cookieyixin, System.Text.Encoding.UTF8, true, true);
                // WR_repeat = JObject.Parse(WSResultRepeat);


            }



            //3:::{"SID":96,"CID":1,"SER":2,"Q":[{"t":"property","v":{"1":"168367856","2":"168324356","3":"发个消息给我","4":"1533115796.337","5":"0","6":"903a263c8e5dd101474290243a289a9e"}}]}

            string ContactType = "";
            DataRow UserRoow = RunnerF.MemberSource.Select("User_ContactID='" + TempToUserID + "' AND User_SourceType='易'")[0];
            ContactType = UserRoow["User_ContactType"].ToString();

            JObject BodyJson = null;
            if (ContactType == "个人")
            {
                BodyJson = JObject.Parse("{\"SID\":96,\"CID\":1,\"SER\":" + YixinSer.ToString() + ",\"Q\":[{\"t\":\"property\",\"v\":{\"1\":\"168367856\",\"2\":\"168324356\",\"3\":\"发个消息给我\",\"4\":\"1533115796.337\",\"5\":\"0\",\"6\":\"903a263c8e5dd101474290243a289a9e\"}}]}");
                BodyJson["SER"] = YixinSer.ToString();
                BodyJson["Q"][0]["v"]["1"] = TempToUserID;
                BodyJson["Q"][0]["v"]["2"] = MyInfo["1"].ToString();
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
                BodyJson["Q"][1]["v"]["2"] = MyInfo["1"].ToString();
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
            string Uploadresult = NetFramework.Util_WEB.UploadYixinImage(ImageFile, cookieyixin, MyInfo["1"].ToString(), MyUploadId);
            // {"result":[{"bucket":"yixinpublic","object":"pr_paqpkc5hwiekxvz5n3dbhg==_1533169024_30997221","etag":"3f28b61cad5b2f0807c647e7df401f34","fileName":"simple.png","fileSize":425136,"uploadCode":0}],"code":"200"}

            if (Uploadresult.Contains("code\":\"200\"") == false)
            {
                NetFramework.Console.WriteLine("图片上传失败");
                NetFramework.Console.WriteLine(Uploadresult);
                return Uploadresult;
            }
            else
            {



                if (YixinSer != NextSer - 1)
                {
                    string FirstSendBody = "3:::{\"SID\":96,\"CID\":4,\"SER\":" + YixinSer.ToString() + ",\"Q\":[{\"t\":\"long\",\"v\":\"" + MyInfo["1"].ToString() + "\"},{\"t\":\"byte\",\"v\":1}]}";
                    YixinSer += 1;
                    string WSResultRepeat = NetFramework.Util_WEB.OpenUrl("https://web.yixin.im:9092/socket.io/1/" + strprefix + "/" + strfindurid + "?t=" + JavaTimeSpan()
                       , "https://web.yixin.im", FirstSendBody, "POST", cookieyixin, System.Text.Encoding.UTF8, true, true);
                    // WR_repeat = JObject.Parse(WSResultRepeat);


                }

                if (Uploadresult == "")
                {
                    NetFramework.Console.WriteLine("图片发送失败，服务器超时");
                    return "";
                }
                JObject JUploadResult = JObject.Parse(Uploadresult);

                string ContactType = "";
                DataRow UserRoow = RunnerF.MemberSource.Select("User_ContactID='" + TEMPUserName + "' AND User_SourceType='易'")[0];
                ContactType = UserRoow["User_ContactType"].ToString();
                JObject BodyJson = null;
                if (ContactType == "个人")
                {


                    BodyJson = JObject.Parse("{\"SID\":96,\"CID\":1,\"SER\":" + YixinSer.ToString() + ",\"Q\":[{\"t\":\"property\",\"v\":{\"1\":\"168367856\",\"2\":\"168324356\",\"3\":\"图片\",\"4\":\"1533116272.971\",\"5\":\"1\",\"6\":\"07cc609372652b4b10d11b963c977897\",\"51\":\"ec6a2cd8d5a4f885945620450f2b9df4\",\"53\":\"http://nos-yx.netease.com/yixinpublic/pr_fj7cturlagjpcf_hyq8q9q==_1533116270_19288624\",\"56\":\"image/jpeg\",\"58\":\"0\"}}]}");
                    BodyJson["SER"] = YixinSer.ToString();
                    BodyJson["Q"][0]["v"]["1"] = TEMPUserName;
                    BodyJson["Q"][0]["v"]["2"] = MyInfo["1"].ToString();
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
                    BodyJson["Q"][1]["v"]["2"] = MyInfo["1"].ToString();
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

                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
                db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

                Linq.WX_PicErrorLog log = new Linq.WX_PicErrorLog();
                log.aspnet_Userid = GlobalParam.Key;
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

        public bool WeiXinOnLine { get { return _WeiXinOnLine; } set { _WeiXinOnLine = value; MI_GameLogManulDeal.Enabled = _YiXinOnline || _WeiXinOnLine; MI_Bouns_Manul.Enabled = _YiXinOnline || _WeiXinOnLine; } }
        public bool YiXinOnline { get { return _YiXinOnline; } set { _YiXinOnline = value; MI_GameLogManulDeal.Enabled = _YiXinOnline || _WeiXinOnLine; ; MI_Bouns_Manul.Enabled = _YiXinOnline || _WeiXinOnLine; } }



        public void DealGameLogAndNotice(bool IgoreDataSettingSend = false)
        {
            NetFramework.Console.WriteLine("正在开奖" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            Linq.aspnet_UsersNewGameResultSend checkus = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key);

            if ((checkus != null && checkus.IsNewSend == true) || (IgoreDataSettingSend == true))
            {


                #region "发送余额"
                var noticeChangelist = db.WX_UserGameLog.Where(t => t.Result_HaveProcess == false
                    && t.aspnet_UserID == GlobalParam.Key
                    && ((_WeiXinOnLine == true && t.WX_SourceType == "微")
                      || (_YiXinOnline == true && t.WX_SourceType == "易"))

                    ).Select(t => new { t.WX_UserName, t.WX_SourceType }).Distinct();
                foreach (var notice_item in noticeChangelist)
                {

                    Int32 TotalChanges = Linq.DataLogic.WX_UserGameLog_Deal(this, notice_item.WX_UserName, notice_item.WX_SourceType);
                    db.SubmitChanges();
                    if (TotalChanges == 0)
                    {
                        continue;
                    }

                    decimal? ReminderMoney = Linq.DataLogic.WXUserChangeLog_GetRemainder(notice_item.WX_UserName, notice_item.WX_SourceType);

                    var Rows = RunnerF.MemberSource.Select("User_ContactID='" + notice_item.WX_UserName + "' and User_SourceType='" + notice_item.WX_SourceType + "'");
                    if (Rows.Count() < 1)
                    {
                        NetFramework.Console.WriteLine("找不到联系人，发不出");
                        return;
                    }
                    string TEMPUserName = Rows.First().Field<string>("User_ContactTEMPID");
                    string SourceType = Rows.First().Field<string>("User_SourceType");

                    String ContentResult = SendRobotContent("余" + (ReminderMoney.HasValue ? ReminderMoney.Value.ToString("N0") : ""), TEMPUserName, SourceType);
                    var updatechangelog = db.WX_UserChangeLog.Where(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == notice_item.WX_UserName && t.WX_SourceType == notice_item.WX_SourceType && t.NeedNotice == false);
                    db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, updatechangelog);
                    foreach (var updatechangeitem in updatechangelog)
                    {
                        updatechangeitem.HaveNotice = true;
                    }
                    db.SubmitChanges();

                }


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
            }
            NetFramework.Console.WriteLine("开奖完成" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
        }

        public static object Proccesing = false;
        public string NewWXContent(DateTime ReceiveTime, string ReceiveContent, DataRow userr, string SourceType, bool adminmode = false)
        {
            lock (Proccesing)
            {
                Proccesing = !(bool)Proccesing;
                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
                db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

                Linq.WX_UserReplyLog log = db.WX_UserReplyLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key
                                              && t.WX_UserName == userr.Field<string>("User_ContactID")
                                              && t.WX_SourceType == userr.Field<string>("User_SourceType")
                                              && t.ReceiveTime == ReceiveTime
                                              && t.SourceType == SourceType

                                              );
                if (log == null)
                {
                    Linq.WX_UserReplyLog newlogr = new Linq.WX_UserReplyLog();
                    newlogr.aspnet_UserID = GlobalParam.Key;
                    newlogr.WX_UserName = userr.Field<string>("User_ContactID");
                    newlogr.WX_SourceType = userr.Field<string>("User_SourceType");
                    newlogr.ReceiveContent = ReceiveContent;
                    newlogr.ReceiveTime = ReceiveTime;
                    newlogr.SourceType = SourceType;

                    newlogr.ReplyContent = "";
                    newlogr.HaveDeal = false;
                    db.WX_UserReplyLog.InsertOnSubmit(newlogr);
                    db.SubmitChanges();

                    #region "老板查询"
                    if (ReceiveContent.Length == 8 || ReceiveContent.Length == 17)
                    {
                        try
                        {
                            Linq.WX_UserReply testu = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key && t.WX_SourceType == newlogr.WX_SourceType && t.WX_UserName == newlogr.WX_UserName);
                            if (testu.IsBoss == true)
                            {
                                NetFramework.Console.WriteLine("准备老板查询发图");
                                DataTable Result2 = WeixinRoboot.Linq.DataLogic.GetBossReportSource(newlogr.WX_SourceType, ReceiveContent);
                                DrawDataTable(Result2);

                                //SendRobotImage(Application.StartupPath + "\\Data" + GlobalParam.UserName + "老板查询.jpg", userr.Field<string>("User_ContactTEMPID"), userr.Field<string>("User_SourceType"));
                                Thread st = new Thread(new ParameterizedThreadStart(ThreadSendRobotImage));
                                st.Start(new object[] { Application.StartupPath + "\\Data" + GlobalParam.UserName + "老板查询.jpg", userr.Field<string>("User_ContactTEMPID"), userr.Field<string>("User_SourceType") });


                                NetFramework.Console.WriteLine("准备老板查询发图完毕");
                                Linq.PIC_EndSendLog bsl = new Linq.PIC_EndSendLog();
                                bsl.WX_BossID = newlogr.WX_UserName;
                                bsl.WX_SourceType = newlogr.WX_SourceType;
                                bsl.WX_SendDate = DateTime.Now;
                                bsl.WX_UserName = newlogr.WX_UserName;
                                bsl.aspnet_UserID = GlobalParam.Key;
                                db.PIC_EndSendLog.InsertOnSubmit(bsl);


                                Linq.PIC_EndSendLog findbsl = db.PIC_EndSendLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key
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
                            NetFramework.Console.WriteLine(AnyError.StackTrace);

                        }

                    }
                    #endregion


                    string ReturnSend = Linq.DataLogic.WX_UserReplyLog_Create(newlogr, userr.Table, adminmode);

                    string[] Splits = newlogr.ReceiveContent.Replace("，", ",").Replace("，", ",")
                                                            .Replace(".", ",").Replace("。", ",").Replace("。", ",")
                        .Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (Splits.Count() != 1 && Linq.DataLogic.IsOrderContent(newlogr.ReceiveContent))
                    {
                        DateTime Times = ReceiveTime;
                        Int32 Count = 0;
                        foreach (var Splititem in Splits)
                        {
                            Count += 1;
                            Times = Times.AddMilliseconds(10);
                            Linq.WX_UserReplyLog newlogr_split = new Linq.WX_UserReplyLog();
                            newlogr_split.aspnet_UserID = GlobalParam.Key;
                            newlogr_split.WX_UserName = userr.Field<string>("User_ContactID");
                            newlogr_split.WX_SourceType = userr.Field<string>("User_SourceType");
                            newlogr_split.ReceiveContent = Splititem;
                            newlogr_split.ReceiveTime = Times;
                            newlogr_split.SourceType = "人工" + Count.ToString();

                            newlogr_split.ReplyContent = "";
                            newlogr_split.HaveDeal = false;
                            db.WX_UserReplyLog.InsertOnSubmit(newlogr_split);
                            db.SubmitChanges();
                            String TmpMessage = Linq.DataLogic.WX_UserReplyLog_Create(newlogr_split, userr.Table, adminmode);

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

                        Console.WriteLine(AnyError2.Message);
                        Console.WriteLine(AnyError2.StackTrace);
                    }



                    return ReturnSend;

                }
                else
                {
                    return log.ReplyContent;
                    NetFramework.Console.WriteLine("下单记录已存在");
                }
            }
            //string Message = "";
            //Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            //db.Logic_WX_UserReplyLog_Create(ReceiveContent, userr.Field<string>("User_ContactID"), userr.Field<string>("User_SourceType"), GlobalParam.Key, ReceiveTime, ref Message, SourceType);

            //return Message;

        }//新消息


        private JObject WXInit()
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


                NetFramework.Console.Write(Result);
                string Redirect = Result.Substring(Result.IndexOf("redirect_uri"));
                Redirect = Redirect.Substring(Redirect.IndexOf("\"") + 1);
                Redirect = Redirect.Substring(0, Redirect.Length - 2);
                string CheckUrl2 = Redirect;

                webhost = CheckUrl2.Substring(CheckUrl2.IndexOf("//") + 2);
                webhost = webhost.Substring(0, webhost.IndexOf("/"));

                //CheckUrl2 = CheckUrl2.Replace(webhost, "wechat.qq.com");
                //webhost = "wechat.qq.com";

                string Result2 = NetFramework.Util_WEB.OpenUrl(CheckUrl2 + "&fun=new&version=v2"
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
                return RepeatGetMembers(Skey, pass_ticket);
            }
            catch (Exception AnyError)
            {
                NetFramework.Console.Write(AnyError.Message);
                NetFramework.Console.Write(AnyError.StackTrace);
                goto retry;
            }
        }

        Int32 GetMembersCount = 1;

        private JObject RepeatGetMembers(string Skey, string pass_ticket)
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



            MyUserName = InitResponse["User"]["UserName"].ToString();

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


            RunnerF.MembersSet(Members);
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
                MessageProcess(Messages[0]);
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
                        MessageProcess(item);
                    }//偶数的
                    ;
                }

            }//收到多消息

        }



        Dictionary<Guid, Boolean> KillThread = new Dictionary<Guid, bool>();


        Guid Download163ThreadID = Guid.NewGuid();
        private void DownLoad163ThreadDo(object ThreadID)
        {
            while (KillThread.ContainsKey((Guid)ThreadID) == false)
            {
                try
                {
                    DownloadResult();
                    System.Threading.Thread.Sleep(2000);
                }
                catch (Exception)
                {


                    System.Threading.Thread.Sleep(2000);
                }


            }
        }
        private void DownloadResult()
        {
            Boolean SendImage = false;
            Boolean SendImage2 = false;
            Boolean SendImage3 = false;
            try
            {


                try
                {


                    DownLoad163CaiPiaoV_1395p(ref SendImage, DateTime.Now, false);


                }
                catch (Exception AnyError)
                { NetFramework.Console.Write(AnyError.Message); NetFramework.Console.Write(AnyError.StackTrace); }


                //try
                //{
                //    Boolean SendImage = false;

                //    DownLoad163CaiPiaoV_13322(ref SendImage, DateTime.Now, false);
                //    if (SendImage == true)
                //    {
                //        DealGameLogAndNotice();
                //        SendChongqingResult();
                //    }

                //}
                //catch (Exception AnyError)
                //{ NetFramework.Console.Write(AnyError.Message); NetFramework.Console.Write(AnyError.StackTrace); }


                //try
                //{
                //    Boolean SendImage = false;
                //    DownLoad163CaiPiaoV_13322(ref SendImage, DateTime.Now.AddDays(-1), false);
                //    if (SendImage == true)
                //    {
                //        DealGameLogAndNotice();
                //        SendChongqingResult();
                //    }

                //}
                //catch (Exception AnyError)
                //{ NetFramework.Console.Write(AnyError.Message); NetFramework.Console.Write(AnyError.StackTrace); }



                //try
                //{
                //    Boolean SendImage = false;
                //    DownLoad163CaiPiaoV_cp222789(ref SendImage, DateTime.Now, false);
                //    if (SendImage == true)
                //    {
                //        DealGameLogAndNotice();
                //        SendChongqingResult();
                //    }

                //}
                //catch (Exception AnyError)
                //{ NetFramework.Console.Write(AnyError.Message); NetFramework.Console.Write(AnyError.StackTrace); }


                //try
                //{
                //    Boolean SendImage = false;
                //    DownLoad163CaiPiaoV_cp222789(ref SendImage, DateTime.Now.AddDays(-1), false);
                //    if (SendImage == true)
                //    {
                //        DealGameLogAndNotice();
                //        SendChongqingResult();
                //    }

                //}
                //catch (Exception AnyError)
                //{ NetFramework.Console.Write(AnyError.Message); NetFramework.Console.Write(AnyError.StackTrace); }


                try
                {

                    DownLoad163CaiPiaoV_kaijiangwang(ref SendImage2, DateTime.Now, false);


                }
                catch (Exception AnyError)
                { NetFramework.Console.Write(AnyError.Message); NetFramework.Console.Write(AnyError.StackTrace); }

                try
                {

                    DownLoad163CaiPiaoV_kaijiangwang(ref SendImage3, DateTime.Now.AddDays(-1), false);


                }
                catch (Exception AnyError)
                { NetFramework.Console.Write(AnyError.Message); NetFramework.Console.Write(AnyError.StackTrace); }

                //try
                //{
                //    Boolean SendImage = false;
                //    DownLoad163CaiPiaoV_Taohua(ref SendImage, DateTime.Now, false);
                //    if (SendImage == true)
                //    {
                //        DealGameLogAndNotice();
                //        SendChongqingResult();
                //    }

                //}
                //catch (Exception AnyError)
                //{ NetFramework.Console.Write(AnyError.Message); NetFramework.Console.Write(AnyError.StackTrace); }
                //try
                //{
                //    Boolean SendImage = false;
                //    DownLoad163CaiPiaoV_Taohua(ref SendImage, DateTime.Now.AddDays(-1), false);
                //    if (SendImage == true)
                //    {
                //        DealGameLogAndNotice();
                //        SendChongqingResult();
                //    }

                //}
                //catch (Exception AnyError) { NetFramework.Console.Write(AnyError.Message); NetFramework.Console.Write(AnyError.StackTrace); }

                //try
                //{
                //    Boolean SendImage = false;
                //    DownLoad163CaiPiaoV_500(ref SendImage, DateTime.Now, false);
                //    if (SendImage == true)
                //    {
                //        SendChongqingResult();
                //    }
                //    DealGameLogAndNotice();
                //}
                //catch (Exception AnyError) { }
                //try
                //{
                //    Boolean SendImage = false;
                //    DownLoad163CaiPiaoV_500(ref SendImage, DateTime.Now.AddDays(-1), false);
                //    if (SendImage == true)
                //    {
                //        SendChongqingResult();
                //    }
                //    DealGameLogAndNotice();
                //}
                //catch (Exception AnyError) { }
                //try
                //{
                //    Boolean SendImage = false;
                //    DownLoad163CaiPiaoV_zhcw(ref SendImage, DateTime.Now, false);
                //    if (SendImage == true)
                //    {
                //        SendChongqingResult();
                //    }
                //    DealGameLogAndNotice();
                //}
                //catch (Exception AnyError)
                //{
                //    //NetFramework.Console.Write(AnyError.Message);
                //    //NetFramework.Console.Write(AnyError.StackTrace); 
                //}

                //try
                //{
                //    Boolean SendImage = false;
                //    DownLoad163CaiPiaoV_zhcw(ref SendImage, DateTime.Now.AddDays(-1), false);
                //    if (SendImage == true)
                //    {
                //        SendChongqingResult();
                //    }
                //    DealGameLogAndNotice();
                //}
                //catch (Exception AnyError) { }

                //try
                //{
                //    Boolean SendImage = false;
                //    DownLoad163CaiPiao_V163(ref SendImage, DateTime.Now, false);
                //    if (SendImage == true)
                //    {
                //        SendChongqingResult();
                //    }
                //    DealGameLogAndNotice();
                //}
                //catch (Exception AnyError) { NetFramework.Console.Write(AnyError.Message); NetFramework.Console.Write(AnyError.StackTrace); }
                //try
                //{
                //    Boolean SendImage = false;
                //    DownLoad163CaiPiao_V163(ref SendImage, DateTime.Now.AddDays(-1), false);
                //    if (SendImage == true)
                //    {
                //        SendChongqingResult();
                //    }
                //    DealGameLogAndNotice();
                //}
                //catch (Exception AnyError)
                //{
                //    NetFramework.Console.Write(AnyError.Message); NetFramework.Console.Write(AnyError.StackTrace);

                //}


                if (SendImage2 || SendImage || SendImage3 == true)
                {
                    DealGameLogAndNotice();
                    SendChongqingResult();
                }

            }
            catch (Exception AnyError)
            {
                NetFramework.Console.Write(AnyError.Message);
                NetFramework.Console.Write(AnyError.StackTrace);
            }

        }

        public void SendChongqingResult(string Mode = "All", string ToUserID = "")
        {
            NetFramework.Console.Write(GlobalParam.UserName + "开始发送图片" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);


            #region
            SendPicEnumWins();

            #endregion

            #region "有新的就通知,以及处理结果"

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            Linq.aspnet_UsersNewGameResultSend myconfig = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key);
            if (
                (DateTime.Now.Hour >= myconfig.SendImageStart && DateTime.Now.Hour <= myconfig.SendImageEnd)
                || (DateTime.Now.Hour >= myconfig.SendImageStart2 && DateTime.Now.Hour <= myconfig.SendImageEnd2)
                || (DateTime.Now.Hour >= myconfig.SendImageStart3 && DateTime.Now.Hour <= myconfig.SendImageEnd3)
                || (DateTime.Now.Hour >= myconfig.SendImageStart4 && DateTime.Now.Hour <= myconfig.SendImageEnd4)
                || (ToUserID != "")

                )
            {

                NetFramework.Console.Write("正在发图" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

                // var users = db.WX_UserReply.Where(t => t.IsReply == true && t.aspnet_UserID == GlobalParam.Key);
                var users = RunnerF.MemberSource.Select("User_IsReply=1 ");
                foreach (var item in users)
                {
                    #region  多人同号不到ID跳过
                    #endregion
                    DataRow[] dr = RunnerF.MemberSource.Select("User_ContactTEMPID='" + item.Field<object>("User_ContactTEMPID").ToString() + "'");
                    if (dr.Length == 0)
                    {
                        continue;
                    }
                    string TEMPUserName = dr[0].Field<string>("User_ContactTEMPID");
                    string SourceType = dr[0].Field<string>("User_SourceType");
                    Linq.aspnet_UsersNewGameResultSend myset = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key);
                    if (!myset.IsSendPIC == true)
                    {
                        continue;
                    }
                    if ((dr[0].Field<string>("User_ContactType") == "群" && ToUserID == "") || (TEMPUserName == ToUserID))
                    {
                        if (System.IO.File.Exists(Application.StartupPath + "\\Data3" + GlobalParam.UserName + ".txt"))
                        {
                            if (dr[0].Field<string>("User_SourceType") == "微")
                            {
                                FileStream fs = new FileStream(Application.StartupPath + "\\Data3" + GlobalParam.UserName + ".txt", System.IO.FileMode.Open);
                                byte[] bs = new byte[fs.Length];
                                fs.Read(bs, 0, bs.Length);
                                fs.Close();
                                fs.Dispose();
                                if (Mode == "All" || (Mode == "图2"))
                                {
                                    SendRobotContent(Encoding.UTF8.GetString(bs), TEMPUserName, SourceType);
                                }
                            }

                            if (dr[0].Field<string>("User_SourceType") == "易")
                            {
                                FileStream fs = new FileStream(Application.StartupPath + "\\Data3_yixin" + GlobalParam.UserName + ".txt", System.IO.FileMode.Open);
                                byte[] bs = new byte[fs.Length];
                                fs.Read(bs, 0, bs.Length);
                                fs.Close();
                                fs.Dispose();
                                if (Mode == "All" || (Mode == "图2"))
                                {
                                    SendRobotContent(Encoding.UTF8.GetString(bs), TEMPUserName, SourceType);
                                }
                            }
                            Thread.Sleep(1000);

                            if (Mode == "All" || (Mode == "图1"))
                            {
                                // SendRobotImage(Application.StartupPath + "\\Data" + GlobalParam.UserName + ".jpg", TEMPUserName, SourceType);
                                Thread st = new Thread(new ParameterizedThreadStart(ThreadSendRobotImage));
                                st.Start(new object[] { Application.StartupPath + "\\Data" + GlobalParam.UserName + ".jpg", TEMPUserName, SourceType });

                            }

                            //SendWXImage(Application.StartupPath + "\\Data2.jpg", TEMPUserName);







                        }
                    }//向监听的群发送图片

                }//设置为自动监听的用户

            }//时间段范围的才发
            else
            {
                NetFramework.Console.WriteLine("不在发图时间段:" + DateTime.Now.Hour
               + (Object2Str(myconfig.SendImageStart) + "-" + Object2Str(myconfig.SendImageEnd))
               + "或" + (Object2Str(myconfig.SendImageStart2) + "-" + Object2Str(myconfig.SendImageEnd2))
                + "或" + (Object2Str(myconfig.SendImageStart3) + "-" + Object2Str(myconfig.SendImageEnd3))
                + "或" + (Object2Str(myconfig.SendImageStart4) + "-" + Object2Str(myconfig.SendImageEnd4))
                );
            }

            #endregion

            NetFramework.Console.Write(GlobalParam.UserName + "发送图片完毕" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);
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


        public void DownLoad163CaiPiao_V163(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi)
        {
            NewResult = false;
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            #region 下载彩票结果
            //http://caipiao.163.com/award/cqssc/20180413.html


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();




            string URL = "http://caipiao.163.com/award/cqssc/";


            URL += SelectDate.ToString("yyyyMMdd") + ".html";

            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie163);
            Regex FindTableData = new Regex("<table width=\"100%\" border=\"0\" cellspacing=\"0\" class=\"awardList\">((?!</table>)[\\S\\s])+</table>", RegexOptions.IgnoreCase);
            string str_json = FindTableData.Match(Result).Value;
            //<td class="start" data-win-number="5 3 9 7 3" data-period="180404002">
            //<td class="award-winNum">
            Regex FindPeriod = new Regex("<td class=\"start\"((?!</td>)[\\S\\s])+</td>", RegexOptions.IgnoreCase);


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

                    bool Newdb = false;

                    Linq.DataLogic.NewGameResult(str_Win, str_dataperiod, out Newdb);

                    if (Newdb)
                    {
                        DealGameLogAndNotice();
                    }


                }//已开奖励
            }//每行处理

            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();
            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            {
                NewResult = true;
                LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();
                DateTime day = DateTime.Now;
                if (day.Hour < 10)
                {
                    day = day.AddDays(-1);
                }

                DrawGdi(day);
            }



            #endregion

        }


        public void DownLoad163CaiPiaoV_zhcw(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi)
        {
            NewResult = false;
            //http://m.zhcw.com/kaijiang/place_info.jsp?id=572

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            #region 下载彩票结果
            //http://caipiao.163.com/award/cqssc/20180413.html


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();

            string URL = "http://m.zhcw.com/clienth5.do?czId=572&pageNo=1&pageSize=20&transactionType=300306&src=0000100001%7C6000003060";

            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie163);

            //{"czname":"重庆时时彩","pageNo":"1","pageSize":"20","totalPage":"5549","dataList":[{"kjIssue":"20180508058","kjdate":"2018/05/08","kjznum":"4 2 6 5 1","kjtnum":"--"},{"kjIssue":"20180508057","kjdate":"2018/05/08","kjznum":"7 2 4 1 9","kjtnum":"--"},{"kjIssue":"20180508056","kjdate":"2018/05/08","kjznum":"5 7 1 7 0","kjtnum":"--"},{"kjIssue":"20180508055","kjdate":"2018/05/08","kjznum":"0 9 8 1 5","kjtnum":"--"},{"kjIssue":"20180508054","kjdate":"2018/05/08","kjznum":"9 0 4 3 8","kjtnum":"--"},{"kjIssue":"20180508053","kjdate":"2018/05/08","kjznum":"7 5 5 0 0","kjtnum":"--"},{"kjIssue":"20180508052","kjdate":"2018/05/08","kjznum":"2 7 6 3 6","kjtnum":"--"},{"kjIssue":"20180508051","kjdate":"2018/05/08","kjznum":"5 1 6 7 6","kjtnum":"--"},{"kjIssue":"20180508050","kjdate":"2018/05/08","kjznum":"8 3 3 7 8","kjtnum":"--"},{"kjIssue":"20180508049","kjdate":"2018/05/08","kjznum":"7 7 0 7 1","kjtnum":"--"},{"kjIssue":"20180508048","kjdate":"2018/05/08","kjznum":"2 5 2 0 5","kjtnum":"--"},{"kjIssue":"20180508047","kjdate":"2018/05/08","kjznum":"1 3 6 7 7","kjtnum":"--"},{"kjIssue":"20180508046","kjdate":"2018/05/08","kjznum":"6 5 2 8 2","kjtnum":"--"},{"kjIssue":"20180508045","kjdate":"2018/05/08","kjznum":"0 3 7 2 7","kjtnum":"--"},{"kjIssue":"20180508044","kjdate":"2018/05/08","kjznum":"8 0 4 8 2","kjtnum":"--"},{"kjIssue":"20180508043","kjdate":"2018/05/08","kjznum":"2 8 6 5 5","kjtnum":"--"},{"kjIssue":"20180508042","kjdate":"2018/05/08","kjznum":"2 1 9 4 4","kjtnum":"--"},{"kjIssue":"20180508041","kjdate":"2018/05/08","kjznum":"8 2 3 4 3","kjtnum":"--"},{"kjIssue":"20180508040","kjdate":"2018/05/08","kjznum":"6 8 9 5 7","kjtnum":"--"},{"kjIssue":"20180508039","kjdate":"2018/05/08","kjznum":"4 8 9 6 1","kjtnum":"--"}]}





            JArray Periods = JObject.Parse(Result)["dataList"] as JArray;


            foreach (JObject item in Periods)
            {

                string str_dataperiod = (item["kjIssue"] as JValue).Value.ToString();
                str_dataperiod = str_dataperiod.Substring(2);


                string str_Win = (item["kjznum"] as JValue).Value.ToString(); ;
                bool Newdb = false;
                Linq.DataLogic.NewGameResult(str_Win, str_dataperiod, out Newdb);
                if (Newdb)
                {
                    DealGameLogAndNotice();
                }


            }//每行处理

            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();
            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            {
                NewResult = true;
                LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();
                DateTime day = DateTime.Now;
                if (day.Hour < 10)
                {
                    day = day.AddDays(-1);
                }

                DrawGdi(day);
            }
            #endregion

        }

        public void DownLoad163CaiPiaoV_500(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi)
        {
            NewResult = false;
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            #region 下载彩票结果
            //http://m.500.com/info/kaijiang/ssc/2018-05-11.shtml


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();




            string URL = "http://m.500.com/info/kaijiang/ssc/";


            URL += SelectDate.ToString("yyyy-MM-dd") + ".shtml";

            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie163);
            Regex FindTableData = new Regex("<div class=\"lcbqc-info-tb\">((?!</div>)[\\S\\s])+</div>", RegexOptions.IgnoreCase);
            string str_json = FindTableData.Match(Result).Value;
            //<td class="start" data-win-number="5 3 9 7 3" data-period="180404002">
            //<td class="award-winNum">
            Regex FindPeriod = new Regex("<ul class=\"l-flex-row\">((?!</ul>)[\\S\\s])+</ul>", RegexOptions.IgnoreCase);


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
                    bool Newdb = false;
                    Linq.DataLogic.NewGameResult(str_Win, str_dataperiod.Substring(2), out Newdb);
                    if (Newdb)
                    {
                        DealGameLogAndNotice();
                    }


                }//已开奖励
            }//每行处理

            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();
            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            {
                NewResult = true;
                LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();
                DateTime day = DateTime.Now;
                if (day.Hour < 10)
                {
                    day = day.AddDays(-1);
                }

                DrawGdi(day);
            }



            #endregion

        }


        public void DownLoad163CaiPiaoV_Taohua(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi)
        {
            NetFramework.Console.WriteLine("开始下载桃花中间表" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            NewResult = false;
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            #region 下载彩票结果
            //http://m.500.com/info/kaijiang/ssc/2018-05-11.shtml


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();


            var taohuaresult = db.TaoHua_GameResult.OrderByDescending(t => t.GamePeriod).Take(120);



            foreach (Linq.TaoHua_GameResult item in taohuaresult)
            {



                string str_dataperiod = item.GamePeriod;


                string str_Win = item.GameResult;
                str_Win = str_Win.Substring(0, 1) + " " + str_Win.Substring(1, 1) + " " + str_Win.Substring(2, 1) + " " + str_Win.Substring(3, 1) + " " + str_Win.Substring(4, 1);

                bool Newdb = false;
                Linq.DataLogic.NewGameResult(str_Win, str_dataperiod, out Newdb);

                if (Newdb)
                {
                    DealGameLogAndNotice();
                }


            }//每行处理

            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();
            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            {
                NewResult = true;
                LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();
                DateTime day = DateTime.Now;
                if (day.Hour < 10)
                {
                    day = day.AddDays(-1);
                }

                DrawGdi(day);
            }



            #endregion

            NetFramework.Console.WriteLine("下载桃花中间表完成" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
        }


        public void DownLoad163CaiPiaoV_kaijiangwang(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);
            NewResult = false;
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            #region 下载彩票结果
            //https://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=2018-05-24&lotCode=10002


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();




            string URL = "https://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=";


            URL += SelectDate.ToString("yyyy-MM-dd") + "&lotCode=10002";
            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie163);


            JObject Resultfull = JObject.Parse(Result);


            foreach (JObject item in Resultfull["result"]["data"])
            {



                string str_dataperiod = (item["preDrawIssue"] as JValue).Value.ToString();
                str_dataperiod = str_dataperiod.Substring(2);

                string str_Win = (item["preDrawCode"] as JValue).Value.ToString();
                str_Win = str_Win.Replace(",", " ");
                bool Newdb = false;
                Linq.DataLogic.NewGameResult(str_Win, str_dataperiod, out Newdb);
                if (Newdb)
                {
                    DealGameLogAndNotice();
                }



            }//每行处理

            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();
            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            {
                NewResult = true;
                LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();
                DateTime day = DateTime.Now;
                if (day.Hour < 10)
                {
                    day = day.AddDays(-1);
                }

                DrawGdi(day);
                DealGameLogAndNotice();
            }



            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }


        //cp222789.com
        public void DownLoad163CaiPiaoV_cp222789(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载cp222789网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);
            NewResult = false;
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            #region 下载彩票结果
            //https://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=2018-05-24&lotCode=10002


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();




            string URL = "https://www.cp222789.com/data/cqssc/lotteryList/";


            URL += SelectDate.ToString("yyyy-MM-dd") + ".json?DPP64KALP77Z8697L9UY";
            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie163);


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
                bool Newdb = false;
                Linq.DataLogic.NewGameResult(str_Win, str_dataperiod, out Newdb);
                if (Newdb)
                {
                    DealGameLogAndNotice();
                }



            }//每行处理

            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();
            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            {
                NewResult = true;
                LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();
                DateTime day = DateTime.Now;
                if (day.Hour < 10)
                {
                    day = day.AddDays(-1);
                }

                DrawGdi(day);
                DealGameLogAndNotice();
            }



            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }


        //https://kj.13322.com/ssc_cqssc_history_d20180830.html
        //13322.com
        public void DownLoad163CaiPiaoV_13322(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载13322.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);
            NewResult = false;
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            #region 下载彩票结果
            //https://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=2018-05-24&lotCode=10002


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();




            string URL = "https://kj.13322.com/ssc_cqssc_history_d";


            URL += SelectDate.ToString("yyyyMMdd") + ".html";
            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie163);

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
                bool Newdb = false;
                Linq.DataLogic.NewGameResult(str_Win, str_dataperiod, out Newdb);
                if (Newdb)
                {
                    DealGameLogAndNotice();
                }



            }//每行处理

            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();
            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            {
                NewResult = true;
                LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();
                DateTime day = DateTime.Now;
                if (day.Hour < 10)
                {
                    day = day.AddDays(-1);
                }

                DrawGdi(day);
                DealGameLogAndNotice();
            }



            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }


        CookieCollection cc1395 = new CookieCollection();
        public void DownLoad163CaiPiaoV_1395p(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi)
        {
            //NetFramework.Console.Write(GlobalParam.UserName + "下载1395.com网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);
            NewResult = false;
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            #region 下载彩票结果
            //https://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=2018-05-24&lotCode=10002


            Int32 LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();




            string URL = "https://m.1395p.com/cqssc/getawarddata?t=0.8210487342419222";



            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cc1395);

            JObject newr = JObject.Parse(Result);

            string str_Win = newr["current"]["award"].Value<string>();

            str_Win = str_Win.Replace(",", "");

            string str_dataperiod = newr["current"]["date"].Value<string>() + newr["current"]["period"].Value<Int32>().ToString("000");
            str_dataperiod = str_dataperiod.Replace("-", "");
            str_dataperiod = str_dataperiod.Substring(2);
            bool Newdb = false;
            Linq.DataLogic.NewGameResult(str_Win, str_dataperiod, out Newdb);
            if (Newdb)
            {
                DealGameLogAndNotice();
            }





            Int32 AfterCheckCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();
            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi == true)
            {
                NewResult = true;
                LocalGameResultCount = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();
                DateTime day = DateTime.Now;
                if (day.Hour < 10)
                {
                    day = day.AddDays(-1);
                }

                DrawGdi(day);
                DealGameLogAndNotice();
            }



            #endregion

            //NetFramework.Console.Write(GlobalParam.UserName + "下载完毕开奖网" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

        }


        public void DrawGdi(DateTime Localday)
        {
            NetFramework.Console.Write(GlobalParam.UserName + "准备发图" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);

            DataTable PrivatePerios = NetFramework.Util_Sql.RunSqlDataTable("LocalSqlServer"
                , @"select GamePeriod as 期号,GameTime as 时间,GameResult as 开奖号码,NumTotal as 和数,BigSmall as 大小,SingleDouble as 单双,DragonTiger as 龙虎 from Game_Result where GamePrivatePeriod like '" + Localday.ToString("yyyyMMdd") + "%' and aspnet_Userid='" + GlobalParam.Key.ToString() + "'");
            DataView dv = PrivatePerios.AsDataView();

            ;
            dv.Sort = "期号";
            DataTable dtCopy = dv.ToTable();

            //GDI准备图片

            #region 画龙虎图表
            string Datatextplain = "";
            int datapindex = 1;
            foreach (DataRow datetextitem in dtCopy.Rows)
            {

                string tigerordragon = datetextitem.Field<string>("龙虎");
                switch (tigerordragon)
                {
                    case "龙":
                        Datatextplain += Linq.DataLogic.Dragon;
                        break;
                    case "虎":
                        Datatextplain += Linq.DataLogic.Tiger;
                        break;
                    case "合":
                        Datatextplain += Linq.DataLogic.OK;
                        break;
                    default:
                        break;
                }
                datapindex += 1;
                //if (datapindex == 11)
                //{
                //    Datatextplain += Environment.NewLine;
                //    datapindex = 1;
                //}

            }//行循环
            if (System.IO.File.Exists(Application.StartupPath + "\\Data3" + GlobalParam.UserName + ".txt"))
            {
                System.IO.File.Delete(Application.StartupPath + "\\Data3" + GlobalParam.UserName + ".txt");
            }
            FileStream filetowrite = new FileStream(Application.StartupPath + "\\Data3" + GlobalParam.UserName + ".txt", FileMode.OpenOrCreate);
            byte[] Result = Encoding.UTF8.GetBytes(Datatextplain);
            filetowrite.Write(Result, 0, Result.Length);
            filetowrite.Flush();
            filetowrite.Close();
            filetowrite.Dispose();
            #endregion


            #region 画龙虎图表 易信
            string Datatextplain_yixin = "";
            int datapindex_yixin = 1;
            foreach (DataRow datetextitem in dtCopy.Rows)
            {

                string tigerordragon = datetextitem.Field<string>("龙虎");
                switch (tigerordragon)
                {
                    case "龙":
                        Datatextplain_yixin += Linq.DataLogic.Dragon_yixin;
                        break;
                    case "虎":
                        Datatextplain_yixin += Linq.DataLogic.Tiger_yixin;
                        break;
                    case "合":
                        Datatextplain_yixin += Linq.DataLogic.OK_yixin;
                        break;
                    default:
                        break;
                }
                datapindex_yixin += 1;
                //if (datapindex == 11)
                //{
                //    Datatextplain += Environment.NewLine;
                //    datapindex = 1;
                //}

            }//行循环
            if (System.IO.File.Exists(Application.StartupPath + "\\Data3_yixin" + GlobalParam.UserName + ".txt"))
            {
                System.IO.File.Delete(Application.StartupPath + "\\Data3_yixin" + GlobalParam.UserName + ".txt");
            }
            FileStream filetowrite_yixin = new FileStream(Application.StartupPath + "\\Data3_yixin" + GlobalParam.UserName + ".txt", FileMode.OpenOrCreate);
            byte[] Result_yixin = Encoding.UTF8.GetBytes(Datatextplain_yixin);
            filetowrite_yixin.Write(Result_yixin, 0, Result_yixin.Length);
            filetowrite_yixin.Flush();
            filetowrite_yixin.Close();
            filetowrite_yixin.Dispose();
            #endregion

            #region 画龙虎图表钉钉
            string Datatextplain_dingding = "";
            int datapindex_dingding = 1;
            foreach (DataRow datetextitem in dtCopy.Rows)
            {

                string tigerordragon = datetextitem.Field<string>("龙虎");
                switch (tigerordragon)
                {
                    case "龙":
                        Datatextplain_dingding += Linq.DataLogic.Dragon_yixin;
                        break;
                    case "虎":
                        Datatextplain_dingding += Linq.DataLogic.Tiger_yixin;
                        break;
                    case "合":
                        Datatextplain_dingding += Linq.DataLogic.OK_yixin;
                        break;
                    default:
                        break;
                }
                datapindex_dingding += 1;
                //if (datapindex == 11)
                //{
                //    Datatextplain += Environment.NewLine;
                //    datapindex = 1;
                //}

            }//行循环
            if (System.IO.File.Exists(Application.StartupPath + "\\Data3_dingding" + GlobalParam.UserName + ".txt"))
            {
                System.IO.File.Delete(Application.StartupPath + "\\Data3_dingding" + GlobalParam.UserName + ".txt");
            }
            FileStream filetowrite_dingding = new FileStream(Application.StartupPath + "\\Data3_dingding" + GlobalParam.UserName + ".txt", FileMode.OpenOrCreate);
            byte[] Result_dingding = Encoding.UTF8.GetBytes(Datatextplain_dingding);
            filetowrite_dingding.Write(Result_dingding, 0, Result_dingding.Length);
            filetowrite_dingding.Flush();
            filetowrite_dingding.Close();
            filetowrite_dingding.Dispose();
            #endregion



            #region 画龙虎合
            Int32 TotalRow = dtCopy.Rows.Count / 10;
            Bitmap img2 = new Bitmap(303, (TotalRow + 2) * 30);
            Graphics g2 = Graphics.FromImage(img2);
            Brush bg = new SolidBrush(Color.White);
            g2.FillRectangle(bg, new Rectangle(0, 0, img2.Width, img2.Height));

            Image img_tiger = Bitmap.FromFile(Application.StartupPath + "\\tiger.png");
            Image img_dragon = Bitmap.FromFile(Application.StartupPath + "\\dragon.png");
            Image img_ok = Bitmap.FromFile(Application.StartupPath + "\\ok.png");

            Int32 RowIndex = 0;
            Int32 ResultIndex = 0;
            Int32 Reminder = 0;
            foreach (DataRow item in dtCopy.Rows)
            {
                RowIndex = ResultIndex / 10;
                Reminder = ResultIndex % 10;

                switch (item.Field<string>("龙虎"))
                {
                    case "龙":
                        g2.DrawImageUnscaled(img_dragon, Reminder * 30 + 3, RowIndex * 30 + 3, 25, 25);
                        break;
                    case "虎":
                        g2.DrawImageUnscaled(img_tiger, Reminder * 30 + 3, RowIndex * 30 + 3, 25, 25);
                        break;
                    case "合":
                        g2.DrawImageUnscaled(img_ok, Reminder * 30 + 3, RowIndex * 30 + 3, 25, 25);
                        break;
                    default:
                        break;
                }
                ResultIndex += 1;
            }

            if (System.IO.File.Exists(Application.StartupPath + "\\Data2" + GlobalParam.UserName + ".jpg"))
            {
                System.IO.File.Delete(Application.StartupPath + "\\Data2" + GlobalParam.UserName + ".jpg");
            }
            img_tiger.Dispose();
            img_dragon.Dispose();
            img_ok.Dispose();

            img2.Save(Application.StartupPath + "\\Data2" + GlobalParam.UserName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            img2.Dispose();

            g2.Dispose();

            #endregion

            #region 画表格
            Bitmap img = new Bitmap(472, 780);
            Graphics g = Graphics.FromImage(img);

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");


            Linq.aspnet_UsersNewGameResultSend myset = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key);

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

                    g.DrawString(currow.Field<string>("期号").Substring(6, 3), sf, br_g, new PointF(MarginLeft, MarginTop + i * 30));//期号
                    g.DrawString((currow.Field<DateTime?>("时间").HasValue ? currow.Field<DateTime?>("时间").Value.ToString("HH:mm") : "")
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
            if (System.IO.File.Exists(Application.StartupPath + "\\Data" + GlobalParam.UserName + ".jpg"))
            {
                System.IO.File.Delete(Application.StartupPath + "\\Data" + GlobalParam.UserName + ".jpg");
            }
            img.Save(Application.StartupPath + "\\Data" + GlobalParam.UserName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            img.Dispose();
            g.Dispose();

            #endregion
            NetFramework.Console.Write(GlobalParam.UserName + "准备完毕发图" + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);
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



        private void tm_refresh_Tick(object sender, EventArgs e)
        {





            SI_url.Text = NetFramework.Util_WEB.CurrentUrl;
            SI_url.ToolTipText = SI_url.Text;





            if (ReloadWX == true)
            {
                btn_resfresh.Visible = true;
                PicBarCode.Visible = true;
                ReloadWX = false;
            }

        }


        private void MI_MyData_Click(object sender, EventArgs e)
        {
            UserSetting us = new UserSetting();
            us.fd_username.Text = GlobalParam.UserName;
            us.SetMode("MyData");
            us.Show();
        }

        private void MI_NewUser_Click(object sender, EventArgs e)
        {
            UserSetting us = new UserSetting();
            us.SetMode("New");
            us.Show();
        }

        private void MI_UserSetting_Click(object sender, EventArgs e)
        {
            UserSetting us = new UserSetting();
            us.SetMode("Modify");
            us.Show();
        }

        private void MI_Ratio_Setting_Click(object sender, EventArgs e)
        {
            F_Game_BasicRatio fm = new F_Game_BasicRatio();
            fm.Show();
        }

        private void StartForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            GC.Collect();
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
            DrawGdi(DateTime.Today);
        }

        private void Btn_StartDownLoad_Click(object sender, EventArgs e)
        {
            while (true)
            {
                try
                {
                    DownloadResult();
                }
                catch (Exception)
                {

                }
                Application.DoEvents();
                Thread.Sleep(1000);
            }
        }

        private void btn_TestOrder_Click(object sender, EventArgs e)
        {

            DataTable MemberSource = new DataTable();
            MemberSource.Columns.Add("User_Contact");
            MemberSource.Columns.Add("User_ContactType");
            MemberSource.Columns.Add("User_ContactID");
            MemberSource.Columns.Add("User_SourceType");
            MemberSource.Columns.Add("User_ContactTEMPID");
            MemberSource.Columns.Add("User_IsReply", typeof(Boolean));
            MemberSource.Columns.Add("User_IsReceiveTransfer", typeof(Boolean));
            MemberSource.Columns.Add("User_IsCaculateFuli", typeof(Boolean));

            MemberSource.Rows.Add(new object[] { "min", "个人", "669811161", "@12345", true, false, true });


            string UpPoint = Linq.DataLogic.WX_UserReplyLog_MySendCreate("上分5000", MemberSource.Rows[0], DateTime.Now.AddSeconds(-1));

            DateTime ck0 = DateTime.Now;
            string R0 = NewWXContent(ck0, "查", MemberSource.Rows[0], "微", true);

            DateTime ckt = ck0.AddSeconds(1); ;
            string R1 = NewWXContent(ckt, "下分50,龙50，和50", MemberSource.Rows[0], "微", true);

            DateTime ckt2 = ckt.AddSeconds(1);
            string R2 = NewWXContent(ckt2, "查", MemberSource.Rows[0], "微", true);



        }
        bool ShowBlack = false;
        private void OpenBlack_Click(object sender, EventArgs e)
        {
            ShowBlack = !ShowBlack;

            if (ShowBlack == true)
            {
                Program.AllocConsole();
                NetFramework.Console.WriteLine("LogStart");
            }
            else
            {
                Program.FreeConsole();
            }



        }


        string yixinQrCodeData = "";
        string yixinQrUrl = "";
        private void LoadYiXinBarCode()
        {

            this.Invoke(new Action(() =>
            {
                PicBarCode_yixin.Visible = true;
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
            DataTable Result1 = WeixinRoboot.Linq.DataLogic.GetBossReportSource("微", "20180905");
            DataTable Result2 = WeixinRoboot.Linq.DataLogic.GetBossReportSource("微", "20180112.20190630");

            DrawDataTable(Result2);
        }


        private void RepeatSendBossReport()
        {
            while (true)
            {
                try
                {



                    if ((DateTime.Now.Hour >= 2 && DateTime.Now.Hour < 10) == true)
                    {


                        Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
                        db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

                        var boss = db.WX_UserReply.Where(t => t.aspnet_UserID == GlobalParam.Key && t.IsBoss == true);
                        foreach (var Bossitem in boss)
                        {
                            if (WeiXinOnLine == true && Bossitem.WX_SourceType == "微")
                            {
                                var Rows = RunnerF.MemberSource.Select("User_ContactID='" + Bossitem.WX_UserName + "' and User_SourceType='" + Bossitem.WX_SourceType + "'");

                                if (Rows.Count() > 0)
                                {
                                    var findsendlog = db.PIC_EndSendLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key && t.WX_BossID == Bossitem.WX_UserName && t.WX_SendDate == DateTime.Today && t.WX_SourceType == "微");
                                    if (findsendlog == null)
                                    {
                                        NetFramework.Console.WriteLine("准备老板查询发图");
                                        DataTable Result2 = WeixinRoboot.Linq.DataLogic.GetBossReportSource(Bossitem.WX_SourceType, DateTime.Today.AddDays(-1).ToString("yyyyMMdd"));
                                        DrawDataTable(Result2);

                                        //SendRobotImage(Application.StartupPath + "\\Data" + GlobalParam.UserName + "老板查询.jpg", Rows[0].Field<string>("User_ContactTEMPID"), Bossitem.WX_SourceType);

                                        Thread st = new Thread(new ParameterizedThreadStart(ThreadSendRobotImage));
                                        st.Start(new object[] { Application.StartupPath + "\\Data" + GlobalParam.UserName + "老板查询.jpg", Rows[0].Field<string>("User_ContactTEMPID"), Bossitem.WX_SourceType });



                                        NetFramework.Console.WriteLine("老板查询发图完毕");
                                        Linq.PIC_EndSendLog bsl = new Linq.PIC_EndSendLog();
                                        bsl.WX_BossID = Bossitem.WX_UserName;
                                        bsl.WX_SourceType = Bossitem.WX_SourceType;
                                        bsl.WX_SendDate = DateTime.Now;
                                        bsl.WX_UserName = Bossitem.WX_UserName;
                                        bsl.aspnet_UserID = GlobalParam.Key;
                                        db.PIC_EndSendLog.InsertOnSubmit(bsl);
                                        db.SubmitChanges();
                                    }

                                }//BOSS联系人找到
                                else
                                {
                                    NetFramework.Console.WriteLine("BOSS联系人找不到，图片发不出");
                                }
                            }
                            if (YiXinOnline == true && Bossitem.WX_SourceType == "易")
                            {
                                var Rows = RunnerF.MemberSource.Select("User_ContactID='" + Bossitem.WX_UserName + "' and User_SourceType='" + Bossitem.WX_SourceType + "'");

                                if (Rows.Count() > 0)
                                {
                                    var findsendlog = db.PIC_EndSendLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key && t.WX_BossID == Bossitem.WX_UserName && t.WX_SendDate == DateTime.Today && t.WX_SourceType == "易");
                                    if (findsendlog == null)
                                    {
                                        NetFramework.Console.WriteLine("准备老板查询发图");
                                        DataTable Result2 = WeixinRoboot.Linq.DataLogic.GetBossReportSource(Bossitem.WX_SourceType, DateTime.Today.AddDays(-1).ToString("yyyyMMdd"));
                                        DrawDataTable(Result2);
                                        //SendRobotImage(Application.StartupPath + "\\Data" + GlobalParam.UserName + "老板查询.jpg", Rows[0].Field<string>("User_ContactTEMPID"), Bossitem.WX_SourceType);

                                        Thread st = new Thread(new ParameterizedThreadStart(ThreadSendRobotImage));
                                        st.Start(new object[] { Application.StartupPath + "\\Data" + GlobalParam.UserName + "老板查询.jpg", Rows[0].Field<string>("User_ContactTEMPID"), Bossitem.WX_SourceType });


                                        NetFramework.Console.WriteLine("老板查询发图完毕");

                                        Linq.PIC_EndSendLog bsl = new Linq.PIC_EndSendLog();
                                        bsl.WX_BossID = Bossitem.WX_UserName;
                                        bsl.WX_SourceType = Bossitem.WX_SourceType;
                                        bsl.WX_SendDate = DateTime.Now;
                                        bsl.WX_UserName = Bossitem.WX_UserName;
                                        bsl.aspnet_UserID = GlobalParam.Key;
                                        db.PIC_EndSendLog.InsertOnSubmit(bsl);
                                        db.SubmitChanges();
                                    }
                                }//BOSS联系人找到
                                else
                                {
                                    NetFramework.Console.WriteLine("BOSS联系人找不到，图片发不出");
                                }
                            }



                        }
                    }//封盘时间才发
                }//try 结束
                catch (Exception AnyError)
                {
                    NetFramework.Console.WriteLine("定时发送老板查询异常" + AnyError.StackTrace);

                }
                Thread.Sleep(60 * 1000);



            }//检查时间循环
        }

        private void btn_InjectAndDo_Click(object sender, EventArgs e)
        {
            NetFramework.WindowsApi.EnumWindows(new NetFramework.WindowsApi.CallBack(EnumWinsCallBack), 0);
        }
        public List<WinSends> InjectWins = new List<WinSends>();
        public bool EnumWinsCallBack(IntPtr hwnd, int lParam)
        {
            StringBuilder sb = new StringBuilder(512);
            NetFramework.WindowsApi.GetClassNameW(hwnd, sb, sb.Capacity);
            if (sb.ToString() == "ChatWnd" || (sb.ToString() == "StandardFrame_DingTalk"))
            {
                StringBuilder RAW = new StringBuilder(512);
                NetFramework.WindowsApi.GetWindowText(hwnd, RAW, 512);

                NetFramework.WindowsApi.SetWindowText(hwnd, "智能发图" + RAW.ToString().Replace("智能发图", ""));
                if (InjectWins.SingleOrDefault(t => t.hwnd == hwnd) == null)
                {
                    WinSends newws = new WinSends();
                    newws.hwnd = hwnd;
                    InjectWins.Add(newws);
                }


            }
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
                    NetFramework.Console.WriteLine(AnyError.Message);

                }
            }

        }
        public bool SendPicEnumWins()
        {
            try
            {
                //StringBuilder sb = new StringBuilder(512);
                //NetFramework.WindowsApi.GetClassNameW(hwnd, sb, sb.Capacity);
                //if (sb.ToString() == "ChatWnd")
                //{
                //    StringBuilder RAW = new StringBuilder(512);
                //    NetFramework.WindowsApi.GetWindowText(hwnd, RAW, 512);
                //    if (RAW.ToString().Contains("智能发图"))
                //    {
                //        hwndSendImageFile(Application.StartupPath + "\\Data" + GlobalParam.UserName + ".jpg", hwnd);
                //        Thread.Sleep(200);
                //        hwndSendTextFile(Application.StartupPath + "\\Data3" + GlobalParam.UserName + ".txt", hwnd);
                //        Thread.Sleep(200);

                //        hwndDragFile(Application.StartupPath + "\\PCGIFS\\开始下注.gif", hwnd);



                //    }



                //}


                for (int i = 0; i < InjectWins.Count; i++)
                {
                    WinSends wins = InjectWins[InjectWins.Count - 1];
                    StringBuilder RAW = new StringBuilder(512);
                    NetFramework.WindowsApi.GetWindowText(wins.hwnd, RAW, 512);

                    if (RAW.ToString() == "")
                    {
                        InjectWins.Remove(wins);
                        continue;

                    }
                }



                foreach (WinSends wins in InjectWins)
                {
                    StringBuilder RAW = new StringBuilder(512);
                    NetFramework.WindowsApi.GetWindowText(wins.hwnd, RAW, 512);

                    NetFramework.WindowsApi.SetWindowText(wins.hwnd, "智能发图" + RAW.ToString().Replace("智能发图", ""));
                    if (
                        (DateTime.Now.Hour * 60 + DateTime.Now.Minute + (DateTime.Now.Hour < 3 ? 60 * 24 : 0) > wins.封盘小时 * 60 + wins.封盘分钟
                              + (wins.封盘小时 < 3 ? 60 * 24 : 0)
                              )

                        ||
                        (DateTime.Now.Hour * 60 + DateTime.Now.Minute + (DateTime.Now.Hour < 3 ? 60 * 24 : 0) < wins.开盘小时 * 60 + wins.开盘分钟
                              + (wins.开盘小时 < 3 ? 60 * 24 : 0)

                        )
                        )
                    {

                        continue;

                    }

                    hwndSendImageFile(Application.StartupPath + "\\Data" + GlobalParam.UserName + ".jpg", wins.hwnd);
                    Thread.Sleep(200);
                    if (RAW.ToString().Contains("钉钉"))
                    {
                        hwndSendTextFile(Application.StartupPath + "\\Data3_dingding" + GlobalParam.UserName + ".txt", wins.hwnd);

                    }
                    else
                    {
                        hwndSendTextFile(Application.StartupPath + "\\Data3" + GlobalParam.UserName + ".txt", wins.hwnd);

                    }

                    Thread.Sleep(200);
                    Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
                    db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

                    if (wins.龙虎单图 == true)
                    {
                        Linq.Game_Result gr = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).OrderByDescending(t => t.GamePeriod).First();
                        if (gr.DragonTiger == "龙")
                        {
                            hwndDragFile(Application.StartupPath + "\\PCGIFS\\龙.gif", wins.hwnd);
                        }
                        else if (gr.DragonTiger == "虎")
                        {
                            hwndDragFile(Application.StartupPath + "\\PCGIFS\\虎.gif", wins.hwnd);
                        }
                        else if (gr.DragonTiger == "合")
                        {
                            hwndDragFile(Application.StartupPath + "\\PCGIFS\\合.gif", wins.hwnd);
                        }
                    }


                    if (wins.开奖后请下注 == true)
                    {
                        Thread.Sleep(2000);
                        hwndDragFile(Application.StartupPath + "\\PCGIFS\\开始下注.gif", wins.hwnd);
                    }




                }
            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine(AnyError.Message);

            }
            return true;
        }

        public bool FindChildsCallBack(IntPtr hwnd, int lParam)
        {
            return false;
        }


        private void hwndSendImageFile(string FileImage, IntPtr hwnd)
        {

            Clipboard.Clear();

            System.Collections.Specialized.StringCollection sc = new System.Collections.Specialized.StringCollection();
            sc.Add(FileImage);
            Clipboard.SetFileDropList(sc);


            NetFramework.WindowsApi.ShowWindow(hwnd, 1);
            NetFramework.WindowsApi.SetForegroundWindow(hwnd);
            NetFramework.WindowsApi.SetActiveWindow(hwnd);
            NetFramework.WindowsApi.SwitchToThisWindow(hwnd, true);
            NetFramework.WindowsApi.SetFocus(hwnd);//设定焦点
            Thread.Sleep(700);

            //NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_MENU, 0, 0, 0);
            //NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_S, 0, 0, 0);

            //Thread.Sleep(50);
            //Application.DoEvents();
            //NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_S, 0, 2, 0);
            //NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_MENU, 0, 2, 0);
           

            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 0, 0);
            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 0, 0);

            Thread.Sleep(50);
            Application.DoEvents();
            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 2, 0);
            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 2, 0);



            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_RETURN, 0, 0, 0);

            Thread.Sleep(50);
            Application.DoEvents();
            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_RETURN, 0, 2, 0);





        }
        private void hwndSendTextFile(string FileText, IntPtr hwnd)
        {

            Clipboard.Clear();
            FileStream fs = new FileStream(FileText, System.IO.FileMode.Open);
            byte[] ToSend = new byte[fs.Length];
            fs.Read(ToSend, 0, ToSend.Length);
            fs.Close();
            fs.Dispose();

            NetFramework.WindowsApi.ShowWindow(hwnd, 1);

            NetFramework.WindowsApi.SetForegroundWindow(hwnd);
            NetFramework.WindowsApi.SetActiveWindow(hwnd);
            NetFramework.WindowsApi.SwitchToThisWindow(hwnd, true);
            NetFramework.WindowsApi.SetFocus(hwnd);//设定焦点
            Thread.Sleep(700);
            //Thread.Sleep(200);

            //NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_MENU, 0, 0, 0);
            //NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_S, 0, 0, 0);

            //Thread.Sleep(50);
            //Application.DoEvents();
            //NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_S, 0, 2, 0);
            //NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_MENU, 0, 2, 0);
            Clipboard.Clear();
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
            string ToSendt = Encoding.UTF8.GetString(ToSend);
            Clipboard.SetText((ToSendt.StartsWith(Linq.DataLogic.Tiger) ? " " : "") + ToSendt);

            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 0, 0);
            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 0, 0);
            Thread.Sleep(50);
            Application.DoEvents();
            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 2, 0);
            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 2, 0);
            Thread.Sleep(50);

            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_RETURN, 0, 0, 0);

            Thread.Sleep(50);
            Application.DoEvents();
            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_RETURN, 0, 2, 0);

        }
        private void hwndSendText(string SendText, IntPtr hwnd)
        {
            if (Setting == true)
            {
                return;
            }

            Clipboard.Clear();



            NetFramework.WindowsApi.ShowWindow(hwnd, 1);
            NetFramework.WindowsApi.SetForegroundWindow(hwnd);
            NetFramework.WindowsApi.SetActiveWindow(hwnd);
            NetFramework.WindowsApi.SwitchToThisWindow(hwnd, true);
            NetFramework.WindowsApi.SetFocus(hwnd);//设定焦点
            Thread.Sleep(700);
            //Thread.Sleep(200);

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


            Clipboard.SetText((SendText.StartsWith(Linq.DataLogic.Tiger) ? " " : "") + SendText);

            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 0, 0);
            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 0, 0);
            Thread.Sleep(50);
            Application.DoEvents();
            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 2, 0);
            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 2, 0);
            Thread.Sleep(50);


            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_RETURN, 0, 0, 0);

            Thread.Sleep(50);
            Application.DoEvents();
            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_RETURN, 0, 2, 0);
        }

        private bool bytecompare(byte[] source, byte[] newdata)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] != newdata[i])
                {
                    return false;
                }
            }
            return true;
        }
        private void hwndDragFile(string FileImage, IntPtr hwnd)
        {

            Clipboard.Clear();

            System.Collections.Specialized.StringCollection sc = new System.Collections.Specialized.StringCollection();
            sc.Add(FileImage);
            Clipboard.SetFileDropList(sc);


            NetFramework.WindowsApi.ShowWindow(hwnd, 1);
            NetFramework.WindowsApi.SetForegroundWindow(hwnd);
            NetFramework.WindowsApi.SetActiveWindow(hwnd);
            NetFramework.WindowsApi.SwitchToThisWindow(hwnd, true);


            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 0, 0);
            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 0, 0);

            Thread.Sleep(100);
            Application.DoEvents();
            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_V, 0, 2, 0);
            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 2, 0);



            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_RETURN, 0, 0, 0);

            Thread.Sleep(50);
            Application.DoEvents();
            NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_RETURN, 0, 2, 0);
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

                        foreach (WinSends sendwins in InjectWins)
                        {
                            StringBuilder RAW = new StringBuilder(512);
                            NetFramework.WindowsApi.GetWindowText(sendwins.hwnd, RAW, 512);

                            if (RAW.ToString() == "" || sendwins.开盘 == false
                                || ((DateTime.Now.Hour * 60 + DateTime.Now.Minute + 10 + (DateTime.Now.Hour < 3 ? 24 * 60 : 0) < sendwins.开盘小时 * 60 + sendwins.开盘分钟 + (sendwins.开盘小时 < 3 ? 24 * 60 : 0)))
                                || ((DateTime.Now.Hour * 60 + DateTime.Now.Minute + 10 + (DateTime.Now.Hour < 3 ? 24 * 60 : 0) > sendwins.开盘小时 * 60 + sendwins.开盘分钟 + (sendwins.开盘小时 < 3 ? 24 * 60 : 0)+5))
                            )
                            {
                                continue;

                            }
                            if (GlobalParam.HaveSend.ContainsKey(DateTime.Today.ToString("yyyyMMdd") + sendwins.hwnd.ToString() + "开盘") == false)
                            {
                                hwndDragFile(Application.StartupPath + "\\PCGIFS\\开盘啦.png", sendwins.hwnd);

                                hwndDragFile(Application.StartupPath + "\\PCGIFS\\开盘啦.png", sendwins.hwnd);

                                hwndDragFile(Application.StartupPath + "\\PCGIFS\\开盘啦.png", sendwins.hwnd);

                                //hwndDragFile(Application.StartupPath + "\\PCGIFS\\财源滚滚来.gif", sendwins.hwnd);

                                //hwndDragFile(Application.StartupPath + "\\PCGIFS\\开始下注.gif", sendwins.hwnd);

                                GlobalParam.HaveSend.Add(DateTime.Today.ToString("yyyyMMdd") + sendwins.hwnd.ToString() + "开盘", true);
                            }

                        }
                    }

                    //封盘
                    //if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour <= 5)
                    {

                        foreach (WinSends sendwins in InjectWins)
                        {
                            StringBuilder RAW = new StringBuilder(512);
                            NetFramework.WindowsApi.GetWindowText(sendwins.hwnd, RAW, 512);

                            if
                                ((RAW.ToString() == "" || sendwins.封盘 == false)
                                || ((DateTime.Now.Hour * 60 + DateTime.Now.Minute + (DateTime.Now.Hour < 3 ? 24 * 60 : 0) < sendwins.封盘小时 * 60 + sendwins.封盘分钟 + 2 + (sendwins.封盘小时 < 3 ? 24 * 60 : 0)))
                                || ((DateTime.Now.Hour * 60 + DateTime.Now.Minute + (DateTime.Now.Hour < 3 ? 24 * 60 : 0) > sendwins.封盘小时 * 60 + sendwins.封盘分钟 + 2 + (sendwins.封盘小时 < 3 ? 24 * 60 : 0)+5))
                               
                                
                                )
                            {
                                //不勾或没到时间不发
                                continue;

                            }
                            if (GlobalParam.HaveSend.ContainsKey(DateTime.Today.ToString("yyyyMMdd") + sendwins.hwnd.ToString() + "封盘") == false)
                            {
                                hwndDragFile(Application.StartupPath + "\\PCGIFS\\封盘.png", sendwins.hwnd);
                                Thread.Sleep(1000);
                                hwndDragFile(Application.StartupPath + "\\PCGIFS\\封盘.png", sendwins.hwnd);
                                Thread.Sleep(1000);
                                hwndDragFile(Application.StartupPath + "\\PCGIFS\\封盘.png", sendwins.hwnd);
                                Thread.Sleep(1000);
                                hwndDragFile(Application.StartupPath + "\\PCGIFS\\正在结算.gif", sendwins.hwnd);
                                Thread.Sleep(1000);
                                hwndDragFile(Application.StartupPath + "\\PCGIFS\\正在结算.gif", sendwins.hwnd);
                                Thread.Sleep(1000);
                                hwndDragFile(Application.StartupPath + "\\PCGIFS\\正在结算.gif", sendwins.hwnd);
                                GlobalParam.HaveSend.Add(DateTime.Today.ToString("yyyyMMdd") + sendwins.hwnd.ToString() + "封盘", true);
                            }

                        }
                    }
                    //下注加
                    //if (true)
                    //{

                    //}
                    //整点停止下注
                    Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
                    db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

                    Linq.Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == DateTime.Now.ToString("HH:mm"));
                    if (testmin != null && GlobalParam.HaveSend.ContainsKey(DateTime.Today.ToString("yyyyMMdd") + testmin.PeriodIndex) == false)
                    {
                        foreach (WinSends sendwins in InjectWins)
                        {
                            StringBuilder RAW = new StringBuilder(512);
                            NetFramework.WindowsApi.GetWindowText(sendwins.hwnd, RAW, 512);

                            if (RAW.ToString() == "" || sendwins.整点停止 == false)
                            {
                                continue;

                            }
                            hwndDragFile(Application.StartupPath + "\\PCGIFS\\停止下注.gif", sendwins.hwnd);
                        }
                        GlobalParam.HaveSend.Add(DateTime.Today.ToString("yyyyMMdd") + testmin.PeriodIndex, true);

                    }




                    //if ( HaveSend.ContainsKey(DateTime.Today.ToString("yyyyMMdd") + "最后一期") == false)
                    //{
                    foreach (WinSends sendwins in InjectWins)
                    {
                        StringBuilder RAW = new StringBuilder(512);
                        NetFramework.WindowsApi.GetWindowText(sendwins.hwnd, RAW, 512);

                        if (RAW.ToString() == "" || sendwins.最后一期 == false
                            || ((DateTime.Now.Hour * 60 + DateTime.Now.Minute + (sendwins.封盘小时 * 60 + sendwins.封盘分钟 >= 600 && sendwins.封盘小时 * 60 + sendwins.封盘分钟 < 1320 ? 7 : 2) + (DateTime.Now.Hour < 3 ? 24 * 60 : 0) < sendwins.封盘小时 * 60 + sendwins.封盘分钟 + (sendwins.封盘小时 < 3 ? 24 * 60 : 0)))
                            )
                        {
                            continue;

                        }
                        if (GlobalParam.HaveSend.ContainsKey(DateTime.Today.ToString("yyyyMMdd") + sendwins.hwnd.ToString() + "最后一期") == false)
                        {

                            hwndDragFile(Application.StartupPath + "\\PCGIFS\\最后一期.gif", sendwins.hwnd);
                            Thread.Sleep(200);
                            hwndDragFile(Application.StartupPath + "\\PCGIFS\\最后一期.gif", sendwins.hwnd);
                            Thread.Sleep(200);
                            hwndDragFile(Application.StartupPath + "\\PCGIFS\\最后一期.gif", sendwins.hwnd);

                            GlobalParam.HaveSend.Add(DateTime.Today.ToString("yyyyMMdd") + sendwins.hwnd.ToString() + "最后一期", true);

                        }

                    }


                    //}




                    //5分钟，10点到2点
                    if (DateTime.Now.Hour <= 1 || DateTime.Now.Hour >= 22)
                    {
                        if (GlobalParam.HaveSend.ContainsKey(DateTime.Today.ToString("yyyyMMdd") + "5分钟") == false)
                        {
                            foreach (WinSends sendwins in InjectWins)
                            {
                                StringBuilder RAW = new StringBuilder(512);
                                NetFramework.WindowsApi.GetWindowText(sendwins.hwnd, RAW, 512);

                                if (RAW.ToString() == "" || sendwins.高速期 == false)
                                {
                                    continue;

                                }
                                if (GlobalParam.HaveSend.ContainsKey(DateTime.Today.ToString("yyyyMMdd") + sendwins.hwnd.ToString() + "5分钟") == false)
                                {
                                    hwndDragFile(Application.StartupPath + "\\PCGIFS\\5分钟.gif", sendwins.hwnd);
                                    Thread.Sleep(500);
                                    hwndDragFile(Application.StartupPath + "\\PCGIFS\\5分钟.gif", sendwins.hwnd);
                                    Thread.Sleep(500);
                                    hwndDragFile(Application.StartupPath + "\\PCGIFS\\5分钟.gif", sendwins.hwnd);

                                    GlobalParam.HaveSend.Add(DateTime.Today.ToString("yyyyMMdd") + sendwins.hwnd.ToString() + "5分钟", true);
                                }
                            }




                        }

                    }
                    //文字1，2，3
                    foreach (WinSends sendwins in InjectWins)
                    {
                        StringBuilder RAW = new StringBuilder(512);
                        NetFramework.WindowsApi.GetWindowText(sendwins.hwnd, RAW, 512);

                        if (RAW.ToString() == "")
                        {
                            continue;

                        }
                        if (sendwins.文字1时间.AddMinutes(sendwins.文字1间隔) <= DateTime.Now && sendwins.文字1 != "")
                        {

                            hwndSendText(sendwins.文字1, sendwins.hwnd);
                            sendwins.文字1时间 = DateTime.Now;
                        }


                        if (sendwins.文字2时间.AddMinutes(sendwins.文字2间隔) <= DateTime.Now && sendwins.文字2 != "")
                        {

                            hwndSendText(sendwins.文字2, sendwins.hwnd);
                            sendwins.文字2时间 = DateTime.Now;
                        }

                        if (sendwins.文字3时间.AddMinutes(sendwins.文字3间隔) <= DateTime.Now && sendwins.文字3 != "")
                        {

                            hwndSendText(sendwins.文字3, sendwins.hwnd);
                            sendwins.文字3时间 = DateTime.Now;
                        }


                    }

                }
                catch (Exception AnyError)
                {

                    Console.WriteLine(AnyError.Message);
                    Console.WriteLine(AnyError.StackTrace);
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
            SendPicEnumWins();

        }

        private void btn_runtest_Click(object sender, EventArgs e)
        {
            foreach (WinSends item in InjectWins)
            {

                hwndSendText(Linq.DataLogic.Tiger + Linq.DataLogic.Dragon + Linq.DataLogic.OK + Linq.DataLogic.Tiger
                    , item.hwnd);
            }
        }

        private void mi_reminderquery_Click(object sender, EventArgs e)
        {
            RemindQuery rq = new RemindQuery();
            rq.Show();
        }






    }
}
