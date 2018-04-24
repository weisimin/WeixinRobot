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
            GlobalParam.db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
           
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                ep_gridview.Clear();
                GlobalParam.db.SubmitChanges();
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
            gv_Game_BasicRatio.Rows[e.RowIndex].Cells["aspnet_UserID"].Value = GlobalParam.Key;
        }

        private void gv_Game_BasicRatio_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }




        private void F_Game_BasicRatio_Load(object sender, EventArgs e)
        {

            BS_Game_BasicRatio.DataSource = GlobalParam.db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.Key);
       
        }

      

      
         }
}
