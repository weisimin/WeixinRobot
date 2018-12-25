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
        }

        private void LogForm_Paint(object sender, PaintEventArgs e)
        {
            tb_log.Text = NetFramework.Console.LastLog;
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            tb_log.Text = NetFramework.Console.LastLog;
        }

    }
}
