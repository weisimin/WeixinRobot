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

            WeixinRoboot.RobootWeb.WebService ws = new WeixinRoboot.RobootWeb.WebService();

            gv_data.DataSource = ws.GetReminder(GlobalParam.GetUserParam(),cb_wxsourcetype.SelectedItem.ToString()) ;

        }

        private void cb_wxsourcetype_SelectedValueChanged(object sender, EventArgs e)
        {
            WX_GetReminder();
        }

        private void gv_data_SelectionChanged(object sender, EventArgs e)
        {
           
            if (gv_data.SelectedRows.Count != 0)
            {
                string WX_UserName = gv_data.SelectedRows[0].Cells["WX_UserName"].Value.ToString();
                string WX_SourceType = gv_data.SelectedRows[0].Cells["WX_SourceType"].Value.ToString();

                WeixinRoboot.RobootWeb.WebService ws = new WeixinRoboot.RobootWeb.WebService();

                var ReplyLog = ws.RemindQuery_GetReplyLog(WX_UserName, WX_SourceType, GlobalParam.GetUserParam());
                gv_talk.DataSource = ReplyLog;
                var changepoint = ws.RemindQuery_GetChangePoint(WX_UserName, WX_SourceType, GlobalParam.GetUserParam());
                gv_changepoint.DataSource = changepoint;



            }
        }
    }
}
