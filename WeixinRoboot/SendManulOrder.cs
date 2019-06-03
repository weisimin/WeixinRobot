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
    public partial class SendManulOrder : Form
    {
        public StartForm StartF = null;
        public RunnerForm RunnerF = null;
        public SendManulOrder()
        {
            InitializeComponent();
            dtp_StartDate.Value = DateTime.Today.AddDays(-3);
            dtp_EndDate.Value = DateTime.Today.AddMonths(1);
        }
        public DataRow UserRow
        {
            get { return _UserRow; }
            set
            {
                _UserRow = value;
                lbl_User.Text = _UserRow.Field<string>("User_Contact");

            }
        }
        private DataRow _UserRow = null;

        private void SendManulOrder_Load(object sender, EventArgs e)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[ GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            //db.ObjectTrackingEnabled = false;
            if (dtp_StartDate.Value == null || dtp_EndDate.Value == null || RunnerF == null || _UserRow == null)
            {
                return;
            }

            var datasource = from ds in db.WX_UserReplyLog
                             join dsgame in db.WX_UserGameLog
                             on new { ds.aspnet_UserID, ds.WX_UserName, ds.ReceiveTime,ds.WX_SourceType } equals new { dsgame.aspnet_UserID, dsgame.WX_UserName, ReceiveTime = dsgame.TransTime,dsgame.WX_SourceType }
                             into leftdsggame
                             from dsgame2 in leftdsggame.DefaultIfEmpty()
                             where ds.ReceiveTime >= dtp_StartDate.Value
                             && ds.ReceiveTime < dtp_EndDate.Value
                             && ds.aspnet_UserID == GlobalParam.UserKey
                             && ds.WX_UserName == _UserRow.Field<string>("User_ContactID")
                             && ds.WX_SourceType == _UserRow.Field<string>("User_SourceType")
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

        private void Btn_Send_Click(object sender, EventArgs e)
        {

            string Result = StartF.NewWXContent(fd_ReceiveTime.Value, fd_BuyPoint.Text, _UserRow,"人工",true); 
            MessageBox.Show(Result);
            SendManulOrder_Load(null, null);

        }

        private void dtp_StartDate_ValueChanged(object sender, EventArgs e)
        {
            SendManulOrder_Load(null, null);
        }

        private void dtp_EndDate_ValueChanged(object sender, EventArgs e)
        {
            SendManulOrder_Load(null, null);
        }



        private void MI_Delete_Click(object sender, EventArgs e)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[ GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            //db.ObjectTrackingEnabled = false;
            ep_sql.Clear();
            DataGridViewRow dr = GV_GameLog.SelectedRows[0];
            string aspnet_UserID = dr.Cells["aspnet_UserID"].Value.ToString();
            string WX_UserName = dr.Cells["WX_UserName"].Value.ToString();
            string WX_SourceType = dr.Cells["WX_SourceType"].Value.ToString();
            string ReceiveTime = dr.Cells["ReceiveTime"].Value.ToString();

            DateTime? DT = null;
            try
            {
                DT = DateTime.Parse(ReceiveTime);
            }
            catch (Exception)
            {

                throw;
            }


            Linq.WX_UserGameLog testg = db.WX_UserGameLog.SingleOrDefault(t => t.aspnet_UserID == new Guid(aspnet_UserID)
                  && t.WX_UserName == WX_UserName
                  && t.WX_SourceType == WX_SourceType
                  && t.TransTime == DT);
            if (testg != null && testg.Result_HaveProcess != false)
            {
                ep_sql.SetError(GV_GameLog, "已开或已处理,不能删除");
            }
            Linq.WX_UserReplyLog testrg = db.WX_UserReplyLog.SingleOrDefault(t => t.aspnet_UserID == new Guid(aspnet_UserID)
                      && t.WX_UserName == WX_UserName
                      &&t.WX_SourceType==WX_SourceType
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
            MessageBox.Show("删除成功");
            SendManulOrder_Load(null, null);
        }

        private void GV_GameLog_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GV_GameLog.SelectedRows.Count != 0)
            {
                MS_Data.Show(this, this.PointToClient(MousePosition));
            }
        }


    }

}
