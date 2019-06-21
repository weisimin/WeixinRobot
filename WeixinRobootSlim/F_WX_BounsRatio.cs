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
                
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                //db.ObjectTrackingEnabled = false;
                db.SubmitChanges();
                BS_GV_DATA.DataSource = db.WX_BounsConfig.Where(t => t.aspnet_UserID == GlobalParam.UserKey).OrderBy(t => t.RowNumber); ;
                MessageBox.Show("保存成功");
            }
            catch (Exception AnyError)
            {
                MessageBox.Show(AnyError.Message);
                System.Data.Linq.ChangeSet cs = db.GetChangeSet();
                cs.Inserts.Clear();
                cs.Deletes.Clear();
                db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, cs.Updates);
                BS_GV_DATA.DataSource = db.WX_BounsConfig.Where(t => t.aspnet_UserID == GlobalParam.UserKey);


            }

        }

        private void GV_DATA_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

       
        private void F_WX_BounsRatio_Load(object sender, EventArgs e)
        {
            BS_GV_DATA.DataSource = db.WX_BounsConfig.Where(t => t.aspnet_UserID == GlobalParam.UserKey).OrderBy(t=>t.RowNumber);
        
        }
    }
}
