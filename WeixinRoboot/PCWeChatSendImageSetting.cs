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
    public partial class PCWeChatSendImageSetting : Form
    {
        public PCWeChatSendImageSetting()
        {
            InitializeComponent();
        }
        public StartForm SF;
        private void PCWeChatSendImageSetting_Load(object sender, EventArgs e)
        {

            

            BS_GV_PicSendSetting.DataSource = SF.InjectWins;
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            BS_GV_PicSendSetting.DataSource = SF.InjectWins;
        }
    }
}
