using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net;
namespace WeixinRobotLib.Entity.Linq
{
   public partial class ProgramLogic
    {
       public enum ShiShiCaiMode { 重庆时时彩, 五分彩, 香港时时彩, 澳洲幸运5, 腾讯十分, 腾讯五分, 北京赛车PK10, VR重庆时时彩, 新疆时时彩, 未知, 腾五信, 腾十信, 全彩, 河内五分 }

       public enum FormatResultState { Initialize, Fail, Multi, SingleSuccess }
       public enum FormatResultType { Initialize, QueryTxt, QueryImage, QueryResult, OrderModify, CancelOrderModify }

       public enum FormatResultDirection { MemoryMatchList, DataBaseGameLog }

       public enum ShiShiCaiPicKeepType { Keep, Once, Stop, UnKnown, RestoreDefault, SetTime }

       public enum GameMode { 时时彩, 球赛, 六合彩, 非玩法 }

       public static string Dragon = Encoding.UTF8.GetString(new byte[] { 240, 159, 144, 178 });
       public static string OK = Encoding.UTF8.GetString(new byte[] { 240, 159, 136, 180 });
       public static string Tiger = Encoding.UTF8.GetString(new byte[] { 238, 129, 144 });

       public static string Tiger_dingding = Encoding.UTF8.GetString(new byte[] { 240, 159, 144, 175 });



       public static string Dragon_yixin = Encoding.UTF8.GetString(new byte[] { 0xF0, 0x9f, 0x90, 0xB2 });
       public static string OK_yixin = Encoding.UTF8.GetString(new byte[] { 0xF0, 0x9f, 0X88, 0xB4 });
       public static string Tiger_yixin = Encoding.UTF8.GetString(new byte[] { 0xF0, 0x9f, 0x90, 0xaf });

       public static bool TimeInDuring(Int32? StartHour, Int32? StartMinute, Int32? EndHour, Int32? EndMinute)
       {
           if (StartHour.HasValue == false || EndHour.HasValue == false || StartMinute.HasValue == false || EndMinute.HasValue == false)
           {
               return false;
           }
           bool CrossDay = false;
           if (StartHour * 60 + StartMinute > EndHour * 60 + EndMinute)
           {
               CrossDay = true;
           }
           Int32 MinMinute = 0;
           Int32 MaxMinute = 0;
           Int32 NowMinute = DateTime.Now.Hour * 60 + DateTime.Now.Minute;
           if (CrossDay)
           {
               MinMinute = EndHour.Value * 60 + EndMinute.Value;
               MaxMinute = StartHour.Value * 60 + StartMinute.Value;
               if (NowMinute <= MinMinute || NowMinute >= MaxMinute)
               {
                   return true;
               }
               else
               {
                   return false;
               }


           }
           else
           {
               MaxMinute = EndHour.Value * 60 + EndMinute.Value;
               MinMinute = StartHour.Value * 60 + StartMinute.Value;
               if (NowMinute <= MaxMinute && NowMinute >= MinMinute)
               {
                   return true;
               }
               else
               {
                   return false;
               }


           }

       }

