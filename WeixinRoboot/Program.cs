using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
namespace WeixinRoboot
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

              



           // AllocConsole();
            string ConfigFile = Application.StartupPath + "\\WeixinRoboot.exe.config";
            string TempFileName = Application.StartupPath + "\\web.config";
            if (System.IO.File.Exists(TempFileName))
            {
                System.IO.File.Delete(TempFileName);
            }
            System.IO.File.Move(ConfigFile, TempFileName);
            System.Diagnostics.Process proc = System.Diagnostics.Process.Start("C:\\Windows\\Microsoft.NET\\Framework\\v2.0.50727\\aspnet_regiis.exe", "-pef \"connectionStrings\" \"" + Application.StartupPath + "\"");


            if (proc != null)
            {
                proc.WaitForExit();
                System.IO.File.Move(TempFileName, ConfigFile);
            }
            try
            {



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

                //Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
                //db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
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
                NetFramework.Console.WriteLine("删除临时图片失败");
                NetFramework.Console.WriteLine(AnyError.Message);
                NetFramework.Console.WriteLine(AnyError.StackTrace);
            }

            //局部线程，不能及时结束会造成没相应
            //全局LINQ数据库，会频繁出现SQLDATAREADER已打开或关闭的问题

            Linq.ProgramLogic.ComboStringInit();

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
            //    MessageBox.Show(AnyError.Message);
            //    if (System.IO.File.Exists(Application.StartupPath + "\\log.txt"))
            //    {
            //        System.IO.File.Delete(Application.StartupPath + "\\log.txt");
            //    }
            //    System.IO.FileStream fs = new System.IO.FileStream(Application.StartupPath + "\\log.txt", System.IO.FileMode.OpenOrCreate);
            //    System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
            //    sw.Write(AnyError.Message + Environment.NewLine + AnyError.StackTrace);
            //    sw.Flush();
            //    sw.Close();
            //    //Environment.Exit(0);

            //}
            CefSharp.Cef.Shutdown();
            //FreeConsole();

        }
        static LoginForm loginf = null;
        static void loginf_OnLoginSuccess(string UserName)
        {
            loginf.Hide();
            #region
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");


            string ActiveCode = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey).ActiveCode;

            DateTime? EndDate = null;
            bool Success = NetFramework.Util_MD5.MD5Success(ActiveCode, out EndDate, (Guid)System.Web.Security.Membership.GetUser(UserName).ProviderUserKey);
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


            if (System.Web.Security.Roles.IsUserInRole(UserName, "Admin"))
            {
                sf.SetMode("Admin");

            }
            else
            {
                sf.SetMode("User");
            }
            sf.Show();
        }
    }
}
