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
        private JObject _Members = null;
        public DataTable MemberSource = null;
        public DataTable ReplySource = null;


        public JObject Members
        {
            get
            {
                return _Members;
            }
            set
            {
                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
                db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

                _Members = value;






                foreach (var item in (_Members["MemberList"]) as JArray)
                {
                    string UserNametempID = (item["UserName"] as JValue).Value.ToString();
                    string NickName = (item["NickName"] as JValue).Value.ToString();
                    string RemarkName = (item["RemarkName"] as JValue).Value.ToString();
                    string HeadImgUrl = (item["HeadImgUrl"] as JValue).Value.ToString();

                    System.Text.RegularExpressions.Regex FindSeq = new System.Text.RegularExpressions.Regex("seq=([0-9])+");

                    string Seq = FindSeq.Match(HeadImgUrl).Value;
                    Seq = Seq.Substring(Seq.IndexOf("=") + 1);
                    Linq.WX_UserReply usrc = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == Seq);
                    if (usrc == null)
                    {
                        Linq.WX_UserReply newusrc = new Linq.WX_UserReply();
                        newusrc.aspnet_UserID = GlobalParam.Key;
                        newusrc.WX_UserName = Seq;

                        newusrc.RemarkName = RemarkName;
                        newusrc.NickName =  NetFramework.Util_WEB.CleanHtml(NickName);

                        db.WX_UserReply.InsertOnSubmit(newusrc);
                        db.SubmitChanges();

                    } //初始化，添加到数据库或同步数据库
                    else
                    {
                        usrc.RemarkName = RemarkName;
                        usrc.NickName = NetFramework.Util_WEB.CleanHtml(NickName); ;
                        db.SubmitChanges();

                    } //初始化，添加到数据库或同步数据库

                    DataRow[] Lists = MemberSource.Select("User_ContactID='" + Seq + "'");
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
                    newr.SetField("User_ContactID", Seq);
                    newr.SetField("User_ContactTEMPID", UserNametempID);
                    newr.SetField("User_ContactType", UserNametempID.StartsWith("@@") ? "群" : "个人");
                    newr.SetField("User_Contact", RemarkName == "" ? NickName : RemarkName);
                    newr.SetField("User_IsReply", usrc == null ? false : usrc.IsReply);
                    newr.SetField("User_IsReceiveTransfer", usrc == null ? false : usrc.IsReceiveTransfer);

                    var UpdateLogs = ReplySource.AsEnumerable().Where(t => t.Field<string>("Reply_ContactID") == Seq);
                    foreach (var logitem in UpdateLogs)
                    {
                        logitem.SetField("Reply_ContactTEMPID", UserNametempID);
                        logitem.SetField("Reply_Contact", RemarkName == "" ? NickName : RemarkName);
                    }

                }//成员列表循环
                foreach (DataRow RepyRowitem in ReplySource.Rows)
                {
                    DataRow[] usr = MemberSource.Select("User_ContactID='" + RepyRowitem.Field<string>("Reply_ContactID") + "'");
                    if (usr.Length == 0)
                    {
                        continue;
                    }
                    RepyRowitem.SetField("Reply_Contact", usr[0].Field<string>("User_Contact"));
                    RepyRowitem.SetField("Reply_ContactTEMPID", usr[0].Field<string>("User_ContactTEMPID"));
                }
            }//Set结束
        }

        public JObject RoomMembers = null;
        public DataTable RoomMerbersSource = null;



        public RunnerForm()
        {
            InitializeComponent();
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");


            dtp_StartDate.Value = DateTime.Today.AddDays(-3);
            dtp_EndDate.Value = DateTime.Today.AddMonths(1);

            ReplySource = new DataTable();

            ReplySource.Columns.Add("Reply_Contact");
            ReplySource.Columns.Add("Reply_ContactID");
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
            MemberSource.Columns.Add("User_IsReceiveTransfer", typeof(Boolean));

            BS_Contact.DataSource = MemberSource;
            BS_Contact.Sort = "User_Contact";

            BS_ReceiveReply.DataSource = ReplySource;


            dtp_StartDate.Value = DateTime.Today.AddDays(-3);
            dtp_EndDate.Value = DateTime.Today.AddMonths(1);


            LoadReplyLog("");

            TM_Refresh.Start();
        }

        private void LoadReplyLog(string SelectUser)
        {

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            //ReplySource.Columns.Add("Reply_Contact");
            //ReplySource.Columns.Add("Reply_ContactID");
            //ReplySource.Columns.Add("Reply_ContactTEMPID");
            //ReplySource.Columns.Add("Reply_ReceiveContent");
            //ReplySource.Columns.Add("Reply_ReplyContent");
            //ReplySource.Columns.Add("Reply_ReceiveTime", typeof(object));
            //ReplySource.Columns.Add("Reply_ReplyTime", typeof(object));

            DataTable PreRend = NetFramework.Util_Sql.RunSqlDataTable("LocalSqlServer"
                  , "Select case when ur.RemarkName<>'' then ur.RemarkName+'@#'+ur.NickName else ur.NickName end as Reply_Contact ,RL.WX_UserName as Reply_ContactID "

              + " ,'' as Reply_ContactTEMPID"
              + " ,ReceiveContent as Reply_ReceiveContent"
              + " ,ReplyContent as Reply_ReplyContent"

              + " ,ReceiveTime as ReplyReceiveTime"
              + " ,ReplyTime as Reply_ReplyTime"

                  + " from WX_UserReplyLog RL with (nolock) join WX_UserReply ur with (nolock) on RL.aspnet_UserID=ur.aspnet_UserID and RL.WX_UserName=ur.WX_UserName   where RL.aspnet_UserID='" + GlobalParam.Key.ToString() + "' and "
                  + "ReceiveTime >='" + dtp_StartDate.Value.Date.ToString("yyyy-MM-dd") + "' and "
                  + "ReceiveTime <'" + dtp_EndDate.Value.Date.ToString("yyyy-MM-dd") + "'  "
                 + (SelectUser == "" ? "" : " and RL.WX_UserName='" + SelectUser + "'")
                  );
            if (ReplySource == null)
            {
                return;
            }
            ReplySource.Rows.Clear();
            foreach (DataRow item in PreRend.Rows)
            {
                string ContactID = item.Field<string>("Reply_ContactID");
                DataRow[] memusr = MemberSource.Select("User_ContactID='" + ContactID + "'");
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
                             on new { ds.aspnet_UserID, ds.WX_UserName, ds.ReceiveTime } equals new { dsgame.aspnet_UserID, dsgame.WX_UserName, ReceiveTime = dsgame.TransTime }
                             into leftdsggame
                             from dsgame2 in leftdsggame.DefaultIfEmpty()
                             where ds.ReceiveTime >= dtp_StartDate.Value
                             && ds.ReceiveTime < dtp_EndDate.Value
                             && ds.aspnet_UserID == GlobalParam.Key
                             && ds.WX_UserName == SelectUser
                             orderby ds.ReceiveTime descending
                             select new
                             {
                                 ds.ReceiveTime,
                                 ds.ReceiveContent,
                                 ds.aspnet_UserID,
                                 ds.WX_UserName,
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
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            if (e.Row != null)
            {
                return;
            }
            Linq.WX_UserReply checkreply = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == e.Row.Field<string>("Reply_ContactID"));
            if (checkreply.IsReply == true)
            {
                string Reply_ContactID = e.Row.Field<string>("Reply_ContactID");
                string Reply_ReceiveTime = e.Row.Field<string>("Reply_ReceiveTime");
                Linq.WX_UserReplyLog log = db.WX_UserReplyLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key
                     && t.WX_UserName == Reply_ContactID
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
            this.Refresh();

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

                LoadReplyLog(((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row.Field<string>("User_ContactID"));


            }
            else
            {

            }

        }

        private void dtp_Start_ValueChanged(object sender, EventArgs e)
        {
            LoadReplyLog("");
        }

        private void dtp_End_ValueChanged(object sender, EventArgs e)
        {
            LoadReplyLog("");
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



                string Result = Linq.DataLogic.WX_UserReplyLog_MySendCreate("自动", editrow);

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


                Linq.DataLogic.WX_UserReplyLog_MySendCreate("取消自动", editrow);


            }
        }

        private void MI_ReceiveTrans_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.DataLogic.WX_UserReplyLog_MySendCreate("转发", editrow);


            }
        }

        private void MI_CancelReceiveTrans_Click(object sender, EventArgs e)
        {
            if (gv_contact.SelectedRows.Count != 0)
            {
                DataRow editrow = ((DataRowView)gv_contact.SelectedRows[0].DataBoundItem).Row;


                Linq.DataLogic.WX_UserReplyLog_MySendCreate("取消转发", editrow);


            }
        }

        private void BtnSaveAndDeal_Click(object sender, EventArgs e)
        {
            Linq.DataLogic.NewGameResult(
              fd_Num1.Text + " " + fd_Num2.Text + " " + fd_Num3.Text + " " + fd_Num4.Text + " " + fd_Num5.Text, fd_day.Value.ToString("yyMMdd") + fd_Period.Text);
            DateTime day = DateTime.Now;
            if (day.Hour < 10)
            {
                day = day.AddDays(-1);
            }
            StartF.DealGameLogAndNotice();

            StartF.DrawGdi(day);
            StartF.SendChongqingResult();
        }





    }
}
