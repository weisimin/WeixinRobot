using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Web.Security;
namespace WeixinRoboot.Linq
{
    /// <summary>
    /// LINQ SUBMIT CHANGERS 不稳定 停用
    /// </summary>
    public class ProgramLogic
    {

        /// <summary>
        /// 修改未兑奖记录以及记录变更
        /// </summary>
        /// <param name="db"></param>
        public static Int32 WX_UserGameLog_Deal(StartForm StartF, string ContactID, string SourceType)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            var toupdate = db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                && ((t.Result_HaveProcess == false) || t.Result_HaveProcess == null)
                && (t.WX_UserName == ContactID)
                && (t.WX_SourceType == SourceType)
                );
            Int32 Result = 0;
            foreach (WX_UserGameLog gamelogitem in toupdate)
            {
                Game_Result gr = db.Game_Result.SingleOrDefault(t =>
                    t.aspnet_UserID == GlobalParam.UserKey
                    && t.GameName == gamelogitem.GameName
                    && t.GamePeriod == gamelogitem.GamePeriod.Substring(2)
                    );
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
                            gamelogitem.Result_Point = 0;
                        }
                        break;
                    case "大小和":
                        if (gamelogitem.Buy_Value == gamelogitem.Gr_BigSmall)
                        {
                            gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;
                        }
                        else
                        {
                            gamelogitem.Result_Point = 0;
                        }
                        break;
                    case "单双":
                        if (gamelogitem.Buy_Value == gamelogitem.Gr_SingleDouble)
                        {
                            gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;
                        }
                        else
                        {
                            gamelogitem.Result_Point = 0;
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
                    case "组合":
                        switch (gamelogitem.Buy_Value)
                        {
                            #region "两个组合3*2+3*3+2*3-1(和双)=20"
                            case "大单":
                                if (gamelogitem.Gr_BigSmall == "大" && gamelogitem.Gr_SingleDouble == "单")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "大双":
                                if (gamelogitem.Gr_BigSmall == "大" && gamelogitem.Gr_SingleDouble == "双")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "小单":
                                if (gamelogitem.Gr_BigSmall == "小" && gamelogitem.Gr_SingleDouble == "单")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "小双":
                                if (gamelogitem.Gr_BigSmall == "小" && gamelogitem.Gr_SingleDouble == "双")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "和单":
                                if (gamelogitem.Gr_BigSmall == "和" && gamelogitem.Gr_SingleDouble == "单")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;

                            case "大虎":
                                if (gamelogitem.Gr_BigSmall == "大" && gamelogitem.Gr_DragonTiger == "虎")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "大合":
                                if (gamelogitem.Gr_BigSmall == "大" && gamelogitem.Gr_DragonTiger == "合")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "大龙":
                                if (gamelogitem.Gr_BigSmall == "大" && gamelogitem.Gr_DragonTiger == "龙")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "小虎":
                                if (gamelogitem.Gr_BigSmall == "小" && gamelogitem.Gr_DragonTiger == "虎")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "小合":
                                if (gamelogitem.Gr_BigSmall == "小" && gamelogitem.Gr_DragonTiger == "合")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "小龙":
                                if (gamelogitem.Gr_BigSmall == "小" && gamelogitem.Gr_DragonTiger == "龙")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "和虎":
                                if (gamelogitem.Gr_BigSmall == "和" && gamelogitem.Gr_DragonTiger == "虎")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "和合":
                                if (gamelogitem.Gr_BigSmall == "和" && gamelogitem.Gr_DragonTiger == "合")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "和龙":
                                if (gamelogitem.Gr_BigSmall == "和" && gamelogitem.Gr_DragonTiger == "龙")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;

                            case "单合":
                                if (gamelogitem.Gr_SingleDouble == "单" && gamelogitem.Gr_DragonTiger == "合")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "双合":
                                if (gamelogitem.Gr_SingleDouble == "双" && gamelogitem.Gr_DragonTiger == "合")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "单龙":
                                if (gamelogitem.Gr_SingleDouble == "单" && gamelogitem.Gr_DragonTiger == "龙")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;

                            case "双龙":
                                if (gamelogitem.Gr_SingleDouble == "双" && gamelogitem.Gr_DragonTiger == "龙")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "单虎":
                                if (gamelogitem.Gr_SingleDouble == "单" && gamelogitem.Gr_DragonTiger == "虎")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;

                            case "双虎":

                                if (gamelogitem.Gr_SingleDouble == "双" && gamelogitem.Gr_DragonTiger == "虎")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            #endregion

                            #region 三个组合3*2*3- 3(和双*3)=15
                            case "大单龙":

                                if (gamelogitem.Gr_BigSmall == "大" && gamelogitem.Gr_SingleDouble == "单" && gamelogitem.Gr_DragonTiger == "龙")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "大单虎":
                                if (gamelogitem.Gr_BigSmall == "大" && gamelogitem.Gr_SingleDouble == "单" && gamelogitem.Gr_DragonTiger == "虎")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "大单合":
                                if (gamelogitem.Gr_BigSmall == "大" && gamelogitem.Gr_SingleDouble == "单" && gamelogitem.Gr_DragonTiger == "合")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "大双龙":
                                if (gamelogitem.Gr_BigSmall == "大" && gamelogitem.Gr_SingleDouble == "双" && gamelogitem.Gr_DragonTiger == "龙")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "大双虎":
                                if (gamelogitem.Gr_BigSmall == "大" && gamelogitem.Gr_SingleDouble == "双" && gamelogitem.Gr_DragonTiger == "虎")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "大双合":
                                if (gamelogitem.Gr_BigSmall == "大" && gamelogitem.Gr_SingleDouble == "双" && gamelogitem.Gr_DragonTiger == "合")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "小单龙":
                                if (gamelogitem.Gr_BigSmall == "小" && gamelogitem.Gr_SingleDouble == "单" && gamelogitem.Gr_DragonTiger == "龙")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "小单虎":
                                if (gamelogitem.Gr_BigSmall == "小" && gamelogitem.Gr_SingleDouble == "单" && gamelogitem.Gr_DragonTiger == "虎")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "小单合":
                                if (gamelogitem.Gr_BigSmall == "小" && gamelogitem.Gr_SingleDouble == "单" && gamelogitem.Gr_DragonTiger == "合")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "小双龙":
                                if (gamelogitem.Gr_BigSmall == "小" && gamelogitem.Gr_SingleDouble == "双" && gamelogitem.Gr_DragonTiger == "龙")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "小双虎":
                                if (gamelogitem.Gr_BigSmall == "小" && gamelogitem.Gr_SingleDouble == "双" && gamelogitem.Gr_DragonTiger == "虎")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "小双合":
                                if (gamelogitem.Gr_BigSmall == "小" && gamelogitem.Gr_SingleDouble == "双" && gamelogitem.Gr_DragonTiger == "合")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "和单龙":
                                if (gamelogitem.Gr_BigSmall == "和" && gamelogitem.Gr_SingleDouble == "单" && gamelogitem.Gr_DragonTiger == "龙")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "和单虎":
                                if (gamelogitem.Gr_BigSmall == "和" && gamelogitem.Gr_SingleDouble == "单" && gamelogitem.Gr_DragonTiger == "虎")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            case "和单合":
                                if (gamelogitem.Gr_BigSmall == "和" && gamelogitem.Gr_SingleDouble == "单" && gamelogitem.Gr_DragonTiger == "合")
                                {
                                    gamelogitem.Result_Point = gamelogitem.Buy_Ratio * gamelogitem.Buy_Point;

                                }
                                else
                                {
                                    gamelogitem.Result_Point = 0;
                                }
                                break;
                            #endregion

                            default:
                                gamelogitem.Result_Point = 0;
                                break;
                        }
                        break;
                    case "全X":
                        string NewBuyXNum = gamelogitem.Buy_Value.Substring(1, 1);

                        string Grnum1 = gamelogitem.GameResult.Substring(0, 1);
                        string Grnum2 = gamelogitem.GameResult.Substring(1, 1);
                        string Grnum3 = gamelogitem.GameResult.Substring(2, 1);
                        string Grnum4 = gamelogitem.GameResult.Substring(3, 1);
                        string Grnum5 = gamelogitem.GameResult.Substring(4, 1);
                        string BuyXNum = "";
                        switch (NewBuyXNum)
                        {
                            case "零":
                                BuyXNum = "0";
                                break;
                            case "一":
                                BuyXNum = "1";
                                break;
                            case "二":
                                BuyXNum = "2";
                                break;
                            case "三":
                                BuyXNum = "3";
                                break;
                            case "四":
                                BuyXNum = "4";
                                break;
                            case "五":
                                BuyXNum = "5";
                                break;
                            case "六":
                                BuyXNum = "6";
                                break;
                            case "七":
                                BuyXNum = "7";
                                break;
                            case "八":
                                BuyXNum = "8";
                                break;
                            case "九":
                                BuyXNum = "9";
                                break;
                            default:
                                break;
                        }
                        int FullCount = 0;
                        if (Grnum1 == BuyXNum)
                        {
                            FullCount += 1;
                        }
                        if (Grnum2 == BuyXNum)
                        {
                            FullCount += 1;
                        }
                        if (Grnum3 == BuyXNum)
                        {
                            FullCount += 1;
                        }
                        if (Grnum4 == BuyXNum)
                        {
                            FullCount += 1;
                        }
                        if (Grnum5 == BuyXNum)
                        {
                            FullCount += 1;
                        }
                        switch (FullCount)
                        {
                            case 1:
                                gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio_Full1;
                                break;
                            case 2:
                                gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio_Full2;
                                break;
                            case 3:
                                gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio_Full3;

                                break;
                            case 4:
                                gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio_Full4;

                                break;
                            case 5:
                                gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio_Full5;

                                break;
                            default:
                                gamelogitem.Result_Point = 0;
                                break;
                        }
                        break;
                    case "定X":
                        string Pos_BuyPos = gamelogitem.Buy_Value.Substring(0, 1);
                        string NewPos_BuyXNum = gamelogitem.Buy_Value.Substring(1, 1);
                        string Pos_BuyXNum = "";
                        switch (NewPos_BuyXNum)
                        {
                            case "零":
                                Pos_BuyXNum = "0";
                                break;
                            case "一":
                                Pos_BuyXNum = "1";
                                break;
                            case "二":
                                Pos_BuyXNum = "2";
                                break;
                            case "三":
                                Pos_BuyXNum = "3";
                                break;
                            case "四":
                                Pos_BuyXNum = "4";
                                break;
                            case "五":
                                Pos_BuyXNum = "5";
                                break;
                            case "六":
                                Pos_BuyXNum = "6";
                                break;
                            case "七":
                                Pos_BuyXNum = "7";
                                break;
                            case "八":
                                Pos_BuyXNum = "8";
                                break;
                            case "九":
                                Pos_BuyXNum = "9";
                                break;

                            default:
                                Pos_BuyXNum = NewPos_BuyXNum;
                                break;
                        }
                        string Pos_BGrnum1 = gamelogitem.GameResult.Substring(0, 1);
                        string Pos_BGrnum2 = gamelogitem.GameResult.Substring(1, 1);
                        string Pos_BGrnum3 = gamelogitem.GameResult.Substring(2, 1);
                        string Pos_BGrnum4 = gamelogitem.GameResult.Substring(3, 1);
                        string Pos_BGrnum5 = gamelogitem.GameResult.Substring(4, 1);

                        if (Pos_BuyXNum == "单" || Pos_BuyXNum == "双")
                        {
                            #region 定位单双
                            switch (Pos_BuyPos)
                            {
                                case "个":
                                    if (
                                        (Convert.ToInt32(Pos_BGrnum5) % 2 == 1 && Pos_BuyXNum == "单")
                                        || (Convert.ToInt32(Pos_BGrnum5) % 2 == 0 && Pos_BuyXNum == "双")
                                        )
                                    {
                                        gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio;
                                    }
                                    else
                                    {
                                        gamelogitem.Result_Point = 0;
                                    }
                                    break;
                                case "十":
                                    if (
                                       (Convert.ToInt32(Pos_BGrnum4) % 2 == 1 && Pos_BuyXNum == "单")
                                       || (Convert.ToInt32(Pos_BGrnum4) % 2 == 0 && Pos_BuyXNum == "双")
                                       )
                                    {
                                        gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio;
                                    }
                                    else
                                    {
                                        gamelogitem.Result_Point = 0;
                                    }
                                    break;
                                case "百":
                                    if (
                                        (Convert.ToInt32(Pos_BGrnum3) % 2 == 1 && Pos_BuyXNum == "单")
                                        || (Convert.ToInt32(Pos_BGrnum3) % 2 == 0 && Pos_BuyXNum == "双")
                                        )
                                    {
                                        gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio;
                                    }
                                    else
                                    {
                                        gamelogitem.Result_Point = 0;
                                    }
                                    break;
                                case "千":
                                    if (
                                        (Convert.ToInt32(Pos_BGrnum2) % 2 == 1 && Pos_BuyXNum == "单")
                                        || (Convert.ToInt32(Pos_BGrnum2) % 2 == 0 && Pos_BuyXNum == "双")
                                        )
                                    {
                                        gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio;
                                    }
                                    else
                                    {
                                        gamelogitem.Result_Point = 0;
                                    }
                                    break;
                                case "万":
                                    if (
                                        (Convert.ToInt32(Pos_BGrnum1) % 2 == 1 && Pos_BuyXNum == "单")
                                        || (Convert.ToInt32(Pos_BGrnum1) % 2 == 0 && Pos_BuyXNum == "双")
                                        )
                                    {
                                        gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio;
                                    }
                                    else
                                    {
                                        gamelogitem.Result_Point = 0;
                                    }
                                    break;
                                default:
                                    gamelogitem.Result_Point = 0;
                                    break;
                            }//switch
                            #endregion
                        }

                        else if (Pos_BuyXNum == "大" || Pos_BuyXNum == "小")
                        {
                            #region 定位大小
                            switch (Pos_BuyPos)
                            {
                                case "个":
                                    if (
                                        (Convert.ToInt32(Pos_BGrnum5) >= 5 && Pos_BuyXNum == "大")
                                        || (Convert.ToInt32(Pos_BGrnum5) <= 4 && Pos_BuyXNum == "小")
                                        )
                                    {
                                        gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio;
                                    }
                                    else
                                    {
                                        gamelogitem.Result_Point = 0;
                                    }
                                    break;
                                case "十":
                                    if (
                                        (Convert.ToInt32(Pos_BGrnum4) >= 5 && Pos_BuyXNum == "大")
                                        || (Convert.ToInt32(Pos_BGrnum4) <= 4 && Pos_BuyXNum == "小")
                                        )
                                    {
                                        gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio;
                                    }
                                    else
                                    {
                                        gamelogitem.Result_Point = 0;
                                    }
                                    break;
                                case "百":
                                    if (
                                       (Convert.ToInt32(Pos_BGrnum3) >= 5 && Pos_BuyXNum == "大")
                                       || (Convert.ToInt32(Pos_BGrnum3) <= 4 && Pos_BuyXNum == "小")
                                       )
                                    {
                                        gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio;
                                    }
                                    else
                                    {
                                        gamelogitem.Result_Point = 0;
                                    }
                                    break;
                                case "千":
                                    if (
                                       (Convert.ToInt32(Pos_BGrnum2) >= 5 && Pos_BuyXNum == "大")
                                       || (Convert.ToInt32(Pos_BGrnum2) <= 4 && Pos_BuyXNum == "小")
                                       )
                                    {
                                        gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio;
                                    }
                                    else
                                    {
                                        gamelogitem.Result_Point = 0;
                                    }
                                    break;
                                case "万":
                                    if (
                                        (Convert.ToInt32(Pos_BGrnum1) >= 5 && Pos_BuyXNum == "大")
                                        || (Convert.ToInt32(Pos_BGrnum1) <= 4 && Pos_BuyXNum == "小")
                                        )
                                    {
                                        gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio;
                                    }
                                    else
                                    {
                                        gamelogitem.Result_Point = 0;
                                    }
                                    break;
                                default:
                                    gamelogitem.Result_Point = 0;
                                    break;
                            }//switch
                            #endregion
                        }
                        else
                        {
                            #region 定位数字
                            switch (Pos_BuyPos)
                            {
                                case "个":
                                    if (Pos_BGrnum5 == Pos_BuyXNum)
                                    {
                                        gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio;
                                    }
                                    else
                                    {
                                        gamelogitem.Result_Point = 0;
                                    }
                                    break;
                                case "十":
                                    if (Pos_BGrnum4 == Pos_BuyXNum)
                                    {
                                        gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio;
                                    }
                                    else
                                    {
                                        gamelogitem.Result_Point = 0;
                                    }
                                    break;
                                case "百":
                                    if (Pos_BGrnum3 == Pos_BuyXNum)
                                    {
                                        gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio;
                                    }
                                    else
                                    {
                                        gamelogitem.Result_Point = 0;
                                    }
                                    break;
                                case "千":
                                    if (Pos_BGrnum2 == Pos_BuyXNum)
                                    {
                                        gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio;
                                    }
                                    else
                                    {
                                        gamelogitem.Result_Point = 0;
                                    }
                                    break;
                                case "万":
                                    if (Pos_BGrnum1 == Pos_BuyXNum)
                                    {
                                        gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio;
                                    }
                                    else
                                    {
                                        gamelogitem.Result_Point = 0;
                                    }
                                    break;
                                default:
                                    gamelogitem.Result_Point = 0;
                                    break;
                            }
                            #endregion

                        }



                        break;
                    default:
                        break;
                }//根据买的类型和数值处理

                #region 单买龙虎出合，大小出和，庄家不吃的返点
                if (gamelogitem.Buy_Value == "大" || gamelogitem.Buy_Value == "小" || gamelogitem.Buy_Value == "和")
                {
                    if (gamelogitem.Gr_BigSmall == "和")
                    {
                        gamelogitem.Result_Point += gamelogitem.Buy_Point * (gamelogitem.BounsRatio_WhenMiddle.HasValue ? gamelogitem.BounsRatio_WhenMiddle.Value : 0);
                    }
                }
                if (gamelogitem.Buy_Value == "龙" || gamelogitem.Buy_Value == "虎" || gamelogitem.Buy_Value == "合")
                {
                    if (gamelogitem.Gr_DragonTiger == "和")
                    {
                        gamelogitem.Result_Point += gamelogitem.Buy_Point * (gamelogitem.BounsRatio_WhenOK.HasValue ? gamelogitem.BounsRatio_WhenOK.Value : 0);
                    }
                }
                if (gamelogitem.Buy_Value == "单" || gamelogitem.Buy_Value == "双")
                {
                    if (gamelogitem.Gr_NumTotal == 23)
                    {
                        gamelogitem.Result_Point += gamelogitem.Buy_Point * (gamelogitem.BounsRatio_Wen23.HasValue ? gamelogitem.BounsRatio_Wen23.Value : 0);
                    }
                }
                #endregion
                //只取正整数
                gamelogitem.Result_Point = Convert.ToInt32(Math.Floor(gamelogitem.Result_Point.Value));

                gamelogitem.Result_HaveProcess = true;
                #region "同步下单立即修改的"
                var cltoupdate = db.WX_UserChangeLog.Where(t => t.FinalStatus == false
                    && t.GamePeriod == gamelogitem.GamePeriod
                    && t.aspnet_UserID == gamelogitem.aspnet_UserID
                    && t.WX_UserName == gamelogitem.WX_UserName
                    && t.WX_SourceType == gamelogitem.WX_SourceType
                    );
                foreach (var clgameitem in cltoupdate)
                {
                    clgameitem.FinalStatus = true;
                }
                #endregion


                if (gamelogitem.Result_Point != 0)
                {
                    WX_UserChangeLog cl = new WX_UserChangeLog();
                    cl.aspnet_UserID = gamelogitem.aspnet_UserID;
                    cl.WX_UserName = gamelogitem.WX_UserName;
                    cl.WX_SourceType = gamelogitem.WX_SourceType;
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

                    cl.GamePeriod = gamelogitem.GamePeriod;
                    cl.GameLocalPeriod = gamelogitem.GameLocalPeriod;
                    cl.ChangeLocalDay = gamelogitem.GameLocalPeriod.Substring(0, 8);
                    cl.BuyValue = gamelogitem.Buy_Value;

                    cl.NeedNotice = true;
                    cl.HaveNotice = false;
                    cl.FinalStatus = true;


                    db.WX_UserChangeLog.InsertOnSubmit(cl);
                }


                db.SubmitChanges();

