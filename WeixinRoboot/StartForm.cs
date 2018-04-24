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
namespace WeixinRoboot
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();

       
            RunnerF.StartF = this;
            RunnerF.Show();



        }
        Guid WaitScanTHreadID = Guid.NewGuid();
        Guid KeepAliveThreadID = Guid.NewGuid();
        Guid DownloadResultThreadID = Guid.NewGuid();
        public string _uuid = "";
        public System.Net.CookieCollection cookie = new CookieCollection();

        private string ImgUrl = "";
        private void LoadBarCode()
        {

            StatusMessage = "等待微信二维码";
            string Result = NetFramework.Util_WEB.OpenUrl("https://login.weixin.qq.com/jslogin?appid=wx782c26e4c19acffb&fun=new&lang=zh_CN&_=" + JavaTimeSpan()
              , "", "", "GET", cookie);


            string UUID = Result.Substring(Result.IndexOf("uuid"));
            UUID = UUID.Substring(UUID.IndexOf("\"") + 1);
            UUID = UUID.Substring(0, UUID.Length - 2);
            _uuid = UUID;
            //登陆https://login.weixin.qq.com/qrcode/XXXXXX
            ImgUrl = "https://login.weixin.qq.com/qrcode/" + UUID + "?t=" + new Random().Next().ToString();

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
        }

        private Int32 _tip = 1;

        public JObject InitResponse = null;
        public XmlDocument newridata = new XmlDocument();
        public string ScanResult = "";
        public string AsyncKey = "";

        string Uin = "";
        string Sid = "";
        string Skey = "";
        string DeviceID = "";

        public JObject j_BaseRequest = null;
        JObject synckeys = null;

        bool HaveScan = false;

        public RunnerForm RunnerF = new RunnerForm();

        static bool MI_GameLogManulDealEnabled = false;



        public string MyUserName = "";

        private void StartThreadDo()
        {

            LoadBarCode();
            //使用get方法，查询地址：https://login.weixin.qq.com/cgi-bin/mmwebwx-bin/login?uuid=XXXXXX&tip=1&_=时间戳

            //这里的XXXXXX是我们刚才获取的uuid，时间戳同上。tip在第一次获取时应为1，这个数是每次查询要变的。

            //如果服务器返回：window.code=201，则说明此时用户在手机端已经完成扫描，但还没有点击确认，继续使用上面的地址查询，但tip要变成0；

            //如果服务器返回：

            //window.code=200
            //window.redirect_uri="XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
            StatusMessage = "等待扫码";
            WaitScanTHreadID = Guid.NewGuid();
            Thread WaitScan = new Thread(new ParameterizedThreadStart(WaitScanThreadDo));
            WaitScan.Start(WaitScanTHreadID);
        }
        private void WaitScanThreadDo(Object ThreadID)
        {

            try
            {
            ReCheckURI:
                Thread.Sleep(500);
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
                ScanResult = Result;
                if (Result.Contains("window.code=201"))
                {
                    _tip = 0;
                    StatusMessage = "手机已扫码";
                    HaveScan = true;
                }
                else if (Result.Contains("window.code=200"))
                {
                    HaveScan = true;
                    StatusMessage = "手机已确认";


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
                    StatusMessage = "获取参数/Cookie";




                    //                    5、微信初始化

                    //这个是很重要的一步，我在这个步骤折腾了很久。。。

                    //要使用POST方法，访问地址：https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxinit?r=时间戳&lang=ch_ZN&pass_ticket=XXXXXX

                    //其中，时间戳不用解释，pass_ticket是我们在上面获取的一长串字符。

                    //POST的内容是个json串，{"BaseRequest":{"Uin":"XXXXXXXX","Sid":"XXXXXXXX","Skey":XXXXXXXXXXXXX","DeviceID":"e123456789012345"}}

                    //uin、sid、skey分别对应上面步骤4获取的字符串，DeviceID是e后面跟着一个15字节的随机数。

                    //程序里面要注意使用UTF8编码方式。
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

                    JObject queryRoomMember = new JObject();
                    queryRoomMember.Add("BaseRequest", j_BaseRequest["BaseRequest"]);
                    Int32 Groupcount = 0;
                    JArray jaroom = new JArray();
                    foreach (var item in (Members["MemberList"]) as JArray)
                    {
                        string UserNametempID = (item["UserName"] as JValue).Value.ToString();
                        if (UserNametempID.StartsWith("@@"))
                        {
                            JObject newroom = new JObject();
                            newroom.Add("UserName", UserNametempID);
                            newroom.Add("EncryChatRoomId", "");
                            jaroom.Add(newroom);
                            Groupcount += 1;
                        }

                    }
                    queryRoomMember.Add("List", jaroom);
                    queryRoomMember.Add("Count", Groupcount);

                    string str_membroom = NetFramework.Util_WEB.OpenUrl("https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxbatchgetcontact?type=ex&r=" + JavaTimeSpan()
                  , "https://wx2.qq.com/", queryRoomMember.ToString(), "POST", cookie, Encoding.UTF8);

                    JObject RoomMembers = JObject.Parse(str_membroom);







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

                    JObject State = new JObject();
                    State.Add("BaseRequest", j_BaseRequest["BaseRequest"]);
                    State.Add("Code", "3");
                    State.Add("FromUserName", MyUserName);
                    State.Add("ToUserName", MyUserName);
                    State.Add("ClientMsgId", JavaTimeSpan());

                    string str_state = NetFramework.Util_WEB.OpenUrl("https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxstatusnotify"
                   , "https://wx2.qq.com/", j_BaseRequest.ToString(), "POST", cookie);

                    Thread Keepalive = new Thread(new ParameterizedThreadStart(KeepAlieveDo));
                    KeepAliveThreadID = Guid.NewGuid();
                    Keepalive.Start(KeepAliveThreadID);

                    return;
                }

                if (_tip != 0)
                {
                    StatusMessage = "等待扫码";
                    _tip += 1;
                }
                goto ReCheckURI;
            }
            catch (Exception AnyError)
            {
                MessageBox.Show(AnyError.Message + Environment.NewLine + AnyError.StackTrace);
            }




        }
        private void KeepAlieveDo(Object ThreadID)
        {
        Repeat:

            try
            {

                if (KillThread.ContainsKey((Guid)ThreadID))
                {
                    return;
                }

                #region "微信监听"
                StatusMessage = "机器人监听中";
                //使用get方法，设置超时为60秒，访问：https://webpush.wx2.qq.com/cgi-bin/mmwebwx-bin/synccheck?sid=XXXXXX&uin=XXXXXX&synckey=XXXXXX&r=时间戳&skey=XXXXXX&deviceid=XXXXXX&_=时间戳

                //其他几个参数不用解释，这里的synckey需要说一下，前面的步骤获取的json串中有多个key信息，需要把这些信息拼起来，key_val，中间用|分割，类似这样：

                //1_652651920|2_652651939|3_652651904|1000_0

                //服务器返回：window.synccheck={retcode:”0”,selector:”0”}

                //retcode为0表示成功，selector为2和6表示有新信息。4表示公众号新信息。
                string CheckUrl3 = "https://webpush.wx2.qq.com/cgi-bin/mmwebwx-bin/synccheck?sid=" + Sid + "&uin=" + Uin + "&synckey=" + System.Web.HttpUtility.UrlEncode(AsyncKey) + "&r=" + JavaTimeSpan() + "&skey=" + Skey + "&deviceid=" + DeviceID + "&_=" + JavaTimeSpan();
                string Result3 = NetFramework.Util_WEB.OpenUrl(CheckUrl3
                  , "https://wx2.qq.com/", "", "GET", cookie, false);
                Result3 = Result3.Substring(Result3.IndexOf("=") + 1);
                JObject Check = JObject.Parse(Result3);

                Console.WriteLine(DateTime.Now.ToString());
                Console.WriteLine(Result3);

                if (Check["retcode"].ToString() == "1101")
                {
                    //MessageBox.Show("微信已在别的地方登录");
                    //ReloadWX = true;
                    //return;
                    WXInit();
                    goto Sleep;
                }

                if ((Result3.Contains("selector:\"0\"")) == false)
                {

                    StatusMessage = Result3;


                    // 9、读取新信息

                    //检测到有信息以后，用POST方法，访问：https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxsync?sid=XXXXXX&skey=XXXXXX

                    //POST的内容：

                    //{"BaseRequest" : {"DeviceID":"XXXXXX,"Sid":"XXXXXX", "Skey":"XXXXXX", "Uin":"XXXXXX"},"SyncKey" : {"Count":4,"List":[{"Key":1,"Val":652653204},{"Key":2,"Val":652653674},{"Key":3,"Val":652653544},{"Key":1000,"Val":0}]},"rr" :时间戳}

                    //注意这里的SyncKey格式，参考前面的说明。
                    string CheckUrl4 = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxsync?sid=" + Sid + "&skey=" + Skey;
                    JObject body4 = new JObject();
                    body4.Add("BaseRequest", j_BaseRequest["BaseRequest"]);
                    body4.Add("SyncKey", synckeys);
                    body4.Add("rr", JavaTimeSpan());
                    string Result4 = NetFramework.Util_WEB.OpenUrl(CheckUrl4
                      , "https://wx2.qq.com/", body4.ToString().Replace(Environment.NewLine, ""), "POST", cookie, Encoding.UTF8, false);

                    JObject Newmsg = JObject.Parse(Result4);


                    string AddMsgCount = Newmsg["AddMsgCount"].ToString();
                    if (AddMsgCount != "0")
                    {
                        foreach (var AddMsgList in (Newmsg["AddMsgList"] as JArray))
                        {
                            string FromUserNameTEMPID = AddMsgList["FromUserName"].ToString();
                            string ToUserNameTEMPID = AddMsgList["ToUserName"].ToString();

                            string Content = AddMsgList["Content"].ToString();
                            string msgTime = AddMsgList["CreateTime"].ToString();

                            if (Content != "")
                            {
                                #region "如果是自己发出的"
                                if (FromUserNameTEMPID == MyUserName)
                                {
                                    Boolean? LogicOk = false;
                                    var tocontacts = RunnerF.MemberSource.Select("User_ContactTEMPID='" + ToUserNameTEMPID + "'");
                                    if (tocontacts.Count() == 0)
                                    {
                                        continue;
                                    }
                                    string MyOutResult = Linq.DataLogic.WX_UserReplyLog_MySendCreate(Content, out LogicOk, tocontacts[0], GlobalParam.db);



                                    if (LogicOk == true)
                                    {
                                        GlobalParam.db.SubmitChanges();

                                    }//可以更新
                                    else if (LogicOk == false)
                                    {
                                        GlobalParam.db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, GlobalParam.db.WX_UserGameLog);

                                    }//不能更新回滚
                                    if (LogicOk != null)
                                    {
                                        if (@ToUserNameTEMPID.Contains("@@") == false && Content != "自动跟踪")
                                        {
                                            decimal? TotalPoint = GlobalParam.db.WX_UserChangeLog.Where(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == tocontacts[0].Field<string>("User_ContactID")).Sum(t => t.ChangePoint);
                                            SendWXContent(MyOutResult + ",剩余:"
                                                + (TotalPoint.HasValue ? TotalPoint.Value.ToString("N0") : ""), tocontacts[0].Field<string>("User_ContactTEMPID"));

                                        }


                                    }

                                    continue;
                                }
                                #endregion
                                var contacts = RunnerF.MemberSource.Select("User_ContactTEMPID='" + FromUserNameTEMPID + "'");
                                if (contacts.Count() == 0)
                                {
                                    continue;
                                }
                                DataRow userr = contacts.First();

                                if (Content.StartsWith("@"))
                                {
                                    Regex FindTmpUserID = new Regex(("@[0-9a-zA-Z]+"), RegexOptions.IgnoreCase);
                                    string FindSayUserID = FindTmpUserID.Match(Content).Value;
                                    // DataRow sayuserr = runnerf.MemberSource.Select("User_ContactTEMPID='" + FindSayUserID + "'").First();
                                }


                                DataRow newr = RunnerF.ReplySource.NewRow();
                                newr.SetField("Reply_Contact", userr.Field<string>("User_Contact"));
                                newr.SetField("Reply_ContactID", userr.Field<string>("User_ContactID"));
                                newr.SetField("Reply_ContactTEMPID", userr.Field<string>("User_ContactTEMPID"));
                                newr.SetField("Reply_ReceiveContent", Content);
                                newr.SetField("Reply_ReceiveTime", JavaTime(Convert.ToInt64(msgTime)));
                                RunnerF.ReplySource.Rows.Add(newr);
                                #region "检查是否启用自动跟踪"
                                Linq.WX_UserReply checkreply = GlobalParam.db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == userr.Field<string>("User_ContactID"));
                                if (checkreply.IsReply == true)
                                {
                                    //群不下单
                                    if (userr.Field<string>("User_ContactTEMPID").StartsWith("@@"))
                                    {
                                        continue;
                                    }
                                    NewWXContent(JavaTime(Convert.ToInt64(msgTime)), Content, userr, "微信");

                                }

                                #endregion
                            }//内容非空白
                        }//JSON消息循环
                    }//新消息数目不为0

                    synckeys = (Newmsg["SyncKey"] as JObject);
                    AsyncKey = "";
                    foreach (var keeyitem in synckeys["List"])
                    {
                        AsyncKey += keeyitem["Key"] + "_" + keeyitem["Val"] + "|";
                    }
                    AsyncKey = AsyncKey.Substring(0, AsyncKey.Length - 1);
                }//有新消息


                //                10、发送信息

                //这个比较简单，用POST方法，访问：https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxsendmsg

                //POST的还是json格式，类似这样：

                //{"Msg":{"Type":1,"Content":"测试信息","FromUserName":"XXXXXX","ToUserName":"XXXXXX","LocalID":"时间戳","ClientMsgId":"时间戳"},"BaseRequest":{"Uin":"XXXXXX","Sid":"XXXXXX","Skey":"XXXXXX","DeviceID":"XXXXXX"}}

                //这里的Content是信息内容，LocalID和ClientMsgId都用当前时间戳。
                #endregion

            }
            catch (Exception AnyError)
            {
                ShowError = AnyError;

            }
        Sleep:
            Thread.Sleep(10000);
            goto Repeat;


        }//KeepAliveDo
        private void DownloadResultThreadDo(Object ThreadID)
        {
        Repeat:
            try
            {
                Boolean SendImage = false;

                DownLoad163CaiPiao(ref SendImage, DateTime.Now,false);

                #region "有新的就通知,以及处理结果"
                if (SendImage == true)
                {


                    var users = GlobalParam.db.WX_UserReply.Where(t => t.IsReply == true && t.aspnet_UserID == GlobalParam.Key);
                    foreach (var item in users)
                    {
                        #region  多人同号不到ID跳过
                        #endregion
                        DataRow[] dr = RunnerF.MemberSource.Select("User_ContactID='" + item.WX_UserName + "'");
                        if (dr.Length == 0)
                        {
                            continue;
                        }
                        string TEMPUserName = dr[0].Field<string>("User_ContactTEMPID");
                        if (TEMPUserName.StartsWith("@@"))
                        {
                            SendWXImage(Application.StartupPath + "\\Data.jpg", TEMPUserName);
                            Thread.Sleep(1000);
                            //SendWXImage(Application.StartupPath + "\\Data2.jpg", TEMPUserName);
                            if (System.IO.File.Exists(Application.StartupPath + "\\Data3.txt"))
                            {
                                FileStream fs = new FileStream(Application.StartupPath + "\\Data3.txt", System.IO.FileMode.Open);
                                byte[] bs = new byte[fs.Length];
                                fs.Read(bs,0, bs.Length);
                                fs.Close();
                                fs.Dispose();
                                SendWXContent(Encoding.UTF8.GetString(bs), TEMPUserName);
                            }
                        }//向监听的群发送图片

                    }//设置为自动监听的用户
                    DealGameLogAndNotice();



                }//新开奖

                #endregion
            }
            catch (Exception)
            {

            }
            Thread.Sleep(5000);
            goto Repeat;
        }

        private void KeepUpdateContactDo(Object ThreadID);


        Exception ShowError = new Exception("");
        public string SendWXContent(string Content, string TempToUserID)
        {
            //10、发送信息

            //这个比较简单，用POST方法，访问：https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxsendmsg

            //POST的还是json格式，类似这样：

            //{"Msg":{"Type":1,"Content":"测试信息","FromUserName":"XXXXXX","ToUserName":"XXXXXX","LocalID":"时间戳","ClientMsgId":"时间戳"},"BaseRequest":{"Uin":"XXXXXX","Sid":"XXXXXX","Skey":"XXXXXX","DeviceID":"XXXXXX"}}
            //?sid=QfLp+Z+FePzvOFoG&r=1377482079876
            //这里的Content是信息内容，LocalID和ClientMsgId都用当前时间戳。
            string CheckUrl4 = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxsendmsg?sid=" + Sid + "&r_=" + JavaTimeSpan();
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
                     , "https://wx2.qq.com/", body4.ToString().Replace(Environment.NewLine, ""), "POST", cookie, Encoding.GetEncoding("UTF-8"), true);

            return Result4;

        }
        public string SendWXImage(string ImageFile, string TEMPUserName)
        {
            string UpLoadResult2 = NetFramework.Util_WEB.UploadWXImage(ImageFile, MyUserName, TEMPUserName, JavaTimeSpan(), cookie, j_BaseRequest);
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
            //Host: wx2.qq.com
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
            string CheckUrl2 = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxsendmsgimg?fun=async&f=json";
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

            string Result2 = NetFramework.Util_WEB.OpenUrl(CheckUrl2
              , "https://wx2.qq.com/", body2.ToString().Replace(Environment.NewLine, ""), "POST", cookie, Encoding.UTF8, false);

            return Result2;


        }

        public void DealGameLogAndNotice(bool IgoreDataSettingSend = false)
        {
            Linq.aspnet_UsersNewGameResultSend checkus = GlobalParam.db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key);

            if ((checkus != null && checkus.IsNewSend == true) || (IgoreDataSettingSend == true))
            {


                #region "发送余额"
                var noticeChangelist = GlobalParam.db.WX_UserGameLog.Where(t => t.Result_HaveProcess == false
                    && t.aspnet_UserID == GlobalParam.Key).Select(t => t.WX_UserName).Distinct();
                foreach (var notice_item in noticeChangelist)
                {

                    Int32 TotalChanges = Linq.DataLogic.WX_UserGameLog_Deal(GlobalParam.db, this, notice_item);
                    GlobalParam.db.SubmitChanges();
                    if (TotalChanges == 0)
                    {
                        continue;
                    }

                    string TEMPUserName = RunnerF.MemberSource.Select("User_ContactID='" + notice_item + "'").First().Field<string>("User_ContactTEMPID");
                    decimal? ReminderMoney = GlobalParam.db.WX_UserChangeLog.Where(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == notice_item).Sum(t => t.ChangePoint);
                    String ContentResult = SendWXContent(ReminderMoney.HasValue ? ReminderMoney.Value.ToString("N0") : "", TEMPUserName);
                    var updatechangelog = GlobalParam.db.WX_UserChangeLog.Where(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == notice_item && t.NeedNotice == false);
                    GlobalParam.db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, updatechangelog);
                    foreach (var updatechangeitem in updatechangelog)
                    {
                        updatechangeitem.HaveNotice = true;
                    }
                    GlobalParam.db.SubmitChanges();

                }

                #endregion
            }

        }
        public string NewWXContent(DateTime ReceiveTime, string ReceiveContent, DataRow userr, string SourceType)
        {
            Linq.WX_UserReplyLog log = GlobalParam.db.WX_UserReplyLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key
                                          && t.WX_UserName == userr.Field<string>("User_ContactID")
                                          && t.ReceiveTime == ReceiveTime
                                          );
            if (log == null)
            {
                Linq.WX_UserReplyLog newlogr = new Linq.WX_UserReplyLog();
                newlogr.aspnet_UserID = GlobalParam.Key;
                newlogr.WX_UserName = userr.Field<string>("User_ContactID");
                newlogr.ReceiveContent = ReceiveContent;
                newlogr.ReceiveTime = ReceiveTime;
                newlogr.SourceType = SourceType;

                newlogr.ReplyContent = "";
                newlogr.HaveDeal = false;
                GlobalParam.db.WX_UserReplyLog.InsertOnSubmit(newlogr);
                GlobalParam.db.SubmitChanges();
                Boolean? LogicOK = false;
                bool ShowBuy = false;
                string ReturnSend = Linq.DataLogic.WX_UserReplyLog_Create(newlogr, GlobalParam.db, RunnerF.MemberSource, out LogicOK, out ShowBuy);




                newlogr.ReplyContent = ReturnSend;
                newlogr.ReplyTime = DateTime.Now;
                newlogr.HaveDeal = false;
                if (LogicOK == true)
                {
                    GlobalParam.db.SubmitChanges();

                }
                else if (LogicOK == false)
                {
                    GlobalParam.db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, GlobalParam.db.WX_UserGameLog);
                }
                if (LogicOK != null&&ShowBuy==true)
                {


                    Linq.DataLogic.TotalResult tr = Linq.DataLogic.BuildResult(
                                    GlobalParam.db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                                        && t.Result_HaveProcess == false
                                        && t.WX_UserName==userr.Field<string>("User_ContactID")
                                        ).ToList()
                                   , RunnerF.MemberSource);
                    SendWXContent(ReturnSend + Environment.NewLine + tr.ToSlimString(), userr.Field<string>("User_ContactTEMPID"));
                }
                return ReturnSend;

            }
            else
            {
                return "下单记录已存在";
            }
        }//新消息



        private JObject WXInit()
        {
            cookie = new CookieCollection();
            MI_GameLogManulDealEnabled = false;
            string Result = NetFramework.Util_WEB.OpenUrl("https://login.weixin.qq.com/cgi-bin/mmwebwx-bin/login?uuid=" + _uuid + "&tip=" + _tip.ToString() + "&_=" + JavaTimeSpan()
                  , "", "", "GET", cookie);
            string Redirect = Result.Substring(Result.IndexOf("redirect_uri"));
            Redirect = Redirect.Substring(Redirect.IndexOf("\"") + 1);
            Redirect = Redirect.Substring(0, Redirect.Length - 2);
            string CheckUrl2 = Redirect;

            string Result2 = NetFramework.Util_WEB.OpenUrl(CheckUrl2 + "&fun=new&version=v2"
           , "", "", "GET", cookie, false);


            newridata.LoadXml(Result2);

            string pass_ticket = newridata.SelectSingleNode("error/pass_ticket").InnerText;
            Uin = newridata.SelectSingleNode("error/wxuin").InnerText;
            Sid = newridata.SelectSingleNode("error/wxsid").InnerText;
            Skey = newridata.SelectSingleNode("error/skey").InnerText;
            StatusMessage = "初始化";
            string CheckUrl3 = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxinit?r=" + JavaTimeSpan() + "&pass_ticket=" + pass_ticket;

            j_BaseRequest = new JObject();
            j_BaseRequest.Add("BaseRequest", "");

            JObject bcc = new JObject();
            bcc.Add("Uin", Uin);
            bcc.Add("Sid", Sid);
            bcc.Add("Skey", Skey);
            DeviceID = "e" + new Random().Next().ToString("000000000000000");
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
           , "https://wx2.qq.com/", j_BaseRequest.ToString().Replace(Environment.NewLine, "").Replace(" ", ""), "POST", cookie, false);


            InitResponse = JObject.Parse(Result3);

            synckeys = (InitResponse["SyncKey"] as JObject);
            AsyncKey = "";
            foreach (var keeyitem in (synckeys["List"] as JArray))
            {
                AsyncKey += keeyitem["Key"] + "_" + keeyitem["Val"] + "|";
            }
            AsyncKey = AsyncKey.Substring(0, AsyncKey.Length - 1);


            MyUserName = InitResponse["User"]["UserName"].ToString();


            // 6、获取好友列表

            //使用POST方法，访问：https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxgetcontact?r=时间戳

            //POST的内容为空。成功则以JSON格式返回所有联系人的信息。格式类似：
            string str_memb = NetFramework.Util_WEB.OpenUrl("https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxgetcontact?r=" + JavaTimeSpan()
        , "https://wx2.qq.com/", "", "POST", cookie);

            JObject Members = JObject.Parse(str_memb);


            RunnerF.Members = Members;

            MI_GameLogManulDealEnabled = true;

            Thread DownloadResultThread = new Thread(new ParameterizedThreadStart(DownloadResultThreadDo));
            DownloadResultThread.Start();
            

            return Members;
        }


        Dictionary<Guid, Boolean> KillThread = new Dictionary<Guid, bool>();

 
      

        public void DownLoad163CaiPiao(ref Boolean NewResult, DateTime SelectDate,bool ReDrawGdi)
        {
            #region 下载彩票结果
            //http://caipiao.163.com/award/cqssc/20180413.html

            
            Int32    LocalGameResultCount = GlobalParam.db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();
            


            DateTime Now = SelectDate;
            string URL = "http://caipiao.163.com/award/cqssc/";
            if ((Now.Hour == 0) && (Now.Minute <= 3))
            {
                URL += SelectDate.AddDays(-1).ToString("yyyyMMdd") + ".html";
            }
            else
            {
                URL += SelectDate.ToString("yyyyMMdd") + ".html";
            }
            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie163);
            Regex FindTableData = new Regex("<table width=\"100%\" border=\"0\" cellspacing=\"0\" class=\"awardList\">((?!</table>)[\\S\\s])+</table>", RegexOptions.IgnoreCase);
            string str_json = FindTableData.Match(Result).Value;
            //<td class="start" data-win-number="5 3 9 7 3" data-period="180404002">
            //<td class="award-winNum">
            Regex FindPeriod = new Regex("<td class=\"start\"((?!</td>)[\\S\\s])+</td>", RegexOptions.IgnoreCase);

            DataTable GDISource = new DataTable();
            GDISource.Columns.Add("期号");
            GDISource.Columns.Add("时间");
            GDISource.Columns.Add("开奖号码");
            GDISource.Columns.Add("和数");
            GDISource.Columns.Add("大小");
            GDISource.Columns.Add("单双");
            GDISource.Columns.Add("龙虎");
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


                    string str_win2 = str_Win;

                    str_win2 = str_win2.Replace(" ", "");

                    char[] numbs = str_win2.ToCharArray();
                    int NUM1 = Convert.ToInt32(numbs[0].ToString());
                    int NUM2 = Convert.ToInt32(numbs[1].ToString());
                    int NUM3 = Convert.ToInt32(numbs[2].ToString());
                    int NUM4 = Convert.ToInt32(numbs[3].ToString());
                    int NUM5 = Convert.ToInt32(numbs[4].ToString());

                    Int32 NumTotal = NUM1 + NUM2 + NUM3 + NUM4 + NUM5;


                    String BigSmall = "";
                    if (NumTotal == 23)
                    {
                        BigSmall = "和";
                    }
                    else if (NumTotal <= 22)
                    {
                        BigSmall = "小";
                    }
                    else
                    {
                        BigSmall = "大";
                    }

                    string SingleDouble = "";
                    if (NumTotal % 2 == 1)
                    {
                        SingleDouble = "单";
                    }
                    else
                    {
                        SingleDouble = "双";
                    }


                    string TigerDragon = "";
                    if (NUM1 > NUM5)
                    {
                        TigerDragon = "龙";
                    }
                    else if (NUM1 == NUM5)
                    {
                        TigerDragon = "合";
                    }
                    else
                    {
                        TigerDragon = "虎";
                    }

                    Linq.Game_ChongqingshishicaiPeriodMinute FindMinute = GlobalParam.db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.PeriodIndex == str_dataperiod.Substring(6, 3) && t.GameType == "重庆时时彩");


                    var GameResult = GlobalParam.db.Game_Result.SingleOrDefault(t => t.GameName == "重庆时时彩" && t.GamePeriod == str_dataperiod && t.aspnet_UserID == GlobalParam.Key);
                    if (GameResult == null)
                    {
                        Linq.Game_Result gr = new Linq.Game_Result();
                        gr.aspnet_UserID = GlobalParam.Key;
                        gr.GamePeriod = str_dataperiod;
                        gr.GameName = "重庆时时彩";
                        gr.GameResult = str_win2;
                        gr.NumTotal = NumTotal;
                        gr.BigSmall = BigSmall;
                        gr.SingleDouble = SingleDouble;
                        gr.DragonTiger = TigerDragon;
                        gr.GameTime = Convert.ToDateTime(
                           "20" + str_dataperiod.Substring(0, 2) + "-" + str_dataperiod.Substring(2, 2) + "-" + str_dataperiod.Substring(4, 2) + " "
                            + FindMinute.TimeMinute);
                        gr.aspnet_UserID = GlobalParam.Key;
                        gr.GamePrivatePeriod = Convert.ToDateTime(
                           "20" + str_dataperiod.Substring(0, 2) + "-" + str_dataperiod.Substring(2, 2) + "-" + str_dataperiod.Substring(4, 2) + " "
                           ).AddDays(Convert.ToDouble(FindMinute.Private_day)).ToString("yyyyMMdd") + FindMinute.Private_Period;

                        GlobalParam.db.Game_Result.InsertOnSubmit(gr);
                        GlobalParam.db.SubmitChanges();


                    }//插入数据库

                    if (GDISource.Select("期号='" + str_dataperiod + "'").Length == 0)
                    {
                        DataRow newr = GDISource.NewRow();
                        newr.SetField("期号", str_dataperiod);

                        newr.SetField("时间", FindMinute.TimeMinute);

                        newr.SetField("开奖号码", str_Win);
                        newr.SetField("和数", NumTotal.ToString());
                        newr.SetField("大小", BigSmall);
                        newr.SetField("单双", SingleDouble);
                        newr.SetField("龙虎", TigerDragon);

                        GDISource.Rows.Add(newr);

                    }


                    GDISource.NewRow();



                }//已开奖励
            }//每行处理

            Int32 AfterCheckCount = GlobalParam.db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();
            if (LocalGameResultCount != AfterCheckCount || ReDrawGdi==true)
            {
                NewResult = true;
                LocalGameResultCount = GlobalParam.db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.Key).Count();
                DateTime day = DateTime.Now;
                if (day.Hour < 10)
                {
                    day = day.AddDays(-1);
                }

                DrawGdi(day);
            }



            #endregion

        }
        private void DrawGdi(DateTime day)
        {


            DataTable PrivatePerios = NetFramework.Util_Sql.RunSqlDataTable("LocalSqlServer"
                , @"select GamePeriod as 期号,GameTime as 时间,GameResult as 开奖号码,NumTotal as 和数,BigSmall as 大小,SingleDouble as 单双,DragonTiger as 龙虎 from Game_Result where GamePrivatePeriod like '" + day.ToString("yyyyMMdd") + "%'");
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
                if (datapindex == 11)
                {
                    Datatextplain += Environment.NewLine;
                    datapindex = 1;
                }
            }//行循环
            if (System.IO.File.Exists(Application.StartupPath + "\\Data3.txt"))
            {
                System.IO.File.Delete(Application.StartupPath + "\\Data3.txt");
            }
            FileStream filetowrite = new FileStream(Application.StartupPath + "\\Data3.txt", FileMode.OpenOrCreate);
            byte[] Result = Encoding.UTF8.GetBytes(Datatextplain);
            filetowrite.Write(Result, 0, Result.Length);
            filetowrite.Flush();
            filetowrite.Close();
            filetowrite.Dispose();
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

            if (System.IO.File.Exists(Application.StartupPath + "\\Data2.jpg"))
            {
                System.IO.File.Delete(Application.StartupPath + "\\Data2.jpg");
            }
            img_tiger.Dispose();
            img_dragon.Dispose();
            img_ok.Dispose();

            img2.Save(Application.StartupPath + "\\Data2.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            img2.Dispose();
            g2.Dispose();
            #endregion

            #region 画表格
            Bitmap img = new Bitmap(472, 780);
            Graphics g = Graphics.FromImage(img);


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
                    Brush br = new SolidBrush(Color.Red);
                    g.DrawString("期号", sf, br, new PointF(MarginLeft, MarginTop));
                    g.DrawString("时间", sf, br, new PointF(MarginLeft + 50, MarginTop));
                    g.DrawString("开奖号码", sf, br, new PointF(MarginLeft + 145, MarginTop));
                    g.DrawString("和数", sf, br, new PointF(MarginLeft + 275, MarginTop));
                    g.DrawString("大小", sf, br, new PointF(MarginLeft + 325, MarginTop));
                    g.DrawString("单双", sf, br, new PointF(MarginLeft + 375, MarginTop));
                    g.DrawString("龙虎", sf, br, new PointF(MarginLeft + 420, MarginTop));
                }
                else if (i <= 25 && i >= 1)
                {
                    if (dtCopy.Rows.Count - i < 0)
                    {
                        continue;
                    }
                    DataRow currow = dtCopy.Rows[dtCopy.Rows.Count - i];
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
                }



            }//每行画图
            if (System.IO.File.Exists(Application.StartupPath + "\\Data.jpg"))
            {
                System.IO.File.Delete(Application.StartupPath + "\\Data.jpg");
            }
            img.Save(Application.StartupPath + "\\Data.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            img.Dispose();
            g.Dispose();

            #endregion

        }


        private System.Net.CookieCollection cookie163 = new CookieCollection();


        public static string JavaTimeSpan()
        {

            Int64 result = 0;
            DateTime startdate = new DateTime(1970, 1, 1, 8, 0, 0);
            TimeSpan seconds = DateTime.Now - startdate;
            result = Convert.ToInt64(seconds.TotalSeconds);
            return result.ToString();

        }
        public static DateTime JavaTime(Int64 time)
        {

            DateTime startdate = new DateTime(1970, 1, 1, 8, 0, 0);
            return startdate.AddSeconds(time);
        }

        private void btn_resfresh_Click(object sender, EventArgs e)
        {

            HaveScan = false;

            KillThread.Add(WaitScanTHreadID, true);
            KillThread.Add(KeepAliveThreadID, true);

            WaitScanTHreadID = Guid.NewGuid();
            KeepAliveThreadID = Guid.NewGuid();


            
            Thread StartThread = new Thread(new ThreadStart(StartThreadDo));
            StartThread.Start();


        }

        static string StatusMessage = "扫描微信登陆";
        static bool ReloadWX = false;

        private void tm_refresh_Tick(object sender, EventArgs e)
        {
            lbl_msg.Text = StatusMessage + Environment.NewLine;
            PicBarCode.Visible = !HaveScan;


            PicBarCode.ImageLocation = ImgUrl;
            SI_url.Text = NetFramework.Util_WEB.CurrentUrl;
            SI_url.ToolTipText = SI_url.Text;

            SI_ShowError.Text = ShowError.Message + Environment.NewLine + ShowError.StackTrace;
            SI_ShowError.ToolTipText = SI_ShowError.Text;

            MI_GameLogManulDeal.Enabled = MI_GameLogManulDealEnabled;

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




    }
}
