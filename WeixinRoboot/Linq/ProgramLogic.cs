using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace WeixinRoboot.Linq
{
    public class DataLogic
    {

        /// <summary>
        /// 修改未兑奖记录以及记录变更
        /// </summary>
        /// <param name="db"></param>
        public static Int32 WX_UserGameLog_Deal(dbDataContext db, StartForm StartF, string ContactID)
        {
            var toupdate = db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                && ((t.Result_HaveProcess == false) || t.Result_HaveProcess == null)
                && (t.WX_UserName == ContactID)
                );
            Int32 Result = 0;
            foreach (WX_UserGameLog gamelogitem in toupdate)
            {
                Game_Result gr = db.Game_Result.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key && t.GameName == gamelogitem.GameName && t.GamePeriod == gamelogitem.GamePeriod.Substring(2));
                if (gr == null)
                {
                    continue;
                }
                gamelogitem.GameResult = gr.GameResult;
                gamelogitem.Gr_BigSmall = gr.BigSmall;
                gamelogitem.Gr_DragonTiger = gr.DragonTiger;
                gamelogitem.Gr_NumTotal = gr.NumTotal;
                gamelogitem.Gr_SingleDouble = gr.SingleDouble;
                switch (gamelogitem.Buy_Type)
                {
                    case "龙虎合":
                        if (gamelogitem.Buy_Value == gamelogitem.Gr_DragonTiger)
                        {
                            gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;
                        }
                        else
                        {
                            gamelogitem.Result_Point = -gamelogitem.Buy_Point;
                        }
                        break;
                    case "大小和":
                        if (gamelogitem.Buy_Value == gamelogitem.Gr_BigSmall)
                        {
                            gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;
                        }
                        else
                        {
                            gamelogitem.Result_Point = -gamelogitem.Buy_Point;
                        }
                        break;
                    case "单双":
                        if (gamelogitem.Buy_Value == gamelogitem.Gr_SingleDouble)
                        {
                            gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;
                        }
                        else
                        {
                            gamelogitem.Result_Point = -gamelogitem.Buy_Point;
                        }
                        break;
                    case "总数":
                        if (gamelogitem.Buy_Value == (gamelogitem.Gr_NumTotal.HasValue ? gamelogitem.Gr_NumTotal.ToString() : ""))
                        {
                            gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;
                        }
                        else
                        {
                            gamelogitem.Result_Point = 0;
                        }
                        break;
                    default:
                        break;
                }//根据买的类型和数值处理

                //只取正整数
                gamelogitem.Result_Point = Convert.ToInt32(Math.Floor(gamelogitem.Result_Point.Value));

                gamelogitem.Result_HaveProcess = true;



                if (gamelogitem.Result_Point != 0)
                {
                    WX_UserChangeLog cl = new WX_UserChangeLog();
                    cl.aspnet_UserID = gamelogitem.aspnet_UserID;
                    cl.WX_UserName = gamelogitem.WX_UserName;
                    cl.RemarkType = "开奖";
                    cl.Remark = "开奖:"
                   + gamelogitem.GamePeriod + " " + gamelogitem.GameResult + " "
                   + gamelogitem.Gr_NumTotal + " "
                   + gamelogitem.Gr_DragonTiger + " "
                   + gamelogitem.Gr_BigSmall + " "
                   + gamelogitem.Gr_SingleDouble + " "
                    + " 下注:" + gamelogitem.Buy_Value + gamelogitem.Buy_Point;
                    cl.ChangeTime = DateTime.Now;
                    cl.ChangePoint = gamelogitem.Result_Point;

                    cl.NeedNotice = true;
                    cl.HaveNotice = false;
                    db.WX_UserChangeLog.InsertOnSubmit(cl);
                }


                db.SubmitChanges();
                Result += 1;

            }//行循环处理
            //增加ChangeLog
            return Result;

        }

        public static string WX_UserGameLog_Cancel(dbDataContext db, WX_UserReplyLog reply, DataTable MemberSource, out Boolean? LogicOK)
        {

            #region "时间转化期数"

            string Minutes = reply.ReceiveTime.ToString("HH:mm");
            string NextPeriod = "";
            string NextLocalPeriod = "";
            var NextMonutes = db.Game_ChongqingshishicaiPeriodMinute.Where(t => string.Compare(t.TimeMinute, Minutes) == 1).OrderBy(t => t.PeriodIndex);
            if (NextMonutes.Count() != 0)
            {
                NextPeriod = NextMonutes.First().PeriodIndex;
                NextLocalPeriod = NextMonutes.First().Private_Period;
            }
            else
            {
                NextPeriod = "120";
                NextLocalPeriod = "097";
            }
            string TotalNextPeriod = reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod;
            #endregion

            string ToCalcel = reply.ReceiveContent;
            ToCalcel = ToCalcel.Substring(2);

            string FirstIndex = ToCalcel.Substring(0, 1);
            string Str_BuyPoint = ToCalcel.Substring(1);

            decimal BuyPoint = 0;
            try
            {
                BuyPoint = Convert.ToDecimal(Str_BuyPoint);
            }
            catch (Exception AnyError)
            {
            }
            if (BuyPoint == 0)
            {
                LogicOK = null;
                return "";

            }
            #region "检查最大可取消"
            var ToModify = db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                 && t.WX_UserName == reply.WX_UserName
                 && t.GameName == "重庆时时彩"
                 && t.GamePeriod == TotalNextPeriod
                 && t.Buy_Value == FirstIndex
                 && t.Buy_Point > 0
                 ).ToList();
            if (ToModify.Sum(t => t.Buy_Point) < BuyPoint)
            {
                List<WX_UserGameLog> rhavebuy = db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                    && t.WX_UserName == reply.WX_UserName
                    && t.GamePeriod == NextPeriod
                    ).ToList();
                LogicOK = false;
                return "超过最大可取消:" + ToModify.Sum(t => t.Buy_Point).ToString() + reply.ReceiveContent + Environment.NewLine;
            }
            #endregion

            foreach (WX_UserGameLog modiitem in ToModify)
            {
                if (BuyPoint == 0)
                {
                    break;
                }
                if (modiitem.Buy_Point > BuyPoint)
                {
                    modiitem.Buy_Point -= BuyPoint;
                    BuyPoint = 0;

                }
                else
                {
                    modiitem.Buy_Point = 0;
                    BuyPoint -= modiitem.Buy_Point.Value;
                }
                #region "赔率重算，如果为0"
                var CheckRatioConfig = db.Game_BasicRatio.SingleOrDefault(t => t.GameType == "重庆时时彩"
              && t.aspnet_UserID == GlobalParam.Key
              && t.BuyType == modiitem.Buy_Type
              && t.BuyValue == modiitem.Buy_Value
              && t.MaxBuy >= modiitem.Buy_Point
              && ((t.MinBuy <= modiitem.Buy_Point && t.IncludeMin == true)
              || (t.MinBuy < modiitem.Buy_Point && t.IncludeMin == false)
              )
              );
                if (CheckRatioConfig == null && modiitem.Buy_Point != 0)
                {
                    LogicOK = false;
                    return "其中一单取消后达不到购买要求";
                }
                else if (modiitem.Buy_Point == 0)
                {
                    modiitem.Result_HaveProcess = true;
                    modiitem.Result_Point = 0;
                    modiitem.GP_LastModify = reply.ReceiveTime;
                }
                else
                {
                    modiitem.Buy_Ratio = CheckRatioConfig.BasicRatio;
                    modiitem.GP_LastModify = reply.ReceiveTime;
                }

                #endregion
            }//循环处理


            List<WX_UserGameLog> havemodify = db.WX_UserGameLog.Where(t => t.Buy_Point != 0
                && t.Result_HaveProcess == false
                && t.aspnet_UserID == GlobalParam.Key
                && t.WX_UserName == reply.WX_UserName
                && t.Buy_Value != FirstIndex
                ).ToList();
            havemodify.AddRange(ToModify.Where(t => t.Buy_Point != 0 && t.Result_HaveProcess != true));

            LogicOK = true;
            return "";











        }
        /// <summary>
        /// 结果文本类
        /// </summary>
        public class TotalResult
        {
            public string UserNickName = "";
            public decimal? Remainder = 0;
            public List<TotalResultRow> Buys = new List<TotalResultRow>();

            public class TotalResultRow
            {
                public TotalResultRow(string P_ShowPeriod, decimal P_TotalTiger,
                 decimal P_TotalDragon,
                 decimal P_TotalOK,
                 decimal P_TotalLarge,
                 decimal P_TotalSmall,
                 decimal P_TotalMiddle,
                 decimal P_TotalSingle,
                 decimal P_TotalDouble,
                    string P_BuyVale,
                decimal? P_BuyPoint,
                     decimal? P_ResultPoint
                    )
                {
                    ShowPeriod = P_ShowPeriod;
                    TotalTiger = P_TotalTiger;
                    TotalDragon = P_TotalDragon;
                    TotalOK = P_TotalOK;
                    TotalLarge = P_TotalLarge;
                    TotalSmall = P_TotalSmall;
                    TotalMiddle = P_TotalMiddle;
                    TotalSingle = P_TotalSingle;
                    TotalDouble = P_TotalDouble;
                    BuyValue = P_BuyVale;
                    BuyPoint = P_BuyPoint;
                    ResultPoint = P_ResultPoint;

                }
                public string ShowPeriod = "";
                public decimal TotalTiger = 0;
                public decimal TotalDragon = 0;
                public decimal TotalOK = 0;
                public decimal TotalLarge = 0;
                public decimal TotalSmall = 0;
                public decimal TotalMiddle = 0;
                public decimal TotalSingle = 0;
                public decimal TotalDouble = 0;

                public string BuyValue = "";
                public decimal? BuyPoint = 0;
                public decimal? ResultPoint = 0;

            }

            public string ToSlimString()
            {

                string Result = "查:" + (Remainder.HasValue ? Remainder.Value.ToString("N0") : "") + Environment.NewLine
                    + "用户：" + UserNickName + Environment.NewLine;
                if (Buys.Count() == 0)
                {
                    return "";
                }
                var Periods = Buys.Select(t => t.ShowPeriod).Distinct();
                foreach (var Perioditem in Periods)
                {
                    Result += "期号: " + Perioditem;
                    foreach (var buyitem in Buys.Where(t => t.ShowPeriod == Perioditem))
                    {
                        Result += (buyitem.TotalTiger == 0 ? "" : ",虎" + buyitem.TotalTiger.ToString("N0"))
                        + (buyitem.TotalDragon == 0 ? "" : ",龙" + buyitem.TotalDragon.ToString("N0"))
                        + (buyitem.TotalOK == 0 ? "" : ",合" + buyitem.TotalOK.ToString("N0"))
                        + (buyitem.TotalLarge == 0 ? "" : ",大" + buyitem.TotalLarge.ToString("N0"))
                        + (buyitem.TotalSmall == 0 ? "" : ",小" + buyitem.TotalSmall.ToString("N0"))
                        + (buyitem.TotalMiddle == 0 ? "" : ",和" + buyitem.TotalMiddle.ToString("N0"))
                        + (buyitem.TotalSingle == 0 ? "" : ",单" + buyitem.TotalSingle.ToString("N0"))
                        + (buyitem.TotalDouble == 0 ? "" : ",双" + buyitem.TotalDouble.ToString("N0"));
                    }
                    Result += Environment.NewLine;
                }

                return Result;



            }

            public string ToSlimStringV2()
            {

                string Result = "";
                var Periods = Buys.Select(t => t.ShowPeriod).Distinct();
                foreach (var Perioditem in Periods)
                {
                    Result +="";
                    foreach (var buyitem in Buys.Where(t => t.ShowPeriod == Perioditem))
                    {
                        Result += (buyitem.TotalTiger == 0 ? "" : ",虎" +ObjectToString (buyitem.TotalTiger,"N0"))
                        + (buyitem.TotalDragon == 0 ? "" : ",龙" +ObjectToString( buyitem.TotalDragon,"N0"))
                        + (buyitem.TotalOK == 0 ? "" : ",合" + ObjectToString(buyitem.TotalOK,"N0"))
                        + (buyitem.TotalLarge == 0 ? "" : ",大" +ObjectToString( buyitem.TotalLarge,"N0"))
                        + (buyitem.TotalSmall == 0 ? "" : ",小" + ObjectToString(buyitem.TotalSmall,"N0"))
                        + (buyitem.TotalMiddle == 0 ? "" : ",和" + ObjectToString(buyitem.TotalMiddle,"N0"))
                        + (buyitem.TotalSingle == 0 ? "" : ",单" +ObjectToString( buyitem.TotalSingle,"N0"))
                        + (buyitem.TotalDouble == 0 ? "" : ",双" + ObjectToString(buyitem.TotalDouble,"N0"));
                    }
                    Result += Environment.NewLine;
                }
                Result=Result.Substring(1);
                Result += "余分" + ObjectToString( Remainder,"N0");
                return Result;



            }

            public override string ToString()
            {
                string Result = "查:" + (Remainder.HasValue ? Remainder.Value.ToString("N0") : "") + Environment.NewLine
                      + "用户：" + UserNickName + Environment.NewLine;
                if (Buys.Count() == 0)
                {
                    return "";
                }


                foreach (var buyitem in Buys)
                {
                    Result += "期号: " + buyitem.ShowPeriod;
                    Result += (buyitem.TotalTiger == 0 ? "" : " 虎" + buyitem.TotalTiger.ToString("N0"))
                 + (buyitem.TotalDragon == 0 ? "" : " 龙" + buyitem.TotalDragon.ToString("N0"))
                 + (buyitem.TotalOK == 0 ? "" : " 合" + buyitem.TotalOK.ToString("N0"))
                 + (buyitem.TotalLarge == 0 ? "" : " 大" + buyitem.TotalLarge.ToString("N0"))
                 + (buyitem.TotalSmall == 0 ? "" : " 小" + buyitem.TotalSmall.ToString("N0"))
                 + (buyitem.TotalMiddle == 0 ? "" : " 和" + buyitem.TotalMiddle.ToString("N0"))
                 + (buyitem.TotalSingle == 0 ? "" : " 单" + buyitem.TotalSingle.ToString("N0"))
                 + (buyitem.TotalDouble == 0 ? "" : " 双" + buyitem.TotalDouble.ToString("N0"));
                }
                Result += Environment.NewLine;


                return Result;
            }

            public string ToOpenString()
            {

                string Result = "查:" + (Remainder.HasValue ? Remainder.Value.ToString("N0") : "") + Environment.NewLine
                    + "用户：" + UserNickName + Environment.NewLine;
                if (Buys.Count() == 0)
                {
                    return "";
                }
                var Periods = Buys.Select(t => t.ShowPeriod).Distinct();
                foreach (var Perioditem in Periods)
                {
                    Result += "期号: " + Perioditem;
                    foreach (var buyitem in Buys.Where(t => t.ShowPeriod == Perioditem))
                    {
                        Result += (buyitem.TotalTiger == 0 ? "" : ",虎" + buyitem.TotalTiger.ToString("N0"))
                        + (buyitem.TotalDragon == 0 ? "" : ",龙" + buyitem.TotalDragon.ToString("N0"))
                        + (buyitem.TotalOK == 0 ? "" : ",合" + buyitem.TotalOK.ToString("N0"))
                        + (buyitem.TotalLarge == 0 ? "" : ",大" + buyitem.TotalLarge.ToString("N0"))
                        + (buyitem.TotalSmall == 0 ? "" : ",小" + buyitem.TotalSmall.ToString("N0"))
                        + (buyitem.TotalMiddle == 0 ? "" : ",和" + buyitem.TotalMiddle.ToString("N0"))
                        + (buyitem.TotalSingle == 0 ? "" : ",单" + buyitem.TotalSingle.ToString("N0"))
                        + (buyitem.TotalDouble == 0 ? "" : ",双" + buyitem.TotalDouble.ToString("N0"));

                        Result += " 下注" + buyitem.BuyValue
                            + (buyitem.BuyPoint.HasValue ? buyitem.BuyPoint.Value.ToString("N0") : "")
                            + "变动" + (buyitem.ResultPoint.HasValue ? buyitem.ResultPoint.Value.ToString("N0") : "");
                    }

                    Result += Environment.NewLine;
                }

                return Result;



            }

        }
        /// <summary>
        /// 生成结果文本类
        /// </summary>
        /// <param name="logs"></param>
        /// <returns></returns>
        public static TotalResult BuildResult(List<WX_UserGameLog> logs, System.Data.DataTable MemberSource)
        {
            TotalResult r = new TotalResult();

            foreach (WX_UserGameLog item in logs)
            {
                DataRow usrw = MemberSource.Select("User_ContactID='" + item.WX_UserName + "'")[0];
                r.UserNickName = usrw.Field<string>("User_Contact");
                decimal? Remainder = GlobalParam.db.WX_UserChangeLog.Where(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == item.WX_UserName).Sum(t => t.ChangePoint);
                r.Remainder = Remainder;
                string SubPeriod = "";
                SubPeriod = item.GamePeriod.Length <= 3 ? item.GamePeriod : item.GamePeriod.Substring(8, 3);

                // string SubPeriod = item.GamePeriod;


                switch (item.Buy_Value)
                {
                    case "龙":
                        r.Buys.Add(new TotalResult.TotalResultRow(SubPeriod, 0, item.Buy_Point.Value, 0, 0, 0, 0, 0, 0, item.Buy_Value, item.Buy_Point, item.Result_Point));
                        break;
                    case "虎":
                        r.Buys.Add(new TotalResult.TotalResultRow(SubPeriod, item.Buy_Point.Value, 0, 0, 0, 0, 0, 0, 0, item.Buy_Value, item.Buy_Point, item.Result_Point));

                        break;
                    case "合":
                        r.Buys.Add(new TotalResult.TotalResultRow(SubPeriod, 0, 0, item.Buy_Point.Value, 0, 0, 0, 0, 0, item.Buy_Value, item.Buy_Point, item.Result_Point));

                        break;
                    case "大":
                        r.Buys.Add(new TotalResult.TotalResultRow(SubPeriod, 0, 0, 0, item.Buy_Point.Value, 0, 0, 0, 0, item.Buy_Value, item.Buy_Point, item.Result_Point));

                        break;
                    case "小":
                        r.Buys.Add(new TotalResult.TotalResultRow(SubPeriod, 0, 0, 0, 0, item.Buy_Point.Value, 0, 0, 0, item.Buy_Value, item.Buy_Point, item.Result_Point));

                        break;
                    case "和":
                        r.Buys.Add(new TotalResult.TotalResultRow(SubPeriod, 0, 0, 0, 0, 0, item.Buy_Point.Value, 0, 0, item.Buy_Value, item.Buy_Point, item.Result_Point));

                        break;
                    case "单":
                        r.Buys.Add(new TotalResult.TotalResultRow(SubPeriod, 0, 0, 0, 0, 0, 0, item.Buy_Point.Value, 0, item.Buy_Value, item.Buy_Point, item.Result_Point));

                        break;
                    case "双":
                        r.Buys.Add(new TotalResult.TotalResultRow(SubPeriod, 0, 0, 0, 0, 0, 0, 0, item.Buy_Point.Value, item.Buy_Value, item.Buy_Point, item.Result_Point));

                        break;
                    default:
                        break;
                }
            }
            return r;
        }


        /// <summary>
        /// 添加消息后转化下单记录或取消记录
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static string WX_UserReplyLog_Create(WX_UserReplyLog reply, dbDataContext db, DataTable MemberSource, out Boolean? LogicOK, out bool ShowOrders)
        {
            if (reply.ReceiveContent != "")
            {
                if (reply.ReceiveContent == "查")
                {
                    LogicOK = true;
                    decimal Remainder = WXUserChangeLog_GetRemainder(reply, db,new List<WX_UserGameLog>());
                    ShowOrders = false;
                    return "查：" + Remainder.ToString("N0");
                }
                if (reply.ReceiveContent == "开奖")
                {
                    string Result = "";
                    var TodatBuyGameLog = db.WX_UserGameLog.Where(t =>
                        t.aspnet_UserID == GlobalParam.Key
                        && t.WX_UserName == reply.WX_UserName
                        && t.Buy_Point != 0
                        && t.TransTime.Date == reply.ReceiveTime.Date
                        );
                    TotalResult tr = BuildResult(TodatBuyGameLog.ToList(), MemberSource);
                    Result = tr.ToOpenString();
                    LogicOK = true;
                    ShowOrders = true;
                    return Result;

                }

                if (reply.ReceiveContent == "未开奖")
                {
                    string Result = "";
                    var TodatBuyGameLog = db.WX_UserGameLog.Where(t =>
                        t.aspnet_UserID == GlobalParam.Key
                        && t.WX_UserName == reply.WX_UserName
                        && t.Buy_Point != 0
                        && (t.Result_HaveProcess == false || t.Result_HaveProcess == null)
                        );
                    TotalResult tr = BuildResult(TodatBuyGameLog.ToList(), MemberSource);
                    Result = tr.ToOpenString();
                    LogicOK = true;
                    ShowOrders = true;
                    return Result;
                }
               
                else if (reply.ReceiveContent.StartsWith("取消"))
                {
                    ShowOrders = true; 
                 if (reply.ReceiveTime.Hour >= 2 && reply.ReceiveTime.Hour < 9)
                {
                    ShowOrders = false;
                    LogicOK = false;
                    return "不在下注时间，开奖无效";
                }
                    return WX_UserGameLog_Cancel(db, reply, MemberSource, out  LogicOK);
                }//取消的单
                else
                {
                    string FirstIndex = reply.ReceiveContent.Substring(0, 1);
                    string Str_BuyPoint = reply.ReceiveContent.Substring(1);
                    decimal BuyPoint = 0;
                    try
                    {
                        BuyPoint = Convert.ToDecimal(Str_BuyPoint);
                    }
                    catch (Exception AnyError)
                    {
                        //List<WX_UserGameLog> rhavebuy = db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == reply.WX_UserName && t.Result_HaveProcess == false).ToList();
                        //TotalResult trbuy = BuildResult(rhavebuy, MemberSource);
                        //return "不能识别:" + reply.ReceiveContent + trbuy.ToString();
                    }
                    if (BuyPoint == 0)
                    {
                        LogicOK = null;
                        ShowOrders = true;
                        return "";
                    }
                    #region
                    if (reply.ReceiveTime.Hour >= 2 && reply.ReceiveTime.Hour < 9)
                    {
                        ShowOrders = false;
                        LogicOK = false;
                        return "不在下注时间，开奖无效";
                    }
                    string CheckResult = "";
                    WX_UserGameLog newr = null;
                    bool NewData = false;
                    List<WX_UserGameLog> checkHaveBuy = db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == reply.WX_UserName && t.Result_HaveProcess == false).ToList();

                    switch (FirstIndex)
                    {
                        case "龙":
                            newr = NewGameLog(reply, "龙虎合", FirstIndex, BuyPoint, out CheckResult, db, out NewData, checkHaveBuy);
                            break;
                        case "虎":
                            newr = NewGameLog(reply, "龙虎合", FirstIndex, BuyPoint, out CheckResult, db, out NewData, checkHaveBuy);
                            break;
                        case "合":
                            newr = NewGameLog(reply, "龙虎合", FirstIndex, BuyPoint, out CheckResult, db, out NewData, checkHaveBuy);
                            break;
                        case "大":
                            newr = NewGameLog(reply, "大小和", FirstIndex, BuyPoint, out CheckResult, db, out NewData, checkHaveBuy);
                            break;
                        case "小":
                            newr = NewGameLog(reply, "大小和", FirstIndex, BuyPoint, out CheckResult, db, out NewData, checkHaveBuy);
                            break;
                        case "和":
                            newr = NewGameLog(reply, "大小和", FirstIndex, BuyPoint, out CheckResult, db, out NewData, checkHaveBuy);
                            break;
                        case "单":
                            newr = NewGameLog(reply, "单双", FirstIndex, BuyPoint, out CheckResult, db, out NewData, checkHaveBuy);
                            break;
                        case "双":
                            newr = NewGameLog(reply, "单双", FirstIndex, BuyPoint, out CheckResult, db, out NewData, checkHaveBuy);
                            break;
                        default:
                            LogicOK = true;
                            ShowOrders = true;
                            return "";
                    }


                    if (newr != null && NewData == true)
                    {
                        newr.Result_Point = 0;
                        Linq.WX_UserChangeLog cl = new WX_UserChangeLog();
                        cl.aspnet_UserID = GlobalParam.Key;
                        cl.WX_UserName = newr.WX_UserName;
                        cl.ChangePoint = -newr.Buy_Point;
                        cl.NeedNotice = false;
                        cl.HaveNotice = false;
                        cl.ChangeTime = newr.TransTime;
                        cl.RemarkType = "下单";
                        cl.Remark = "下单自动扣减";
                        db.WX_UserChangeLog.InsertOnSubmit(cl);
                        db.WX_UserGameLog.InsertOnSubmit(newr);
                        checkHaveBuy.Add(newr);
                    }
                    if (CheckResult != "")
                    {
                        LogicOK = false;
                    }
                    else
                    {
                        LogicOK = true;
                    }
                    ShowOrders = true;
                    return CheckResult;
                }//下单
                    #endregion
            }//有文字消息
            else
            {
                LogicOK = true;
                ShowOrders = true;
                return "";
            }
        }


        public static string WX_UserReplyLog_MySendCreate(string Content, out Boolean? LogicOK, DataRow UserRow, dbDataContext db)
        {
            if (Content == "自动跟踪")
            {
                string Contact = UserRow.Field<string>("User_ContactID");
                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == Contact);
                UserRow.SetField("User_IsReply", true);
                toupdate.IsReply = true;
                LogicOK = true;
                return "";
            }


            if (Content.Length >= 2)
            {
                string Mode = Content.Substring(0, 2);
                string Money = Content.Substring(2);
                decimal ChargeMoney = 0;
                try
                {
                    ChargeMoney = Convert.ToDecimal(Money);
                }
                catch (Exception)
                {
                    LogicOK = null;
                    return "";
                }
                if (ChargeMoney <= 0)
                {
                    LogicOK = false;
                    return "点数不能小于等于0";
                }
                switch (Mode)
                {
                    case "上分":
                        Linq.WX_UserChangeLog change = new Linq.WX_UserChangeLog();
                        change.aspnet_UserID = GlobalParam.Key;
                        change.ChangeTime = DateTime.Now;
                        change.ChangePoint = ChargeMoney;
                        change.Remark = "上分:" + UserRow.Field<string>("User_ContactID");
                        change.RemarkType = "上分";
                        change.WX_UserName = UserRow.Field<string>("User_ContactID");
                        db.WX_UserChangeLog.InsertOnSubmit(change);

                        decimal? TotalPoint = WXUserChangeLog_GetRemainder(null,GlobalParam.db,new List<WX_UserGameLog>());

                        LogicOK = true;
                        return "余分:" + ChargeMoney.ToString("N0");

                        break;
                    case "下分":
                        var data = from dsl in db.WX_UserChangeLog
                                   join dsu in db.WX_UserReply on dsl.WX_UserName equals dsu.WX_UserName
                                   where dsl.aspnet_UserID == GlobalParam.Key
                                   && dsl.WX_UserName == UserRow.Field<string>("User_ContactID")
                                   select new
                                   {
                                       UserName = dsu.WX_UserName
                                       ,
                                       dsl.Remark
                                       ,
                                       dsl.RemarkType
                                       ,
                                       dsl.ChangePoint
                                   };
                        if (data.Sum(t => t.ChangePoint) < ChargeMoney)
                        {
                            LogicOK = false;
                            return "下分点数不能大于查" + data.Sum(t => t.ChangePoint).ToString();

                        }

                        Linq.WX_UserChangeLog cleanup = new Linq.WX_UserChangeLog();
                        cleanup.aspnet_UserID = GlobalParam.Key;
                        cleanup.ChangeTime = DateTime.Now;
                        cleanup.ChangePoint = -ChargeMoney;
                        cleanup.Remark = "下分:" + UserRow.Field<string>("User_ContactID");
                        cleanup.RemarkType = "下分";
                        cleanup.WX_UserName = UserRow.Field<string>("User_ContactID");
                        db.WX_UserChangeLog.InsertOnSubmit(cleanup);
                        db.SubmitChanges();


                        LogicOK = true;
                        return "余分:" + ChargeMoney.ToString("N0");


                        break;
                    default:
                        LogicOK = null;
                        return "";
                        break;
                }
            }//超过2个长度
            else
            {
                LogicOK = null;
                return "";
            }

        }







        /// <summary>
        /// 下单记录数据补充（比率等）和验证
        /// </summary>
        /// <param name="replylog"></param>
        /// <param name="BuyType"></param>
        /// <param name="BuyValue"></param>
        /// <param name="BuyPoint"></param>
        /// <param name="CheckResult"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private static WX_UserGameLog NewGameLog(WX_UserReplyLog replylog, string BuyType, string BuyValue, decimal BuyPoint, out string CheckResult, dbDataContext db, out Boolean NewData, List<WX_UserGameLog> CheckHaveBuy)
        {
            #region "时间转化期数"
            string Minutes = replylog.ReceiveTime.ToString("HH:mm");
            string NextPeriod = "";
            string NextLocalPeriod = "";
            var NextMonutes = db.Game_ChongqingshishicaiPeriodMinute.Where(t => string.Compare(t.TimeMinute, Minutes) == 1).OrderBy(t => t.PeriodIndex);
            if (NextMonutes.Count() != 0)
            {
                NextPeriod = NextMonutes.First().PeriodIndex;
                NextLocalPeriod = NextMonutes.First().Private_Period;
            }
            else
            {
                NextPeriod = "120";
                NextLocalPeriod = "097";
            }
            #endregion

            #region "检查整点"
            Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == replylog.ReceiveTime.ToString("HH:mm"));
            if (testmin != null)
            {
                CheckResult = "整点不能下";
                NewData = false;
                return null;
            }
            #endregion

            WX_UserGameLog CheckExists = CheckHaveBuy.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key
                && t.WX_UserName == replylog.WX_UserName && t.GamePeriod == replylog.ReceiveTime.ToString("yyyyMMdd") + NextPeriod && t.Buy_Value == BuyValue);
            if (CheckExists != null)
            {
                CheckExists.Buy_Point += BuyPoint;

                NewData = false;
                GameLogChangeAndCheck(replylog, CheckExists, out CheckResult, db, CheckHaveBuy);

                WX_UserChangeLog cl = db.WX_UserChangeLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key
                    && t.WX_UserName == CheckExists.WX_UserName
                    && t.ChangeTime == CheckExists.TransTime
                    );
                if (cl != null)
                {
                    cl.ChangePoint = -CheckExists.Buy_Point;
                    cl.Remark += "@#下单修改" + replylog.ReceiveContent;
                }


                return CheckExists;

            }
            else
            {
                NewData = true;
                #region "没有的新加"
                WX_UserGameLog newgl = new WX_UserGameLog();
                newgl.aspnet_UserID = GlobalParam.Key;
                newgl.Buy_Point = BuyPoint;
                newgl.Buy_Type = BuyType;
                newgl.Buy_Value = BuyValue;
                newgl.GameName = "重庆时时彩";
                newgl.TransTime = replylog.ReceiveTime;
                newgl.WX_UserName = replylog.WX_UserName;


                newgl.Result_HaveProcess = false;
                newgl.GamePeriod = replylog.ReceiveTime.ToString("yyyyMMdd") + NextPeriod;

                string LocalDay = (replylog.ReceiveTime.Hour <= 2 ? replylog.ReceiveTime.AddDays(-1).ToString("yyyyMMdd") : replylog.ReceiveTime.ToString("yyyyMMdd"));
                newgl.GameLocalPeriod = LocalDay + NextLocalPeriod;

                GameLogChangeAndCheck(replylog, newgl, out CheckResult, db, CheckHaveBuy);
                if (CheckResult != "")
                {
                    return null;
                }
                else
                {
                    return newgl;
                }
                #endregion
            }



        }

        private static void GameLogChangeAndCheck(WX_UserReplyLog replylog, WX_UserGameLog newgl, out string CheckResult, dbDataContext db, List<WX_UserGameLog> CheckHaveBuy)
        {
            #region "转化赔率"
            var CheckRatioConfig = db.Game_BasicRatio.SingleOrDefault(t => t.GameType == "重庆时时彩"
                && t.aspnet_UserID == GlobalParam.Key
                && t.BuyType == newgl.Buy_Type
                && t.BuyValue == newgl.Buy_Value
                && t.MaxBuy >= newgl.Buy_Point
                && ((t.MinBuy <= newgl.Buy_Point && t.IncludeMin == true)
                || (t.MinBuy < newgl.Buy_Point && t.IncludeMin == false)
                )
                );
            if (CheckRatioConfig == null)
            {
                Decimal? MaxLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.Key
                    && t.BuyValue == newgl.Buy_Value
                    ).Max(t=>t.MaxBuy);
                Decimal? MinLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.Key
                    && t.BuyValue == newgl.Buy_Value
                    ).Min(t => t.MinBuy);

                CheckResult = "下注无效，不在限范围" +ObjectToString(MinLimit,"N0")+"-"+ObjectToString(MaxLimit,"N0")+",余分";
                return;
            }
            else
            {

                newgl.Buy_Ratio = CheckRatioConfig.BasicRatio;
            }
            #endregion

            #region 检查余分
            decimal? Remainder = WXUserChangeLog_GetRemainder(replylog, db,CheckHaveBuy);
            if (Remainder == null || Remainder <= CheckHaveBuy.Sum(t => t.Buy_Point) + newgl.Buy_Point)
            {
                CheckResult = "下注额度" + newgl.Buy_Point.Value.ToString("N0") + "大于查" + Remainder.Value.ToString("N0");
                return;
            }
            #endregion
            CheckResult = "";

        }

        public static decimal WXUserChangeLog_GetRemainder(WX_UserReplyLog replylog, dbDataContext db,List<WX_UserGameLog> HaveBuy)
        {
            decimal? Remainder = db.WX_UserChangeLog.Where(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == replylog.WX_UserName).Sum(t => t.ChangePoint);
            return Remainder.HasValue ? Remainder.Value : 0;
        }

        private static string ObjectToString(object param)
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
        private static string ObjectToString(decimal? param, string Format = "N2")
        {
            if (param == null)
            {
                return "";
            }
            else
            {
                return param.Value.ToString(Format);
            }
        }

        public static string Dragon = Encoding.UTF8.GetString(new byte[] { 240, 159, 144, 178 });
        public static string OK = Encoding.UTF8.GetString(new byte[] { 240, 159, 136, 180 });
        public static string Tiger = Encoding.UTF8.GetString(new byte[] {  238, 129, 144 });



    }
}
