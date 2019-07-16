using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace WeixinRobootSlim
{
    public partial class UpdateActiveCode : Form
    {
        public UpdateActiveCode()
        {
            InitializeComponent();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {


            WeixinRobotLib.Entity.Linq.aspnet_UsersNewGameResultSend updatecode = Linq.Util_Services.GetServicesSetting();
            updatecode.ActiveCode = fd_activecode.Text;
            WeixinRoboot.RobootWeb.WebService ws = new WeixinRoboot.RobootWeb.WebService();
            string Res=ws.SaveSetting(GlobalParam.UserName, GlobalParam.Password, JsonConvert.SerializeObject(updatecode));
            MessageBox.Show(Res);
            this.Close ();

        }

        private void UpdateActiveCode_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
