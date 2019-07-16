using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace WeixinRobootSlim
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

            if (dtp_StartDate.Value == null || dtp_EndDate.Value == null || RunnerF == null || _UserRow == null)
            {
                return;
            }
            WeixinRoboot.RobootWeb.WebService ws = new WeixinRoboot.RobootWeb.WebService();
            var datasource = ws.SendManulOrder_GetOrderSource(
                              dtp_StartDate.Value
                            , dtp_EndDate.Value
                            , GlobalParam.UserKey
                            ,_UserRow.Field<string>("User_ContactID")
                             , _UserRow.Field<string>("User_SourceType")
                            ) ;
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
            WeixinRoboot.RobootWeb.WebService ws = new WeixinRoboot.RobootWeb.WebService();

            string Result = ws.SendManulOrder_Delete(aspnet_UserID
                 , WX_UserName
                  , WX_SourceType
                  , DT.Value);


           MessageBox.Show(Result);
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
