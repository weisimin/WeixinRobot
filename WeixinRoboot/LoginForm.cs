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
using System.Web.Security;
using System.Reflection;
using System.Web.Profile;
namespace WeixinRoboot
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            OnLoginSuccess += new LoginSuccess(Login_OnLoginSuccess);
            cb_datasource.SelectedItem = "远程服务器";
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
                    GlobalParam.UserKey = (Guid)System.Web.Security.Membership.GetUser(tb_UserName.Text).ProviderUserKey;
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

        private void cb_datasource_SelectedValueChanged(object sender, EventArgs e)
        {
            GlobalParam.DataSourceName = cb_datasource.SelectedItem.ToString() == "本机" ? "LocalSqlServer" : "RemoteSqlServer";
            SetProviderConnectionString(ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
        }
        /// <summary>
        /// Sets the provider connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        private void SetProviderConnectionString(string connectionString)
        {
            var connectionStringField = Membership.Provider.GetType().GetField("_sqlConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
            if (connectionStringField != null)
                connectionStringField.SetValue(Membership.Provider, connectionString);

            var roleField = Roles.Provider.GetType().GetField("_sqlConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
            if (roleField != null)
                roleField.SetValue(Roles.Provider, connectionString);

            var profileField = ProfileManager.Provider.GetType().GetField("_sqlConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
            if (profileField != null)
                profileField.SetValue(ProfileManager.Provider, connectionString);
        }
    }
}
