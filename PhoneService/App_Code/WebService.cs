using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Xml;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
/// <summary>
/// WebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://13828081978.zicp.vip/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
//[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
        Context.Response.ContentType = "text/html;cahrset=UTF-8";//这句话有二层作用
    }


    [WebMethod]
    public void UserLogIn(string UserName, string Password)
    {

        bool r = Membership.ValidateUser(UserName, Password);
        MembershipUser msr = Membership.GetUser(UserName);
        if (r == true)
        {
            Context.Response.Write(Membership.GetUser().ProviderUserKey.ToString());
        }
        else if (msr == null)
        {
            Context.Response.Write("用户不存在");
        }
        else if (msr.IsLockedOut)
        {
            Context.Response.Write("密码错误次数太多,或已停用");
        }
        else
        {
            Context.Response.Write("密码错误");
        }

        Context.Response.End();
    }

    [WebMethod]
    public void GetSetting(string saspnetUserid)
    {
        Guid aspnetUserid = Guid.Parse(saspnetUserid);
        dbDataContext db = new dbDataContext();
        aspnet_UsersNewGameResultSend sets = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == aspnetUserid);
        Context.Response.Write(JsonConvert.SerializeObject(sets));
        Context.Response.End();

    }
    [WebMethod]
    public void SaveSetting(string UserName, string Password, string jaspnet_UsersNewGameResultSend)
    {
        MembershipUser msr = Membership.GetUser(UserName);
        dbDataContext db = new dbDataContext();
        aspnet_UsersNewGameResultSend tins_sets = (aspnet_UsersNewGameResultSend)JsonConvert.DeserializeObject(jaspnet_UsersNewGameResultSend);

        aspnet_UsersNewGameResultSend save_sets = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == (Guid)msr.ProviderUserKey);

        save_sets.ActiveCode = tins_sets.ActiveCode;

        save_sets.IsNewSend = tins_sets.IsNewSend;

        save_sets.ActiveCode = tins_sets.ActiveCode;

        save_sets.IsBlock = tins_sets.IsBlock;

        save_sets.IsSendPIC = tins_sets.IsSendPIC;

        save_sets.IsReceiveOrder = tins_sets.IsReceiveOrder;

        save_sets.MaxPlayerCount = tins_sets.MaxPlayerCount;

        save_sets.bossaspnet_UserID = tins_sets.bossaspnet_UserID;

        save_sets.SendImageEnd = tins_sets.SendImageEnd;

        save_sets.SendImageStart = tins_sets.SendImageStart;

        save_sets.SendImageEnd2 = tins_sets.SendImageEnd2;

        save_sets.SendImageEnd3 = tins_sets.SendImageEnd3;

        save_sets.SendImageEnd4 = tins_sets.SendImageEnd4;

        save_sets.SendImageStart2 = tins_sets.SendImageStart2;

        save_sets.SendImageStart3 = tins_sets.SendImageStart3;

        save_sets.SendImageStart4 = tins_sets.SendImageStart4;

        save_sets.ImageEndText = tins_sets.ImageEndText;

        save_sets.ImageTopText = tins_sets.ImageTopText;

        save_sets.BlockEndHour = tins_sets.BlockEndHour;

        save_sets.BlockEndMinute = tins_sets.BlockEndMinute;

        save_sets.BlockStartHour = tins_sets.BlockStartHour;

        save_sets.BlockStartMinute = tins_sets.BlockStartMinute;

        save_sets.LeiDianPath = tins_sets.LeiDianPath;

        save_sets.NoxPath = tins_sets.NoxPath;

        save_sets.LeiDianSharePath = tins_sets.LeiDianSharePath;

        save_sets.NoxSharePath = tins_sets.NoxSharePath;

        save_sets.AdbLeidianMode = tins_sets.AdbLeidianMode;

        save_sets.AdbNoxMode = tins_sets.AdbNoxMode;

        save_sets.TwoTreeNotSingle = tins_sets.TwoTreeNotSingle;

        save_sets.XinJiangMode = tins_sets.XinJiangMode;

        save_sets.TengXunShiFenMode = tins_sets.TengXunShiFenMode;

        save_sets.FuliRatio = tins_sets.FuliRatio;

        save_sets.LiuShuiRatio = tins_sets.LiuShuiRatio;

        save_sets.Thread_AoZhouCai = tins_sets.Thread_AoZhouCai;

        save_sets.Thread_ChongQingShiShiCai = tins_sets.Thread_ChongQingShiShiCai;

        save_sets.Thread_TengXunShiFen = tins_sets.Thread_TengXunShiFen;

        save_sets.Thread_TengXunWuFen = tins_sets.Thread_TengXunWuFen;

        save_sets.Thread_VRChongqing = tins_sets.Thread_VRChongqing;

        save_sets.Thread_WuFen = tins_sets.Thread_WuFen;

        save_sets.Thread_XinJiangShiShiCai = tins_sets.Thread_XinJiangShiShiCai;

        save_sets.Thread_TengXunShiFenXin = tins_sets.Thread_TengXunShiFenXin;

        save_sets.Thread_TengXunWuFenXin = tins_sets.Thread_TengXunWuFenXin;

        save_sets.Thread_HeNeiWuFen = tins_sets.Thread_HeNeiWuFen;
        try
        {
            db.SubmitChanges();
            Context.Response.Write("保存成功");
            Context.Response.End();
        }
        catch (Exception anyerror)
        {

            Context.Response.Write("保存失败"+anyerror.Message);
            Context.Response.End();
        }


    }

}
