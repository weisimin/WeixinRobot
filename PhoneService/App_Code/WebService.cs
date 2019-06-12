using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Xml;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Net;
using System.IO;
using System.IO.Compression;
/// <summary>
/// WebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://13828081978.zicp.vip/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
        Context.Response.ContentType = "text/html;cahrset=UTF-8";//这句话有二层作用
    }


    [WebMethod]
    public string UserLogIn(string UserName, string Password)
    {

        bool r = Membership.ValidateUser(UserName, Password);
        MembershipUser msr = Membership.GetUser(UserName);
        if (r == true)
        {
            FormsAuthentication.SetAuthCookie(UserName, true);
            string Cookie = FormsAuthentication.GetAuthCookie(UserName, true).Value;

            return (msr.ProviderUserKey.ToString());
        }
        else if (msr == null)
        {
            return ("用户不存在");
        }
        else if (msr.IsLockedOut)
        {
            return ("密码错误次数太多,已停用");
        }
        else
        {
            return ("密码错误");
        }


    }

    [WebMethod]
    public string GetSetting(string saspnetUserid)
    {
        Guid aspnetUserid = Guid.Parse(saspnetUserid);
        dbDataContext db = new dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        //db.ObjectTrackingEnabled = false;
        aspnet_UsersNewGameResultSend save_sets = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == aspnetUserid);
        if (save_sets == null)
        {

            save_sets = new aspnet_UsersNewGameResultSend();
            save_sets.aspnet_UserID = aspnetUserid;
            db.aspnet_UsersNewGameResultSend.InsertOnSubmit(save_sets);
            db.SubmitChanges();
        }
        return (JsonConvert.SerializeObject(save_sets));


    }
    [WebMethod]
    public string SaveSetting(string UserName, string Password, string jaspnet_UsersNewGameResultSend)
    {
        //MembershipUser msr = Membership.GetUser(UserName);
        dbDataContext db = new dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        // db.ObjectTrackingEnabled = false;
        aspnet_UsersNewGameResultSend tins_sets = (aspnet_UsersNewGameResultSend)JsonConvert.DeserializeObject(jaspnet_UsersNewGameResultSend, typeof(aspnet_UsersNewGameResultSend));

        aspnet_UsersNewGameResultSend save_sets = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == tins_sets.aspnet_UserID);
        if (save_sets == null)
        {

            save_sets = new aspnet_UsersNewGameResultSend();
            save_sets.aspnet_UserID = tins_sets.aspnet_UserID;
            db.aspnet_UsersNewGameResultSend.InsertOnSubmit(save_sets);
        }
        save_sets.ActiveCode = tins_sets.ActiveCode;

        save_sets.IsNewSend = tins_sets.IsNewSend;

        save_sets.ActiveCode = tins_sets.ActiveCode;

        save_sets.IsBlock = tins_sets.IsBlock;

        save_sets.IsSendPIC = tins_sets.IsSendPIC;

        save_sets.IsReceiveOrder = tins_sets.IsReceiveOrder;

        save_sets.MaxPlayerCount = tins_sets.MaxPlayerCount;

        save_sets.bossaspnet_UserID = tins_sets.bossaspnet_UserID;

        save_sets.SendImageEnd = tins_sets.SendImageEnd;

        save_sets.SendImageStart = tins_sets.SendImageStart;

        save_sets.SendImageEnd2 = tins_sets.SendImageEnd2;

        save_sets.SendImageEnd3 = tins_sets.SendImageEnd3;

        save_sets.SendImageEnd4 = tins_sets.SendImageEnd4;

        save_sets.SendImageStart2 = tins_sets.SendImageStart2;

        save_sets.SendImageStart3 = tins_sets.SendImageStart3;

        save_sets.SendImageStart4 = tins_sets.SendImageStart4;

        save_sets.ImageEndText = tins_sets.ImageEndText;

        save_sets.ImageTopText = tins_sets.ImageTopText;

        save_sets.BlockEndHour = tins_sets.BlockEndHour;

        save_sets.BlockEndMinute = tins_sets.BlockEndMinute;

        save_sets.BlockStartHour = tins_sets.BlockStartHour;

        save_sets.BlockStartMinute = tins_sets.BlockStartMinute;

        save_sets.LeiDianPath = tins_sets.LeiDianPath;

        save_sets.NoxPath = tins_sets.NoxPath;

        save_sets.LeiDianSharePath = tins_sets.LeiDianSharePath;

        save_sets.NoxSharePath = tins_sets.NoxSharePath;

        save_sets.AdbLeidianMode = tins_sets.AdbLeidianMode;

        save_sets.AdbNoxMode = tins_sets.AdbNoxMode;

        save_sets.TwoTreeNotSingle = tins_sets.TwoTreeNotSingle;

        save_sets.XinJiangMode = tins_sets.XinJiangMode;

        save_sets.TengXunShiFenMode = tins_sets.TengXunShiFenMode;

        save_sets.FuliRatio = tins_sets.FuliRatio;

        save_sets.LiuShuiRatio = tins_sets.LiuShuiRatio;

        save_sets.Thread_AoZhouCai = tins_sets.Thread_AoZhouCai;

        save_sets.Thread_ChongQingShiShiCai = tins_sets.Thread_ChongQingShiShiCai;

        save_sets.Thread_TengXunShiFen = tins_sets.Thread_TengXunShiFen;

        save_sets.Thread_TengXunWuFen = tins_sets.Thread_TengXunWuFen;

        save_sets.Thread_VRChongqing = tins_sets.Thread_VRChongqing;

        save_sets.Thread_WuFen = tins_sets.Thread_WuFen;

        save_sets.Thread_XinJiangShiShiCai = tins_sets.Thread_XinJiangShiShiCai;

        save_sets.Thread_TengXunShiFenXin = tins_sets.Thread_TengXunShiFenXin;

        save_sets.Thread_TengXunWuFenXin = tins_sets.Thread_TengXunWuFenXin;

        save_sets.Thread_HeNeiWuFen = tins_sets.Thread_HeNeiWuFen;

        save_sets.OpenMode = tins_sets.OpenMode;

        save_sets.SuperUser = tins_sets.SuperUser;
        try
        {
            db.SubmitChanges();
            return ("保存成功");

        }
        catch (Exception anyerror)
        {

            return ("保存失败" + anyerror.Message);

        }


    }

    [WebMethod]
    public List<Guid> GetBossUsers(string bossaspnetuserid)
    {
        dbDataContext db = new dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        // db.ObjectTrackingEnabled = false;
        List<Guid> takeusers = ((from ds in db.aspnet_UsersNewGameResultSend
                                 where ds.bossaspnet_UserID == Guid.Parse(bossaspnetuserid)

                                 select ds.aspnet_UserID).Distinct()

                                              ).ToList();
        return takeusers;

    }

    [WebMethod]
    public string GetUserToken(string UserName, string Password)
    {
        bool r = Membership.ValidateUser(UserName, Password);
        if (r == false)
        {
            return "账号密码错误";
        }
        else
        {
            MembershipUser msr = Membership.GetUser(UserName);
            FormsAuthentication.SetAuthCookie(UserName, true);
            string Cookie = FormsAuthentication.GetAuthCookie(UserName, true).Value;

            return Cookie;

        }

    }
    [WebMethod]
    public void ChangePassword(Guid userid, String NewPassord)
    {

        MembershipUser user = Membership.GetUser(userid);
        string tmpPassword = user.ResetPassword();
        user.ChangePassword(tmpPassword, NewPassord);
    }
    [WebMethod]
    public string GetUserInfo(Guid UserID)
    {

        MembershipUser usr = System.Web.Security.Membership.GetUser(UserID);
        return JsonConvert.SerializeObject(usr);
    }
    [WebMethod]
    public string GetTemplateRatios()
    {
        Guid CopySourceID = (Guid)System.Web.Security.Membership.GetUser("sysadmin").ProviderUserKey;
        dbDataContext db = new dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        var CopyRatio = db.Game_BasicRatio.Where(t => t.aspnet_UserID == CopySourceID);
        return JsonConvert.SerializeObject(CopyRatio);
    }
    [WebMethod]
    public string GetTemplateBonus()
    {
        Guid CopySourceID = (Guid)System.Web.Security.Membership.GetUser("sysadmin").ProviderUserKey;
        dbDataContext db = new dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        var CopyRatio = db.WX_BounsConfig.Where(t => t.aspnet_UserID == CopySourceID);
        return JsonConvert.SerializeObject(CopyRatio);
    }
    public static string CleanHtml(string strHtml)
    {
        if (string.IsNullOrEmpty(strHtml)) return strHtml;
        //删除脚本
        //Regex.Replace(strHtml, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase)
        strHtml = Regex.Replace(strHtml, "(<script(.+?)</script>)|(<style(.+?)</style>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        //删除标签
        var r = new Regex(@"</?[^>]*>", RegexOptions.IgnoreCase);
        Match m;
        for (m = r.Match(strHtml); m.Success; m = m.NextMatch())
        {
            strHtml = strHtml.Replace(m.Groups[0].ToString(), "");
        }
        return strHtml.Trim().Replace("&nbsp;", "");
    }
    [WebMethod]
    public string SetMembers(String Members, Guid UserKey, string _GameMode)
    {
        DateTime StartTime = DateTime.Now;


        DataTable MemberSource = new DataTable();
        MemberSource.Columns.Add("User_Contact");
        MemberSource.Columns.Add("User_ContactType");
        MemberSource.Columns.Add("User_ContactID");
        MemberSource.Columns.Add("User_ContactTEMPID");
        MemberSource.Columns.Add("User_IsReply", typeof(Boolean));
        MemberSource.Columns.Add("User_IsSendPic", typeof(Boolean));
        MemberSource.Columns.Add("User_IsReceiveTransfer", typeof(Boolean));
        MemberSource.Columns.Add("User_IsCaculateFuli", typeof(Boolean));
        MemberSource.Columns.Add("User_SourceType");
        MemberSource.Columns.Add("User_IsBoss", typeof(Boolean));

        MemberSource.Columns.Add("User_IsBallPIC", typeof(Boolean));
        MemberSource.Columns.Add("User_ISSendCard", typeof(Boolean));
        MemberSource.Columns.Add("User_IsAdmin", typeof(Boolean));

        MemberSource.Columns.Add("User_FiveMinuteMode", typeof(Boolean));
        MemberSource.Columns.Add("User_HkMode", typeof(Boolean));
        MemberSource.Columns.Add("User_AozcMode", typeof(Boolean));

        MemberSource.Columns.Add("User_ChongqingMode", typeof(Boolean));


        MemberSource.Columns.Add("User_TengXunShiFen", typeof(Boolean));
        MemberSource.Columns.Add("User_TengXunWuFen", typeof(Boolean));
        MemberSource.Columns.Add("User_HeNeiWuFen", typeof(Boolean));


        MemberSource.Columns.Add("User_TengXunShiFenXin", typeof(Boolean));
        MemberSource.Columns.Add("User_TengXunWuFenXin", typeof(Boolean));

        MemberSource.Columns.Add("User_XinJiangShiShiCai", typeof(Boolean));

        MemberSource.Columns.Add("User_VR", typeof(Boolean));

        DataColumn[] dcs = { MemberSource.Columns["User_ContactTEMPID"], MemberSource.Columns["User_SourceType"] };
        MemberSource.PrimaryKey = dcs;


        DateTime EachStart = DateTime.Now;
        JObject _Members = JObject.Parse(Members);
        dbDataContext db = new dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
        //db.ObjectTrackingEnabled = false;
        //this.Invoke(new Action(() => { BS_Contact.DataSource = null; }));
        foreach (var item in (_Members["MemberList"]) as JArray)
        {
            EachStart = DateTime.Now;
            string UserNametempID = "";
            string NickName = "";
            string RemarkName = "";
            string HeadImgUrl = "";


            UserNametempID = (item["UserName"] as JValue).Value.ToString();
            NickName = (item["NickName"] as JValue).Value.ToString();
            RemarkName = (item["RemarkName"] as JValue).Value.ToString();
            HeadImgUrl = (item["HeadImgUrl"] as JValue).Value.ToString();

            //NetFramework.Console.WriteLine("更新联系人" + NickName);
            //Application.DoEvents();

            System.Text.RegularExpressions.Regex FindSeq = new System.Text.RegularExpressions.Regex("seq=([0-9])+");

            string Seq = FindSeq.Match(HeadImgUrl).Value;
            //Seq = Seq.Substring(Seq.IndexOf("=") + 1);

            Seq = RemarkName == "" ? CleanHtml(NickName) : RemarkName;
            //if (Seq.Contains("-"))
            //{
            //    string[] Names = Seq.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            //    Seq = Names[Names.Length-1];
            //}
            WX_UserReply usrc = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == UserKey && t.WX_UserName == Seq && t.WX_SourceType == "微");
            if (usrc == null)
            {
                WX_UserReply newusrc = new WX_UserReply();
                newusrc.aspnet_UserID = UserKey;
                newusrc.WX_UserName = Seq;
                newusrc.WX_SourceType = "微";
                newusrc.RemarkName = RemarkName;
                newusrc.NickName = CleanHtml(NickName);

                newusrc.IsCaculateFuli = true;
                if (UserNametempID.StartsWith("@@") == false)
                {
                    newusrc.IsReply = true;
                }
                else
                {
                    newusrc.IsReply = true;
                }
                db.WX_UserReply.InsertOnSubmit(newusrc);
                db.SubmitChanges();
                ;
            } //初始化，添加到数据库或同步数据库
            else
            {
                if ((usrc.RemarkName != RemarkName) || (usrc.NickName != CleanHtml(NickName))
                    )
                {

                    usrc.RemarkName = RemarkName;
                    usrc.NickName = CleanHtml(NickName);

                }
                //if (UserNametempID.StartsWith("@@") == false && Seq != "0")
                {
                    usrc.IsReply = true;
                }
            } //初始化，添加到数据库或同步数据库
            WX_WebSendPICSetting webpcset = db.WX_WebSendPICSetting.SingleOrDefault(t => t.aspnet_UserID == UserKey
               && t.WX_SourceType == "微"
                && t.WX_UserName == Seq
               );
            if (webpcset == null)
            {
                webpcset = new WX_WebSendPICSetting();

                webpcset.aspnet_UserID = UserKey;

                webpcset.WX_SourceType = "微";
                webpcset.WX_UserName = Seq;

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

                {
                    webpcset.PIC_StartHour = 8;
                }
                {
                    webpcset.PIC_StartMinute = 58;
                }
                {
                    webpcset.PIC_EndHour = 2;
                }
                {
                    webpcset.Pic_EndMinute = 3;
                }
                db.WX_WebSendPICSetting.InsertOnSubmit(webpcset);
                db.SubmitChanges();
            }



            usrc = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == UserKey && t.WX_UserName == Seq && t.WX_SourceType == "微");

            if (_GameMode != "")
            {
                usrc.ChongqingMode = (_GameMode == "重庆" ? true : false);
                usrc.VRMode = (_GameMode == "VR" ? true : false); ;
                usrc.AozcMode = (_GameMode == "澳彩" ? true : false); ;
                usrc.HkMode = (_GameMode == "香港" ? true : false);
                usrc.XinJiangMode = (_GameMode == "新疆" ? true : false);
                usrc.FiveMinuteMode = (_GameMode == "五分" ? true : false); ;
                usrc.TengXunWuFenMode = (_GameMode == "腾五" ? true : false); ;
                usrc.TengXunWuFenXinMode = (_GameMode == "腾五信" ? true : false); ;
                usrc.TengXunShiFenMode = (_GameMode == "腾十" ? true : false); ;
                usrc.TengXunShiFenXinMode = (_GameMode == "腾十信" ? true : false); ;
                usrc.HeNeiWuFenMode = (_GameMode == "河内" ? true : false); ;
            }

            DataRow[] Lists = { MemberSource.Rows.Find(new object[] { UserNametempID, "微" }) }; //
            //MemberSource.Select("User_ContactTEMPID='" + UserNametempID + "' and User_SourceType='微'");
            DataRow newr = null;
            if (Lists.Length == 0 || Lists[0] == null)
            {
                newr = MemberSource.NewRow();
                newr.SetField("User_ContactTEMPID", UserNametempID);
                newr.SetField("User_SourceType", "微");

                MemberSource.Rows.Add(newr);
            }
            else
            {
                newr = Lists[0];
            }
            newr.SetField("User_ContactID", Seq);

            newr.SetField("User_Contact", NickName);
            newr.SetField("User_ContactType", UserNametempID.StartsWith("@@") ? "群" : "个人");


            newr.SetField("User_IsReply", usrc.IsReply);
            newr.SetField("User_IsSendPic", webpcset.IsSendPIC);
            newr.SetField("User_IsAdmin", usrc.IsAdmin);
            newr.SetField("User_IsBallPIC", usrc.IsBallPIC);

            //if (UserNametempID.StartsWith("@@") == false && Seq != "0")
            {
                newr.SetField("User_IsReply", usrc == null ? false : usrc.IsReply);
            }


            newr.SetField("User_IsReceiveTransfer", usrc == null ? false : usrc.IsReceiveTransfer);
            newr.SetField("User_IsCaculateFuli", usrc == null ? false : usrc.IsCaculateFuli);
            newr.SetField("User_IsBoss", usrc == null ? false : (usrc.IsBoss == null ? false : usrc.IsBoss));

            newr.SetField("User_FiveMinuteMode", usrc == null ? false : (usrc.FiveMinuteMode == null ? false : usrc.FiveMinuteMode));
            newr.SetField("User_HkMode", usrc == null ? false : (usrc.HkMode == null ? false : usrc.HkMode));
            newr.SetField("User_AozcMode", usrc == null ? false : (usrc.AozcMode == null ? false : usrc.AozcMode));
            newr.SetField("User_ChongqingMode", usrc == null ? false : (usrc.ChongqingMode == null ? false : usrc.ChongqingMode));
            newr.SetField("User_TengXunShiFen", usrc == null ? false : (usrc.TengXunShiFenMode == null ? false : usrc.TengXunShiFenMode));
            newr.SetField("User_TengXunWuFen", usrc == null ? false : (usrc.TengXunWuFenMode == null ? false : usrc.TengXunWuFenMode));
            newr.SetField("User_TengXunShiFenXin", usrc == null ? false : (usrc.TengXunShiFenXinMode == null ? false : usrc.TengXunShiFenXinMode));
            newr.SetField("User_TengXunWuFenXin", usrc == null ? false : (usrc.TengXunWuFenXinMode == null ? false : usrc.TengXunWuFenXinMode));
            newr.SetField("User_XinJiangShiShiCai", usrc == null ? false : (usrc.XinJiangMode == null ? false : usrc.XinJiangMode));
            newr.SetField("User_VR", usrc == null ? false : (usrc.VRMode == null ? false : usrc.VRMode));
            newr.SetField("User_HeNeiWuFen", usrc == null ? false : (usrc.HeNeiWuFenMode == null ? false : usrc.HeNeiWuFenMode));



            //var UpdateLogs = ReplySource.AsEnumerable().Where(t => t.Field<string>("Reply_ContactID") == Seq);
            //foreach (var logitem in UpdateLogs)
            //{
            //    logitem.SetField("Reply_ContactTEMPID", UserNametempID);
            //    logitem.SetField("Reply_Contact", RemarkName == "" ? NickName : RemarkName);
            //}
            EachStart = DateTime.Now;
            db.SubmitChanges();




        }//成员列表循环



        return JsonConvert.SerializeObject(MemberSource);


        // BS_Contact.Sort = "User_Contact";





    }
    [WebMethod]
    public   string OpenUrl(string TargetURL, string RefURL, string Body, string Method, string S_BrowCookie, bool AllowRedirect = true, bool KeepAlive = false, string ContentType = "application/json;charset=UTF-8", string authorization = "")
    {
        System.Net.CookieCollection BrowCookie = new CookieCollection();
        // (System.Net.CookieCollection)JsonConvert.DeserializeObject(S_BrowCookie, typeof(System.Net.CookieCollection));
        DateTime Pre = DateTime.Now;
        string Result = OpenUrl(TargetURL, RefURL, Body, Method, BrowCookie, Encoding.UTF8, AllowRedirect, KeepAlive, ContentType, authorization);
        //NetFramework.Console.WriteLine("--------------网站下载总时间：" + (DateTime.Now - Pre).TotalSeconds.ToString(), false);
        return Result;
    }

    public static bool CheckValidationResult(object sender
        , System.Security.Cryptography.X509Certificates.X509Certificate certificate
        , System.Security.Cryptography.X509Certificates.X509Chain chain
        , System.Net.Security.SslPolicyErrors errors)
    {
        return true;
    }

    public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
    {
        var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        if (property != null)
        {
            var collection = property.GetValue(header, null) as System.Collections.Specialized.NameValueCollection;
            collection[name] = value;
        }
    }

    public static string OpenUrl(string TargetURL, string RefURL, string Body, string Method, System.Net.CookieCollection BrowCookie, Encoding ContactEncoding, bool AllowRedirect = false, bool KeepAlive = true, string ContentType = "application/json;charset=UTF-8", string authorization = "")
    {

        //System.Net.ServicePointManager.MaxServicePoints=20;

        System.Net.ServicePointManager.DefaultConnectionLimit = 500;

        System.Net.ServicePointManager.SetTcpKeepAlive(true, 15000, 15000);
        //HttpWebRequest LoginPage = null;
        //    GetHttpWebResponseNoRedirect(TargetURL,"","",out LoginPage);

        WebRequest LoginPage = HttpWebRequest.Create(TargetURL);
        ((HttpWebRequest)LoginPage).AllowAutoRedirect = AllowRedirect;
        //((HttpWebRequest)LoginPage).KeepAlive = KeepAlive;
        //SetHeaderValue(((HttpWebRequest)LoginPage).Headers, "Connection", "Keep-Alive");
        ((HttpWebRequest)LoginPage).Timeout = 6000;
        ((HttpWebRequest)LoginPage).Credentials = CredentialCache.DefaultCredentials;
        if (authorization != "")
        {
            LoginPage.Headers.Add("Authorization", authorization);

        }

        LoginPage.Method = Method;
        if (TargetURL.ToLower().StartsWith("https"))
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;

            //System.Net.ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
            ((HttpWebRequest)LoginPage).ProtocolVersion = System.Net.HttpVersion.Version11;
        }

        switch (Method)
        {
            case "GET":
                // ((HttpWebRequest)LoginPage).Accept = "application/x-ms-application, image/jpeg, application/xaml+xml, image/gif, image/pjpeg, application/x-ms-xbap, application/vnd.ms-powerpoint, application/msword, application/vnd.ms-excel,application/json, text/plain, */*";
                ((HttpWebRequest)LoginPage).Accept = "*/*";
                ((HttpWebRequest)LoginPage).UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36 OPR/52.0.2871.40";
                LoginPage.Headers.Add("Accept-Encoding", "gzip, deflate,br");
                LoginPage.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
                ((HttpWebRequest)LoginPage).CookieContainer = new CookieContainer();
                ((HttpWebRequest)LoginPage).CookieContainer.Add(BrowCookie);

                //((HttpWebRequest)LoginPage).Connection = "KeepAlive,Close";
                ((HttpWebRequest)LoginPage).Referer = RefURL;

                break;
            case "POST":
                ((HttpWebRequest)LoginPage).Accept = "application/x-ms-application, image/jpeg, application/xaml+xml, image/gif, image/pjpeg, application/x-ms-xbap, application/vnd.ms-powerpoint, application/msword, application/vnd.ms-excel,application/json, text/plain, */*";
                ((HttpWebRequest)LoginPage).Referer = RefURL;
                ((HttpWebRequest)LoginPage).UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
                LoginPage.Headers.Add("Accept-Encoding", "gzip, deflate,br");
                LoginPage.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
                ((HttpWebRequest)LoginPage).CookieContainer = new CookieContainer();
                ((HttpWebRequest)LoginPage).CookieContainer.Add(BrowCookie);
                ((HttpWebRequest)LoginPage).ContentType = ContentType;
                //((HttpWebRequest)LoginPage).ServicePoint.Expect100Continue = true;
                //((HttpWebRequest)LoginPage).Connection = "KeepAlive";
                if (((HttpWebRequest)LoginPage).Referer != null)
                {
                    LoginPage.Headers.Add("Origin", ((HttpWebRequest)LoginPage).Referer.Substring(0, ((HttpWebRequest)LoginPage).Referer.Length - 1));

                }

                if (Body != "")
                {
                    Stream bodys = LoginPage.GetRequestStream();

                    byte[] text = ContactEncoding.GetBytes(Body);

                    bodys.Write(text, 0, text.Length);

                    bodys.Flush();
                    bodys.Close();
                }
                break;
            case "OPTIONS":
                // ((HttpWebRequest)LoginPage).Accept = "application/x-ms-application, image/jpeg, application/xaml+xml, image/gif, image/pjpeg, application/x-ms-xbap, application/vnd.ms-powerpoint, application/msword, application/vnd.ms-excel,application/json, text/plain, */*";
                ((HttpWebRequest)LoginPage).Accept = "*/*";
                ((HttpWebRequest)LoginPage).UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36 OPR/52.0.2871.40";
                LoginPage.Headers.Add("Accept-Encoding", "gzip, deflate,br");
                LoginPage.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
                ((HttpWebRequest)LoginPage).CookieContainer = new CookieContainer();
                ((HttpWebRequest)LoginPage).CookieContainer.Add(BrowCookie);

                //((HttpWebRequest)LoginPage).Connection = "KeepAlive";
                ((HttpWebRequest)LoginPage).Referer = RefURL;
                LoginPage.Headers.Add("Origin", RefURL);

                break;
            default:
                break;
        }
        //((HttpWebRequest)LoginPage).KeepAlive = true;
        SetHeaderValue(((HttpWebRequest)LoginPage).Headers, "Connection", "Keep-Alive");
        LoginPage.Timeout = 6000;
        if (RefURL.ToLower().StartsWith("https"))
        {
            //System.Net.ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
            ((HttpWebRequest)LoginPage).ProtocolVersion = System.Net.HttpVersion.Version11;
        }
        //System.GC.Collect();
        System.Threading.Thread.Sleep(100);
        HttpWebResponse LoginPage_Return = null;
        try
        {
            //CurrentUrl = "正在下载" + TargetURL;
            //System.GC.Collect();

            // NetFramework.Console.WriteLine("下载URL" + LoginPage.RequestUri.AbsoluteUri + Environment.NewLine);
            LoginPage_Return = (HttpWebResponse)LoginPage.GetResponse();

            //CurrentUrl = "已下载" + TargetURL;

            if (((HttpWebResponse)LoginPage_Return).Headers["Set-Cookie"] != null)
            {
                string Start = LoginPage.RequestUri.Host.Substring(0, LoginPage.RequestUri.Host.LastIndexOf("."));
                string Host = LoginPage.RequestUri.Host.Substring(LoginPage.RequestUri.Host.LastIndexOf("."));

                foreach (Cookie cookieitem in ((HttpWebResponse)LoginPage_Return).Cookies)
                {
                    string[] SplitDomain = cookieitem.Domain.Split((".").ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    Int32 Length = SplitDomain.Length;
                    cookieitem.Domain = "." + SplitDomain[Length - 2] + "." + SplitDomain[Length - 1];
                    cookieitem.Expires = cookieitem.Expires == null ? DateTime.Now.AddHours(168) : cookieitem.Expires.AddHours(168);
                    BrowCookie.Add(cookieitem);
                }


                //CookieContainer NC = new CookieContainer();
                //NC.SetCookies(((HttpWebResponse)LoginPage_Return).ResponseUri, ((HttpWebResponse)LoginPage_Return).Headers["Set-Cookie"]);
                //BrowCookie.Add(NC.GetCookies(((HttpWebResponse)LoginPage_Return).ResponseUri));

                // Host = Start.Substring(Start.LastIndexOf(".")) + Host;
                // AddCookieWithCookieHead(tmpcookie, ((HttpWebResponse)LoginPage_Return).Headers["Set-Cookie"].Replace("Secure,", ""), Host);
            }

        }
        catch (Exception AnyError)
        {

            LoginPage = null;
            System.GC.Collect();

            //NetFramework.Console.WriteLine("网址打开失败" + TargetURL, false);
            //NetFramework.Console.WriteLine("网址打开失败" + AnyError.Message, false);
            //NetFramework.Console.WriteLine("网址打开失败" + AnyError.StackTrace, false);
            return "";
        }

        string responseBody = string.Empty;
        try
        {


            if (LoginPage_Return.ContentEncoding.ToLower().Contains("gzip"))
            {
                using (GZipStream stream = new GZipStream(LoginPage_Return.GetResponseStream(), CompressionMode.Decompress))
                {

                    using (StreamReader reader = new StreamReader(stream, ContactEncoding))
                    {
                        responseBody = reader.ReadToEnd();
                        stream.Close();
                    }
                }
            }
            else if (LoginPage_Return.ContentEncoding.ToLower().Contains("deflate"))
            {
                using (DeflateStream stream = new DeflateStream(LoginPage_Return.GetResponseStream(), CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(stream, ContactEncoding))
                    {
                        responseBody = reader.ReadToEnd();
                        stream.Close();
                    }
                }
            }
            else if (LoginPage_Return.ContentEncoding.ToLower().Contains("br"))
            {
                using (Brotli.BrotliStream stream = new Brotli.BrotliStream(LoginPage_Return.GetResponseStream(), CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(stream, ContactEncoding))
                    {
                        responseBody = reader.ReadToEnd();
                        stream.Close();
                    }
                }
            }
            else
            {
                using (Stream stream = LoginPage_Return.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, ContactEncoding))
                    {
                        responseBody = reader.ReadToEnd();
                        stream.Close();
                    }
                }
            }
        }
        catch (Exception AnyError)
        {
            LoginPage.Abort();
            LoginPage = null;
            //System.GC.Collect();

            //NetFramework.Console.WriteLine("网址打开失败" + TargetURL, true);
            //NetFramework.Console.WriteLine("网址打开失败" + AnyError.Message, true);
            //NetFramework.Console.WriteLine("网址打开失败" + AnyError.StackTrace, true);
            return "";
        }
        LoginPage.Abort();


        //  NetFramework.Console.WriteLine("下载完成" + LoginPage_Return.ResponseUri.AbsoluteUri + Environment.NewLine);

        LoginPage_Return.Close();
        LoginPage_Return = null;
        LoginPage = null;
        System.GC.Collect();


        return responseBody;

    }


}
