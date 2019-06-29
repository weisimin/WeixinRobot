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
    public partial class F_Game_BasicRatio : Form
    {

        public F_Game_BasicRatio()
        {
            InitializeComponent();

        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {

            try
            {
                ep_gridview.Clear();
                try
                {
                    RobootWeb.WebService ws = new RobootWeb.WebService();
                    ws.SaveBasicRatio(GlobalParam.GetUserParam(),)
                }
                catch (Exception AnyError)
                {
                    ep_gridview.SetError(Btn_Save, AnyError.Message);
                }


                MessageBox.Show("保存成功");
            }
            catch (Exception AnyError)
            {
                ep_gridview.SetError(gv_Game_BasicRatio, AnyError.Message + Environment.NewLine + AnyError.StackTrace);
                MessageBox.Show("保存失败");
            }

        }

        private void gv_Game_BasicRatio_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (gv_Game_BasicRatio.Rows[e.RowIndex].Cells["aspnet_UserID"].Value == null)
            {
                gv_Game_BasicRatio.Rows[e.RowIndex].Cells["aspnet_UserID"].Value = GlobalParam.UserKey;
            }



        }

        private void gv_Game_BasicRatio_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }


       


        private void F_Game_BasicRatio_Load(object sender, EventArgs e)
        {

            RobootWeb.WebService ws = new RobootWeb.WebService();
            BS_Game_BasicRatio.DataSource =ws.GetBasicRatio(GlobalParam.GetUserParam());

        }




    }
}
