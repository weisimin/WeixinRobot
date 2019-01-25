using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WeixinRoboot
{
    public partial class SendBouns : Form
    {
        public SendBouns()
        {
            InitializeComponent();
        }
        public static void LoadLog()
        {

        }
        public StartForm StartF = null;

        private void Btn_Send_Click(object sender, EventArgs e)
        {

        }

        private void Btn_SendImage_Click(object sender, EventArgs e)
        {

        }

        private void BTN_QUERY_Click(object sender, EventArgs e)
        {
            BS_DataSource.DataSource = Linq.ProgramLogic.GetBounsSource(dtp_querydate.Value, cb_SourceType.SelectedItem.ToString());


        }

        private void BTN_SEND_Click(object sender, EventArgs e)
        {

            //Result.Columns.Add("aspnet_UserID", typeof(Guid));
            //Result.Columns.Add("WX_UserName");
            //Result.Columns.Add("NickNameRemarkName");
            //Result.Columns.Add("LocalPeriodDay");
            //Result.Columns.Add("PeriodCount", typeof(decimal));
            //Result.Columns.Add("TotalBuy", typeof(decimal));
            //Result.Columns.Add("TotalResult", typeof(decimal));
            //Result.Columns.Add("AverageBuy", typeof(decimal));
            //Result.Columns.Add("FixNumber", typeof(decimal));
            //Result.Columns.Add("FlowPercent", typeof(decimal));
            //Result.Columns.Add("IfDivousPercent", typeof(decimal));
            //Result.Columns.Add("BounsCount", typeof(decimal));
            //Result.Columns.Add("Remark");

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");


            DataTable ToSend = (DataTable)BS_DataSource.DataSource;
            var SendList = ToSend.AsEnumerable().Where(t => t.Field<decimal?>("BounsCount") > 10);
            MessageBox.Show("福利少于10的不发");
            foreach (var Senditem in SendList)
            {
                DataRow[] usrrow = StartF.RunnerF.MemberSource.Select("User_ContactID='" + Senditem.Field<string>("WX_UserName").Replace("'", "''") + "' and User_SourceType='" + cb_SourceType.SelectedItem.ToString() + "'");

                var fcl = db.WX_UserChangeLog.Where(t =>
                    t.aspnet_UserID == GlobalParam.UserKey
                    && t.WX_UserName == Senditem.Field<string>("WX_UserName")
                    && t.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                    && t.RemarkType == "福利"
                    && t.ChangeLocalDay == Senditem.Field<String>("LocalPeriodDay")
                    );
                //取消禁止多次发放
                // if (fcl.Count() == 0 && usrrow.Length != 0)
                if (Senditem.Field<decimal?>("BounsCount").HasValue==false)
                {
                    NetFramework.Console.WriteLine(Senditem.Field<string>("WX_UserName")+"无福利跳过");
                    continue;
                }
                if (Senditem.Field<decimal?>("BounsCount").Value == 0)
                {
                    NetFramework.Console.WriteLine(Senditem.Field<string>("WX_UserName") + "0福利跳过");
                    continue;
                }

                String Returnstr = Linq.ProgramLogic.WX_UserReplyLog_MySendCreate(
                     "福利" + NetFramework.Util_Math.NullToZero(Senditem.Field<decimal?>("BounsCount"))
                     , null, DateTime.Now, Senditem.Field<string>("WX_UserName"), cb_SourceType.SelectedItem.ToString());
                if (Returnstr != "")
                {
                    if (usrrow.Length != 0)
                    {
                        try
                        {
                            StartF.SendRobotContent(
                                                       (cb_SourceType.SelectedItem.ToString() != "微" && cb_SourceType.SelectedItem.ToString() != "易" ?
                                                       ("@"+usrrow.First().Field<string>("User_Contact") + "##") : "")
                                                       + "发放福利" + NetFramework.Util_Math.NullToZero(Senditem.Field<decimal?>("BounsCount")) + "," + Returnstr
                                                    , usrrow.First().Field<string>("User_ContactTEMPID")
                                                     , usrrow.First().Field<string>("User_SourceType")
                                                    );
                        }
                        catch (Exception AnyError)
                        {

                            NetFramework.Console.WriteLine(AnyError.Message);
                            NetFramework.Console.WriteLine(AnyError.StackTrace);
                        }

                    }
                    else
                    {

                        NetFramework.Console.WriteLine(Senditem.Field<string>("WX_UserName") + "福利不能通知");
                    }
                }



            }//循环发放
            MessageBox.Show("发放完成");

        }

        private void SendBouns_Load(object sender, EventArgs e)
        {
            if (StartF.WeiXinOnLine)
            {
                this.cb_SourceType.Items.AddRange(new object[] {
            "微"});
            }
            if (StartF.YiXinOnline)
            {
                this.cb_SourceType.Items.AddRange(new object[] {
            "易"});
            }

            this.cb_SourceType.Items.AddRange(new object[] {
            "PCQ"});

            cb_SourceType.SelectedIndex = 0;
            dtp_querydate.Value = DateTime.Today.AddDays(-1);

        }
    }
}
