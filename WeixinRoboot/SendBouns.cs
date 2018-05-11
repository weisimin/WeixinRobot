using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
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
            BS_DataSource.DataSource = Linq.DataLogic.GetBounsSource(dtp_querydate.Value);


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
            foreach (var Senditem in SendList)
            {
                DataRow[] usrrow = StartF.RunnerF.MemberSource.Select("User_ContactID='" + Senditem.Field<string>("WX_UserName") + "'");

                var fcl = db.WX_UserChangeLog.Where(t =>
                    t.aspnet_UserID == GlobalParam.Key
                    && t.WX_UserName == Senditem.Field<string>("WX_UserName")
                    && t.RemarkType == "福利"
                    && t.ChangeLocalDay == Senditem.Field<String>("LocalPeriodDay")
                    );
                if (fcl.Count() == 0 && usrrow.Length != 0)
                {

                    String Returnstr = Linq.DataLogic.WX_UserReplyLog_MySendCreate(
                         "福利" + NetFramework.Util_Math.NullToZero(Senditem.Field<decimal?>("BounsCount"))
                         , usrrow.First());
                    if (Returnstr != "")
                    {
                        StartF.SendWXContent( "福利" + NetFramework.Util_Math.NullToZero(Senditem.Field<decimal?>("BounsCount"))+","+Returnstr
                         , usrrow.First().Field<string>("User_ContactTEMPID")
                         );
                    }


                }
            }//循环发放
            MessageBox.Show("发放完成");

        }

        private void SendBouns_Load(object sender, EventArgs e)
        {
            dtp_querydate.Value = DateTime.Today.AddDays(-1);
        }
    }
}
