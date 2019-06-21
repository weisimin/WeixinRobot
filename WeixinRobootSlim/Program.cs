using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Net;
using System.Security.AccessControl;
//xl1234567密码123456
//http://down.1goubao.com/hy-android-new/
namespace WeixinRoboot
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();

        static void AddSecurityControll2File(string filePath)
        {

            //获取文件信息
            FileInfo fileInfo = new FileInfo(filePath);
            //获得该文件的访问权限
            System.Security.AccessControl.FileSecurity fileSecurity = fileInfo.GetAccessControl();
            //添加ereryone用户组的访问权限规则 完全控制权限
            fileSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
            //添加Users用户组的访问权限规则 完全控制权限
            fileSecurity.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));
            //设置访问权限
            fileInfo.SetAccessControl(fileSecurity);
        }
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {


            if (File.Exists(Application.StartupPath + "\\EasyRoboot.mdf"))
            {
                File.Delete(Application.StartupPath + "\\EasyRoboot.mdf");
                File.Delete(Application.StartupPath + "\\EasyRoboot_log.ldf");
            }
            if (File.Exists(Application.StartupPath + "\\EasyRoboot.mdf.bak"))
            {
                File.Copy(Application.StartupPath + "\\EasyRoboot.mdf.bak", Application.StartupPath + "\\EasyRoboot.mdf");
                File.Copy(Application.StartupPath + "\\EasyRoboot_log.ldf.bak", Application.StartupPath + "\\EasyRoboot_log.ldf");
            }
            if (File.Exists("E:\\EasyRoboot.mdf") == false)
            {
                File.Copy(Application.StartupPath + "\\EasyRoboot.mdf", "E:\\EasyRoboot.mdf");
                File.Copy(Application.StartupPath + "\\EasyRoboot_log.ldf", "E:\\EasyRoboot_log.ldf");
                AddSecurityControll2File("E:\\EasyRoboot.mdf");
                AddSecurityControll2File("E:\\EasyRoboot_log.ldf");
            }
            EO.Base.Runtime.EnableEOWP = true;
            EO.WebBrowser.Runtime.AddLicense("f5mkwOm7aNjw/Rr2d7PzAw/kq8Dy9xqfndj49uihaamzwd2ua6e1yM2fr9z2BBTup7SmwuKhaLXABBTmp9j4Bh3kd9nYBw/kcN3l6vrYasH7+xG0sru1xuy8drOzBBTmp9j4Bh3kd7Oz/RTinuX39ul14+30EO2s3MLNF+ic3PIEEMidtbTG27ZwrbXG3LN1pvD6DuSn6unaD7114+30EO2s3OmxGeCm3MGz8M5nzunz7fGo7vf2HaF3s7P9FOKe5ff2EL112PD9GvZ3s+X1D5+t8PT26KF+xrLUE/Go5Omzy5+v3PYEFO6ntKbC4q1p");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            AppDomain.CurrentDomain.SetData("DataDirectory", Application.StartupPath);
            Action run = () =>
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);



                // AllocConsole();
                try
                {

                    string ConfigFile = Application.StartupPath + "\\WeixinRoboot.exe.config";
                    string TempFileName = Application.StartupPath + "\\web.config";
                    if (System.IO.File.Exists(TempFileName))
                    {
                        System.IO.File.Delete(TempFileName);
                    }
                    System.IO.File.Copy(ConfigFile, TempFileName);



                    //ConfigFile = Application.StartupPath + "\\OpenWebKitSharp.manifest.bak";
                    //TempFileName = Application.StartupPath + "\\OpenWebKitSharp.manifest";
                    //if (System.IO.File.Exists(TempFileName))
                    //{
                    //    System.IO.File.Delete(TempFileName);
                    //}
                    //System.IO.File.Copy(ConfigFile, TempFileName);

                    //System.Diagnostics.Process proc = System.Diagnostics.Process.Start("C:\\Windows\\Microsoft.NET\\Framework\\v2.0.50727\\aspnet_regiis.exe", "-pef \"connectionStrings\" \"" + Application.StartupPath + "\"");


                    //if (proc != null)
                    //{
                    //    proc.WaitForExit();
                    //    System.IO.File.Move(TempFileName, ConfigFile);
                    //}

                    if (Directory.Exists(Application.StartupPath + "\\output") == true)
                    {
                        string[] todels = Directory.GetFiles(Application.StartupPath + "\\output");
                        foreach (var item in todels)
                        {
                            File.Delete(item);
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(Application.StartupPath + "\\output");
                    }
                    if (Directory.Exists(Application.StartupPath + "\\EmuFile") == true)
                    {
                        string[] todels = Directory.GetFiles(Application.StartupPath + "\\EmuFile");
                        foreach (var item in todels)
                        {
                            File.Delete(item);
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(Application.StartupPath + "\\EmuFile");
                    }
                    //WeixinRobotLib.Linq.dbDataContext db = new WeixinRobotLib.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[ GlobalParam.DataSourceName].ConnectionString);
                    ////db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                    //var todel = db.Game_FootBall_VS.Where(t => t.LastAliveTime < DateTime.Now.AddDays(-3)

                    //    );
                    //foreach (var item in todel)
                    //{
                    //    var todeldetail = db.Game_FootBall_VSRatios.Where(t => t.aspnet_UserID == item.aspnet_UserID && t.GameKey == item.GameKey);
                    //    db.Game_FootBall_VSRatios.DeleteAllOnSubmit(todeldetail);

                    //    var todellast = db.Game_Football_LastRatio.Where(t => t.aspnet_UserID == GlobalParam.UserKey && t.GameKey == item.GameKey);
                    //    db.Game_Football_LastRatio.DeleteAllOnSubmit(todellast);

                    //}


                    //db.Game_FootBall_VS.DeleteAllOnSubmit(todel);


                    //db.SubmitChanges();
                }
                catch (Exception AnyError)
                {
                    MessageBox.Show("启动失败" + AnyError.Message);
                    NetFramework.Console.WriteLine("删除临时图片失败", true);
                    NetFramework.Console.WriteLine(AnyError.Message, true);
                    NetFramework.Console.WriteLine(AnyError.StackTrace, true);
                    return;
                }

                //局部线程，不能及时结束会造成没相应
                //全局LINQ数据库，会频繁出现SQLDATAREADER已打开或关闭的问题

                WeixinRobotLib.Linq.ProgramLogic.ComboStringInit();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                loginf = new LoginForm();
                loginf.OnLoginSuccess += new LoginForm.LoginSuccess(loginf_OnLoginSuccess);
                //try
                //{
                Application.Run(loginf);
                //}
                //catch (Exception AnyError)
                //{
                ////    MessageBox.Show(AnyError.Message);
                ////    if (System.IO.File.Exists(Application.StartupPath + "\\log.txt"))
                ////    {
                ////        System.IO.File.Delete(Application.StartupPath + "\\log.txt");
                ////    }
                ////    System.IO.FileStream fs = new System.IO.FileStream(Application.StartupPath + "\\log.txt", System.IO.FileMode.OpenOrCreate);
                ////    System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
                ////    sw.Write(AnyError.Message + Environment.NewLine + AnyError.StackTrace);
                ////    sw.Flush();
                ////    sw.Close();
                ////    //Environment.Exit(0);

                //}
                //CefSharp.Cef.Shutdown();
                //FreeConsole();
            };//action结束
            WindowsIdentity wi = WindowsIdentity.GetCurrent();
            bool runAsAdmin = wi != null && new WindowsPrincipal(wi).IsInRole(WindowsBuiltInRole.Administrator);
            if (!runAsAdmin)
            {
                try
                {
                    //不可能以管理员方式直接启动一个 ClickOnce 部署的应用程序，所以尝试以管理员方式启动一个新的进程
                    Process.Start(new ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase) { UseShellExecute = true, Verb = "runas" });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("以管理员方式启动失败，将尝试以普通方式启动！{0}{1}", Environment.NewLine, ex), "出错啦！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    run();//以管理员方式启动失败，则尝试普通方式启动
                }
                Application.Exit();
            }
            else
            {
                run();
            }
        }
        static LoginForm loginf = null;
        static void loginf_OnLoginSuccess(string UserName)
        {
            loginf.Hide();
            #region
            WeixinRobotLib.Linq.dbDataContext db = new WeixinRobotLib.Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
            //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            //db.ObjectTrackingEnabled = false;

            string ActiveCode = WeixinRobotLib.Linq.Util_Services.GetServicesSetting().ActiveCode;

            DateTime? EndDate = null;
            bool Success = NetFramework.Util_MD5.MD5Success(ActiveCode, out EndDate, GlobalParam.UserKey);
            Success = true;
            EndDate = Convert.ToDateTime("2020-01-01");
            if (Success == false)
            {
                MessageBox.Show("激活码异常");
                Environment.Exit(0);
            }
            else
            {
                DateTime Now = db.ExecuteQuery<DateTime>("select getdate()").First();
                if (Now >= EndDate)
                {
                    MessageBox.Show("激活码已过期");
                    UpdateActiveCode uac = new UpdateActiveCode();
                    uac.ShowDialog();
                    MessageBox.Show("激活码已保存，重新启动");
                    Environment.Exit(0);
                }

            }
            #endregion
            StartForm sf = new StartForm();


            if (UserName == "sysadmin")
            {
                sf.SetMode("Admin", "");

            }
            else
            {
                sf.SetMode("User", "");
            }
            if (GlobalParam.DataSourceName != "Admin" && GlobalParam.DataSourceName != "User")
            {
                WeixinRobotLib.Linq.aspnet_UsersNewGameResultSend wsr = WeixinRobotLib.Linq.Util_Services.GetServicesSetting();
                sf.SetMode("EasyRobot", wsr.OpenMode == null ? "" : wsr.OpenMode);
            }



            sf.Show();
        }
    }
}
