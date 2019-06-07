using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
namespace WeixinRoboot
{
    public partial class RunnerForm : Form
    {
        public JObject _Members = null;
        public DataTable MemberSource = null;
        public DataTable ReplySource = null;


        public Boolean MembersSet_firstrun { get; set; }

        public void MembersSet(JObject value)
        {


            _Members = value;
            // SetMembers();
            System.Threading.Thread asyncData = new System.Threading.Thread(new System.Threading.ThreadStart(SetMembers));

            asyncData.Start();



            //Set结束
        }

        Boolean SetMemberRuning = false;
        private void SetMembers()
        {
            NetFramework.Console.WriteLine("开始更新更新联系人" + DateTime.Now.ToString("yyyy-MM-dd HH::mm:ss:fff"), true);
            DateTime StartTime = DateTime.Now;

            //if (SetMemberRuning == true)
            //{
            //    return;
            //}

            SetMemberRuning = true;
            DateTime EachStart = DateTime.Now;
            this.Invoke(new Action(() =>
            {
                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                var MyUsers = db.WX_UserReply.Where(t => t.aspnet_UserID == GlobalParam.UserKey).ToList();
                var MyUserSetss = db.WX_WebSendPICSetting.Where(t => t.aspnet_UserID == GlobalParam.UserKey).ToList();
                //db.ObjectTrackingEnabled = false;
                //this.Invoke(new Action(() => { BS_Contact.DataSource = null; }));
                foreach (var item in (_Members["MemberList"]) as JArray)
                {
                    EachStart = DateTime.Now;
                    string UserNametempID = "";
                    string NickName = "";
                    string RemarkName = "";
                    string HeadImgUrl = "";
                    try
                    {

                        UserNametempID = (item["UserName"] as JValue).Value.ToString();
                        NickName = (item["NickName"] as JValue).Value.ToString();
                        RemarkName = (item["RemarkName"] as JValue).Value.ToString();
                        HeadImgUrl = (item["HeadImgUrl"] as JValue).Value.ToString();

                        //NetFramework.Console.WriteLine("更新联系人" + NickName);
                        //Application.DoEvents();

                        System.Text.RegularExpressions.Regex FindSeq = new System.Text.RegularExpressions.Regex("seq=([0-9])+");

                        string Seq = FindSeq.Match(HeadImgUrl).Value;
                        //Seq = Seq.Substring(Seq.IndexOf("=") + 1);

                        Seq = RemarkName == "" ? NetFramework.Util_WEB.CleanHtml(NickName) : RemarkName;
                        //if (Seq.Contains("-"))
                        //{
                        //    string[] Names = Seq.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        //    Seq = Names[Names.Length-1];
                        //}
                        Linq.WX_UserReply usrc = MyUsers.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == Seq && t.WX_SourceType == "微");
                        if (usrc == null)
                        {
                            Linq.WX_UserReply newusrc = new Linq.WX_UserReply();
                            newusrc.aspnet_UserID = GlobalParam.UserKey;
                            newusrc.WX_UserName = Seq;
                            newusrc.WX_SourceType = "微";
                            newusrc.RemarkName = RemarkName;
                            newusrc.NickName = NetFramework.Util_WEB.CleanHtml(NickName);

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
                            MyUsers.Add(newusrc);
 ;
                        } //初始化，添加到数据库或同步数据库
                        else
                        {
                            if ((usrc.RemarkName != RemarkName) || (usrc.NickName != NetFramework.Util_WEB.CleanHtml(NickName))
                                )
                            {

                                usrc.RemarkName = RemarkName;
                                usrc.NickName = NetFramework.Util_WEB.CleanHtml(NickName);

                            }
                            //if (UserNametempID.StartsWith("@@") == false && Seq != "0")
                            {
                                usrc.IsReply = true;
                            }
                        } //初始化，添加到数据库或同步数据库
                        Linq.WX_WebSendPICSetting webpcset = MyUserSetss.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                           && t.WX_SourceType == "微"
                            && t.WX_UserName == Seq
                           );
                        if (webpcset == null)
                        {
                            webpcset = new Linq.WX_WebSendPICSetting();

                            webpcset.aspnet_UserID = GlobalParam.UserKey;

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
                            MyUserSetss.Add(webpcset);
                        }

                        NetFramework.Console.WriteLine("准备提交,耗时:" + (DateTime.Now - EachStart).TotalSeconds.ToString(), true);


                        usrc = MyUsers.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == Seq && t.WX_SourceType == "微");

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


                        NetFramework.Console.WriteLine("单个联系人,耗时:" + (DateTime.Now - EachStart).TotalSeconds.ToString(), true);

                        //var UpdateLogs = ReplySource.AsEnumerable().Where(t => t.Field<string>("Reply_ContactID") == Seq);
                        //foreach (var logitem in UpdateLogs)
                        //{
                        //    logitem.SetField("Reply_ContactTEMPID", UserNametempID);
                        //    logitem.SetField("Reply_Contact", RemarkName == "" ? NickName : RemarkName);
                        //}

                    }
                    catch (Exception AnyError)
                    {
                        MessageBox.Show((RemarkName == "" ? NickName : RemarkName) + "联系人保存失败");
                        NetFramework.Console.WriteLine(AnyError.Message, true);
                        NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                    }
                    this.Invalidate();
                    Application.DoEvents();

                }//成员列表循环
                EachStart = DateTime.Now;
                db.SubmitChanges();



                NetFramework.Console.WriteLine("数据库,耗时:" + (DateTime.Now - EachStart).TotalSeconds.ToString(), true);


                // BS_Contact.Sort = "User_Contact";
            }));//Invoke

            SetMemberRuning = false;

            //this.Invoke(new Action(() => { BS_Contact.DataSource = MemberSource; }));
            NetFramework.Console.WriteLine("更新联系人完成,耗时:" + (DateTime.Now - StartTime).TotalSeconds.ToString(), true);



        }

        public JObject RoomMembers = null;
        public DataTable RoomMerbersSource = null;


        public void SetYixinMembers(List<StartForm.YixinContact> contact, List<StartForm.YixinContactInfo> contactinf)
        {
            NetFramework.Console.WriteLine("开始更新更新易信联系人" + DateTime.Now.ToString("yyyy-MM-dd HH::mm:ss:fff"), false);
            try
            {

                if (contactinf.Count == 0)
                {
                    return;
                }
                this.Invoke(new Action(() =>
                {
                    Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                    //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                    //db.ObjectTrackingEnabled = false;
                    BS_Contact.DataSource = null;
                    foreach (var item in contact)
                    {
                        StartForm.YixinContactInfo yf = contactinf.SingleOrDefault(t => t.ContactID == item.ContactID);

                        string UserNametempID = item.ContactID;
                        string NickName = "";
                        if (yf != null)
                        {
                            NickName = yf.ContactName;
                        }


                        string RemarkName = item.ContactRemarkName;


                        //NetFramework.Console.WriteLine("更新联系人" + NickName);
                        //Application.DoEvents();




                        Linq.WX_UserReply usrc = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == item.ContactID && t.WX_SourceType == "易");
                        if (usrc == null)
                        {
                            Linq.WX_UserReply newusrc = new Linq.WX_UserReply();
                            newusrc.aspnet_UserID = GlobalParam.UserKey;
                            newusrc.WX_UserName = item.ContactID;
                            newusrc.RemarkName = RemarkName;
                            newusrc.NickName = NetFramework.Util_WEB.CleanHtml(NickName);
                            newusrc.WX_SourceType = "易";
                            newusrc.IsCaculateFuli = true;
                            if (item.ContactType == "个人")
                            {
                                newusrc.IsReply = true;
                            }
                            else
                            {
                                newusrc.IsReply = false;
                            }
                            newusrc.FiveMinuteMode = false;
                            newusrc.HkMode = false;
                            newusrc.AozcMode = false;
                            db.WX_UserReply.InsertOnSubmit(newusrc);


                        } //初始化，添加到数据库或同步数据库
                        else
                        {
                            if ((usrc.RemarkName != RemarkName) || (usrc.NickName != NetFramework.Util_WEB.CleanHtml(NickName))
                                )
                            {

                                usrc.RemarkName = RemarkName;
                                usrc.NickName = NetFramework.Util_WEB.CleanHtml(NickName);

                            }
                            if (item.ContactType == "个人")
                            {
                                usrc.IsReply = true;
                            }



                        } //初始化，添加到数据库或同步数据库
                        db.SubmitChanges();
                        Linq.WX_WebSendPICSetting webpcset = db.WX_WebSendPICSetting.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                      && t.WX_SourceType == "易"
                       && t.WX_UserName == item.ContactID
                      );
                        if (webpcset == null)
                        {
                            webpcset = new Linq.WX_WebSendPICSetting();

                            webpcset.aspnet_UserID = GlobalParam.UserKey;

                            webpcset.WX_SourceType = "易";
                            webpcset.WX_UserName = item.ContactID;

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
                        else
                        {
                            //webpcset.IsSendPIC = false;
                            //webpcset.NiuNiuPic = false;
                            //webpcset.NoBigSmallSingleDoublePIC = false;
                            //webpcset.NumberDragonTxt = true;
                            //webpcset.NumberPIC = false;
                            //webpcset.dragonpic = false;
                            //db.SubmitChanges();
                        }
                        usrc = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == item.ContactID && t.WX_SourceType == "易");



                        DataRow[] Lists = MemberSource.Select("User_ContactID='" + item.ContactID.Replace("'", "''") + "' and User_SourceType='易'");
                        DataRow newr = null;
                        if (Lists.Length == 0)
                        {
                            newr = MemberSource.NewRow();
                            MemberSource.Rows.Add(newr);
                        }
                        else
                        {
                            newr = Lists[0];
                        }
                        newr.SetField("User_ContactID", item.ContactID);
                        newr.SetField("User_ContactTEMPID", UserNametempID);
                        newr.SetField("User_ContactType", item.ContactType);
                        newr.SetField("User_SourceType", "易");




                        newr.SetField("User_Contact", RemarkName == "" ? NickName : RemarkName);



                        newr.SetField("User_IsReply", usrc.IsReply);

                        if (item.ContactType == "个人")
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

                        newr.SetField("User_HeNeiWuFen", usrc == null ? false : (usrc.HeNeiWuFenMode == null ? false : usrc.HeNeiWuFenMode));

                        //var UpdateLogs = ReplySource.AsEnumerable().Where(t => t.Field<string>("Reply_ContactID") == Seq);
                        //foreach (var logitem in UpdateLogs)
                        //{
                        //    logitem.SetField("Reply_ContactTEMPID", UserNametempID);
                        //    logitem.SetField("Reply_Contact", RemarkName == "" ? NickName : RemarkName);
                        //}


                    }//成员列表循环

                    BS_Contact.DataSource = MemberSource;
                    // BS_Contact.Sort = "User_Contact";
                }));
            }
            catch (Exception AnyError)
            {

                NetFramework.Console.WriteLine(AnyError.Message, true);
                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
            }
            NetFramework.Console.WriteLine("更新易信联系人完成" + DateTime.Now.ToString("yyyy-MM-dd HH::mm:ss:fff"), false);

        }


        public RunnerForm()
        {
            InitializeComponent();
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            //db.ObjectTrackingEnabled = false;

            dtp_StartDate.Value = DateTime.Today.AddDays(-3);
            dtp_EndDate.Value = DateTime.Today.AddMonths(1);

            ReplySource = new DataTable();

            ReplySource.Columns.Add("Reply_Contact");
            ReplySource.Columns.Add("Reply_ContactID");
            ReplySource.Columns.Add("Reply_SourceType");
            ReplySource.Columns.Add("Reply_ContactTEMPID");
            ReplySource.Columns.Add("Reply_ReceiveContent");
            ReplySource.Columns.Add("Reply_ReplyContent");
            ReplySource.Columns.Add("Reply_ReceiveTime", typeof(DateTime));
            ReplySource.Columns.Add("Reply_ReplyTime", typeof(DateTime));



            MemberSource = new DataTable();
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

            BS_ReceiveReply.DataSource = ReplySource;


            dtp_StartDate.Value = DateTime.Today.AddDays(-3);
            dtp_EndDate.Value = DateTime.Today.AddMonths(1);

            BS_Contact.DataSource = MemberSource;

            LoadReplyLog("", "");

            TM_Refresh.Start();
        }

        private void LoadReplyLog(string SelectUser, string SourceType)
        {

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            //db.ObjectTrackingEnabled = false;
            //ReplySource.Columns.Add("Reply_Contact");
            //ReplySource.Columns.Add("Reply_ContactID");
            //ReplySource.Columns.Add("Reply_ContactTEMPID");
            //ReplySource.Columns.Add("Reply_ReceiveContent");
            //ReplySource.Columns.Add("Reply_ReplyContent");
            //ReplySource.Columns.Add("Reply_ReceiveTime", typeof(object));
            //ReplySource.Columns.Add("Reply_ReplyTime", typeof(object));

            DataTable PreRend = NetFramework.Util_Sql.RunSqlDataTable(GlobalParam.DataSourceName
                  , "Select case when ur.RemarkName<>'' then ur.RemarkName+'@#'+ur.NickName else ur.NickName end as Reply_Contact ,RL.WX_UserName as Reply_ContactID,RL.WX_SourceType as Reply_SourceType "

              + " ,'' as Reply_ContactTEMPID"
              + " ,ReceiveContent as Reply_ReceiveContent"
              + " ,ReplyContent as Reply_ReplyContent"

              + " ,ReceiveTime as ReplyReceiveTime"
              + " ,ReplyTime as Reply_ReplyTime"

                  + " from WX_UserReplyLog RL with (nolock) join WX_UserReply ur with (nolock) on RL.aspnet_UserID=ur.aspnet_UserID and RL.WX_UserName=ur.WX_UserName and   RL.WX_SourceType=ur.WX_SourceType  where RL.aspnet_UserID='" + GlobalParam.UserKey.ToString() + "' and "
                  + "ReceiveTime >='" + dtp_StartDate.Value.Date.ToString("yyyy-MM-dd") + "' and "
                  + "ReceiveTime <'" + dtp_EndDate.Value.Date.ToString("yyyy-MM-dd") + "'  "
                  + (SelectUser == "" ? "" : " and RL.WX_UserName='" + SelectUser.Replace("'", "''") + "' ")
                  + (SourceType == "" ? "" : " and RL.WX_SourceType='" + SourceType + "' ")

                  );
            if (ReplySource == null)
            {
                return;
            }
            ReplySource.Rows.Clear();
            foreach (DataRow item in PreRend.Rows)
            {
                string ContactID = item.Field<string>("Reply_ContactID");
                //string SourceType = item.Field<string>("Reply_SourceType");
                DataRow[] memusr = MemberSource.Select("User_ContactID='" + ContactID.Replace("'", "''") + "' and User_SourceType='" + SourceType + "'");
                if (memusr.Length != 0)
                {
                    item.SetField("Reply_ContactTEMPID", memusr[0].Field<string>("User_ContactTEMPID"));

                    item.SetField("Reply_Contact", memusr[0].Field<string>("User_Contact"));

                }
                else
                {
                    //item.SetField("Reply_Contact", "非此微信号联系人");

                }
                ReplySource.Rows.Add(item.ItemArray);
            }


            BS_ReceiveReply.Sort = "Reply_ContactID,Reply_ReceiveTime";


            if (dtp_StartDate.Value == null || dtp_EndDate.Value == null)
            {
                return;
            }

            var datasource = from ds in db.WX_UserReplyLog
                             join dsgame in db.WX_UserGameLog
                             on new { ds.aspnet_UserID, ds.WX_UserName, ds.ReceiveTime, ds.WX_SourceType } equals new { dsgame.aspnet_UserID, dsgame.WX_UserName, ReceiveTime = dsgame.TransTime, WX_SourceType = dsgame.WX_SourceType }
                             into leftdsggame
                             from dsgame2 in leftdsggame.DefaultIfEmpty()
                             where ds.ReceiveTime >= dtp_StartDate.Value
                             && ds.ReceiveTime < dtp_EndDate.Value
                             && ds.aspnet_UserID == GlobalParam.UserKey
                             && ds.WX_UserName == SelectUser
                             && ds.WX_SourceType == SourceType
                             orderby ds.ReceiveTime descending
                             select new
                             {
                                 ds.ReceiveTime,
                                 ds.ReceiveContent,
                                 ds.aspnet_UserID,
                                 ds.WX_UserName,
                                 ds.WX_SourceType,
                                 TransTime = (DateTime?)dsgame2.TransTime,
                                 dsgame2.GamePeriod
                                 ,
                                 GameLocalPeriod = dsgame2.GameLocalPeriod
                                 ,
                                 dsgame2.GameResult
                                 ,
                                 dsgame2.Buy_Value
                                 ,
                                 dsgame2.Buy_Type

                                 ,
                                 dsgame2.Buy_Point,
                                 dsgame2.Result_Point

                             };
            GV_GameLog.DataSource = datasource;
        }


        void RelySource_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            //db.ObjectTrackingEnabled = false;
            if (e.Row != null)
            {
                return;
            }
            Linq.WX_UserReply checkreply = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == e.Row.Field<string>("Reply_ContactID") && t.WX_SourceType == e.Row.Field<string>("Reply_SourceType"));
            if (checkreply.IsReply == true)
            {
                string Reply_ContactID = e.Row.Field<string>("Reply_ContactID");
                string Reply_SourceType = e.Row.Field<string>("Reply_SourceType");
                string Reply_ReceiveTime = e.Row.Field<string>("Reply_ReceiveTime");
                Linq.WX_UserReplyLog log = db.WX_UserReplyLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                     && t.WX_UserName == Reply_ContactID
                     && t.WX_SourceType == Reply_SourceType
                     && t.ReceiveTime == Convert.ToDateTime(Reply_ReceiveTime)
                     );
                if (log == null)
                {
                    db.WX_UserReplyLog.InsertOnSubmit(log);
                    db.SubmitChanges();
                }
            }


        }



        private void TM_Refresh_Tick(object sender, EventArgs e)
        {
            BS_Contact.Filter = "User_Contact like '%" + tb_ContactFilter.Text + "%'";
            BS_ReceiveReply.Filter = "";
            //this.Refresh();

        }




        public StartForm StartF = null;
        private void MI_FasongXinxi_Click(object sender, EventArgs e)
        {

            SendMessage SM = new SendMessage();
            SM.UserRow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;
            SM.StartF = StartF;
            SM.RunnerF = this;
            SM.Show();


        }

        private void gv_contact_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void gv_ReceiveReply_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void tb_ContactFilter_TextChanged(object sender, EventArgs e)
        {
            BS_Contact.Filter = "User_Contact like '%" + tb_ContactFilter.Text + "%'";
        }





        private void MI_ChongZhi_Click(object sender, EventArgs e)
        {

            SendCharge sc = new SendCharge();
            sc.RunnerF = this;
            sc.StartF = this.StartF;
            sc.UserRow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;
            sc.Mode = "Charge";
            sc.Show();
        }

        private void Runner_Load(object sender, EventArgs e)
        {
            TM_Refresh.Start();
        }

        private void gv_contact_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {

                return;
            }

            if (gv_contact.SelectedRows.Count != 0)
            {
                gv_contact.ContextMenuStrip = MouseMenuReply;
                gv_contact.ContextMenuStrip.Show(
                    this, this.PointToClient(MousePosition)
                    );

                LoadReplyLog(((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row.Field<string>("User_ContactID")
                    , ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row.Field<string>("User_SourceType")
                    );


            }
            else
            {

            }

        }

        private void dtp_Start_ValueChanged(object sender, EventArgs e)
        {
            LoadReplyLog("", "");
        }

        private void dtp_End_ValueChanged(object sender, EventArgs e)
        {
            LoadReplyLog("", "");
        }

        private void gv_contact_Leave(object sender, EventArgs e)
        {
            gv_contact.ClearSelection();
        }

        private void MI_OrderManual_Click(object sender, EventArgs e)
        {

            SendManulOrder SM = new SendManulOrder();
            SM.StartF = this.StartF;
            SM.RunnerF = this;
            SM.UserRow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;
            SM.Show();

        }

        private void MI_CleanUp_Click(object sender, EventArgs e)
        {

            SendCharge sc = new SendCharge();
            sc.RunnerF = this;
            sc.StartF = this.StartF;
            sc.UserRow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;
            sc.Mode = "CleanUp";
            sc.Show();
        }

        private void gv_contact_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void MI_IsReply_Click(object sender, EventArgs e)
        {


            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;



                string Result = Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("自动", editrow, DateTime.Now);


                if (Result != "")
                {
                    MessageBox.Show(Result);
                }

            }
        }

        private void MI_CancelIsReply_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("取消自动", editrow, DateTime.Now);


            }
        }

        private void MI_ReceiveTrans_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("转发", editrow, DateTime.Now);


            }
        }

        private void MI_CancelReceiveTrans_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("取消转发", editrow, DateTime.Now);


            }
        }

        private void BtnSaveAndDeal_Click(object sender, EventArgs e)
        {
            try
            {
                bool Newdb = false;
                Linq.ProgramLogic.ShiShiCaiMode subm = Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩;
                if (cb_gamemode.SelectedItem != null && cb_gamemode.SelectedItem.ToString() == "重庆时时彩")
                {
                    subm = Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩;
                }
                else if (cb_gamemode.SelectedItem != null && cb_gamemode.SelectedItem.ToString() == "五分彩")
                {
                    subm = Linq.ProgramLogic.ShiShiCaiMode.五分彩;
                }
                else if (cb_gamemode.SelectedItem != null && cb_gamemode.SelectedItem.ToString() == "香港时时彩")
                {
                    subm = Linq.ProgramLogic.ShiShiCaiMode.香港时时彩;
                }
                else if (cb_gamemode.SelectedItem != null && cb_gamemode.SelectedItem.ToString() == "澳洲幸运5")
                {
                    subm = Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5;
                }
                Linq.ProgramLogic.NewGameResult(
                            fd_Num1.Text + " " + fd_Num2.Text + " " + fd_Num3.Text + " " + fd_Num4.Text + " " + fd_Num5.Text, fd_day.Value.ToString("yyMMdd") + fd_Period.Text, ref Newdb, subm);

                if (Newdb)
                {
                    StartF.ShiShiCaiDealGameLogAndNotice(subm);
                }
                StartF.DrawChongqingshishicai(subm);
                StartF.SendChongqingResultPic(subm);
            }
            catch (Exception AnyError)
            {

                NetFramework.Console.WriteLine(AnyError.Message, true);
                NetFramework.Console.WriteLine(AnyError.StackTrace, true);
            }


        }

        private void MI_FuliCheck_Click(object sender, EventArgs e)
        {

            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("福利", editrow, DateTime.Now);


            }
        }

        private void MI_CancelFuliCheck_Click(object sender, EventArgs e)
        {

            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("取消福利", editrow, DateTime.Now);


            }
        }

        private void Btn_Resend_Click(object sender, EventArgs e)
        {
            try
            {

                StartF.DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
                StartF.DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.五分彩);
                StartF.DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.香港时时彩);
                StartF.DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5);
                StartF.DrawChongqingshishicai(Linq.ProgramLogic.ShiShiCaiMode.腾讯十分);



                StartF.ShiShiCaiDealGameLogAndNotice(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);

                StartF.SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩);
                StartF.SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.五分彩);
                StartF.SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.香港时时彩);
                StartF.SendChongqingResultPic(Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5);
            }
            catch (Exception AnyError)
            {
                NetFramework.Console.Write(AnyError.Message);

            }
        }

        private void 老板查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("老板查询", editrow, DateTime.Now);


            }
        }

        private void 取消老板查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("取消老板查询", editrow, DateTime.Now);


            }
        }

        private void MI_SendPCS_Click(object sender, EventArgs e)
        {

        }

        private void btn_resendballl_Click(object sender, EventArgs e)
        {

        }

        private void MI_BALLPIC_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("球赛图片", editrow, DateTime.Now);


            }
        }

        private void MI_CancelBALLPIC_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("取消球赛图片", editrow, DateTime.Now);


            }
        }



        private void MI_WebSendSetting_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;

                string ContactID = editrow.Field<string>("User_ContactID");
                string SourceType = editrow.Field<string>("User_SourceType");

                string ContactName = editrow.Field<string>("User_Contact");

                WebWeChatImageSetting wset = new WebWeChatImageSetting();
                wset.RunnerF = this;
                wset.WX_SourceType = SourceType;
                wset.WX_UserName = ContactID;
                wset.Show();
                wset.Text = ContactName + "发图设置";
            }
        }

        private void MI_HUIYAN_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("会员", editrow, DateTime.Now);


            }
        }

        private void mi_cancelhuiyan_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("取消会员", editrow, DateTime.Now);


            }
        }

        private void MI_ChongQingMode_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("重庆模式", editrow, DateTime.Now);




            }
        }

        private void MI_FiveMinuteMode_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("五分模式", editrow, DateTime.Now);


            }
        }

        private void MI_HkMode_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("香港模式", editrow, DateTime.Now);


            }
        }

        private void 澳洲幸运5模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("澳彩模式", editrow, DateTime.Now);


            }
        }

        private void MI_XinJiangShiShiCai_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("新疆模式", editrow, DateTime.Now);


            }
        }

        private void MI_TengXunShiFen_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("腾十模式", editrow, DateTime.Now);


            }
        }


        private void MI_腾讯五分模式_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("腾五模式", editrow, DateTime.Now);


            }
        }

        private void MI_vRMode_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("VR模式", editrow, DateTime.Now);
                ((ToolStripMenuItem)sender).Checked = true;


            }
        }

        private void MI_TengXunShiFenXinMode_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("腾十信模式", editrow, DateTime.Now);


            }
        }

        private void MI_TengXunWuFenXinMode_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("腾五信模式", editrow, DateTime.Now);


            }
        }
        private void MI_HeNeiWuFenXinMode_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("河五模式", editrow, DateTime.Now);


            }
        }

        string _GameMode = "";
        public void SetMode(String GameMode)
        {
            _GameMode = GameMode;
            MI_ChongQingMode.Visible = (GameMode == "重庆" ? true : false);
            MI_vRMode.Visible = (GameMode == "VR" ? true : false); ;
            MI_AozcMode.Visible = (GameMode == "澳彩" ? true : false); ;
            MI_HkMode.Visible = (GameMode == "香港" ? true : false);
            MI_XinJiangShiShiCaiMode.Visible = (GameMode == "新疆" ? true : false);
            MI_FiveMinuteMode.Visible = (GameMode == "五分" ? true : false); ;
            MI_TengXunWuFenMode.Visible = (GameMode == "腾五" ? true : false); ;
            MI_TengXunWuFenXinMode.Visible = (GameMode == "腾五信" ? true : false); ;
            MI_TengXunShiFenMode.Visible = (GameMode == "腾十" ? true : false); ;
            MI_TengXunShiFenXinMode.Visible = (GameMode == "腾十信" ? true : false); ;
            MI_HeNeiWuFenMode.Visible = (GameMode == "河内" ? true : false); ;

            gv_contact.Columns["重"].Visible = (GameMode == "重庆" ? true : false); ;
            gv_contact.Columns["VR"].Visible = (GameMode == "VR" ? true : false); ;
            gv_contact.Columns["澳"].Visible = (GameMode == "澳彩" ? true : false); ;
            gv_contact.Columns["港"].Visible = (GameMode == "香港" ? true : false); ;
            gv_contact.Columns["疆"].Visible = (GameMode == "新疆" ? true : false); ;
            gv_contact.Columns["五"].Visible = (GameMode == "五分" ? true : false); ;
            gv_contact.Columns["腾五"].Visible = (GameMode == "腾五" ? true : false); ;
            gv_contact.Columns["腾五信"].Visible = (GameMode == "腾五信" ? true : false); ;
            gv_contact.Columns["腾十"].Visible = (GameMode == "腾十" ? true : false);
            gv_contact.Columns["腾十信"].Visible = (GameMode == "腾十信" ? true : false); ;
            gv_contact.Columns["河五"].Visible = (GameMode == "河五" ? true : false); ;




        }






    }
}
