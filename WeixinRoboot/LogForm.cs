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
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
            //tb_log. = NetFramework.Console.LastLog;
            tm_refresh.Interval = 300;
            tm_refresh.Start();
        }

        private void LogForm_Paint(object sender, PaintEventArgs e)
        {
           // tb_log.Text = NetFramework.Console.LastLog;
            //this.Invalidate();
            //System.Threading.Thread.Sleep(25);
           // Application.DoEvents();
           // this.Refresh();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            tb_log.Text = NetFramework.Console.LastLog;
        }

        private void tm_refresh_Tick(object sender, EventArgs e)
        {
            tb_log.Text = NetFramework.Console.LastLog;
        }

    }
}
