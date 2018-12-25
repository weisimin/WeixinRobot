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
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
    
            try
            {
                ep_sql.Clear();
               
                switch (_Mode)
                {
                    case "Charge":


                        string Result = Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("上分" + tb_ChargeMoney.Text, _UserRow, DateTime.Now);

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
                        string Result2 = Linq.ProgramLogic.WX_UserReplyLog_MySendCreate("下分" + tb_ChargeMoney.Text, _UserRow, DateTime.Now);

                        decimal? TotalPointClean = Linq.ProgramLogic.WXUserChangeLog_GetRemainder(UserRow.Field<string>("User_ContactTEMPID"), UserRow.Field<string>("User_SourceType"));

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
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
    
            var data = from dsl in db.WX_UserChangeLog
                       join dsu in db.WX_UserReply
                       on new { dsl.WX_UserName, dsl.aspnet_UserID ,dsl.WX_SourceType} equals new { dsu.WX_UserName, dsu.aspnet_UserID ,dsu.WX_SourceType}
                       where dsl.aspnet_UserID == GlobalParam.UserKey
                        && dsl.WX_UserName == UserRow.Field<string>("User_ContactID")
                        && dsl.WX_SourceType == UserRow.Field<string>("User_SourceType")
                       select new
                       {
                           UserName = dsu.WX_UserName
                           ,
                           dsl.Remark
                           ,
                           dsl.RemarkType
                           ,
                           dsl.ChangePoint
                           ,dsl.ChangeTime
                           ,dsl.GamePeriod
                           ,SourceType=dsu.WX_SourceType
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
