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
    public partial class BallGames : Form
    {
        public BallGames()
        {
            InitializeComponent();
            gv_GameList.AutoGenerateColumns = false;
            gv_ratios.AutoGenerateColumns = false;
            gv_ratiocurrent.AutoGenerateColumns = false;
        }


        private void BallGames_Load(object sender, EventArgs e)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            var source = db.Game_FootBall_VS.Where(t => t.aspnet_UserID == GlobalParam.UserKey 
                //&& (t.LastAliveTime == null || t.LastAliveTime >= DateTime.Today.AddDays(-3))
                &&t.Jobid==GlobalParam.JobID
                );
            // var classsource = (from ds in source
            //select new { ds.GameType, ds.MatchClass }).Distinct();
            bs_gamelist.DataSource = source;
        }

        private void gv_GameList_SelectionChanged(object sender, EventArgs e)
        {
            if (gv_GameList.SelectedRows.Count != 0)
            {
                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
                db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

                IQueryable<Linq.Game_FootBall_VSRatios> DbRatios = Linq.ProgramLogic.GameVSGetRatios(db, ((Linq.Game_FootBall_VS)gv_GameList.SelectedRows[0].DataBoundItem));
                bs_ratios.DataSource = DbRatios;

                bs_ratiocurrent.DataSource = RatioConvertToGridData((Linq.Game_FootBall_VS)gv_GameList.SelectedRows[0].DataBoundItem, db);
                bs_ratiocurrent2.DataSource = Linq.ProgramLogic.VSGetCurRatio((Linq.Game_FootBall_VS)gv_GameList.SelectedRows[0].DataBoundItem, db);

            }
        }
        //public List<TeamRowFormat> RatioConvertToGridDataSource = new List<TeamRowFormat>();
        public List<TeamRowFormat> RatioConvertToGridData(Linq.Game_FootBall_VS toc, Linq.dbDataContext db)
        {
            List<TeamRowFormat> RatioConvertToGridDataSource = new List<TeamRowFormat>();

            TeamRowFormat ATEAM = new TeamRowFormat();

            TeamRowFormat BTEAM = new TeamRowFormat();



            Linq.Game_FootBall_VSRatios cr = Linq.ProgramLogic.VSGetCurRatio(toc, db);

            if (cr==null)
            {
                return RatioConvertToGridDataSource; 
            }
            ATEAM.Team = toc.A_Team;

            ATEAM.R1_0 = cr.R1_0_A;
            ATEAM.R2_0 = cr.R2_0_A;
            ATEAM.R2_1 = cr.R2_1_A;
            ATEAM.R3_0 = cr.R3_0_A;
            ATEAM.R3_1 = cr.R3_1_A;
            ATEAM.R3_2 = cr.R3_2_A;
            ATEAM.R4_0 = cr.R4_0_A;
            ATEAM.R4_1 = cr.R4_1_A;
            ATEAM.R4_2 = cr.R4_2_A;
            ATEAM.R4_3 = cr.R4_3_A;

            ATEAM.R0_0 = cr.R0_0;
            ATEAM.R1_1 = cr.R1_1;
            ATEAM.R2_2 = cr.R2_2;
            ATEAM.R3_3 = cr.R3_3;
            ATEAM.R4_4 = cr.R4_4;

            ATEAM.ROTHER = cr.ROTHER;



            BTEAM.Team = toc.B_Team;

            BTEAM.R1_0 = cr.R1_0_B;
            BTEAM.R2_0 = cr.R2_0_B;
            BTEAM.R2_1 = cr.R2_1_B;
            BTEAM.R3_0 = cr.R3_0_B;
            BTEAM.R3_1 = cr.R3_1_B;
            BTEAM.R3_2 = cr.R3_2_B;
            BTEAM.R4_0 = cr.R4_0_B;
            BTEAM.R4_1 = cr.R4_1_B;
            BTEAM.R4_2 = cr.R4_2_B;
            BTEAM.R4_3 = cr.R4_3_B;

            BTEAM.R0_0 = cr.R0_0;
            BTEAM.R1_1 = cr.R1_1;
            BTEAM.R2_2 = cr.R2_2;
            BTEAM.R3_3 = cr.R3_3;
            BTEAM.R4_4 = cr.R4_4;
            BTEAM.ROTHER = cr.ROTHER;


            RatioConvertToGridDataSource.Add(ATEAM);
            RatioConvertToGridDataSource.Add(BTEAM);

            return RatioConvertToGridDataSource;
        }
        public class TeamRowFormat
        {
            private string _R1_0 = "";
            private string _R2_0 = "";
            private string _R2_1 = "";
            private string _R3_0 = "";
            private string _R3_1 = "";
            private string _R3_2 = "";
            private string _R4_0 = "";
            private string _R4_1 = "";
            private string _R4_2 = "";
            private string _R4_3 = "";
            private string _R0_0 = "";
            private string _R1_1 = "";
            private string _R2_2 = "";
            private string _R3_3 = "";
            private string _R4_4 = "";
            private string _ROTHER = "";
            private string _Team = "";

            public string R1_0 { get { return _R1_0; } set { _R1_0 = value; } }
            public string R2_0 { get { return _R2_0; } set { _R2_0 = value; } }
            public string R2_1 { get { return _R2_1; } set { _R2_1 = value; } }
            public string R3_0 { get { return _R3_0; } set { _R3_0 = value; } }
            public string R3_1 { get { return _R3_1; } set { _R3_1 = value; } }
            public string R3_2 { get { return _R3_2; } set { _R3_2 = value; } }
            public string R4_0 { get { return _R4_0; } set { _R4_0 = value; } }
            public string R4_1 { get { return _R4_1; } set { _R4_1 = value; } }
            public string R4_2 { get { return _R4_2; } set { _R4_2 = value; } }
            public string R4_3 { get { return _R4_3; } set { _R4_3 = value; } }
            public string R0_0 { get { return _R0_0; } set { _R0_0 = value; } }
            public string R1_1 { get { return _R1_1; } set { _R1_1 = value; } }
            public string R2_2 { get { return _R2_2; } set { _R2_2 = value; } }
            public string R3_3 { get { return _R3_3; } set { _R3_3 = value; } }
            public string R4_4 { get { return _R4_4; } set { _R4_4 = value; } }
            public string ROTHER { get { return _ROTHER; } set { _ROTHER = value; } }

            public string Team { get { return _Team; } set { _Team = value; } }

        }
    }
}
