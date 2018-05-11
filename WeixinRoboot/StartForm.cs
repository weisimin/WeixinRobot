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

        public string _uuid = "";
        public System.Net.CookieCollection cookie = new CookieCollection();


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
            this.Invoke(new Action(() => { PicBarCode.ImageLocation = "https://login.weixin.qq.com/qrcode/" + UUID + "?t=" + new Random().Next().ToString(); }));

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

            this.Text = "会员号:" + GlobalParam.UserName;
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
        string pass_ticket = "";
        public JObject j_BaseRequest = null;
        JObject synckeys = null;



        public RunnerForm RunnerF = new RunnerForm();





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
            this.Invoke(new Action(() => { lbl_msg.Text = "等待扫码"; }));
            WaitScanTHreadID = Guid.NewGuid();
            Thread WaitScan = new Thread(new ParameterizedThreadStart(WaitScanThreadDo));

            WaitScan.Start(WaitScanTHreadID);
        }
        Guid WaitScanTHreadID = Guid.NewGuid();


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

                    //要使用POST方法，访问地址：https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxinit?r=时间戳&lang=ch_ZN&pass_ticket=XXXXXX

                    //其中，时间戳不用解释，pass_ticket是我们在上面获取的一长串字符。

                    //POST的内容是个json串，{"BaseRequest":{"Uin":"XXXXXXXX","Sid":"XXXXXXXX","Skey":XXXXXXXXXXXXX","DeviceID":"e123456789012345"}}

                    //uin、sid、skey分别对应上面步骤4获取的字符串，DeviceID是e后面跟着一个15字节的随机数。

                    //程序里面要注意使用UTF8编码方式。
                    JObject Members = WXInit();

                    RunnerF.Invoke(new Action(() => { RunnerF.Members = Members; }));

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

                    Thread DownLoad163 = new Thread(new ParameterizedThreadStart(DownLoad163ThreadDo));
                    DownLoad163.Start(Download163ThreadID);

                    this.Invoke(new Action(() => { MI_GameLogManulDeal.Enabled = true; }));
                    while ((KillThread.ContainsKey((Guid)ThreadID)) == false)
                    {
                        Application.DoEvents();
                        KeepAlieveDo();
                    }

                    return;


                }//200 code

                if (_tip != 0)
                {
                    this.Invoke(new Action(() => { lbl_msg.Text = "等待扫码"; }));
                    _tip += 1;
                }
                goto ReCheckURI;
            }
            catch (Exception AnyError)
            {
                MessageBox.Show(AnyError.Message + Environment.NewLine + AnyError.StackTrace);
            }




        }

        private void KeepAlieveDo()
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");


            try
            {
                #region "微信监听"
                this.Invoke(new Action(() => { lbl_msg.Text = "机器人监听中"; }));


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
                if (Check["retcode"].ToString() != "0")
                {

                    //MessageBox.Show("微信已在别的地方登录");
                    //ReloadWX = true;
                    //return;
                    WXInit();

                    return;
                }

                if ((Result3.Contains("selector:\"0\"")) == false)
                {

                    this.Invoke(new Action(() => { lbl_msg.Text = Result3; }));


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

                            Linq.aspnet_UsersNewGameResultSend mysetting = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key);

                            string FromUserNameTEMPID = AddMsgList["FromUserName"].ToString();
                            string ToUserNameTEMPID = AddMsgList["ToUserName"].ToString();

                            string Content = AddMsgList["Content"].ToString();
                            string msgTime = AddMsgList["CreateTime"].ToString();
                            string msgType = AddMsgList["MsgType"].ToString();

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
                                        DataRow[] ToContact = RunnerF.MemberSource.Select("User_ContactID='" + recitem.WX_UserName + "'");
                                        if (ToContact.Length != 0)
                                        {
                                            SendWXContent(FromContactName + ":" + NetFramework.Util_WEB.CleanHtml(Content), ToContact[0].Field<string>("User_ContactTEMPID"));
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
                                    RepeatGetMembers(Skey, pass_ticket);
                                }
                                if (FromUserNameTEMPID == MyUserName)
                                {

                                    var tocontacts = RunnerF.MemberSource.Select("User_ContactTEMPID='" + ToUserNameTEMPID + "'");
                                    if (tocontacts.Count() == 0)
                                    {
                                        continue;
                                    }
                                    string MyOutResult = "";
                                    try
                                    {
                                        MyOutResult = Linq.DataLogic.WX_UserReplyLog_MySendCreate(Content, tocontacts[0]);

                                    }
                                    catch (Exception mysenderror)
                                    {

                                        Console.Write(mysenderror.Message);
                                        Console.Write(mysenderror.StackTrace);
                                    }



                                    if (@ToUserNameTEMPID.Contains("@@") == false && tocontacts[0].Field<Boolean?>("User_IsReply") == true)
                                    {
                                        decimal? TotalPoint = Linq.DataLogic.WXUserChangeLog_GetRemainder(tocontacts[0].Field<string>("User_ContactID"));
                                        SendWXContent(MyOutResult, tocontacts[0].Field<string>("User_ContactTEMPID"));
                                        string OrderManul = NewWXContent(JavaSecondTime(Convert.ToInt64(msgTime)), Content, tocontacts[0], "人工");
                                        SendWXContent(OrderManul, tocontacts[0].Field<string>("User_ContactTEMPID"));

                                    }




                                    //string MyOutResult = "";

                                    //db.Logic_WX_UserReplyLog_MySendCreate(Content, tocontacts[0].Field<string>("User_ContactID"), GlobalParam.Key, JavaTime(Convert.ToInt64(msgTime)), ref MyOutResult);
                                    //if (MyOutResult != "")
                                    //{
                                    //    SendWXContent(MyOutResult, tocontacts[0].Field<string>("User_ContactTEMPID"));

                                    //}


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

                                RunnerF.Invoke(new Action(() =>
                                {

                                    DataRow newr = RunnerF.ReplySource.NewRow();
                                    newr.SetField("Reply_Contact", userr.Field<string>("User_Contact"));
                                    newr.SetField("Reply_ContactID", userr.Field<string>("User_ContactID"));
                                    newr.SetField("Reply_ContactTEMPID", userr.Field<string>("User_ContactTEMPID"));
                                    newr.SetField("Reply_ReceiveContent", Content);
                                    newr.SetField("Reply_ReceiveTime", JavaSecondTime(Convert.ToInt64(msgTime)));
                                    RunnerF.ReplySource.Rows.Add(newr);
                                }));





                                #region "检查是否启用自动跟踪"

                                Linq.WX_UserReply checkreply = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == userr.Field<string>("User_ContactID"));
                                if (checkreply.IsReply == true)
                                {
                                    //群不下单
                                    if (userr.Field<string>("User_ContactTEMPID").StartsWith("@@"))
                                    {
                                        continue;
                                    }
                                    //授权不处理订单
                                    if (mysetting.IsReceiveOrder != true)
                                    {
                                        continue;
                                    }
                                    String OutMessage = "";
                                    try
                                    {
                                        OutMessage = NewWXContent(JavaSecondTime(Convert.ToInt64(msgTime)), Content, userr, "微信");
                                    }
                                    catch (Exception mysenderror)
                                    {

                                        Console.Write(mysenderror.Message);
                                        Console.Write(mysenderror.StackTrace);
                                    }
                                    if (OutMessage != "")
                                    {

                                        SendWXContent(OutMessage, userr.Field<string>("User_ContactTEMPID"));
                                    }
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
                Console.Write(AnyError.Message);
                Console.Write(AnyError.StackTrace);

            }

            Thread.Sleep(5000);



        }//KeepAliveDo









        public string SendWXContent(string Content, string TempToUserID)
        {
            Thread.Sleep(1500);
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
            Thread.Sleep(1500);
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
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            Linq.aspnet_UsersNewGameResultSend checkus = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key);

            if ((checkus != null && checkus.IsNewSend == true) || (IgoreDataSettingSend == true))
            {


                #region "发送余额"
                var noticeChangelist = db.WX_UserGameLog.Where(t => t.Result_HaveProcess == false
                    && t.aspnet_UserID == GlobalParam.Key).Select(t => t.WX_UserName).Distinct();
                foreach (var notice_item in noticeChangelist)
                {

                    Int32 TotalChanges = Linq.DataLogic.WX_UserGameLog_Deal(this, notice_item);
                    db.SubmitChanges();
                    if (TotalChanges == 0)
                    {
                        continue;
                    }

                    string TEMPUserName = RunnerF.MemberSource.Select("User_ContactID='" + notice_item + "'").First().Field<string>("User_ContactTEMPID");
                    decimal? ReminderMoney = Linq.DataLogic.WXUserChangeLog_GetRemainder(notice_item);
                    String ContentResult = SendWXContent("余" + (ReminderMoney.HasValue ? ReminderMoney.Value.ToString("N0") : ""), TEMPUserName);
                    var updatechangelog = db.WX_UserChangeLog.Where(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == notice_item && t.NeedNotice == false);
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
                //    DataRow[] user = RunnerF.MemberSource.Select("User_ContactID='" + item.WX_UserName + "'");
                //    if (user.Length == 0)
                //    {
                //        continue;
                //    }
                //    SendWXContent((item.Remainder.HasValue ? item.Remainder.Value.ToString("N0") : "0"), user[0].Field<string>("User_ContactID"));
                //}

                #endregion
            }

        }
        public string NewWXContent(DateTime ReceiveTime, string ReceiveContent, DataRow userr, string SourceType)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            Linq.WX_UserReplyLog log = db.WX_UserReplyLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key
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
                db.WX_UserReplyLog.InsertOnSubmit(newlogr);
                db.SubmitChanges();


                string ReturnSend = Linq.DataLogic.WX_UserReplyLog_Create(newlogr, RunnerF.MemberSource);

                newlogr.ReplyContent = ReturnSend;
                newlogr.ReplyTime = DateTime.Now;
                newlogr.HaveDeal = false;
                db.SubmitChanges();


                return ReturnSend;

            }
            else
            {
                return "下单记录已存在";
            }
            //string Message = "";
            //Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            //db.Logic_WX_UserReplyLog_Create(ReceiveContent, userr.Field<string>("User_ContactID"), GlobalParam.Key, ReceiveTime, ref Message, SourceType);

            //return Message;

        }//新消息

        Int32 MaxInitCount = 0;
        private JObject WXInit()
        {
            MaxInitCount += 1;
            if (MaxInitCount > 5)
            {
                MessageBox.Show("无法加载微信联系人");
                Environment.Exit(0);
            }
            cookie = new CookieCollection();

            string Result = NetFramework.Util_WEB.OpenUrl("https://login.weixin.qq.com/cgi-bin/mmwebwx-bin/login?uuid=" + _uuid + "&tip=" + _tip.ToString() + "&_=" + JavaTimeSpan()
                  , "", "", "GET", cookie);
            string Redirect = Result.Substring(Result.IndexOf("redirect_uri"));
            Redirect = Redirect.Substring(Redirect.IndexOf("\"") + 1);
            Redirect = Redirect.Substring(0, Redirect.Length - 2);
            string CheckUrl2 = Redirect;

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



            JObject Members = RepeatGetMembers(Skey, pass_ticket); ;
            //Thread KeepUpdateContactThread = new Thread(new ParameterizedThreadStart(KeepUpdateContactThreadDo));
            //KeepUpdateContactThread.Start(new object[]{ KeepUpdateContactThreadID,Skey,pass_ticket});
            return Members;
        }
        private JObject RepeatGetMembers(string Skey, string pass_ticket)
        {
            try
            {
                // 6、获取好友列表

                //使用POST方法，访问：https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxgetcontact?r=时间戳

                //POST的内容为空。成功则以JSON格式返回所有联系人的信息。格式类似：
                string str_memb = NetFramework.Util_WEB.OpenUrl("https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxgetcontact?r=" + JavaTimeSpan() + "&skey=" + HttpUtility.UrlDecode(Skey) + "&pass_ticket=" + pass_ticket
            , "https://wx2.qq.com/", "", "POST", cookie);

                JObject Members = JObject.Parse(str_memb);
                RunnerF.Invoke(new Action(() => { RunnerF.Members = Members; }));
                return Members;
            }
            catch (Exception AnyError)
            {
                Console.Write(AnyError.Message);
                Console.Write(AnyError.StackTrace);
                return JObject.Parse("{MemberList{}}");
            }

        }

        private JObject RepeatGetMembers_bug()
        {
            string CheckUrl3 = "";
            string str_memb = NetFramework.Util_WEB.OpenUrl(CheckUrl3
           , "https://wx2.qq.com/", j_BaseRequest.ToString().Replace(Environment.NewLine, "").Replace(" ", ""), "POST", cookie, false);
            JObject Members = JObject.Parse(str_memb);
            RunnerF.Invoke(new Action(() => { RunnerF.Members = Members; }));
            return Members;
        }
        Dictionary<Guid, Boolean> KillThread = new Dictionary<Guid, bool>();


        Guid Download163ThreadID = Guid.NewGuid();
        private void DownLoad163ThreadDo(object ThreadID)
        {
            while (KillThread.ContainsKey((Guid)ThreadID) == false)
            {
                DownloadResult();
                System.Threading.Thread.Sleep(5000);
            }
        }
        private void DownloadResult()
        {
            try
            {
                Boolean SendImage = false;
                try
                {
                    DownLoad163CaiPiaoV4(ref SendImage, DateTime.Now, false);

                }
                catch (Exception AnyError) { Console.Write(AnyError.Message); Console.Write(AnyError.StackTrace); }
                try
                {
                    DownLoad163CaiPiaoV3(ref SendImage, DateTime.Now, false);
                }
                catch (Exception AnyError) { Console.Write(AnyError.Message); Console.Write(AnyError.StackTrace); }
                try
                {
                    DownLoad163CaiPiaoV3(ref SendImage, DateTime.Now.AddDays(-1), false);
                }
                catch (Exception AnyError) { Console.Write(AnyError.Message); Console.Write(AnyError.StackTrace); }
                try
                {
                    DownLoad163CaiPiaoV2(ref SendImage, DateTime.Now, false);
                }
                catch (Exception AnyError) { Console.Write(AnyError.Message); Console.Write(AnyError.StackTrace); }

                try
                {
                    DownLoad163CaiPiaoV2(ref SendImage, DateTime.Now.AddDays(-1), false);
                }
                catch (Exception AnyError) { Console.Write(AnyError.Message); Console.Write(AnyError.StackTrace); }

                try
                {
                    DownLoad163CaiPiao(ref SendImage, DateTime.Now, false);
                }
                catch (Exception AnyError) { Console.Write(AnyError.Message); Console.Write(AnyError.StackTrace); }
                try
                {
                    DownLoad163CaiPiao(ref SendImage, DateTime.Now.AddDays(-1), false);
                }
                catch (Exception AnyError) { Console.Write(AnyError.Message); Console.Write(AnyError.StackTrace); }
              
                DealGameLogAndNotice();

                if (SendImage == true)
                {
                    SendChongqingResult();
                }
            }
            catch (Exception AnyError)
            {
                Console.Write(AnyError.Message);
                Console.Write(AnyError.StackTrace);
            }

        }

        public void SendChongqingResult()
        {
            #region "有新的就通知,以及处理结果"

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");


            var users = db.WX_UserReply.Where(t => t.IsReply == true && t.aspnet_UserID == GlobalParam.Key);
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
                Linq.aspnet_UsersNewGameResultSend myset = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key);
                if (!myset.IsSendPIC == true)
                {
                    continue;
                }
                if (TEMPUserName.StartsWith("@@"))
                {
                    SendWXImage(Application.StartupPath + "\\Data.jpg", TEMPUserName);
                    Thread.Sleep(1000);
                    //SendWXImage(Application.StartupPath + "\\Data2.jpg", TEMPUserName);
                    if (System.IO.File.Exists(Application.StartupPath + "\\Data3.txt"))
                    {
                        FileStream fs = new FileStream(Application.StartupPath + "\\Data3.txt", System.IO.FileMode.Open);
                        byte[] bs = new byte[fs.Length];
                        fs.Read(bs, 0, bs.Length);
                        fs.Close();
                        fs.Dispose();
                        SendWXContent(Encoding.UTF8.GetString(bs), TEMPUserName);
                    }
                }//向监听的群发送图片

            }//设置为自动监听的用户
          


            #endregion

        }



        public void DownLoad163CaiPiao(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi)
        {
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


                    Linq.DataLogic.NewGameResult(str_Win, str_dataperiod);



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


        public void DownLoad163CaiPiaoV2(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi)
        {
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

                Linq.DataLogic.NewGameResult(str_Win, str_dataperiod);



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

        public void DownLoad163CaiPiaoV3(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi)
        {
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
                    Linq.DataLogic.NewGameResult(str_Win, str_dataperiod.Substring(2));



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


        public void DownLoad163CaiPiaoV4(ref Boolean NewResult, DateTime SelectDate, bool ReDrawGdi)
        {
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
                str_Win = str_Win.Substring(0, 1) + " " + str_Win.Substring(1, 1) + " " + str_Win.Substring(2, 1) + " " + str_Win.Substring(3, 1) + " " + str_Win.Substring(4, 1) ;

                Linq.DataLogic.NewGameResult(str_Win, str_dataperiod);




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


        public void DrawGdi(DateTime Localday)
        {


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

            double result = 0;
            DateTime startdate = new DateTime(1970, 1, 1, 8, 0, 0);
            TimeSpan seconds = DateTime.Now - startdate;
            result = Math.Round(seconds.TotalMilliseconds, 0);
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
            KillThread.Add(WaitScanTHreadID, true);
            KillThread.Add(Download163ThreadID, true);


            WaitScanTHreadID = Guid.NewGuid();
            Download163ThreadID = Guid.NewGuid();


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




    }
}
