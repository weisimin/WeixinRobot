using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeixinRobotLib;
namespace WeixinRoboot
{
    public partial class F_WX_BounsRatio : Form
    {
        public F_WX_BounsRatio()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                RobootWeb.WebService ws = new RobootWeb.WebService();
                ws.SaveBounsConfig(GlobalParam.GetUserParam(), Newtonsoft.Json.JsonConvert.SerializeObject( (WeixinRobotLib.Linq.WX_BounsConfig[])BS_GV_DATA.DataSource));
               
                MessageBox.Show("保存成功");
            }
            catch (Exception AnyError)
            {
                MessageBox.Show(AnyError.Message);
               

            }

        }

        private void GV_DATA_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

       
        private void F_WX_BounsRatio_Load(object sender, EventArgs e)
        {
             RobootWeb.WebService ws = new RobootWeb.WebService();
            BS_GV_DATA.DataSource = ws.GetBounsConfig(GlobalParam.GetUserParam());
        
        }
    }
}
