using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
namespace WeixinRobootSlim
{
    public partial class SendCharge : Form
    {
        public SendCharge()
        {
            InitializeComponent();
        }
        public static void LoadLog()
        {

        }
        public StartForm StartF = null;
        public RunnerForm RunnerF = null;

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

        private void Btn_Send_Click(object sender, EventArgs e)
        {
            WeixinRoboot.RobootWeb.WebService ws = new WeixinRoboot.RobootWeb.WebService();
            try
            {
                ep_sql.Clear();

                switch (_Mode)
                {
                    case "Charge":


                        string Result = ws.WX_UserReplyLog_MySendCreate("上分" + tb_ChargeMoney.Text, JsonConvert.SerializeObject(_UserRow), DateTime.Now, GlobalParam.GetUserParam(), new Guid[] { }, JsonConvert.SerializeObject(WeixinRobootSlim.Linq.Util_Services.GetServicesSetting()), "", "");

                        string WXSend = StartF.SendRobotContent(Result
                            , UserRow.Field<string>("User_ContactTEMPID")
                             , UserRow.Field<string>("User_SourceType")
                            );

                        //   string Result = "";
                        //  db.Logic_WX_UserReplyLog_MySendCreate("上分"+tb_ChargeMoney.Text, _UserRow.Field<string>("User_ContactID"), _UserRow.Field<string>("User_SourceType"), GlobalParam.Key, DateTime.Now, ref Result);

                        //string WXResult=   StartF.SendWXContent(Result
                        //      , UserRow.Field<string>("User_ContactTEMPID")
                        //      );


                        break;
                    case "CleanUp":
                        string Result2 = ws.WX_UserReplyLog_MySendCreate("下分" + tb_ChargeMoney.Text, JsonConvert.SerializeObject(_UserRow), DateTime.Now, GlobalParam.GetUserParam(), new Guid[] { }, JsonConvert.SerializeObject(WeixinRobootSlim.Linq.Util_Services.GetServicesSetting()), "", "");

                        decimal? TotalPointClean = ws.WXUserChangeLog_GetRemainder(UserRow.Field<string>("User_ContactTEMPID"), UserRow.Field<string>("User_SourceType"), GlobalParam.GetUserParam());

                        string WXSendClean = StartF.SendRobotContent(Result2
                            , UserRow.Field<string>("User_ContactTEMPID")
                            , UserRow.Field<string>("User_SourceType")
                            );
                        //    string Result2 = "";
                        //db.Logic_WX_UserReplyLog_MySendCreate("下分"+tb_ChargeMoney.Text, _UserRow.Field<string>("User_ContactID"), _UserRow.Field<string>("User_SourceType"), GlobalParam.Key, DateTime.Now, ref Result2);

                        //string WXResult2 = StartF.SendWXContent(Result2
                        //    , UserRow.Field<string>("User_ContactTEMPID")
                        //    );

                        break;
                    default:
                        break;
                }



                SendCharge_Load(null, null);




            }
            catch (Exception AnyError)
            {
                ep_sql.SetError(Btn_Send, AnyError.Message);
            }


        }

        private void SendCharge_Load(object sender, EventArgs e)
        {

            WeixinRoboot.RobootWeb.WebService ws = new WeixinRoboot.RobootWeb.WebService();

            var data = ws.SendCharge_GetSource(GlobalParam.GetUserParam()
                       , UserRow.Field<string>("User_ContactID")
                       , UserRow.Field<string>("User_SourceType"));
            BS_TransLog.DataSource = data;




        }


        private string _Mode = "";
        public string Mode
        {
            get { return _Mode; }
            set
            {
                _Mode = value;
                switch (value)
                {
                    case "Charge":
                        this.Text = "充值";
                        lbl_ChargeMoney.Text = "充值";
                        break;
                    case "CleanUp":
                        this.Text = "清算";
                        lbl_ChargeMoney.Text = "返点";
                        break;
                    default:
                        break;
                }

            }
        }



    }
}
