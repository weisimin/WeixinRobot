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
    public partial class RemindQuery : Form
    {
        public RemindQuery()
        {
            InitializeComponent();
        }

        private void RemindQuery_Load(object sender, EventArgs e)
        {
            cb_wxsourcetype.SelectedIndex = 0;
            WX_GetReminder();
        }
        private void WX_GetReminder()
        {
            Linq.dbDataContext db = new Linq.dbDataContext();
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            var datasource = from ds in db.WX_UserChangeLog
                             group ds by new { ds.WX_UserName, ds.WX_SourceType, ds.aspnet_UserID } into g
                             join dm in db.WX_UserReply on new { g.Key.aspnet_UserID, g.Key.WX_UserName, g.Key.WX_SourceType } equals new { dm.aspnet_UserID, dm.WX_UserName, dm.WX_SourceType }

                             where g.Key.aspnet_UserID == GlobalParam.Key
                             && g.Key.WX_SourceType == cb_wxsourcetype.SelectedItem.ToString()


                             select new
                             {
                                 玩家 = dm.NickName + "(" + dm.RemarkName + ")"
                                 ,
                                 余 = g.Sum(t => t.ChangePoint)
                                 ,
                                 g.Key.WX_UserName
                                 ,
                                 g.Key.WX_SourceType
                             };
            gv_data.DataSource = datasource;

        }

        private void cb_wxsourcetype_SelectedValueChanged(object sender, EventArgs e)
        {
            WX_GetReminder();
        }

        private void gv_data_SelectionChanged(object sender, EventArgs e)
        {
            Linq.dbDataContext db = new Linq.dbDataContext();
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            if (gv_data.SelectedRows.Count != 0)
            {
                string WX_UserName = gv_data.SelectedRows[0].Cells["WX_UserName"].Value.ToString();
                string WX_SourceType = gv_data.SelectedRows[0].Cells["WX_SourceType"].Value.ToString();

                var ReplyLog = from ds in db.WX_UserReplyLog
                               join user in db.WX_UserReply on new { ds.aspnet_UserID, ds.WX_UserName, ds.WX_SourceType } equals new { user.aspnet_UserID, user.WX_UserName, user.WX_SourceType }
                         
                               where ds.aspnet_UserID == GlobalParam.Key
                               && ds.WX_UserName == WX_UserName
                               && ds.WX_SourceType == WX_SourceType
                               orderby ds.ReceiveTime ascending
                               select
                               new
                               {
                                   玩家 = (ds.SourceType=="微"||ds.SourceType=="易"? user.NickName + "(" + user.RemarkName + ")":"我")
                                   ,
                                   内容 = ds.ReceiveContent
                                   ,
                                   时间 = ds.ReceiveTime


                               };
                gv_talk.DataSource = ReplyLog;
                var changepoint = from ds in db.WX_UserChangeLog
                                  join user in db.WX_UserReply on new { ds.aspnet_UserID, ds.WX_UserName, ds.WX_SourceType } equals new { user.aspnet_UserID, user.WX_UserName, user.WX_SourceType }
                                  where ds.aspnet_UserID == GlobalParam.Key
                                  && ds.WX_UserName == WX_UserName
                                  && ds.WX_SourceType == WX_SourceType
                                  orderby ds.ChangeTime ascending
                                  select new
                                  {

                                      分数变动 = ds.ChangePoint
                                      ,
                                      时间 = ds.ChangeTime
                                      ,
                                      期号 = ds.GamePeriod
                                      ,
                                      类型 = ds.RemarkType
                                      ,
                                      下注 = ds.BuyValue


                                  };
                gv_changepoint.DataSource = changepoint;



            }
        }
    }
}
