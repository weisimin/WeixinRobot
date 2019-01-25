using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace WeixinRoboot
{
    public partial class Download163AndDeal : Form
    {
        public StartForm StartF = null;
        public RunnerForm RunnerF = null;
        public Download163AndDeal()
        {
            InitializeComponent();
        }

        private void btn_download_Click(object sender, EventArgs e)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            bool SendImage = false;
            StartF.DownLoad163CaiPiaoV_kaijiangwang(ref SendImage, Dtp_DownloadDate.Value, true,true);

            string LastPeriod = db.Game_Result.Where(t => t.aspnet_UserID == GlobalParam.UserKey).OrderByDescending(t=>t.GamePeriod).First().GamePeriod;
            StartF.ShiShiCaiDealGameLogAndNotice(true);
            db.SubmitChanges();

            SendImage = true;
            #region "有新的就通知,以及处理结果"
            if (SendImage == true)
            {



                DataRow[] dr = RunnerF.MemberSource.Select("User_IsReply='true'");

                foreach (var Rowitem in dr)
                {
                    #region  多人同号不到ID跳过
                    #endregion

                    string TEMPUserName = dr[0].Field<string>("User_ContactTEMPID");
                    string WXUserName = dr[0].Field<string>("User_ContactID");
                    string WX_SourceType = dr[0].Field<string>("User_SourceType");

                    Linq.WX_WebSendPICSetting webpcset = db.WX_WebSendPICSetting.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                      && t.WX_SourceType == WX_SourceType
                       && t.WX_UserName == WXUserName
                      );
                    if (webpcset == null)
                    {
                        continue;
                    }

                    if (dr[0].Field<string>("User_SourceType") == "微")
                    {
                        if ((webpcset.dragonpic == true))
                        {
                            StartF.SendRobotTxtFile(Application.StartupPath + "\\Data3" + GlobalParam.UserName + ".txt", TEMPUserName, WX_SourceType);
                        }
                    }

                    if (dr[0].Field<string>("User_SourceType") == "易")
                    {
                        if ((webpcset.dragonpic == true))
                        {
                            StartF.SendRobotTxtFile(Application.StartupPath + "\\Data3_yixin" + GlobalParam.UserName + ".txt", TEMPUserName, WX_SourceType);
                        }
                    }
                    Thread.Sleep(1000);

                    if ((webpcset.NumberPIC == true))
                    {
                        StartF.SendRobotImage(Application.StartupPath + "\\Data" + GlobalParam.UserName + ".jpg", TEMPUserName, WX_SourceType);

                    }

                    //if ((Mode == "All" && webpcset.NumberAndDragonPIC == true) || (Mode == "图3"))
                    //{
                    //    SendRobotImage(Application.StartupPath + "\\Data" + GlobalParam.UserName + "_v3.jpg", TEMPUserName, SourceType);
                    //}

                    if ((webpcset.NumberAndDragonPIC == true))
                    {
                        StartF.SendRobotTxtFile(Application.StartupPath + "\\Data数字龙虎" + GlobalParam.UserName + ".txt", TEMPUserName, WX_SourceType);
                    }

                    if (webpcset.shishicailink == true)
                    {
                        StartF.SendRobotLink("查询开奖网地址", "https://h5.13322.com/kaijiang/ssc_cqssc_history_dtoday.html", TEMPUserName, WX_SourceType);
                    }


                }//设置为自动监听的用户

            }//新开奖

            #endregion


            Download163AndDeal_Load(null, null);





        }

        private void Download163AndDeal_Load(object sender, EventArgs e)
        {
            Dtp_DownloadDate_ValueChanged(null, null);




        }

        private void Dtp_DownloadDate_ValueChanged(object sender, EventArgs e)
        {

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            BS_GameResult.DataSource = db.Game_Result
                .Where(t => t.aspnet_UserID == GlobalParam.UserKey
                    && t.GameTime.Value.Date == Dtp_DownloadDate.Value.Date)

                .Select(t => new
                {

                    Gr_GamePeriod = t.GamePeriod,
                    Gr_GameTime = (DateTime?)t.GameTime,
                    Gr_GameResult = t.GameResult,
                    Gr_NumTotal = t.NumTotal,
                    Gr_BigSmall = t.BigSmall,
                    Gr_SingleDouble = t.SingleDouble,
                    Gr_DragonTiger = t.DragonTiger,
                    Gr_GamePrivatePeriod = t.GamePrivatePeriod
                }).ToList();

            var GameLog = (from ds in db.WX_UserGameLog
                           where
                           (ds.aspnet_UserID == GlobalParam.UserKey)

                           && (
                 (ds.Result_HaveProcess == false || ds.Result_HaveProcess == null)
                 )

                           select new GameLogClass
               (
                     ds.WX_UserName,
                     ds.WX_UserName,
                    (DateTime?)ds.TransTime,
                     ds.GamePeriod,
                     ds.Buy_Value,
                     ds.Buy_Point
                     , ds.GameLocalPeriod
                     , ds.WX_SourceType
               )
                ).ToList();
          

            BS_GameLogNotDeal.DataSource = GameLog;


        }

        private void CB_AllNotDeal_CheckedChanged(object sender, EventArgs e)
        {
            Dtp_DownloadDate_ValueChanged(null, null);
        }

        protected class GameLogClass
        {
            public GameLogClass(string P_Wgl_Contant, string P_Wgl_ContantID
                , DateTime? P_Wgl_TransTime, string P_Wgl_GamePeriod
                , string P_Wgl_Buy_Value, decimal? P_Wgl_Buy_Point, string P_Wgl_GamePrivatePeriod, string P_WX_UerSourceType)
            {
                _Wgl_Contact = P_Wgl_Contant;
                _Wgl_ContactID = P_Wgl_ContantID;
                _Wgl_TransTime = P_Wgl_TransTime;
                _Wgl_GamePeriod = P_Wgl_GamePeriod;
                _Wgl_Buy_Value = P_Wgl_Buy_Value;
                _Wgl_Buy_Point = P_Wgl_Buy_Point;
                _Wgl_GamePrivatePeriod = P_Wgl_GamePrivatePeriod;
                _WX_UerSourceType = P_WX_UerSourceType;
            }
            private string _Wgl_Contact = "";
            private string _Wgl_ContactID = "";
            private DateTime? _Wgl_TransTime = null;
            private string _Wgl_GamePeriod = "";
            private string _Wgl_GamePrivatePeriod = "";
            private string _Wgl_Buy_Value = "";
            private decimal? _Wgl_Buy_Point = null;
            private string _WX_UerSourceType = "";

            public string Wgl_Contact { get { return _Wgl_Contact; } set { _Wgl_Contact = value; } }
            public string Wgl_ContantID { get { return _Wgl_ContactID; } set { _Wgl_ContactID = value; } }
            public DateTime? Wgl_TransTime { get { return _Wgl_TransTime; } set { _Wgl_TransTime = value; } }
            public string Wgl_GamePeriod { get { return _Wgl_GamePeriod; } set { _Wgl_GamePeriod = value; } }
            public string Wgl_Buy_Value { get { return _Wgl_Buy_Value; } set { _Wgl_Buy_Value = value; } }
            public decimal? Wgl_Buy_Point { get { return _Wgl_Buy_Point; } set { _Wgl_Buy_Point = value; } }
            public string Wgl_GamePrivatePeriod { get { return _Wgl_GamePrivatePeriod; } set { _Wgl_GamePrivatePeriod = value; } }

            public string WX_UerSourceType { get { return _WX_UerSourceType; } set { _WX_UerSourceType = value; } }

        }

        private void BtnSaveAndDeal_Click(object sender, EventArgs e)
        {
            bool Newdb = false;
            Linq.ProgramLogic.NewGameResult(
                fd_Num1.Text + " " + fd_Num2.Text + " " + fd_Num3.Text + " " + fd_Num4.Text + " " + fd_Num5.Text, fd_day.Value.ToString("yyMMdd") + fd_Period.Text, out Newdb);
            if (Newdb)
            {
                StartF.ShiShiCaiDealGameLogAndNotice();
            }

            DateTime day = DateTime.Now;
            if (day.Hour < 10)
            {
                day = day.AddDays(-1);
            }

            StartF.DrawChongqingshishicai(day);
            if (Newdb)
            {
                StartF.ShiShiCaiDealGameLogAndNotice();
            }
            StartF.SendChongqingResult();

            Dtp_DownloadDate_ValueChanged(null, null); ;
        }

    }

}
