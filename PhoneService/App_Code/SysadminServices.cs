using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data;
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
    public MembershipUser CreateUser(string UserName, String Password)
    {

        MembershipUser usr = System.Web.Security.Membership.CreateUser(UserName, Password);
        return usr;
    }



}
