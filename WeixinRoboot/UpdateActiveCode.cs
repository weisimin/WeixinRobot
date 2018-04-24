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
    public partial class UpdateActiveCode : Form
    {
        public UpdateActiveCode()
        {
            InitializeComponent();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Linq.aspnet_UsersNewGameResultSend updatecode = GlobalParam.db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key);
            updatecode.ActiveCode = fd_activecode.Text;
            GlobalParam.db.SubmitChanges();
            this.Close ();

        }

        private void UpdateActiveCode_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
