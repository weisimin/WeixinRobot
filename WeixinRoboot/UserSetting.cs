using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.Security;
namespace WeixinRoboot
{
    public partial class UserSetting : Form
    {
        public UserSetting()
        {
            InitializeComponent();

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            switch (_Mode)
            {
                case "New":
                    try
                    {
                        MembershipUser usr = System.Web.Security.Membership.CreateUser(fd_username.Text, fd_password.Text);
                        Linq.aspnet_UsersNewGameResultSend newGameResultSend = new Linq.aspnet_UsersNewGameResultSend();
                        newGameResultSend.aspnet_UserID = (Guid)usr.ProviderUserKey;
                        newGameResultSend.IsNewSend = fd_NewGameSend.Checked;
                        newGameResultSend.ActiveCode = fd_activecode.Text;
                        newGameResultSend.IsBlock = Fd_IsBlock.Checked;

                        newGameResultSend.IsSendPIC = FD_SendPIC.Checked;
                        newGameResultSend.IsReceiveOrder = FD_ReceiveOrder.Checked;
                        newGameResultSend.MaxPlayerCount = Convert.ToInt32(fd_MaxPlayerCount.Text);

                        db.aspnet_UsersNewGameResultSend.InsertOnSubmit(newGameResultSend);
                        db.SubmitChanges();

                        MembershipUser sysadmin = System.Web.Security.Membership.GetUser("sysadmin");



                        var CopyRatio = db.Game_BasicRatio.Where(t => t.aspnet_UserID == (sysadmin == null ? Guid.Empty : (Guid)sysadmin.ProviderUserKey));

                        if (CopyRatio.Count() != 0)
                        {
                            foreach (var item in CopyRatio)
                            {
                                Linq.Game_BasicRatio newr = new Linq.Game_BasicRatio();
                                newr.aspnet_UserID = (Guid)usr.ProviderUserKey;
                                newr.BasicRatio = item.BasicRatio;
                                newr.BuyType = item.BuyType;
                                newr.BuyValue = item.BuyValue;
                                newr.GameType = item.GameType;
                                newr.IncludeMin = item.IncludeMin;
                                newr.MaxBuy = item.MaxBuy;
                                newr.MinBuy = item.MinBuy;

                                newr.OrderIndex = item.OrderIndex;
                                db.Game_BasicRatio.InsertOnSubmit(newr);
                                db.SubmitChanges();
                            }
                        }

                        MessageBox.Show("保存成功");
                    }
                    catch (Exception anyerror)
                    {

                        ep_wf.SetError(btn_Save, anyerror.Message + Environment.NewLine + anyerror.StackTrace);
                    }

                    break;
                case "Modify":
                    try
                    {

                        MembershipUser user = System.Web.Security.Membership.GetUser(fd_username.Text);
                        if (fd_password.Text != "")
                        {
                            string NewPassword = user.ResetPassword();
                            user.ChangePassword(NewPassword, fd_password.Text);
                        }
                        if (fd_IsLock.Checked == false)
                        {
                            user.UnlockUser();
                        }
                        System.Web.Security.Membership.UpdateUser(user);
                        if (fd_IsLock.Checked == true)
                        {
                            Linq.aspnet_Users aspnet_Users = db.aspnet_Users.SingleOrDefault(t => t.UserId == new Guid(user.ProviderUserKey.ToString()));
                            aspnet_Users.aspnet_Membership.IsLockedOut = true;
                            db.SubmitChanges();
                        }

                        #region 开奖立即发送设置
                        Linq.aspnet_UsersNewGameResultSend finds = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == (Guid)user.ProviderUserKey);
                        if (finds == null)
                        {
                            Linq.aspnet_UsersNewGameResultSend newGameResultSend = new Linq.aspnet_UsersNewGameResultSend();
                            newGameResultSend.aspnet_UserID = (Guid)user.ProviderUserKey;
                            newGameResultSend.IsNewSend = fd_NewGameSend.Checked;
                            newGameResultSend.ActiveCode = fd_activecode.Text;
                            newGameResultSend.IsBlock = Fd_IsBlock.Checked;
                            newGameResultSend.IsSendPIC = FD_SendPIC.Checked;
                            newGameResultSend.IsReceiveOrder = FD_ReceiveOrder.Checked;
                            newGameResultSend.MaxPlayerCount = Convert.ToInt32(fd_MaxPlayerCount.Text);

                            db.aspnet_UsersNewGameResultSend.InsertOnSubmit(newGameResultSend);


                        }
                        else
                        {
                            finds.IsNewSend = fd_NewGameSend.Checked;
                            finds.IsBlock = fd_IsLock.Checked;
                            finds.IsSendPIC = FD_SendPIC.Checked;
                            finds.IsReceiveOrder = FD_ReceiveOrder.Checked;
                            finds.MaxPlayerCount = Convert.ToInt32(fd_MaxPlayerCount.Text);

                        }
                        db.SubmitChanges();

                        #endregion


                        MessageBox.Show("保存成功");


                    }
                    catch (Exception anyerror)
                    {

                        ep_wf.SetError(btn_Save, anyerror.Message + Environment.NewLine + anyerror.StackTrace);
                    }
                    break;
                case "MyData":
                    MembershipUser usermydata = System.Web.Security.Membership.GetUser(fd_username.Text);
                    if (fd_password.Text != "")
                    {
                        string NewPassword = usermydata.ResetPassword();
                        usermydata.ChangePassword(NewPassword, fd_password.Text);
                    }
                    System.Web.Security.Membership.UpdateUser(usermydata);
                    #region 开奖立即发送设置
                    Linq.aspnet_UsersNewGameResultSend findsmydata = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == (Guid)usermydata.ProviderUserKey);
                    if (findsmydata == null)
                    {
                        Linq.aspnet_UsersNewGameResultSend newGameResultSend = new Linq.aspnet_UsersNewGameResultSend();
                        newGameResultSend.aspnet_UserID = (Guid)usermydata.ProviderUserKey;
                        newGameResultSend.IsNewSend = fd_NewGameSend.Checked;
                        db.aspnet_UsersNewGameResultSend.InsertOnSubmit(newGameResultSend);
                    }
                    else
                    {
                        findsmydata.IsNewSend = fd_NewGameSend.Checked;
                    }
                    db.SubmitChanges();

