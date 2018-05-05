using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace WeixinRoboot.Linq
{
    /// <summary>
    /// LINQ SUBMIT CHANGERS 不稳定 停用
    /// </summary>
    public class DataLogic
    {

        /// <summary>
        /// 修改未兑奖记录以及记录变更
        /// </summary>
        /// <param name="db"></param>
        public static Int32 WX_UserGameLog_Deal(StartForm StartF, string ContactID)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            var toupdate = db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                && ((t.Result_HaveProcess == false) || t.Result_HaveProcess == null)
                && (t.WX_UserName == ContactID)
                );
            Int32 Result = 0;
            foreach (WX_UserGameLog gamelogitem in toupdate)
            {
                Game_Result gr = db.Game_Result.SingleOrDefault(t =>
                    t.aspnet_UserID == GlobalParam.Key
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
                                if (gamelogitem.Gr_BigSmall == "大" && gamelogitem.Gr_SingleDouble == "单" && gamelogitem.Gr_DragonTiger == "龙")
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


                    cl.NeedNotice = true;
                    cl.HaveNotice = false;
                    cl.FinalStatus = true;

                    cl.BuyValue = "";
                    cl.GamePeriod = "";
                    db.WX_UserChangeLog.InsertOnSubmit(cl);
                }


                db.SubmitChanges();

                Result += 1;

            }//行循环处理
            //增加ChangeLog
            return Result;

        }

        public static string WX_UserGameLog_Cancel(dbDataContext db, WX_UserReplyLog reply, DataTable MemberSource)
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

            Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == reply.ReceiveTime.ToString("HH:mm"));
            if (testmin != null)
            {
                return "整点" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

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
                catch (Exception AnyError)
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
                         t.aspnet_UserID == GlobalParam.Key
                             && t.WX_UserName == reply.WX_UserName
                             && t.GamePeriod == TotalNextPeriod
                             && t.Buy_Value == reply.ReceiveContent.Substring(2, 2)
                         );

                    #region "检查最大可取消"
                    var ToModify = db.WX_UserGameLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key
                         && t.WX_UserName == reply.WX_UserName
                         && t.GameName == "重庆时时彩"
                         && t.GamePeriod == TotalNextPeriod
                         && t.Buy_Value == reply.ReceiveContent.Substring(2, 2)
                         && t.Buy_Point > 0
                         );
                    if (ToModify.Buy_Point < Convert.ToDecimal(Str_BuyPointful))
                    {
                        TotalResult tr1 = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                      && t.WX_UserName == reply.WX_UserName
                      && t.Buy_Point != 0
                      && t.Result_HaveProcess != true
                      ).ToList(), MemberSource);
                        return "下注不足，" + tr1.ToSlimStringV2() + "余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0"); ;
                    }
                    #endregion

                    #region 检查赔率
                    Decimal CheckBuy = (findupdate == null ? 0 : findupdate.Buy_Point.Value) - Convert.ToDecimal(Str_BuyPoint);

                    var ratios = db.Game_BasicRatio.Where(t =>
                        t.BuyType == "全X"
                      && t.aspnet_UserID == GlobalParam.Key
                       && t.MinBuy <= CheckBuy
                      && t.MaxBuy >= CheckBuy
                      );
                    if (ratios.Count() == 0 && CheckBuy != 0)
                    {
                        Decimal? MaxLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.Key
               && t.BuyType == "全X"

               ).Max(t => t.MaxBuy);

                        Decimal? MinLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.Key
                            && t.BuyType == "全X"
                                ).Min(t => t.MinBuy);
                        return "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0"); ;

                    }
                    if (ratios.Count() != 5 && ratios.Count() != 0)
                    {
                        return "全X限额范围不一致" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0"); ;
                    }
                    #endregion



                    findupdate.Buy_Point -= Convert.ToDecimal(Str_BuyPointful);
                    findupdate.Buy_Ratio_Full1 = ratios.SingleOrDefault(t => t.BuyValue == "连1个") == null ? 0 : ratios.SingleOrDefault(t => t.BuyValue == "连1个").BasicRatio;
                    findupdate.Buy_Ratio_Full2 = ratios.SingleOrDefault(t => t.BuyValue == "连2个") == null ? 0 : ratios.SingleOrDefault(t => t.BuyValue == "连2个").BasicRatio;
                    findupdate.Buy_Ratio_Full3 = ratios.SingleOrDefault(t => t.BuyValue == "连3个") == null ? 0 : ratios.SingleOrDefault(t => t.BuyValue == "连3个").BasicRatio;
                    findupdate.Buy_Ratio_Full4 = ratios.SingleOrDefault(t => t.BuyValue == "连4个") == null ? 0 : ratios.SingleOrDefault(t => t.BuyValue == "连4个").BasicRatio;
                    findupdate.Buy_Ratio_Full5 = ratios.SingleOrDefault(t => t.BuyValue == "连5个") == null ? 0 : ratios.SingleOrDefault(t => t.BuyValue == "连5个").BasicRatio;



                    Linq.WX_UserChangeLog cl = null;

                    cl = new WX_UserChangeLog();
                    cl.aspnet_UserID = GlobalParam.Key;
                    cl.WX_UserName = reply.WX_UserName;
                    cl.ChangePoint = Convert.ToDecimal(Str_BuyPointful); ;
                    cl.NeedNotice = false;
                    cl.HaveNotice = false;
                    cl.ChangeTime = reply.ReceiveTime;
                    cl.RemarkType = "取消";
                    cl.Remark = "取消@#" + reply.ReceiveContent;
                    cl.FinalStatus = false;
                    cl.BuyValue = findupdate.Buy_Value;
                    cl.GamePeriod = findupdate.GamePeriod;
                    cl.FinalStatus = true;
                    db.WX_UserChangeLog.InsertOnSubmit(cl);
                    try
                    {
                        db.SubmitChanges();

                        TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == reply.WX_UserName
                            && t.Result_HaveProcess == false
                            && t.Buy_Point != 0).ToList(), MemberSource);
                        return tr.ToSlimStringV2() + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                    }
                    catch (Exception AnyError)
                    {

                        return AnyError.Message + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");
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
                catch (Exception AnyError)
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
                                                t.aspnet_UserID == GlobalParam.Key
                                                    && t.WX_UserName == reply.WX_UserName
                                                    && t.GamePeriod == reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod
                                                    && t.Buy_Value == reply.ReceiveContent.Substring(2, 2)
                                                );
                    #region "检查最大可取消"
                    var ToModify = db.WX_UserGameLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key
                         && t.WX_UserName == reply.WX_UserName
                         && t.GameName == "重庆时时彩"
                         && t.GamePeriod == TotalNextPeriod
                         && t.Buy_Value == reply.ReceiveContent.Substring(2, 2)
                         && t.Buy_Point > 0
                         );
                    if (ToModify.Buy_Point < Convert.ToDecimal(Str_BuyPointpos))
                    {
                        TotalResult tr1 = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                      && t.WX_UserName == reply.WX_UserName
                      && t.Buy_Point != 0
                      && t.Result_HaveProcess != true
                      ).ToList(), MemberSource);
                        return "下注不足，" + tr1.ToSlimStringV2() + "余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0"); ;
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

                    Decimal CheckBuy = (findupdate == null ? 0 : findupdate.Buy_Point.Value) - Convert.ToDecimal(Str_BuyPoint);

                    var ratios = db.Game_BasicRatio.SingleOrDefault(t => t.BuyType == "定X" && t.aspnet_UserID == GlobalParam.Key
                       && (t.BuyValue == (BuyXnUMBERIsNumber == true ? "数字" : BuyXnUMBER))

                        && t.MinBuy <= CheckBuy
                        && t.MaxBuy >= CheckBuy
                        );
                    if (ratios == null && (findupdate == null ? 0 : findupdate.Buy_Point) - Convert.ToDecimal(Str_BuyPointpos) != 0)
                    {
                        Decimal? MaxLimit = db.Game_BasicRatio.Where(t =>
                            t.aspnet_UserID == GlobalParam.Key
               && t.BuyType == "定X"
                  && (t.BuyValue == (BuyXnUMBERIsNumber == true ? "数字" : BuyXnUMBER))

               ).Max(t => t.MaxBuy);
                        Decimal? MinLimit = db.Game_BasicRatio.Where(t =>
                            t.aspnet_UserID == GlobalParam.Key
                            && t.BuyType == "定X"
                               && (t.BuyValue == (BuyXnUMBERIsNumber == true ? "数字" : BuyXnUMBER))

                            ).Min(t => t.MinBuy);
                        return "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                    }
                    #endregion





                    findupdate.Buy_Point -= BuyPointpos;
                    findupdate.Buy_Ratio = ratios == null ? 0 : ratios.BasicRatio;



                    Linq.WX_UserChangeLog cl = null;

                    cl = new WX_UserChangeLog();
                    cl.aspnet_UserID = GlobalParam.Key;
                    cl.WX_UserName = reply.WX_UserName;
                    cl.ChangePoint = Convert.ToDecimal(Str_BuyPointpos);
                    cl.NeedNotice = false;
                    cl.HaveNotice = false;
                    cl.ChangeTime = reply.ReceiveTime;
                    cl.RemarkType = "取消";
                    cl.Remark = "取消@#" + reply.ReceiveContent;
                    cl.FinalStatus = false;
                    cl.BuyValue = findupdate.Buy_Value;
                    cl.GamePeriod = findupdate.GamePeriod;
                    cl.FinalStatus = true;
                    db.WX_UserChangeLog.InsertOnSubmit(cl);
                    try
                    {
                        db.SubmitChanges();

                        TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                            && t.WX_UserName == reply.WX_UserName
                            && t.Result_HaveProcess == false
                            && t.Buy_Point != 0).ToList(), MemberSource);
                        return tr.ToSlimStringV2() + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                    }
                    catch (Exception AnyError)
                    {

                        return AnyError.Message + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");
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
                                        t.aspnet_UserID == GlobalParam.Key
                                        && t.WX_UserName == reply.WX_UserName
                                        && t.Buy_Value == KeyValue3
                                         && t.GamePeriod == reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod
                                        );


                                    #region "检查最大可取消"
                                    var ToModify = db.WX_UserGameLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key
                                         && t.WX_UserName == reply.WX_UserName
                                         && t.GameName == "重庆时时彩"
                                         && t.GamePeriod == TotalNextPeriod
                                         && t.Buy_Value == KeyValue3
                                         && t.Buy_Point > 0
                                         );
                                    if (ToModify.Buy_Point < Convert.ToDecimal(StrBuyPoint3))
                                    {
                                        TotalResult tr1 = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                                      && t.WX_UserName == reply.WX_UserName
                                      && t.Buy_Point != 0
                                      && t.Result_HaveProcess != true
                                      ).ToList(), MemberSource);
                                        return "下注不足，" + tr1.ToSlimStringV2() + "余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0"); ;
                                    }
                                    #endregion
                                    #region 检查赔率

                                    Decimal CheckBuy = (findupdate3 == null ? 0 : findupdate3.Buy_Point.Value) - Convert.ToDecimal(StrBuyPoint3);

                                    var ratios = db.Game_BasicRatio.SingleOrDefault(t =>
                                        t.BuyType == "组合"
                                        && t.aspnet_UserID == GlobalParam.Key
                                       && (t.BuyValue == KeyValue3)

                                        && t.MinBuy <= CheckBuy
                                        && t.MaxBuy >= CheckBuy
                                        );
                                    if (ratios == null && (findupdate3 == null ? 0 : findupdate3.Buy_Point) - Convert.ToDecimal(StrBuyPoint3) != 0)
                                    {
                                        Decimal? MaxLimit = db.Game_BasicRatio.Where(t =>
                                            t.aspnet_UserID == GlobalParam.Key
                               && t.BuyType == "组合"
                                  && (t.BuyValue == KeyValue3)

                               ).Max(t => t.MaxBuy);
                                        Decimal? MinLimit = db.Game_BasicRatio.Where(t =>
                                            t.aspnet_UserID == GlobalParam.Key
                                           && t.BuyType == "组合"
                                  && (t.BuyValue == KeyValue3)
                                            ).Min(t => t.MinBuy);
                                        return "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                                    }
                                    #endregion

                                    #region 检查余额
                                    decimal Remainder = WXUserChangeLog_GetRemainder(reply.WX_UserName);
                                    if (Remainder < Convert.ToDecimal(StrBuyPoint3))
                                    {
                                        return "余分不足，余" + ObjectToString(Remainder, "N0");
                                    }
                                    #endregion




                                    findupdate3.Buy_Point -= Convert.ToDecimal(StrBuyPoint3); ;
                                    findupdate3.Buy_Ratio = (ratios == null ? 0 : ratios.BasicRatio);



                                    Linq.WX_UserChangeLog cl = null;

                                    cl = new WX_UserChangeLog();
                                    cl.aspnet_UserID = GlobalParam.Key;
                                    cl.WX_UserName = reply.WX_UserName;
                                    cl.ChangePoint = Convert.ToDecimal(StrBuyPoint3);
                                    cl.NeedNotice = false;
                                    cl.HaveNotice = false;
                                    cl.ChangeTime = reply.ReceiveTime;
                                    cl.RemarkType = "取消";
                                    cl.Remark = "取消@#" + reply.ReceiveContent;
                                    cl.FinalStatus = false;
                                    cl.BuyValue = findupdate3.Buy_Value;
                                    cl.GamePeriod = findupdate3.GamePeriod;
                                    cl.FinalStatus = true;
                                    db.WX_UserChangeLog.InsertOnSubmit(cl);
                                    try
                                    {
                                        db.SubmitChanges();

                                        TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                                            && t.WX_UserName == reply.WX_UserName
                                            && t.Result_HaveProcess == false
                                            && t.Buy_Point != 0).ToList(), MemberSource);
                                        return tr.ToSlimStringV2() + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                                    }
                                    catch (Exception AnyError)
                                    {

                                        return AnyError.Message + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");
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
                                t.aspnet_UserID == GlobalParam.Key
                                && t.WX_UserName == reply.WX_UserName
                                && t.Buy_Value == KeyValue2
                                 && t.GamePeriod == reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod
                                );

                            #region "检查最大可取消"
                            var ToModify = db.WX_UserGameLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key
                                 && t.WX_UserName == reply.WX_UserName
                                 && t.GameName == "重庆时时彩"
                                 && t.GamePeriod == TotalNextPeriod
                                 && t.Buy_Value == KeyValue2
                                 && t.Buy_Point > 0
                                 );

                            if (ToModify.Buy_Point < Convert.ToDecimal(StrBuyPoint2))
                            {
                                TotalResult tr1 = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                              && t.WX_UserName == reply.WX_UserName
                              && t.Buy_Point != 0
                              && t.Result_HaveProcess != true
                              ).ToList(), MemberSource);
                                return "下注不足，" + tr1.ToSlimStringV2() + "余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0"); ;
                            }
                            #endregion
                            #region 检查赔率

                            Decimal CheckBuy = (findupdate2 == null ? 0 : findupdate2.Buy_Point.Value) - Convert.ToDecimal(StrBuyPoint2);

                            var ratios = db.Game_BasicRatio.SingleOrDefault(t =>
                                t.BuyType == "组合"
                                && t.aspnet_UserID == GlobalParam.Key
                               && (t.BuyValue == KeyValue2)

                                && t.MinBuy <= CheckBuy
                                && t.MaxBuy >= CheckBuy
                                );
                            if (ratios == null && (findupdate2 == null ? 0 : findupdate2.Buy_Point) - Convert.ToDecimal(StrBuyPoint2) != 0)
                            {
                                Decimal? MaxLimit = db.Game_BasicRatio.Where(t =>
                                    t.aspnet_UserID == GlobalParam.Key
                       && t.BuyType == "组合"
                          && (t.BuyValue == KeyValue2)

                       ).Max(t => t.MaxBuy);
                                Decimal? MinLimit = db.Game_BasicRatio.Where(t =>
                                    t.aspnet_UserID == GlobalParam.Key
                                   && t.BuyType == "组合"
                          && (t.BuyValue == KeyValue2)
                                    ).Min(t => t.MinBuy);
                                return "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                            }
                            #endregion

                            #region 检查余额
                            decimal Remainder = WXUserChangeLog_GetRemainder(reply.WX_UserName);
                            if (Remainder < Convert.ToDecimal(StrBuyPoint2))
                            {
                                return "余分不足，余" + ObjectToString(Remainder, "N0");
                            }
                            #endregion

                            findupdate2.Buy_Point -= Convert.ToDecimal(StrBuyPoint2);
                            findupdate2.Buy_Ratio = ratios == null ? 0 : ratios.BasicRatio;


                            Linq.WX_UserChangeLog cl = null;

                            cl = new WX_UserChangeLog();
                            cl.aspnet_UserID = GlobalParam.Key;
                            cl.WX_UserName = reply.WX_UserName;
                            cl.ChangePoint = Convert.ToDecimal(StrBuyPoint2);
                            cl.NeedNotice = false;
                            cl.HaveNotice = false;
                            cl.ChangeTime = reply.ReceiveTime;
                            cl.RemarkType = "取消";
                            cl.Remark = "取消@#" + reply.ReceiveContent;
                            cl.FinalStatus = false;

                            cl.BuyValue = findupdate2.Buy_Value;
                            cl.GamePeriod = findupdate2.GamePeriod;

                            cl.FinalStatus = true;
                            db.WX_UserChangeLog.InsertOnSubmit(cl);
                            try
                            {
                                db.SubmitChanges();

                                TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                                    && t.WX_UserName == reply.WX_UserName
                                    && t.Result_HaveProcess == false
                                   ).ToList(), MemberSource);
                                return tr.ToSlimStringV2() + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                            }
                            catch (Exception AnyError)
                            {

                                return AnyError.Message + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");
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
                var ToModify = db.WX_UserGameLog.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key
                     && t.WX_UserName == reply.WX_UserName
                     && t.GameName == "重庆时时彩"
                     && t.GamePeriod == TotalNextPeriod
                     && t.Buy_Value == FirstIndex
                     && t.Buy_Point > 0
                     );
                if (ToModify.Buy_Point < Convert.ToDecimal(Str_BuyPoint))
                {
                    TotalResult tr1 = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                  && t.WX_UserName == reply.WX_UserName
                  && t.Buy_Point != 0
                  && t.Result_HaveProcess != true
                  ).ToList(), MemberSource);
                    return "下注不足，" + tr1.ToSlimStringV2() + "余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0"); ;
                }
                #endregion

                #region "赔率重算，如果为0"

                string CheckResult = "";
                Decimal CheckBuy = (ToModify == null ? 0 : ToModify.Buy_Point.Value) - Convert.ToDecimal(Str_BuyPoint);

                #region "转化赔率"
                var CheckRatioConfig = db.Game_BasicRatio.SingleOrDefault(t => t.GameType == "重庆时时彩"
                    && t.aspnet_UserID == GlobalParam.Key
                    && t.BuyType == ToModify.Buy_Type
                    && t.BuyValue == ToModify.Buy_Value
                    && t.MaxBuy >= CheckBuy
                    && ((t.MinBuy <= CheckBuy && t.IncludeMin == true)
                    || (t.MinBuy < CheckBuy && t.IncludeMin == false)
                    )
                    );
                if (CheckRatioConfig == null && (ToModify.Buy_Point - Convert.ToDecimal(Str_BuyPoint) != 0))
                {
                    Decimal? MaxLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.Key
                        && t.BuyValue == ToModify.Buy_Value
                        && t.BuyType == ToModify.Buy_Type
                        ).Max(t => t.MaxBuy);
                    Decimal? MinLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.Key
                        && t.BuyValue == ToModify.Buy_Value
                         && t.BuyType == ToModify.Buy_Type
                        ).Min(t => t.MinBuy);
                    CheckResult = "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围";

                }
                #endregion


                if (CheckResult != "")
                {
                    TotalResult tr2 = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
               && t.WX_UserName == reply.WX_UserName
               && t.Buy_Point != 0
               && t.Result_HaveProcess != true
               ).ToList(), MemberSource);
                    return CheckResult + "," + tr2.ToSlimStringV2() + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0"); ; ;
                }


                ToModify.Buy_Point -= Convert.ToDecimal(Str_BuyPoint);
                ToModify.Buy_Ratio = CheckRatioConfig == null ? 0 : CheckRatioConfig.BasicRatio;

                Linq.WX_UserChangeLog cl = null;
                cl = new WX_UserChangeLog();
                cl.aspnet_UserID = GlobalParam.Key;
                cl.WX_UserName = ToModify.WX_UserName;
                cl.ChangePoint = Convert.ToDecimal(Str_BuyPoint);
                cl.NeedNotice = false;
                cl.HaveNotice = false;
                cl.ChangeTime = reply.ReceiveTime;
                cl.RemarkType = "取消";
                cl.Remark = "取消@#" + reply.ReceiveContent;
                cl.FinalStatus = false;
                cl.BuyValue = FirstIndex;
                cl.GamePeriod = ToModify.GamePeriod;
                cl.FinalStatus = true;

                cl.BuyValue = ToModify.Buy_Value;
                cl.GamePeriod = ToModify.GamePeriod;

                db.WX_UserChangeLog.InsertOnSubmit(cl);
                db.SubmitChanges();






                #endregion



                TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                    && t.WX_UserName == reply.WX_UserName
                    && t.Buy_Point != 0
                    && t.Result_HaveProcess != true
                    ).ToList(), MemberSource);

                return tr.ToSlimStringV2() + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

            }//X+数字
            #endregion








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

                    Result += Perioditem + "期";
                    foreach (var buyitem in Buys.Where(t => t.ShowPeriod == Perioditem))
                    {
                        Result += buyitem.BuyValue + ObjectToString(buyitem.BuyPoint, "N0") + ",";
                    }
                    Result += Environment.NewLine;
                }
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
                    + "用户：" + UserNickName + Environment.NewLine;
                if (Buys.Count() == 0)
                {
                    return "";
                }
                var Periods = Buys.Select(t => t.ShowPeriod).Distinct();

                foreach (var buyitem in Buys)
                {
                    Result += "期号: " + buyitem.ShowPeriod;
                    Result += " " + buyitem.GameResult + Environment.NewLine;

                    Result += " 下注" + buyitem.BuyValue
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
                    + "用户：" + UserNickName + Environment.NewLine;
                if (Buys.Count() == 0)
                {
                    return "";
                }
                var Periods = Buys.Select(t => new { t.ShowPeriod, t.GameResult }).Distinct();

                foreach (var period in Periods)
                {

                    Result += "期号: " + period.ShowPeriod;
                    Result += " " + period.GameResult + Environment.NewLine + "本期下注";
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

                Result += Environment.NewLine;


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
                DataRow usrw = MemberSource.Select("User_ContactID='" + item.WX_UserName + "'")[0];
                r.UserNickName = usrw.Field<string>("User_Contact");
                decimal? Remainder = WXUserChangeLog_GetRemainder(item.WX_UserName);
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
        public static string WX_UserReplyLog_Create(WX_UserReplyLog reply, DataTable MemberSource)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");


            WX_UserReply testr = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == reply.aspnet_UserID && t.WX_UserName == reply.WX_UserName);
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
                if (reply.ReceiveContent == "查")
                {
                    DateTime TestPeriod = DateTime.Now;
                    if (TestPeriod.Hour <= 8)
                    {
                        TestPeriod = TestPeriod.AddDays(-1);
                    }

                    var TodayBuys = db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                        && t.WX_UserName == reply.WX_UserName
                        && t.GameLocalPeriod.StartsWith(TestPeriod.ToString("yyyyMMdd"))
                        && t.Buy_Point != 0
                        );

                    string Result = "下注期数是" + ObjectToString(TodayBuys.Select(t => t.GamePeriod).Distinct().Count(), "N0")
                    + "期，下注总额是" + ObjectToString(TodayBuys.Sum(t => t.Buy_Point), "N0")
                        // + "平均下注" + ObjectToString(TodayBuys.Select(t => t.GamePeriod).Distinct().Count() == 0 ? 0 : (TodayBuys.Sum(t => t.Buy_Point.HasValue ? t.Buy_Point.Value : 0) / TodayBuys.Select(t => t.GamePeriod).Distinct().Count()), "N2")
                    + ",得分总额是" + ObjectToString(TodayBuys.Sum(t => t.Result_Point), "N0")
                    + ",结果是" + ObjectToString((TodayBuys.Sum(t => t.Result_Point.HasValue ? t.Result_Point.Value : 0) - TodayBuys.Sum(t => t.Buy_Point.HasValue ? t.Buy_Point.Value : 0)), "N0");

                    return Result;
                }
                else if (reply.ReceiveContent == "全部取消")
                {
                    Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == reply.ReceiveTime.ToString("HH:mm"));
                    Linq.aspnet_UsersNewGameResultSend myset = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key);
                    if (myset.IsBlock == true)
                    {
                        return "封盘";
                    }
                    if (testmin != null)
                    {
                        return "整点" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                    }


                    var ToCalcel = db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                        && t.WX_UserName == reply.WX_UserName
                        && t.Result_HaveProcess == false
                        && t.Buy_Point != 0
                        && string.Compare(t.GamePeriod, reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod) >= 0

                        );
                    foreach (var cancelitem in ToCalcel)
                    {


                        WX_UserChangeLog cl = new WX_UserChangeLog();
                        cl.aspnet_UserID = GlobalParam.Key;
                        cl.WX_UserName = cancelitem.WX_UserName;
                        cl.ChangePoint = cancelitem.Buy_Point;
                        cl.NeedNotice = false;
                        cl.HaveNotice = false;
                        cl.ChangeTime = reply.ReceiveTime;
                        cl.RemarkType = "取消@#" + cancelitem.Buy_Value;
                        cl.Remark = "下单@#" + reply.ReceiveContent;
                        cl.FinalStatus = false;
                        cl.BuyValue = cancelitem.Buy_Value;
                        cl.GamePeriod = cancelitem.GamePeriod;
                        db.WX_UserChangeLog.InsertOnSubmit(cl);

                        cancelitem.Buy_Point = 0;
                        cancelitem.GP_LastModify = reply.ReceiveTime;
                    }
                    db.SubmitChanges();

                    return "全部取消成功,余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                }
                else if (reply.ReceiveContent.StartsWith("开奖"))
                {
                    string Result = "";
                    string QueryDate = reply.ReceiveContent.Substring(2);
                    DateTime TestPeriod = DateTime.Now;
                    if (TestPeriod.Hour <= 8)
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
                        t.aspnet_UserID == GlobalParam.Key
                        && t.WX_UserName == reply.WX_UserName
                        && t.Buy_Point != 0
                        && t.GameLocalPeriod.StartsWith(TestPeriod.ToString("yyyyMMdd"))
                        );
                    TotalResult tr = BuildResult(TodatBuyGameLog.ToList(), MemberSource);
                    Result = tr.ToOpenStringV2();

                    return Result;

                }

                else if (reply.ReceiveContent == "未开奖")
                {
                    string Result = "";
                    var TodatBuyGameLog = db.WX_UserGameLog.Where(t =>
                        t.aspnet_UserID == GlobalParam.Key
                        && t.WX_UserName == reply.WX_UserName
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
                    Linq.aspnet_UsersNewGameResultSend myset = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key);
                    if (myset.IsBlock == true)
                    {
                        return "封盘";
                    }
                    Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == reply.ReceiveTime.ToString("HH:mm"));
                    if (testmin != null)
                    {
                        return "整点" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0"); ;

                    }
                    #endregion
                    return WX_UserGameLog_Cancel(db, reply, MemberSource);
                }//取消的单
                #region 全
                else if (reply.ReceiveContent.StartsWith("全"))
                {
                    Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == reply.ReceiveTime.ToString("HH:mm"));
                    if (testmin != null)
                    {
                        return "整点" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

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
                    catch (Exception AnyError)
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
                             t.aspnet_UserID == GlobalParam.Key
                                 && t.WX_UserName == reply.WX_UserName
                                 && t.GamePeriod == reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod
                                 && t.Buy_Value == reply.ReceiveContent.Substring(0, 2)
                             );

                        #region 检查赔率
                        Decimal CheckBuy = (findupdate == null ? 0 : findupdate.Buy_Point.Value) + Convert.ToDecimal(Str_BuyPoint);

                        var ratios = db.Game_BasicRatio.Where(t =>
                            t.BuyType == "全X"
                          && t.aspnet_UserID == GlobalParam.Key
                           && t.MinBuy <= CheckBuy
                          && t.MaxBuy >= CheckBuy
                          );
                        if (ratios.Count() == 0)
                        {
                            Decimal? MaxLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.Key
                   && t.BuyType == "全X"

                   ).Max(t => t.MaxBuy);

                            Decimal? MinLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.Key
                                && t.BuyType == "全X"
                                    ).Min(t => t.MinBuy);
                            return "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0"); ;

                        }
                        if (ratios.Count() != 5)
                        {
                            return "全X限额范围不一致" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0"); ;
                        }
                        #endregion

                        #region 检查余额
                        decimal Remainder = WXUserChangeLog_GetRemainder(reply.WX_UserName);
                        if (Remainder < Convert.ToDecimal(Str_BuyPoint))
                        {
                            return "余分不足，余" + ObjectToString(Remainder, "N0");
                        }
                        #endregion
                        WX_UserGameLog newgl = null;
                        if (findupdate == null)
                        {


                            newgl = new WX_UserGameLog();
                            newgl.aspnet_UserID = GlobalParam.Key;
                            newgl.Buy_Point = BuyPoint;
                            newgl.Buy_Type = "全X";
                            newgl.Buy_Value = reply.ReceiveContent.Substring(0, 2); ;
                            newgl.Buy_Point = Convert.ToDecimal(Str_BuyPoint);
                            newgl.GameName = "重庆时时彩";
                            newgl.TransTime = reply.ReceiveTime;
                            newgl.WX_UserName = reply.WX_UserName;

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
                            findupdate.Buy_Point += Convert.ToDecimal(Str_BuyPoint);
                            findupdate.Buy_Ratio_Full1 = ratios.SingleOrDefault(t => t.BuyValue == "连1个").BasicRatio;
                            findupdate.Buy_Ratio_Full2 = ratios.SingleOrDefault(t => t.BuyValue == "连2个").BasicRatio;
                            findupdate.Buy_Ratio_Full3 = ratios.SingleOrDefault(t => t.BuyValue == "连3个").BasicRatio;
                            findupdate.Buy_Ratio_Full4 = ratios.SingleOrDefault(t => t.BuyValue == "连4个").BasicRatio;
                            findupdate.Buy_Ratio_Full5 = ratios.SingleOrDefault(t => t.BuyValue == "连5个").BasicRatio;

                        }

                        Linq.WX_UserChangeLog cl = null;

                        cl = new WX_UserChangeLog();
                        cl.aspnet_UserID = GlobalParam.Key;
                        cl.WX_UserName = reply.WX_UserName;
                        cl.ChangePoint = -Convert.ToDecimal(Str_BuyPoint); ;
                        cl.NeedNotice = false;
                        cl.HaveNotice = false;
                        cl.ChangeTime = reply.ReceiveTime;
                        cl.RemarkType = "下单";
                        cl.Remark = "下单@#" + reply.ReceiveContent;
                        cl.FinalStatus = false;
                        cl.BuyValue = (findupdate == null ? newgl.Buy_Value : findupdate.Buy_Value);
                        cl.GamePeriod = (findupdate == null ? newgl.GamePeriod : findupdate.GamePeriod);
                        cl.FinalStatus = true;
                        db.WX_UserChangeLog.InsertOnSubmit(cl);
                        try
                        {
                            db.SubmitChanges();

                            TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == reply.WX_UserName
                                && t.Result_HaveProcess == false
                                && t.Buy_Point != 0).ToList(), MemberSource);
                            return tr.ToSlimStringV2() + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                        }
                        catch (Exception AnyError)
                        {

                            return AnyError.Message + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");
                        }



                    }
                    else
                    {
                        return "";
                    }


                }//全
                #endregion
                #region 定数字或定大小
                else if (reply.ReceiveContent.StartsWith("个") ||
                    reply.ReceiveContent.StartsWith("十") ||
                    reply.ReceiveContent.StartsWith("百") ||
                    reply.ReceiveContent.StartsWith("千") ||
                    reply.ReceiveContent.StartsWith("万")
                    )
                {
                    Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == reply.ReceiveTime.ToString("HH:mm"));
                    if (testmin != null)
                    {
                        return "整点" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

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
                    catch (Exception AnyError)
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
                                                    t.aspnet_UserID == GlobalParam.Key
                                                        && t.WX_UserName == reply.WX_UserName
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

                        var ratios = db.Game_BasicRatio.SingleOrDefault(t => t.BuyType == "定X" && t.aspnet_UserID == GlobalParam.Key
                           && (t.BuyValue == (BuyXnUMBERIsNumber == true ? "数字" : BuyXnUMBER))

                            && t.MinBuy <= CheckBuy
                            && t.MaxBuy >= CheckBuy
                            );
                        if (ratios == null)
                        {
                            Decimal? MaxLimit = db.Game_BasicRatio.Where(t =>
                                t.aspnet_UserID == GlobalParam.Key
                   && t.BuyType == "定X"
                      && (t.BuyValue == (BuyXnUMBERIsNumber == true ? "数字" : BuyXnUMBER))

                   ).Max(t => t.MaxBuy);
                            Decimal? MinLimit = db.Game_BasicRatio.Where(t =>
                                t.aspnet_UserID == GlobalParam.Key
                                && t.BuyType == "定X"
                                   && (t.BuyValue == (BuyXnUMBERIsNumber == true ? "数字" : BuyXnUMBER))

                                ).Min(t => t.MinBuy);
                            return "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                        }
                        #endregion

                        #region 检查余额
                        decimal Remainder = WXUserChangeLog_GetRemainder(reply.WX_UserName);
                        if (Remainder < Convert.ToDecimal(Str_BuyPoint))
                        {
                            return "余分不足，余" + ObjectToString(Remainder, "N0");
                        }
                        #endregion


                        WX_UserGameLog newgl = null;
                        if (findupdate == null)
                        {


                            newgl = new WX_UserGameLog();
                            newgl.aspnet_UserID = GlobalParam.Key;
                            newgl.Buy_Point = BuyPoint;
                            newgl.Buy_Type = "定X";
                            newgl.Buy_Value = reply.ReceiveContent.Substring(0, 2); ;
                            newgl.Buy_Point = Convert.ToDecimal(Str_BuyPoint);
                            newgl.GameName = "重庆时时彩";
                            newgl.TransTime = reply.ReceiveTime;
                            newgl.WX_UserName = reply.WX_UserName;
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
                            findupdate.Buy_Point += BuyPoint;
                            findupdate.Buy_Ratio = ratios == null ? 0 : ratios.BasicRatio;

                        }

                        Linq.WX_UserChangeLog cl = null;

                        cl = new WX_UserChangeLog();
                        cl.aspnet_UserID = GlobalParam.Key;
                        cl.WX_UserName = reply.WX_UserName;
                        cl.ChangePoint = -Convert.ToDecimal(Str_BuyPoint);
                        cl.NeedNotice = false;
                        cl.HaveNotice = false;
                        cl.ChangeTime = reply.ReceiveTime;
                        cl.RemarkType = "下单";
                        cl.Remark = "下单@#" + reply.ReceiveContent;
                        cl.FinalStatus = false;
                        cl.BuyValue = findupdate == null ? newgl.Buy_Value : findupdate.Buy_Value;
                        cl.GamePeriod = findupdate == null ? newgl.GamePeriod : findupdate.GamePeriod;
                        cl.FinalStatus = true;
                        db.WX_UserChangeLog.InsertOnSubmit(cl);
                        try
                        {
                            db.SubmitChanges();

                            TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                                && t.WX_UserName == reply.WX_UserName
                                && t.Result_HaveProcess == false
                                && t.Buy_Point != 0).ToList(), MemberSource);
                            return tr.ToSlimStringV2() + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                        }
                        catch (Exception AnyError)
                        {

                            return AnyError.Message + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");
                        }



                    }
                    else
                    {
                        return "";
                    }

                }//定数字或定大小
                #endregion

                else
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
                                            Linq.aspnet_UsersNewGameResultSend myset = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key);
                                            if (myset.IsBlock == true)
                                            {
                                                return "封盘";
                                            }
                                            Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == reply.ReceiveTime.ToString("HH:mm"));
                                            if (testmin != null)
                                            {
                                                return "整点" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                                            }
                                            string KeyValue3 = "";
                                            ComboString.TryGetValue(BuyType3, out KeyValue3);
                                            WX_UserGameLog findupdate3 = db.WX_UserGameLog.SingleOrDefault(t =>
                                                t.aspnet_UserID == GlobalParam.Key
                                                && t.WX_UserName == reply.WX_UserName
                                                && t.Buy_Value == KeyValue3
                                                 && t.GamePeriod == reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod
                                                );

                                            #region 检查赔率

                                            Decimal CheckBuy = (findupdate3 == null ? 0 : findupdate3.Buy_Point.Value) + Convert.ToDecimal(StrBuyPoint3);

                                            var ratios = db.Game_BasicRatio.SingleOrDefault(t =>
                                                t.BuyType == "组合"
                                                && t.aspnet_UserID == GlobalParam.Key
                                               && (t.BuyValue == KeyValue3)

                                                && t.MinBuy <= CheckBuy
                                                && t.MaxBuy >= CheckBuy
                                                );
                                            if (ratios == null)
                                            {
                                                Decimal? MaxLimit = db.Game_BasicRatio.Where(t =>
                                                    t.aspnet_UserID == GlobalParam.Key
                                       && t.BuyType == "组合"
                                          && (t.BuyValue == KeyValue3)

                                       ).Max(t => t.MaxBuy);
                                                Decimal? MinLimit = db.Game_BasicRatio.Where(t =>
                                                    t.aspnet_UserID == GlobalParam.Key
                                                   && t.BuyType == "组合"
                                          && (t.BuyValue == KeyValue3)
                                                    ).Min(t => t.MinBuy);
                                                return "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                                            }
                                            #endregion

                                            #region 检查余额
                                            decimal Remainder = WXUserChangeLog_GetRemainder(reply.WX_UserName);
                                            if (Remainder < Convert.ToDecimal(StrBuyPoint3))
                                            {
                                                return "余分不足，余" + ObjectToString(Remainder, "N0");
                                            }
                                            #endregion

                                            WX_UserGameLog newgl = null;
                                            if (findupdate3 == null)
                                            {


                                                newgl = new WX_UserGameLog();
                                                newgl.aspnet_UserID = GlobalParam.Key;
                                                newgl.Buy_Point = Convert.ToDecimal(StrBuyPoint3);
                                                newgl.Buy_Type = "组合";
                                                newgl.Buy_Value = KeyValue3;
                                                newgl.Buy_Point = Convert.ToDecimal(StrBuyPoint3);
                                                newgl.GameName = "重庆时时彩";
                                                newgl.TransTime = reply.ReceiveTime;
                                                newgl.WX_UserName = reply.WX_UserName;
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
                                                findupdate3.Buy_Point += Convert.ToDecimal(StrBuyPoint3);
                                                findupdate3.Buy_Ratio = ratios == null ? 0 : ratios.BasicRatio;
                                            }

                                            Linq.WX_UserChangeLog cl = null;

                                            cl = new WX_UserChangeLog();
                                            cl.aspnet_UserID = GlobalParam.Key;
                                            cl.WX_UserName = reply.WX_UserName;
                                            cl.ChangePoint = -Convert.ToDecimal(StrBuyPoint3);
                                            cl.NeedNotice = false;
                                            cl.HaveNotice = false;
                                            cl.ChangeTime = reply.ReceiveTime;
                                            cl.RemarkType = "下单";
                                            cl.Remark = "下单@#" + reply.ReceiveContent;
                                            cl.FinalStatus = false;
                                            cl.BuyValue = (findupdate3 == null ? newgl.Buy_Value : findupdate3.Buy_Value);
                                            cl.GamePeriod = (findupdate3 == null ? newgl.GamePeriod : findupdate3.GamePeriod);
                                            cl.FinalStatus = true;
                                            db.WX_UserChangeLog.InsertOnSubmit(cl);
                                            try
                                            {
                                                db.SubmitChanges();

                                                TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                                                    && t.WX_UserName == reply.WX_UserName
                                                    && t.Result_HaveProcess == false
                                                    && t.Buy_Point != 0).ToList(), MemberSource);
                                                return tr.ToSlimStringV2() + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                                            }
                                            catch (Exception AnyError)
                                            {

                                                return AnyError.Message + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");
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
                                    Linq.aspnet_UsersNewGameResultSend myset = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key);
                                    if (myset.IsBlock == true)
                                    {
                                        return "封盘";
                                    }
                                    Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == reply.ReceiveTime.ToString("HH:mm"));
                                    if (testmin != null)
                                    {
                                        return "整点" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                                    }
                                    string KeyValue2 = "";
                                    ComboString.TryGetValue(BuyType2, out KeyValue2);

                                    WX_UserGameLog findupdate2 = db.WX_UserGameLog.SingleOrDefault(t =>
                                        t.aspnet_UserID == GlobalParam.Key
                                        && t.WX_UserName == reply.WX_UserName
                                        && t.Buy_Value == KeyValue2
                                         && t.GamePeriod == reply.ReceiveTime.ToString("yyyyMMdd") + NextPeriod
                                        );

                                    #region 检查赔率

                                    Decimal CheckBuy = (findupdate2 == null ? 0 : findupdate2.Buy_Point.Value) + Convert.ToDecimal(StrBuyPoint2);

                                    var ratios = db.Game_BasicRatio.SingleOrDefault(t =>
                                        t.BuyType == "组合"
                                        && t.aspnet_UserID == GlobalParam.Key
                                       && (t.BuyValue == KeyValue2)

                                        && t.MinBuy <= CheckBuy
                                        && t.MaxBuy >= CheckBuy
                                        );
                                    if (ratios == null)
                                    {
                                        Decimal? MaxLimit = db.Game_BasicRatio.Where(t =>
                                            t.aspnet_UserID == GlobalParam.Key
                               && t.BuyType == "组合"
                                  && (t.BuyValue == KeyValue2)

                               ).Max(t => t.MaxBuy);
                                        Decimal? MinLimit = db.Game_BasicRatio.Where(t =>
                                            t.aspnet_UserID == GlobalParam.Key
                                           && t.BuyType == "组合"
                                  && (t.BuyValue == KeyValue2)
                                            ).Min(t => t.MinBuy);
                                        return "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                                    }
                                    #endregion

                                    #region 检查余额
                                    decimal Remainder = WXUserChangeLog_GetRemainder(reply.WX_UserName);
                                    if (Remainder < Convert.ToDecimal(StrBuyPoint2))
                                    {
                                        return "余分不足，余" + ObjectToString(Remainder, "N0");
                                    }
                                    #endregion

                                    WX_UserGameLog newgl = null;
                                    if (findupdate2 == null)
                                    {
                                        newgl = new WX_UserGameLog();
                                        newgl.aspnet_UserID = GlobalParam.Key;
                                        newgl.Buy_Point = Convert.ToDecimal(StrBuyPoint2);
                                        newgl.Buy_Type = "组合";
                                        newgl.Buy_Value = KeyValue2;
                                        newgl.Buy_Point = Convert.ToDecimal(StrBuyPoint2);
                                        newgl.GameName = "重庆时时彩";
                                        newgl.TransTime = reply.ReceiveTime;
                                        newgl.WX_UserName = reply.WX_UserName;
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
                                        findupdate2.Buy_Point += Convert.ToDecimal(StrBuyPoint2);
                                        findupdate2.Buy_Ratio = ratios == null ? 0 : ratios.BasicRatio;
                                    }

                                    Linq.WX_UserChangeLog cl = null;

                                    cl = new WX_UserChangeLog();
                                    cl.aspnet_UserID = GlobalParam.Key;
                                    cl.WX_UserName = reply.WX_UserName;
                                    cl.ChangePoint = -Convert.ToDecimal(StrBuyPoint2);
                                    cl.NeedNotice = false;
                                    cl.HaveNotice = false;
                                    cl.ChangeTime = reply.ReceiveTime;
                                    cl.RemarkType = "下单";
                                    cl.Remark = "下单@#" + reply.ReceiveContent;
                                    cl.FinalStatus = false;

                                    cl.BuyValue = (findupdate2 == null ? newgl.Buy_Value : findupdate2.Buy_Value);
                                    cl.GamePeriod = (findupdate2 == null ? newgl.GamePeriod : findupdate2.GamePeriod);

                                    cl.FinalStatus = true;
                                    db.WX_UserChangeLog.InsertOnSubmit(cl);
                                    try
                                    {
                                        db.SubmitChanges();

                                        TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                                            && t.WX_UserName == reply.WX_UserName
                                            && t.Result_HaveProcess == false
                                           ).ToList(), MemberSource);
                                        return tr.ToSlimStringV2() + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                                    }
                                    catch (Exception AnyError)
                                    {

                                        return AnyError.Message + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");
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
                            CheckResult = NewGameLog(reply, "龙虎合", FirstIndex, BuyPoint, db);
                            break;
                        case "虎":
                            CheckResult = NewGameLog(reply, "龙虎合", FirstIndex, BuyPoint, db);
                            break;
                        case "合":
                            CheckResult = NewGameLog(reply, "龙虎合", FirstIndex, BuyPoint, db);
                            break;
                        case "大":
                            CheckResult = NewGameLog(reply, "大小和", FirstIndex, BuyPoint, db);
                            break;
                        case "小":
                            CheckResult = NewGameLog(reply, "大小和", FirstIndex, BuyPoint, db);
                            break;
                        case "和":
                            CheckResult = NewGameLog(reply, "大小和", FirstIndex, BuyPoint, db);
                            break;
                        case "单":
                            CheckResult = NewGameLog(reply, "单双", FirstIndex, BuyPoint, db);
                            break;
                        case "双":
                            CheckResult = NewGameLog(reply, "单双", FirstIndex, BuyPoint, db);
                            break;
                        default:
                            return "";
                    }

                    if (CheckResult != "")
                    {
                        TotalResult tr = BuildResult(
                            db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                                && t.WX_UserName == reply.WX_UserName
                                && t.Result_HaveProcess == false
                                ).ToList()
                            , MemberSource);
                        return CheckResult + tr.ToSlimStringV2() + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");
                    }
                    else
                    {

                        TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                            && t.WX_UserName == reply.WX_UserName
                            && t.Result_HaveProcess == false
                            && t.Buy_Point != 0).ToList()
                        , MemberSource);
                        return tr.ToSlimStringV2() + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");
                    }

                }//下单
                    #endregion
            }//有文字消息Result
            else
            {
                return "";
            }
        }

        private static Dictionary<string, string> ComboString = null;




        public static string WX_UserReplyLog_MySendCreate(string Content, DataRow UserRow)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            if (Content == "自动")
            {

                string Contact = UserRow.Field<string>("User_ContactID");

                Int32? MaxTraceCount = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key).MaxPlayerCount;
                if (MaxTraceCount.HasValue == false)
                {
                    MaxTraceCount = 50;
                }
                if (db.WX_UserReply.Where(t => t.aspnet_UserID == GlobalParam.Key && t.IsReply == true).Count() + 1 > MaxTraceCount)
                {
                    return "超过最大跟踪玩家数量";
                }
                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == Contact);
                toupdate.IsReply = true;

                db.SubmitChanges();
                UserRow.SetField("User_IsReply", true);

                return "";
            }
            else if (Content == "取消自动")
            {

                string Contact = UserRow.Field<string>("User_ContactID");


                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == Contact);
                toupdate.IsReply = false;

                db.SubmitChanges();
                UserRow.SetField("User_IsReply", false);

                return "";
            }
            else if (Content == "转发")
            {
                string Contact = UserRow.Field<string>("User_ContactID");


                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == Contact);
                toupdate.IsReceiveTransfer = true;

                db.SubmitChanges();
                UserRow.SetField("User_IsReceiveTransfer", true);

                return "";
            }
            else if (Content == "取消转发")
            {
                string Contact = UserRow.Field<string>("User_ContactID");


                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == Contact);
                toupdate.IsReceiveTransfer = false;

                db.SubmitChanges();
                UserRow.SetField("User_IsReceiveTransfer", false);

                return "";
            }
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
                                             where ds.bossaspnet_UserID == GlobalParam.Key

                                             select ds.aspnet_UserID).Distinct()

                                             ).ToList();
                    takeusers.Add(GlobalParam.Key);
                    string Result = "";
                    foreach (var item in takeusers)
                    {
                        var TodayBuys = db.WX_UserGameLog.Where(t =>
                            t.aspnet_UserID == item
                       && String.Compare(t.GameLocalPeriod.Substring(0, 8), dt_From.Value.ToString("yyyyMMdd")) >= 0
                         && String.Compare(t.GameLocalPeriod.Substring(0, 8), dt_To.Value.ToString("yyyyMMdd")) <= 0
                       && t.Buy_Point != 0
                       );
                        System.Web.Security.MembershipUser usr = System.Web.Security.Membership.GetUser(item);
                        Result += usr.UserName + "的玩家," + dt_From.Value.ToString("yyyyMMdd") + "-" + dt_To.Value.ToString("yyyyMMdd") + Environment.NewLine
                         + "下注" + ObjectToString(TodayBuys.Sum(t => t.Buy_Point), "N0")
                         + ",得分" + ObjectToString(TodayBuys.Sum(t => t.Result_Point), "N0")
                         + ",结果" + ObjectToString((TodayBuys.Sum(t => t.Result_Point.HasValue ? t.Result_Point.Value : 0) - TodayBuys.Sum(t => t.Buy_Point.HasValue ? t.Buy_Point.Value : 0)), "N0")
                        + Environment.NewLine;

                    }
                    return Result;



                }
                else
                {
                    return "";
                }
            }
            else if (Content.Length >= 2)
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
                    return "";
                }
                if (ChargeMoney <= 0)
                {
                    return "";
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
                        change.FinalStatus = true;

                        change.BuyValue = "";
                        change.GamePeriod = "";
                        db.WX_UserChangeLog.InsertOnSubmit(change);

                        db.SubmitChanges();

                        decimal? TotalPoint = WXUserChangeLog_GetRemainder(UserRow.Field<string>("User_ContactID"));

                        return "余:" + TotalPoint.Value.ToString("N0");

                        break;
                    case "下分":
                        decimal? TotalPointIn = WXUserChangeLog_GetRemainder(UserRow.Field<string>("User_ContactID"));

                        if (TotalPointIn < ChargeMoney)
                        {
                            return "下分失败,余分不足,余" + ObjectToString(WXUserChangeLog_GetRemainder(UserRow.Field<string>("User_ContactID")), "N0");

                        }

                        Linq.WX_UserChangeLog cleanup = new Linq.WX_UserChangeLog();
                        cleanup.aspnet_UserID = GlobalParam.Key;
                        cleanup.ChangeTime = DateTime.Now;
                        cleanup.ChangePoint = -ChargeMoney;
                        cleanup.Remark = "下分:" + UserRow.Field<string>("User_ContactID");
                        cleanup.RemarkType = "下分";
                        cleanup.FinalStatus = true;
                        cleanup.WX_UserName = UserRow.Field<string>("User_ContactID");

                        cleanup.BuyValue = "";
                        cleanup.GamePeriod = "";
                        db.WX_UserChangeLog.InsertOnSubmit(cleanup);

                        db.SubmitChanges();



                        decimal? TotalPoint2 = WXUserChangeLog_GetRemainder(UserRow.Field<string>("User_ContactID"));

                        return "余:" + TotalPoint2.Value.ToString("N0");


                        break;
                    case "封盘":
                        Linq.aspnet_UsersNewGameResultSend myset = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key);
                        myset.IsBlock = true;
                        db.SubmitChanges();
                        return "已封盘";
                        break;
                    case "解封":
                        Linq.aspnet_UsersNewGameResultSend myset2 = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key);
                        myset2.IsBlock = false;
                        db.SubmitChanges();
                        return "已解封";
                        break;
                    default:
                        return "";
                        break;
                }
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
        private static string NewGameLog(WX_UserReplyLog replylog, string BuyType, string BuyValue, decimal BuyPoint, dbDataContext db)
        {
            if (string.Compare(replylog.ReceiveTime.ToString("HH:mm"), "01:55") >= 0 && string.Compare(replylog.ReceiveTime.ToString("HH:mm"), "09:00") <= 0)
            {
                return "封盘时间";
            }
            Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == replylog.ReceiveTime.ToString("HH:mm"));
            if (testmin != null)
            {
                return "整点" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(replylog.WX_UserName), "N0");

            }
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



            WX_UserGameLog CheckExists = db.WX_UserGameLog.SingleOrDefault(t =>
                t.aspnet_UserID == GlobalParam.Key
                && t.WX_UserName == replylog.WX_UserName
                && t.GamePeriod == replylog.ReceiveTime.ToString("yyyyMMdd") + NextPeriod
                && t.Buy_Value == BuyValue);

            decimal Remainder = WXUserChangeLog_GetRemainder(replylog.WX_UserName);
            if (Remainder < BuyPoint)
            {
                return "余分不足";
            }

            if (CheckExists != null)
            {




                #region "转化赔率"
                Decimal CheckBuy = (CheckExists == null ? 0 : CheckExists.Buy_Point.Value) + BuyPoint;

                var CheckRatioConfig = db.Game_BasicRatio.Where(t => t.GameType == "重庆时时彩"
                    && t.aspnet_UserID == GlobalParam.Key
                    && t.BuyType == CheckExists.Buy_Type
                    && t.BuyValue == CheckExists.Buy_Value
                    && t.MaxBuy >= CheckBuy
                    && ((t.MinBuy <= (CheckBuy) && t.IncludeMin == true)
                    || (t.MinBuy < (CheckBuy) && t.IncludeMin == false)
                    )
                    );
                if (CheckRatioConfig.Count() == 0)
                {
                    Decimal? MaxLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.Key
                        && t.BuyValue == CheckExists.Buy_Value
                        && t.BuyType == CheckExists.Buy_Type
                        ).Max(t => t.MaxBuy);
                    Decimal? MinLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.Key
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
                cl.aspnet_UserID = GlobalParam.Key;
                cl.WX_UserName = replylog.WX_UserName;
                cl.ChangePoint = -BuyPoint;
                cl.NeedNotice = false;
                cl.HaveNotice = false;
                cl.ChangeTime = replylog.ReceiveTime;
                cl.RemarkType = "下单";
                cl.Remark = "下单@#" + replylog.ReceiveContent;
                cl.FinalStatus = false;
                cl.BuyValue = BuyValue;
                cl.GamePeriod = CheckExists.GamePeriod;
                cl.FinalStatus = true;
                db.WX_UserChangeLog.InsertOnSubmit(cl);

                try
                {
                    db.SubmitChanges();
                    return "";
                }
                catch (Exception AnyError)
                {


                    Console.Write(AnyError.Message + Environment.NewLine);
                    Console.Write(AnyError.StackTrace + Environment.NewLine);
                    return AnyError.Message;
                }

                return "";

            }//有记录合并
            else
            {

                #region "没有的新加"

                #region "转化赔率"
                var CheckRatioConfig = db.Game_BasicRatio.Where(t => t.GameType == "重庆时时彩"
                    && t.aspnet_UserID == GlobalParam.Key
                    && t.BuyType == BuyType
                    && t.BuyValue == BuyValue
                    && t.MaxBuy >= BuyPoint
                    && ((t.MinBuy <= (BuyPoint) && t.IncludeMin == true)
                    || (t.MinBuy < (BuyPoint) && t.IncludeMin == false)
                    )
                    );
                if (CheckRatioConfig.Count() == 0)
                {
                    Decimal? MaxLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.Key
                        && t.BuyValue == BuyValue
                        && t.BuyType == BuyType
                        ).Max(t => t.MaxBuy);
                    Decimal? MinLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.Key
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
                newgl.aspnet_UserID = GlobalParam.Key;
                newgl.Buy_Point = BuyPoint;
                newgl.Buy_Type = BuyType;
                newgl.Buy_Value = BuyValue;
                newgl.GameName = "重庆时时彩";
                newgl.TransTime = replylog.ReceiveTime;
                newgl.WX_UserName = replylog.WX_UserName;
                newgl.Buy_Ratio = CheckRatioConfig.Count() == 0 ? 0 : CheckRatioConfig.First().BasicRatio;

                newgl.Result_HaveProcess = false;
                newgl.GamePeriod = replylog.ReceiveTime.ToString("yyyyMMdd") + NextPeriod;

                string LocalDay = (replylog.ReceiveTime.Hour <= 2 ? replylog.ReceiveTime.AddDays(-1).ToString("yyyyMMdd") : replylog.ReceiveTime.ToString("yyyyMMdd"));
                newgl.GameLocalPeriod = LocalDay + NextLocalPeriod;

                Game_BasicRatio Middbouns = db.Game_BasicRatio.SingleOrDefault(t =>
                    t.aspnet_UserID == GlobalParam.Key
                    && t.BuyType == "不吃"
                    && t.BuyValue == "和"
                    && t.MaxBuy >= newgl.Buy_Point
                     && t.MinBuy <= newgl.Buy_Point
                    );

                newgl.BounsRatio_WhenMiddle = (Middbouns == null ? 0.0M : Middbouns.BasicRatio);


                Game_BasicRatio okbouns = db.Game_BasicRatio.SingleOrDefault(t =>
                   t.aspnet_UserID == GlobalParam.Key
                   && t.BuyType == "不吃"
                   && t.BuyValue == "合"
                   && t.MaxBuy >= newgl.Buy_Point
                    && t.MinBuy <= newgl.Buy_Point
                   );

                newgl.BounsRatio_WhenOK = (okbouns == null ? 0.0M : okbouns.BasicRatio);

                Game_BasicRatio bouns23 = db.Game_BasicRatio.SingleOrDefault(t =>
                   t.aspnet_UserID == GlobalParam.Key
                   && t.BuyType == "不吃"
                   && t.BuyValue == "23"
                   && t.MaxBuy >= newgl.Buy_Point
                    && t.MinBuy <= newgl.Buy_Point
                   );

                newgl.BounsRatio_Wen23 = (bouns23 == null ? 0.0M : bouns23.BasicRatio);


                db.WX_UserGameLog.InsertOnSubmit(newgl);
                Linq.WX_UserChangeLog cl = null;

                cl = new WX_UserChangeLog();
                cl.aspnet_UserID = GlobalParam.Key;
                cl.WX_UserName = replylog.WX_UserName;
                cl.ChangePoint = -BuyPoint;
                cl.NeedNotice = false;
                cl.HaveNotice = false;
                cl.ChangeTime = replylog.ReceiveTime;
                cl.RemarkType = "下单";
                cl.Remark = "下单@#" + replylog.ReceiveContent;
                cl.FinalStatus = false;
                cl.BuyValue = BuyValue;
                cl.GamePeriod = newgl.GamePeriod;
                cl.FinalStatus = true;
                db.WX_UserChangeLog.InsertOnSubmit(cl);

                try
                {
                    db.SubmitChanges();
                    return "";
                }
                catch (Exception AnyError)
                {
                    return AnyError.Message;
                    Console.Write(AnyError.Message + Environment.NewLine);
                    Console.Write(AnyError.StackTrace + Environment.NewLine);
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
        public static decimal WXUserChangeLog_GetRemainder(string UserContactID)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            var RemindList = db.WX_UserChangeLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                && t.WX_UserName == UserContactID
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



    }
}
