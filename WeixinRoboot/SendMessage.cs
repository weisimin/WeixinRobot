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
    public partial class SendMessage : Form
    {
        public SendMessage()
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
                this.Text = _UserRow.Field<string>("User_Contact");

            }
        }

        private DataRow _UserRow = null;

        private void Btn_Send_Click(object sender, EventArgs e)
        {
           
            StartF.SendRobotContent(  tb_MessageContent.Text, _UserRow.Field<string>("User_ContactTEMPID"),_UserRow.Field<string>("User_SourceType"));

        
            tb_MessageContent.Text = "";
        }

        private void Btn_SendImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            DialogResult dr = fd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string Result = StartF.SendRobotImage(fd.FileName, _UserRow.Field<string>("User_ContactTEMPID"), _UserRow.Field<string>("User_SourceType"));
           
            }
        }
    }
}