       public static WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode GetMode(WX_UserReply dr)
       {
           WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.未知;
           if (dr.ChongqingMode == true
               )
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩;
           }
           else if (dr.FiveMinuteMode== true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.五分彩;
           }
           else if (dr.HkMode == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.香港时时彩;
           }
           else if (dr.AozcMode == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5;
           }
           else if (dr.TengXunWuFenMode == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.腾讯五分;
           }
           else if (dr.TengXunShiFenMode == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.腾讯十分;
           }
           else if (dr.VRMode == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩;
           }
           else if (dr.XinJiangMode == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.新疆时时彩;
           }
           else if (dr.TengXunShiFenXinMode == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.腾十信;
           }
           else if (dr.TengXunWuFenXinMode == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.腾五信;
           }
           else if (dr.HeNeiWuFenMode == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.河内五分;
           }
           return subm;
       }
       public static WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode GetMode(DataRow dr)
       {
           WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.未知;
           if (dr.Field<Boolean>("User_ChongqingMode") == true
               )
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩;
           }
           else if (dr.Field<Boolean>("User_FiveMinuteMode") == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.五分彩;
           }
           else if (dr.Field<Boolean>("User_HkMode") == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.香港时时彩;
           }
           else if (dr.Field<Boolean>("User_AozcMode") == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5;
           }
           else if (dr.Field<Boolean>("User_TengXunWuFen") == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.腾讯五分;
           }
           else if (dr.Field<Boolean>("User_TengXunShiFen") == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.腾讯十分;
           }
           else if (dr.Field<Boolean>("User_VR") == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩;
           }
           else if (dr.Field<Boolean>("User_XinJiangShiShiCai") == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.新疆时时彩;
           }
           else if (dr.Field<Boolean>("User_TengXunShiFenXin") == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.腾十信;
           }
           else if (dr.Field<Boolean>("User_TengXunWuFenXin") == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.腾五信;
           }
           else if (dr.Field<Boolean>("User_HeNeiWuFen") == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.河内五分;
           }
           return subm;
       }

       public static WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode GetMode(DataRow[] dr)
       {
           return dr.Length == 0 ? WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.未知 : GetMode(dr[0]);
       }
       public static WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode GetMode(WeixinRobotLib.Entity.Linq.WX_PCSendPicSetting dr)
       {
           WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.未知;
           if (dr.ChongqingMode == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.重庆时时彩;
           }
           else if (dr.FiveMinuteMode == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.五分彩;
           }
           else if (dr.HkMode == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.香港时时彩;
           }
           else if (dr.AozcMode == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.澳洲幸运5;
           }
           else if (dr.Tengxunshifen == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.腾讯十分;
           }
           else if (dr.Tengxunwufen == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.腾讯五分;
           }
           else if (dr.TengxunshifenXin == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.腾十信;
           }
           else if (dr.TengxunwufenXin == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.腾五信;
           }
           else if (dr.HeNeiWuFenMode == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.腾五信;
           }
           else if (dr.HeNeiWuFenMode == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.腾五信;
           }
           else if (dr.VRChongqing == true)
           {
               subm = WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiMode.VR重庆时时彩;
           }
           return subm;
       }

       public class UserParam
       {
           public string UserName;
           public string Password;
           public string ASPXAUTH;
           public bool LogInSuccess = false;
           public CookieContainer LoginCookie = new CookieContainer();

           public Guid UserKey = Guid.Empty;
           public Guid JobID = Guid.Empty;


           public string DataSourceName = "";

           public static string MemberSourceode { get; set; }

           public Linq.aspnet_UsersNewGameResultSend Membersetting {get;set;}

       }

       public static string NumberToAnmial(Int32 spec)
       {
           if (spec == 11 || spec == 23 || spec == 35 || spec == 47)
           {
               return "牛";
           }
           if (spec == 10 || spec == 22 || spec == 34 || spec == 46)
           {
               return "虎";
           }
           if (spec == 9 || spec == 21 || spec == 33 || spec == 45)
           {
               return "兔";
           }
           if (spec == 8 || spec == 20 || spec == 32 || spec == 44)
           {
               return "龙";
           }
           if (spec == 7 || spec == 19 || spec == 31 || spec == 43)
           {
               return "蛇";
           }
           if (spec == 6 || spec == 18 || spec == 30 || spec == 42)
           {
               return "马";
           }
           if (spec == 5 || spec == 17 || spec == 29 || spec == 41)
           {
               return "羊";
           }
           if (spec == 4 || spec == 16 || spec == 28 || spec == 40)
           {
               return "猴";
           }
           if (spec == 3 || spec == 15 || spec == 27 || spec == 39)
           {
               return "鸡";
           }
           if (spec == 2 || spec == 14 || spec == 26 || spec == 38)
           {
               return "狗";
           }
           if (spec == 1 || spec == 13 || spec == 25 || spec == 37 || spec == 49)
           {
               return "猪";
           }
           if (spec == 12 || spec == 24 || spec == 36 || spec == 48)
           {
               return "鼠";
           }
           return "？";
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="Paramter"></param>
       /// <param name="GameType">重庆，新疆，五分，VR,滕五，腾十，澳彩</param>
       /// <param name="PicType">文本，龙虎，独龙虎，牛牛，图1（jpg）</param>
       public static WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiPicKeepType ShiShiCaiPicTypeCaculate(string NewParamter, ref string GameType, ref  string PicType, ref string SettingUserName)
       {
           string Paramter = "";
           if (NewParamter.Contains("群"))
           {
               SettingUserName = NewParamter.Substring(0, NewParamter.LastIndexOf("群"));
               Paramter = NewParamter.Substring(NewParamter.LastIndexOf("群") + 1);
           }
           else
           {
               SettingUserName = "";
               Paramter = NewParamter;
           }
           GameType = "";
           PicType = "";


           string first2 = (Paramter.Length > 2 ? Paramter.Substring(0, 2) : Paramter);
           string first3 = (Paramter.Length > 3 ? Paramter.Substring(0, 3) : Paramter);
           first2 = first2.ToUpper();
           first3 = first3.ToUpper();
           switch (first2)
           {
               case "重庆":
                   GameType = first2;
                   break;
               case "新疆":
                   GameType = first2;
                   break;
               case "五分":
                   GameType = first2;
                   break;
               case "VR":
                   GameType = first2;
                   break;
               case "腾五":
                   if (first3 == "腾五信")
                   {
                       GameType = "腾五信";
                   }
                   else
                   {
                       GameType = first2;
                   }
                   break;
               case "腾十":
                   if (first3 == "腾十信")
                   {
                       GameType = "腾十信";
                   }
                   else
                   {
                       GameType = first2;
                   }

                   break;
               case "澳彩":
                   GameType = first2;
                   break;
               case "河五":
                   GameType = first2;
                   break;
               default:
                   GameType = "";
                   break;
           }
           string End2 = Paramter.Length > (GameType.Length + 2) ? Paramter.Substring((GameType.Length), 2) : Paramter.Substring(GameType.Length);
           Int32 StartPrefix = 0;
           switch (End2)
           {
               case "":
                   PicType = "";
                   StartPrefix = GameType.Length;
                   break;
               case "文本":
                   PicType = "文本";
                   StartPrefix = GameType.Length + 2;
                   break;
               case "龙虎":
                   PicType = "龙虎";
                   StartPrefix = GameType.Length + 2;
                   break;
               case "独龙":
                   PicType = "独龙虎";
                   StartPrefix = GameType.Length + 3;
                   break;
               case "牛牛":
                   PicType = "牛牛";
                   StartPrefix = GameType.Length + 2;
                   break;
               case "图1":
                   PicType = "图1";
                   StartPrefix = GameType.Length + 2;
                   break;
               default:
                   PicType = "";
                   StartPrefix = GameType.Length;
                   break;
           }

           string Operation = Paramter.Substring(StartPrefix);
           if (Operation == ("发图"))
           {
               return WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiPicKeepType.Keep;
           }
           else if (Operation == ("1"))
           {
               return WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiPicKeepType.Keep;
           }
           else if (Operation == ("停图"))
           {
               return WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiPicKeepType.Stop;
           }
           else if (Operation == ("3"))
           {
               return WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiPicKeepType.Stop;
           }
           else if (Operation == ("补图"))
           {
               return WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiPicKeepType.Once;
           }
           else if (Operation == ("2"))
           {
               return WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiPicKeepType.Once;
           }
           else if (Operation == ("停止"))
           {
               return WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiPicKeepType.RestoreDefault;
           }
           else if (Operation == ("4"))
           {
               return WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiPicKeepType.RestoreDefault;
           }
           else if (Operation == (""))
           {
               return WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiPicKeepType.Once;
           }
           else if (Operation == ("图"))
           {
               return WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiPicKeepType.Once;
           }
           if (Operation.StartsWith("发图"))
           {
               return WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiPicKeepType.SetTime;
           }
           if (Operation.StartsWith("发图"))
           {
               return WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiPicKeepType.SetTime;
           }
           else
           {
               return WeixinRobotLib.Entity.Linq.ProgramLogic.ShiShiCaiPicKeepType.UnKnown;
           }

       }

       public static bool ShiShiCaiIsOrderContent(string Content)
       {

           string NewContent = Content.Replace("上", "");
           NewContent = NewContent.Replace("下", "");

           NewContent = NewContent.Replace("大", "");
           NewContent = NewContent.Replace("小", "");
           NewContent = NewContent.Replace("和", "");

           NewContent = NewContent.Replace("单", "");
           NewContent = NewContent.Replace("双", "");

           NewContent = NewContent.Replace("龙", "");
           NewContent = NewContent.Replace("虎", "");
           NewContent = NewContent.Replace("合", "");

           NewContent = NewContent.Replace("全", "");

           NewContent = NewContent.Replace("个", "");
           NewContent = NewContent.Replace("十", "");
           NewContent = NewContent.Replace("百", "");
           NewContent = NewContent.Replace("千", "");
           NewContent = NewContent.Replace("万", "");

           NewContent = NewContent.Replace("0", "");
           NewContent = NewContent.Replace("1", "");
           NewContent = NewContent.Replace("2", "");
           NewContent = NewContent.Replace("3", "");
           NewContent = NewContent.Replace("4", "");
           NewContent = NewContent.Replace("5", "");

           NewContent = NewContent.Replace("6", "");
           NewContent = NewContent.Replace("7", "");
           NewContent = NewContent.Replace("8", "");
           NewContent = NewContent.Replace("9", "");


           NewContent = NewContent.Replace("零", "");
           NewContent = NewContent.Replace("一", "");
           NewContent = NewContent.Replace("二", "");
           NewContent = NewContent.Replace("三", "");
           NewContent = NewContent.Replace("四", "");
           NewContent = NewContent.Replace("五", "");

           NewContent = NewContent.Replace("六", "");
           NewContent = NewContent.Replace("七", "");
           NewContent = NewContent.Replace("八", "");
           NewContent = NewContent.Replace("九", "");


           NewContent = NewContent.Replace("，", "");
           NewContent = NewContent.Replace(",", "");
           NewContent = NewContent.Replace("，,", "");


           NewContent = NewContent.Replace("。", "");
           NewContent = NewContent.Replace(".", "");
           NewContent = NewContent.Replace("。", "");


           NewContent = NewContent.Replace(" ", "");

           NewContent = NewContent.Replace("查", "");

           NewContent = NewContent.Replace("流水", "");

           NewContent = NewContent.Replace("开", "");

           NewContent = NewContent.Replace("加", "");

           NewContent = NewContent.Replace("/", "");

           NewContent = NewContent.Replace("\\", "");

           NewContent = NewContent.Replace("-", "");
           if (NewContent == "")
           {
               return true;
           }
           else
           {
               return false;
           }




       }//fun end
       public static string CaculateNiuNiu(string str_win2)
       {
           //string[] numbuerstocheck =numbers.to
           char[] numbs = str_win2.ToCharArray();
           int NUM1 = Convert.ToInt32(numbs[0].ToString());
           int NUM2 = Convert.ToInt32(numbs[1].ToString());
           int NUM3 = Convert.ToInt32(numbs[2].ToString());
           int NUM4 = Convert.ToInt32(numbs[3].ToString());
           int NUM5 = Convert.ToInt32(numbs[4].ToString());


           for (int i = 0; i < numbs.Length; i++)
           {
               for (int j = 0; j < numbs.Length; j++)
               {
                   for (int k = 0; k < numbs.Length; k++)
                   {
                       if (i != j & i != k & j != k)
                       {
                           if (
                               (Convert.ToInt32(numbs[i].ToString())
                               + Convert.ToInt32(numbs[j].ToString())
                                 + Convert.ToInt32(numbs[k].ToString())) % 10 == 0
                                     )
                           {
                               Int32 Reminder = (NUM1 + NUM2 + NUM3 + NUM4 + NUM5) % 10;
                               switch (Reminder)
                               {
                                   case 0:
                                       return "牛牛 大双";

                                   case 1:
                                       return "牛一 小单";

                                   case 2:
                                       return "牛二 小双";

                                   case 3:
                                       return "牛三 小单";

                                   case 4:
                                       return "牛四 小双";

                                   case 5:
                                       return "牛五 小单";

                                   case 6:
                                       return "牛六 大双";

                                   case 7:
                                       return "牛七 大单";

                                   case 8:
                                       return "牛八 大双";

                                   case 9:
                                       return "牛九 大单";

                                   default:
                                       break;
                               }//根据结果出牛

                           }
                       }//有牛的
                   }//任意取数3
               }//任意取数2
           }//任意取数1
           return "无牛";
       }//fun end

       public static string BallBuyTypeToChinseFrontShow(string BuyType)
       {
           switch (BuyType)
           {
               case "A_WIN":
                   return "主";

               case "Winless":
                   return "让球";

               case "B_WIN":
                   return "客";

               case "BIGWIN":
                   return "大球";

               case "Total":
                   return "总球";

               case "SMALLWIN":
                   return "小球";

               case "R_A_A":
                   return "主/主";

               case "R_A_SAME":
                   return "主/和";

               case "R_A_B":
                   return "主/客";

               case "R_SAME_A":
                   return "和/主";

               case "R_SAME_SAME":
                   return "和/和";

               case "R_SAME_B":
                   return "和/客";

               case "R_B_A":
                   return "客/主";

               case "R_B_SAME":
                   return "客/和";

               case "R_B_B":
                   return "客/客";

               case "R1_0_A":
                   return "1-0";

               case "R1_0_B":
                   return "0-1";

               case "R2_0_A":
                   return "2-0";

               case "R2_0_B":
                   return "0-2";

               case "R2_1_A":
                   return "2-1";

               case "R2_1_B":
                   return "1-2";

               case "R3_0_A":
                   return "3-0";

               case "R3_0_B":
                   return "0-3";

               case "R3_1_A":
                   return "3-1";

               case "R3_1_B":
                   return "1-3";

               case "R3_2_A":
                   return "3-2";

               case "R3_2_B":
                   return "2-3";

               case "R4_0_A":
                   return "4-0";

               case "R4_0_B":
                   return "0-4";

               case "R4_1_A":
                   return "4-1";

               case "R4_1_B":
                   return "1-4";

               case "R4_2_A":
                   return "4-2";

               case "R4_2_B":
                   return "2-4";

               case "R4_3_A":
                   return "4-3";

               case "R4_3_B":
                   return "3-4";


               case "R0_0":
                   return "0-0";


               case "R1_1":
                   return "1-1";


               case "R2_2":
                   return "2-2";


               case "R3_3":
                   return "3-3";


               case "R4_4":
                   return "4-4";


               case "ROTHER":
                   return "其他";





               default:
                   return "";
           }
       }



    }
}
