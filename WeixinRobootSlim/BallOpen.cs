using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeixinRobotLib.Entity.Linq;
using Newtonsoft.Json;
namespace WeixinRobootSlim
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

            WeixinRoboot.RobootWeb.WebService ws = new WeixinRoboot.RobootWeb.WebService();
            var source = JsonConvert.DeserializeObject<GameFormat[]>( ws.BallOpen_Reload(
                        cb_wxsourcetype.SelectedItem.ToString()
                         ,GlobalParam.UserKey)
            );
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

                WeixinRoboot.RobootWeb.WebService ws = new WeixinRoboot.RobootWeb.WebService();

               
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                //db.ObjectTrackingEnabled = false;
                Game_ResultFootBall gr_fb = JsonConvert.DeserializeObject<Game_ResultFootBall>(ws.Game_ResultFootBall_SingleOrDefault(((GameFormat)gv_balls.SelectedRows[0].DataBoundItem).GameID));

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

                var unplaysource = ws.BallOpen_GetUpPaySource(
                                    ((GameFormat)gv_balls.SelectedRows[0].DataBoundItem).GameID
                                     , cb_wxsourcetype.SelectedItem.ToString()
                                    ,GlobalParam.UserKey
                                   );
                gv_playersbuys.DataSource = unplaysource;
            }//没选择
            else
            {
                gv_result.DataSource = null;
                gv_playersbuys.DataSource = null;
                return;

            }

        }

        private void btn_open_Click(object sender, EventArgs e)
        {
           
            Game_ResultFootBall newgr = new Game_ResultFootBall();
            WeixinRoboot.RobootWeb.WebService ws = new WeixinRoboot.RobootWeb.WebService();
           Game_ResultFootBall findgr = JsonConvert.DeserializeObject<Game_ResultFootBall>(ws.Game_ResultFootBall_SingleOrDefault(((List<GameHalfResult>)gv_result.DataSource).First().OpenGameID));

            //if (findgr == null)
            //{

            //    foreach (GameHalfResult item in ((List<GameHalfResult>)gv_result.DataSource))
            //    {
            //        newgr.GameID = item.OpenGameID;
            //        newgr.aspnet_UserID  = GlobalParam.UserKey;
            //        newgr.GameVS = ((GameFormat)gv_balls.SelectedRows[0].DataBoundItem).GameVS;
            //        if (item.TimeType == "上半场")
            //        {
            //            newgr.A_FrontHalf = item.TeamA;
            //            newgr.B_FrontHalf = item.TeamB;
            //        }
            //        if (item.TimeType == "下半场")
            //        {
            //            newgr.A_EndHalf = item.TeamA;
            //            newgr.B_EndHalf = item.TeamB;
            //        }

            //    }


            //    db.Game_ResultFootBall.InsertOnSubmit(newgr);
            //    db.SubmitChanges();
            //}

            //else
            //{
            //    foreach (GameHalfResult item in ((List<GameHalfResult>)gv_result.DataSource))
            //    {
            //        findgr.GameID = item.OpenGameID;
            //        findgr.aspnet_UserID = GlobalParam.UserKey;
            //        findgr.GameVS = ((GameFormat)gv_balls.SelectedRows[0].DataBoundItem).GameVS;
            //        if (item.TimeType == "上半场")
            //        {
            //            findgr.A_FrontHalf = item.TeamA;
            //            findgr.B_FrontHalf = item.TeamB;
            //        }
            //        if (item.TimeType == "下半场")
            //        {
            //            findgr.A_EndHalf = item.TeamA;
            //            findgr.B_EndHalf = item.TeamB;
            //        }

            //    }
            //    db.SubmitChanges();

            //}
           
            ws.BallOpen_Open(JsonConvert.SerializeObject( gv_result.DataSource),GlobalParam.UserKey, JsonConvert.SerializeObject( gv_balls.SelectedRows[0].DataBoundItem));

            var ToOpenList = JsonConvert.DeserializeObject<WX_UserGameLog_Football[]> (ws.WX_UserGameLog_Football_Where((findgr == null ? newgr.GameID : findgr.GameID)
               ,GlobalParam.UserKey
                ));

            foreach (WeixinRobotLib.Entity.Linq. WX_UserGameLog_Football item in ToOpenList)
            {
                string ToSend = "";
                if (findgr == null)
                {
                    ToSend=ws.OpenBallGameLog(JsonConvert.SerializeObject( item),  newgr.A_FrontHalf.Value, newgr.B_FrontHalf.Value, newgr.A_EndHalf.Value, newgr.B_EndHalf.Value,GlobalParam.GetUserParam());
         
                }
                else
                {
                    ToSend = ws.OpenBallGameLog(JsonConvert.SerializeObject(item), findgr.A_FrontHalf.Value, findgr.B_FrontHalf.Value, findgr.A_EndHalf.Value, findgr.B_EndHalf.Value, GlobalParam.GetUserParam());
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
                            + Environment.NewLine + WeixinRobotLib.Entity. Linq.ProgramLogic.BallBuyTypeToChinseFrontShow(item.BuyType) + ",赔率" + item.BuyRatio.ToString() 
                            + ((item.BuyType == "A_WIN" || item.BuyType == "B_Win") ?("让球"+item.Winless):"")
                            + ((item.BuyType == "BigWin" || item.BuyType == "SmallWin") ? ("总球"+item.Total) : "")
                            + ToSend, findcontact.Field<object>("User_ContactTEMPID").ToString(), item.WX_SourceType);
                    }
                    else  {
                        sf.SendRobotContent(findgr.GameVS + Environment.NewLine + "上半场:" + findgr.A_FrontHalf + "-" + findgr.B_FrontHalf + "下半场" + findgr.A_EndHalf + "-" + findgr.B_EndHalf
                    + Environment.NewLine + WeixinRobotLib.Entity.Linq.ProgramLogic.BallBuyTypeToChinseFrontShow(item.BuyType) + ",赔率" + item.BuyRatio.ToString() 
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
