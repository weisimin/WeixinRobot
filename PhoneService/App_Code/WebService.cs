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
using WeixinRobotLib.Linq;
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
            return ("错误:用户不存在");
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
    public string UserLogInUsrpar(string UserName, string Password)
    {

        bool r = Membership.ValidateUser(UserName, Password);
        MembershipUser msr = Membership.GetUser(UserName);
        if (r == true)
        {
            FormsAuthentication.SetAuthCookie(UserName, true);
            string Cookie = FormsAuthentication.GetAuthCookie(UserName, true).Value;

            WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam result = new WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam();
            result.UserName = UserName;
            result.Password = Password;

            result.ASPXAUTH = GetUserToken(UserName, Password);
            result.LoginCookie = null;
            result.UserKey = (Guid)msr.ProviderUserKey;
            result.JobID = Guid.Empty;
            WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
            //db.ObjectTrackingEnabled = false;
            WeixinRobotLib.Entity.Linq.aspnet_UsersNewGameResultSend save_sets = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == (Guid)msr.ProviderUserKey);
            if (save_sets == null)
            {

                save_sets = new WeixinRobotLib.Entity.Linq.aspnet_UsersNewGameResultSend();
                save_sets.aspnet_UserID = (Guid)msr.ProviderUserKey;
                db.aspnet_UsersNewGameResultSend.InsertOnSubmit(save_sets);
                db.SubmitChanges();
            }
            WeixinRobotLib.Entity.Linq.aspnet_UsersNewGameResultSend loadset = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == (Guid)msr.ProviderUserKey);

            result.Membersetting = loadset;
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);


        }
        else if (msr == null)
        {
            return ("错误:用户不存在");
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
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        //db.ObjectTrackingEnabled = false;
        WeixinRobotLib.Entity.Linq.aspnet_UsersNewGameResultSend save_sets = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == aspnetUserid);
        if (save_sets == null)
        {

            save_sets = new WeixinRobotLib.Entity.Linq.aspnet_UsersNewGameResultSend();
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
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        // db.ObjectTrackingEnabled = false;
        WeixinRobotLib.Entity.Linq.aspnet_UsersNewGameResultSend tins_sets = (WeixinRobotLib.Entity.Linq.aspnet_UsersNewGameResultSend)JsonConvert.DeserializeObject(jaspnet_UsersNewGameResultSend, typeof(WeixinRobotLib.Entity.Linq.aspnet_UsersNewGameResultSend));

        WeixinRobotLib.Entity.Linq.aspnet_UsersNewGameResultSend save_sets = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == tins_sets.aspnet_UserID);
        if (save_sets == null)
        {

            save_sets = new WeixinRobotLib.Entity.Linq.aspnet_UsersNewGameResultSend();
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
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
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
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        var CopyRatio = db.Game_BasicRatio.Where(t => t.aspnet_UserID == CopySourceID);
        return JsonConvert.SerializeObject(CopyRatio);
    }
    [WebMethod]
    public string GetTemplateBonus()
    {
        Guid CopySourceID = (Guid)System.Web.Security.Membership.GetUser("sysadmin").ProviderUserKey;
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
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
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
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
            WeixinRobotLib.Entity.Linq.WX_UserReply usrc = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == UserKey && t.WX_UserName == Seq && t.WX_SourceType == "微");
            if (usrc == null)
            {
                WeixinRobotLib.Entity.Linq.WX_UserReply newusrc = new WeixinRobotLib.Entity.Linq.WX_UserReply();
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
            WeixinRobotLib.Entity.Linq.WX_WebSendPICSetting webpcset = db.WX_WebSendPICSetting.SingleOrDefault(t => t.aspnet_UserID == UserKey
               && t.WX_SourceType == "微"
                && t.WX_UserName == Seq
               );
            if (webpcset == null)
            {
                webpcset = new WeixinRobotLib.Entity.Linq.WX_WebSendPICSetting();

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
    public string OpenUrl(string TargetURL, string RefURL, string Body, string Method, string S_BrowCookie, bool AllowRedirect = true, bool KeepAlive = false, string ContentType = "application/json;charset=UTF-8", string authorization = "")
    {
        System.Net.CookieCollection BrowCookie = new CookieCollection();
        // (System.Net.CookieCollection)JsonConvert.DeserializeObject(S_BrowCookie, typeof(System.Net.CookieCollection));
        DateTime Pre = DateTime.Now;
        string Result = OpenUrl(TargetURL, RefURL, Body, Method, BrowCookie, Encoding.UTF8, AllowRedirect, KeepAlive, ContentType, authorization);
        //NetFramework.Console.WriteLine("--------------网站下载总时间：" + (DateTime.Now - Pre).TotalSeconds.ToString(), false);
        return Result;
    }
    [WebMethod]
    public string OpenLongTimeUrl(string TargetURL, string RefURL, string Body, string Method, string S_BrowCookie, bool AllowRedirect = true, bool KeepAlive = false, string ContentType = "application/json;charset=UTF-8", string authorization = "")
    {
        System.Net.CookieCollection BrowCookie = new CookieCollection();
        // (System.Net.CookieCollection)JsonConvert.DeserializeObject(S_BrowCookie, typeof(System.Net.CookieCollection));
        DateTime Pre = DateTime.Now;
        string Result = OpenLongTimeUrl(TargetURL, RefURL, Body, Method, BrowCookie, Encoding.UTF8, AllowRedirect, KeepAlive, ContentType, authorization);
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
        ((HttpWebRequest)LoginPage).KeepAlive = KeepAlive;
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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;

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
        ((HttpWebRequest)LoginPage).KeepAlive = true;
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

    public static string OpenLongTimeUrl(string TargetURL, string RefURL, string Body, string Method, System.Net.CookieCollection BrowCookie, Encoding ContactEncoding, bool AllowRedirect = false, bool KeepAlive = true, string ContentType = "application/json;charset=UTF-8", string authorization = "")
    {

        //System.Net.ServicePointManager.MaxServicePoints=20;

        System.Net.ServicePointManager.DefaultConnectionLimit = 500;

        System.Net.ServicePointManager.SetTcpKeepAlive(true, 15000, 15000);
        //HttpWebRequest LoginPage = null;
        //    GetHttpWebResponseNoRedirect(TargetURL,"","",out LoginPage);

        WebRequest LoginPage = HttpWebRequest.Create(TargetURL);
        ((HttpWebRequest)LoginPage).AllowAutoRedirect = AllowRedirect;
        ((HttpWebRequest)LoginPage).KeepAlive = KeepAlive;
        //SetHeaderValue(((HttpWebRequest)LoginPage).Headers, "Connection", "Keep-Alive");
        ((HttpWebRequest)LoginPage).Timeout = 30000;
        ((HttpWebRequest)LoginPage).Credentials = CredentialCache.DefaultCredentials;
        if (authorization != "")
        {
            LoginPage.Headers.Add("Authorization", authorization);

        }

        LoginPage.Method = Method;
        if (TargetURL.ToLower().StartsWith("https"))
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;

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
        ((HttpWebRequest)LoginPage).KeepAlive = true;
        SetHeaderValue(((HttpWebRequest)LoginPage).Headers, "Connection", "Keep-Alive");
        LoginPage.Timeout = 30000;
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
            return AnyError.Message;
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
            return AnyError.Message;
        }
        LoginPage.Abort();


        //  NetFramework.Console.WriteLine("下载完成" + LoginPage_Return.ResponseUri.AbsoluteUri + Environment.NewLine);

        LoginPage_Return.Close();
        LoginPage_Return = null;
        LoginPage = null;
        System.GC.Collect();


        return responseBody;

    }

    [WebMethod]
    public string WX_UserReplyLog_Create(string JMemberSource, string sgm, string ssubm, string RequestPeriod, DateTime RequestTime, string GameContent, string WX_UserName, string WX_SourceType, string Jusrpar, string Jloadset, bool adminmode = false, string MemberGroupName = "")
    {
        DataTable MemberSource = JsonConvert.DeserializeObject<DataTable>(JMemberSource);
        WeixinRobotLib.Entity.Linq.ProgramLogic.GameMode gm = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.GameMode>(sgm);
        WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode subm = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode>(ssubm);
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(Jusrpar);
        WeixinRobotLib.Entity.Linq.aspnet_UsersNewGameResultSend loadset = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.aspnet_UsersNewGameResultSend>(Jusrpar);

        return WeixinRobotLib.Linq.ProgramLogic.WX_UserReplyLog_Create(gm, subm, RequestPeriod, RequestTime, GameContent, WX_UserName, WX_SourceType, usrpar, loadset, adminmode, MemberGroupName);
    }
    [WebMethod]
    public string WX_UserReplyLog_MySendCreate(string Content, string jUserRow, DateTime ReceiveTime, string jusrpar, List<Guid> takeusers, string jloadset, string WX_UserName = "", string WX_SourceType = "")
    {
        WeixinRobotLib.Entity.Linq.WX_UserReply UserRow = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.WX_UserReply>(jUserRow);
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);
        WeixinRobotLib.Entity.Linq.aspnet_UsersNewGameResultSend loadset = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.aspnet_UsersNewGameResultSend>(jloadset);
        return WeixinRobotLib.Linq.ProgramLogic.WX_UserReplyLog_MySendCreate(Content, UserRow, ReceiveTime, usrpar, takeusers, loadset, WX_UserName, WX_SourceType);
    }
    [WebMethod]
    public ChongQingShiShiCaiCaculatePeriodResult ChongQingShiShiCaiCaculatePeriod(DateTime RequestTime, string RequestPeriod, string WX_UserName, string WX_SourceType, Boolean adminmode, string JSpecMode, string jusrpar, Boolean NoBlock = false)
    {
        ChongQingShiShiCaiCaculatePeriodResult r = new ChongQingShiShiCaiCaculatePeriodResult();
        string GameFullPeriod = "";
        string GameFullLocalPeriod = "";
        Boolean Success = false;
        string ErrorMessage = "";

        WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode SpecMode = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode>(JSpecMode);

        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);
        WeixinRobotLib.Linq.ProgramLogic.ChongQingShiShiCaiCaculatePeriod(RequestTime, RequestPeriod, db, WX_UserName, WX_SourceType, out  GameFullPeriod, out  GameFullLocalPeriod, adminmode, out  Success, out  ErrorMessage, SpecMode, usrpar.UserKey, NoBlock);
        r.GameFullPeriod = GameFullPeriod;
        r.GameFullLocalPeriod = GameFullLocalPeriod;
        r.Success = Success;
        r.ErrorMessage = ErrorMessage;

        return r;
    }

    [WebMethod]
    public decimal WXUserChangeLog_GetRemainder(string UserContactID, string SourceType, string jusrpar)
    {
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);
        return WeixinRobotLib.Linq.ProgramLogic.WXUserChangeLog_GetRemainder(UserContactID, SourceType, usrpar.UserKey);
    }
    [WebMethod]
    public DataTable GetBossReportSource(string SourceType, string QueryTime, string jusrpar)
    {
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);
        return WeixinRobotLib.Linq.ProgramLogic.GetBossReportSource(SourceType, QueryTime, usrpar);
    }




    public class ChongQingShiShiCaiCaculatePeriodResult
    {
        public string GameFullPeriod { get; set; }
        public string GameFullLocalPeriod { get; set; }
        public Boolean Success { get; set; }
        public string ErrorMessage { get; set; }
    }

    [WebMethod]
    public WeixinRobotLib.Entity.Linq.WX_BounsConfig[] GetBounsConfig(string jusrpar)
    {
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        return db.WX_BounsConfig.Where(t => t.aspnet_UserID == usrpar.UserKey).OrderBy(t => t.RowNumber).ToArray();
    }
    [WebMethod]
    public string SaveBounsConfig(string jusrpar, string JDatas)
    {
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);
        WeixinRobotLib.Entity.Linq.WX_BounsConfig[] Datas = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.WX_BounsConfig[]>(JDatas);
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        foreach (var item in Datas)
        {

            var toupd = db.WX_BounsConfig.SingleOrDefault(t => t.aspnet_UserID == usrpar.UserKey && t.RowNumber == item.RowNumber);
            if (toupd != null)
            {
                toupd.StartBuyPeriod = item.StartBuyPeriod;
                toupd.EndBuyPeriod = item.EndBuyPeriod;
                toupd.StartBuyAverage = item.StartBuyAverage;
                toupd.EndBuyAverage = item.EndBuyAverage;
                toupd.FixNumber = item.FixNumber;
                toupd.FlowPercent = item.FlowPercent;
                toupd.IfDivousPercent = item.IfDivousPercent;

            }
            else
            {
                WeixinRobotLib.Entity.Linq.WX_BounsConfig toins = new WeixinRobotLib.Entity.Linq.WX_BounsConfig();
                toins.aspnet_UserID = usrpar.UserKey;
                toins.RowNumber = item.RowNumber;
                toins.StartBuyPeriod = item.StartBuyPeriod;
                toins.EndBuyPeriod = item.EndBuyPeriod;
                toins.StartBuyAverage = item.StartBuyAverage;
                toins.EndBuyAverage = item.EndBuyAverage;
                toins.FixNumber = item.FixNumber;
                toins.FlowPercent = item.FlowPercent;
                toins.IfDivousPercent = item.IfDivousPercent;
                db.WX_BounsConfig.InsertOnSubmit(toins);

            }

        }//行循环
        db.SubmitChanges();
        return "保存成功";
    }
    [WebMethod]
    public WeixinRobotLib.Entity.Linq.Game_BasicRatio[] GetBasicRatio(string jusrpar)
    {
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        return db.Game_BasicRatio.Where(t => t.aspnet_UserID == usrpar.UserKey).ToArray();
    }

    [WebMethod]
    public string SaveBasicRatio(string jusrpar, string jDatas)
    {
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);
        WeixinRobotLib.Entity.Linq.Game_BasicRatio[] Datas = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.Game_BasicRatio[]>(jDatas);
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        foreach (var item in Datas)
        {
            var toupd = db.Game_BasicRatio.SingleOrDefault(t =>
                t.aspnet_UserID == usrpar.UserKey
                && t.GameType == item.GameType
                     && t.BuyType == item.BuyType
                     && t.BuyValue == item.BuyValue
 && t.IncludeMin == item.IncludeMin
 && t.BonusBuyValueCondition == item.BonusBuyValueCondition
     && t.WX_SourceType == item.WX_SourceType

                );
            if (toupd != null)
            {
                toupd.MinBuy = item.MinBuy;
                toupd.MaxBuy = item.MaxBuy;
                toupd.BasicRatio = item.BasicRatio;
                toupd.OrderIndex = item.OrderIndex;
                toupd.Enable = item.Enable;


            }
            else
            {
                WeixinRobotLib.Entity.Linq.Game_BasicRatio toins = new WeixinRobotLib.Entity.Linq.Game_BasicRatio();

                toins.MinBuy = item.MinBuy;
                toins.MaxBuy = item.MaxBuy;
                toins.BasicRatio = item.BasicRatio;
                toins.OrderIndex = item.OrderIndex;
                toins.Enable = item.Enable;
                toins.aspnet_UserID = usrpar.UserKey;
                toins.GameType = item.GameType;
                toins.BuyType = item.BuyType;
                toins.BuyValue = item.BuyValue;
                toins.IncludeMin = item.IncludeMin;
                toins.BonusBuyValueCondition = item.BonusBuyValueCondition;
                toins.WX_SourceType = item.WX_SourceType;
                db.Game_BasicRatio.InsertOnSubmit(toins);


            }

        }//行循环
        db.SubmitChanges();
        return "保存成功";
    }//函数结束

    [WebMethod]
    public ReminderType[] GetReminder(string jusrpar, string SourceType)
    {
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);

        var datasource = (from ds in db.WX_UserChangeLog
                          group ds by new { ds.WX_UserName, ds.WX_SourceType, ds.aspnet_UserID } into g
                          join dm in db.WX_UserReply on new { g.Key.aspnet_UserID, g.Key.WX_UserName, g.Key.WX_SourceType } equals new { dm.aspnet_UserID, dm.WX_UserName, dm.WX_SourceType }

                          where g.Key.aspnet_UserID == usrpar.UserKey
                          && g.Key.WX_SourceType == SourceType


                          select new ReminderType
                          {
                              玩家 = dm.NickName + "(" + dm.RemarkName + ")"
                              ,
                              余 = g.Sum(t => t.ChangePoint)
                              ,
                              WX_UserName = g.Key.WX_UserName
                              ,
                              WX_SourceType = g.Key.WX_SourceType
                          }).ToArray();
        return datasource;
    }
    public class ReminderType
    {
        public string 玩家 { get; set; }
        public decimal? 余 { get; set; }
        public string WX_UserName { get; set; }
        public string WX_SourceType { get; set; }
    }
    [WebMethod]
    public UserChangeLogType[] GetUserChangeLog(string jusrpar, string jUserRow)
    {
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);
        DataRow UserRow = JsonConvert.DeserializeObject<DataRow>(jUserRow);

        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);

        var data = (from dsl in db.WX_UserChangeLog
                    join dsu in db.WX_UserReply
                    on new { dsl.WX_UserName, dsl.aspnet_UserID, dsl.WX_SourceType } equals new { dsu.WX_UserName, dsu.aspnet_UserID, dsu.WX_SourceType }
                    where dsl.aspnet_UserID == usrpar.UserKey
                              && dsl.WX_UserName == UserRow.Field<string>("User_ContactID")
                              && dsl.WX_SourceType == UserRow.Field<string>("User_SourceType")
                    select new UserChangeLogType
                    {
                        UserName = dsu.WX_UserName
                        ,
                        Remark = dsl.Remark
                        ,
                        RemarkType = dsl.RemarkType
                        ,
                        ChangePoint = dsl.ChangePoint
                        ,
                        ChangeTime = dsl.ChangeTime
                        ,
                        GamePeriod = dsl.GamePeriod
                        ,
                        SourceType = dsu.WX_SourceType
                    }).ToArray();
        return data;
    }

    public class UserChangeLogType
    {
        public string UserName { get; set; }
        public string Remark { get; set; }
        public string RemarkType { get; set; }
        public decimal? ChangePoint { get; set; }
        public DateTime? ChangeTime { get; set; }
        public string GamePeriod { get; set; }
        public string SourceType { get; set; }
    }

    [WebMethod]
    public string GetHKSixLast16(string jusrpar)
    {
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);
        return WeixinRobotLib.Linq.ProgramLogic.GetHKSixLast16(usrpar);
    }
    [WebMethod]
    public RemindQuery_GetReplyLogClass[] RemindQuery_GetReplyLog(string WX_UserName, string WX_SourceType, string jusrpar)
    {
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);

        var ReplyLog = (from ds in db.WX_UserReplyLog
                        join user in db.WX_UserReply on new { ds.aspnet_UserID, ds.WX_UserName, ds.WX_SourceType } equals new { user.aspnet_UserID, user.WX_UserName, user.WX_SourceType }

                        where ds.aspnet_UserID == usrpar.UserKey
                        && ds.WX_UserName == WX_UserName
                        && ds.WX_SourceType == WX_SourceType
                        orderby ds.ReceiveTime ascending
                        select
                        new RemindQuery_GetReplyLogClass
                        {
                            玩家 = (ds.SourceType == "微" || ds.SourceType == "易" ? user.NickName + "(" + user.RemarkName + ")" : "我")
                            ,
                            内容 = ds.ReceiveContent
                            ,
                            时间 = ds.ReceiveTime

                        }).ToArray();

        return ReplyLog;


    }//fun end
    public class RemindQuery_GetReplyLogClass
    {
        public string 玩家 { get; set; }
        public string 内容 { get; set; }
        public DateTime? 时间 { get; set; }
    }
    [WebMethod]
    public RemindQuery_GetChangePointClass[] RemindQuery_GetChangePoint(string WX_UserName, string WX_SourceType, string jusrpar)
    {
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);



        var ChangePoint = (from ds in db.WX_UserChangeLog
                           join user in db.WX_UserReply on new { ds.aspnet_UserID, ds.WX_UserName, ds.WX_SourceType } equals new { user.aspnet_UserID, user.WX_UserName, user.WX_SourceType }
                           where ds.aspnet_UserID == usrpar.UserKey
                           && ds.WX_UserName == WX_UserName
                           && ds.WX_SourceType == WX_SourceType
                           orderby ds.ChangeTime ascending
                           select new RemindQuery_GetChangePointClass
                          {

                              分数变动 = ds.ChangePoint
                              ,
                              时间 = ds.ChangeTime
                              ,
                              期号 = ds.GamePeriod
                              ,
                              类型 = ds.RemarkType
                              ,
                              下注 = ds.BuyValue


                          }).ToArray();

        return ChangePoint;

    }//fun end
    public class RemindQuery_GetChangePointClass
    {
        public decimal? 分数变动 { get; set; }
        public DateTime? 时间 { get; set; }
        public string 期号 { get; set; }
        public string 类型 { get; set; }
        public string 下注 { get; set; }
    }//fun end
    [WebMethod]
    public string GetWebSendPicSetting(Guid UserKey, string Row_WX_SourceType, string Row_WX_UserName)
    {
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);

        var data = db.WX_WebSendPICSetting.SingleOrDefault(t => t.aspnet_UserID == UserKey
            && t.WX_SourceType == Row_WX_SourceType
            && t.WX_UserName == Row_WX_UserName
            );
        if (data != null)
        {
            return JsonConvert.SerializeObject(data);
        }
        else
        {
            WeixinRobotLib.Entity.Linq.WX_WebSendPICSetting newd = new WeixinRobotLib.Entity.Linq.WX_WebSendPICSetting();
            newd.aspnet_UserID = UserKey;
            newd.WX_SourceType = Row_WX_SourceType;
            newd.WX_UserName = Row_WX_UserName;
            return JsonConvert.SerializeObject(newd); ;
        }

    }//fun end
    [WebMethod]
    public string SaveWebSendPicSetting(string jusrpar, string JWebSendPicSetting)
    {
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        WeixinRobotLib.Entity.Linq.WX_WebSendPICSetting picset = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.WX_WebSendPICSetting>(JWebSendPicSetting);

        var data = db.WX_WebSendPICSetting.SingleOrDefault(t => t.aspnet_UserID == picset.aspnet_UserID
     && t.WX_SourceType == picset.WX_SourceType
     && t.WX_UserName == picset.WX_UserName
     );
        if (data != null)
        {
            data.ballinterval = Convert.ToInt32(picset.ballinterval);
            data.footballPIC = picset.footballPIC;
            data.bassketballpic = picset.bassketballpic;
            data.balluclink = picset.balluclink;

            data.card = picset.card;
            data.cardname = picset.cardname;
            data.shishicailink = picset.shishicailink;
            data.NumberPIC = picset.NumberPIC;
            data.dragonpic = picset.dragonpic;
            data.numericlink = picset.numericlink;
            data.dragonlink = picset.dragonlink;
            data.NumberAndDragonPIC = picset.NumberAndDragonPIC;

            data.ballstart = picset.ballstart;
            data.ballend = picset.ballend;

            data.balllivepoint = picset.balllivepoint;
            data.HKSixResult = picset.HKSixResult;


            data.NumberDragonTxt = picset.NumberDragonTxt;
            data.NiuNiuPic = picset.NiuNiuPic;
            data.NoBigSmallSingleDoublePIC = picset.NoBigSmallSingleDoublePIC;

            data.IsSendPIC = picset.IsSendPIC;

            data.PIC_StartHour = Convert.ToInt32(picset.PIC_StartHour);
            data.PIC_StartMinute = Convert.ToInt32(picset.PIC_StartMinute);
            data.PIC_EndHour = Convert.ToInt32(picset.PIC_EndHour);
            data.Pic_EndMinute = Convert.ToInt32(picset.Pic_EndMinute);
            try
            {
                db.SubmitChanges();
            }
            catch (Exception AnyError)
            {
                return AnyError.Message;

            }

        }
        else
        {
            WeixinRobotLib.Entity.Linq.WX_WebSendPICSetting newd = new WeixinRobotLib.Entity.Linq.WX_WebSendPICSetting();

            newd.aspnet_UserID = picset.aspnet_UserID;
            newd.WX_UserName = picset.WX_UserName;
            newd.WX_SourceType = picset.WX_SourceType;


            newd.ballinterval = Convert.ToInt32(picset.ballinterval);
            newd.footballPIC = picset.footballPIC;
            newd.bassketballpic = picset.bassketballpic;
            newd.balluclink = picset.balluclink;

            newd.card = picset.card;
            newd.cardname = picset.cardname;
            newd.shishicailink = picset.shishicailink;
            newd.NumberPIC = picset.NumberPIC;
            newd.dragonpic = picset.dragonpic;
            newd.numericlink = picset.numericlink;
            newd.dragonlink = picset.dragonlink;
            newd.balllivepoint = picset.balllivepoint;

            newd.NumberAndDragonPIC = picset.NumberAndDragonPIC;

            newd.ballstart = picset.ballstart;
            newd.ballend = picset.ballend;
            newd.HKSixResult = picset.HKSixResult;


            newd.NumberDragonTxt = picset.NumberDragonTxt;
            newd.NiuNiuPic = picset.NiuNiuPic;
            newd.NoBigSmallSingleDoublePIC = picset.NoBigSmallSingleDoublePIC;

            newd.IsSendPIC = picset.IsSendPIC;

            newd.PIC_StartHour = Convert.ToInt32(picset.PIC_StartHour);
            newd.PIC_StartMinute = Convert.ToInt32(picset.PIC_StartMinute);
            newd.PIC_EndHour = Convert.ToInt32(picset.PIC_EndHour);
            newd.Pic_EndMinute = Convert.ToInt32(picset.Pic_EndMinute);

            db.WX_WebSendPICSetting.InsertOnSubmit(newd);
            try
            {
                db.SubmitChanges();
            }
            catch (Exception AnyError)
            {
                return AnyError.Message;

            }
        }
        return "保存成功";
    }//fun end

    [WebMethod]
    public string ReceiveContentFormat(string ReceiveContent, out string jState, out string jModeType, string jdirect, out string BuyType, out string BuyMoney, out string[] ContextTeams, string jusrpar)
    {
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);
        WeixinRobotLib.Entity.Linq.ProgramLogic.FormatResultDirection direct = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.FormatResultDirection>(jdirect);
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);

        WeixinRobotLib.Entity.Linq.ProgramLogic.FormatResultState State = WeixinRobotLib.Entity.Linq.ProgramLogic.FormatResultState.Initialize;
        WeixinRobotLib.Entity.Linq.ProgramLogic.FormatResultType ModeType = WeixinRobotLib.Entity.Linq.ProgramLogic.FormatResultType.Initialize;

        var res = WeixinRobotLib.Linq.ProgramLogic.ReceiveContentFormat(ReceiveContent, out State, out   ModeType, direct, out  BuyType, out  BuyMoney, out  ContextTeams, usrpar);
        jState = JsonConvert.SerializeObject(State);
        jModeType = JsonConvert.SerializeObject(ModeType);
        return JsonConvert.SerializeObject(res);

    }
    [WebMethod]
    public decimal GetUserPeriodInOut(string GamePeriod, string WX_UserName, string WX_SourceType, string jusrpar)
    {
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        return WeixinRobotLib.Linq.ProgramLogic.GetUserPeriodInOut(GamePeriod, WX_UserName, WX_SourceType, db, usrpar.UserKey);
    }
    [WebMethod]
    public DataTable GetBounsSource(DateTime QueryDate, string SourceType, string jusrpar)
    {
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        return WeixinRobotLib.Linq.ProgramLogic.GetBounsSource(QueryDate, SourceType, usrpar);

    }
    [WebMethod]
    public SendCharge_GetSourceClass[] SendCharge_GetSource(string jusrpar, string WX_UserName, string WX_SourceType)
    {
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);

        var data = (from dsl in db.WX_UserChangeLog
                    join dsu in db.WX_UserReply
                    on new { dsl.WX_UserName, dsl.aspnet_UserID, dsl.WX_SourceType } equals new { dsu.WX_UserName, dsu.aspnet_UserID, dsu.WX_SourceType }
                    where dsl.aspnet_UserID == usrpar.UserKey
                    && dsl.WX_UserName == WX_UserName
                    && dsl.WX_SourceType == WX_SourceType
                    select new SendCharge_GetSourceClass
                    {
                        UserName = dsu.WX_UserName
                        ,
                        Remark = dsl.Remark
                        ,
                        RemarkType = dsl.RemarkType
                        ,
                        ChangePoint = dsl.ChangePoint
                        ,
                        ChangeTime = dsl.ChangeTime
                        ,
                        GamePeriod = dsl.GamePeriod
                        ,
                        SourceType = dsu.WX_SourceType
                    }).ToArray();
        return data;
    }
    public class SendCharge_GetSourceClass
    {
        public string UserName { get; set; }
        public string Remark { get; set; }
        public string RemarkType { get; set; }
        public decimal? ChangePoint { get; set; }
        public DateTime? ChangeTime { get; set; }
        public string GamePeriod { get; set; }
        public string SourceType { get; set; }
    }
    [WebMethod]
    public string GameVSGetRatios(string jVS)
    {
        WeixinRobotLib.Entity.Linq.Game_FootBall_VS VS = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.Game_FootBall_VS>(jVS);

        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);

        return JsonConvert.SerializeObject(WeixinRobotLib.Linq.ProgramLogic.GameVSGetRatios(db, VS).ToArray());
    }
    [WebMethod]
    public string VSGetCurRatio(string jVS)
    {
        WeixinRobotLib.Entity.Linq.Game_FootBall_VS VS = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.Game_FootBall_VS>(jVS);

        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);

        return JsonConvert.SerializeObject(WeixinRobotLib.Linq.ProgramLogic.VSGetCurRatio(VS, db));

    }
    [WebMethod]
    public BallOpen_GetUpPaySourceClass[] BallOpen_GetUpPaySource(string GameID, string WX_SourceType, Guid UserKey)
    {
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);

        var unplaysource = (from ds in db.WX_UserGameLog_Football
                            join urname in db.WX_UserReply on new { ds.aspnet_UserID, ds.WX_UserName, ds.WX_SourceType } equals new { urname.aspnet_UserID, urname.WX_UserName, urname.WX_SourceType } into urnamr_emp
                            from urnamr_emp_d in urnamr_emp.DefaultIfEmpty()

                            where ds.HaveOpen == false
                            && ds.GameKey == GameID
                             && ds.WX_SourceType == WX_SourceType
                             && ds.aspnet_UserID == UserKey
                            select new BallOpen_GetUpPaySourceClass
                            {
                                WX_UserNameOrRemark = urnamr_emp_d.NickName + "(" + urnamr_emp_d.RemarkName + ")"
                                ,
                                aspnet_UserID = ds.aspnet_UserID
                                ,
                                transtime = ds.transtime
                                ,
                                GameID = ds.GameKey
                                ,
                                GameVS = ds.GameVS
                                ,
                                BuyType = ds.BuyType
                                ,
                                BuyRatio = ds.BuyRatio
                                ,
                                BuyMoney = ds.BuyMoney
                                ,
                                HaveOpen = ds.HaveOpen
                                ,
                                ResultMoney = ds.ResultMoney
                                ,
                                A_WIN = ds.A_WIN
                                ,
                                Winless = ds.Winless
                                ,
                                B_Win = ds.B_WIN
                                ,
                                BigWin = ds.BIGWIN
                                ,
                                Total = ds.Total
                                ,
                                SmallWin = ds.SMALLWIN
                                ,
                                R_A_A = ds.R_A_A
                                ,
                                R_A_SAME = ds.R_A_SAME
                                ,
                                R_A_B = ds.R_A_B
                                ,
                                R_SAME_A = ds.R_SAME_A
                                ,
                                R_SAME_SAME = ds.R_SAME_SAME
                                ,
                                R_SAME_B = ds.R_SAME_B
                                ,
                                R_B_A = ds.R_B_A
                                ,
                                R_B_SAME = ds.R_B_SAME
                                ,
                                R_B_B = ds.R_B_B
                                ,
                                R1_0_A = ds.R1_0_A
                                ,
                                R1_0_B = ds.R1_0_B
                                ,
                                R2_0_A = ds.R2_0_A
                                ,
                                R2_0_B = ds.R2_0_B
                                ,
                                R2_1_A = ds.R2_1_A
                                ,
                                R2_1_B = ds.R2_1_B
                                ,
                                R3_0_A = ds.R3_0_A
                                ,
                                R3_0_B = ds.R3_0_B
                                ,
                                R3_1_A = ds.R3_1_A
                                ,
                                R3_1_B = ds.R3_1_B
                                ,
                                R3_2_A = ds.R3_2_A
                                ,
                                R3_2_B = ds.R3_2_B
                                ,
                                R4_0_A = ds.R4_0_A
                                ,
                                R4_0_B = ds.R4_0_B
                                ,
                                R4_1_A = ds.R4_1_A
                                ,
                                R4_1_B = ds.R4_1_B
                                ,
                                R4_2_A = ds.R4_2_A
                                ,
                                R4_2_B = ds.R4_2_B
                                ,
                                R4_3_A = ds.R4_3_A
                                ,
                                R4_3_B = ds.R4_3_B
                                ,
                                R0_0 = ds.R0_0
                                ,
                                R1_1 = ds.R1_1
                                ,
                                R2_2 = ds.R2_2
                                ,
                                R3_3 = ds.R3_3
                                ,
                                R4_4 = ds.R4_4
                                ,
                                Rother = ds.ROTHER
                                ,
                                WX_SourceType = ds.WX_SourceType
                                ,
                                A_Team = ds.A_Team
                                ,
                                B_Team = ds.B_Team


                            }).ToArray();
        return unplaysource;
    }

    public class BallOpen_GetUpPaySourceClass
    {
        public string WX_UserNameOrRemark { get; set; }
        public Guid? aspnet_UserID { get; set; }
        public DateTime? transtime { get; set; }
        public string GameID { get; set; }
        public string GameVS { get; set; }
        public string BuyType { get; set; }
        public decimal? BuyRatio { get; set; }
        public decimal? BuyMoney { get; set; }
        public bool? HaveOpen { get; set; }
        public decimal? ResultMoney { get; set; }
        public decimal? A_WIN { get; set; }
        public string Winless { get; set; }
        public decimal? B_Win { get; set; }
        public decimal? BigWin { get; set; }
        public string Total { get; set; }
        public decimal? SmallWin { get; set; }
        public decimal? R_A_A { get; set; }
        public decimal? R_A_SAME { get; set; }
        public decimal? R_A_B { get; set; }
        public decimal? R_SAME_A { get; set; }
        public decimal? R_SAME_SAME { get; set; }
        public decimal? R_SAME_B { get; set; }
        public decimal? R_B_A { get; set; }
        public decimal? R_B_SAME { get; set; }
        public decimal? R_B_B { get; set; }
        public decimal? R1_0_A { get; set; }
        public decimal? R1_0_B { get; set; }
        public decimal? R2_0_A { get; set; }
        public decimal? R2_0_B { get; set; }
        public decimal? R2_1_A { get; set; }
        public decimal? R2_1_B { get; set; }
        public decimal? R3_0_A { get; set; }
        public decimal? R3_0_B { get; set; }
        public decimal? R3_1_A { get; set; }
        public decimal? R3_1_B { get; set; }
        public decimal? R3_2_A { get; set; }
        public decimal? R3_2_B { get; set; }
        public decimal? R4_0_A { get; set; }
        public decimal? R4_0_B { get; set; }
        public decimal? R4_1_A { get; set; }
        public decimal? R4_1_B { get; set; }
        public decimal? R4_2_A { get; set; }
        public decimal? R4_2_B { get; set; }
        public decimal? R4_3_A { get; set; }
        public decimal? R4_3_B { get; set; }
        public decimal? R0_0 { get; set; }
        public decimal? R1_1 { get; set; }
        public decimal? R2_2 { get; set; }
        public decimal? R3_3 { get; set; }
        public decimal? R4_4 { get; set; }
        public decimal? Rother { get; set; }
        public string WX_SourceType { get; set; }
        public string A_Team { get; set; }
        public string B_Team { get; set; }
    }

    [WebMethod]
    public SendManulOrder_GetOrderSourceClass[] SendManulOrder_GetOrderSource(DateTime dtp_StartDate, DateTime dtp_EndDate, Guid UserKey, string WX_UserName, string WX_SourceType)
    {
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);

        var datasource = (from ds in db.WX_UserReplyLog
                          join dsgame in db.WX_UserGameLog
                          on new { ds.aspnet_UserID, ds.WX_UserName, ds.ReceiveTime, ds.WX_SourceType } equals new { dsgame.aspnet_UserID, dsgame.WX_UserName, ReceiveTime = dsgame.TransTime, dsgame.WX_SourceType }
                          into leftdsggame
                          from dsgame2 in leftdsggame.DefaultIfEmpty()
                          where ds.ReceiveTime >= dtp_StartDate
                          && ds.ReceiveTime < dtp_EndDate
                          && ds.aspnet_UserID == UserKey
                          && ds.WX_UserName == WX_UserName
                          && ds.WX_SourceType == WX_SourceType
                          select new SendManulOrder_GetOrderSourceClass
                          {
                              ReceiveTime = ds.ReceiveTime,
                              ReceiveContent = ds.ReceiveContent,
                              aspnet_UserID = ds.aspnet_UserID,
                              WX_UserName = ds.WX_UserName,
                              WX_SourceType = ds.WX_SourceType,
                              TransTime = (DateTime?)dsgame2.TransTime,
                              GamePeriod = dsgame2.GamePeriod
                              ,
                              GameLocalPeriod = dsgame2.GameLocalPeriod
                              ,
                              GameResult = dsgame2.GameResult
                              ,
                              Buy_Value = dsgame2.Buy_Value
                              ,
                              Buy_Type = dsgame2.Buy_Type

                              ,
                              Buy_Point = dsgame2.Buy_Point,
                              Result_Point = dsgame2.Result_Point

                          }).ToArray();
        return datasource;
    }//fun end
    public class SendManulOrder_GetOrderSourceClass
    {
        public DateTime ReceiveTime { get; set; }
        public string ReceiveContent { get; set; }
        public Guid? aspnet_UserID { get; set; }
        public string WX_UserName { get; set; }
        public string WX_SourceType { get; set; }
        public DateTime? TransTime { get; set; }
        public string GamePeriod { get; set; }
        public string GameLocalPeriod { get; set; }
        public string GameResult { get; set; }
        public string Buy_Value { get; set; }
        public string Buy_Type { get; set; }

        public decimal? Buy_Point { get; set; }
        public decimal? Result_Point { get; set; }
    }
    [WebMethod]
    public string SendManulOrder_Delete(string aspnet_UserID, string WX_UserName, string WX_SourceType, DateTime DT)
    {
        try
        {
            WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);


            WeixinRobotLib.Entity.Linq.WX_UserGameLog testg = db.WX_UserGameLog.SingleOrDefault(t => t.aspnet_UserID == new Guid(aspnet_UserID)
                   && t.WX_UserName == WX_UserName
                   && t.WX_SourceType == WX_SourceType
                   && t.TransTime == DT);
            if (testg != null && testg.Result_HaveProcess != false)
            {
                return "已开或已处理,不能删除";
            }
            WeixinRobotLib.Entity.Linq.WX_UserReplyLog testrg = db.WX_UserReplyLog.SingleOrDefault(t => t.aspnet_UserID == new Guid(aspnet_UserID)
                      && t.WX_UserName == WX_UserName
                      && t.WX_SourceType == WX_SourceType
                      && t.ReceiveTime == DT);
            if (testg != null)
            {
                db.WX_UserGameLog.DeleteOnSubmit(testg);
            }
            if (testrg != null)
            {
                db.WX_UserReplyLog.DeleteOnSubmit(testrg);
            }
            db.SubmitChanges();
            return "删除成功";
        }
        catch (Exception AnyError)
        {

            return AnyError.Message;
        }

    }
    [WebMethod]
    public string WX_UserReply_Where(Guid aspnet_UserID)
    {
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);

        var Result = db.WX_UserReply.Where(t => t.aspnet_UserID == aspnet_UserID
                                && t.IsAdmin == true).ToArray();
        return JsonConvert.SerializeObject(Result);
    }
    [WebMethod]
    public string WX_UserReply_SingleOrDefault(Guid aspnet_UserID, string WX_UserName, string WX_SourceType)
    {

        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);

        var Result = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == aspnet_UserID
            && t.WX_UserName == WX_UserName
            && t.WX_SourceType == WX_SourceType);
        return JsonConvert.SerializeObject(Result);
    }//fun end
    [WebMethod]
    public string Game_FootBall_VS_Where(Guid aspnet_UserID, Guid JobID)
    {
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);

        var Result = db.Game_FootBall_VS.Where(t => t.aspnet_UserID == aspnet_UserID
            //&& (t.LastAliveTime == null || t.LastAliveTime >= DateTime.Today.AddDays(-3))
                    && t.Jobid == JobID
                    );
        return JsonConvert.SerializeObject(Result);
    }
    [WebMethod]
    public string BallOpen_Reload(string WX_SourceType, Guid UserKey)
    {
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);

        var source = ((from ds in db.WX_UserGameLog_Football
                       where ds.HaveOpen == false
                       && ds.WX_SourceType == WX_SourceType
                       && ds.aspnet_UserID == UserKey
                       select new GameFormat(ds.GameKey, ds.GameVS)).Distinct()).ToArray();
        return JsonConvert.SerializeObject(source);
    }
    public class GameFormat
    {
        public GameFormat(string p_gameid, string p_gamevs)
        {
            GameID = p_gameid;
            GameVS = p_gamevs;

        }
        public string GameID { get; set; }
        public string GameVS { get; set; }
    }

    [WebMethod]
    public string WX_WebSendPICSettingMatchClassSave(string jsubsource, string jusrpar, string WX_SourceType, string WX_UserName)
    {
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);

        List<WeixinRobotLib.Entity.Linq.WX_WebSendPICSettingMatchClass> subsource = JsonConvert.DeserializeObject<List<WeixinRobotLib.Entity.Linq.WX_WebSendPICSettingMatchClass>>(jsubsource);
        foreach (var item in subsource)
        {
            var datasub = db.WX_WebSendPICSettingMatchClass.SingleOrDefault(t =>
                t.aspnet_UserID == usrpar.UserKey
              && t.WX_SourceType == WX_SourceType
              && t.WX_UserName == WX_UserName
              && t.MatchBallType == item.MatchBallType
              && t.MatchClassName == item.MatchClassName
              );
            if (datasub != null)
            {
                datasub.SendAny = item.SendAny;
                db.SubmitChanges();
            }
            else
            {
                WeixinRobotLib.Entity.Linq.WX_WebSendPICSettingMatchClass newsub = new WeixinRobotLib.Entity.Linq.WX_WebSendPICSettingMatchClass();

                newsub.SendAny = item.SendAny;

                newsub.aspnet_UserID = usrpar.UserKey;
                newsub.WX_SourceType = WX_SourceType;
                newsub.WX_UserName = WX_UserName;
                newsub.MatchBallType = item.MatchBallType;
                newsub.MatchClassName = item.MatchClassName;


                db.WX_WebSendPICSettingMatchClass.InsertOnSubmit(newsub);
                db.SubmitChanges();

            }

        }
        return "保存成功";
    }
    [WebMethod]
    public string BallOpen_Open(string jDataSource, Guid UserKey, string jGameFormat)
    {
        try
        {


            List<GameHalfResult> DataSource = JsonConvert.DeserializeObject<List<GameHalfResult>>(jDataSource);
            WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);

            WeixinRobotLib.Entity.Linq.Game_ResultFootBall newgr = new WeixinRobotLib.Entity.Linq.Game_ResultFootBall();

            WeixinRobotLib.Entity.Linq.Game_ResultFootBall findgr = db.Game_ResultFootBall.SingleOrDefault(t => t.GameID == DataSource.First().OpenGameID);

            GameFormat gf = JsonConvert.DeserializeObject<GameFormat>(jGameFormat);
            if (findgr == null)
            {

                foreach (GameHalfResult item in DataSource)
                {
                    newgr.GameID = item.OpenGameID;
                    newgr.aspnet_UserID = UserKey;
                    newgr.GameVS = gf.GameVS;
                    if (item.TimeType == "上半场")
                    {
                        newgr.A_FrontHalf = item.TeamA;
                        newgr.B_FrontHalf = item.TeamB;
                    }
                    if (item.TimeType == "下半场")
                    {
                        newgr.A_EndHalf = item.TeamA;
                        newgr.B_EndHalf = item.TeamB;
                    }

                }


                db.Game_ResultFootBall.InsertOnSubmit(newgr);
                db.SubmitChanges();
            }

            else
            {
                foreach (GameHalfResult item in DataSource)
                {
                    findgr.GameID = item.OpenGameID;
                    findgr.aspnet_UserID = UserKey;
                    findgr.GameVS = gf.GameVS;
                    if (item.TimeType == "上半场")
                    {
                        findgr.A_FrontHalf = item.TeamA;
                        findgr.B_FrontHalf = item.TeamB;
                    }
                    if (item.TimeType == "下半场")
                    {
                        findgr.A_EndHalf = item.TeamA;
                        findgr.B_EndHalf = item.TeamB;
                    }

                }
                db.SubmitChanges();

            }
            return "保存成功";
        }
        catch (Exception AnyError)
        {

            return "保存失败" + AnyError.Message;
        }
    }

    public class GameHalfResult
    {
        public string TimeType { get; set; }

        public Int32? TeamA { get; set; }

        public Int32? TeamB { get; set; }

        public string OpenGameID { get; set; }

    }
    [WebMethod]
    public string Game_ResultFootBall_SingleOrDefault(string GameID)
    {
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        return JsonConvert.SerializeObject(db.Game_ResultFootBall.SingleOrDefault(t => t.GameID == GameID));
    }
    [WebMethod]
    public string WX_UserGameLog_Football_Where(string GameID, Guid UserKey)
    {
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);

        return JsonConvert.SerializeObject(db.WX_UserGameLog_Football.Where(t => (t.GameKey == GameID)
                    && (t.HaveOpen == false || t.HaveOpen == null)
                    && t.aspnet_UserID == UserKey
                    ).ToArray());
    }
    [WebMethod]
    public string OpenBallGameLog(string jgl, Int32 fronthalf_A, Int32 fronthalf_B, Int32 endhalf_A, Int32 endhalf_B, string jusrpar)
    {

        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(jusrpar);
        WeixinRobotLib.Entity.Linq.WX_UserGameLog_Football gl = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.WX_UserGameLog_Football>(jgl);

        return WeixinRobotLib.Linq.ProgramLogic.OpenBallGameLog(gl, db, fronthalf_A, fronthalf_B, endhalf_A, endhalf_B, usrpar);

    }
    [WebMethod]
    public string WX_UserGameLog_Where(Guid UserKey, string WX_SourceType, DateTime dtp_startdate, DateTime dtp_enddate)
    {
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);

        var dats = (from ds in
                        db.WX_UserGameLog
                    where
                         ds.aspnet_UserID == UserKey
                        && String.Compare(ds.GameLocalPeriod.Substring(0, 8), dtp_startdate.ToString("yyyyMMdd")) >= 0
                        && String.Compare(ds.GameLocalPeriod.Substring(0, 8), dtp_enddate.ToString("yyyyMMdd")) <= 0
                        && ds.WX_SourceType == WX_SourceType
                    select ds).ToArray();
        return JsonConvert.SerializeObject(dats);

    }
    [WebMethod]
    public DataTable OpenQuery_Query(Guid UserKey, DateTime dtp_startdate, DateTime dtp_enddate, string cb_SourceType, string jur)
    {

        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
        //db.ObjectTrackingEnabled = false;



        DataTable Result = new DataTable();
        Result.Columns.Add("类别");
        //DataGridViewColumn dc = new DataGridViewColumn();
        //dc.HeaderText = "类别";
        //dc.Name = "类别";
        //dc.DataPropertyName = "类别";
        //dc.CellTemplate = new DataGridViewTextBoxCell();
        //dc.Frozen = true;
        //gv_result.Columns.Add(dc);



        var buys = (from ds in db.WX_UserGameLog
                    where
                    ds.aspnet_UserID == UserKey
    && String.Compare(ds.GameLocalPeriod.Substring(0, 8), dtp_startdate.ToString("yyyyMMdd")) >= 0
    && String.Compare(ds.GameLocalPeriod.Substring(0, 8), dtp_enddate.ToString("yyyyMMdd")) <= 0
    && ds.WX_SourceType == cb_SourceType
                    select ds).ToList();

        NetFramework.Console.WriteLine("########################################################################", false);
        NetFramework.Console.WriteLine("查询日期" + dtp_startdate.ToString("yyyyMMdd"), false);
        NetFramework.Console.WriteLine("查询日期" + dtp_enddate.ToString("yyyyMMdd"), false);
        NetFramework.Console.WriteLine("########################################################################", false);

        var myWXUSERS = buys.Select(t => t.WX_UserName).Distinct();

        foreach (var item in myWXUSERS)
        {

            DataColumn uc = new DataColumn();
            uc.ColumnName = item;

            //DataGridViewColumn dcc = new DataGridViewColumn();

            //dcc.Name = item;
            //dcc.DataPropertyName = item;
            //dcc.CellTemplate = new DataGridViewTextBoxCell();

            //gv_result.Columns.Add(dcc);



            DataRow[] ur = JsonConvert.DeserializeObject<DataRow[]>(jur); //RunnerF.MemberSource.Select("User_ContactID='" + item.Replace("'", "''") + "' and User_SourceType='" + cb_SourceType + "'");
            if (ur.Count() != 0)
            {
                uc.Caption = ur[0].Field<string>("user_Contact");
            }
            else
            {
                var FidnUser = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == UserKey && t.WX_UserName == item && t.WX_SourceType == cb_SourceType);

                uc.Caption = (FidnUser == null ? item : (FidnUser.RemarkName != "" && FidnUser.RemarkName != null ? FidnUser.RemarkName + "@#" + FidnUser.NickName : FidnUser.NickName));

            }
            //dcc.HeaderText = uc.Caption;
            Result.Columns.Add(uc);

        }

        //DataColumn ucfull = new DataColumn();
        //ucfull.ColumnName = "全部玩家";

        //Result.Columns.Add(ucfull);

        //DataGridViewColumn dccful = new DataGridViewColumn();

        //dccful.Name = "全部玩家";
        //dccful.DataPropertyName = "全部玩家";
        //dccful.CellTemplate = new DataGridViewTextBoxCell();
        //gv_result.Columns.Add(dccful);

        var BuyDays = buys.Select(t => t.GameLocalPeriod.Substring(0, 8)).Distinct().OrderBy(t => t);
        NetFramework.Console.WriteLine("########################################################################", false);
        NetFramework.Console.WriteLine("获得天数" + BuyDays.Count(), false);

        NetFramework.Console.WriteLine("########################################################################", false);

        #region "天数"
        foreach (var item in BuyDays)
        {
            DataRow newr = Result.NewRow();

            newr.SetField("类别", item + "下注");
            foreach (var usritem in myWXUSERS)
            {
                newr.SetField(usritem, NetFramework.Util_Math.NullToZero(buys.Where(t =>
                    t.WX_UserName == usritem
                    && t.WX_SourceType == cb_SourceType
                    && t.aspnet_UserID == UserKey
                    && t.GameLocalPeriod.StartsWith(item)).Sum(t => t.Buy_Point)));
            }
            newr.SetField("全部玩家", NetFramework.Util_Math.NullToZero(buys.Where(t =>
                     t.GameLocalPeriod.StartsWith(item)
                        && t.aspnet_UserID == UserKey
                     ).Sum(t => t.Buy_Point)));




            DataRow newr2 = Result.NewRow();
            newr2.SetField("类别", item + "得分");
            foreach (var usritem in myWXUSERS)
            {
                newr2.SetField(usritem, NetFramework.Util_Math.NullToZero(buys.Where(t =>
                    t.WX_UserName == usritem
                    && t.WX_SourceType == cb_SourceType
                       && t.aspnet_UserID == UserKey
                     && t.GameLocalPeriod.StartsWith(item)
                    ).Sum(t => t.Result_Point)));
            }
            newr2.SetField("全部玩家", NetFramework.Util_Math.NullToZero(buys.Where(t =>
                    t.GameLocalPeriod.StartsWith(item)
                     && t.aspnet_UserID == UserKey
                    ).Sum(t => t.Result_Point)));



            DataRow newr6 = Result.NewRow();
            newr6.SetField("类别", item + "福利");
            foreach (var usritem in myWXUSERS)
            {
                newr6.SetField(usritem, NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                    t.WX_UserName == usritem
                      && t.WX_SourceType == cb_SourceType
                     && t.RemarkType == "福利"
                        && t.aspnet_UserID == UserKey
                     && t.ChangeLocalDay.StartsWith(item)
                    ).Sum(t => t.ChangePoint)));
            }
            newr6.SetField("全部玩家", NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                                 t.RemarkType == "福利"
                                    && t.aspnet_UserID == UserKey
                     && t.ChangeLocalDay.StartsWith(item)
                    ).Sum(t => t.ChangePoint)));



            DataRow newr3 = Result.NewRow();
            newr3.SetField("类别", item + "合计");
            foreach (var usritem in myWXUSERS)
            {
                newr3.SetField(usritem,

                    NetFramework.Util_Math.NullToZero(buys.Where(t =>
                    t.WX_UserName == usritem
                      && t.WX_SourceType == cb_SourceType
                       && t.aspnet_UserID == UserKey
                     && t.GameLocalPeriod.StartsWith(item)
                    ).Sum(t => t.Buy_Point))

                                        - NetFramework.Util_Math.NullToZero(buys.Where(t =>
                    t.WX_UserName == usritem
                      && t.WX_SourceType == cb_SourceType
                       && t.aspnet_UserID == UserKey
                     && t.GameLocalPeriod.StartsWith(item)
                    ).Sum(t => t.Result_Point))


                   - NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                                 t.RemarkType == "福利"
                                 && t.WX_UserName == usritem
                                   && t.WX_SourceType == cb_SourceType
                                    && t.aspnet_UserID == UserKey
                     && t.ChangeLocalDay.StartsWith(item)
                    ).Sum(t => t.ChangePoint))


                    );
            }
            newr3.SetField("全部玩家",

                    NetFramework.Util_Math.NullToZero(buys.Where(t =>

                     t.GameLocalPeriod.StartsWith(item)
                        && t.aspnet_UserID == UserKey
                    ).Sum(t => t.Buy_Point))

                                        - NetFramework.Util_Math.NullToZero(buys.Where(t =>

                     t.GameLocalPeriod.StartsWith(item)
                        && t.aspnet_UserID == UserKey
                    ).Sum(t => t.Result_Point))

                    - NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                                 t.RemarkType == "福利"
                                    && t.aspnet_UserID == UserKey
                     && t.ChangeLocalDay.StartsWith(item)
                    ).Sum(t => t.ChangePoint))

                    );




            #region 总期数

            DataRow newr8 = Result.NewRow();
            newr8.SetField("类别", item + "期数");
            foreach (var usritem in myWXUSERS)
            {
                newr8.SetField(usritem, NetFramework.Util_Math.NullToZero(db.WX_UserGameLog.Where(t =>
                    t.WX_UserName == usritem
                      && t.WX_SourceType == cb_SourceType
                        && t.aspnet_UserID == UserKey
                     && t.GameLocalPeriod.StartsWith(item)
                    ).Select(t => t.GamePeriod).Distinct().Count()));
            }
            newr8.SetField("全部玩家", NetFramework.Util_Math.NullToZero(db.WX_UserGameLog.Where(t =>
                          t.aspnet_UserID == UserKey
                     && t.GameLocalPeriod.StartsWith(item)
                    ).Select(t => new { t.GamePeriod, t.WX_UserName, t.WX_SourceType }).Distinct().Count()));

            #endregion

            #region "平均值"

            DataRow newr7 = Result.NewRow();
            newr7.SetField("类别", item + "平均");
            foreach (var usritem in myWXUSERS)
            {

                decimal e_TotalBuy = NetFramework.Util_Math.NullToZero(buys.Where(t =>
                     t.WX_UserName == usritem
                        && t.GameLocalPeriod.StartsWith(item)
                          && t.WX_SourceType == cb_SourceType
                           && t.aspnet_UserID == UserKey
                       ).Sum(t => t.Buy_Point));

                decimal e_TotalPeriod = NetFramework.Util_Math.NullToZero(db.WX_UserGameLog.Where(t =>
                    t.WX_UserName == usritem
                      && t.WX_SourceType == cb_SourceType
                        && t.aspnet_UserID == UserKey
                     && t.GameLocalPeriod.StartsWith(item)
                    ).Select(t => t.GamePeriod).Distinct().Count());


                newr7.SetField(usritem, (e_TotalPeriod == 0 ? 0 : e_TotalBuy / e_TotalPeriod));
            }

            decimal TotalBuy = NetFramework.Util_Math.NullToZero(buys.Where(t =>

                    t.GameLocalPeriod.StartsWith(item)
                       && t.aspnet_UserID == UserKey
                   ).Sum(t => t.Buy_Point));

            decimal TotalPeriod = NetFramework.Util_Math.NullToZero(db.WX_UserGameLog.Where(t =>

                     t.aspnet_UserID == UserKey
                 && t.GameLocalPeriod.StartsWith(item)
                ).Select(t => t.GamePeriod).Distinct().Count());

            newr7.SetField("全部玩家", (TotalPeriod == 0 ? 0 : TotalBuy / TotalPeriod));


            #endregion

            Result.Rows.Add(newr);
            Result.Rows.Add(newr2);
            Result.Rows.Add(newr6);
            Result.Rows.Add(newr3);
            Result.Rows.Add(newr8);
            Result.Rows.Add(newr7);

        }//每日循环

        #endregion
        #region 所有天数
        DataRow newr_alldays = Result.NewRow();

        newr_alldays.SetField("类别", "所有天数下注");
        foreach (var usritem in myWXUSERS)
        {
            newr_alldays.SetField(usritem, NetFramework.Util_Math.NullToZero(buys.Where(t =>
                t.WX_UserName == usritem
                  && t.WX_SourceType == cb_SourceType
                && t.aspnet_UserID == UserKey

                ).Sum(t => t.Buy_Point)));
        }
        newr_alldays.SetField("全部玩家", NetFramework.Util_Math.NullToZero(buys.Where(t =>
                     t.aspnet_UserID == UserKey
                 ).Sum(t => t.Buy_Point)));




        DataRow newr2_alldays = Result.NewRow();
        newr2_alldays.SetField("类别", "所有天数得分");
        foreach (var usritem in myWXUSERS)
        {
            newr2_alldays.SetField(usritem, NetFramework.Util_Math.NullToZero(buys.Where(t =>
                t.WX_UserName == usritem
                  && t.WX_SourceType == cb_SourceType
                   && t.aspnet_UserID == UserKey

                ).Sum(t => t.Result_Point)));
        }
        newr2_alldays.SetField("全部玩家", NetFramework.Util_Math.NullToZero(buys.Where(t =>

                  t.aspnet_UserID == UserKey
                ).Sum(t => t.Result_Point)));



        DataRow newr6_alldays = Result.NewRow();
        newr6_alldays.SetField("类别", "所有天数福利");
        foreach (var usritem in myWXUSERS)
        {
            newr6_alldays.SetField(usritem, NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                t.WX_UserName == usritem
                  && t.WX_SourceType == cb_SourceType
                 && t.RemarkType == "福利"
                    && t.aspnet_UserID == UserKey
                     && (string.Compare(t.ChangeLocalDay, dtp_startdate.ToString("yyyyMMdd")) >= 0)
                                && (string.Compare(dtp_enddate.ToString("yyyyMMdd"), t.ChangeLocalDay) >= 0)
      ).Sum(t => t.ChangePoint)));
        }
        newr6_alldays.SetField("全部玩家", NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                             t.RemarkType == "福利"
                                && t.aspnet_UserID == UserKey

                                && (string.Compare(t.ChangeLocalDay, dtp_startdate.ToString("yyyyMMdd")) >= 0)
                                && (string.Compare(dtp_enddate.ToString("yyyyMMdd"), t.ChangeLocalDay) >= 0)

                ).Sum(t => t.ChangePoint)));



        DataRow newr3_alldays = Result.NewRow();
        newr3_alldays.SetField("类别", "所有天数合计");
        foreach (var usritem in myWXUSERS)
        {
            newr3_alldays.SetField(usritem,

                NetFramework.Util_Math.NullToZero(buys.Where(t =>
                t.WX_UserName == usritem
                  && t.WX_SourceType == cb_SourceType
                   && t.aspnet_UserID == UserKey

                ).Sum(t => t.Buy_Point))

                                    - NetFramework.Util_Math.NullToZero(buys.Where(t =>
                t.WX_UserName == usritem
                  && t.WX_SourceType == cb_SourceType
                   && t.aspnet_UserID == UserKey

                ).Sum(t => t.Result_Point))


               - NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                             t.RemarkType == "福利"

                             && t.WX_UserName == usritem
                               && t.WX_SourceType == cb_SourceType
                                && t.aspnet_UserID == UserKey
                                 && (string.Compare(t.ChangeLocalDay, dtp_startdate.ToString("yyyyMMdd")) >= 0)
                                && (string.Compare(dtp_enddate.ToString("yyyyMMdd"), t.ChangeLocalDay) >= 0)
).Sum(t => t.ChangePoint))


                );
        }
        newr3_alldays.SetField("全部玩家",

                NetFramework.Util_Math.NullToZero(buys.Where(t =>


                     t.aspnet_UserID == UserKey
                ).Sum(t => t.Buy_Point))

                                    - NetFramework.Util_Math.NullToZero(buys.Where(t =>


                     t.aspnet_UserID == UserKey
                ).Sum(t => t.Result_Point))

                - NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                             t.RemarkType == "福利"
                                && t.aspnet_UserID == UserKey
 && (string.Compare(t.ChangeLocalDay, dtp_startdate.ToString("yyyyMMdd")) >= 0)
                                && (string.Compare(dtp_enddate.ToString("yyyyMMdd"), t.ChangeLocalDay) >= 0)
                ).Sum(t => t.ChangePoint))

                );



        #region 总期数

        DataRow newr8_alldays = Result.NewRow();
        newr8_alldays.SetField("类别", "所有天数期数");
        foreach (var usritem in myWXUSERS)
        {
            newr8_alldays.SetField(usritem, NetFramework.Util_Math.NullToZero(db.WX_UserGameLog.Where(t =>
                t.WX_UserName == usritem
                  && t.WX_SourceType == cb_SourceType.ToString()
                    && t.aspnet_UserID == UserKey
                    && (string.Compare(t.GameLocalPeriod, dtp_startdate.ToString("yyyyMMdd")) >= 1)
                      && (string.Compare(t.GameLocalPeriod, dtp_enddate.ToString("yyyyMMdd")) <= 1)
                ).Select(t => t.GamePeriod).Distinct().Count()));
        }
        newr8_alldays.SetField("全部玩家", NetFramework.Util_Math.NullToZero(db.WX_UserGameLog.Where(t =>
                     t.aspnet_UserID == UserKey
                      && (string.Compare(t.GameLocalPeriod, dtp_startdate.ToString("yyyyMMdd")) >= 1)
                      && (string.Compare(t.GameLocalPeriod, dtp_enddate.ToString("yyyyMMdd")) <= 1)
                ).Select(t => t.GamePeriod).Distinct().Count()));

        #endregion

        #region "平均值"

        DataRow newr7_alldays = Result.NewRow();
        newr7_alldays.SetField("类别", "所有天数平均");
        foreach (var usritem in myWXUSERS)
        {

            decimal e_TotalBuy = NetFramework.Util_Math.NullToZero(buys.Where(t =>
                 t.WX_UserName == usritem
                   && t.WX_SourceType == cb_SourceType
                       && t.aspnet_UserID == UserKey
                   ).Sum(t => t.Buy_Point));

            decimal e_TotalPeriod = NetFramework.Util_Math.NullToZero(db.WX_UserGameLog.Where(t =>
                t.WX_UserName == usritem
                  && t.WX_SourceType == cb_SourceType
                    && t.aspnet_UserID == UserKey
                     && (string.Compare(t.GameLocalPeriod, dtp_startdate.ToString("yyyyMMdd")) >= 1)
                      && (string.Compare(t.GameLocalPeriod, dtp_enddate.ToString("yyyyMMdd")) <= 1)
                ).Select(t => t.GamePeriod).Distinct().Count());


            newr7_alldays.SetField(usritem, (e_TotalPeriod == 0 ? 0 : e_TotalBuy / e_TotalPeriod));
        }

        decimal TotalBuy_alldays = NetFramework.Util_Math.NullToZero(buys.Where(t =>


                    t.aspnet_UserID == UserKey
               ).Sum(t => t.Buy_Point));

        decimal TotalPeriod_alldays = NetFramework.Util_Math.NullToZero(db.WX_UserGameLog.Where(t =>

                 t.aspnet_UserID == UserKey

            ).Select(t => t.GamePeriod).Distinct().Count());

        newr7_alldays.SetField("全部玩家", (TotalPeriod_alldays == 0 ? 0 : TotalBuy_alldays / TotalPeriod_alldays));


        #endregion

        Result.Rows.Add(newr_alldays);
        Result.Rows.Add(newr2_alldays);
        Result.Rows.Add(newr6_alldays);
        Result.Rows.Add(newr3_alldays);
        Result.Rows.Add(newr8_alldays);
        Result.Rows.Add(newr7_alldays);

        #endregion
        return Result;
    }

    [WebMethod]
    public string WX_PCSendPicSetting_SingleOrDefault(string hwnd)
    {
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        WeixinRobotLib.Entity.Linq.WX_PCSendPicSetting pcset = db.WX_PCSendPicSetting.SingleOrDefault(t => t.WX_UserTMPID == hwnd);
        return JsonConvert.SerializeObject(hwnd);
    }
    [WebMethod]
    public string MessageRobootDo(String RawContent, String WX_SourceType, String UserNameOrRemark, String FromUserNameTEMPID, String ToUserNameTEMPID, string JavaMsgTime, string msgType, Boolean IsTalkGroup, String MyUserTEMPID, string Jusrpar)
    {
        try
        {

            String SavePath = HttpContext.Current.Server.MapPath("~/PIC");
            WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(Jusrpar);

            if (Membership.ValidateUser(usrpar.UserName, usrpar.Password) == false)
            {
                return "授权失败";
            }
            String NewContent = (RawContent.Contains(":\n") ? (RawContent.Split(":\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1]) : RawContent);
            return WeixinRobotLib.Linq.ProgramLogic.MessageRobotDo(WX_SourceType, FromUserNameTEMPID, ToUserNameTEMPID, NewContent, JavaMsgTime, msgType, IsTalkGroup, MyUserTEMPID, usrpar, SavePath);

        }
        catch (Exception AnyError)
        {
            return "服务器错误" + AnyError.Message;

        }
    }
    [WebMethod]
    public string GetLastGamePeriod(String JShiShiCaiMode)
    {
        WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode ssm = (WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode)Enum.Parse(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), JShiShiCaiMode);
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);
        return db.Game_Result.Where(t => t.GameName == Enum.GetName(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), ssm)).OrderByDescending(t => t.GameTime).First().GamePeriod;

    }
    public enum PicType
    {
        龙虎图, 龙虎图_易信, 龙虎图_QQ, 龙虎图_钉钉, 数字图, 数字龙虎图,
        数字文本
        , 龙虎数字文本, 龙虎数字文本_易信, 龙虎数字文本_QQ,
        龙虎数字文本_钉钉
            , 龙虎数字大单牛牛文本, 龙虎数字大单牛牛文本_易信, 龙虎数字大单牛牛文本_QQ,
        龙虎数字大单牛牛文本_钉钉
            , 龙虎数字无大单文本, 龙虎数字无大单文本_易信, 龙虎数字无大单文本_QQ, 龙虎数字无大单文本_钉钉
    }
    [WebMethod]
    public string GetLastGamePIC(String JShiShiCaiMode, String JPICType, String Jusrpar)
    {
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(Jusrpar);

        PicType pPICType = (PicType)Enum.Parse(typeof(PicType), JPICType);
        WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode subm = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode>(JShiShiCaiMode);
        string UserName = Membership.GetUser(usrpar.UserKey).UserName;
        WeixinRobotLib.Linq.ProgramLogic.DrawChongqingshishicai(subm, usrpar.UserKey);
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSQLServer"].ConnectionString);

        switch (pPICType)
        {
            case PicType.龙虎图:
                return WeixinRobotLib.Linq.ProgramLogic.ReadVirtualFile("Data3" + UserName + "_" + (Enum.GetName(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", db, usrpar.UserKey);


            case PicType.龙虎图_易信:
                return WeixinRobotLib.Linq.ProgramLogic.ReadVirtualFile("Data3_yixin" + UserName + "_" + (Enum.GetName(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", db, usrpar.UserKey);


            case PicType.龙虎图_QQ:
                return WeixinRobotLib.Linq.ProgramLogic.ReadVirtualFile("Data3_QQ" + UserName + "_" + (Enum.GetName(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", db, usrpar.UserKey);


            case PicType.龙虎图_钉钉:
                return WeixinRobotLib.Linq.ProgramLogic.ReadVirtualFile("Data3_dingding" + UserName + "_" + (Enum.GetName(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", db, usrpar.UserKey);


            case PicType.数字图:
                return WeixinRobotLib.Linq.ProgramLogic.ReadVirtualFile("Data" + UserName + "_" + (Enum.GetName(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".jpg", db, usrpar.UserKey);


            case PicType.数字龙虎图:
                return WeixinRobotLib.Linq.ProgramLogic.ReadVirtualFile("Data" + UserName + "_" + (Enum.GetName(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), subm)) + "_v3.jpg", db, usrpar.UserKey);


            case PicType.数字文本:
                return WeixinRobotLib.Linq.ProgramLogic.ReadVirtualFile("Data数字龙虎" + UserName + "_" + (Enum.GetName(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), subm)) + "V7.txt", db, usrpar.UserKey);


            case PicType.龙虎数字文本:
                return WeixinRobotLib.Linq.ProgramLogic.ReadVirtualFile("Data数字龙虎" + UserName + "_" + (Enum.GetName(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", db, usrpar.UserKey);


            case PicType.龙虎数字文本_易信:
                return "未知图类型";


            case PicType.龙虎数字文本_QQ:
                return WeixinRobotLib.Linq.ProgramLogic.ReadVirtualFile("Data数字龙虎qq" + UserName + "_" + (Enum.GetName(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", db, usrpar.UserKey);


            case PicType.龙虎数字文本_钉钉:
                return WeixinRobotLib.Linq.ProgramLogic.ReadVirtualFile("Data数字龙虎dingding" + UserName + "_" + (Enum.GetName(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", db, usrpar.UserKey);



            case PicType.龙虎数字大单牛牛文本:
                return WeixinRobotLib.Linq.ProgramLogic.ReadVirtualFile("Data数字龙虎_五分龙虎Vr牛牛" + UserName + "_" + (Enum.GetName(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", db, usrpar.UserKey);


            case PicType.龙虎数字大单牛牛文本_易信:
                return "未知图类型_易信图类型";

            case PicType.龙虎数字大单牛牛文本_QQ:
                return WeixinRobotLib.Linq.ProgramLogic.ReadVirtualFile("Data数字龙虎qq_五分龙虎Vr牛牛" + UserName + "_" + (Enum.GetName(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", db, usrpar.UserKey);


            case PicType.龙虎数字大单牛牛文本_钉钉:
                return WeixinRobotLib.Linq.ProgramLogic.ReadVirtualFile("Data数字龙虎dingding_五分龙虎Vr牛牛" + UserName + "_" + (Enum.GetName(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", db, usrpar.UserKey);


            case PicType.龙虎数字无大单文本:
                return WeixinRobotLib.Linq.ProgramLogic.ReadVirtualFile("Data数字龙虎_五分龙虎" + UserName + "_" + (Enum.GetName(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", db, usrpar.UserKey);


            case PicType.龙虎数字无大单文本_易信:
                return "未知图类型";

            case PicType.龙虎数字无大单文本_QQ:
                return WeixinRobotLib.Linq.ProgramLogic.ReadVirtualFile("Data数字龙虎qq_五分龙虎" + UserName + "_" + (Enum.GetName(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", db, usrpar.UserKey);


            case PicType.龙虎数字无大单文本_钉钉:
                return WeixinRobotLib.Linq.ProgramLogic.ReadVirtualFile("Data数字龙虎dingding_五分龙虎" + UserName + "_" + (Enum.GetName(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), subm)) + ".txt", db, usrpar.UserKey);


            default:
                return "未知图类型";

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
    [WebMethod]
    public void ShiShiCaiDealGameLogSaveNotice(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode subm, bool IgoreDataSettingSend = true, bool IgoreMemberGroup = false, string Jusrpar = "")
    {



        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
        ////db.ObjectTrackingEnabled = false;
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(Jusrpar);

        WeixinRobotLib.Entity.Linq.aspnet_UsersNewGameResultSend checkus = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == usrpar.UserKey);
        string LastPeriod = db.Game_Result.Where(t => t.GameName == subm.ToString()

            ).OrderByDescending(t => t.GamePeriod).First().GamePeriod;

        if ((checkus != null && checkus.IsNewSend == true) || (IgoreDataSettingSend == true))
        {


            #region "发送余额"
            var noticeChangelist = db.WX_UserGameLog.Where(t => t.Result_HaveProcess == false
                && t.aspnet_UserID == usrpar.UserKey
                && (t.WX_SourceType == "微安")
               && (string.Compare(t.GamePeriod, (t.OpenMode == "澳洲幸运5" || t.OpenMode == "VR重庆时时彩" ? "" : "20") + LastPeriod) <= 0)

                ).Select(t => new { t.WX_UserName, t.WX_SourceType, t.MemberGroupName, t.GamePeriod ,t.aspnet_UserID}).Distinct().ToArray();

            var lst_membergroup = (from dssub in
                                       (from ds in noticeChangelist
                                        select new { ds.MemberGroupName, ds.WX_SourceType }).Distinct()
                                   select new TMP_MemberGroup(dssub.MemberGroupName, dssub.WX_SourceType)).ToArray();



            foreach (var notice_item in noticeChangelist)
            {

                Int32 TotalChanges = WeixinRobotLib.Linq.ProgramLogic.WX_UserGameLog_ServerDeal(notice_item.WX_UserName, notice_item.WX_SourceType);
                if (TotalChanges == 0)
                {
                    continue;
                }

                decimal? ReminderMoney = WeixinRobotLib.Linq.ProgramLogic.WXUserChangeLog_GetRemainder(notice_item.WX_UserName, notice_item.WX_SourceType, usrpar.UserKey);

                var Rows = db.WX_UserReply.Where(t => t.WX_UserName == notice_item.WX_UserName.Replace("'", "''") && t.WX_SourceType == notice_item.WX_SourceType);

                string TEMPUserName = Rows.First().WeChatID;
                string SourceType = Rows.First().WX_SourceType;

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

                        var sets = db.WX_PCSendPicSetting.Where(t => t.aspnet_UserID == usrpar.UserKey
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
                            string noticeTEMPUserName = noticeitem.WeChatID;
                            SendRobotContent("已开奖，可继续下注，余" + (ReminderMoney.HasValue ? ReminderMoney.Value.ToString("N0") : ""), noticeTEMPUserName, notice_item.WX_SourceType, usrpar);

                        }

                    }
                }
                #endregion
                var updatechangelog = db.WX_UserChangeLog.Where(t => t.aspnet_UserID == usrpar.UserKey && t.WX_UserName == notice_item.WX_UserName && t.WX_SourceType == notice_item.WX_SourceType && t.NeedNotice == false);
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
                var sets = db.WX_UserReply.Where(t => t.aspnet_UserID == usrpar.UserKey
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

                WeixinRobotLib.Linq.ProgramLogic.ChongQingShiShiCaiCaculatePeriod(DateTime.Now, "", db, "", "", out GameFullPeriod, out GameFullLocalPeriod, true, out ShiShiCaiSuccess, out ShiShiCaiErrorMessage, subm, usrpar.UserKey);

                NextSubPeriod = GameFullPeriod.Substring(GameFullPeriod.Length - 3, 3);
                ReturnSend += "战斗胜负数据如下：" + Environment.NewLine;

                var buyusers = noticeChangelist.Where(t => t.MemberGroupName == memberite.MemberGroupName && t.WX_SourceType == memberite.WX_SourceType).Select(t => new { t.WX_UserName, t.WX_SourceType, t.GamePeriod ,t.aspnet_UserID}).Distinct();
                foreach (var useritem in buyusers)
                {


                    decimal? ReminderMoney = WeixinRobotLib.Linq.ProgramLogic.WXUserChangeLog_GetRemainder(useritem.WX_UserName, useritem.WX_SourceType, useritem.aspnet_UserID);
                    ReturnSend += "[" + useritem.WX_UserName + "]本期盈亏:"
                        + WeixinRobotLib.Linq.ProgramLogic.GetUserPeriodInOut(useritem.GamePeriod, useritem.WX_UserName, useritem.WX_SourceType, db, usrpar.UserKey).ToString("N0")
                        + ",总分:" + (ReminderMoney.HasValue ? ReminderMoney.Value.ToString() : "0") + Environment.NewLine;
                    ReturnSend += "---------------" + Environment.NewLine;



                }

                ReturnSend += "====================" + Environment.NewLine
                    + Enum.GetName(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), subm) + "【" + NextSubPeriod + "回合】" + Environment.NewLine
                            + "开战！" + Environment.NewLine
                            + "请各指挥官开始进攻！" + Environment.NewLine
                            + "====================" + Environment.NewLine;

                if (lst_membergroup.Count() != 0)
                {
                    SendRobotContent(ReturnSend, memberite.TMPID, memberite.WX_SourceType, usrpar);
                    //foreach (var setitem in sets)
                    //{
                    //    setitem.NextSendString = ReturnSend;
                    //}
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

            //NetFramework.Console.WriteLine("开奖完成" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), false);
        }
    }

    public String SendRobotContent(String Content, String TempToUserID, String WX_SourceType, WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar)
    {
        switch (WX_SourceType)
        {
            case "易":
                return "";

            case "微":
                return "";
            case "安微":
                return SendAndroidWXContent(Content, TempToUserID, usrpar.UserKey);
            default:
                return "";

        }

    }
    public string SendAndroidWXContent(string Content, string TempToUserID, Guid UserID)
    {
        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);

        var replys = db.WX_UserReply.Where(t => t.aspnet_UserID == UserID && t.WeChatID == TempToUserID);

        WeixinRobotLib.Entity.Linq.aspnet_UserSendJob newjob = new WeixinRobotLib.Entity.Linq.aspnet_UserSendJob();
        newjob.aspnet_Userid = UserID;
        newjob.ToSendMessage = Content;
        newjob.WX_UserName = (replys.Count() == 0 ? "找不到" + TempToUserID : replys.First().WX_UserName);
        newjob.WechatID = TempToUserID;
        newjob.Status = "未发";
        db.aspnet_UserSendJob.InsertOnSubmit(newjob);
        db.SubmitChanges();
        return "";
    }

    [WebMethod]
    public string UploadContacts(string Jcontacts, string Jusrpar, string WX_SourceType)
    {
        try
        {


            WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(Jusrpar);

            AndroidContact[] contacts = JsonConvert.DeserializeObject<AndroidContact[]>(Jcontacts);
            foreach (var item in contacts)
            {
                string Seq = (item.conRemark == null || item.conRemark == "") ? item.nickname : item.conRemark;
                WeixinRobotLib.Entity.Linq.WX_UserReply usrc = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == usrpar.UserKey && t.WX_UserName == Seq && t.WX_SourceType == WX_SourceType);

                if (usrc == null)
                {

                    WeixinRobotLib.Entity.Linq.WX_UserReply newusrc = new WeixinRobotLib.Entity.Linq.WX_UserReply();
                    newusrc.aspnet_UserID = usrpar.UserKey;
                    newusrc.WX_UserName = Seq;
                    newusrc.WX_SourceType = WX_SourceType;
                    newusrc.RemarkName = item.conRemark;
                    newusrc.NickName = NetFramework.Util_WEB.CleanHtml(item.nickname);
                    newusrc.WeChatID = item.username;
                    newusrc.IsCaculateFuli = true;
                    if (Seq.EndsWith("@chatroom") == false)
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
                    if ((usrc.RemarkName != item.conRemark) || (usrc.NickName != NetFramework.Util_WEB.CleanHtml(item.nickname))
                        )
                    {

                        usrc.RemarkName = item.conRemark;
                        usrc.NickName = NetFramework.Util_WEB.CleanHtml(item.nickname);

                    }
                    //if (UserNametempID.StartsWith("@@") == false && Seq != "0")
                    {
                        usrc.IsReply = true;
                    }
                    usrc.WeChatID = item.username;
                    db.SubmitChanges();
                } //初始化，添加到数据库或同步数据库
                WeixinRobotLib.Entity.Linq.WX_WebSendPICSetting webpcset = db.WX_WebSendPICSetting.SingleOrDefault(t => t.aspnet_UserID == usrpar.UserKey
                   && t.WX_SourceType == WX_SourceType
                    && t.WX_UserName == Seq
                   );
                if (webpcset == null)
                {
                    webpcset = new WeixinRobotLib.Entity.Linq.WX_WebSendPICSetting();

                    webpcset.aspnet_UserID = usrpar.UserKey;

                    webpcset.WX_SourceType = WX_SourceType;
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



                usrc = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == usrpar.UserKey && t.WX_UserName == Seq && t.WX_SourceType == WX_SourceType);
            }
            return "上传成功";
        }//try
        catch (Exception Anyerror)
        {

            return "错误:" + Anyerror.Message;
        }//catch

    }//fun end
    public class AndroidContact
    {
        public string username { get; set; }
        public string nickname { get; set; }
        public string conRemark { get; set; }
        public string type { get; set; }
    }

    [WebMethod]
    public String GetAndroidWeChatContacts(string WX_Sourcetype, string Jusrpar)
    {
        try
        {

            WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(Jusrpar);
            var datas = db.WX_UserReply.Where(t => t.aspnet_UserID == usrpar.UserKey && t.WX_SourceType == WX_Sourcetype).ToArray();
            return JsonConvert.SerializeObject(datas);
        }//try
        catch (Exception Anyerror)
        {

            return "错误:" + Anyerror.Message;
        }//catch
    }

    [WebMethod]
    public String GetSendJobs(string WX_Sourcetype, string Jusrpar)
    {

        WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
        ////db.ObjectTrackingEnabled = false;
        WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam usrpar = JsonConvert.DeserializeObject<WeixinRobotLib.Entity.Linq.ProgramLogic.UserParam>(Jusrpar);

        return JsonConvert.SerializeObject(db.aspnet_UserSendJob.Where(t => t.aspnet_Userid == usrpar.UserKey && t.Status == "未发").ToArray());

    }
    [WebMethod]
    public String UpdateSendJobs(string WX_Sourcetype, String Userid, Int32 Jobid)
    {
        try
        {

            WeixinRobotLib.Entity.Linq.dbDataContext db = new WeixinRobotLib.Entity.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ////db.ObjectTrackingEnabled = false;
            WeixinRobotLib.Entity.Linq.aspnet_UserSendJob updjob = db.aspnet_UserSendJob.SingleOrDefault(t => t.aspnet_Userid == Guid.Parse(Userid)
                && t.Joibid == Jobid
                );
            updjob.Status = "已发";
            db.SubmitChanges();
            return "成功";

        }
        catch (Exception AnyError)
        {

            return "错误：" + AnyError.Message;
        }
    }


    [WebMethod]
    public void ShiShiCaiServerDealGameLogAndNotice(String JShiShiCaiMode, bool IgoreDataSettingSend = true, bool IgoreMemberGroup = false)
    {
        WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode subm = (WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode)Enum.Parse(typeof(WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode), JShiShiCaiMode);
        WeixinRobotLib.Linq.ProgramLogic.ShiShiCaiServerDealGameLogAndNotice(subm, IgoreDataSettingSend, IgoreMemberGroup);
    }

}//class end 