                Result += 1;

            }//行循环处理
            //增加ChangeLog
            return Result;

        }





        public static string WX_UserGameLog_Cancel(dbDataContext db, WX_UserReplyLog reply, DataTable MemberSource, bool adminmode)
        {


            #region "取消球赛类"

            FormatResultState BallState = FormatResultState.Initialize;
            FormatResultType BallType = FormatResultType.Initialize;
            string BuyType = "";
            string BuyMoney = "";

            string[] q_Teams = new string[] { };

            Linq.WX_UserGameLog_Football[] Games = (Linq.WX_UserGameLog_Football[])ReceiveContentFormat(reply.ReceiveContent, out BallState, out BallType, FormatResultDirection.DataBaseGameLog, out BuyType, out BuyMoney, out q_Teams);

            Int32 testsuccess = 0;

            WX_UserGameLog_Football[] cancancel = ContentToGameLogBall(reply, Games, BuyType, BuyMoney, out testsuccess, FormatResultDirection.DataBaseGameLog, db);


            if (testsuccess == 1 && BallType == FormatResultType.CancelOrderModify)
            {
                decimal Op_CancelMoeny = Convert.ToDecimal(BuyMoney);

                if ((cancancel.Sum(t => t.BuyMoney) == null ? 0 : cancancel.Sum(t => t.BuyMoney)) < Op_CancelMoeny)
                {
                    return "下注不足,取消失败" + WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType);
                }

                #region
                foreach (WX_UserGameLog_Football item in cancancel)
                {
                    if (item.BuyMoney >= Convert.ToDecimal(BuyMoney))
                    {
                        item.BuyMoney = item.BuyMoney - Op_CancelMoeny;
                        Op_CancelMoeny = 0;
                    }
                    else
                    {
                        Op_CancelMoeny -= item.BuyMoney.Value;
                        item.BuyMoney = 0;
                        item.HaveOpen = true;

                    }
                }
                db.SubmitChanges();
                string Buys = GetUserUpOpenBallGame(db, reply.WX_UserName, reply.WX_SourceType);
                return Buys + ",余" + WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType);
                #endregion


            }



            #endregion




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

            Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == reply.ReceiveTime.ToString("HH:mm"));
            if (testmin != null)
            {
                return "整点" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

            }

            #region "取消全"
            if (reply.ReceiveContent.StartsWith("取消全"))
            {
                if (reply.ReceiveContent.Length <= 3)
                {
                    return "";
                }
                string Str_BuyPointful = reply.ReceiveContent.Substring(4);
                string BuyXnUMBER = reply.ReceiveContent.Substring(3, 1);

                decimal BuyPointfull = 0;
                try
                {
                    BuyPointfull = Convert.ToDecimal(Str_BuyPointful);
                }
                catch (Exception)
                {
                }
                if (BuyPointfull == 0)
                {
                    return "";
                }
                if (string.Compare(reply.ReceiveTime.ToString("HH:mm"), "01:55") >= 0 && string.Compare(reply.ReceiveTime.ToString("HH:mm"), "09:00") <= 0)
                {
                    return "封盘时间";
                }

                if (BuyXnUMBER == "零" || BuyXnUMBER == "一" || BuyXnUMBER == "二" || BuyXnUMBER == "三" || BuyXnUMBER == "四" || BuyXnUMBER == "五" || BuyXnUMBER == "六" || BuyXnUMBER == "七" || BuyXnUMBER == "八" || BuyXnUMBER == "九")
                {

                    if (NetFramework.Util_Math.IsNumber(Str_BuyPointful) == false)
                    {
                        return "";
                    }


                    WX_UserGameLog findupdate = db.WX_UserGameLog.SingleOrDefault(t =>
                         t.aspnet_UserID == GlobalParam.UserKey
                             && t.WX_UserName == reply.WX_UserName
                             && t.WX_SourceType == reply.WX_SourceType
                             && t.GamePeriod == TotalNextPeriod
                             && t.Buy_Value == reply.ReceiveContent.Substring(2, 2)
                         );

                    #region "检查最大可取消"
                    var ToModify = db.WX_UserGameLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                         && t.WX_UserName == reply.WX_UserName
                         && t.WX_SourceType == reply.WX_SourceType
                         && t.GameName == "重庆时时彩"
                         && t.GamePeriod == TotalNextPeriod
                         && t.Buy_Value == reply.ReceiveContent.Substring(2, 2)
                         && t.Buy_Point > 0
                         );
                    if (ToModify.Buy_Point < Convert.ToDecimal(Str_BuyPointful))
                    {
                        TotalResult tr1 = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                      && t.WX_UserName == reply.WX_UserName
                      && t.WX_SourceType == reply.WX_SourceType
                      && t.Buy_Point != 0
                      && t.Result_HaveProcess != true
                      ).ToList(), MemberSource);
                        return "下注不足，" + tr1.ToSlimStringV2() + "余:" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0"); ;
                    }
                    #endregion

                    #region 检查赔率
                    Decimal CheckBuy = (findupdate == null ? 0 : findupdate.Buy_Point.Value) - Convert.ToDecimal(Str_BuyPointful);

                    var ratios = db.Game_BasicRatio.Where(t =>
                        t.BuyType == "全X"
                      && t.aspnet_UserID == GlobalParam.UserKey
                       && t.MinBuy <= CheckBuy
                      && t.MaxBuy >= CheckBuy
                      );
                    if (ratios.Count() == 0 && CheckBuy != 0)
                    {
                        Decimal? MaxLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.UserKey
               && t.BuyType == "全X"

               ).Max(t => t.MaxBuy);

                        Decimal? MinLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                            && t.BuyType == "全X"
                                ).Min(t => t.MinBuy);
                        return "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0"); ;

                    }
                    if (ratios.Count() != 5 && ratios.Count() != 0)
                    {
                        return "全X限额范围不一致" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0"); ;
                    }
                    #endregion

                    if (findupdate.Result_HaveProcess == true)
                    {

                        WX_UserChangeLog findcl = db.WX_UserChangeLog.SingleOrDefault(t =>
                              t.aspnet_UserID == GlobalParam.UserKey
                              && t.WX_UserName == findupdate.WX_UserName
                              && t.WX_SourceType == findupdate.WX_SourceType
                              && t.RemarkType == "开奖"
                              && t.GamePeriod == findupdate.GamePeriod
                              && t.BuyValue == findupdate.Buy_Value
                              );
                        if (findcl != null)
                        {
                            db.WX_UserChangeLog.DeleteOnSubmit(findcl);
                        }


                        findupdate.Result_HaveProcess = false;
                    }

                    findupdate.Buy_Point -= Convert.ToDecimal(Str_BuyPointful);
                    findupdate.Buy_Ratio_Full1 = ratios.SingleOrDefault(t => t.BuyValue == "连1个") == null ? 0 : ratios.SingleOrDefault(t => t.BuyValue == "连1个").BasicRatio;
                    findupdate.Buy_Ratio_Full2 = ratios.SingleOrDefault(t => t.BuyValue == "连2个") == null ? 0 : ratios.SingleOrDefault(t => t.BuyValue == "连2个").BasicRatio;
                    findupdate.Buy_Ratio_Full3 = ratios.SingleOrDefault(t => t.BuyValue == "连3个") == null ? 0 : ratios.SingleOrDefault(t => t.BuyValue == "连3个").BasicRatio;
                    findupdate.Buy_Ratio_Full4 = ratios.SingleOrDefault(t => t.BuyValue == "连4个") == null ? 0 : ratios.SingleOrDefault(t => t.BuyValue == "连4个").BasicRatio;
                    findupdate.Buy_Ratio_Full5 = ratios.SingleOrDefault(t => t.BuyValue == "连5个") == null ? 0 : ratios.SingleOrDefault(t => t.BuyValue == "连5个").BasicRatio;



                    Linq.WX_UserChangeLog cl = null;

                    cl = new WX_UserChangeLog();
                    cl.aspnet_UserID = GlobalParam.UserKey;
                    cl.WX_UserName = reply.WX_UserName;
                    cl.WX_SourceType = reply.WX_SourceType;
                    cl.ChangePoint = Convert.ToDecimal(Str_BuyPointful); ;
                    cl.NeedNotice = false;
                    cl.HaveNotice = false;
                    cl.ChangeTime = reply.ReceiveTime;
                    cl.RemarkType = "取消";
                    cl.Remark = "取消@#" + reply.ReceiveContent;
                    cl.FinalStatus = false;
                    cl.BuyValue = findupdate.Buy_Value;
                    cl.GamePeriod = findupdate.GamePeriod;
                    cl.GameLocalPeriod = findupdate.GameLocalPeriod;
                    cl.ChangeLocalDay = findupdate.GameLocalPeriod.Substring(0, 8);

                    cl.FinalStatus = true;
                    db.WX_UserChangeLog.InsertOnSubmit(cl);
                    try
                    {
                        db.SubmitChanges();

                        TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == reply.WX_UserName && t.WX_SourceType == reply.WX_SourceType
                            && t.Result_HaveProcess == false
                            && t.Buy_Point != 0).ToList(), MemberSource);
                        return tr.ToSlimStringV2() + "余:" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                    }
                    catch (Exception AnyError)
                    {

                        return AnyError.Message + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");
                    }



                }
                else
                {
                    return "";
                }
            }
            #endregion
            else if (reply.ReceiveContent.StartsWith("取消个")
               || reply.ReceiveContent.StartsWith("取消十")
                || reply.ReceiveContent.StartsWith("取消百")
                || reply.ReceiveContent.StartsWith("取消千")
                  || reply.ReceiveContent.StartsWith("取消万")
               )
            {
                if (reply.ReceiveContent.Length < 3)
                {
                    return "";
                }
                string BuyXnUMBER = reply.ReceiveContent.Substring(3, 1);
                string Str_BuyPointpos = reply.ReceiveContent.Substring(4);
                decimal BuyPointpos = 0;
                try
                {
                    BuyPointpos = Convert.ToDecimal(Str_BuyPointpos);
                }
                catch (Exception)
                {
                }
                if (BuyPointpos == 0)
                {
                    return "";
                }
                if (string.Compare(reply.ReceiveTime.ToString("HH:mm"), "01:55") >= 0 && string.Compare(reply.ReceiveTime.ToString("HH:mm"), "09:00") <= 0)
                {
                    return "封盘时间";
                }


                if (BuyXnUMBER == "零" || BuyXnUMBER == "一" || BuyXnUMBER == "二" || BuyXnUMBER == "三" || BuyXnUMBER == "四" || BuyXnUMBER == "五" || BuyXnUMBER == "六" || BuyXnUMBER == "七" || BuyXnUMBER == "八" || BuyXnUMBER == "九"
                    || BuyXnUMBER == "单" || BuyXnUMBER == "双" || BuyXnUMBER == "大" || BuyXnUMBER == "小"
                    )
                {

                    if (NetFramework.Util_Math.IsNumber(Str_BuyPointpos) == false)
                    {
                        return "";
                    }



                    WX_UserGameLog findupdate = db.WX_UserGameLog.SingleOrDefault(t =>
                                                t.aspnet_UserID == GlobalParam.UserKey
                                                    && t.WX_UserName == reply.WX_UserName
                                                    && t.WX_SourceType == reply.WX_SourceType
                                                    && t.GamePeriod == reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod
                                                    && t.Buy_Value == reply.ReceiveContent.Substring(2, 2)
                                                );
                    #region "检查最大可取消"
                    var ToModify = db.WX_UserGameLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                         && t.WX_UserName == reply.WX_UserName
                         && t.WX_SourceType == reply.WX_SourceType
                         && t.GameName == "重庆时时彩"
                         && t.GamePeriod == TotalNextPeriod
                         && t.Buy_Value == reply.ReceiveContent.Substring(2, 2)
                         && t.Buy_Point > 0
                         );
                    if (ToModify.Buy_Point < Convert.ToDecimal(Str_BuyPointpos))
                    {
                        TotalResult tr1 = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                      && t.WX_UserName == reply.WX_UserName
                      && t.WX_SourceType == reply.WX_SourceType
                      && t.Buy_Point != 0
                      && t.Result_HaveProcess != true
                      ).ToList(), MemberSource);
                        return "下注不足，" + tr1.ToSlimStringV2() + "余:" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0"); ;
                    }
                    #endregion

                    #region 检查赔率
                    string NewBuyXNumber = "";
                    switch (BuyXnUMBER)
                    {
                        case "零":
                            NewBuyXNumber = "0";
                            break;
                        case "一":
                            NewBuyXNumber = "1";
                            break;
                        case "二":
                            NewBuyXNumber = "2";
                            break;
                        case "三":
                            NewBuyXNumber = "3";
                            break;
                        case "四":
                            NewBuyXNumber = "4";
                            break;
                        case "五":
                            NewBuyXNumber = "5";
                            break;
                        case "六":
                            NewBuyXNumber = "6";
                            break;
                        case "七":
                            NewBuyXNumber = "7";
                            break;
                        case "八":
                            NewBuyXNumber = "8";
                            break;
                        case "九":
                            NewBuyXNumber = "9";
                            break;
                        default:
                            break;
                    }
                    Boolean BuyXnUMBERIsNumber = NetFramework.Util_Math.IsNumber(NewBuyXNumber);

                    Decimal CheckBuy = (findupdate == null ? 0 : findupdate.Buy_Point.Value) - Convert.ToDecimal(Str_BuyPointpos);

                    var ratios = db.Game_BasicRatio.SingleOrDefault(t => t.BuyType == "定X" && t.aspnet_UserID == GlobalParam.UserKey
                       && (t.BuyValue == (BuyXnUMBERIsNumber == true ? "数字" : BuyXnUMBER))

                        && t.MinBuy <= CheckBuy
                        && t.MaxBuy >= CheckBuy
                        );
                    if (ratios == null && (findupdate == null ? 0 : findupdate.Buy_Point) - Convert.ToDecimal(Str_BuyPointpos) != 0)
                    {
                        Decimal? MaxLimit = db.Game_BasicRatio.Where(t =>
                            t.aspnet_UserID == GlobalParam.UserKey
               && t.BuyType == "定X"
                  && (t.BuyValue == (BuyXnUMBERIsNumber == true ? "数字" : BuyXnUMBER))

               ).Max(t => t.MaxBuy);
                        Decimal? MinLimit = db.Game_BasicRatio.Where(t =>
                            t.aspnet_UserID == GlobalParam.UserKey
                            && t.BuyType == "定X"
                               && (t.BuyValue == (BuyXnUMBERIsNumber == true ? "数字" : BuyXnUMBER))

                            ).Min(t => t.MinBuy);
                        return "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                    }
                    #endregion



                    if (findupdate.Result_HaveProcess == true)
                    {

                        WX_UserChangeLog findcl = db.WX_UserChangeLog.SingleOrDefault(t =>
                              t.aspnet_UserID == GlobalParam.UserKey
                              && t.WX_UserName == findupdate.WX_UserName
                              && t.WX_SourceType == findupdate.WX_SourceType
                              && t.RemarkType == "开奖"
                              && t.GamePeriod == findupdate.GamePeriod
                              && t.BuyValue == findupdate.Buy_Value
                              );
                        if (findcl != null)
                        {
                            db.WX_UserChangeLog.DeleteOnSubmit(findcl);
                        }
                        findupdate.Result_HaveProcess = false;
                    }

                    findupdate.Buy_Point -= BuyPointpos;
                    findupdate.Buy_Ratio = ratios == null ? 0 : ratios.BasicRatio;



                    Linq.WX_UserChangeLog cl = null;

                    cl = new WX_UserChangeLog();
                    cl.aspnet_UserID = GlobalParam.UserKey;
                    cl.WX_UserName = reply.WX_UserName;
                    cl.WX_SourceType = reply.WX_SourceType;
                    cl.ChangePoint = Convert.ToDecimal(Str_BuyPointpos);
                    cl.NeedNotice = false;
                    cl.HaveNotice = false;
                    cl.ChangeTime = reply.ReceiveTime;
                    cl.RemarkType = "取消";
                    cl.Remark = "取消@#" + reply.ReceiveContent;
                    cl.FinalStatus = false;
                    cl.BuyValue = findupdate.Buy_Value;
                    cl.GamePeriod = findupdate.GamePeriod;
                    cl.GameLocalPeriod = findupdate.GameLocalPeriod;
                    cl.ChangeLocalDay = findupdate.GameLocalPeriod.Substring(0, 8);
                    cl.FinalStatus = true;
                    db.WX_UserChangeLog.InsertOnSubmit(cl);
                    try
                    {
                        db.SubmitChanges();

                        TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                            && t.WX_UserName == reply.WX_UserName
                            && t.WX_SourceType == reply.WX_SourceType
                            && t.Result_HaveProcess == false
                            && t.Buy_Point != 0).ToList(), MemberSource);
                        return tr.ToSlimStringV2() + "余:" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                    }
                    catch (Exception AnyError)
                    {

                        return AnyError.Message + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");
                    }



                }
                else
                {
                    return "";
                }
            }

            else if (NetFramework.Util_Math.IsNumber(Str_BuyPoint) == false)
            {
                #region 组合类
                if (reply.ReceiveContent.Length >= 5)
                {
                    string BuyType2 = reply.ReceiveContent.Substring(2, 2);
                    string StrBuyPoint2 = reply.ReceiveContent.Substring(4);

                    if (NetFramework.Util_Math.IsNumber(StrBuyPoint2) == false)
                    {
                        string BuyType3 = reply.ReceiveContent.Substring(2, 3);
                        string StrBuyPoint3 = reply.ReceiveContent.Substring(5);
                        if (reply.ReceiveContent.Length >= 4)
                        {
                            if (NetFramework.Util_Math.IsNumber(StrBuyPoint3) == false)
                            {
                                return "";
                            }//3位后不是数字
                            else
                            {
                                if (ComboString.ContainsKey(BuyType3))
                                {
                                    string KeyValue3 = "";
                                    ComboString.TryGetValue(BuyType3, out KeyValue3);
                                    WX_UserGameLog findupdate3 = db.WX_UserGameLog.SingleOrDefault(t =>
                                        t.aspnet_UserID == GlobalParam.UserKey
                                        && t.WX_UserName == reply.WX_UserName
                                        && t.WX_SourceType == reply.WX_SourceType
                                        && t.Buy_Value == KeyValue3
                                         && t.GamePeriod == reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod
                                        );


                                    #region "检查最大可取消"
                                    var ToModify = db.WX_UserGameLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                                         && t.WX_UserName == reply.WX_UserName
                                         && t.WX_SourceType == reply.WX_SourceType
                                         && t.GameName == "重庆时时彩"
                                         && t.GamePeriod == TotalNextPeriod
                                         && t.Buy_Value == KeyValue3
                                         && t.Buy_Point > 0
                                         );
                                    if (ToModify.Buy_Point < Convert.ToDecimal(StrBuyPoint3))
                                    {
                                        TotalResult tr1 = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                                      && t.WX_UserName == reply.WX_UserName
                                      && t.WX_SourceType == reply.WX_SourceType
                                      && t.Buy_Point != 0
                                      && t.Result_HaveProcess != true
                                      ).ToList(), MemberSource);
                                        return "下注不足，" + tr1.ToSlimStringV2() + "余:" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0"); ;
                                    }
                                    #endregion
                                    #region 检查赔率

                                    Decimal CheckBuy = (findupdate3 == null ? 0 : findupdate3.Buy_Point.Value) - Convert.ToDecimal(StrBuyPoint3);

                                    var ratios = db.Game_BasicRatio.SingleOrDefault(t =>
                                        t.BuyType == "组合"
                                        && t.aspnet_UserID == GlobalParam.UserKey
                                       && (t.BuyValue == KeyValue3)

                                        && t.MinBuy <= CheckBuy
                                        && t.MaxBuy >= CheckBuy
                                        );
                                    if (ratios == null && (findupdate3 == null ? 0 : findupdate3.Buy_Point) - Convert.ToDecimal(StrBuyPoint3) != 0)
                                    {
                                        Decimal? MaxLimit = db.Game_BasicRatio.Where(t =>
                                            t.aspnet_UserID == GlobalParam.UserKey
                               && t.BuyType == "组合"
                                  && (t.BuyValue == KeyValue3)

                               ).Max(t => t.MaxBuy);
                                        Decimal? MinLimit = db.Game_BasicRatio.Where(t =>
                                            t.aspnet_UserID == GlobalParam.UserKey
                                           && t.BuyType == "组合"
                                  && (t.BuyValue == KeyValue3)
                                            ).Min(t => t.MinBuy);
                                        return "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                                    }
                                    #endregion




                                    if (findupdate3.Result_HaveProcess == true)
                                    {

                                        WX_UserChangeLog findcl = db.WX_UserChangeLog.SingleOrDefault(t =>
                              t.aspnet_UserID == GlobalParam.UserKey
                              && t.WX_UserName == findupdate3.WX_UserName
                              && t.WX_SourceType == findupdate3.WX_SourceType
                              && t.RemarkType == "开奖"
                              && t.GamePeriod == findupdate3.GamePeriod
                              && t.BuyValue == findupdate3.Buy_Value
                              );
                                        if (findcl != null)
                                        {
                                            db.WX_UserChangeLog.DeleteOnSubmit(findcl);
                                        }
                                        findupdate3.Result_HaveProcess = false;
                                    }

                                    findupdate3.Buy_Point -= Convert.ToDecimal(StrBuyPoint3); ;
                                    findupdate3.Buy_Ratio = (ratios == null ? 0 : ratios.BasicRatio);



                                    Linq.WX_UserChangeLog cl = null;

                                    cl = new WX_UserChangeLog();
                                    cl.aspnet_UserID = GlobalParam.UserKey;
                                    cl.WX_UserName = reply.WX_UserName;
                                    cl.WX_SourceType = reply.WX_SourceType;
                                    cl.ChangePoint = Convert.ToDecimal(StrBuyPoint3);
                                    cl.NeedNotice = false;
                                    cl.HaveNotice = false;
                                    cl.ChangeTime = reply.ReceiveTime;
                                    cl.RemarkType = "取消";
                                    cl.Remark = "取消@#" + reply.ReceiveContent;
                                    cl.FinalStatus = false;
                                    cl.BuyValue = findupdate3.Buy_Value;
                                    cl.GamePeriod = findupdate3.GamePeriod;
                                    cl.ChangeLocalDay = findupdate3.GameLocalPeriod.Substring(0, 8);

                                    cl.FinalStatus = true;
                                    db.WX_UserChangeLog.InsertOnSubmit(cl);
                                    try
                                    {
                                        db.SubmitChanges();

                                        TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                                            && t.WX_UserName == reply.WX_UserName
                                            && t.WX_SourceType == reply.WX_SourceType
                                            && t.Result_HaveProcess == false
                                            && t.Buy_Point != 0).ToList(), MemberSource);
                                        return tr.ToSlimStringV2() + "余:" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                                    }
                                    catch (Exception AnyError)
                                    {

                                        return AnyError.Message + ",余:" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");
                                    }

                                }//字典有数字
                                else
                                {
                                    return "";
                                }//字典没数字
                            }//3位后是数字
                        }//超过4位才可能是3数字的组合
                        else
                        {
                            return "";
                        }
                    }//2位后不是数字
                    {

                        if (ComboString.ContainsKey(BuyType2))
                        {
                            string KeyValue2 = "";
                            ComboString.TryGetValue(BuyType2, out KeyValue2);

                            WX_UserGameLog findupdate2 = db.WX_UserGameLog.SingleOrDefault(t =>
                                t.aspnet_UserID == GlobalParam.UserKey
                                && t.WX_UserName == reply.WX_UserName
                                && t.WX_SourceType == reply.WX_SourceType
                                && t.Buy_Value == KeyValue2
                                 && t.GamePeriod == reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod
                                );

                            #region "检查最大可取消"
                            var ToModify = db.WX_UserGameLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                                 && t.WX_UserName == reply.WX_UserName
                                 && t.WX_SourceType == reply.WX_SourceType
                                 && t.GameName == "重庆时时彩"
                                 && t.GamePeriod == TotalNextPeriod
                                 && t.Buy_Value == KeyValue2
                                 && t.Buy_Point > 0
                                 );

                            if (ToModify.Buy_Point < Convert.ToDecimal(StrBuyPoint2))
                            {
                                TotalResult tr1 = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                              && t.WX_UserName == reply.WX_UserName
                              && t.WX_SourceType == reply.WX_SourceType
                              && t.Buy_Point != 0
                              && t.Result_HaveProcess != true
                              ).ToList(), MemberSource);
                                return "下注不足，" + tr1.ToSlimStringV2() + "余:" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0"); ;
                            }
                            #endregion
                            #region 检查赔率

                            Decimal CheckBuy = (findupdate2 == null ? 0 : findupdate2.Buy_Point.Value) - Convert.ToDecimal(StrBuyPoint2);

                            var ratios = db.Game_BasicRatio.SingleOrDefault(t =>
                                t.BuyType == "组合"
                                && t.aspnet_UserID == GlobalParam.UserKey
                               && (t.BuyValue == KeyValue2)

                                && t.MinBuy <= CheckBuy
                                && t.MaxBuy >= CheckBuy
                                );
                            if (ratios == null && (findupdate2 == null ? 0 : findupdate2.Buy_Point) - Convert.ToDecimal(StrBuyPoint2) != 0)
                            {
                                Decimal? MaxLimit = db.Game_BasicRatio.Where(t =>
                                    t.aspnet_UserID == GlobalParam.UserKey
                       && t.BuyType == "组合"
                          && (t.BuyValue == KeyValue2)

                       ).Max(t => t.MaxBuy);
                                Decimal? MinLimit = db.Game_BasicRatio.Where(t =>
                                    t.aspnet_UserID == GlobalParam.UserKey
                                   && t.BuyType == "组合"
                          && (t.BuyValue == KeyValue2)
                                    ).Min(t => t.MinBuy);
                                return "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                            }
                            #endregion


                            if (findupdate2.Result_HaveProcess == true)
                            {

                                WX_UserChangeLog findcl = db.WX_UserChangeLog.SingleOrDefault(t =>
                               t.aspnet_UserID == GlobalParam.UserKey
                               && t.WX_UserName == findupdate2.WX_UserName
                               && t.WX_SourceType == findupdate2.WX_SourceType
                               && t.RemarkType == "开奖"
                               && t.GamePeriod == findupdate2.GamePeriod
                               && t.BuyValue == findupdate2.Buy_Value
                               );
                                if (findcl != null)
                                {
                                    db.WX_UserChangeLog.DeleteOnSubmit(findcl);
                                }
                                findupdate2.Result_HaveProcess = false;
                            }
                            findupdate2.Buy_Point -= Convert.ToDecimal(StrBuyPoint2);
                            findupdate2.Buy_Ratio = ratios == null ? 0 : ratios.BasicRatio;


                            Linq.WX_UserChangeLog cl = null;

                            cl = new WX_UserChangeLog();
                            cl.aspnet_UserID = GlobalParam.UserKey;
                            cl.WX_UserName = reply.WX_UserName;
                            cl.WX_SourceType = reply.WX_SourceType;
                            cl.ChangePoint = Convert.ToDecimal(StrBuyPoint2);
                            cl.NeedNotice = false;
                            cl.HaveNotice = false;
                            cl.ChangeTime = reply.ReceiveTime;
                            cl.RemarkType = "取消";
                            cl.Remark = "取消@#" + reply.ReceiveContent;
                            cl.FinalStatus = false;

                            cl.BuyValue = findupdate2.Buy_Value;
                            cl.GamePeriod = findupdate2.GamePeriod;
                            cl.GameLocalPeriod = findupdate2.GameLocalPeriod;
                            cl.ChangeLocalDay = findupdate2.GameLocalPeriod.Substring(0, 8);
                            cl.FinalStatus = true;
                            db.WX_UserChangeLog.InsertOnSubmit(cl);
                            try
                            {
                                db.SubmitChanges();

                                TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                                    && t.WX_UserName == reply.WX_UserName
                                    && t.WX_SourceType == reply.WX_SourceType
                                    && t.Result_HaveProcess == false
                                   ).ToList(), MemberSource);
                                return tr.ToSlimStringV2() + "余:" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                            }
                            catch (Exception AnyError)
                            {

                                return AnyError.Message + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");
                            }
                        }//字典有数字
                        else
                        {
                            return "";
                        }//字典没数字
                    }//2位后数数字


                }//超过3位才可能是数字
                else
                {
                    return "";
                }
                #endregion
            }//非X+数字

            else
            #region X+数字
            {
                #region "检查最大可取消"
                var ToModify = db.WX_UserGameLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                     && t.WX_UserName == reply.WX_UserName
                     && t.WX_SourceType == reply.WX_SourceType
                     && t.GameName == "重庆时时彩"
                     && t.GamePeriod == TotalNextPeriod
                     && t.Buy_Value == FirstIndex
                     && t.Buy_Point > 0
                     );
                if (ToModify.Buy_Point < Convert.ToDecimal(Str_BuyPoint))
                {
                    TotalResult tr1 = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.WX_UserName == reply.WX_UserName
                  && t.WX_SourceType == reply.WX_SourceType
                  && t.Buy_Point != 0
                  && t.Result_HaveProcess != true
                  ).ToList(), MemberSource);
                    return "下注不足，" + tr1.ToSlimStringV2() + "余:" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0"); ;
                }
                #endregion

                #region "赔率重算，如果为0"

                string CheckResult = "";
                Decimal CheckBuy = (ToModify == null ? 0 : ToModify.Buy_Point.Value) - Convert.ToDecimal(Str_BuyPoint);

                #region "转化赔率"
                var CheckRatioConfig = db.Game_BasicRatio.SingleOrDefault(t => t.GameType == "重庆时时彩"

                    && t.aspnet_UserID == GlobalParam.UserKey
                    && t.BuyType == ToModify.Buy_Type
                    && t.BuyValue == ToModify.Buy_Value
                    && t.MaxBuy >= CheckBuy
                    && ((t.MinBuy <= CheckBuy && t.IncludeMin == true)
                    || (t.MinBuy < CheckBuy && t.IncludeMin == false)
                    )
                    );
                if (CheckRatioConfig == null && (ToModify.Buy_Point - Convert.ToDecimal(Str_BuyPoint) != 0))
                {
                    Decimal? MaxLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                        && t.BuyValue == ToModify.Buy_Value
                        && t.BuyType == ToModify.Buy_Type
                        ).Max(t => t.MaxBuy);
                    Decimal? MinLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                        && t.BuyValue == ToModify.Buy_Value
                         && t.BuyType == ToModify.Buy_Type
                        ).Min(t => t.MinBuy);
                    CheckResult = "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围";

                }
                #endregion


                if (CheckResult != "")
                {
                    TotalResult tr2 = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
               && t.WX_UserName == reply.WX_UserName
               && t.WX_SourceType == reply.WX_SourceType
               && t.Buy_Point != 0
               && t.Result_HaveProcess != true
               ).ToList(), MemberSource);
                    return CheckResult + "," + tr2.ToSlimStringV2() + "余:" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0"); ; ;
                }

                if (ToModify.Result_HaveProcess == true)
                {

                    WX_UserChangeLog findcl = db.WX_UserChangeLog.SingleOrDefault(t =>
                              t.aspnet_UserID == GlobalParam.UserKey
                              && t.WX_UserName == ToModify.WX_UserName
                              && t.WX_SourceType == ToModify.WX_SourceType
                              && t.RemarkType == "开奖"
                              && t.GamePeriod == ToModify.GamePeriod
                              && t.BuyValue == ToModify.Buy_Value
                              );
                    if (findcl != null)
                    {
                        db.WX_UserChangeLog.DeleteOnSubmit(findcl);
                    }
                    ToModify.Result_HaveProcess = false;
                }
                ToModify.Buy_Point -= Convert.ToDecimal(Str_BuyPoint);
                ToModify.Buy_Ratio = CheckRatioConfig == null ? 0 : CheckRatioConfig.BasicRatio;

                Linq.WX_UserChangeLog cl = null;
                cl = new WX_UserChangeLog();
                cl.aspnet_UserID = GlobalParam.UserKey;
                cl.WX_UserName = ToModify.WX_UserName;
                cl.WX_SourceType = ToModify.WX_SourceType;
                cl.ChangePoint = Convert.ToDecimal(Str_BuyPoint);
                cl.NeedNotice = false;
                cl.HaveNotice = false;
                cl.ChangeTime = reply.ReceiveTime;
                cl.RemarkType = "取消";
                cl.Remark = "取消@#" + reply.ReceiveContent;
                cl.FinalStatus = false;
                cl.BuyValue = FirstIndex;
                cl.GamePeriod = ToModify.GamePeriod;
                cl.GameLocalPeriod = ToModify.GameLocalPeriod;
                cl.ChangeLocalDay = ToModify.GameLocalPeriod.Substring(0, 8);
                cl.FinalStatus = true;

                cl.BuyValue = ToModify.Buy_Value;
                cl.GamePeriod = ToModify.GamePeriod;

                db.WX_UserChangeLog.InsertOnSubmit(cl);
                db.SubmitChanges();






                #endregion



                TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                    && t.WX_SourceType == reply.WX_SourceType
                    && t.WX_UserName == reply.WX_UserName
                    && t.Buy_Point != 0
                    && t.Result_HaveProcess != true
                    ).ToList(), MemberSource);

                return tr.ToSlimStringV2() + "余:" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

            }//X+数字
            #endregion








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

            if (NewContent == "")
            {
                return true;
            }
            else
            {
                return false;
            }




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
                public TotalResultRow(string P_ShowPeriod,
                    string P_GameResult,
                    string P_BuyVale,
                decimal? P_BuyPoint,
                     decimal? P_ResultPoint,
                       decimal? P_MiddleRatio,
                              decimal? P_OKRatio,
                      decimal? P_BasicRatio,
                    Boolean? P_ResultProcess
                    )
                {
                    ShowPeriod = P_ShowPeriod;
                    GameResult = P_GameResult;
                    BuyValue = P_BuyVale;
                    BuyPoint = P_BuyPoint;
                    ResultPoint = P_ResultPoint;
                    MiddleRatio = P_MiddleRatio;
                    OKRatio = P_OKRatio;
                    ResultProcess = P_ResultProcess;
                    BasicRatio = P_BasicRatio;
                }
                public string ShowPeriod = "";

                public string GameResult = "";


                public string BuyValue = "";
                public decimal? BuyPoint = 0;
                public decimal? ResultPoint = 0;

                public decimal? MiddleRatio = 0;
                public decimal? OKRatio = 0;
                public decimal? BasicRatio = 0;
                public bool? ResultProcess = false;

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
                        Result += (buyitem.BuyValue + buyitem.BuyPoint.Value.ToString("N0"));

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

                    Result += Perioditem + "期：";
                    foreach (var buyitem in Buys.Where(t => t.ShowPeriod == Perioditem))
                    {
                        Result += buyitem.BuyValue + ObjectToString(buyitem.BuyPoint, "N0") + "，";
                    }
                    if (Result.EndsWith("，"))
                    {
                        Result = Result.Substring(0, Result.Length - 1);
                    }

                }
                Result += Environment.NewLine + Environment.NewLine;
                return Result;



            }

            public override string ToString()
            {
                string Result = "余:" + (Remainder.HasValue ? Remainder.Value.ToString("N0") : "") + Environment.NewLine
                      + "用户：" + UserNickName + Environment.NewLine;
                if (Buys.Count() == 0)
                {
                    return "";
                }


                foreach (var buyitem in Buys)
                {
                    Result += "期号: " + buyitem.ShowPeriod;
                    Result += buyitem.BuyValue + ObjectToString(buyitem.BuyPoint);
                }
                Result += Environment.NewLine;


                return Result;
            }

            public string ToOpenString()
            {

                string Result = "余:" + (Remainder.HasValue ? Remainder.Value.ToString("N0") : "") + Environment.NewLine
                 ;
                if (Buys.Count() == 0)
                {
                    return "";
                }
                var Periods = Buys.Select(t => t.ShowPeriod).Distinct();

                foreach (var buyitem in Buys)
                {
                    Result += "期号: " + buyitem.ShowPeriod;
                    Result += " " + buyitem.GameResult + Environment.NewLine;

                    Result += " " + buyitem.BuyValue
                        + ObjectToString(buyitem.BuyPoint, "N0")
                        + "赔率" + ObjectToString(buyitem.BasicRatio, "N0") + Environment.NewLine
                        + "变动" + ObjectToString(buyitem.ResultPoint, "N0")
                        + " " + (buyitem.ResultProcess == true ? "已兑" : "未兑")
                    + Environment.NewLine
                          + "和返" + ObjectToString(buyitem.MiddleRatio * 100, "N0") + "%"
                           + "合返" + ObjectToString(buyitem.OKRatio * 100, "N0") + "%";

                    Result += Environment.NewLine;
                }

                Result += Environment.NewLine;


                return Result;



            }

            public string ToOpenStringV2()
            {

                string Result = "余:" + (Remainder.HasValue ? Remainder.Value.ToString("N0") : "") + Environment.NewLine
                ;
                if (Buys.Count() == 0)
                {
                    return "";
                }
                var Periods = Buys.Select(t => new { t.ShowPeriod, t.GameResult }).Distinct();

                foreach (var period in Periods)
                {

                    Result += "期号: " + period.ShowPeriod;
                    Result += " " + period.GameResult + Environment.NewLine + "本期";
                    var subbuys = Buys.Where(t => t.ShowPeriod == period.ShowPeriod);
                    string ResultOpen = "";
                    foreach (var subnuyitem in subbuys)
                    {

                        Result += subnuyitem.BuyValue + ObjectToString(subnuyitem.BuyPoint, "N0") + ",";
                        ResultOpen += subnuyitem.BuyValue + ObjectToString(subnuyitem.ResultPoint, "N0") + ",";
                    }
                    Result += Environment.NewLine + "结果";
                    Result += ResultOpen + Environment.NewLine;
                }

                Result += Environment.NewLine + Environment.NewLine;


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
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            TotalResult r = new TotalResult();

            foreach (WX_UserGameLog item in logs)
            {
                if (item.Buy_Point == 0)
                {
                    continue;
                }
                DataRow usrw = MemberSource.Select("User_ContactID='" + item.WX_UserName + "' and User_SourceType='" + item.WX_SourceType + "'")[0];
                r.UserNickName = usrw.Field<string>("User_Contact");
                decimal? Remainder = WXUserChangeLog_GetRemainder(item.WX_UserName, item.WX_SourceType);
                r.Remainder = Remainder;
                string SubPeriod = "";
                SubPeriod = item.GamePeriod.Length <= 3 ? item.GamePeriod : item.GamePeriod.Substring(8, 3);

                // string SubPeriod = item.GamePeriod;


                r.Buys.Add(new TotalResult.TotalResultRow(SubPeriod, item.GameResult + item.Gr_BigSmall + item.Gr_SingleDouble + item.Gr_DragonTiger, item.Buy_Value, item.Buy_Point, item.Result_Point, item.BounsRatio_WhenMiddle, item.BounsRatio_WhenOK, item.Buy_Ratio, item.Result_HaveProcess));

            }
            return r;
        }


        public static void ComboStringInit()
        {

            #region
            if (ComboString == null)
            {
                ComboString = new Dictionary<string, string>();
                ComboString.Add("大龙单", "大单龙");
                ComboString.Add("大虎单", "大单虎");
                ComboString.Add("大合单", "大单合");
                ComboString.Add("大龙双", "大双龙");
                ComboString.Add("大虎双", "大双虎");
                ComboString.Add("大合双", "大双合");
                ComboString.Add("大单龙", "大单龙");
                ComboString.Add("大双龙", "大双龙");
                ComboString.Add("大单虎", "大单虎");
                ComboString.Add("大双虎", "大双虎");
                ComboString.Add("大单合", "大单合");
                ComboString.Add("大双合", "大双合");
                ComboString.Add("小龙单", "小单龙");
                ComboString.Add("小虎单", "小单虎");
                ComboString.Add("小合单", "小单合");
                ComboString.Add("小龙双", "小双龙");
                ComboString.Add("小虎双", "小双虎");
                ComboString.Add("小合双", "小双合");
                ComboString.Add("小单龙", "小单龙");
                ComboString.Add("小双龙", "小双龙");
                ComboString.Add("小单虎", "小单虎");
                ComboString.Add("小双虎", "小双虎");
                ComboString.Add("小单合", "小单合");
                ComboString.Add("小双合", "小双合");
                ComboString.Add("和龙单", "和单龙");
                ComboString.Add("和虎单", "和单虎");
                ComboString.Add("和合单", "和单合");
                ComboString.Add("和单龙", "和单龙");
                ComboString.Add("和单虎", "和单虎");
                ComboString.Add("和单合", "和单合");
                ComboString.Add("单龙大", "大单龙");
                ComboString.Add("单虎大", "大单虎");
                ComboString.Add("单合大", "大单合");
                ComboString.Add("单龙小", "小单龙");
                ComboString.Add("单虎小", "小单虎");
                ComboString.Add("单合小", "小单合");
                ComboString.Add("单龙和", "和单龙");
                ComboString.Add("单虎和", "和单虎");
                ComboString.Add("单合和", "和单合");
                ComboString.Add("单大龙", "大单龙");
                ComboString.Add("单小龙", "小单龙");
                ComboString.Add("单和龙", "和单龙");
                ComboString.Add("单大虎", "大单虎");
                ComboString.Add("单小虎", "小单虎");
                ComboString.Add("单和虎", "和单虎");
                ComboString.Add("单大合", "大单合");
                ComboString.Add("单小合", "小单合");
                ComboString.Add("单和合", "和单合");
                ComboString.Add("双龙大", "大双龙");
                ComboString.Add("双虎大", "大双虎");
                ComboString.Add("双合大", "大双合");
                ComboString.Add("双龙小", "小双龙");
                ComboString.Add("双虎小", "小双虎");
                ComboString.Add("双合小", "小双合");
                ComboString.Add("双大龙", "大双龙");
                ComboString.Add("双小龙", "小双龙");
                ComboString.Add("双大虎", "大双虎");
                ComboString.Add("双小虎", "小双虎");
                ComboString.Add("双大合", "大双合");
                ComboString.Add("双小合", "小双合");
                ComboString.Add("龙单大", "大单龙");
                ComboString.Add("龙双大", "大双龙");
                ComboString.Add("龙单小", "小单龙");
                ComboString.Add("龙双小", "小双龙");
                ComboString.Add("龙单和", "和单龙");
                ComboString.Add("龙大单", "大单龙");
                ComboString.Add("龙小单", "小单龙");
                ComboString.Add("龙和单", "和单龙");
                ComboString.Add("龙大双", "大双龙");
                ComboString.Add("龙小双", "小双龙");
                ComboString.Add("虎单大", "大单虎");
                ComboString.Add("虎双大", "大双虎");
                ComboString.Add("虎单小", "小单虎");
                ComboString.Add("虎双小", "小双虎");
                ComboString.Add("虎单和", "和单虎");
                ComboString.Add("虎大单", "大单虎");
                ComboString.Add("虎小单", "小单虎");
                ComboString.Add("虎和单", "和单虎");
                ComboString.Add("虎大双", "大双虎");
                ComboString.Add("虎小双", "小双虎");
                ComboString.Add("合单大", "大单合");
                ComboString.Add("合双大", "大双合");
                ComboString.Add("合单小", "小单合");
                ComboString.Add("合双小", "小双合");
                ComboString.Add("合单和", "和单合");
                ComboString.Add("合大单", "大单合");
                ComboString.Add("合小单", "小单合");
                ComboString.Add("合和单", "和单合");
                ComboString.Add("合大双", "大双合");
                ComboString.Add("合小双", "小双合");
                ComboString.Add("大单", "大单");
                ComboString.Add("大双", "大双");
                ComboString.Add("小单", "小单");
                ComboString.Add("小双", "小双");
                ComboString.Add("和单", "和单");
                ComboString.Add("单大", "大单");
                ComboString.Add("单小", "小单");
                ComboString.Add("单和", "和单");
                ComboString.Add("双大", "大双");
                ComboString.Add("双小", "小双");
                ComboString.Add("大龙", "大龙");
                ComboString.Add("大虎", "大虎");
                ComboString.Add("大合", "大合");
                ComboString.Add("小龙", "小龙");
                ComboString.Add("小虎", "小虎");
                ComboString.Add("小合", "小合");
                ComboString.Add("和龙", "和龙");
                ComboString.Add("和虎", "和虎");
                ComboString.Add("和合", "和合");
                ComboString.Add("龙大", "大龙");
                ComboString.Add("龙小", "小龙");
                ComboString.Add("龙和", "和龙");
                ComboString.Add("虎大", "大虎");
                ComboString.Add("虎小", "小虎");
                ComboString.Add("虎和", "和虎");
                ComboString.Add("合大", "大合");
                ComboString.Add("合小", "小合");
                ComboString.Add("合和", "和合");
                ComboString.Add("单龙", "单龙");
                ComboString.Add("单虎", "单虎");
                ComboString.Add("单合", "单合");
                ComboString.Add("双龙", "双龙");
                ComboString.Add("双虎", "双虎");
                ComboString.Add("双合", "双合");
                ComboString.Add("龙单", "单龙");
                ComboString.Add("龙双", "双龙");
                ComboString.Add("虎单", "单虎");
                ComboString.Add("虎双", "双虎");
                ComboString.Add("合单", "单合");
                ComboString.Add("合双", "双合");

            }
            #endregion
        }



        /// <summary>
        /// 添加消息后转化下单记录或取消记录
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        /// 

        //public static IQueryable<Linq.Game_FootBall_VS> GameMatches
        //{
        //    get {
        //        BindingList<Linq.Game_FootBall_VS> result = new BindingList<Game_FootBall_VS>();
        //        Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        //        db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

        //        var source= db.Game_FootBall_VS.Where(t => t.aspnet_UserID == GlobalParam.Key && t.TmpJobid == GlobalParam.JobID);


        //        return source;
        //    }
        //}

        //public static BindingList<c_vstype> GameMatcheClasss
        //{
        //    get {
        //        var source = (from ds in GameMatches
        //                      select new { ds.GameType, ds.MatchClass }).Distinct();
        //        BindingList<c_vstype> r = new BindingList<c_vstype>();
        //        foreach (var item in source)
        //        {
        //            c_vstype newr = new c_vstype(item.GameType, item.MatchClass);
        //            r.Add(newr);
        //        }
        //        return r;
        //} }

        public enum GameMode { 时时彩, 球赛, 六合彩 }

        public static string WX_UserReplyLog_Create(WX_UserReplyLog reply, DataTable MemberSource, GameMode gm, bool adminmode = false)
        {

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");


            WX_UserReply testr = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == reply.aspnet_UserID && t.WX_UserName == reply.WX_UserName && t.WX_SourceType == reply.WX_SourceType);
            if (testr.IsReply == false)
            {
                return "";
            }


            if (reply.ReceiveContent != "")
            {
                #region "时间转化期数"
                string Minutes = reply.ReceiveTime.ToString("HH:mm");
                string NextPeriod = "";
                string NextLocalPeriod = "";
                var NextMonutes = db.Game_ChongqingshishicaiPeriodMinute.Where(t => string.Compare(t.TimeMinute, Minutes) >= (adminmode == false ? 1 : 0)).OrderBy(t => t.PeriodIndex);
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


                #region 寻找队伍
                Linq.ProgramLogic.FormatResultState BallState = Linq.ProgramLogic.FormatResultState.Initialize;
                Linq.ProgramLogic.FormatResultType BallType = Linq.ProgramLogic.FormatResultType.Initialize;
                string BuyType = "";
                string BuyMoney = "";
                string[] q_Teams = new string[] { };
                Linq.Game_FootBall_VS[] AllTeams = (Linq.Game_FootBall_VS[])Linq.ProgramLogic.ReceiveContentFormat(reply.ReceiveContent, out BallState, out BallType, Linq.ProgramLogic.FormatResultDirection.MemoryMatchList, out BuyType, out BuyMoney, out q_Teams);



                #endregion
                if (reply.ReceiveContent == "查" && adminmode == true && gm == GameMode.时时彩)
                {
                    DateTime TestPeriod = DateTime.Now;
                    if (TestPeriod.Hour <= 9)
                    {
                        TestPeriod = TestPeriod.AddDays(-1);
                    }

                    var TodayBuys = db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                        && t.WX_UserName == reply.WX_UserName
                        && t.WX_SourceType == reply.WX_SourceType
                        && t.GameLocalPeriod.StartsWith(TestPeriod.ToString("yyyyMMdd"))
                        && t.Buy_Point != 0
                        );
                    decimal? TotalPeriodCount = TodayBuys.Select(t => t.GamePeriod).Distinct().Count();
                    decimal? TotalBuy = TodayBuys.Sum(t => t.Buy_Point);
                    TotalBuy = (TotalBuy == null ? 0 : TotalBuy.Value);
                    decimal? TotalResult = TodayBuys.Sum(t => t.Result_Point);
                    TotalResult = (TotalResult == null ? 0 : TotalResult.Value);
                    string Result = "期数是" + ObjectToString(TotalPeriodCount, "N0")
                    + "期，总额是" + ObjectToString(TotalBuy, "N0")
                    + "平均" + ObjectToString((TotalPeriodCount == 0 ? 0 : TotalBuy / TotalPeriodCount), "N2")
                    + ",得分总额是" + ObjectToString(TotalResult, "N0")
                    + ",结果是" + ObjectToString(TotalResult - TotalBuy, "N0");

                    return Result;
                }
                //全取消
                else if (reply.ReceiveContent == "取消")
                {
                    Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == reply.ReceiveTime.ToString("HH:mm"));
                    Linq.aspnet_UsersNewGameResultSend myset = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey);
                    if (myset.IsBlock == true)
                    {
                        return "封盘";
                    }
                    if (testmin != null)
                    {
                        return "整点" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                    }


                    var ToCalcel = db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                        && t.WX_UserName == reply.WX_UserName
                        && t.WX_SourceType == reply.WX_SourceType
                        && t.Result_HaveProcess == false
                        && t.Buy_Point != 0
                        && string.Compare(t.GamePeriod, reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod) >= 0

                        );
                    foreach (var cancelitem in ToCalcel)
                    {


                        WX_UserChangeLog cl = new WX_UserChangeLog();
                        cl.aspnet_UserID = GlobalParam.UserKey;
                        cl.WX_UserName = cancelitem.WX_UserName;
                        cl.WX_SourceType = cancelitem.WX_SourceType;
                        cl.ChangePoint = cancelitem.Buy_Point;
                        cl.NeedNotice = false;
                        cl.HaveNotice = false;
                        cl.ChangeTime = reply.ReceiveTime;
                        cl.RemarkType = "取消@#" + cancelitem.Buy_Value;
                        cl.Remark = "取消@#" + reply.ReceiveContent;
                        cl.FinalStatus = false;
                        cl.BuyValue = cancelitem.Buy_Value;
                        cl.GamePeriod = cancelitem.GamePeriod;
                        db.WX_UserChangeLog.InsertOnSubmit(cl);

                        cancelitem.Buy_Point = 0;
                        cancelitem.GP_LastModify = reply.ReceiveTime;
                    }
                    db.SubmitChanges();

                    return "取消成功,余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                }
                else if (reply.ReceiveContent == "取消球赛" && adminmode == true)
                {
                    var ToCalcel = db.WX_UserGameLog_Football.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                      && t.WX_UserName == reply.WX_UserName
                      && t.WX_SourceType == reply.WX_SourceType
                      && t.HaveOpen == false
                      && t.BuyMoney != 0
                      );
                    foreach (var cancelitem in ToCalcel)
                    {


                        WX_UserChangeLog cl = new WX_UserChangeLog();
                        cl.aspnet_UserID = GlobalParam.UserKey;
                        cl.WX_UserName = cancelitem.WX_UserName;
                        cl.WX_SourceType = cancelitem.WX_SourceType;
                        cl.ChangePoint = cancelitem.BuyMoney;
                        cl.NeedNotice = false;
                        cl.HaveNotice = false;
                        cl.ChangeTime = DateTime.Now;
                        cl.RemarkType = "取消@#" + reply.ReceiveContent;
                        cl.Remark = "取消@#" + BallBuyTypeToChinseFrontShow(cancelitem.BuyType) + cancelitem.BuyMoney.ToString() + cancelitem.GameVS; ;
                        cl.FinalStatus = false;
                        cl.BuyValue = cancelitem.BuyType;
                        cl.GamePeriod = cancelitem.GameKey;
                        db.WX_UserChangeLog.InsertOnSubmit(cl);

                        cancelitem.BuyMoney = 0;
                        cancelitem.HaveOpen = true;
                        System.Threading.Thread.Sleep(20);
                    }
                    db.SubmitChanges();

                    return "取消成功,余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");


                }
                else if (reply.ReceiveContent == "余")
                {
                    return "余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");
                }
                else if (reply.ReceiveContent == "开" && gm == GameMode.时时彩)
                {
                    string Result = "";
                    string QueryDate = reply.ReceiveContent.Substring(1);
                    DateTime TestPeriod = DateTime.Now;
                    if (TestPeriod.Hour <= 9)
                    {
                        TestPeriod = TestPeriod.AddDays(-1);
                    }
                    try
                    {
                        TestPeriod = (QueryDate == "" ? TestPeriod : Convert.ToDateTime(QueryDate));
                    }
                    catch (Exception)
                    {
                        return "";

                    }
                    var TodatBuyGameLog = db.WX_UserGameLog.Where(t =>
                        t.aspnet_UserID == GlobalParam.UserKey
                        && t.WX_UserName == reply.WX_UserName
                        && t.WX_SourceType == reply.WX_SourceType
                        && t.Buy_Point != 0
                        && t.GameLocalPeriod.StartsWith(TestPeriod.ToString("yyyyMMdd"))
                        );
                    TotalResult tr = BuildResult(TodatBuyGameLog.ToList(), MemberSource);
                    Result = tr.ToOpenStringV2();

                    return Result;

                }

                else if (reply.ReceiveContent == "未开" && gm == GameMode.时时彩)
                {
                    string Result = "";
                    var TodatBuyGameLog = db.WX_UserGameLog.Where(t =>
                        t.aspnet_UserID == GlobalParam.UserKey
                        && t.WX_UserName == reply.WX_UserName
                        && t.WX_SourceType == reply.WX_SourceType
                        && t.Buy_Point != 0
                        && (t.Result_HaveProcess == false || t.Result_HaveProcess == null)
                        );
                    TotalResult tr = BuildResult(TodatBuyGameLog.ToList(), MemberSource);
                    Result = tr.ToOpenStringV2();

                    return Result;
                }

                else if (reply.ReceiveContent.StartsWith("取消"))
                {
                    #region "检查整点"
                    Linq.aspnet_UsersNewGameResultSend myset = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey);
                    if (myset.IsBlock == true)
                    {
                        return "封盘";
                    }
                    Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == reply.ReceiveTime.ToString("HH:mm"));
                    if (testmin != null)
                    {
                        return "整点" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0"); ;

                    }
                    #endregion
                    if (reply.ReceiveContent.Length > 2)
                    {
                        return WX_UserGameLog_Cancel(db, reply, MemberSource, adminmode);
                    }
                    else
                    {
                        return "";
                    }
                }//取消的单
                #region 全
                else if (reply.ReceiveContent.StartsWith("全") && gm == GameMode.时时彩)
                {
                    Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == reply.ReceiveTime.ToString("HH:mm"));
                    if (testmin != null && adminmode == false)
                    {
                        return "整点" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                    }
                    if (reply.ReceiveContent.Length <= 3)
                    {
                        return "";
                    }
                    string Str_BuyPoint = reply.ReceiveContent.Substring(2);
                    string BuyXnUMBER = reply.ReceiveContent.Substring(1, 1);

                    decimal BuyPoint = 0;
                    try
                    {
                        BuyPoint = Convert.ToDecimal(Str_BuyPoint);
                    }
                    catch (Exception)
                    {
                    }
                    if (BuyPoint == 0)
                    {
                        return "";
                    }
                    if (string.Compare(reply.ReceiveTime.ToString("HH:mm"), "01:55") >= 0 && string.Compare(reply.ReceiveTime.ToString("HH:mm"), "09:00") <= 0)
                    {
                        return "封盘时间";
                    }

                    if (BuyXnUMBER == "零" || BuyXnUMBER == "一" || BuyXnUMBER == "二" || BuyXnUMBER == "三" || BuyXnUMBER == "四" || BuyXnUMBER == "五" || BuyXnUMBER == "六" || BuyXnUMBER == "七" || BuyXnUMBER == "八" || BuyXnUMBER == "九")
                    {

                        if (NetFramework.Util_Math.IsNumber(Str_BuyPoint) == false)
                        {
                            return "";
                        }


                        WX_UserGameLog findupdate = db.WX_UserGameLog.SingleOrDefault(t =>
                             t.aspnet_UserID == GlobalParam.UserKey
                                 && t.WX_UserName == reply.WX_UserName
                                 && t.WX_SourceType == reply.WX_SourceType
                                 && t.GamePeriod == reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod
                                 && t.Buy_Value == reply.ReceiveContent.Substring(0, 2)
                             );

                        #region 检查赔率
                        Decimal CheckBuy = (findupdate == null ? 0 : findupdate.Buy_Point.Value) + Convert.ToDecimal(Str_BuyPoint);

                        var ratios = db.Game_BasicRatio.Where(t =>
                            t.BuyType == "全X"
                          && t.aspnet_UserID == GlobalParam.UserKey
                           && t.MinBuy <= CheckBuy
                          && t.MaxBuy >= CheckBuy
                          );
                        if (ratios.Count() == 0)
                        {
                            Decimal? MaxLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                   && t.BuyType == "全X"

                   ).Max(t => t.MaxBuy);

                            Decimal? MinLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                                && t.BuyType == "全X"
                                    ).Min(t => t.MinBuy);
                            return "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0"); ;

                        }
                        if (ratios.Count() != 5)
                        {
                            return "全X限额范围不一致" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0"); ;
                        }
                        #endregion

                        #region 检查余额
                        decimal Remainder = WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType);
                        if (Remainder < Convert.ToDecimal(Str_BuyPoint))
                        {
                            return "余分不足，余" + ObjectToString(Remainder, "N0");
                        }
                        #endregion
                        WX_UserGameLog newgl = null;
                        if (findupdate == null)
                        {


                            newgl = new WX_UserGameLog();
                            newgl.aspnet_UserID = GlobalParam.UserKey;
                            newgl.Buy_Point = BuyPoint;
                            newgl.Buy_Type = "全X";
                            newgl.Buy_Value = reply.ReceiveContent.Substring(0, 2); ;
                            newgl.Buy_Point = Convert.ToDecimal(Str_BuyPoint);
                            newgl.GameName = "重庆时时彩";
                            newgl.TransTime = reply.ReceiveTime;
                            newgl.WX_UserName = reply.WX_UserName;
                            newgl.WX_SourceType = reply.WX_SourceType;

                            newgl.Buy_Ratio_Full1 = ratios.SingleOrDefault(t => t.BuyValue == "连1个").BasicRatio;
                            newgl.Buy_Ratio_Full2 = ratios.SingleOrDefault(t => t.BuyValue == "连2个").BasicRatio;
                            newgl.Buy_Ratio_Full3 = ratios.SingleOrDefault(t => t.BuyValue == "连3个").BasicRatio;
                            newgl.Buy_Ratio_Full4 = ratios.SingleOrDefault(t => t.BuyValue == "连4个").BasicRatio;
                            newgl.Buy_Ratio_Full5 = ratios.SingleOrDefault(t => t.BuyValue == "连5个").BasicRatio;


                            newgl.BounsRatio_WhenMiddle = 0;
                            newgl.BounsRatio_WhenOK = 0;

                            newgl.Result_HaveProcess = false;
                            newgl.GamePeriod = reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod;

                            string LocalDay = (reply.ReceiveTime.Hour <= 2 ? reply.ReceiveTime.AddDays(-1).ToString("yyyyMMdd") : reply.ReceiveTime.ToString("yyyyMMdd"));
                            newgl.GameLocalPeriod = LocalDay + NextLocalPeriod;
                            db.WX_UserGameLog.InsertOnSubmit(newgl);
                        }
                        else
                        {
                            if (findupdate.Result_HaveProcess == true)
                            {

                                WX_UserChangeLog findcl = db.WX_UserChangeLog.SingleOrDefault(t =>
                              t.aspnet_UserID == GlobalParam.UserKey
                              && t.WX_UserName == findupdate.WX_UserName
                              && t.WX_SourceType == findupdate.WX_SourceType
                              && t.RemarkType == "开奖"
                              && t.GamePeriod == findupdate.GamePeriod
                              && t.BuyValue == findupdate.Buy_Value
                              );
                                if (findcl != null)
                                {
                                    db.WX_UserChangeLog.DeleteOnSubmit(findcl);
                                }
                                findupdate.Result_HaveProcess = false;
                            }

                            findupdate.Buy_Point += Convert.ToDecimal(Str_BuyPoint);
                            findupdate.Buy_Ratio_Full1 = ratios.SingleOrDefault(t => t.BuyValue == "连1个").BasicRatio;
                            findupdate.Buy_Ratio_Full2 = ratios.SingleOrDefault(t => t.BuyValue == "连2个").BasicRatio;
                            findupdate.Buy_Ratio_Full3 = ratios.SingleOrDefault(t => t.BuyValue == "连3个").BasicRatio;
                            findupdate.Buy_Ratio_Full4 = ratios.SingleOrDefault(t => t.BuyValue == "连4个").BasicRatio;
                            findupdate.Buy_Ratio_Full5 = ratios.SingleOrDefault(t => t.BuyValue == "连5个").BasicRatio;

                        }

                        Linq.WX_UserChangeLog cl = null;

                        cl = new WX_UserChangeLog();
                        cl.aspnet_UserID = GlobalParam.UserKey;
                        cl.WX_UserName = reply.WX_UserName;
                        cl.WX_SourceType = reply.WX_SourceType;
                        cl.ChangePoint = -Convert.ToDecimal(Str_BuyPoint); ;
                        cl.NeedNotice = false;
                        cl.HaveNotice = false;
                        cl.ChangeTime = reply.ReceiveTime;
                        cl.RemarkType = "下单";
                        cl.Remark = "下单@#" + reply.ReceiveContent;
                        cl.FinalStatus = false;
                        cl.BuyValue = (findupdate == null ? newgl.Buy_Value : findupdate.Buy_Value);
                        cl.GamePeriod = (findupdate == null ? newgl.GamePeriod : findupdate.GamePeriod);
                        cl.GameLocalPeriod = (findupdate == null ? newgl.GameLocalPeriod : findupdate.GameLocalPeriod);
                        cl.ChangeLocalDay = (findupdate == null ? newgl.GameLocalPeriod : findupdate.GameLocalPeriod).Substring(0, 8);
                        cl.FinalStatus = true;
                        db.WX_UserChangeLog.InsertOnSubmit(cl);
                        try
                        {
                            db.SubmitChanges();

                            TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == reply.WX_UserName && t.WX_SourceType == reply.WX_SourceType
                                && t.Result_HaveProcess == false
                                && t.Buy_Point != 0).ToList(), MemberSource);
                            return tr.ToSlimStringV2() + "余:" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                        }
                        catch (Exception AnyError)
                        {

                            return AnyError.Message + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");
                        }



                    }
                    else
                    {
                        return "";
                    }


                }//全
                #endregion
                #region 定数字或定大小
                else if ((reply.ReceiveContent.StartsWith("个") ||
                    reply.ReceiveContent.StartsWith("十") ||
                    reply.ReceiveContent.StartsWith("百") ||
                    reply.ReceiveContent.StartsWith("千") ||
                    reply.ReceiveContent.StartsWith("万")
                    ) && gm == GameMode.时时彩)
                {
                    Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == reply.ReceiveTime.ToString("HH:mm"));
                    if (testmin != null && adminmode == false)
                    {
                        return "整点" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                    }
                    if (reply.ReceiveContent.Length < 3)
                    {
                        return "";
                    }
                    string BuyXnUMBER = reply.ReceiveContent.Substring(1, 1);
                    string Str_BuyPoint = reply.ReceiveContent.Substring(2);
                    decimal BuyPoint = 0;
                    try
                    {
                        BuyPoint = Convert.ToDecimal(Str_BuyPoint);
                    }
                    catch (Exception)
                    {
                    }
                    if (BuyPoint == 0)
                    {
                        return "";
                    }
                    if (string.Compare(reply.ReceiveTime.ToString("HH:mm"), "01:55") >= 0 && string.Compare(reply.ReceiveTime.ToString("HH:mm"), "09:00") <= 0)
                    {
                        return "封盘时间";
                    }


                    if (BuyXnUMBER == "零" || BuyXnUMBER == "一" || BuyXnUMBER == "二" || BuyXnUMBER == "三" || BuyXnUMBER == "四" || BuyXnUMBER == "五" || BuyXnUMBER == "六" || BuyXnUMBER == "七" || BuyXnUMBER == "八" || BuyXnUMBER == "九"
                        || BuyXnUMBER == "单" || BuyXnUMBER == "双" || BuyXnUMBER == "大" || BuyXnUMBER == "小"
                        )
                    {

                        if (NetFramework.Util_Math.IsNumber(Str_BuyPoint) == false)
                        {
                            return "";
                        }



                        WX_UserGameLog findupdate = db.WX_UserGameLog.SingleOrDefault(t =>
                                                    t.aspnet_UserID == GlobalParam.UserKey
                                                        && t.WX_UserName == reply.WX_UserName
                                                        && t.WX_SourceType == reply.WX_SourceType
                                                        && t.GamePeriod == reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod
                                                        && t.Buy_Value == reply.ReceiveContent.Substring(0, 2)
                                                    );

                        #region 检查赔率
                        string NewBuyXNumber = "";
                        switch (BuyXnUMBER)
                        {
                            case "零":
                                NewBuyXNumber = "0";
                                break;
                            case "一":
                                NewBuyXNumber = "1";
                                break;
                            case "二":
                                NewBuyXNumber = "2";
                                break;
                            case "三":
                                NewBuyXNumber = "3";
                                break;
                            case "四":
                                NewBuyXNumber = "4";
                                break;
                            case "五":
                                NewBuyXNumber = "5";
                                break;
                            case "六":
                                NewBuyXNumber = "6";
                                break;
                            case "七":
                                NewBuyXNumber = "7";
                                break;
                            case "八":
                                NewBuyXNumber = "8";
                                break;
                            case "九":
                                NewBuyXNumber = "9";
                                break;
                            default:
                                break;
                        }
                        Boolean BuyXnUMBERIsNumber = NetFramework.Util_Math.IsNumber(NewBuyXNumber);

                        Decimal CheckBuy = (findupdate == null ? 0 : findupdate.Buy_Point.Value) + Convert.ToDecimal(Str_BuyPoint);

                        var ratios = db.Game_BasicRatio.SingleOrDefault(t => t.BuyType == "定X" && t.aspnet_UserID == GlobalParam.UserKey
                           && (t.BuyValue == (BuyXnUMBERIsNumber == true ? "数字" : BuyXnUMBER))

                            && t.MinBuy <= CheckBuy
                            && t.MaxBuy >= CheckBuy
                            );
                        if (ratios == null)
                        {
                            Decimal? MaxLimit = db.Game_BasicRatio.Where(t =>
                                t.aspnet_UserID == GlobalParam.UserKey
                   && t.BuyType == "定X"
                      && (t.BuyValue == (BuyXnUMBERIsNumber == true ? "数字" : BuyXnUMBER))

                   ).Max(t => t.MaxBuy);
                            Decimal? MinLimit = db.Game_BasicRatio.Where(t =>
                                t.aspnet_UserID == GlobalParam.UserKey
                                && t.BuyType == "定X"
                                   && (t.BuyValue == (BuyXnUMBERIsNumber == true ? "数字" : BuyXnUMBER))

                                ).Min(t => t.MinBuy);
                            return "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                        }
                        #endregion

                        #region 检查余额
                        decimal Remainder = WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType);
                        if (Remainder < Convert.ToDecimal(Str_BuyPoint))
                        {
                            return "余分不足，余" + ObjectToString(Remainder, "N0");
                        }
                        #endregion


                        WX_UserGameLog newgl = null;
                        if (findupdate == null)
                        {


                            newgl = new WX_UserGameLog();
                            newgl.aspnet_UserID = GlobalParam.UserKey;
                            newgl.Buy_Point = BuyPoint;
                            newgl.Buy_Type = "定X";
                            newgl.Buy_Value = reply.ReceiveContent.Substring(0, 2); ;
                            newgl.Buy_Point = Convert.ToDecimal(Str_BuyPoint);
                            newgl.GameName = "重庆时时彩";
                            newgl.TransTime = reply.ReceiveTime;
                            newgl.WX_UserName = reply.WX_UserName;
                            newgl.WX_SourceType = reply.WX_SourceType;
                            newgl.Buy_Ratio = ratios.BasicRatio;

                            newgl.BounsRatio_WhenMiddle = 0;
                            newgl.BounsRatio_WhenOK = 0;

                            newgl.Result_HaveProcess = false;
                            newgl.GamePeriod = reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod;

                            string LocalDay = (reply.ReceiveTime.Hour <= 2 ? reply.ReceiveTime.AddDays(-1).ToString("yyyyMMdd") : reply.ReceiveTime.ToString("yyyyMMdd"));
                            newgl.GameLocalPeriod = LocalDay + NextLocalPeriod;
                            db.WX_UserGameLog.InsertOnSubmit(newgl);
                        }
                        else
                        {
                            if (findupdate.Result_HaveProcess == true)
                            {

                                WX_UserChangeLog findcl = db.WX_UserChangeLog.SingleOrDefault(t =>
                               t.aspnet_UserID == GlobalParam.UserKey
                               && t.WX_UserName == findupdate.WX_UserName
                               && t.WX_SourceType == findupdate.WX_SourceType
                               && t.RemarkType == "开奖"
                               && t.GamePeriod == findupdate.GamePeriod
                               && t.BuyValue == findupdate.Buy_Value
                               );
                                if (findcl != null)
                                {
                                    db.WX_UserChangeLog.DeleteOnSubmit(findcl);
                                }
                                findupdate.Result_HaveProcess = false;
                            }
                            findupdate.Buy_Point += BuyPoint;
                            findupdate.Buy_Ratio = ratios == null ? 0 : ratios.BasicRatio;

                        }

                        Linq.WX_UserChangeLog cl = null;

                        cl = new WX_UserChangeLog();
                        cl.aspnet_UserID = GlobalParam.UserKey;
                        cl.WX_UserName = reply.WX_UserName;
                        cl.WX_SourceType = reply.WX_SourceType;
                        cl.ChangePoint = -Convert.ToDecimal(Str_BuyPoint);
                        cl.NeedNotice = false;
                        cl.HaveNotice = false;
                        cl.ChangeTime = reply.ReceiveTime;
                        cl.RemarkType = "下单";
                        cl.Remark = "下单@#" + reply.ReceiveContent;
                        cl.FinalStatus = false;
                        cl.BuyValue = findupdate == null ? newgl.Buy_Value : findupdate.Buy_Value;
                        cl.GamePeriod = findupdate == null ? newgl.GamePeriod : findupdate.GamePeriod;
                        cl.GameLocalPeriod = (findupdate == null ? newgl.GameLocalPeriod : findupdate.GameLocalPeriod);
                        cl.ChangeLocalDay = (findupdate == null ? newgl.GameLocalPeriod : findupdate.GameLocalPeriod).Substring(0, 8);

                        cl.FinalStatus = true;
                        db.WX_UserChangeLog.InsertOnSubmit(cl);
                        try
                        {
                            db.SubmitChanges();

                            TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                                && t.WX_UserName == reply.WX_UserName
                                && t.WX_SourceType == reply.WX_SourceType
                                && t.Result_HaveProcess == false
                                && t.Buy_Point != 0).ToList(), MemberSource);
                            return tr.ToSlimStringV2() + "余:" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                        }
                        catch (Exception AnyError)
                        {

                            return AnyError.Message + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");
                        }



                    }
                    else
                    {
                        return "";
                    }

                }//定数字或定大小
                #endregion

                //足球篮球类下单
                else if (BallType == FormatResultType.OrderModify && BallState == FormatResultState.SingleSuccess && gm == GameMode.时时彩)
                {
                    Int32 success = -1;


                    WX_UserGameLog_Football[] fb = ContentToGameLogBall(reply, AllTeams, BuyType, BuyMoney, out success, FormatResultDirection.MemoryMatchList, db);
                    if (success == 1 && fb.Count() == 1)
                    {
                        #region 检查开赛
                        Linq.Game_ResultFootBall_Last lst = db.Game_ResultFootBall_Last.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                            && t.GameKey == fb[0].GameKey
                            );
                        if (lst != null && lst.EndState.ToUpper() == "完")
                        {
                            return fb[0].GameVS + "已完赛";
                        }

                        #endregion


                        #region 检查余额
                        decimal Remainder = WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType);
                        if (Remainder < Convert.ToDecimal(fb[0].BuyMoney))
                        {
                            return "余分不足，余" + ObjectToString(Remainder, "N0");
                        }
                        #endregion

                        WX_UserChangeLog cl = new WX_UserChangeLog();
                        cl.aspnet_UserID = GlobalParam.UserKey;
                        cl.WX_SourceType = reply.WX_SourceType;
                        cl.WX_UserName = reply.WX_UserName;
                        cl.Remark = reply.ReceiveContent;
                        cl.HaveNotice = true;
                        cl.NeedNotice = false;
                        cl.FinalStatus = true;
                        cl.ChangePoint = -fb[0].BuyMoney;
                        cl.BuyValue = fb[0].BuyType;
                        cl.ChangeTime = DateTime.Now;
                        cl.GamePeriod = fb[0].GameKey;
                        cl.GameLocalPeriod = fb[0].GameVS;
                        cl.RemarkType = "球赛下单";



                        WX_UserGameLog_Football findexists = db.WX_UserGameLog_Football.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                            && t.WX_UserName == reply.WX_UserName
                            && t.WX_SourceType == reply.WX_SourceType
                            && t.GameKey == fb[0].GameKey
                            && t.BuyType == fb[0].BuyType
                            );


                        db.WX_UserChangeLog.InsertOnSubmit(cl);

                        if (findexists == null)
                        {
                            db.WX_UserGameLog_Football.InsertOnSubmit(fb[0]);
                        }
                        else
                        {
                            findexists.BuyMoney += fb[0].BuyMoney;
                            fb[0].HaveOpen = false;
                        }

                        db.SubmitChanges();

                        Remainder = WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType);


                        string rtsfb = GetUserUpOpenBallGame(db, reply.WX_UserName, reply.WX_SourceType);

                        return rtsfb + ",余" + Remainder.ToString("N0");

                    }
                    else if (success == 0 && fb.Count() == 1)
                    {
                        return "赛事没波胆";
                    }
                    else if (fb.Count() > 0)
                    {

                        string returnstr = "";
                        foreach (var fbitem in fb)
                        {
                            returnstr += fbitem.A_Team + "VS" + fbitem.B_Team;
                        }
                        return "赛事有多个" + returnstr;
                    }
                    else
                    {
                        return "";
                    }



                }

                else if (gm == GameMode.六合彩)
                {


                    return "";
                }

                else if (gm == GameMode.时时彩)
                {


                    string FirstIndex = reply.ReceiveContent.Substring(0, 1);
                    string Str_BuyPoint = reply.ReceiveContent.Substring(1);
                    decimal BuyPoint = 0;




                    if (NetFramework.Util_Math.IsNumber(Str_BuyPoint) == false)
                    {
                        #region 组合类
                        if (reply.ReceiveContent.Length >= 3)
                        {
                            string BuyType2 = reply.ReceiveContent.Substring(0, 2);
                            string StrBuyPoint2 = reply.ReceiveContent.Substring(2);

                            if (NetFramework.Util_Math.IsNumber(StrBuyPoint2) == false)
                            {
                                string BuyType3 = reply.ReceiveContent.Substring(0, 3);
                                string StrBuyPoint3 = reply.ReceiveContent.Substring(3);
                                if (reply.ReceiveContent.Length >= 4)
                                {
                                    if (NetFramework.Util_Math.IsNumber(StrBuyPoint3) == false)
                                    {
                                        return "";
                                    }//3位后不是数字
                                    else
                                    {

                                        if (ComboString.ContainsKey(BuyType3))
                                        {
                                            Linq.aspnet_UsersNewGameResultSend myset = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey);
                                            if (myset.IsBlock == true)
                                            {
                                                return "封盘";
                                            }
                                            Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == reply.ReceiveTime.ToString("HH:mm"));
                                            if (testmin != null && adminmode == false)
                                            {
                                                return "整点" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                                            }
                                            string KeyValue3 = "";
                                            ComboString.TryGetValue(BuyType3, out KeyValue3);
                                            WX_UserGameLog findupdate3 = db.WX_UserGameLog.SingleOrDefault(t =>
                                                t.aspnet_UserID == GlobalParam.UserKey
                                                && t.WX_UserName == reply.WX_UserName
                                                && t.WX_SourceType == reply.WX_SourceType
                                                && t.Buy_Value == KeyValue3
                                                 && t.GamePeriod == reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod
                                                );

                                            #region 检查赔率

                                            Decimal CheckBuy = (findupdate3 == null ? 0 : findupdate3.Buy_Point.Value) + Convert.ToDecimal(StrBuyPoint3);

                                            var ratios = db.Game_BasicRatio.SingleOrDefault(t =>
                                                t.BuyType == "组合"
                                                && t.aspnet_UserID == GlobalParam.UserKey
                                               && (t.BuyValue == KeyValue3)

                                                && t.MinBuy <= CheckBuy
                                                && t.MaxBuy >= CheckBuy
                                                );
                                            if (ratios == null)
                                            {
                                                Decimal? MaxLimit = db.Game_BasicRatio.Where(t =>
                                                    t.aspnet_UserID == GlobalParam.UserKey
                                       && t.BuyType == "组合"
                                          && (t.BuyValue == KeyValue3)

                                       ).Max(t => t.MaxBuy);
                                                Decimal? MinLimit = db.Game_BasicRatio.Where(t =>
                                                    t.aspnet_UserID == GlobalParam.UserKey
                                                   && t.BuyType == "组合"
                                          && (t.BuyValue == KeyValue3)
                                                    ).Min(t => t.MinBuy);
                                                return "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                                            }
                                            #endregion

                                            #region 检查余额
                                            decimal Remainder = WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType);
                                            if (Remainder < Convert.ToDecimal(StrBuyPoint3))
                                            {
                                                return "余分不足，余" + ObjectToString(Remainder, "N0");
                                            }
                                            #endregion

                                            WX_UserGameLog newgl = null;
                                            if (findupdate3 == null)
                                            {


                                                newgl = new WX_UserGameLog();
                                                newgl.aspnet_UserID = GlobalParam.UserKey;
                                                newgl.Buy_Point = Convert.ToDecimal(StrBuyPoint3);
                                                newgl.Buy_Type = "组合";
                                                newgl.Buy_Value = KeyValue3;
                                                newgl.Buy_Point = Convert.ToDecimal(StrBuyPoint3);
                                                newgl.GameName = "重庆时时彩";
                                                newgl.TransTime = reply.ReceiveTime;
                                                newgl.WX_UserName = reply.WX_UserName;
                                                newgl.WX_SourceType = reply.WX_SourceType;
                                                newgl.Buy_Ratio = ratios.BasicRatio;

                                                newgl.BounsRatio_WhenMiddle = 0;
                                                newgl.BounsRatio_WhenOK = 0;
                                                newgl.Result_HaveProcess = false;
                                                newgl.GamePeriod = reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod;

                                                string LocalDay = (reply.ReceiveTime.Hour <= 2 ? reply.ReceiveTime.AddDays(-1).ToString("yyyyMMdd") : reply.ReceiveTime.ToString("yyyyMMdd"));
                                                newgl.GameLocalPeriod = LocalDay + NextLocalPeriod;
                                                db.WX_UserGameLog.InsertOnSubmit(newgl);
                                            }
                                            else
                                            {
                                                if (findupdate3.Result_HaveProcess == true)
                                                {

                                                    WX_UserChangeLog findcl = db.WX_UserChangeLog.SingleOrDefault(t =>
                               t.aspnet_UserID == GlobalParam.UserKey
                               && t.WX_UserName == findupdate3.WX_UserName
                               && t.WX_SourceType == findupdate3.WX_SourceType
                               && t.RemarkType == "开奖"
                               && t.GamePeriod == findupdate3.GamePeriod
                               && t.BuyValue == findupdate3.Buy_Value
                               );
                                                    if (findcl != null)
                                                    {
                                                        db.WX_UserChangeLog.DeleteOnSubmit(findcl);
                                                    }
                                                    findupdate3.Result_HaveProcess = false;
                                                }
                                                findupdate3.Buy_Point += Convert.ToDecimal(StrBuyPoint3);
                                                findupdate3.Buy_Ratio = ratios == null ? 0 : ratios.BasicRatio;
                                            }

                                            Linq.WX_UserChangeLog cl = null;

                                            cl = new WX_UserChangeLog();
                                            cl.aspnet_UserID = GlobalParam.UserKey;
                                            cl.WX_UserName = reply.WX_UserName;
                                            cl.WX_SourceType = reply.WX_SourceType;
                                            cl.ChangePoint = -Convert.ToDecimal(StrBuyPoint3);
                                            cl.NeedNotice = false;
                                            cl.HaveNotice = false;
                                            cl.ChangeTime = reply.ReceiveTime;
                                            cl.RemarkType = "下单";
                                            cl.Remark = "下单@#" + reply.ReceiveContent;
                                            cl.FinalStatus = false;
                                            cl.BuyValue = (findupdate3 == null ? newgl.Buy_Value : findupdate3.Buy_Value);
                                            cl.GamePeriod = (findupdate3 == null ? newgl.GamePeriod : findupdate3.GamePeriod);
                                            cl.GameLocalPeriod = (findupdate3 == null ? newgl.GameLocalPeriod : findupdate3.GameLocalPeriod);
                                            cl.ChangeLocalDay = (findupdate3 == null ? newgl.GameLocalPeriod : findupdate3.GameLocalPeriod).Substring(0, 8);

                                            cl.FinalStatus = true;
                                            db.WX_UserChangeLog.InsertOnSubmit(cl);
                                            try
                                            {
                                                db.SubmitChanges();

                                                TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                                                    && t.WX_UserName == reply.WX_UserName
                                                    && t.WX_SourceType == reply.WX_SourceType
                                                    && t.Result_HaveProcess == false
                                                    && t.Buy_Point != 0).ToList(), MemberSource);
                                                return tr.ToSlimStringV2() + "余:" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                                            }
                                            catch (Exception AnyError)
                                            {

                                                return AnyError.Message + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");
                                            }

                                        }//字典有数字
                                        else
                                        {
                                            return "";
                                        }//字典没数字
                                    }//3位后是数字
                                }//超过4位才可能是3数字的组合
                                else
                                {
                                    return "";
                                }
                            }//2位后不是数字
                            {

                                if (ComboString.ContainsKey(BuyType2))
                                {
                                    Linq.aspnet_UsersNewGameResultSend myset = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey);
                                    if (myset.IsBlock == true)
                                    {
                                        return "封盘";
                                    }
                                    Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == reply.ReceiveTime.ToString("HH:mm"));
                                    if (testmin != null && adminmode == false)
                                    {
                                        return "整点" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                                    }
                                    string KeyValue2 = "";
                                    ComboString.TryGetValue(BuyType2, out KeyValue2);

                                    WX_UserGameLog findupdate2 = db.WX_UserGameLog.SingleOrDefault(t =>
                                        t.aspnet_UserID == GlobalParam.UserKey
                                        && t.WX_UserName == reply.WX_UserName
                                        && t.WX_SourceType == reply.WX_SourceType
                                        && t.Buy_Value == KeyValue2
                                         && t.GamePeriod == reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod
                                        );

                                    #region 检查赔率

                                    Decimal CheckBuy = (findupdate2 == null ? 0 : findupdate2.Buy_Point.Value) + Convert.ToDecimal(StrBuyPoint2);

                                    var ratios = db.Game_BasicRatio.SingleOrDefault(t =>
                                        t.BuyType == "组合"
                                        && t.aspnet_UserID == GlobalParam.UserKey
                                       && (t.BuyValue == KeyValue2)

                                        && t.MinBuy <= CheckBuy
                                        && t.MaxBuy >= CheckBuy
                                        );
                                    if (ratios == null)
                                    {
                                        Decimal? MaxLimit = db.Game_BasicRatio.Where(t =>
                                            t.aspnet_UserID == GlobalParam.UserKey
                               && t.BuyType == "组合"
                                  && (t.BuyValue == KeyValue2)

                               ).Max(t => t.MaxBuy);
                                        Decimal? MinLimit = db.Game_BasicRatio.Where(t =>
                                            t.aspnet_UserID == GlobalParam.UserKey
                                           && t.BuyType == "组合"
                                  && (t.BuyValue == KeyValue2)
                                            ).Min(t => t.MinBuy);
                                        return "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                                    }
                                    #endregion

                                    #region 检查余额
                                    decimal Remainder = WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType);
                                    if (Remainder < Convert.ToDecimal(StrBuyPoint2))
                                    {
                                        return "余分不足，余" + ObjectToString(Remainder, "N0");
                                    }
                                    #endregion

                                    WX_UserGameLog newgl = null;
                                    if (findupdate2 == null)
                                    {
                                        newgl = new WX_UserGameLog();
                                        newgl.aspnet_UserID = GlobalParam.UserKey;
                                        newgl.Buy_Point = Convert.ToDecimal(StrBuyPoint2);
                                        newgl.Buy_Type = "组合";
                                        newgl.Buy_Value = KeyValue2;
                                        newgl.Buy_Point = Convert.ToDecimal(StrBuyPoint2);
                                        newgl.GameName = "重庆时时彩";
                                        newgl.TransTime = reply.ReceiveTime;
                                        newgl.WX_UserName = reply.WX_UserName;
                                        newgl.WX_SourceType = reply.WX_SourceType;
                                        newgl.Buy_Ratio = ratios.BasicRatio;

                                        newgl.BounsRatio_WhenMiddle = 0;
                                        newgl.BounsRatio_WhenOK = 0;

                                        newgl.Result_HaveProcess = false;
                                        newgl.GamePeriod = reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod;

                                        string LocalDay = (reply.ReceiveTime.Hour <= 2 ? reply.ReceiveTime.AddDays(-1).ToString("yyyyMMdd") : reply.ReceiveTime.ToString("yyyyMMdd"));
                                        newgl.GameLocalPeriod = LocalDay + NextLocalPeriod;
                                        db.WX_UserGameLog.InsertOnSubmit(newgl);
                                    }
                                    else
                                    {
                                        if (findupdate2.Result_HaveProcess == true)
                                        {

                                            WX_UserChangeLog findcl = db.WX_UserChangeLog.SingleOrDefault(t =>
                              t.aspnet_UserID == GlobalParam.UserKey
                              && t.WX_UserName == findupdate2.WX_UserName
                              && t.WX_SourceType == findupdate2.WX_SourceType
                              && t.RemarkType == "开奖"
                              && t.GamePeriod == findupdate2.GamePeriod
                              && t.BuyValue == findupdate2.Buy_Value
                              );
                                            if (findcl != null)
                                            {
                                                db.WX_UserChangeLog.DeleteOnSubmit(findcl);
                                            }
                                            findupdate2.Result_HaveProcess = false;
                                        }
                                        findupdate2.Buy_Point += Convert.ToDecimal(StrBuyPoint2);
                                        findupdate2.Buy_Ratio = ratios == null ? 0 : ratios.BasicRatio;
                                    }

                                    Linq.WX_UserChangeLog cl = null;

                                    cl = new WX_UserChangeLog();
                                    cl.aspnet_UserID = GlobalParam.UserKey;
                                    cl.WX_UserName = reply.WX_UserName;
                                    cl.WX_SourceType = reply.WX_SourceType;
                                    cl.ChangePoint = -Convert.ToDecimal(StrBuyPoint2);
                                    cl.NeedNotice = false;
                                    cl.HaveNotice = false;
                                    cl.ChangeTime = reply.ReceiveTime;
                                    cl.RemarkType = "下单";
                                    cl.Remark = "下单@#" + reply.ReceiveContent;
                                    cl.FinalStatus = false;

                                    cl.BuyValue = (findupdate2 == null ? newgl.Buy_Value : findupdate2.Buy_Value);
                                    cl.GamePeriod = (findupdate2 == null ? newgl.GamePeriod : findupdate2.GamePeriod);
                                    cl.GameLocalPeriod = (findupdate2 == null ? newgl.GameLocalPeriod : findupdate2.GameLocalPeriod);
                                    cl.ChangeLocalDay = (findupdate2 == null ? newgl.GameLocalPeriod : findupdate2.GameLocalPeriod).Substring(0, 8);

                                    cl.FinalStatus = true;
                                    db.WX_UserChangeLog.InsertOnSubmit(cl);
                                    try
                                    {
                                        db.SubmitChanges();

                                        TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                                            && t.WX_UserName == reply.WX_UserName
                                            && t.WX_SourceType == reply.WX_SourceType
                                            && t.Result_HaveProcess == false
                                           ).ToList(), MemberSource);
                                        return tr.ToSlimStringV2() + "余:" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");

                                    }
                                    catch (Exception AnyError)
                                    {

                                        return AnyError.Message + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");
                                    }
                                }//字典有数字
                                else
                                {
                                    return "";
                                }//字典没数字
                            }//2位后数数字


                        }//超过3位才可能是数字
                        else
                        {
                            return "";
                        }
                        #endregion

                    }//1位后的不是数字
                    BuyPoint = Convert.ToDecimal(Str_BuyPoint);
                    #region


                    string CheckResult = "";


                    switch (FirstIndex)
                    {
                        case "龙":
                            CheckResult = NewGameLog(reply, "龙虎合", FirstIndex, BuyPoint, db, adminmode);
                            break;
                        case "虎":
                            CheckResult = NewGameLog(reply, "龙虎合", FirstIndex, BuyPoint, db, adminmode);
                            break;
                        case "合":
                            CheckResult = NewGameLog(reply, "龙虎合", FirstIndex, BuyPoint, db, adminmode);
                            break;
                        case "大":
                            CheckResult = NewGameLog(reply, "大小和", FirstIndex, BuyPoint, db, adminmode);
                            break;
                        case "小":
                            CheckResult = NewGameLog(reply, "大小和", FirstIndex, BuyPoint, db, adminmode);
                            break;
                        case "和":
                            CheckResult = NewGameLog(reply, "大小和", FirstIndex, BuyPoint, db, adminmode);
                            break;
                        case "单":
                            CheckResult = NewGameLog(reply, "单双", FirstIndex, BuyPoint, db, adminmode);
                            break;
                        case "双":
                            CheckResult = NewGameLog(reply, "单双", FirstIndex, BuyPoint, db, adminmode);
                            break;
                        default:
                            return "";
                    }

                    if (CheckResult != "")
                    {
                        TotalResult tr = BuildResult(
                            db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                                && t.WX_UserName == reply.WX_UserName
                                && t.WX_SourceType == reply.WX_SourceType
                                && t.Result_HaveProcess == false
                                ).ToList()
                            , MemberSource);
                        return CheckResult + tr.ToSlimStringV2() + "余:" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");
                    }
                    else
                    {

                        TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                            && t.WX_UserName == reply.WX_UserName
                            && t.WX_SourceType == reply.WX_SourceType
                            && t.Result_HaveProcess == false
                            && t.Buy_Point != 0).ToList()
                        , MemberSource);
                        return tr.ToSlimStringV2() + "余:" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName, reply.WX_SourceType), "N0");
                    }
                    #endregion
                }//下单

            }//有文字消息Result
            else
            {
                return "";
            }

        }

        public enum FormatResultState { Initialize, Fail, Multi, SingleSuccess }
        public enum FormatResultType { Initialize, QueryTxt, QueryImage, QueryResult, OrderModify, CancelOrderModify }

        public enum FormatResultDirection { MemoryMatchList, DataBaseGameLog }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReceiveContent">1开头查图2开头查赛果</param>
        /// <param name="OutTeams"></param>
        /// <param name="Mode"></param>
        /// <returns></returns>
        public static object[] ReceiveContentFormat(string ReceiveContent, out FormatResultState State, out FormatResultType ModeType, FormatResultDirection direct, out string BuyType, out string BuyMoney, out string[] ContextTeams)
        {

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");



            State = FormatResultState.Initialize;
            ModeType = FormatResultType.Initialize;

            string NewContext = ReceiveContent;
            if (NewContext.StartsWith("图"))
            {
                NewContext = NewContext.Substring(1);
                ModeType = FormatResultType.QueryImage;
            }
            else if (NewContext.StartsWith("即时"))
            {
                NewContext = NewContext.Substring(2);
                ModeType = FormatResultType.QueryResult;
            }
            else
            {
                ModeType = FormatResultType.QueryTxt;
            }

            if (NewContext.StartsWith("取消"))
            {
                NewContext = NewContext.Substring(2);
                ModeType = FormatResultType.CancelOrderModify;
            }
            NewContext = NewContext.Replace(",", "").Replace(".", "").Replace("。", "");


            Int32 BuyTypeIndex = FindChineseBuyType(NewContext.Replace("-", "比"));
            string[] TeamsAndBuy = (BuyTypeIndex > 0 ? (new string[] { NewContext.Substring(0, BuyTypeIndex), NewContext.Substring(BuyTypeIndex) }) : (new string[] { NewContext }));
            if (TeamsAndBuy.Length > 1)
            {
                string strfindmoney = TeamsAndBuy[1]
                 .Replace("1比0", "")
                 .Replace("0比1", "")
                 .Replace("2比0", "")
                 .Replace("0比2", "")
                 .Replace("2比1", "")
                 .Replace("1比2", "")
                 .Replace("3比0", "")
                 .Replace("0比3", "")
                 .Replace("3比1", "")
                 .Replace("1比3", "")
                 .Replace("3比2", "")
                 .Replace("2比3", "")
                 .Replace("4比0", "")
                 .Replace("0比4", "")
                 .Replace("4比1", "")
                 .Replace("4比2", "")
                 .Replace("4比2", "")
                 .Replace("2比4", "")
                 .Replace("4比3", "")
                 .Replace("3比4", "")
                 .Replace("0比0", "")
                 .Replace("1比1", "")
                 .Replace("2比2", "")
                 .Replace("3比3", "")
                 .Replace("4比4", "")
                 .Replace("其他", "")
                 .Replace("大球", "")
                 .Replace("小球", "")
                 .Replace("主主", "")
                 .Replace("主和", "")
                 .Replace("主客", "")
                 .Replace("和主", "")
                 .Replace("和和", "")
                 .Replace("和客", "")
                 .Replace("客主", "")
                 .Replace("客和", "")
                 .Replace("客客", "")
                 .Replace("主", "")
                 .Replace("客", "")
                 .Replace(" ", "");

                BuyMoney = strfindmoney;
                BuyType = TeamsAndBuy[1].Replace(strfindmoney, "").Replace(" ", "");

                ModeType = (ModeType == FormatResultType.CancelOrderModify ? ModeType : FormatResultType.OrderModify);
            }
            else
            {
                BuyType = "";
                BuyMoney = "";
            }
            string[] newTeams = TeamsAndBuy[0].Split("对".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            string A_Team = newTeams.Length > 0 ? newTeams[0] : "空白的内容";
            string B_Team = newTeams.Length > 1 ? newTeams[1] : "";


            ContextTeams = new string[] { A_Team, B_Team };
            var source = db.Game_FootBall_VS.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                //&& (t.LastAliveTime == null || t.LastAliveTime >= DateTime.Today.AddDays(-3))
                 && t.Jobid == GlobalParam.JobID
                );
            var mem_machines = source.Where(t =>
                                 (t.A_Team.Contains(A_Team) && t.B_Team.Contains(B_Team))
                                 || (t.A_Team.Contains(B_Team) && t.B_Team.Contains(A_Team))
                                 );
            var db_machines = db.WX_UserGameLog_Football.Where(t =>
                             (t.A_Team.Contains(A_Team) && t.B_Team.Contains(B_Team))
                                 || (t.A_Team.Contains(B_Team) && t.B_Team.Contains(A_Team))
                                 );
            if (direct == FormatResultDirection.MemoryMatchList)
            {
                if (mem_machines.Count() == 0 || TeamsAndBuy[0].Length < 2)
                {
                    State = FormatResultState.Fail;
                    return new Game_FootBall_VS[] { };
                }
                else if (mem_machines.Count() == 1)
                {
                    if (State == FormatResultState.Initialize)
                    {
                        State = FormatResultState.SingleSuccess;

                    }
                    return mem_machines.ToArray();
                }
                else
                {

                    State = FormatResultState.Multi;
                    return mem_machines.ToArray();
                }
            }//下单查询类，从内存的球赛列表
            else if (direct == FormatResultDirection.DataBaseGameLog)
            {

                if (db_machines.Count() == 0 || TeamsAndBuy[0].Length < 2)
                {
                    State = FormatResultState.Fail;
                    return null;
                }
                else if (db_machines.Count() == 1)
                {
                    if (State == FormatResultState.Initialize)
                    {
                        State = FormatResultState.SingleSuccess;

                    }
                    return db_machines.ToArray();
                }
                else
                {

                    State = FormatResultState.Multi;
                    return db_machines.ToArray();
                }
            }//取消的从数据库查询
            else
            {
                State = FormatResultState.Fail;
                throw new Exception("无法识别的数据来源类型" + direct);
            }//其他没有的

        }

        private static string GetUserUpOpenBallGame(dbDataContext db, string WX_UserName, string WX_SourceType)
        {
            var glunopens = db.WX_UserGameLog_Football.Where(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == WX_UserName && t.WX_SourceType == WX_SourceType && t.HaveOpen == false);

            var KEYS = glunopens.Select(t => new { t.A_Team, t.B_Team, t.GameTime, t.MatchClass }).Distinct();
            string rtsfb = "";



            foreach (var item in KEYS)
            {
                try
                {
                    rtsfb +=
                      Convert.ToDateTime(DateTime.Today.Year.ToString() + "-" + item.GameTime).ToString("yyyy年MM月dd日 HH:mm") + Environment.NewLine;
                }
                catch
                {
                    NetFramework.Console.WriteLine("赛事时间错误" + DateTime.Today.Year.ToString() + "-" + item.GameTime);
                }
                rtsfb += (item.MatchClass == null ? "" : item.MatchClass) + item.A_Team + " VS " + item.B_Team + Environment.NewLine;
                var subglunopens = glunopens.Where(t => t.A_Team == item.A_Team && t.B_Team == item.B_Team);
                foreach (var subitem in subglunopens)
                {
                    string Newwinless = "";
                    if (subitem.BuyType == "A_WIN" || subitem.BuyType == "B_WIN")
                    {
                        Newwinless = (BallWinlessToNumber(subitem.Winless));
                    }
                    else
                    {
                        Newwinless = "";
                    }

                    string NewTtoal = "";
                    if (subitem.BuyType == "BIGWIN" || subitem.BuyType == "SMALLWIN")
                    {
                        NewTtoal = "大小:" + subitem.Total;
                    }
                    else
                    {
                        NewTtoal = "";
                    }



                    rtsfb += BallBuyTypeToChinseFrontShow(subitem.BuyType) + ":" + subitem.BuyMoney.ToString() + "，"
                          + Newwinless
                          + NewTtoal
                          + (Newwinless != "" && NewTtoal != "" ? "" : "，") + subitem.BuyRatio + "水" + Environment.NewLine;

                }
                rtsfb += Environment.NewLine;

            }
            return rtsfb;
        }

        private static Dictionary<string, string> ComboString = null;

        public static string WX_UserReplyLog_MySendCreate(string Content, DataRow UserRow, DateTime ReceiveTime)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            string Contact = UserRow.Field<string>("User_ContactID");
            string SourceType = UserRow.Field<string>("User_SourceType");
            if (Content == "自动")
            {


                Int32? MaxTraceCount = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey).MaxPlayerCount;
                //if (MaxTraceCount.HasValue == false)
                //{
                //    MaxTraceCount = 50;
                //}
                //if (db.WX_UserReply.Where(t => t.aspnet_UserID == GlobalParam.Key && t.IsReply == true).Count() + 1 > MaxTraceCount)
                //{
                //    return "超过最大跟踪玩家数量";
                //}
                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == Contact && t.WX_SourceType == SourceType);
                toupdate.IsReply = true;
                UserRow.SetField("User_IsReply", true);
                db.SubmitChanges();


                return "";
            }
            else if (Content == "取消自动")
            {


                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == Contact && t.WX_SourceType == SourceType);
                toupdate.IsReply = false;
                UserRow.SetField("User_IsReply", false);
                db.SubmitChanges();


                return "";
            }
            else if (Content == "转发")
            {



                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == Contact && t.WX_SourceType == SourceType);
                toupdate.IsReceiveTransfer = true;

                db.SubmitChanges();
                UserRow.SetField("User_IsReceiveTransfer", true);

                return "";
            }
            else if (Content == "取消转发")
            {



                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == Contact && t.WX_SourceType == SourceType);
                toupdate.IsReceiveTransfer = false;

                db.SubmitChanges();
                UserRow.SetField("User_IsReceiveTransfer", false);

                return "";
            }
            #region 查询加日期
            else if (Content.StartsWith("查询"))
            {
                //查询2018-06-01到2018-08-01
                if (Content.Length >= 12)
                {

                    string FromDate = Content.Substring(2, 10);

                    string ToDate = FromDate;
                    try
                    {
                        ToDate = Content.Substring(13, 10);
                    }
                    catch (Exception)
                    {

                    }

                    DateTime? dt_From = DateTime.Today;
                    DateTime? dt_To = DateTime.Today;
                    try
                    {
                        dt_From = Convert.ToDateTime(FromDate);
                    }
                    catch (Exception)
                    {
                        return "";
                    }
                    try
                    {
                        dt_To = Convert.ToDateTime(ToDate);
                    }
                    catch (Exception)
                    {


                    }
                    List<Guid> takeusers = ((from ds in db.aspnet_UsersNewGameResultSend
                                             where ds.bossaspnet_UserID == GlobalParam.UserKey

                                             select ds.aspnet_UserID).Distinct()

                                             ).ToList();
                    takeusers.Add(GlobalParam.UserKey);


                    string Result = "";

                    foreach (Guid item in takeusers)
                    {
                        System.Web.Security.MembershipUser usr = System.Web.Security.Membership.GetUser(item);
                        DataTable Fulls = BuildOpenQueryTable(dt_From.Value, dt_To.Value, item);
                        foreach (DataRow rowitem in Fulls.Rows)
                        {
                            Result += usr.UserName + ":"
                                + ObjectToString(rowitem.Field<object>("类别"))
                                 + ObjectToString(rowitem.Field<object>("全部玩家")) + Environment.NewLine;

                        }
                    }

                    return Result;



                }
                else
                {
                    return "";
                }
            }
            #endregion

            else if (Content.StartsWith("20"))
            {
                Regex checkisboss = new Regex("[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][\\S\\s][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]", RegexOptions.IgnoreCase);
                if (Content.Length > 11 && checkisboss.Matches(Content).Count < 1)
                {
                    string Period = Content.Substring(0, 11);
                    string BuyContent = Content.Substring(11);
                    string Date = Period.Substring(0, 4) + "-" + Period.Substring(4, 2) + "-" + Period.Substring(6, 2);
                    string SubPeriod = Period.Substring(8, 3);

                    Game_ChongqingshishicaiPeriodMinute find = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.PeriodIndex == SubPeriod);
                    string ReceiveTimeStr = DateTime.Today.ToString("yyyy-MM-dd") + " " + find.TimeMinute;

                    WX_UserReplyLog newrl = new WX_UserReplyLog();
                    newrl.aspnet_UserID = GlobalParam.UserKey;
                    newrl.WX_UserName = UserRow.Field<string>("User_ContactID");
                    newrl.WX_SourceType = UserRow.Field<string>("User_SourceType");
                    newrl.ReceiveTime = Convert.ToDateTime(ReceiveTimeStr).AddMilliseconds(-1);
                    while (db.WX_UserChangeLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == newrl.WX_UserName && t.WX_SourceType == newrl.WX_SourceType && t.ChangeTime == newrl.ReceiveTime) != null)
                    {
                        newrl.ReceiveTime = newrl.ReceiveTime.AddMilliseconds(-1);
                    }
                    newrl.SourceType = "补单";
                    newrl.ReceiveContent = BuyContent;
                    return WX_UserReplyLog_Create(newrl, UserRow.Table, GameMode.时时彩, true);

                }
                else
                {
                    return "";
                }

            }
            else if (Content == "福利")
            {


                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == Contact && t.WX_SourceType == SourceType);
                toupdate.IsCaculateFuli = true;

                db.SubmitChanges();
                UserRow.SetField("User_IsCaculateFuli", true);

                return "";
            }
            else if (Content == "取消福利")
            {


                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == Contact && t.WX_SourceType == SourceType);
                toupdate.IsCaculateFuli = false;

                db.SubmitChanges();
                UserRow.SetField("User_IsCaculateFuli", false);

                return "";
            }


            else if (Content == "老板查询")
            {


                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == Contact && t.WX_SourceType == SourceType);
                toupdate.IsBoss = true;

                db.SubmitChanges();
                UserRow.SetField("User_IsBoss", true);

                return "";
            }


            else if (Content == "取消老板查询")
            {


                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == Contact && t.WX_SourceType == SourceType);
                toupdate.IsBoss = false;

                db.SubmitChanges();
                UserRow.SetField("User_IsBoss", false);

                return "";
            }

            else if (Content == "球赛图片")
            {


                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == Contact && t.WX_SourceType == SourceType);
                toupdate.IsBallPIC = true;

                db.SubmitChanges();
                UserRow.SetField("User_IsBallPIC", true);

                return "";
            }


            else if (Content == "取消球赛图片")
            {


                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == Contact && t.WX_SourceType == SourceType);
                toupdate.IsBallPIC = false;

                db.SubmitChanges();
                UserRow.SetField("User_IsBallPIC", false);

                return "";
            }
            else if (Content == "会员")
            {


                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == Contact && t.WX_SourceType == SourceType);
                toupdate.IsAdmin = true;

                db.SubmitChanges();
                UserRow.SetField("User_IsAdmin", true);

                return "";
            }


            else if (Content == "取消会员")
            {


                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == Contact && t.WX_SourceType == SourceType);
                toupdate.IsAdmin = false;

                db.SubmitChanges();
                UserRow.SetField("User_IsAdmin", false);

                return "";
            }




            else if (Content.Length >= 2)
            {
                if (Content.StartsWith("上"))
                {

                    Content = "上分" + Content.Substring(1);
                }
                else if (Content.StartsWith("下"))
                {
                    Content = "下分" + Content.Substring(1);
                }

                string Mode = Content.Substring(0, 2);
                string Money = Content.Substring(2);
                decimal ChargeMoney = 0;
                try
                {
                    ChargeMoney = Convert.ToDecimal(Money);
                }
                catch (Exception)
                {
                    return "";
                }
                if (ChargeMoney <= 0)
                {
                    return "";
                }


                string LocalDay = (ReceiveTime.Hour <= 2 ? ReceiveTime.AddDays(-1).ToString("yyyyMMdd") : ReceiveTime.ToString("yyyyMMdd"));





                switch (Mode)
                {
                    case "上分":
                        Linq.WX_UserChangeLog change = new Linq.WX_UserChangeLog();
                        change.aspnet_UserID = GlobalParam.UserKey;
                        change.ChangeTime = ReceiveTime;
                        change.ChangePoint = ChargeMoney;
                        change.Remark = "上分:" + UserRow.Field<string>("User_ContactID");
                        change.RemarkType = "上分";
                        change.WX_UserName = UserRow.Field<string>("User_ContactID");
                        change.WX_SourceType = UserRow.Field<string>("User_SourceType");
                        change.FinalStatus = true;
                        change.ChangeLocalDay = LocalDay;

                        change.BuyValue = "";
                        change.GamePeriod = "";
                        db.WX_UserChangeLog.InsertOnSubmit(change);

                        db.SubmitChanges();

                        decimal? TotalPoint = WXUserChangeLog_GetRemainder(UserRow.Field<string>("User_ContactID"), UserRow.Field<string>("User_SourceType"));

                        return "余:" + TotalPoint.Value.ToString("N0");

                    case "下分":
                        decimal? TotalPointIn = WXUserChangeLog_GetRemainder(UserRow.Field<string>("User_ContactID"), UserRow.Field<string>("User_SourceType"));

                        if (TotalPointIn < ChargeMoney)
                        {
                            return "下分失败,余分不足,余" + ObjectToString(WXUserChangeLog_GetRemainder(UserRow.Field<string>("User_ContactID"), UserRow.Field<string>("User_SourceType")), "N0");

                        }

                        Linq.WX_UserChangeLog cleanup = new Linq.WX_UserChangeLog();
                        cleanup.aspnet_UserID = GlobalParam.UserKey;
                        cleanup.ChangeTime = ReceiveTime;
                        cleanup.ChangePoint = -ChargeMoney;
                        cleanup.Remark = "下分:" + UserRow.Field<string>("User_ContactID");
                        cleanup.RemarkType = "下分";
                        cleanup.FinalStatus = true;
                        cleanup.WX_UserName = UserRow.Field<string>("User_ContactID");
                        cleanup.WX_SourceType = UserRow.Field<string>("User_SourceType");
                        cleanup.ChangeLocalDay = LocalDay;
                        cleanup.BuyValue = "";
                        cleanup.GamePeriod = "";
                        db.WX_UserChangeLog.InsertOnSubmit(cleanup);

                        db.SubmitChanges();



                        decimal? TotalPoint2 = WXUserChangeLog_GetRemainder(UserRow.Field<string>("User_ContactID"), UserRow.Field<string>("User_SourceType"));

                        return "余:" + TotalPoint2.Value.ToString("N0");



                    case "封盘":
                        Linq.aspnet_UsersNewGameResultSend myset = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey);
                        myset.IsBlock = true;
                        db.SubmitChanges();
                        return "已封盘";

                    case "解封":
                        Linq.aspnet_UsersNewGameResultSend myset2 = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey);
                        myset2.IsBlock = false;
                        db.SubmitChanges();
                        return "已解封";

                    case "福利":
                        Linq.WX_UserChangeLog ful_change = new Linq.WX_UserChangeLog();
                        ful_change.aspnet_UserID = GlobalParam.UserKey;
                        ful_change.ChangeTime = DateTime.Now;
                        ful_change.ChangePoint = ChargeMoney;
                        ful_change.Remark = "福利:" + UserRow.Field<string>("User_ContactID");
                        ful_change.RemarkType = "福利";
                        ful_change.WX_UserName = UserRow.Field<string>("User_ContactID");
                        ful_change.WX_SourceType = UserRow.Field<string>("User_SourceType");
                        ful_change.FinalStatus = true;

                        ful_change.BuyValue = "";
                        ful_change.GamePeriod = "";

                        DateTime testtime = DateTime.Now;
                        if (testtime.Hour <= 10)
                        {
                            testtime = testtime.AddDays(-1);
                        }

                        ful_change.ChangeLocalDay = testtime.ToString("yyyyMMdd");

                        db.WX_UserChangeLog.InsertOnSubmit(ful_change);

                        db.SubmitChanges();

                        decimal? fyl_TotalPoint = WXUserChangeLog_GetRemainder(UserRow.Field<string>("User_ContactID"), UserRow.Field<string>("User_SourceType"));

                        return "余:" + fyl_TotalPoint.Value.ToString("N0");




                    default:
                        return "";

                }//SWITCH结束

            }//超过2个长度
            else
            {
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
        private static string NewGameLog(WX_UserReplyLog replylog, string BuyType, string BuyValue, decimal BuyPoint, dbDataContext db, bool adminmode)
        {


            if (string.Compare(replylog.ReceiveTime.ToString("HH:mm"), "01:55") >= 0 && string.Compare(replylog.ReceiveTime.ToString("HH:mm"), "09:00") <= 0)
            {
                return "封盘时间";
            }
            Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == replylog.ReceiveTime.ToString("HH:mm"));
            if (testmin != null && adminmode == false)
            {
                return "整点" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(replylog.WX_UserName, replylog.WX_SourceType), "N0");

            }
            #region "时间转化期数"
            string Minutes = replylog.ReceiveTime.ToString("HH:mm");
            string NextPeriod = "";
            string NextLocalPeriod = "";
            var NextMonutes = db.Game_ChongqingshishicaiPeriodMinute.Where(t => string.Compare(t.TimeMinute, Minutes) >= (adminmode == false ? 1 : 0)).OrderBy(t => t.PeriodIndex);
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



            WX_UserGameLog CheckExists = db.WX_UserGameLog.SingleOrDefault(t =>
                t.aspnet_UserID == GlobalParam.UserKey
                && t.WX_UserName == replylog.WX_UserName
                && t.WX_SourceType == replylog.WX_SourceType
                && t.GamePeriod == replylog.ReceiveTime.ToString("yyyyMMdd") + NextPeriod
                && t.Buy_Value == BuyValue);

            decimal Remainder = WXUserChangeLog_GetRemainder(replylog.WX_UserName, replylog.WX_SourceType);
            if (Remainder < BuyPoint)
            {
                return "余分不足";
            }

            if (CheckExists != null)
            {


                if (CheckExists.Result_HaveProcess == true)
                {

                    WX_UserChangeLog findcl = db.WX_UserChangeLog.SingleOrDefault(t =>
                             t.aspnet_UserID == GlobalParam.UserKey
                             && t.WX_UserName == CheckExists.WX_UserName
                             && t.WX_SourceType == CheckExists.WX_SourceType
                             && t.RemarkType == "开奖"
                             && t.GamePeriod == CheckExists.GamePeriod
                             && t.BuyValue == CheckExists.Buy_Value
                             );
                    if (findcl != null)
                    {
                        db.WX_UserChangeLog.DeleteOnSubmit(findcl);
                    }
                    CheckExists.Result_HaveProcess = false;
                }

                #region "转化赔率"
                Decimal CheckBuy = (CheckExists == null ? 0 : CheckExists.Buy_Point.Value) + BuyPoint;

                var CheckRatioConfig = db.Game_BasicRatio.Where(t => t.GameType == "重庆时时彩"
                    && t.aspnet_UserID == GlobalParam.UserKey
                    && t.BuyType == CheckExists.Buy_Type
                    && t.BuyValue == CheckExists.Buy_Value
                    && t.MaxBuy >= CheckBuy
                    && ((t.MinBuy <= (CheckBuy) && t.IncludeMin == true)
                    || (t.MinBuy < (CheckBuy) && t.IncludeMin == false)
                    )
                    );
                if (CheckRatioConfig.Count() == 0)
                {
                    Decimal? MaxLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                        && t.BuyValue == CheckExists.Buy_Value
                        && t.BuyType == CheckExists.Buy_Type
                        ).Max(t => t.MaxBuy);
                    Decimal? MinLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                        && t.BuyValue == CheckExists.Buy_Value
                         && t.BuyType == CheckExists.Buy_Type
                        ).Min(t => t.MinBuy);
                    return "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围";

                }
                if (CheckRatioConfig.Select(t => t.BasicRatio).Distinct().Count() > 1)
                {
                    return "符合购买范围的赔率有多个";

                }

                #endregion



                CheckExists.Buy_Point += BuyPoint;
                CheckExists.Buy_Ratio = CheckRatioConfig.Count() == 0 ? 0 : CheckRatioConfig.First().BasicRatio;
                WX_UserChangeLog cl = new WX_UserChangeLog();
                cl.aspnet_UserID = GlobalParam.UserKey;
                cl.WX_UserName = replylog.WX_UserName;
                cl.WX_SourceType = replylog.WX_SourceType;
                cl.ChangePoint = -BuyPoint;
                cl.NeedNotice = false;
                cl.HaveNotice = false;
                cl.ChangeTime = replylog.ReceiveTime;
                cl.RemarkType = "下单";
                cl.Remark = "下单@#" + replylog.ReceiveContent;
                cl.FinalStatus = false;
                cl.BuyValue = BuyValue;
                cl.GamePeriod = CheckExists.GamePeriod;
                cl.GameLocalPeriod = CheckExists.GameLocalPeriod;
                cl.ChangeLocalDay = CheckExists.GameLocalPeriod.Substring(0, 8);

                cl.FinalStatus = true;
                db.WX_UserChangeLog.InsertOnSubmit(cl);

                try
                {
                    db.SubmitChanges();
                    return "";
                }
                catch (Exception AnyError)
                {


                    NetFramework.Console.Write(AnyError.Message + Environment.NewLine);
                    NetFramework.Console.Write(AnyError.StackTrace + Environment.NewLine);
                    return AnyError.Message;
                }



            }//有记录合并
            else
            {

                #region "没有的新加"

                #region "转化赔率"
                var CheckRatioConfig = db.Game_BasicRatio.Where(t => t.GameType == "重庆时时彩"
                    && t.aspnet_UserID == GlobalParam.UserKey
                    && t.BuyType == BuyType
                    && t.BuyValue == BuyValue
                    && t.MaxBuy >= BuyPoint
                    && ((t.MinBuy <= (BuyPoint) && t.IncludeMin == true)
                    || (t.MinBuy < (BuyPoint) && t.IncludeMin == false)
                    )
                    );
                if (CheckRatioConfig.Count() == 0)
                {
                    Decimal? MaxLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                        && t.BuyValue == BuyValue
                        && t.BuyType == BuyType
                        ).Max(t => t.MaxBuy);
                    Decimal? MinLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                        && t.BuyValue == BuyValue
                         && t.BuyType == BuyType
                        ).Min(t => t.MinBuy);
                    return "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围";

                }

                if (CheckRatioConfig.Select(t => t.BasicRatio).Distinct().Count() > 1)
                {
                    return "符合购买范围的赔率有多个";

                }
                #endregion

                WX_UserGameLog newgl = new WX_UserGameLog();
                newgl.aspnet_UserID = GlobalParam.UserKey;
                newgl.Buy_Point = BuyPoint;
                newgl.Buy_Type = BuyType;
                newgl.Buy_Value = BuyValue;
                newgl.GameName = "重庆时时彩";
                newgl.TransTime = replylog.ReceiveTime;
                newgl.WX_UserName = replylog.WX_UserName;
                newgl.WX_SourceType = replylog.WX_SourceType;
                newgl.Buy_Ratio = CheckRatioConfig.Count() == 0 ? 0 : CheckRatioConfig.First().BasicRatio;

                newgl.Result_HaveProcess = false;
                newgl.GamePeriod = replylog.ReceiveTime.ToString("yyyyMMdd") + NextPeriod;

                string LocalDay = (replylog.ReceiveTime.Hour <= 2 ? replylog.ReceiveTime.AddDays(-1).ToString("yyyyMMdd") : replylog.ReceiveTime.ToString("yyyyMMdd"));
                newgl.GameLocalPeriod = LocalDay + NextLocalPeriod;

                Game_BasicRatio Middbouns = db.Game_BasicRatio.SingleOrDefault(t =>
                    t.aspnet_UserID == GlobalParam.UserKey
                    && t.BuyType == "不吃"
                    && t.BuyValue == "和"
                    && t.MaxBuy >= newgl.Buy_Point
                     && t.MinBuy <= newgl.Buy_Point
                    );

                newgl.BounsRatio_WhenMiddle = (Middbouns == null ? 0.0M : Middbouns.BasicRatio);


                Game_BasicRatio okbouns = db.Game_BasicRatio.SingleOrDefault(t =>
                   t.aspnet_UserID == GlobalParam.UserKey
                   && t.BuyType == "不吃"
                   && t.BuyValue == "合"
                   && t.MaxBuy >= newgl.Buy_Point
                    && t.MinBuy <= newgl.Buy_Point
                   );

                newgl.BounsRatio_WhenOK = (okbouns == null ? 0.0M : okbouns.BasicRatio);

                Game_BasicRatio bouns23 = db.Game_BasicRatio.SingleOrDefault(t =>
                   t.aspnet_UserID == GlobalParam.UserKey
                   && t.BuyType == "不吃"
                   && t.BuyValue == "23"
                   && t.MaxBuy >= newgl.Buy_Point
                    && t.MinBuy <= newgl.Buy_Point
                   );

                newgl.BounsRatio_Wen23 = (bouns23 == null ? 0.0M : bouns23.BasicRatio);


                db.WX_UserGameLog.InsertOnSubmit(newgl);
                Linq.WX_UserChangeLog cl = null;

                cl = new WX_UserChangeLog();
                cl.aspnet_UserID = GlobalParam.UserKey;
                cl.WX_UserName = replylog.WX_UserName;
                cl.WX_SourceType = replylog.WX_SourceType;
                cl.ChangePoint = -BuyPoint;
                cl.NeedNotice = false;
                cl.HaveNotice = false;
                cl.ChangeTime = replylog.ReceiveTime;
                cl.RemarkType = "下单";
                cl.Remark = "下单@#" + replylog.ReceiveContent;
                cl.FinalStatus = false;
                cl.BuyValue = BuyValue;
                cl.GamePeriod = newgl.GamePeriod;
                cl.GameLocalPeriod = newgl.GameLocalPeriod;
                cl.FinalStatus = true;
                db.WX_UserChangeLog.InsertOnSubmit(cl);

                try
                {
                    db.SubmitChanges();
                    return "";
                }
                catch (Exception AnyError)
                {
                    NetFramework.Console.Write(AnyError.Message + Environment.NewLine);
                    NetFramework.Console.Write(AnyError.StackTrace + Environment.NewLine);

                    return AnyError.Message;
                }




                #endregion
            }//没记录新加



        }


        /// <summary>
        /// 获取余分，只可以更新数据库后调用
        /// </summary>
        /// <param name="replylog"></param>
        /// <param name="db"></param>
        /// <param name="HaveBuy"></param>
        /// <param name="TakeFinalStatus"></param>
        /// <returns></returns>
        public static decimal WXUserChangeLog_GetRemainder(string UserContactID, string SourceType)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            var RemindList = db.WX_UserChangeLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                && t.WX_UserName == UserContactID

                && t.WX_SourceType == SourceType
                );
            return RemindList.Count() == 0 ? 0.0M : RemindList.Sum(t => t.ChangePoint).Value;
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
        public static string Tiger = Encoding.UTF8.GetString(new byte[] { 238, 129, 144 });

        public static string Tiger_dingding = Encoding.UTF8.GetString(new byte[] { 240, 159, 144, 175 });



        public static string Dragon_yixin = Encoding.UTF8.GetString(new byte[] { 0xF0, 0x9f, 0x90, 0xB2 });
        public static string OK_yixin = Encoding.UTF8.GetString(new byte[] { 0xF0, 0x9f, 0X88, 0xB4 });
        public static string Tiger_yixin = Encoding.UTF8.GetString(new byte[] { 0xF0, 0x9f, 0x90, 0xaf });




        public static DataTable BuildOpenQueryTable(DateTime StartDate, DateTime EndDate, Guid UserGuid)
        {

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");




            DataTable Result = new DataTable();
            Result.Columns.Add("类别");




            var buys = (from ds in db.WX_UserGameLog
                        where
                        ds.aspnet_UserID == UserGuid
        && String.Compare(ds.GameLocalPeriod.Substring(0, 8), StartDate.ToString("yyyyMMdd")) >= 0
        && String.Compare(ds.GameLocalPeriod.Substring(0, 8), EndDate.ToString("yyyyMMdd")) <= 0
                        select ds).ToList();





            DataColumn ucfull = new DataColumn();
            ucfull.ColumnName = "全部玩家";
            Result.Columns.Add(ucfull);



            var BuyDays = buys.Select(t => t.GameLocalPeriod.Substring(0, 8)).Distinct().OrderBy(t => t);
            foreach (var item in BuyDays)
            {
                DataRow newr = Result.NewRow();

                newr.SetField("类别", item + "下注");

                newr.SetField("全部玩家", NetFramework.Util_Math.NullToZero(buys.Where(t =>
                         t.GameLocalPeriod.StartsWith(item)
                            && t.aspnet_UserID == UserGuid
                         ).Sum(t => t.Buy_Point)));




                DataRow newr2 = Result.NewRow();
                newr2.SetField("类别", item + "得分");

                newr2.SetField("全部玩家", NetFramework.Util_Math.NullToZero(buys.Where(t =>
                        t.GameLocalPeriod.StartsWith(item)
                         && t.aspnet_UserID == UserGuid
                        ).Sum(t => t.Result_Point)));






                DataRow newr4 = Result.NewRow();
                newr4.SetField("类别", item + "上分");

                newr4.SetField("全部玩家", NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                                     t.RemarkType == "上分"
                                        && t.aspnet_UserID == UserGuid
                         && t.ChangeLocalDay.StartsWith(item)
                        ).Sum(t => t.ChangePoint)));



                DataRow newr5 = Result.NewRow();
                newr5.SetField("类别", item + "下分");

                newr5.SetField("全部玩家", NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                                     t.RemarkType == "下分"
                                        && t.aspnet_UserID == UserGuid
                         && t.ChangeLocalDay.StartsWith(item
                         )
                        ).Sum(t => t.ChangePoint)));



                DataRow newr6 = Result.NewRow();
                newr6.SetField("类别", item + "福利");

                newr6.SetField("全部玩家", NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                                     t.RemarkType == "福利"
                                        && t.aspnet_UserID == UserGuid
                         && t.ChangeLocalDay.StartsWith(item)
                        ).Sum(t => t.ChangePoint)));



                DataRow newr3 = Result.NewRow();
                newr3.SetField("类别", item + "合计");

                newr3.SetField("全部玩家",

                   NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>

                          t.RemarkType == "上分"
                             && t.aspnet_UserID == UserGuid
                         && t.ChangeLocalDay.StartsWith(item)
                        ).Sum(t => t.ChangePoint))

                       + NetFramework.Util_Math.NullToZero(buys.Where(t =>

                         t.GameLocalPeriod.StartsWith(item)
                            && t.aspnet_UserID == UserGuid
                        ).Sum(t => t.Buy_Point))

                                            - NetFramework.Util_Math.NullToZero(buys.Where(t =>

                         t.GameLocalPeriod.StartsWith(item)
                            && t.aspnet_UserID == UserGuid
                        ).Sum(t => t.Result_Point))

                        - NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>

                          t.RemarkType == "下分"
                         && t.ChangeLocalDay.StartsWith(item)
                            && t.aspnet_UserID == UserGuid
                        ).Sum(t => t.ChangePoint))

                        - NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                                     t.RemarkType == "福利"
                                        && t.aspnet_UserID == UserGuid
                         && t.ChangeLocalDay.StartsWith(item)
                        ).Sum(t => t.ChangePoint))



                        );







                Result.Rows.Add(newr4);
                Result.Rows.Add(newr);


                Result.Rows.Add(newr2);

                Result.Rows.Add(newr5);
                Result.Rows.Add(newr6);
                Result.Rows.Add(newr3);

            }

            return Result;
        }//函数结束


        public static DataTable GetBounsSource(DateTime QueryDate, string SourceType)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            var buys = from ds in db.WX_UserGameLog
                       where ds.GameLocalPeriod.StartsWith(QueryDate.ToString("yyyyMMdd"))
                       && ds.aspnet_UserID == GlobalParam.UserKey
                       && ds.WX_SourceType == SourceType
                       select ds;
            DataTable Result = new DataTable();
            Result.Columns.Add("aspnet_UserID", typeof(Guid));
            Result.Columns.Add("WX_UserName");
            Result.Columns.Add("NickNameRemarkName");
            Result.Columns.Add("LocalPeriodDay");
            Result.Columns.Add("PeriodCount", typeof(decimal));
            Result.Columns.Add("TotalBuy", typeof(decimal));
            Result.Columns.Add("TotalResult", typeof(decimal));
            Result.Columns.Add("AverageBuy", typeof(decimal));

            Result.Columns.Add("FixNumber", typeof(decimal));
            Result.Columns.Add("FlowPercent", typeof(decimal));
            Result.Columns.Add("IfDivousPercent", typeof(decimal));

            Result.Columns.Add("BounsCount", typeof(decimal));
            Result.Columns.Add("Remark");

            var users = buys.Select(t => t.WX_UserName).Distinct();
            foreach (var usritem in users)
            {



                Linq.WX_UserReply contact = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == usritem && t.WX_SourceType == SourceType);
                DataRow newr = Result.NewRow();

                newr.SetField("aspnet_UserID", GlobalParam.UserKey);
                newr.SetField("WX_UserName", usritem);
                if (contact != null)
                {
                    newr.SetField("NickNameRemarkName", (contact.RemarkName != "" && contact.RemarkName != null ? contact.RemarkName + "@#" + contact.NickName : contact.NickName));
                    if (contact.IsCaculateFuli == true)
                    {
                        Result.Rows.Add(newr);
                    }
                }
                else
                {
                    newr.SetField("NickNameRemarkName", usritem);
                }
                newr.SetField("LocalPeriodDay", QueryDate.ToString("yyyyMMdd"));
                decimal PeriodCount = buys.Where(t => t.WX_UserName == usritem && t.WX_SourceType == SourceType).Select(t => t.GamePeriod).Distinct().Count();
                newr.SetField("PeriodCount", PeriodCount);
                decimal TotalBuy = NetFramework.Util_Math.NullToZero(buys.Where(t => t.WX_UserName == usritem && t.WX_SourceType == SourceType).Sum(t => t.Buy_Point));
                newr.SetField("TotalBuy",
                 TotalBuy
                  );
                decimal TotalResult = NetFramework.Util_Math.NullToZero(buys.Where(t => t.WX_UserName == usritem && t.WX_SourceType == SourceType).Sum(t => t.Result_Point - t.Buy_Point));
                newr.SetField("TotalResult",
                 TotalResult
                  );
                decimal AverageBuy = (PeriodCount == 0 ? 0 : TotalBuy / PeriodCount);
                newr.SetField("AverageBuy",
                    AverageBuy
                                 );

                var FindFuli = db.WX_BounsConfig.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                    && t.StartBuyPeriod <= PeriodCount
                    && (t.EndBuyPeriod >= PeriodCount || t.EndBuyPeriod == null)
                    && t.StartBuyAverage <= AverageBuy
                    && (t.EndBuyAverage > AverageBuy || t.EndBuyAverage == null)
                    );
                string Remark = "福利行数：" + FindFuli.Count().ToString();
                if (FindFuli.Count() != 0)
                {
                    newr.SetField("FixNumber", FindFuli.First().FixNumber);
                    newr.SetField("FlowPercent", FindFuli.First().FlowPercent);
                    newr.SetField("IfDivousPercent", FindFuli.First().IfDivousPercent);


                    Decimal FixBouns = NetFramework.Util_Math.NullToZero(FindFuli.First().FixNumber);
                    Remark += "福利分数:" + FixBouns.ToString();
                    Decimal TotalBouns = NetFramework.Util_Math.NullToZero(TotalBuy * NetFramework.Util_Math.NullToZero(FindFuli.First().FlowPercent, 6));
                    Remark += "总额返点:" + TotalBouns.ToString();
                    Decimal IfDivous = NetFramework.Util_Math.NullToZero((TotalResult < 0 ? -TotalResult : 0) * NetFramework.Util_Math.NullToZero(FindFuli.First().IfDivousPercent, 6));
                    Remark += "负返点:" + IfDivous.ToString();

                    decimal ResultNous = FixBouns;
                    if (ResultNous < TotalBouns)
                    {
                        ResultNous = TotalBouns;
                    }
                    if (ResultNous < IfDivous)
                    {
                        ResultNous = IfDivous;
                    }
                    newr.SetField("BounsCount", ResultNous);
                    newr.SetField("Remark", Remark);
                }
            }//用户结束
            return Result;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="SourceType"></param>
        /// <param name="QueryTime">20180831或20180801.20180831两种格式</param>
        /// <returns></returns>
        public static DataTable GetBossReportSource(string SourceType, string QueryTime)
        {
            DataTable Result = new DataTable();
            DateTime StartDate = DateTime.MaxValue;
            DateTime EndDate = DateTime.MinValue;
            try
            {
                if (QueryTime.Length == 8)
                {
                    StartDate = Convert.ToDateTime(QueryTime.Substring(0, 4) + "-" + QueryTime.Substring(4, 2) + "-" + QueryTime.Substring(6, 2));
                    EndDate = StartDate;
                }
                else if (QueryTime.Length == 17)
                {
                    string[] Times = QueryTime.Replace("。", ".").Replace("-", ".").Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    StartDate = Convert.ToDateTime(Times[0].Substring(0, 4) + "-" + Times[0].Substring(4, 2) + "-" + Times[0].Substring(6, 2));
                    EndDate = Convert.ToDateTime(Times[1].Substring(0, 4) + "-" + Times[1].Substring(4, 2) + "-" + Times[1].Substring(6, 2));



                }


            }
            catch (Exception)
            {
                throw new Exception(QueryTime + QueryTime + "日期格式错误");

            }

            Result.Columns.Add(QueryTime, typeof(string));

            Result.Columns.Add("上分", typeof(Int32));
            Result.Columns.Add("下分", typeof(Int32));
            Result.Columns.Add("福利", typeof(Int32));
            Result.Columns.Add("下注", typeof(Int32));
            Result.Columns.Add("得分", typeof(Int32));
            Result.Columns.Add("结果", typeof(Int32));
            Result.Columns.Add("期数", typeof(Int32));


            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");


            var Users = (from ds in db.WX_UserGameLog
                         join rpl in db.WX_UserReply on new { ds.WX_UserName, ds.WX_SourceType, ds.aspnet_UserID } equals new { rpl.WX_UserName, rpl.WX_SourceType, rpl.aspnet_UserID }
                         where (ds.aspnet_UserID == GlobalParam.UserKey
               && ds.WX_SourceType == SourceType
                && string.Compare(ds.GameLocalPeriod.Substring(0, 8), StartDate.ToString("yyyyMMdd")) >= 0
                    && string.Compare(ds.GameLocalPeriod.Substring(0, 8), EndDate.ToString("yyyyMMdd")) <= 0

               )
                         select new { rpl.NickName, rpl.RemarkName, rpl.WX_SourceType, rpl.WX_UserName }).Distinct();

            foreach (var item in Users)
            {
                DataRow newr = Result.NewRow();
                Result.Rows.Add(newr);
                newr.SetField<string>(QueryTime, item.RemarkName + "(" + item.NickName + ")");
                var channgs = db.WX_UserChangeLog.Where(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_SourceType == SourceType
                    && string.Compare(t.ChangeLocalDay, StartDate.ToString("yyyyMMdd")) >= 0
                    && string.Compare(t.ChangeLocalDay, EndDate.ToString("yyyyMMdd")) <= 0
                    && t.WX_UserName == item.WX_UserName
                    );

                var buys = db.WX_UserGameLog.Where(
                    t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_SourceType == SourceType
                    && string.Compare(t.GameLocalPeriod.Substring(0, 8), StartDate.ToString("yyyyMMdd")) >= 0
                    && string.Compare(t.GameLocalPeriod.Substring(0, 8), EndDate.ToString("yyyyMMdd")) <= 0
                    && t.WX_UserName == item.WX_UserName
                    );



                newr.SetField<Int32>("上分", MyConvert.ToInt32(channgs.Where(t => t.RemarkType == "上分").Sum(t => t.ChangePoint)));

                newr.SetField<Int32>("下分", MyConvert.ToInt32(channgs.Where(t => t.RemarkType == "下分").Sum(t => t.ChangePoint)));

                newr.SetField<Int32>("下注", MyConvert.ToInt32(buys.Sum(t => t.Buy_Point)));

                newr.SetField<Int32>("福利", MyConvert.ToInt32(channgs.Where(t => t.RemarkType == "福利").Sum(t => t.ChangePoint)));


                newr.SetField<Int32>("得分", MyConvert.ToInt32(buys.Sum(t => t.Result_Point)));

                newr.SetField<Int32>("结果", MyConvert.ToInt32(buys.Sum(t => t.Buy_Point - t.Result_Point)) - MyConvert.ToInt32(channgs.Where(t => t.RemarkType == "福利").Sum(t => t.ChangePoint)));


                newr.SetField<Int32>("期数", MyConvert.ToInt32(buys.Select(t => t.GameLocalPeriod).Distinct().Count()));


            }


            DataRow Totalnewr = Result.NewRow();
            Result.Rows.Add(Totalnewr);
            Totalnewr.SetField<string>(QueryTime, "合计");




            Totalnewr.SetField<Int32>("上分", Result.AsEnumerable().Sum(t => MyConvert.ToInt32(t.Field<object>("上分"))));

            Totalnewr.SetField<Int32>("下分", Result.AsEnumerable().Sum(t => MyConvert.ToInt32(t.Field<object>("下分"))));

            Totalnewr.SetField<Int32>("下注", Result.AsEnumerable().Sum(t => MyConvert.ToInt32(t.Field<object>("下注"))));

            Totalnewr.SetField<Int32>("得分", Result.AsEnumerable().Sum(t => MyConvert.ToInt32(t.Field<object>("得分"))));

            Totalnewr.SetField<Int32>("结果", Result.AsEnumerable().Sum(t => MyConvert.ToInt32(t.Field<object>("结果"))));

            Totalnewr.SetField<Int32>("福利", Result.AsEnumerable().Sum(t => MyConvert.ToInt32(t.Field<object>("福利"))));

            var totalbuys = db.WX_UserGameLog.Where(
                   t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_SourceType == SourceType
                   && string.Compare(t.GameLocalPeriod.Substring(0, 8), StartDate.ToString("yyyyMMdd")) >= 0
                   && string.Compare(t.GameLocalPeriod.Substring(0, 8), EndDate.ToString("yyyyMMdd")) <= 0
                   );


            Totalnewr.SetField<Int32>("期数", totalbuys.Select(t => t.GameLocalPeriod).Distinct().Count());




            return Result;
        }

        public class MyConvert
        {
            public static Int32 ToInt32(object param)
            {
                try
                {
                    return Convert.ToInt32(param);
                }
                catch (Exception)
                {

                    return 0;
                }
            }


        }

        public static Game_Result NewGameResult(string str_Win, string str_dataperiod, out bool NewDbResult)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            if (str_Win != "")
            {





                string str_win2 = str_Win;

                str_win2 = str_win2.Replace(" ", "");

                char[] numbs = str_win2.ToCharArray();
                int NUM1 = Convert.ToInt32(numbs[0].ToString());
                int NUM2 = Convert.ToInt32(numbs[1].ToString());
                int NUM3 = Convert.ToInt32(numbs[2].ToString());
                int NUM4 = Convert.ToInt32(numbs[3].ToString());
                int NUM5 = Convert.ToInt32(numbs[4].ToString());

                Int32 NumTotal = NUM1 + NUM2 + NUM3 + NUM4 + NUM5;


                String BigSmall = "";
                if (NumTotal == 23)
                {
                    BigSmall = "和";
                }
                else if (NumTotal <= 22)
                {
                    BigSmall = "小";
                }
                else
                {
                    BigSmall = "大";
                }

                string SingleDouble = "";
                if (NumTotal % 2 == 1)
                {
                    SingleDouble = "单";
                }
                else
                {
                    SingleDouble = "双";
                }


                string TigerDragon = "";
                if (NUM1 > NUM5)
                {
                    TigerDragon = "龙";
                }
                else if (NUM1 == NUM5)
                {
                    TigerDragon = "合";
                }
                else
                {
                    TigerDragon = "虎";
                }

                Linq.Game_ChongqingshishicaiPeriodMinute FindMinute = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.PeriodIndex == str_dataperiod.Substring(6, 3) && t.GameType == "重庆时时彩");


                var findGameResult = db.Game_Result.SingleOrDefault(t => t.GameName == "重庆时时彩" && t.GamePeriod == str_dataperiod && t.aspnet_UserID == GlobalParam.UserKey);
                if (findGameResult == null)
                {
                    Linq.Game_Result gr = new Linq.Game_Result();
                    gr.aspnet_UserID = GlobalParam.UserKey;
                    gr.GamePeriod = str_dataperiod;
                    gr.GameName = "重庆时时彩";
                    gr.GameResult = str_win2;
                    gr.NumTotal = NumTotal;
                    gr.BigSmall = BigSmall;
                    gr.SingleDouble = SingleDouble;
                    gr.DragonTiger = TigerDragon;
                    gr.GameTime = Convert.ToDateTime(
                       "20" + str_dataperiod.Substring(0, 2) + "-" + str_dataperiod.Substring(2, 2) + "-" + str_dataperiod.Substring(4, 2) + " "
                        + FindMinute.TimeMinute);
                    gr.aspnet_UserID = GlobalParam.UserKey;
                    gr.GamePrivatePeriod = Convert.ToDateTime(
                       "20" + str_dataperiod.Substring(0, 2) + "-" + str_dataperiod.Substring(2, 2) + "-" + str_dataperiod.Substring(4, 2) + " "
                       ).AddDays(Convert.ToDouble(FindMinute.Private_day)).ToString("yyyyMMdd") + FindMinute.Private_Period;
                    gr.InsertDate = DateTime.Now;
                    db.Game_Result.InsertOnSubmit(gr);
                    db.SubmitChanges();
                    NewDbResult = true;
                    return gr;

                }//插入数据库
                NewDbResult = false;
                return findGameResult;
            }
            NewDbResult = false;
            return null;
        }


        private static Int32 FindChineseBuyType(string OrderContext)
        {
            Int32 Result = -1;
            Result = OrderContext.IndexOf("主");
            if (Result > -1)
            {
                return Result;
            }

            Result = OrderContext.IndexOf("客");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("大球");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("小球");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("主主");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("主和");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("主客");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("和主");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("和和");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("和客");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("客主");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("客和");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("客客");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("1比0");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("0比1");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("2比0");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("0比2");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("2比1");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("1比2");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("3比0");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("0比3");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("3比1");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("1比3");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("3比2");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("2比3");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("4比0");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("0比4");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("4比1");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("1比4");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("4比2");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("2比4");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("4比3");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("3比4");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("0比0");
            if (Result > -1) { return Result; }

            Result = OrderContext.IndexOf("1比1");
            if (Result > -1) { return Result; }
            Result = OrderContext.IndexOf("2比2");
            if (Result > -1) { return Result; }
            Result = OrderContext.IndexOf("3比3");
            if (Result > -1) { return Result; }
            Result = OrderContext.IndexOf("4比4");
            if (Result > -1) { return Result; }
            Result = OrderContext.IndexOf("其他");
            if (Result > -1) { return Result; }

            return Result;
        }

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


        public static BallBuyType BallChinseToBuyType(string Str_BuyType)
        {
            switch (Str_BuyType)
            {
                case "主":
                    return BallBuyType.A_WIN;

                case "客":
                    return BallBuyType.B_WIN;

                case "大球":
                    return BallBuyType.BIGWIN;

                case "小球":
                    return BallBuyType.SMALLWIN;

                case "主主":
                    return BallBuyType.R_A_A;

                case "主和":
                    return BallBuyType.R_A_SAME;

                case "主客":
                    return BallBuyType.R_A_B;

                case "和主":
                    return BallBuyType.R_SAME_A;

                case "和和":
                    return BallBuyType.R_SAME_SAME;

                case "和客":
                    return BallBuyType.R_SAME_B;

                case "客主":
                    return BallBuyType.R_B_A;

                case "客和":
                    return BallBuyType.R_B_SAME;

                case "客客":
                    return BallBuyType.R_B_B;

                case "1比0":
                    return BallBuyType.R1_0_A;

                case "0比1":
                    return BallBuyType.R1_0_B;

                case "2比0":
                    return BallBuyType.R2_0_A;

                case "0比2":
                    return BallBuyType.R2_0_B;

                case "2比1":
                    return BallBuyType.R2_1_A;

                case "1比2":
                    return BallBuyType.R2_1_B;

                case "3比0":
                    return BallBuyType.R3_0_A;

                case "0比3":
                    return BallBuyType.R3_0_B;

                case "3比1":
                    return BallBuyType.R3_1_A;

                case "1比3":
                    return BallBuyType.R3_1_B;

                case "3比2":
                    return BallBuyType.R3_2_A;

                case "2比3":
                    return BallBuyType.R3_2_B;

                case "4比0":
                    return BallBuyType.R4_0_A;

                case "0比4":
                    return BallBuyType.R4_0_B;

                case "4比1":
                    return BallBuyType.R4_1_A;

                case "1比4":
                    return BallBuyType.R4_1_B;

                case "4比2":
                    return BallBuyType.R4_2_A;

                case "2比4":
                    return BallBuyType.R4_2_B;

                case "4比3":
                    return BallBuyType.R4_3_A;

                case "3比4":
                    return BallBuyType.R4_3_B;

                case "0比0":
                    return BallBuyType.R0_0;

                case "1比1":
                    return BallBuyType.R1_1;
                case "2比2":
                    return BallBuyType.R2_2;
                case "3比3":
                    return BallBuyType.R3_3;
                case "4比4":
                    return BallBuyType.R4_4;
                case "其他":
                    return BallBuyType.ROTHER;
                default:
                    return BallBuyType.UNKNOWN;

            }
        }

        public static string BallWinlessToNumber(string winless)
        {
            string result = "";
            result = winless.Replace("-", "受让");

            result = result.Replace("平手", "0")
                .Replace("半球", "0.5")
                .Replace("半", "0.5")
                .Replace("一球", "1")
                .Replace("两球", "2")
                .Replace("二球", "2")
                .Replace("三球", "3")
                .Replace("四球", "4")
                .Replace("五球", "5")
                .Replace("六球", "6")
                .Replace("七球", "7")
                .Replace("八球", "8")
                .Replace("九球", "9")
                .Replace("十球", "10")
                .Replace("球", "1")
                ;



            return (result.Contains("受让") ? "" : "让分") + result + "球   ";


        }

        public static string GetMatchGameString(Game_FootBall_VS matchitem, dbDataContext db, bool NeedSubRatio = true)
        {
            // IQueryable<Linq.Game_FootBall_VSRatios> Ratios = Linq.ProgramLogic.GameVSGetRatios(db, matchitem);

            string Result = Convert.ToDateTime(DateTime.Today.Year.ToString() + "-" + matchitem.GameTime).ToString("yyyy年MM月dd日 HH:mm") + Environment.NewLine;
            Result += matchitem.MatchClass + " " + matchitem.A_Team + "VS" + matchitem.B_Team + Environment.NewLine;
            Linq.Game_FootBall_VSRatios cur = VSGetCurRatio(matchitem, db);
            if (cur == null)
            {
                Result += Environment.NewLine;
                return Result;
            }
            Result += "主" + cur.A_WIN + "   " + Linq.ProgramLogic.BallWinlessToNumber(cur.Winless) + "   客" + cur.B_WIN + Environment.NewLine;
            Result += "大球" + cur.BIGWIN + "   " + "大小" + cur.Total + "   小球" + cur.SMALLWIN + Environment.NewLine;

            if (NeedSubRatio == true)
            {




                string NewTxt = "";
                NewTxt = (IsNullOrEmpty(cur.R1_0_A) ? "" : ("1-0:   " + cur.R1_0_A)) + (IsNullOrEmpty(cur.R1_0_B) ? "" : ("倍   0-1:   " + cur.R1_0_B + "倍"));
                Result += (NewTxt == "" ? "" : NewTxt + Environment.NewLine);
                NewTxt = (IsNullOrEmpty(cur.R2_0_A) ? "" : ("2-0:   " + cur.R2_0_A)) + (IsNullOrEmpty(cur.R2_0_B) ? "" : ("倍   0-2:   " + cur.R2_0_B + "倍"));
                Result += (NewTxt == "" ? "" : NewTxt + Environment.NewLine);
                NewTxt = (IsNullOrEmpty(cur.R2_1_A) ? "" : ("2-1:   " + cur.R2_1_A)) + (IsNullOrEmpty(cur.R2_1_B) ? "" : ("倍   1-2:   " + cur.R2_1_B + "倍"));
                Result += (NewTxt == "" ? "" : NewTxt + Environment.NewLine);
                NewTxt = (IsNullOrEmpty(cur.R3_0_A) ? "" : ("3-0:   " + cur.R3_0_A)) + (IsNullOrEmpty(cur.R3_0_B) ? "" : ("倍   0-3:   " + cur.R3_0_B + "倍"));
                Result += (NewTxt == "" ? "" : NewTxt + Environment.NewLine);
                NewTxt = (IsNullOrEmpty(cur.R3_1_A) ? "" : ("3-1:   " + cur.R3_1_A)) + (IsNullOrEmpty(cur.R3_1_B) ? "" : ("倍   1-3:   " + cur.R3_1_B + "倍"));
                Result += (NewTxt == "" ? "" : NewTxt + Environment.NewLine);
                NewTxt = (IsNullOrEmpty(cur.R3_2_A) ? "" : ("3-2:   " + cur.R3_2_A)) + (IsNullOrEmpty(cur.R3_2_B) ? "" : ("倍   2-3:   " + cur.R3_2_B + "倍"));
                Result += (NewTxt == "" ? "" : NewTxt + Environment.NewLine);
                NewTxt = (IsNullOrEmpty(cur.R4_0_A) ? "" : ("4-0:   " + cur.R4_0_A)) + (IsNullOrEmpty(cur.R4_0_B) ? "" : ("倍   0-4:   " + cur.R4_0_B + "倍"));
                Result += (NewTxt == "" ? "" : NewTxt + Environment.NewLine);
                NewTxt = (IsNullOrEmpty(cur.R4_1_A) ? "" : ("4-1:   " + cur.R4_1_A)) + (IsNullOrEmpty(cur.R4_1_B) ? "" : ("倍   1-4:   " + cur.R4_1_B + "倍"));
                Result += (NewTxt == "" ? "" : NewTxt + Environment.NewLine);
                NewTxt = (IsNullOrEmpty(cur.R4_2_A) ? "" : ("4-2:   " + cur.R4_2_A)) + (IsNullOrEmpty(cur.R4_2_B) ? "" : ("倍   2-4:   " + cur.R4_2_B + "倍"));
                Result += (NewTxt == "" ? "" : NewTxt + Environment.NewLine);
                NewTxt = (IsNullOrEmpty(cur.R4_3_A) ? "" : ("4-3:   " + cur.R4_3_A)) + (IsNullOrEmpty(cur.R4_3_B) ? "" : ("倍   3-4:   " + cur.R4_3_B + "倍"));
                Result += (NewTxt == "" ? "" : NewTxt + Environment.NewLine);
                NewTxt = (IsNullOrEmpty(cur.R0_0) ? "" : ("0-0:   " + cur.R0_0)) + (IsNullOrEmpty(cur.R1_1) ? "" : ("倍   1-1:   " + cur.R1_1 + "倍"));
                Result += (NewTxt == "" ? "" : NewTxt + Environment.NewLine);
                NewTxt = (IsNullOrEmpty(cur.R2_2) ? "" : ("2-2:   " + cur.R2_2)) + (IsNullOrEmpty(cur.R3_3) ? "" : ("倍   3-3:   " + cur.R3_3 + "倍"));
                Result += (NewTxt == "" ? "" : NewTxt + Environment.NewLine);
                NewTxt = (IsNullOrEmpty(cur.R4_4) ? "" : ("4-4：   " + cur.R4_4)) + (IsNullOrEmpty(cur.ROTHER) ? "" : ("倍   其他: " + cur.ROTHER + "倍"));
                Result += (NewTxt == "" ? "" : NewTxt + Environment.NewLine);
                NewTxt = (IsNullOrEmpty(cur.R_A_A) ? "" : ("主主: " + cur.R_A_A)) + (IsNullOrEmpty(cur.R_A_SAME) ? "" : ("倍 主和: " + cur.R_A_SAME)) + (IsNullOrEmpty(cur.R_A_B) ? "" : ("倍 主客: " + cur.R_A_B + "倍"));
                Result += (NewTxt == "" ? "" : NewTxt + Environment.NewLine);
                NewTxt = (IsNullOrEmpty(cur.R_B_A) ? "" : ("客主: " + cur.R_B_A)) + (IsNullOrEmpty(cur.R_B_SAME) ? "" : ("倍 客和: " + cur.R_B_SAME)) + (IsNullOrEmpty(cur.R_B_B) ? "" : ("倍 客客: " + cur.R_B_B + "倍"));
                Result += (NewTxt == "" ? "" : NewTxt + Environment.NewLine);
                NewTxt = (IsNullOrEmpty(cur.R_SAME_A) ? "" : ("和主: " + cur.R_SAME_A)) + (IsNullOrEmpty(cur.R_SAME_SAME) ? "" : ("倍 和和:   " + cur.R_SAME_SAME)) + (IsNullOrEmpty(cur.R_SAME_B) ? "" : ("倍 和客:   " + cur.R_SAME_B + "倍"));
                Result += (NewTxt == "" ? "" : NewTxt + Environment.NewLine);

            }

            Result += Environment.NewLine;
            return Result;
        }

        private static bool IsNullOrEmpty(string param)
        {
            return param == null || param == "" ? true : false;
        }
        public static string OpenBallGameLog(WX_UserGameLog_Football gl, dbDataContext db, Int32 fronthalf_A, Int32 fronthalf_B, Int32 endhalf_A, Int32 endhalf_B)
        {
            if (gl.HaveOpen == false)
            {
                string WhoWin = "";
                decimal rratio = CaculateRatio(gl, fronthalf_A, fronthalf_B, endhalf_A, endhalf_B, out WhoWin);
                decimal FrontRemainder = WXUserChangeLog_GetRemainder(gl.WX_UserName, gl.WX_SourceType);

                if (rratio > 0)
                {


                    gl.ResultMoney = gl.BuyMoney * gl.BuyRatio;
                    gl.HaveOpen = true;
                    WX_UserChangeLog cl = new WX_UserChangeLog();
                    cl.BuyValue = gl.BuyType;
                    cl.HaveNotice = false;
                    cl.NeedNotice = true;
                    cl.Remark = "开奖 " + gl.GameVS + " " + BallBuyTypeToChinseFrontShow(gl.BuyType) + " " + gl.BuyRatio + " " + gl.BuyMoney + " 上半" + fronthalf_A.ToString() + "-" + fronthalf_B.ToString() + ",下半" + endhalf_A.ToString() + "-" + endhalf_B.ToString() + "让球:" + gl.Winless + "总球" + gl.Total;
                    cl.RemarkType = "球赛开奖";
                    cl.WX_UserName = gl.WX_UserName;
                    cl.aspnet_UserID = gl.aspnet_UserID;
                    cl.GamePeriod = gl.GameKey;
                    cl.GameLocalPeriod = gl.GameVS;
                    cl.ChangeTime = DateTime.Now;
                    cl.FinalStatus = true;
                    cl.ChangePoint = gl.BuyMoney * rratio;
                    cl.WX_SourceType = gl.WX_SourceType;
                    db.WX_UserChangeLog.InsertOnSubmit(cl);
                    db.SubmitChanges();

                }
                else
                {
                    gl.ResultMoney = 0;
                    gl.HaveOpen = true;

                    db.SubmitChanges();
                }




                decimal Remainder = WXUserChangeLog_GetRemainder(gl.WX_UserName, gl.WX_SourceType);

                string Responsestr = "";

                Responsestr =

                    gl.A_Team + " VS " + gl.B_Team + Environment.NewLine
                    + "上半场" + fronthalf_A.ToString() + "-" + fronthalf_B.ToString() +
                    ",下半场" + endhalf_A.ToString() + "-" + endhalf_B.ToString()
                    + ",全场" + (fronthalf_A + endhalf_A).ToString() + "-" + (fronthalf_B + endhalf_B).ToString() + Environment.NewLine;

                Responsestr += WhoWin + Environment.NewLine;

                string Newwinless = "";
                if (gl.BuyType == "A_WIN" || gl.BuyType == "B_WIN")
                {
                    Newwinless = (BallWinlessToNumber(gl.Winless));
                }
                else
                {
                    Newwinless = "";
                }

                string NewTtoal = "";
                if (gl.BuyType == "BIGWIN" || gl.BuyType == "SMALLWIN")
                {
                    NewTtoal = "大小 " + gl.Total;
                }
                else
                {
                    NewTtoal = "";
                }



                Responsestr += BallBuyTypeToChinseFrontShow(gl.BuyType) + "" + gl.BuyMoney.ToString() + "，"
                      + Newwinless
                      + NewTtoal
                      + (Newwinless != "" && NewTtoal != "" ? "，" : "") + gl.BuyRatio + "水" + Environment.NewLine;

                return Responsestr + "计算前余" + FrontRemainder.ToString("N0") + ",现余" + Remainder.ToString("N0");





            }
            else return "";

        }

        public static decimal CaculateRatio(WX_UserGameLog_Football gl, Int32 fronthalf_A, Int32 fronthalf_B, Int32 endhalf_A, Int32 endhalf_B, out string WhoWin)
        {

            BallBuyType GameBuyType = (BallBuyType)Enum.Parse(typeof(BallBuyType), gl.BuyType, true);
            string CheckWhoWin = "";

            CaculateATemBTeamBigSmallWinless(
           BallBuyType.A_WIN
            , gl.Winless, gl.Total, (gl.BuyRatio.HasValue ? gl.BuyRatio.Value : 0), fronthalf_A, fronthalf_B, endhalf_A, endhalf_B, out CheckWhoWin);
            WhoWin = CheckWhoWin;

            CaculateATemBTeamBigSmallWinless(
           BallBuyType.BIGWIN
           , gl.Winless, gl.Total, (gl.BuyRatio.HasValue ? gl.BuyRatio.Value : 0), fronthalf_A, fronthalf_B, endhalf_A, endhalf_B, out CheckWhoWin);
            WhoWin += "," + CheckWhoWin;




            if ((GameBuyType == BallBuyType.R_A_A && (fronthalf_A > fronthalf_B && endhalf_A > endhalf_B))
        || (GameBuyType == BallBuyType.R_A_SAME && (fronthalf_A > fronthalf_B && endhalf_A == endhalf_B))
        || (GameBuyType == BallBuyType.R_A_B && (fronthalf_A > fronthalf_B && endhalf_A < endhalf_B))
        || (GameBuyType == BallBuyType.R_SAME_A && (fronthalf_A == fronthalf_B && endhalf_A > endhalf_B))
        || (GameBuyType == BallBuyType.R_SAME_SAME && (fronthalf_A == fronthalf_B && endhalf_A == endhalf_B))
        || (GameBuyType == BallBuyType.R_SAME_B && (fronthalf_A == fronthalf_B && endhalf_A < endhalf_B))
        || (GameBuyType == BallBuyType.R_B_A && (fronthalf_A < fronthalf_B && endhalf_A > endhalf_B))
        || (GameBuyType == BallBuyType.R_B_SAME && (fronthalf_A < fronthalf_B && endhalf_A == endhalf_B))
        || (GameBuyType == BallBuyType.R_B_B && (fronthalf_A < fronthalf_B && endhalf_A < endhalf_B))
        || (GameBuyType == BallBuyType.R1_0_A && (fronthalf_A + endhalf_A == 1 && fronthalf_B + endhalf_B == 0))
        || (GameBuyType == BallBuyType.R1_0_B && (fronthalf_A + endhalf_A == 0 && fronthalf_B + endhalf_B == 1))
        || (GameBuyType == BallBuyType.R2_0_A && (fronthalf_A + endhalf_A == 2 && fronthalf_B + endhalf_B == 0))
        || (GameBuyType == BallBuyType.R2_0_B && (fronthalf_A + endhalf_A == 0 && fronthalf_B + endhalf_B == 2))
        || (GameBuyType == BallBuyType.R2_1_A && (fronthalf_A + endhalf_A == 2 && fronthalf_B + endhalf_B == 1))
        || (GameBuyType == BallBuyType.R2_1_B && (fronthalf_A + endhalf_A == 1 && fronthalf_B + endhalf_B == 2))
        || (GameBuyType == BallBuyType.R3_0_A && (fronthalf_A + endhalf_A == 3 && fronthalf_B + endhalf_B == 0))
        || (GameBuyType == BallBuyType.R3_0_B && (fronthalf_A + endhalf_A == 0 && fronthalf_B + endhalf_B == 3))
        || (GameBuyType == BallBuyType.R3_1_A && (fronthalf_A + endhalf_A == 3 && fronthalf_B + endhalf_B == 1))
        || (GameBuyType == BallBuyType.R3_1_B && (fronthalf_A + endhalf_A == 1 && fronthalf_B + endhalf_B == 3))
        || (GameBuyType == BallBuyType.R3_2_A && (fronthalf_A + endhalf_A == 3 && fronthalf_B + endhalf_B == 2))
        || (GameBuyType == BallBuyType.R3_2_B && (fronthalf_A + endhalf_A == 2 && fronthalf_B + endhalf_B == 3))
        || (GameBuyType == BallBuyType.R4_0_A && (fronthalf_A + endhalf_A == 4 && fronthalf_B + endhalf_B == 0))
        || (GameBuyType == BallBuyType.R4_0_B && (fronthalf_A + endhalf_A == 0 && fronthalf_B + endhalf_B == 4))
        || (GameBuyType == BallBuyType.R4_1_A && (fronthalf_A + endhalf_A == 4 && fronthalf_B + endhalf_B == 1))
        || (GameBuyType == BallBuyType.R4_1_B && (fronthalf_A + endhalf_A == 1 && fronthalf_B + endhalf_B == 4))
        || (GameBuyType == BallBuyType.R4_2_A && (fronthalf_A + endhalf_A == 4 && fronthalf_B + endhalf_B == 2))
        || (GameBuyType == BallBuyType.R4_2_B && (fronthalf_A + endhalf_A == 2 && fronthalf_B + endhalf_B == 4))
        || (GameBuyType == BallBuyType.R4_3_A && (fronthalf_A + endhalf_A == 4 && fronthalf_B + endhalf_B == 3))
        || (GameBuyType == BallBuyType.R4_3_B && (fronthalf_A + endhalf_A == 3 && fronthalf_B + endhalf_B == 4))
        || (GameBuyType == BallBuyType.R0_0 && (fronthalf_A + endhalf_A == 0 && fronthalf_B + endhalf_B == 0))

        || (GameBuyType == BallBuyType.R1_1 && (fronthalf_A + endhalf_A == 1 && fronthalf_B + endhalf_B == 1))

        || (GameBuyType == BallBuyType.R2_2 && (fronthalf_A + endhalf_A == 2 && fronthalf_B + endhalf_B == 2))

        || (GameBuyType == BallBuyType.R3_3 && (fronthalf_A + endhalf_A == 3 && fronthalf_B + endhalf_B == 3))

        || (GameBuyType == BallBuyType.R4_4 && (fronthalf_A + endhalf_A == 4 && fronthalf_B + endhalf_B == 4))

        || (GameBuyType == BallBuyType.ROTHER &&
        (
        (fronthalf_A + endhalf_A > 4)
        || (fronthalf_B + endhalf_B > 4)
        ))
                )
            {
                WhoWin = CheckWhoWin;
                return gl.BuyRatio.Value;
            }
            else if (GameBuyType == BallBuyType.A_WIN || GameBuyType == BallBuyType.B_WIN || GameBuyType == BallBuyType.BIGWIN || GameBuyType == BallBuyType.SMALLWIN)
            {
                decimal CheckRatio = CaculateATemBTeamBigSmallWinless(
               GameBuyType
                , gl.Winless, gl.Total, (gl.BuyRatio.HasValue ? gl.BuyRatio.Value : 0), fronthalf_A, fronthalf_B, endhalf_A, endhalf_B, out CheckWhoWin);
                WhoWin = CheckWhoWin;
                return CheckRatio;

            }
            else
            {
                return 0;
            }





        }


        public static decimal CaculateATemBTeamBigSmallWinless(BallBuyType OrderBuyType, String Winless, string Total, decimal BuyRatio, Int32 fronthalf_A, Int32 fronthalf_B, Int32 endhalf_A, Int32 endhalf_B, out string WhoWin)
        {

            string Newwinless = (BallWinlessToNumber(Winless));
            string NewTotal = "大小:" + Total;

            if ((OrderBuyType == BallBuyType.A_WIN) || (OrderBuyType == BallBuyType.B_WIN))
            {
                decimal ADiviousB = fronthalf_A + endhalf_A - fronthalf_B - endhalf_B;
                string[] balls = Winless.Replace("受让", "").Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);


                if (Winless.Contains("/"))
                {
                    decimal ball1 = BallToCount(balls[0], Winless.Contains("受让") ? BallWinlessMode.受让 : BallWinlessMode.让);
                    decimal ball2 = BallToCount(balls[1], Winless.Contains("受让") ? BallWinlessMode.受让 : BallWinlessMode.让);



                    if (OrderBuyType == BallBuyType.A_WIN && ADiviousB - ball1 > 0 && ADiviousB - ball2 > 0)
                    {
                        WhoWin = Newwinless + ",主赢";
                        return BuyRatio;
                    }
                    else if (OrderBuyType == BallBuyType.A_WIN && ADiviousB - ball1 > 0 && ADiviousB - ball2 == 0)
                    {
                        WhoWin = Newwinless + ",主赢半";
                        return 1 + 0.5M * BuyRatio;
                    }
                    else if (OrderBuyType == BallBuyType.A_WIN && ADiviousB - ball1 == 0 && ADiviousB - ball2 < 0)
                    {
                        WhoWin = Newwinless + ",主输半";
                        return 0.5M;
                    }
                    else if (OrderBuyType == BallBuyType.A_WIN && ADiviousB - ball1 < 0 && ADiviousB - ball2 < 0)
                    {
                        WhoWin = Newwinless + ",主输";
                        return 0;
                    }


                    if (OrderBuyType == BallBuyType.B_WIN && ADiviousB - ball1 > 0 && ADiviousB - ball2 > 0)
                    {
                        WhoWin = Newwinless + ",客输";
                        return 0;
                    }
                    else if (OrderBuyType == BallBuyType.B_WIN && ADiviousB - ball1 > 0 && ADiviousB - ball2 == 0)
                    {
                        WhoWin = Newwinless + ",客输半";
                        return 0.5M;
                    }
                    else if (OrderBuyType == BallBuyType.B_WIN && ADiviousB - ball1 == 0 && ADiviousB - ball2 < 0)
                    {
                        WhoWin = Newwinless + ",客赢半";
                        return 1 + 0.5M * BuyRatio;
                    }
                    else if (OrderBuyType == BallBuyType.B_WIN && ADiviousB - ball1 < 0 && ADiviousB - ball2 < 0)
                    {
                        WhoWin = Newwinless + ",客赢";
                        return BuyRatio;
                    }
                    else
                    {
                        WhoWin = "判断错误" + Enum.GetName(typeof(BallBuyType), OrderBuyType) + "让分:" + Winless + "上半场" + fronthalf_A.ToString() + "-" + fronthalf_B.ToString() + "下半场" + endhalf_A.ToString() + "-" + endhalf_B.ToString();
                        return 0;
                    }


                }//带半球
                else
                {
                    decimal ball = BallToCount(Winless, Winless.Contains("受让") ? BallWinlessMode.受让 : BallWinlessMode.让);
                    if (ADiviousB - ball == 0)
                    {
                        WhoWin = Newwinless + ",平手";
                        return 1;
                    }
                    else if (ADiviousB - ball > 0 && OrderBuyType == BallBuyType.A_WIN)
                    {
                        WhoWin = Newwinless + ",主赢";
                        return BuyRatio;
                    }
                    else if (ADiviousB - ball > 0 && OrderBuyType == BallBuyType.B_WIN)
                    {
                        WhoWin = Newwinless + ",客输";
                        return 0;
                    }
                    else if (ADiviousB - ball < 0 && OrderBuyType == BallBuyType.B_WIN)
                    {
                        WhoWin = Newwinless + ",客赢";
                        return BuyRatio;
                    }
                    else if (ADiviousB - ball < 0 && OrderBuyType == BallBuyType.A_WIN)
                    {
                        WhoWin = Newwinless + ",主输";
                        return 0;
                    }
                    else
                    {
                        WhoWin = "判断错误" + Enum.GetName(typeof(BallBuyType), OrderBuyType) + "让分:" + Winless.ToString() + "上半场" + fronthalf_A.ToString() + "-" + fronthalf_B.ToString() + "下半场" + endhalf_A.ToString() + "-" + endhalf_B.ToString();
                        return 0;
                    }
                }//不带半球


            }
            else if ((OrderBuyType == BallBuyType.BIGWIN) || (OrderBuyType == BallBuyType.SMALLWIN))
            {
                string[] balls = Total.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                decimal TotalBall = fronthalf_A + fronthalf_B + endhalf_A + endhalf_B;
                if (Total.Contains("/"))
                {
                    decimal ball1 = Convert.ToDecimal(balls[0]);
                    decimal ball2 = Convert.ToDecimal(balls[1]);

                    if (OrderBuyType == BallBuyType.BIGWIN && TotalBall - ball1 > 0 && TotalBall - ball2 > 0)
                    {
                        WhoWin = NewTotal + ",大球赢";
                        return BuyRatio;
                    }
                    else if (OrderBuyType == BallBuyType.BIGWIN && TotalBall - ball1 > 0 && TotalBall - ball2 == 0)
                    {
                        WhoWin = NewTotal + ",大球赢半";
                        return 1 + 0.5M * BuyRatio;
                    }
                    else if (OrderBuyType == BallBuyType.BIGWIN && TotalBall - ball1 == 0 && TotalBall - ball2 < 0)
                    {
                        WhoWin = NewTotal + ",大球输半";
                        return 0.5M;
                    }
                    else if (OrderBuyType == BallBuyType.BIGWIN && TotalBall - ball1 < 0 && TotalBall - ball2 < 0)
                    {
                        WhoWin = NewTotal + ",大球输";
                        return 0;
                    }


                    if (OrderBuyType == BallBuyType.SMALLWIN && TotalBall - ball1 > 0 && TotalBall - ball2 > 0)
                    {
                        WhoWin = NewTotal + ",小球输";
                        return 0;
                    }
                    else if (OrderBuyType == BallBuyType.SMALLWIN && TotalBall - ball1 > 0 && TotalBall - ball2 == 0)
                    {
                        WhoWin = NewTotal + ",小球输半";
                        return 0.5M;
                    }
                    else if (OrderBuyType == BallBuyType.SMALLWIN && TotalBall - ball1 == 0 && TotalBall - ball2 < 0)
                    {
                        WhoWin = NewTotal + ",小球赢半";
                        return 1 + 0.5M * BuyRatio;
                    }
                    else if (OrderBuyType == BallBuyType.SMALLWIN && TotalBall - ball1 < 0 && TotalBall - ball2 < 0)
                    {
                        WhoWin = NewTotal + ",小球赢";
                        return BuyRatio;
                    }
                    else
                    {
                        WhoWin = "判断错误" + Enum.GetName(typeof(BallBuyType), OrderBuyType) + "指数:" + TotalBall.ToString() + "上半场" + fronthalf_A.ToString() + "-" + fronthalf_B.ToString() + "下半场" + endhalf_A.ToString() + "-" + endhalf_B.ToString();
                        return 0;
                    }

                }//总球数带半球
                else
                {
                    decimal ball = Convert.ToDecimal(Total);
                    if (TotalBall - ball == 0)
                    {
                        WhoWin = NewTotal + ",平手";
                        return 1;
                    }
                    else if (TotalBall - ball > 0 && OrderBuyType == BallBuyType.BIGWIN)
                    {
                        WhoWin = NewTotal + ", 大球赢";
                        return BuyRatio;
                    }
                    else if (TotalBall - ball > 0 && OrderBuyType == BallBuyType.SMALLWIN)
                    {
                        WhoWin = NewTotal + ",小球输";
                        return 0;
                    }
                    else if (TotalBall - ball < 0 && OrderBuyType == BallBuyType.SMALLWIN)
                    {
                        WhoWin = NewTotal + ",小球赢";
                        return BuyRatio;
                    }
                    else if (TotalBall - ball < 0 && OrderBuyType == BallBuyType.BIGWIN)
                    {
                        WhoWin = NewTotal + ",大球输";
                        return 0;
                    }
                    else
                    {
                        WhoWin = "判断错误" + Enum.GetName(typeof(BallBuyType), OrderBuyType) + "指数:" + TotalBall.ToString() + "上半场" + fronthalf_A.ToString() + "-" + fronthalf_B.ToString() + "下半场" + endhalf_A.ToString() + "-" + endhalf_B.ToString();
                        return 0;
                    }

                }//总球数不带半球
            }
            //其他波胆
            else
            {
                WhoWin = "";
                return 0;
            }
        }

        public enum BallBuyType
        {
            A_WIN, B_WIN, BIGWIN, SMALLWIN,
            R_A_A, R_A_SAME,
            R_A_B
                , R_SAME_A, R_SAME_SAME,
            R_SAME_B
                , R_B_A, R_B_SAME, R_B_B, R1_0_A,
            R1_0_B, R2_0_A, R2_0_B, R2_1_A,
            R2_1_B, R3_0_A, R3_0_B, R3_1_A, R3_1_B, R3_2_A, R3_2_B, R4_0_A, R4_0_B, R4_1_A, R4_1_B,
            R4_2_A
                , R4_2_B, R4_3_A, R4_3_B, R0_0, R1_1, R2_2, R3_3, R4_4, ROTHER, UNKNOWN
        }
        public enum BallWinlessMode { 让, 受让, 总球 }
        public static decimal BallToCount(string ballname, BallWinlessMode mode)
        {
            decimal prefix = (mode == BallWinlessMode.受让 ? -1 : 1);
            ballname = ballname.Replace("受让", "");
            switch (ballname)
            {
                case "平手":
                    return prefix * 0;
                case "半球":
                    return prefix * 0.5M;
                case "球":
                    return prefix * 1;
                case "一球":
                    return prefix * 1;
                case "球半":
                    return prefix * 1.5M;
                case "一球半":
                    return prefix * 1.5M;
                case "两球":
                    return prefix * 2;
                case "两球半":
                    return prefix * 2.5M;
                case "三球":
                    return prefix * 3;
                case "三球半":
                    return prefix * 3.5M;

                case "四球":
                    return prefix * 4;
                case "四球半":
                    return prefix * 4.5M;


                case "五球":
                    return prefix * 5;
                case "五球半":
                    return prefix * 5.5M;

                case "六球":
                    return prefix * 6;
                case "六球半":
                    return prefix * 6.5M;

                case "七球":
                    return prefix * 7;
                case "七球半":
                    return prefix * 7.5M;

                case "八球":
                    return prefix * 8;
                case "八球半":
                    return prefix * 8.5M;

                case "九球":
                    return prefix * 9;
                case "九球半":
                    return prefix * 9.5M;

                case "十球":
                    return prefix * 10;
                case "十球半":
                    return prefix * 10.5M;
                default:
                    try
                    {
                        return Convert.ToDecimal(ballname);
                    }
                    catch (Exception)
                    {

                        return 0;
                    }


            }
        }

        private static WX_UserGameLog_Football[] ContentToGameLogBall(WX_UserReplyLog reply, object[] MatchList, string Str_ChineseBuyType, string Str_BuyMoney, out Int32 succhess, FormatResultDirection direct, dbDataContext db)
        {


            decimal buymoney = 0;
            try
            {
                buymoney = Convert.ToDecimal(Str_BuyMoney);
            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine(AnyError.Message);
                NetFramework.Console.WriteLine(AnyError.StackTrace);
                succhess = 0;
                return null;
            }

            if (MatchList.Count() > 1)
            {
                succhess = 2;
                return null;
            }
            else if (MatchList.Count() == 0)
            {
                succhess = 2;
                return null;
            }
            else
            {
                if (direct == FormatResultDirection.MemoryMatchList)
                {
                    //List<Linq.Game_FootBall_VSRatios> DbRatios = Linq.ProgramLogic.GameVSGetRatios(db, ((Game_FootBall_VS[])MatchList).First()).ToList();

                    Game_FootBall_VSRatios inr = VSGetCurRatio(((Game_FootBall_VS[])MatchList).First(), db);
                    WX_UserGameLog_Football gl = new WX_UserGameLog_Football();
                    gl.aspnet_UserID = GlobalParam.UserKey;
                    gl.WX_UserName = reply.WX_UserName;
                    gl.WX_SourceType = reply.WX_SourceType;
                    gl.BuyMoney = buymoney;
                    gl.BuyType = Enum.GetName(typeof(BallBuyType), BallChinseToBuyType(Str_ChineseBuyType));
                    gl.GameTime = ((Game_FootBall_VS[])MatchList).First().GameTime;
                    gl.GameKey = ((Game_FootBall_VS[])MatchList).First().GameKey;

                    gl.GameKey = ((Game_FootBall_VS[])MatchList).First().GameKey;
                    gl.GameVS = ((Game_FootBall_VS[])MatchList).First().GameVS;

                    gl.MatchClass = ((Game_FootBall_VS[])MatchList).First().MatchClass;

                    gl.HaveOpen = false;
                    gl.ResultMoney = null;
                    gl.transtime = reply.ReceiveTime;

                    gl.A_Team = ((Game_FootBall_VS[])MatchList).First().A_Team;
                    gl.B_Team = ((Game_FootBall_VS[])MatchList).First().B_Team;

                    if (gl.BuyType == "")
                    {
                        succhess = 0;

                        return null;
                    }
                    switch (gl.BuyType)
                    {
                        case "A_WIN": gl.BuyRatio = ObjectToDecimal(inr.A_WIN); break;
                        case "B_WIN": gl.BuyRatio = ObjectToDecimal(inr.B_WIN); break;
                        case "BIGWIN": gl.BuyRatio = ObjectToDecimal(inr.BIGWIN); break;
                        case "SMALLWIN": gl.BuyRatio = ObjectToDecimal(inr.SMALLWIN); break;



                        case "R1_0_A": gl.BuyRatio = ObjectToDecimal(inr.R1_0_A); break;
                        case "R1_0_B": gl.BuyRatio = ObjectToDecimal(inr.R1_0_B); break;

                        case "R2_0_A": gl.BuyRatio = ObjectToDecimal(inr.R2_0_A); break;
                        case "R2_0_B": gl.BuyRatio = ObjectToDecimal(inr.R2_0_B); break;

                        case "R2_1_A": gl.BuyRatio = ObjectToDecimal(inr.R2_1_A); break;
                        case "R2_1_B": gl.BuyRatio = ObjectToDecimal(inr.R2_1_B); break;

                        case "R3_0_A": gl.BuyRatio = ObjectToDecimal(inr.R3_0_A); break;
                        case "R3_0_B": gl.BuyRatio = ObjectToDecimal(inr.R3_0_B); break;

                        case "R3_1_A": gl.BuyRatio = ObjectToDecimal(inr.R3_1_A); break;
                        case "R3_1_B": gl.BuyRatio = ObjectToDecimal(inr.R3_1_B); break;

                        case "R3_2_A": gl.BuyRatio = ObjectToDecimal(inr.R3_2_A); break;
                        case "R3_2_B": gl.BuyRatio = ObjectToDecimal(inr.R3_2_B); break;

                        case "R4_0_A": gl.BuyRatio = ObjectToDecimal(inr.R4_0_A); break;
                        case "R4_0_B": gl.BuyRatio = ObjectToDecimal(inr.R4_0_B); break;

                        case "R4_1_A": gl.BuyRatio = ObjectToDecimal(inr.R4_1_A); break;
                        case "R4_1_B": gl.BuyRatio = ObjectToDecimal(inr.R4_1_B); break;

                        case "R4_2_A": gl.BuyRatio = ObjectToDecimal(inr.R4_2_A); break;
                        case "R4_2_B": gl.BuyRatio = ObjectToDecimal(inr.R4_2_B); break;

                        case "R4_3_A": gl.BuyRatio = ObjectToDecimal(inr.R4_3_A); break;
                        case "R4_3_B": gl.BuyRatio = ObjectToDecimal(inr.R4_3_B); break;

                        case "R0_0": gl.BuyRatio = ObjectToDecimal(inr.R0_0); break;
                        case "R1_1": gl.BuyRatio = ObjectToDecimal(inr.R1_1); break;
                        case "R2_2": gl.BuyRatio = ObjectToDecimal(inr.R2_2); break;
                        case "R3_3": gl.BuyRatio = ObjectToDecimal(inr.R3_3); break;
                        case "R4_4": gl.BuyRatio = ObjectToDecimal(inr.R4_4); break;
                        case "ROTHER": gl.BuyRatio = ObjectToDecimal(inr.ROTHER); break;



                        case "R_A_A": gl.BuyRatio = ObjectToDecimal(inr.R_A_A); break;
                        case "R_A_SAME": gl.BuyRatio = ObjectToDecimal(inr.R_A_SAME); break;
                        case "R_A_B": gl.BuyRatio = ObjectToDecimal(inr.R_A_B); break;
                        case "R_SAME_A": gl.BuyRatio = ObjectToDecimal(inr.R_SAME_A); break;
                        case "R_SAME_SAME": gl.BuyRatio = ObjectToDecimal(inr.R_SAME_SAME); break;
                        case "R_SAME_B": gl.BuyRatio = ObjectToDecimal(inr.R_SAME_B); break;
                        case "R_B_A": gl.BuyRatio = ObjectToDecimal(inr.R_B_A); break;
                        case "R_B_SAME": gl.BuyRatio = ObjectToDecimal(inr.R_B_SAME); break;
                        case "R_B_B": gl.BuyRatio = ObjectToDecimal(inr.R_B_B); break;
                        default:
                            succhess = 0;

                            return null;

                    }//转化购买类型


                    #region 保存下单时的赔率
                    gl.A_WIN = ObjectToDecimal(inr.A_WIN);
                    gl.B_WIN = ObjectToDecimal(inr.B_WIN);
                    gl.BIGWIN = ObjectToDecimal(inr.BIGWIN);
                    gl.SMALLWIN = ObjectToDecimal(inr.SMALLWIN);

                    gl.Winless = inr.Winless;
                    gl.Total = inr.Total;



                    gl.R1_0_A = ObjectToDecimal(inr.R1_0_A);
                    gl.R1_0_B = ObjectToDecimal(inr.R1_0_B);

                    gl.R2_0_A = ObjectToDecimal(inr.R2_0_A);
                    gl.R2_0_B = ObjectToDecimal(inr.R2_0_B);

                    gl.R2_1_A = ObjectToDecimal(inr.R2_1_A);
                    gl.R2_1_B = ObjectToDecimal(inr.R2_1_B);

                    gl.R3_0_A = ObjectToDecimal(inr.R3_0_A);
                    gl.R3_0_B = ObjectToDecimal(inr.R3_0_B);

                    gl.R3_1_A = ObjectToDecimal(inr.R3_1_A);
                    gl.R3_1_B = ObjectToDecimal(inr.R3_1_B);

                    gl.R3_2_A = ObjectToDecimal(inr.R3_2_A);
                    gl.R3_2_B = ObjectToDecimal(inr.R3_2_B);

                    gl.R4_0_A = ObjectToDecimal(inr.R4_0_A);
                    gl.R4_0_B = ObjectToDecimal(inr.R4_0_B);

                    gl.R4_1_A = ObjectToDecimal(inr.R4_1_A);
                    gl.R4_1_B = ObjectToDecimal(inr.R4_1_B);

                    gl.R4_2_A = ObjectToDecimal(inr.R4_2_A);
                    gl.R4_2_B = ObjectToDecimal(inr.R4_2_B);

                    gl.R4_3_A = ObjectToDecimal(inr.R4_3_A);
                    gl.R4_3_B = ObjectToDecimal(inr.R4_3_B);

                    gl.R0_0 = ObjectToDecimal(inr.R0_0);
                    gl.R1_1 = ObjectToDecimal(inr.R1_1);
                    gl.R2_2 = ObjectToDecimal(inr.R2_2);
                    gl.R3_3 = ObjectToDecimal(inr.R3_3);
                    gl.R4_4 = ObjectToDecimal(inr.R4_4);
                    gl.ROTHER = ObjectToDecimal(inr.ROTHER);



                    gl.R_A_A = ObjectToDecimal(inr.R_A_A);
                    gl.R_A_SAME = ObjectToDecimal(inr.R_A_SAME);
                    gl.R_A_B = ObjectToDecimal(inr.R_A_B);
                    gl.R_SAME_A = ObjectToDecimal(inr.R_SAME_A);
                    gl.R_SAME_SAME = ObjectToDecimal(inr.R_SAME_SAME);
                    gl.R_SAME_B = ObjectToDecimal(inr.R_SAME_B);
                    gl.R_B_A = ObjectToDecimal(inr.R_B_A);
                    gl.R_B_SAME = ObjectToDecimal(inr.R_B_SAME);
                    gl.R_B_B = ObjectToDecimal(inr.R_B_B);
                    #endregion

                    if (gl.BuyRatio == 0)
                    {
                        succhess = 0;

                        return null;
                    }
                    succhess = 1;
                    return new WX_UserGameLog_Football[] { gl };
                }//从内存赛事查数
                else if (direct == FormatResultDirection.DataBaseGameLog)
                {
                    succhess = 1;
                    return ((WX_UserGameLog_Football[])MatchList).Where(
                        t => t.aspnet_UserID == GlobalParam.UserKey
                            && t.WX_UserName == reply.WX_UserName
                            && t.WX_SourceType == reply.WX_SourceType
                            && t.BuyType == Enum.GetName(typeof(BallBuyType), BallChinseToBuyType(Str_ChineseBuyType))
                        ).ToArray();
                }
                else
                {
                    succhess = 0;
                    return null;
                }
            }//内存或数据库赛事行数是1的

        }

        //public class Game_FootBall_VS
        //{
        //    private string _GameKey = "";
        //    private string _A_Team = "";
        //    private string _B_Team = "";
        //    private List<Game_FootBall_VSRatios> _ratios = new List<Game_FootBall_VSRatios>();
        //    private string _GameType = "";
        //    private string _HeadDiv = "";
        //    private string _RowData = "";


        //    private string _GameVS = "";

        //    private Int32 _CurRatioCount = 1;

        //    private string _GameTime = "";
        //    private string _MatchClass = "";

        //    public string GameKey { get { return _GameKey; } set { _GameKey = value; } }
        //    public string A_Team { get { return _A_Team; } set { _A_Team = value; } }
        //    public string B_Team { get { return _B_Team; } set { _B_Team = value; } }
        //    public List<Game_FootBall_VSRatios> ratios { get { return _ratios; } set { _ratios = value; } }
        //    public string GameType { get { return _GameType; } set { _GameType = value; } }
        //    public string HeadDiv { get { return _HeadDiv; } set { _HeadDiv = value; } }
        //    public string RowData { get { return _RowData; } set { _RowData = value; } }

        //    public string RowDataWithName { get; set; }

        //    public string GameVS { get { return _GameVS; } set { _GameVS = value; } }

        //    public Int32 CurRatioCount { get { return _CurRatioCount; } set { _CurRatioCount = value; } }

        //    public string GameTime { get { return _GameTime; } set { _GameTime = value; } }
        //    public string MatchClass { get { return _MatchClass; } set { _MatchClass = value; } }


        //}

        //public class Game_FootBall_VSRatios
        //{


        //    private string _A_WIN = "";
        //    private string _Winless = "";
        //    private string _B_WIN = "";
        //    private string _BIGWIN = "";
        //    private string _Total = "";
        //    private string _SMALLWIN = "";
        //    private string _RatioType = "";


        //    private string _R1_0_A = "";
        //    private string _R1_0_B = "";

        //    private string _R2_0_A = "";
        //    private string _R2_0_B = "";

        //    private string _R2_1_A = "";
        //    private string _R2_1_B = "";

        //    private string _R3_0_A = "";
        //    private string _R3_0_B = "";

        //    private string _R3_1_A = "";
        //    private string _R3_1_B = "";

        //    private string _R3_2_A = "";
        //    private string _R3_2_B = "";

        //    private string _R4_0_A = "";
        //    private string _R4_0_B = "";

        //    private string _R4_1_A = "";
        //    private string _R4_1_B = "";

        //    private string _R4_2_A = "";
        //    private string _R4_2_B = "";

        //    private string _R4_3_A = "";
        //    private string _R4_3_B = "";

        //    private string _R0_0 = "";
        //    private string _R1_1 = "";
        //    private string _R2_2 = "";
        //    private string _R3_3 = "";
        //    private string _R4_4 = "";
        //    private string _ROTHER = "";



        //    private string _R_A_A = "";
        //    private string _R_A_SAME = "";
        //    private string _R_A_B = "";
        //    private string _R_SAME_A = "";
        //    private string _R_SAME_SAME = "";
        //    private string _R_SAME_B = "";
        //    private string _R_B_A = "";
        //    private string _R_B_SAME = "";
        //    private string _R_B_B = "";

        //    private Int32 _rowtypeindex = 0;



        //    public string A_WIN { get { return _A_WIN; } set { _A_WIN = value; } }
        //    public string Winless { get { return _Winless; } set { _Winless = value; } }
        //    public string B_WIN { get { return _B_WIN; } set { _B_WIN = value; } }
        //    public string BIGWIN { get { return _BIGWIN; } set { _BIGWIN = value; } }
        //    public string Total { get { return _Total; } set { _Total = value; } }
        //    public string SMALLWIN { get { return _SMALLWIN; } set { _SMALLWIN = value; } }
        //    public string RatioType { get { return _RatioType; } set { _RatioType = value; } }


        //    public string R1_0_A { get { return _R1_0_A; } set { _R1_0_A = value; } }
        //    public string R1_0_B { get { return _R1_0_B; } set { _R1_0_B = value; } }

        //    public string R2_0_A { get { return _R2_0_A; } set { _R2_0_A = value; } }
        //    public string R2_0_B { get { return _R2_0_B; } set { _R2_0_B = value; } }

        //    public string R2_1_A { get { return _R2_1_A; } set { _R2_1_A = value; } }
        //    public string R2_1_B { get { return _R2_1_B; } set { _R2_1_B = value; } }

        //    public string R3_0_A { get { return _R3_0_A; } set { _R3_0_A = value; } }
        //    public string R3_0_B { get { return _R3_0_B; } set { _R3_0_B = value; } }

        //    public string R3_1_A { get { return _R3_1_A; } set { _R3_1_A = value; } }
        //    public string R3_1_B { get { return _R3_1_B; } set { _R3_1_B = value; } }

        //    public string R3_2_A { get { return _R3_2_A; } set { _R3_2_A = value; } }
        //    public string R3_2_B { get { return _R3_2_B; } set { _R3_2_B = value; } }

        //    public string R4_0_A { get { return _R4_0_A; } set { _R4_0_A = value; } }
        //    public string R4_0_B { get { return _R4_0_B; } set { _R4_0_B = value; } }

        //    public string R4_1_A { get { return _R4_1_A; } set { _R4_1_A = value; } }
        //    public string R4_1_B { get { return _R4_1_B; } set { _R4_1_B = value; } }

        //    public string R4_2_A { get { return _R4_2_A; } set { _R4_2_A = value; } }
        //    public string R4_2_B { get { return _R4_2_B; } set { _R4_2_B = value; } }

        //    public string R4_3_A { get { return _R4_3_A; } set { _R4_3_A = value; } }
        //    public string R4_3_B { get { return _R4_3_B; } set { _R4_3_B = value; } }

        //    public string R0_0 { get { return _R0_0; } set { _R0_0 = value; } }
        //    public string R1_1 { get { return _R1_1; } set { _R1_1 = value; } }
        //    public string R2_2 { get { return _R2_2; } set { _R2_2 = value; } }
        //    public string R3_3 { get { return _R3_3; } set { _R3_3 = value; } }
        //    public string R4_4 { get { return _R4_4; } set { _R4_4 = value; } }
        //    public string ROTHER { get { return _ROTHER; } set { _ROTHER = value; } }



        //    public string R_A_A { get { return _R_A_A; } set { _R_A_A = value; } }
        //    public string R_A_SAME { get { return _R_A_SAME; } set { _R_A_SAME = value; } }
        //    public string R_A_B { get { return _R_A_B; } set { _R_A_B = value; } }
        //    public string R_SAME_A { get { return _R_SAME_A; } set { _R_SAME_A = value; } }
        //    public string R_SAME_SAME { get { return _R_SAME_SAME; } set { _R_SAME_SAME = value; } }
        //    public string R_SAME_B { get { return _R_SAME_B; } set { _R_SAME_B = value; } }
        //    public string R_B_A { get { return _R_B_A; } set { _R_B_A = value; } }
        //    public string R_B_SAME { get { return _R_B_SAME; } set { _R_B_SAME = value; } }
        //    public string R_B_B { get { return _R_B_B; } set { _R_B_B = value; } }

        //    public Int32 rowtypeindex { get { return _rowtypeindex; } set { _rowtypeindex = value; } }

        //    public CompanyType RCompanyType { get; set; }



        //}
        public enum BallCompanyType { 澳彩, 皇冠 }
        //public class c_vstype
        //{
        //    public c_vstype(string p_MatchBallType, string p_MatchClassName)
        //    {
        //        GameType = p_MatchBallType;
        //        MatchClass = p_MatchClassName;
        //    }
        //    public string GameType { get; set; }
        //    public string MatchClass { get; set; }

        //    public override string ToString()
        //    {
        //        return GameType + MatchClass;
        //    }
        //}


        private static WX_UserGameLog_HKSix ContentToHKSix(WX_UserReplyLog reply, dbDataContext db, out bool Success)
        {
            WX_UserGameLog_HKSix result = new WX_UserGameLog_HKSix();
            result.aspnet_UserID = GlobalParam.UserKey;
            result.WX_UserName = reply.WX_UserName;
            result.WX_SourceType = reply.WX_SourceType;
            result.HaveOpen = false;
            result.ResultMoney = null;

            string NewContent = reply.ReceiveContent.Replace("六", "").Replace(" ", "");
            Regex FindMoney = new Regex("[0-9]+", RegexOptions.IgnoreCase);
            string[] typesandmoney = NewContent.Split("#".ToCharArray());
            string strfindMoney = (typesandmoney.Length > 1 ? typesandmoney[1] : "");

            Int32 BuyNumber = 0;
            decimal BuyMoney = 0;
            try
            {
                BuyNumber = Convert.ToInt32(typesandmoney[0]);
            }
            catch (Exception)
            {

            }

            try
            {
                BuyMoney = Convert.ToDecimal(strfindMoney);
            }
            catch (Exception)
            {

                if (BuyMoney == 0)
                {
                    Success = false;
                    return null;
                }
            }

            if (typesandmoney[0] == "单")
            {
                result.BuyType = Enum.GetName(typeof(HKSixBuyType), HKSixBuyType.单双);
                result.BuyValue = "单";
                Success = true;
            }
            else if (typesandmoney[0] == "双")
            {
                result.BuyType = Enum.GetName(typeof(HKSixBuyType), HKSixBuyType.单双);
                result.BuyValue = "双";
                Success = true;
            }
            else if (typesandmoney[0] == "大")
            {
                result.BuyType = Enum.GetName(typeof(HKSixBuyType), HKSixBuyType.大小);
                result.BuyValue = "大";
                Success = true;
            }
            else if (typesandmoney[0] == "小")
            {
                result.BuyType = Enum.GetName(typeof(HKSixBuyType), HKSixBuyType.大小);
                result.BuyValue = "小";
                Success = true;
            }
            else if (BuyNumber > 0 && BuyNumber <= 49)
            {
                result.BuyType = Enum.GetName(typeof(HKSixBuyType), HKSixBuyType.特码);
                result.BuyValue = BuyNumber.ToString();
                Success = true;
            }
            else
            {
                Success = false;
                return null;
            }

            #region
            result.BuyMoney = BuyMoney;
            #endregion
            return result;
        }

        public static string GetUpOpenHKSix(string WX_UserName, string WX_SourceType, dbDataContext db)
        {

            string Result = "";

            var UnOpenLogs = db.WX_UserGameLog_HKSix.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                && t.WX_SourceType == WX_SourceType
                && t.WX_UserName == WX_UserName
                && t.HaveOpen == false
                );

            var periods = UnOpenLogs.Select(t => t.GamePeriod).Distinct();

            foreach (var peritem in periods)
            {
                Result += peritem + "期";
                foreach (var logitem in UnOpenLogs.Where(t => t.GamePeriod == peritem))
                {
                    Result += logitem.BuyValue + "#" + logitem.BuyMoney.ToString() + "，";
                }


            }



            return Result;
        }


        public static IQueryable<Game_FootBall_VSRatios> GameVSGetRatios(dbDataContext db, Game_FootBall_VS VS)
        {
            return db.Game_FootBall_VSRatios.Where(t => t.aspnet_UserID == VS.aspnet_UserID && t.GameKey == VS.GameKey);
        }
        public static Game_FootBall_VSRatios VSGetCurRatio(Game_FootBall_VS VS, dbDataContext db)
        {
            try
            {
                var ALL = db.Game_FootBall_VSRatios.SingleOrDefault(t => t.aspnet_UserID == VS.aspnet_UserID
                               && t.GameKey == VS.GameKey
                               && (t.RatioType.Contains("当前") || t.RatioType.Contains("即时"))
                               );
                return ALL;
            }
            catch (Exception AnyError)
            {
                NetFramework.Console.WriteLine(VS.A_Team + "VS" + VS.B_Team + " " + VS.GameKey + "当前盘异常");
                NetFramework.Console.WriteLine(AnyError.StackTrace);
                return null;
            }



        }
        public static decimal ObjectToDecimal(object param)
        {
            try
            {
                return Convert.ToDecimal(param);
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public static string GetPointLog(dbDataContext db, string GameKey)
        {
            string Result = "";
            var logs = db.Game_ResultFootBallPointLog_Last.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                  && t.GameKey == GameKey
                  );
            foreach (var item in logs)
            {
                Result += item.PointTime + " " + item.PointTeam + "进球" + Environment.NewLine;
            }
            return Result;
        }


        public enum HKSixBuyType { 大小, 单双, 特码 }
        public static string OpenHKSix(WX_UserGameLog_HKSix toopen, dbDataContext db, Game_ResultHKSix hksixresult)
        {
            string Result = "";

            HKSixBuyType optype = (HKSixBuyType)Enum.Parse(typeof(HKSixBuyType), toopen.BuyType);

            Result = hksixresult.GamePeriod + "期 开"
            + hksixresult.Num1.ToString() + ","
                 + hksixresult.Num2.ToString() + ","
                  + hksixresult.Num3.ToString() + ","
                   + hksixresult.Num4.ToString() + ","
                    + hksixresult.Num5.ToString() + ","
                     + hksixresult.Num6.ToString() + ",特码"
                     + hksixresult.NumSpec.ToString() + " ";

            Int32 Total = hksixresult.Num1.Value + hksixresult.Num2.Value + hksixresult.Num3.Value + hksixresult.Num4.Value + hksixresult.Num5.Value + hksixresult.Num6.Value;

            string ResultBigSmall = "";
            string ResultSingleDouble = "";
            if (Total <= 149)
            {
                Result += "小 ";
                ResultBigSmall = "小";
            }
            else
            {
                Result += "大 ";
                ResultBigSmall = "大";
            }
            if (Total % 2 == 1)
            {
                Result += "单";
                ResultSingleDouble = "单";
            }
            else
            {
                Result += "双";
                ResultSingleDouble = "双";
            }


            Result += Environment.NewLine;
            if (optype == HKSixBuyType.大小)
            {
                if (toopen.BuyValue == ResultBigSmall)
                {

                }
                else
                {
                    toopen.ResultMoney = 0;
                    toopen.HaveOpen = true;
                    db.SubmitChanges();
                }
            }
            if (optype == HKSixBuyType.单双)
            {
                if (toopen.BuyValue == ResultSingleDouble)
                {

                }
                else
                {
                    toopen.ResultMoney = 0;
                    toopen.HaveOpen = true;
                    db.SubmitChanges();
                }
            }
            else if (optype == HKSixBuyType.特码)
            {

                try
                {
                    Convert.ToInt32(toopen.BuyValue);
                }
                catch (Exception)
                {


                }
            }
            else
            {
                toopen.ResultMoney = 0;
                toopen.HaveOpen = true;
                db.SubmitChanges();
            }

            Result = WXUserChangeLog_GetRemainder(toopen.WX_UserName, toopen.WX_SourceType).ToString("N0");
            return Result;
        }


        public static void CheckAndCopyHKSixRatio(dbDataContext db, Guid checkuserid)
        {
            MembershipUser sysadmin = System.Web.Security.Membership.GetUser("sysadmin");

            var CopyRatio = db.Game_BasicRatio.Where(t => t.aspnet_UserID == (sysadmin == null ? Guid.Empty : (Guid)sysadmin.ProviderUserKey));

            var exists = db.Game_BasicRaioHKSix.Where(t => t.aspnet_UserID == GlobalParam.UserKey);

            if (CopyRatio.Count() != 0 && exists.Count() == 0)
            {
                foreach (var item in CopyRatio)
                {
                    Linq.Game_BasicRatio newr = new Linq.Game_BasicRatio();
                    newr.aspnet_UserID = checkuserid;
                    newr.BasicRatio = item.BasicRatio;
                    newr.BuyType = item.BuyType;
                    newr.BuyValue = item.BuyValue;
                    newr.GameType = item.GameType;
                    newr.IncludeMin = item.IncludeMin;
                    newr.MaxBuy = item.MaxBuy;
                    newr.MinBuy = item.MinBuy;

                    newr.OrderIndex = item.OrderIndex;
                    db.Game_BasicRatio.InsertOnSubmit(newr);
                    db.SubmitChanges();
                }
            }
        }
        public static string GetHKSixLast10()
        {
            string Result = "";
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            var logs = db.Game_ResultHKSix.Where(t => t.aspnet_UserID == GlobalParam.UserKey).OrderByDescending(t => t.GamePeriod);
            Int32 TotaIndex = 1;
            foreach (var item in logs)
            {
                if (TotaIndex > 10)
                {
                    break;
                }
                TotaIndex += 1;
                Result += item.GamePeriod + "期" + item.Num1.ToString() + ","
                    + item.Num2.ToString() + "," + item.Num3.ToString() + "," + item.Num4.ToString() + "," + item.Num5.ToString() + ","
                    + "," + item.Num6.ToString() + "特码" + item.NumSpec.ToString();
                Result += "" + Environment.NewLine;
            }



            return Result;

        }

    }
}
