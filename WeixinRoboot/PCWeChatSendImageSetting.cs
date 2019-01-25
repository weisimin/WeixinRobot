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
            gv_subcontants.AutoGenerateColumns = false;
        }
        public StartForm SF;
        private void PCWeChatSendImageSetting_Load(object sender, EventArgs e)
        {



            BS_GV_PicSendSetting.DataSource = SF.InjectWins;
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            BS_GV_PicSendSetting.DataSource = SF.InjectWins;

            foreach (Linq.WX_PCSendPicSetting loadset in SF.InjectWins.Where(t => t.Is_Reply == true))
            {
                if (loadset.GroupOwner != null && loadset.GroupOwner != "")
                {
                    Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
                    db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

                    QqWindowHelper qh = new QqWindowHelper(new IntPtr(Convert.ToInt32(loadset.WX_UserTMPID)), "", false);
                    qh.ReloadMembers(loadset.GroupOwner, SF.RunnerF.MemberSource, loadset.WX_SourceType, db, new IntPtr(Convert.ToInt32(loadset.WX_UserTMPID)));
                }

            }

        }

        private void GV_PicSendSetting_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (GV_PicSendSetting.SelectedRows.Count > 0)
            {
                Linq.WX_PCSendPicSetting data = ((Linq.WX_PCSendPicSetting)GV_PicSendSetting.SelectedRows[0].DataBoundItem);

                foreach (var contactitem in bs)
                {
                    if (SF.winsdb.WX_PCSendPicSettingRandomTalk.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                        && t.WX_UserName == contactitem.WX_UserName
                        && t.WX_SourceType == contactitem.WX_SourceType
                        && t.MessageIndex == contactitem.MessageIndex
                        ) == null)
                    {
                        contactitem.aspnet_UserID = data.aspnet_UserID;
                        contactitem.WX_UserName = data.WX_UserName;
                        contactitem.WX_SourceType = data.WX_SourceType;
                        SF.winsdb.WX_PCSendPicSettingRandomTalk.InsertOnSubmit(contactitem);

                    }//数据库不存在的
                }//数据循环
            }//有选择才执行新加

            SF.winsdb.SubmitChanges();


            btn_refresh_Click(sender, e);
        }

        private void GV_PicSendSetting_SelectionChanged(object sender, EventArgs e)
        {

            bs.Clear();

            if (GV_PicSendSetting.SelectedRows.Count > 0)
            {
                Linq.WX_PCSendPicSetting data = ((Linq.WX_PCSendPicSetting)GV_PicSendSetting.SelectedRows[0].DataBoundItem);
                var contancts = SF.winsdb.WX_PCSendPicSettingRandomTalk.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                                 && t.WX_SourceType == data.WX_SourceType
                                 && t.WX_UserName == data.WX_UserName
                                 );
                foreach (var conitem in contancts)
                {

                  
                    bs.Add(conitem);
                }
            }
            gv_subcontants.DataSource = bs;

        }
        BindingList<Linq.WX_PCSendPicSettingRandomTalk> bs = new BindingList<Linq.WX_PCSendPicSettingRandomTalk>();

        private void gv_subcontants_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
           
        }

        private void gv_subcontants_Leave(object sender, EventArgs e)
        {
            if (bs.Count > 0)
            {


                DialogResult dr = MessageBox.Show("数据已更改，保存吗?", "警告", MessageBoxButtons.OKCancel);
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    btn_save_Click(null, null);
                }
            }

        }

     
       
    }
}
