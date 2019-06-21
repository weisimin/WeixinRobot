using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace NetFramework
{
    #region 用户检验相关
    public class Util_User
    {

        public static bool? ValidateUser(string UserName, string Password)
        {
            if (Membership.GetUser(UserName) == null)
            {
                return null;
            }
            return Membership.ValidateUser(UserName, Password);

        }

        public static bool? ValidateWebUser(string UserName, string Password,ref Guid ProviderUserKey,ref string AspxAuth,ref CookieContainer otscookie)
        {
            WeixinRoboot.RobootWeb.WebService ws = new WeixinRoboot.RobootWeb.WebService();
            ws.CookieContainer= new CookieContainer();
            
           string Result= ws.UserLogIn(UserName, Password);
           if (Result == "用户不存在")
           {
               return null;
           }
           else if (Result=="密码错误次数太多,已停用")
           {
               return false;
           }
           else if (Result == "密码错误")
           {
               return false;
           }
           else
           {
               ProviderUserKey = Guid.Parse(Result);
               AspxAuth = ws.GetUserToken(UserName, Password);
               otscookie = ws.CookieContainer;
               return true;
           }
        }

    }
    #endregion

    #region  邮件相关
    public class Util_Email
    {
        public static void EMail_SendEmail(string Server, Int32 Port, bool EnableSSL, string UserName, string Password, System.Net.Mail.MailAddress FromAddress, List<System.Net.Mail.MailAddress> To, List<System.Net.Mail.MailAddress> CC, string Subject, string BodyHtml, List<System.Net.Mail.Attachment> attachlist)
        {
            //简单邮件传输协议类
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Host = Server;//邮件服务器
            client.Port = Port;//smtp主机上的端口号,默认是25.
            client.EnableSsl = EnableSSL;

            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;//邮件发送方式:通过网络发送到SMTP服务器

            client.UseDefaultCredentials = true;
            client.Credentials = new NetworkCredential(UserName, Password); ;//凭证,发件人登录邮箱的用户名和密码



            //电子邮件信息类
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();//创建一个电子邮件类
            //似乎部分邮件不允许显示人改名
            mailMessage.From = FromAddress;

            foreach (System.Net.Mail.MailAddress item in To)
            {
                mailMessage.To.Add(item);
            }
            foreach (System.Net.Mail.MailAddress item in CC)
            {
                mailMessage.CC.Add(item);
            }


            mailMessage.Subject = Subject;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;//邮件主题编码

            mailMessage.Body = BodyHtml;//可为html格式文本
            mailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("UTF-8");//邮件内容编码
            mailMessage.IsBodyHtml = true;//邮件内容是否为html格式

            mailMessage.Priority = System.Net.Mail.MailPriority.High;//邮件的优先级,有三个值:高(在邮件主题前有一个红色感叹号,表示紧急),低(在邮件主题前有一个蓝色向下箭头,表示缓慢),正常(无显示).
            //附件
            foreach (System.Net.Mail.Attachment att in attachlist)
            {
                mailMessage.Attachments.Add(att);
            }
            //异步传输事件
            client.Timeout = 60000;
            client.SendCompleted += new System.Net.Mail.SendCompletedEventHandler(client_SendCompleted);
            try
            {

                client.SendAsync(mailMessage, mailMessage);//发送邮件
            }
            catch (Exception AnyError)
            {
                MessageBox.Show(AnyError.Message);
            }


        }

        static void client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                MessageBox.Show("发送完成");
            }
        }

        /// <summary>
        /// 验证EMail是否合法
        /// </summary>
        /// <param name="email">要验证的Email</param>
        public static bool IsEmail(string emailStr)
        {
            return Regex.IsMatch(emailStr, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static string SplitGetLast(string EMLFolder)
        {
            string Result = "";
            string[] FullList = EMLFolder.Split("\"\"".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = FullList.Length - 1; i >= 0; i--)
            {
                if ((FullList[i] != ""))
                {
                    Result = FullList[i];
                    break;
                }
            }
            return Result;
        }

    }
    #endregion

    #region 半数据转换
    public class Util_Convert
    {
        public static bool HalfBool(string Value)
        {
            try
            {
                return Convert.ToBoolean(Value);
            }
            catch (Exception)
            {

                return false;
            }
        }
        public static string ToString(object param)
        {
            if (param == null)
            {
                return "";
            }
            else
            {
                return param.ToString();
            }
        }
    }
    #endregion

    #region
    //Quoted-Printable 解码
    public class Util_Quoted
    {
        private const string QpSinglePattern = "(\\=([0-9A-F][0-9A-F]))";

        private const string QpMutiplePattern = @"((\=[0-9A-F][0-9A-F])+=?\s*)+";

        public static string Decode(string contents, Encoding encoding)
        {
            if (contents == null)
            {
                throw new ArgumentNullException("contents");
            }

            // 替换被编码的内容
            string result = Regex.Replace(contents, QpMutiplePattern, new MatchEvaluator(delegate(Match m)
            {
                List<byte> buffer = new List<byte>();
                // 把匹配得到的多行内容逐个匹配得到后转换成byte数组
                MatchCollection matches = Regex.Matches(m.Value, QpSinglePattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
                foreach (Match match in matches)
                {
                    buffer.Add((byte)HexToByte(match.Groups[2].Value.Trim()));
                }
                return encoding.GetString(buffer.ToArray());
            }), RegexOptions.IgnoreCase | RegexOptions.Compiled);

            // 替换多余的链接=号
            result = Regex.Replace(result, @"=\s+", "");

            return result;
        }

        private static int HexToByte(string hex)
        {
            int num1 = 0;
            string text1 = "0123456789ABCDEF";
            for (int num2 = 0; num2 < hex.Length; num2++)
            {
                if (text1.IndexOf(hex[num2]) == -1)
                {
                    return -1;
                }
                num1 = (num1 * 0x10) + text1.IndexOf(hex[num2]);
            }
            return num1;
        }

    }

    #endregion

    public class Util_Sql
    {

        public static object RunSqlText(string ConnectionStringName, string SqlText)
        {
            object Result = new object();
            string dbConnection = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            SqlConnection TempConnection = new SqlConnection(dbConnection);//连接字符串
            try
            {
                SqlDataAdapter ToRun = new SqlDataAdapter();　　//創建SqlDataAdapter 类

                ToRun.SelectCommand = new SqlCommand(SqlText, TempConnection);
                TempConnection.Open();
                ToRun.SelectCommand.CommandType = System.Data.CommandType.Text;
                Result = ToRun.SelectCommand.ExecuteScalar();
            }
            catch (Exception AnyError)
            {
                throw AnyError;
            }
            finally
            {
                TempConnection.Close();
            }


            return Result;
        }
        public static DataTable RunSqlDataTable(string ConnectionStringName, string SqlText)
        {

            DataTable Result = new DataTable();
            string dbConnection = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            SqlConnection TempConnection = new SqlConnection(dbConnection);//连接字符串
            try
            {
                SqlDataAdapter ToRun = new SqlDataAdapter(SqlText, TempConnection);　　//創建SqlDataAdapter 类
                TempConnection.Open();
                ToRun.Fill(Result);

            }
            catch (Exception AnyError)
            {
                throw AnyError;
            }
            finally
            {
                TempConnection.Close();
            }


            return Result;
        }
    }


}
