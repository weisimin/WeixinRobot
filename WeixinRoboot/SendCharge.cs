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
            try
            {
                ep_sql.Clear();

                Boolean? LogicOK = false;
                switch (_Mode)
                {
                    case "Charge":

                        string Result = Linq.DataLogic.WX_UserReplyLog_MySendCreate("充值" + tb_ChargeMoney.Text, out LogicOK, _UserRow, GlobalParam.db);

                        string WXSend = StartF.SendWXContent(Result
                            , UserRow.Field<string>("User_ContactTEMPID")
                            );
                        break;
                    case "CleanUp":
                        string Result2 = Linq.DataLogic.WX_UserReplyLog_MySendCreate("清算" + tb_ChargeMoney.Text, out LogicOK, _UserRow, GlobalParam.db);

                        decimal? TotalPointClean = GlobalParam.db.WX_UserChangeLog.Where(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == UserRow.Field<string>("User_ContactID")).Sum(t => t.ChangePoint);

                        string WXSendClean = StartF.SendWXContent("清算:" + Result2
                            , UserRow.Field<string>("User_ContactTEMPID")
                            );

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
            var data = from dsl in GlobalParam.db.WX_UserChangeLog
                       join dsu in GlobalParam.db.WX_UserReply
                       on new { dsl.WX_UserName, dsl.aspnet_UserID } equals new { dsu.WX_UserName, dsu.aspnet_UserID }
                       where dsl.aspnet_UserID == GlobalParam.Key
                        && dsl.WX_UserName == UserRow.Field<string>("User_ContactID")

                       select new
                       {
                           UserName = dsu.WX_UserName
                           ,
                           dsl.Remark
                           ,
                           dsl.RemarkType
                           ,
                           dsl.ChangePoint
                       };
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
