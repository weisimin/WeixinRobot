using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Xml;


namespace WeixinRoboot
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            OnLoginSuccess += new LoginSuccess(Login_OnLoginSuccess);
        }

        void Login_OnLoginSuccess(string UserName)
        {
            this.Hide();
        
        }

        private void Login_Load(object sender, EventArgs e)
        {
           
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            try
            {

                #region 不使用WEBSERVICE的登录
                bool? Success = NetFramework.Util_User.ValidateUser(tb_UserName.Text, tb_pwd.Text);
                if (Success == null)
                {
                    MessageBox.Show("找不到用户名:" + tb_UserName.Text);
                }
                else if (Success.Value == true)
                {
                    GlobalParam.UserName = tb_UserName.Text;
                    GlobalParam.LogInSuccess = true;
                    GlobalParam.Key = (Guid)System.Web.Security.Membership.GetUser(tb_UserName.Text).ProviderUserKey;
                    GlobalParam.db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
                    OnLoginSuccess(tb_UserName.Text);
                }
                else if (Success.Value == false)
                {
                    MessageBox.Show("密码错误");
                }
                #endregion
            }
            catch (Exception AnyError)
            {

                MessageBox.Show(AnyError.Message+Environment.NewLine+AnyError.StackTrace);
            }
        }
        public delegate void LoginSuccess(string UserName);
        public event LoginSuccess OnLoginSuccess;

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
