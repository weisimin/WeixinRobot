using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeixinRoboot
{


    public partial class BallOpen : Form
    {
        public BallOpen()
        {
            InitializeComponent();
            gv_playersbuys.AutoGenerateColumns = false;
            gv_balls.AutoGenerateColumns = false;
            gv_result.AutoGenerateColumns = false;


            cb_wxsourcetype.SelectedIndex = 0;
        }

        public StartForm sf { get; set; }


        public class GameHalfResult
        {
            public string TimeType { get; set; }

            public Int32? TeamA { get; set; }

            public Int32? TeamB { get; set; }

            public string OpenGameID { get; set; }

        }
        public class GameFormat
        {
            public GameFormat(string p_gameid, string p_gamevs)
            {
                GameID = p_gameid;
                GameVS = p_gamevs;

            }
            public string GameID { get; set; }
            public string GameVS { get; set; }
        }

        private void BallOpen_Load(object sender, EventArgs e)
        {
            Reload();
        }
        private void Reload()
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[ GlobalParam.DataSourceName].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            db.ObjectTrackingEnabled = false;
            var source = (from ds in db.WX_UserGameLog_Football
                          where ds.HaveOpen == false
                          && ds.WX_SourceType == cb_wxsourcetype.SelectedItem.ToString()
                          &&ds.aspnet_UserID==GlobalParam.UserKey
                          select new GameFormat(ds.GameKey, ds.GameVS)).Distinct();
            gv_balls.DataSource = source;
        }
        private void GV_BallUnOpen_SelectionChanged(object sender, EventArgs e)
        {
            if (gv_balls.SelectedRows.Count == 0)
            {
                gv_result.DataSource = null;
                gv_playersbuys.DataSource = null;
                return;
            }
            else if (gv_balls.SelectedRows.Count == 1)
            {


                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[ GlobalParam.DataSourceName].ConnectionString);

                db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                db.ObjectTrackingEnabled = false;
                Linq.Game_ResultFootBall gr_fb = db.Game_ResultFootBall.SingleOrDefault(t => t.GameID == ((GameFormat)gv_balls.SelectedRows[0].DataBoundItem).GameID);

                if (gr_fb == null)
                {


                    List<GameHalfResult> gr = new List<GameHalfResult>();
                    GameHalfResult newr = new GameHalfResult();
                    newr.TimeType = "上半场";
                    newr.OpenGameID = ((GameFormat)gv_balls.SelectedRows[0].DataBoundItem).GameID;
                    gr.Add(newr);

                    GameHalfResult newr2 = new GameHalfResult();
                    newr2.TimeType = "下半场";
                    newr2.OpenGameID = ((GameFormat)gv_balls.SelectedRows[0].DataBoundItem).GameID;
                    gr.Add(newr2);

                    gv_result.DataSource = gr;

                }
                else
                {

                    List<GameHalfResult> gr = new List<GameHalfResult>();
                    GameHalfResult newr = new GameHalfResult();
                    newr.TimeType = "上半场";
                    newr.OpenGameID = ((GameFormat)gv_balls.SelectedRows[0].DataBoundItem).GameID;
                    newr.TeamA = gr_fb.A_FrontHalf;
                    newr.TeamB = gr_fb.B_FrontHalf;
                    gr.Add(newr);

                    GameHalfResult newr2 = new GameHalfResult();
                    newr2.TimeType = "下半场";
                    newr2.OpenGameID = ((GameFormat)gv_balls.SelectedRows[0].DataBoundItem).GameID;
                    newr2.TeamA = gr_fb.A_EndHalf;
                    newr2.TeamB = gr_fb.B_EndHalf;
                    gr.Add(newr2);

                    gv_result.DataSource = gr;
                }




                var unplaysource = (from ds in db.WX_UserGameLog_Football
                                    join urname in db.WX_UserReply on new { ds.aspnet_UserID, ds.WX_UserName, ds.WX_SourceType } equals new { urname.aspnet_UserID, urname.WX_UserName, urname.WX_SourceType } into urnamr_emp
                                    from urnamr_emp_d in urnamr_emp.DefaultIfEmpty()

                                    where ds.HaveOpen == false
                                    && ds.GameKey == ((GameFormat)gv_balls.SelectedRows[0].DataBoundItem).GameID
                                     && ds.WX_SourceType == cb_wxsourcetype.SelectedItem.ToString()
                                     &&ds.aspnet_UserID==GlobalParam.UserKey
                                    select new
                                    {
                                        WX_UserNameOrRemark = urnamr_emp_d.NickName + "(" + urnamr_emp_d.RemarkName + ")"
                                        ,
                                        aspnet_UserID = ds.aspnet_UserID
                                        ,
                                        transtime = ds.transtime
                                        ,
                                        GameID = ds.GameKey
                                        ,
                                        GameVS = ds.GameVS
                                        ,
                                        BuyType = ds.BuyType
                                        ,
                                        BuyRatio = ds.BuyRatio
                                        ,
                                        BuyMoney = ds.BuyMoney
                                        ,
                                        HaveOpen = ds.HaveOpen
                                        ,
                                        ResultMoney = ds.ResultMoney
                                        ,
                                        A_WIN = ds.A_WIN
                                        ,
                                        Winless = ds.Winless
                                        ,
                                        B_Win = ds.B_WIN
                                        ,
                                        BigWin = ds.BIGWIN
                                        ,
                                        Total = ds.Total
                                        ,
                                        SmallWin = ds.SMALLWIN
                                        ,
                                        R_A_A = ds.R_A_A
                                        ,
                                        R_A_SAME = ds.R_A_SAME
                                        ,
                                        R_A_B = ds.R_A_B
                                        ,
                                        R_SAME_A = ds.R_SAME_A
                                        ,
                                        R_SAME_SAME = ds.R_SAME_SAME
                                        ,
                                        R_SAME_B = ds.R_SAME_B
                                        ,
                                        R_B_A = ds.R_B_A
                                        ,
                                        R_B_SAME = ds.R_B_SAME
                                        ,
                                        R_B_B = ds.R_B_B
                                        ,
                                        R1_0_A = ds.R1_0_A
                                        ,
                                        R1_0_B = ds.R1_0_B
                                        ,
                                        R2_0_A = ds.R2_0_A
                                        ,
                                        R2_0_B = ds.R2_0_B
                                        ,
                                        R2_1_A = ds.R2_1_A
                                        ,
                                        R2_1_B = ds.R2_1_B
                                        ,
                                        R3_0_A = ds.R3_0_A
                                        ,
                                        R3_0_B = ds.R3_0_B
                                        ,
                                        R3_1_A = ds.R3_1_A
                                        ,
                                        R3_1_B = ds.R3_1_B
                                        ,
                                        R3_2_A = ds.R3_2_A
                                        ,
                                        R3_2_B = ds.R3_2_B
                                        ,
                                        R4_0_A = ds.R4_0_A
                                        ,
                                        R4_0_B = ds.R4_0_B
                                        ,
                                        R4_1_A = ds.R4_1_A
                                        ,
                                        R4_1_B = ds.R4_1_B
                                        ,
                                        R4_2_A = ds.R4_2_A
                                        ,
                                        R4_2_B = ds.R4_2_B
                                        ,
                                        R4_3_A = ds.R4_3_A
                                        ,
                                        R4_3_B = ds.R4_3_B
                                        ,
                                        R0_0 = ds.R0_0
                                        ,
                                        R1_1 = ds.R1_1
                                        ,
                                        R2_2 = ds.R2_2
                                        ,
                                        R3_3 = ds.R3_3
                                        ,
                                        R4_4 = ds.R4_4
                                        ,
                                        Rother = ds.ROTHER
                                        ,
                                        WX_SourceType = ds.WX_SourceType
                                        ,
                                        A_Team = ds.A_Team
                                        ,
                                        B_Team = ds.B_Team


                                    });
                gv_playersbuys.DataSource = unplaysource;
            }
            else
            {
                gv_result.DataSource = null;
                gv_playersbuys.DataSource = null;
                return;

            }

        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[ GlobalParam.DataSourceName].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            db.ObjectTrackingEnabled = false;
            Linq.Game_ResultFootBall newgr = new Linq.Game_ResultFootBall();

            Linq.Game_ResultFootBall findgr = db.Game_ResultFootBall.SingleOrDefault(t => t.GameID == ((List<GameHalfResult>)gv_result.DataSource).First().OpenGameID);

            if (findgr == null)
            {

                foreach (GameHalfResult item in ((List<GameHalfResult>)gv_result.DataSource))
                {
                    newgr.GameID = item.OpenGameID;
                    newgr.aspnet_UserID  = GlobalParam.UserKey;
                    newgr.GameVS = ((GameFormat)gv_balls.SelectedRows[0].DataBoundItem).GameVS;
                    if (item.TimeType == "上半场")
                    {
                        newgr.A_FrontHalf = item.TeamA;
                        newgr.B_FrontHalf = item.TeamB;
                    }
                    if (item.TimeType == "下半场")
                    {
                        newgr.A_EndHalf = item.TeamA;
                        newgr.B_EndHalf = item.TeamB;
                    }

                }


                db.Game_ResultFootBall.InsertOnSubmit(newgr);
                db.SubmitChanges();
            }

            else
            {
                foreach (GameHalfResult item in ((List<GameHalfResult>)gv_result.DataSource))
                {
                    findgr.GameID = item.OpenGameID;
                    findgr.aspnet_UserID = GlobalParam.UserKey;
                    findgr.GameVS = ((GameFormat)gv_balls.SelectedRows[0].DataBoundItem).GameVS;
                    if (item.TimeType == "上半场")
                    {
                        findgr.A_FrontHalf = item.TeamA;
                        findgr.B_FrontHalf = item.TeamB;
                    }
                    if (item.TimeType == "下半场")
                    {
                        findgr.A_EndHalf = item.TeamA;
                        findgr.B_EndHalf = item.TeamB;
                    }

                }
                db.SubmitChanges();

            }
            var ToOpenList = db.WX_UserGameLog_Football.Where(t => t.GameKey == (findgr == null ? newgr.GameID : findgr.GameID)
                &&(t.HaveOpen==false||t.HaveOpen==null)
                &&t.aspnet_UserID==GlobalParam.UserKey
                );

            foreach (Linq.WX_UserGameLog_Football item in ToOpenList)
            {
                string ToSend = "";
                if (findgr == null)
                {
                    ToSend=Linq.ProgramLogic.OpenBallGameLog(item, db, newgr.A_FrontHalf.Value, newgr.B_FrontHalf.Value, newgr.A_EndHalf.Value, newgr.B_EndHalf.Value);
         
                }
                else
                {
                   ToSend= Linq.ProgramLogic.OpenBallGameLog(item, db, findgr.A_FrontHalf.Value, findgr.B_FrontHalf.Value, findgr.A_EndHalf.Value, findgr.B_EndHalf.Value);
                }
                DataRow findcontact = sf.RunnerF.MemberSource.AsEnumerable().SingleOrDefault(t => NetFramework.Util_Convert.ToString(t.Field<object>("User_SourceType")) == item.WX_SourceType
                      && NetFramework.Util_Convert.ToString(t.Field<object>("User_ContactID")) == item.WX_UserName
                      );
                if (findcontact == null)
                {
                    NetFramework.Console.WriteLine("找不到玩家，开奖结果发不出",true);
                }
                else
                {
                    if (findgr == null)
                    {


                        sf.SendRobotContent(newgr.GameVS + Environment.NewLine + "上半场:" + newgr.A_FrontHalf + "-" + newgr.B_FrontHalf + "下半场" + newgr.A_EndHalf + "-" + newgr.B_EndHalf
                            + Environment.NewLine + Linq.ProgramLogic.BallBuyTypeToChinseFrontShow(item.BuyType) + ",赔率" + item.BuyRatio.ToString() 
                            + ((item.BuyType == "A_WIN" || item.BuyType == "B_Win") ?("让球"+item.Winless):"")
                            + ((item.BuyType == "BigWin" || item.BuyType == "SmallWin") ? ("总球"+item.Total) : "")
                            + ToSend, findcontact.Field<object>("User_ContactTEMPID").ToString(), item.WX_SourceType);
                    }
                    else  {
                        sf.SendRobotContent(findgr.GameVS + Environment.NewLine + "上半场:" + findgr.A_FrontHalf + "-" + findgr.B_FrontHalf + "下半场" + findgr.A_EndHalf + "-" + findgr.B_EndHalf
                    + Environment.NewLine + Linq.ProgramLogic.BallBuyTypeToChinseFrontShow(item.BuyType) + ",赔率" + item.BuyRatio.ToString() 
                    + ((item.BuyType == "A_WIN" || item.BuyType == "B_Win") ? ("让球" + item.Winless) : "")
                    + ((item.BuyType == "BigWin" || item.BuyType == "SmallWin") ? ("总球" + item.Total) : "")
                    + ToSend, findcontact.Field<object>("User_ContactTEMPID").ToString(), item.WX_SourceType);
                 
                    }
                }
            }






        }

        private void cb_wxsourcetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            Reload();
        }

    }
}
