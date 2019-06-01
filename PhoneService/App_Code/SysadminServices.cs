using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data;
using System.Text;
/// <summary>
/// SysadminServices 的摘要说明
/// </summary>
[WebService(Namespace = "http://13828081978.zicp.vip/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class SysadminServices : System.Web.Services.WebService
{

    public SysadminServices()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent();
        if (Membership.GetUser() == null || Membership.GetUser().UserName != "sysadmin")
        {
            throw new Exception("仅允许管理员登录使用");
        }

    }
    [WebMethod]
    public string GetAllUsers()
    {
        dbDataContext db = new dbDataContext("LocalSqlServer");
        var source = (from ms in db.aspnet_Membership
                      join us in db.aspnet_Users on ms.UserId equals us.UserId
                      select new { us.UserId, us.UserName, ms.IsLockedOut }).ToList();
        return JsonConvert.SerializeObject(source);
    }
    [WebMethod]
    public string CreateUser(string UserName, String Password)
    {

        MembershipUser usr = System.Web.Security.Membership.CreateUser(UserName, Password);
        return JsonConvert.SerializeObject(usr);
    }
    [WebMethod]
    public Boolean SetUserLock(string UserName, Boolean IsLockOut)
    {

        MembershipUser usr = System.Web.Security.Membership.GetUser(UserName);
        if (IsLockOut == false)
        {
            return usr.UnlockUser();
        }
        else
        {
            dbDataContext db = new dbDataContext("LocalSqlServer");
            aspnet_Users aspnet_Users = db.aspnet_Users.SingleOrDefault(t => t.UserId == new Guid(usr.ProviderUserKey.ToString()));
            aspnet_Users.aspnet_Membership.IsLockedOut = true;
            db.SubmitChanges();
            return true;
        }
    }

    [WebMethod]
    public string GetUserInfo(string UserName)
    {

        MembershipUser usr = System.Web.Security.Membership.GetUser(UserName);
        return JsonConvert.SerializeObject(usr);
    }
    [WebMethod]
    public  string BuidMD5ActiveCode(DateTime EndDate, Guid MyGuid)
    {
        string ToConvert = MyGuid.ToString() + EndDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
        ToConvert = GetStrMd5X2(ToConvert) + ToConvert;
        byte[] bs = Encoding.UTF8.GetBytes(ToConvert);
        return Convert.ToBase64String(bs);
    }
    public static string GetStrMd5X2(string ConvertString)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)));
        t2 = t2.Replace("-", "");
        return t2;
    }
}