                    #endregion


                    break;
                default:
                    break;
            }//按模式操作
            fd_password.Text = "";
            fd_password.Enabled = false;
            fd_IsLock.Checked = false;
            fd_IsLock.Enabled = false;
        }//函数结束

        private string _Mode = "";
        public void SetMode(string Mode)
        {
            _Mode = Mode;
            switch (Mode)
            {
                case "New":
                    Btn_Load.Visible = false;
                    fd_EndDate.Value = DateTime.Today.AddMonths(3);
                    break;
                case "Modify":
                    fd_password.Enabled = false;
                    fd_IsLock.Enabled = false;
                    btn_Save.Enabled = false;
                    break;
                case "MyData":
                    fd_username.Enabled = false;
                    TC_Main.Controls.Remove(TP_UserList);
                    fd_IsLock.Visible = false;
                    lbl_Islock.Visible = false;

                    fd_password.Enabled = false;
                    fd_IsLock.Enabled = false;
                    btn_Save.Enabled = false;

                    fd_EndDate.Enabled = false;
                    Btn_Build.Visible = false;

                    lbl_pic.Visible = false;
                    lbl_order.Visible = false;
                    lbl_tracecount.Visible = false;

                    FD_SendPIC.Visible = false;
                    FD_ReceiveOrder.Visible = false;
                    fd_MaxPlayerCount.Visible = false;
                    break;
                default:
                    break;
            }
        }



        private void Btn_Load_Click(object sender, EventArgs e)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            try
            {
                MembershipUser usr = Membership.GetUser(fd_username.Text);
                if (usr != null)
                {
                    fd_password.Enabled = true;
                    fd_IsLock.Enabled = true;

                    fd_IsLock.Checked = usr.IsLockedOut;


                    btn_Save.Enabled = true;


                    Linq.aspnet_UsersNewGameResultSend newgs = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == (Guid)usr.ProviderUserKey);
                    if (newgs == null)
                    {
                        fd_NewGameSend.Checked = false;

                        fd_IsLock.Checked = false;
                        FD_SendPIC.Checked = false;
                        FD_ReceiveOrder.Checked = false;
                        fd_MaxPlayerCount.Text = "50";

                    }
                    else
                    {
                        fd_NewGameSend.Checked = newgs.IsNewSend.HasValue ? newgs.IsNewSend.Value : false;
                        fd_activecode.Text = newgs.ActiveCode;
                        DateTime? LastDate = null;
                        NetFramework.Util_MD5.MD5Success(newgs.ActiveCode, out LastDate, GlobalParam.Key);
                        fd_EndDate.Value = LastDate.HasValue ? LastDate.Value : fd_EndDate.MinDate;


                        fd_IsLock.Checked = newgs.IsBlock.HasValue ? newgs.IsBlock.Value : false; ;
                        FD_SendPIC.Checked = newgs.IsSendPIC.HasValue ? newgs.IsSendPIC.Value : false; ;
                        FD_ReceiveOrder.Checked = newgs.IsReceiveOrder.HasValue ? newgs.IsReceiveOrder.Value : false; ;
                        fd_MaxPlayerCount.Text = newgs.MaxPlayerCount.HasValue ? newgs.MaxPlayerCount.ToString() : "50";



                    }

                }
                else
                {
                    throw new Exception("用户找不到");
                }

            }
            catch (Exception AnyError)
            {
                ep_wf.SetError(Btn_Load, AnyError.Message + Environment.NewLine + AnyError.StackTrace);

            }


        }

        private void gv_UserList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (gv_UserList.SelectedRows.Count != 0)
            {
                fd_username.Text = gv_UserList.SelectedRows[0].Cells["UserName"].Value.ToString();
                TC_Main.SelectedTab = TP_Data;
                TP_Data.Show();

            }

        }

        private void UserSetting_Load(object sender, EventArgs e)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            var source = from ms in db.aspnet_Membership
                         join us in db.aspnet_Users on ms.UserId equals us.UserId
                         select new { us.UserId, us.UserName, ms.IsLockedOut };
            BS_UserList.DataSource = source;
        }

        private void Btn_Build_Click(object sender, EventArgs e)
        {
            fd_activecode.Text = NetFramework.Util_MD5.BuidMD5ActiveCode(fd_EndDate.Value, (Guid)Membership.GetUser(fd_username.Text).ProviderUserKey);
        }

    }
}
