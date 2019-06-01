using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
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
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            ep_wf.Clear();
            RobotWebAdmin.SysadminServices adws = new RobotWebAdmin.SysadminServices();
            adws.CookieContainer = GlobalParam.LoginCookie;
            RobootWeb.WebService usrws = new RobootWeb.WebService();
            if (fd_BossUserName.Text != "")
            {
                //string checkbossid = Membership.GetUser(fd_BossUserName.Text);
                //if (checkboss == null)
                //{
                //    ep_wf.SetError(fd_BossUserName, "老板号找不到");
                //    return;
               // }
            }

            switch (_Mode)
            {
                case "New":
                    try
                    {
                       JObject  usr = JObject.Parse( adws.CreateUser(fd_username.Text, fd_password.Text));
                        Linq.aspnet_UsersNewGameResultSend newGameResultSend = new Linq.aspnet_UsersNewGameResultSend();
                        newGameResultSend.aspnet_UserID = Guid.Parse(usr["ProviderUserKey"].ToString()) ;
                        newGameResultSend.IsNewSend = fd_NewGameSend.Checked;

                        newGameResultSend.IsBlock = Fd_IsBlock.Checked;

                        newGameResultSend.IsSendPIC = FD_SendPIC.Checked;
                        newGameResultSend.IsReceiveOrder = FD_ReceiveOrder.Checked;
                        newGameResultSend.MaxPlayerCount = Convert.ToInt32(fd_MaxPlayerCount.Text);

                        if (fd_activecode.Text == "" || fd_EndDate.Value.Date == DateTime.Today)
                        {
                            fd_EndDate.Value = DateTime.Today.AddMonths(3);
                            Btn_Build_Click(null, null);
                        }
                        newGameResultSend.ActiveCode = fd_activecode.Text;

                        newGameResultSend.BlockStartHour = 3;
                        newGameResultSend.BlockStartMinute = 11;
                        newGameResultSend.BlockEndHour = 7;
                        newGameResultSend.BlockEndMinute = 9;

                        //MembershipUser boss = Membership.GetUser(fd_BossUserName.Text);
                        //newGameResultSend.bossaspnet_UserID = (boss == null ? Guid.Empty : (Guid)boss.ProviderUserKey);

                        newGameResultSend.SendImageStart = Convert.ToInt32(fd_SendTimeStart1.Text);
                        newGameResultSend.SendImageEnd = Convert.ToInt32(fd_SendTimeEnd1.Text);


                        newGameResultSend.SendImageStart2 = Convert.ToInt32(fd_SendTimeStart2.Text);
                        newGameResultSend.SendImageEnd2 = Convert.ToInt32(fd_SendTimeEnd2.Text);

                        newGameResultSend.SendImageStart3 = Convert.ToInt32(fd_SendTimeStart3.Text);
                        newGameResultSend.SendImageEnd3 = Convert.ToInt32(fd_SendTimeEnd3.Text);

                        newGameResultSend.SendImageStart4 = Convert.ToInt32(fd_SendTimeStart4.Text);
                        newGameResultSend.SendImageEnd4 = Convert.ToInt32(fd_SendTimeEnd4.Text);

                        newGameResultSend.ImageTopText = fd_ImageTopText.Text;
                        newGameResultSend.ImageEndText = fd_ImageEndText.Text; ;



                        Linq.Util_Services.SaveServicesSetting(newGameResultSend);
                        Linq.Game_BasicRatio[] CopyRatio = (Linq.Game_BasicRatio[])JsonConvert.DeserializeObject(usrws.GetTemplateRatios());

                        if (CopyRatio.Count() != 0)
                        {
                            foreach (var item in CopyRatio)
                            {
                                Linq.Game_BasicRatio newr = new Linq.Game_BasicRatio();
                                newr.aspnet_UserID = Guid.Parse(usr["ProviderUserKey"].ToString());
                                newr.BasicRatio = item.BasicRatio;
                                newr.BuyType = item.BuyType;
                                newr.BuyValue = item.BuyValue;
                                newr.GameType = item.GameType;
                                newr.IncludeMin = item.IncludeMin;
                                newr.MaxBuy = item.MaxBuy;
                                newr.MinBuy = item.MinBuy;
                                newr.BonusBuyValueCondition = item.BonusBuyValueCondition;
                                newr.WX_SourceType = item.WX_SourceType;
                                newr.Enable = item.Enable;


                                newr.OrderIndex = item.OrderIndex;
                                db.Game_BasicRatio.InsertOnSubmit(newr);
                                db.SubmitChanges();
                            }
                        }


                        Linq.WX_BounsConfig[] BounsConfig = (Linq.WX_BounsConfig[])JsonConvert.DeserializeObject(usrws.GetTemplateBonus());

                        if (BounsConfig.Count() != 0)
                        {
                            foreach (var item in BounsConfig)
                            {
                                Linq.WX_BounsConfig newr = new Linq.WX_BounsConfig();
                                newr.aspnet_UserID = Guid.Parse(usr["ProviderUserKey"].ToString());
                                newr.RowNumber = item.RowNumber;
                                newr.StartBuyPeriod = item.StartBuyPeriod;
                                newr.EndBuyPeriod = item.EndBuyPeriod;
                                newr.StartBuyAverage = item.StartBuyAverage;
                                newr.EndBuyAverage = item.EndBuyAverage;
                                newr.FixNumber = item.FixNumber;
                                newr.FlowPercent = item.FlowPercent;
                                newr.IfDivousPercent = item.IfDivousPercent;

                                db.WX_BounsConfig.InsertOnSubmit(newr);
                                db.SubmitChanges();
                            }
                        }



                        MessageBox.Show("保存成功");
                    }
                    catch (Exception anyerror)
                    {

                        ep_wf.SetError(btn_Save, anyerror.Message + Environment.NewLine + anyerror.StackTrace);
                        fd_password.Enabled = true;
                    }

                    break;
                case "Modify":
                    try
                    {
                        bool success=true;
                      
                        JObject juser=  JObject.Parse( adws.GetUserInfo(fd_username.Text));
                        //MembershipUser user = System.Web.Security.Membership.GetUser(fd_username.Text);
                        if (fd_password.Text != "")
                        {
                            //string NewPassword = user.ResetPassword();
                            //user.ChangePassword(NewPassword, fd_password.Text);
                            usrws.ChangePassword(Guid.Parse(juser["ProviderUserKey"].ToString()), fd_password.Text);
                        }
                        if (fd_IsLock.Checked == false)
                        {
                            //user.UnlockUser();
                           success&=   adws.SetUserLock(fd_username.Text, false);
                        }
                        //System.Web.Security.Membership.UpdateUser(user);
                        if (fd_IsLock.Checked == true)
                        {
                            //Linq.aspnet_Users aspnet_Users = db.aspnet_Users.SingleOrDefault(t => t.UserId == new Guid(user.ProviderUserKey.ToString()));
                            //aspnet_Users.aspnet_Membership.IsLockedOut = true;
                            //db.SubmitChanges();
                            success &= adws.SetUserLock(fd_username.Text, true);
                        }

                        #region 开奖立即发送设置
                        Linq.aspnet_UsersNewGameResultSend finds = Linq.Util_Services.GetServicesSetting(Guid.Parse(juser["ProviderUserKey"].ToString()));
                        if (finds == null)
                        {
                            Linq.aspnet_UsersNewGameResultSend newGameResultSend = new Linq.aspnet_UsersNewGameResultSend();
                            newGameResultSend.aspnet_UserID = Guid.Parse(juser["ProviderUserKey"].ToString()); //(Guid)userguid.ProviderUserKey;
                            newGameResultSend.IsNewSend = fd_NewGameSend.Checked;
                            newGameResultSend.ActiveCode = fd_activecode.Text;
                            newGameResultSend.IsBlock = Fd_IsBlock.Checked;
                            newGameResultSend.IsSendPIC = FD_SendPIC.Checked;
                            newGameResultSend.IsReceiveOrder = FD_ReceiveOrder.Checked;
                            newGameResultSend.MaxPlayerCount = Convert.ToInt32(fd_MaxPlayerCount.Text);
                            //MembershipUser boss = Membership.GetUser(fd_BossUserName.Text);
                            //newGameResultSend.bossaspnet_UserID = (boss == null ? Guid.Empty : (Guid)boss.ProviderUserKey);

                            newGameResultSend.SendImageStart = Convert.ToInt32(fd_SendTimeStart1.Text);
                            newGameResultSend.SendImageEnd = Convert.ToInt32(fd_SendTimeEnd1.Text);

                            newGameResultSend.SendImageStart2 = Convert.ToInt32(fd_SendTimeStart2.Text);
                            newGameResultSend.SendImageEnd2 = Convert.ToInt32(fd_SendTimeEnd2.Text);

                            newGameResultSend.SendImageStart3 = Convert.ToInt32(fd_SendTimeStart3.Text);
                            newGameResultSend.SendImageEnd3 = Convert.ToInt32(fd_SendTimeEnd3.Text);

                            newGameResultSend.SendImageStart4 = Convert.ToInt32(fd_SendTimeStart4.Text);
                            newGameResultSend.SendImageEnd4 = Convert.ToInt32(fd_SendTimeEnd4.Text);

                            newGameResultSend.ImageTopText = fd_ImageTopText.Text;
                            newGameResultSend.ImageEndText = fd_ImageEndText.Text;

                            newGameResultSend.BlockStartHour = 3;
                            newGameResultSend.BlockStartMinute = 11;
                            newGameResultSend.BlockEndHour = 7;
                            newGameResultSend.BlockEndMinute = 9;

                            Linq.Util_Services.SaveServicesSetting(newGameResultSend);


                        }
                        else
                        {
                            finds.IsNewSend = fd_NewGameSend.Checked;
                            finds.IsBlock = fd_IsLock.Checked;
                            finds.IsSendPIC = FD_SendPIC.Checked;
                            finds.IsReceiveOrder = FD_ReceiveOrder.Checked;
                            finds.MaxPlayerCount = Convert.ToInt32(fd_MaxPlayerCount.Text);
                            finds.ActiveCode = fd_activecode.Text;

                            finds.IsBlock = Fd_IsBlock.Checked;
                            finds.IsSendPIC = FD_SendPIC.Checked;
                            finds.IsReceiveOrder = FD_ReceiveOrder.Checked;
                            finds.MaxPlayerCount = Convert.ToInt32(fd_MaxPlayerCount.Text);
                            //MembershipUser boss = Membership.GetUser(fd_BossUserName.Text);
                            //finds.bossaspnet_UserID = (boss == null ? Guid.Empty : (Guid)boss.ProviderUserKey);

                            finds.SendImageStart = Convert.ToInt32(fd_SendTimeStart1.Text);
                            finds.SendImageEnd = Convert.ToInt32(fd_SendTimeEnd1.Text);


                            finds.SendImageStart2 = Convert.ToInt32(fd_SendTimeStart2.Text);
                            finds.SendImageEnd2 = Convert.ToInt32(fd_SendTimeEnd2.Text);

                            finds.SendImageStart3 = Convert.ToInt32(fd_SendTimeStart3.Text);
                            finds.SendImageEnd3 = Convert.ToInt32(fd_SendTimeEnd3.Text);

                            finds.SendImageStart4 = Convert.ToInt32(fd_SendTimeStart4.Text);
                            finds.SendImageEnd4 = Convert.ToInt32(fd_SendTimeEnd4.Text);

                            finds.ImageTopText = fd_ImageTopText.Text;
                            finds.ImageEndText = fd_ImageEndText.Text; ;


                        }

                        fd_SendTimeStart1.Enabled = false;
                        fd_SendTimeEnd1.Enabled = false;

                        Linq.Util_Services.SaveServicesSetting(finds);

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
                    //MembershipUser usermydata = System.Web.Security.Membership.GetUser(fd_username.Text);
                    if (fd_password.Text != "")
                    {
                        //string NewPassword = usermydata.ResetPassword();
                        //usermydata.ChangePassword(NewPassword, fd_password.Text);
                        usrws.ChangePassword(GlobalParam.UserKey, fd_password.Text);
                    }
                   // System.Web.Security.Membership.UpdateUser(usermydata);
                    Linq.aspnet_UsersNewGameResultSend findsmydata = Linq.Util_Services.GetServicesSetting(GlobalParam.UserKey);
                    if (findsmydata == null)
                    {
                        Linq.aspnet_UsersNewGameResultSend newGameResultSend = new Linq.aspnet_UsersNewGameResultSend();
                        newGameResultSend.aspnet_UserID = GlobalParam.UserKey;
                        newGameResultSend.IsNewSend = fd_NewGameSend.Checked;
                        newGameResultSend.ActiveCode = fd_activecode.Text;
                        newGameResultSend.IsBlock = Fd_IsBlock.Checked;
                        newGameResultSend.IsSendPIC = FD_SendPIC.Checked;
                        newGameResultSend.IsReceiveOrder = FD_ReceiveOrder.Checked;
                        newGameResultSend.MaxPlayerCount = Convert.ToInt32(fd_MaxPlayerCount.Text);
                        //MembershipUser boss = Membership.GetUser(fd_BossUserName.Text);
                        //newGameResultSend.bossaspnet_UserID = (boss == null ? Guid.Empty : (Guid)boss.ProviderUserKey);

                        newGameResultSend.SendImageStart = Convert.ToInt32(fd_SendTimeStart1.Text);
                        newGameResultSend.SendImageEnd = Convert.ToInt32(fd_SendTimeEnd1.Text);



                        newGameResultSend.SendImageStart2 = Convert.ToInt32(fd_SendTimeStart2.Text);
                        newGameResultSend.SendImageEnd2 = Convert.ToInt32(fd_SendTimeEnd2.Text);

                        newGameResultSend.SendImageStart3 = Convert.ToInt32(fd_SendTimeStart3.Text);
                        newGameResultSend.SendImageEnd3 = Convert.ToInt32(fd_SendTimeEnd3.Text);

                        newGameResultSend.SendImageStart4 = Convert.ToInt32(fd_SendTimeStart4.Text);
                        newGameResultSend.SendImageEnd4 = Convert.ToInt32(fd_SendTimeEnd4.Text);

                        newGameResultSend.ImageTopText = fd_ImageTopText.Text;
                        newGameResultSend.ImageEndText = fd_ImageEndText.Text; ;

                        newGameResultSend.BlockStartHour = 3;
                        newGameResultSend.BlockStartMinute = 11;
                        newGameResultSend.BlockEndHour = 7;
                        newGameResultSend.BlockEndMinute = 9;

                        Linq.Util_Services.SaveServicesSetting(newGameResultSend);


                    }
                    else
                    {
                        findsmydata.IsNewSend = fd_NewGameSend.Checked;
                        findsmydata.IsBlock = fd_IsLock.Checked;
                        findsmydata.IsSendPIC = FD_SendPIC.Checked;
                        findsmydata.IsReceiveOrder = FD_ReceiveOrder.Checked;
                        findsmydata.MaxPlayerCount = Convert.ToInt32(fd_MaxPlayerCount.Text);
                        findsmydata.ActiveCode = fd_activecode.Text;
                        //MembershipUser boss = Membership.GetUser(fd_BossUserName.Text);
                        //findsmydata.bossaspnet_UserID = (boss == null ? Guid.Empty : (Guid)boss.ProviderUserKey);

                        findsmydata.SendImageStart = Convert.ToInt32(fd_SendTimeStart1.Text);
                        findsmydata.SendImageEnd = Convert.ToInt32(fd_SendTimeEnd1.Text);

                        findsmydata.SendImageStart2 = Convert.ToInt32(fd_SendTimeStart2.Text);
                        findsmydata.SendImageEnd2 = Convert.ToInt32(fd_SendTimeEnd2.Text);

                        findsmydata.SendImageStart3 = Convert.ToInt32(fd_SendTimeStart3.Text);
                        findsmydata.SendImageEnd3 = Convert.ToInt32(fd_SendTimeEnd3.Text);

                        findsmydata.SendImageStart4 = Convert.ToInt32(fd_SendTimeStart4.Text);
                        findsmydata.SendImageEnd4 = Convert.ToInt32(fd_SendTimeEnd4.Text);


                        findsmydata.ImageTopText = fd_ImageTopText.Text;
                        findsmydata.ImageEndText = fd_ImageEndText.Text; ;
                        Linq.Util_Services.SaveServicesSetting(findsmydata);
                    }




                    break;
                default:
                    break;
            }//按模式操作
            fd_password.Text = "";
            fd_password.Enabled = false;
            fd_IsLock.Checked = false;
            fd_IsLock.Enabled = false;
            db.SubmitChanges();
            fd_ImageEndText.Enabled = false;
            fd_ImageTopText.Enabled = false;

            MessageBox.Show("保存成功");
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
                    fd_EndDate.Enabled = true;
                    Btn_Build.Visible = true;
                    Btn_Build.Enabled = false;
                    break;
                case "Modify":
                    fd_password.Enabled = false;
                    fd_IsLock.Enabled = false;
                    btn_Save.Enabled = false;
                    fd_EndDate.Enabled = true;
                    Btn_Build.Visible = true;
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
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            fd_SendTimeEnd1.Enabled = true;
            fd_SendTimeStart1.Enabled = true;
            RobotWebAdmin.SysadminServices adws = new RobotWebAdmin.SysadminServices();
            adws.CookieContainer = GlobalParam.LoginCookie;
            RobootWeb.WebService usrws = new RobootWeb.WebService();
            try
            {
                Newtonsoft.Json.Linq.JObject Juser = null;
                if (_Mode == "MyData")
                {
                    Juser = Newtonsoft.Json.Linq.JObject.Parse(usrws.GetUserInfo(GlobalParam.UserKey));
                }
                else
                { 
               Juser=  Newtonsoft.Json.Linq.JObject.Parse( adws.GetUserInfo(fd_username.Text));
                }
                   
               
                if (Juser != null)
                {
                    fd_password.Enabled = true;
                    fd_IsLock.Enabled = true;

                    fd_IsLock.Checked = Convert.ToBoolean(Juser["IsLockedOut"].ToString());


                    btn_Save.Enabled = true;
                    Btn_Build.Enabled = true;

                    fd_ImageEndText.Enabled = true;
                    fd_ImageTopText.Enabled = true;

                    Linq.aspnet_UsersNewGameResultSend newgs = Linq.Util_Services.GetServicesSetting(Guid.Parse(Juser["ProviderUserKey"].ToString()));
                    if (newgs == null)
                    {
                        fd_NewGameSend.Checked = false;

                        fd_IsLock.Checked = false;
                        FD_SendPIC.Checked = false;
                        FD_ReceiveOrder.Checked = false;
                        fd_MaxPlayerCount.Text = "500";

                        fd_SendTimeStart1.Text = "0";
                        fd_SendTimeEnd1.Text = "24";

                    }
                    else
                    {
                        fd_NewGameSend.Checked = newgs.IsNewSend.HasValue ? newgs.IsNewSend.Value : false;
                        fd_activecode.Text = newgs.ActiveCode;
                        DateTime? LastDate = null;
                        NetFramework.Util_MD5.MD5Success(newgs.ActiveCode, out LastDate, newgs.aspnet_UserID);
                        fd_EndDate.Value = LastDate.HasValue ? LastDate.Value : fd_EndDate.MinDate;


                        fd_IsLock.Checked = newgs.IsBlock.HasValue ? newgs.IsBlock.Value : false; ;
                        FD_SendPIC.Checked = newgs.IsSendPIC.HasValue ? newgs.IsSendPIC.Value : false; ;
                        FD_ReceiveOrder.Checked = newgs.IsReceiveOrder.HasValue ? newgs.IsReceiveOrder.Value : false; ;
                        fd_MaxPlayerCount.Text = newgs.MaxPlayerCount.HasValue ? newgs.MaxPlayerCount.ToString() : "50";
                        //System.Web.Security.MembershipUser boss = System.Web.Security.Membership.GetUser(newgs.bossaspnet_UserID == null ? Guid.Empty : newgs.bossaspnet_UserID);
                        //fd_BossUserName.Text = (boss == null ? "" : boss.UserName);

                        fd_SendTimeStart1.Text = Object2Str(newgs.SendImageStart, "0");
                        fd_SendTimeEnd1.Text = Object2Str(newgs.SendImageEnd, "24");

                        fd_SendTimeStart2.Text = Object2Str(newgs.SendImageStart2, "0");
                        fd_SendTimeEnd2.Text = Object2Str(newgs.SendImageEnd2, "24");

                        fd_SendTimeStart3.Text = Object2Str(newgs.SendImageStart3, "0");
                        fd_SendTimeEnd3.Text = Object2Str(newgs.SendImageEnd3, "24");

                        fd_SendTimeStart4.Text = Object2Str(newgs.SendImageStart4, "0");
                        fd_SendTimeEnd4.Text = Object2Str(newgs.SendImageEnd4, "24");


                        fd_ImageTopText.Text = Object2Str(newgs.ImageTopText);
                        fd_ImageEndText.Text = Object2Str(newgs.ImageEndText);
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
                Btn_Load_Click(null, null);

            }

        }

        private void UserSetting_Load(object sender, EventArgs e)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            RobotWebAdmin.SysadminServices adws = new  RobotWebAdmin.SysadminServices();
            adws.CookieContainer = new System.Net.CookieContainer();
            System.Net.Cookie login= new  System.Net.Cookie(".ASPXAUTH",GlobalParam.ASPXAUTH);
           
            adws.CookieContainer=GlobalParam.LoginCookie;

            var source = adws.GetAllUsers();
            BS_UserList.DataSource = source;
        }

        private void Btn_Build_Click(object sender, EventArgs e)
        {
            RobotWebAdmin.SysadminServices adws = new RobotWebAdmin.SysadminServices();
            adws.CookieContainer = new System.Net.CookieContainer();
           JObject Juser=  JObject.Parse( adws.GetUserInfo(fd_username.Text));
           fd_activecode.Text = adws.BuidMD5ActiveCode(fd_EndDate.Value, Guid.Parse(Juser["ProviderUserKey"].ToString()));
        }

        private string Object2Str(object param, string NullValue = "")
        {

            return param == null ? NullValue : param.ToString();
        }
    }
}
