using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using WeixinRobotLib.Entity.Linq;
namespace WeixinRobootSlim.Linq
{

    public class Util_Services
    {
        public static aspnet_UsersNewGameResultSend GetServicesSetting()
        {

            return GetServicesSetting(WeixinRobootSlim.GlobalParam.UserKey);

        }

        public static aspnet_UsersNewGameResultSend GetServicesSetting(Guid key)
        {

            if (buffers.ContainsKey(key) == false)
            {
                WeixinRoboot.RobootWeb.WebService ws = new WeixinRoboot.RobootWeb.WebService();
                string jaspnet_UsersNewGameResultSend = ws.GetSetting(key.ToString());
                buffers.Add(key, (aspnet_UsersNewGameResultSend)JsonConvert.DeserializeObject(jaspnet_UsersNewGameResultSend, typeof(aspnet_UsersNewGameResultSend)));
                aspnet_UsersNewGameResultSend outs = null;
                buffers.TryGetValue(key, out outs);
                return outs;
            }
            else
            {
                aspnet_UsersNewGameResultSend outs = null;
                buffers.TryGetValue(key, out outs);
                return outs;
            }


        }
        private static Dictionary<Guid, aspnet_UsersNewGameResultSend> buffers = new Dictionary<Guid, aspnet_UsersNewGameResultSend>();
        public static string SaveServicesSetting(aspnet_UsersNewGameResultSend tosaves)
        {
            if (buffers.ContainsKey(tosaves.aspnet_UserID))
            {
                buffers[tosaves.aspnet_UserID] = tosaves;
            }

            WeixinRoboot.RobootWeb.WebService ws = new WeixinRoboot.RobootWeb.WebService();
            string Jresult = ws.SaveSetting(WeixinRobootSlim.GlobalParam.UserName, WeixinRobootSlim.GlobalParam.Password, JsonConvert.SerializeObject(tosaves));
            return Jresult;
        }

        public static WX_WebSendPICSetting GetWebSendPicSetting(Guid UserKey, string Row_WX_SourceType, string Row_WX_UserName)
        {
            if (WX_WebSendPICSettingBuf == null)
            {
                WX_WebSendPICSettingBuf = new DataTable();
                DataColumn key1 = WX_WebSendPICSettingBuf.Columns.Add("UserKey", typeof(Guid));
                DataColumn key2 = WX_WebSendPICSettingBuf.Columns.Add("Row_WX_SourceType", typeof(string));
                DataColumn key3 = WX_WebSendPICSettingBuf.Columns.Add("Row_WX_UserName", typeof(string));
                WX_WebSendPICSettingBuf.Columns.Add("DataObject", typeof(WX_WebSendPICSetting));
                WX_WebSendPICSettingBuf.PrimaryKey = new DataColumn[] { key1, key2, key3 };
                WeixinRoboot.RobootWeb.WebService ws = new WeixinRoboot.RobootWeb.WebService();
            }
            var rows = WX_WebSendPICSettingBuf.AsEnumerable().Where(t => t.Field<Guid>("UserKey") == UserKey
            && t.Field<string>("Row_WX_SourceType") == Row_WX_SourceType
            && t.Field<string>("Row_WX_UserName") == Row_WX_UserName
            ).ToArray();
            if (rows.Count() == 0)
            {
                WeixinRoboot.RobootWeb.WebService ws = new WeixinRoboot.RobootWeb.WebService();
                WX_WebSendPICSetting toload = JsonConvert.DeserializeObject < WX_WebSendPICSetting >( ws.GetWebSendPicSetting(UserKey, Row_WX_SourceType, Row_WX_UserName));
                DataRow dats = WX_WebSendPICSettingBuf.NewRow();
                dats.SetField<Guid>("UserKey", GlobalParam.UserKey);
                dats.SetField<string>("Row_WX_SourceType", toload.WX_UserName);
                dats.SetField<string>("Row_WX_UserName", toload.WX_UserName);
                dats.SetField<WX_WebSendPICSetting>("DataObject", toload);
                return toload;
            }

            else
            {
                return rows.First().Field<WX_WebSendPICSetting>("DataObject");
            }


        }

        public static DataTable WX_WebSendPICSettingBuf = null;

        public static string SaveWebSendPicSetting(WX_WebSendPICSetting tosave)
        {
            WeixinRoboot.RobootWeb.WebService ws = new WeixinRoboot.RobootWeb.WebService();
            string Result = ws.SaveWebSendPicSetting(GlobalParam.GetUserParam(), JsonConvert.SerializeObject(tosave));
            var rows = WX_WebSendPICSettingBuf.AsEnumerable().Where(t => t.Field<Guid>("UserKey") == tosave.aspnet_UserID
           && t.Field<string>("Row_WX_SourceType") == tosave.WX_SourceType
           && t.Field<string>("Row_WX_UserName") == tosave.WX_UserName
           ).ToArray();

            if (rows.Count() == 0)
            {
                DataRow dats = WX_WebSendPICSettingBuf.NewRow();
                dats.SetField<Guid>("UserKey", GlobalParam.UserKey);
                dats.SetField<string>("Row_WX_SourceType", tosave.WX_UserName);
                dats.SetField<string>("Row_WX_UserName", tosave.WX_UserName);
                dats.SetField<WX_WebSendPICSetting>("DataObject", tosave);
            }
            else
            {
                rows[0].SetField<WX_WebSendPICSetting>("DataObject", tosave);
            }
            return Result;
        }// fun end

    }

}
