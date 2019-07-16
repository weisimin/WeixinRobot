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
    public partial class PCWeChatSendImageSetting : Form
    {
        public PCWeChatSendImageSetting()
        {
            InitializeComponent();
            gv_subcontants.AutoGenerateColumns = false;
        }
        public StartForm SF;
        private void PCWeChatSendImageSetting_Load(object sender, EventArgs e)
        {



            BS_GV_PicSendSetting.DataSource = SF.InjectWins;
            BS_GVRandomTalk.DataMember = null;
           
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            BS_GV_PicSendSetting.DataSource = SF.InjectWins;
           
            foreach (WeixinRobotLib.Entity.Linq.WX_PCSendPicSetting loadset in SF.InjectWins.Where(t => t.Is_Reply == true))
            {
                if (loadset.GroupOwner != null && loadset.GroupOwner != "")
                {
                    
                    QqWindowHelper qh = new QqWindowHelper(new IntPtr(Convert.ToInt32(loadset.WX_UserTMPID)), "", false);
                    qh.ReloadMembers(loadset.GroupOwner, SF.RunnerF.MemberSource, loadset.WX_SourceType,  new IntPtr(Convert.ToInt32(loadset.WX_UserTMPID)));
                }

            }
            BS_GVRandomTalk.DataMember = null;
        }

        private void GV_PicSendSetting_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (GV_PicSendSetting.SelectedRows.Count > 0)
            {
                WeixinRobotLib.Entity.Linq.WX_PCSendPicSetting data = ((WeixinRobotLib.Entity.Linq.WX_PCSendPicSetting)GV_PicSendSetting.SelectedRows[0].DataBoundItem);

               
            }//有选择才执行新加

            foreach (var Injectitem in SF.InjectWins)
            {
               #region 同步采集模式勾



                DataRow[] rs = SF.RunnerF.MemberSource.Select("User_ContactTEMPID='" + Injectitem.WX_UserTMPID + "'");
                   foreach (var rowitem in rs)
                   {
                       rowitem.SetField("User_ChongqingMode", Injectitem.ChongqingMode);
                       rowitem.SetField("User_FiveMinuteMode", Injectitem.FiveMinuteMode);
                       rowitem.SetField("User_HkMode", Injectitem.HkMode);
                       rowitem.SetField("User_AozcMode", Injectitem.AozcMode);

                       rowitem.SetField("User_TengXunShiFen", Injectitem.Tengxunshifen);
                       rowitem.SetField("User_TengXunWuFen", Injectitem.Tengxunwufen);
                       rowitem.SetField("User_XinJiangShiShiCai", Injectitem.XinJiangMode);

                       //UserRow.SetField("User_ChongqingMode", false);
                       //UserRow.SetField("User_FiveMinuteMode", false);
                       //UserRow.SetField("User_HkMode", false);
                       //UserRow.SetField("User_AozcMode", false);
                       //UserRow.SetField("User_TengXunShiFen", false);
                       //UserRow.SetField("User_TengXunWuFen", true);
                       //UserRow.SetField("User_XinJiangShiShiCai", false);

                   }
                    #endregion 
            }
   
            SF.winsdb.SubmitChanges();


            btn_refresh_Click(sender, e);
        }

        private void GV_PicSendSetting_SelectionChanged(object sender, EventArgs e)
        {

         


        }
      

        private void gv_subcontants_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
           
        }

        private void gv_subcontants_Leave(object sender, EventArgs e)
        {
            

                DialogResult dr = MessageBox.Show("数据已更改，保存吗?", "警告", MessageBoxButtons.OKCancel);
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    btn_save_Click(null, null);
                }
            

        }

        private void gv_subcontants_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void MI_SelectFile_Click(object sender, EventArgs e)
        {
            FileDialog fd = new OpenFileDialog();
            DialogResult dr = fd.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
	{
        GV_PicSendSetting.CurrentCell.Value = fd.FileName;
	}
        }

        private void GV_PicSendSetting_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if  (GV_PicSendSetting.Columns[e.ColumnIndex].Name=="Text1PicPath")
            {
                MI_GridMouse.Show(Control.MousePosition.X,Control.MousePosition.Y);
            }
        }

     
       
    }
}
