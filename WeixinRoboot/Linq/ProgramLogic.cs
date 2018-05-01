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
                        string BuyXNum = gamelogitem.Buy_Value.Substring(1, 1);
                        string Grnum1 = gamelogitem.GameResult.Substring(0, 1);
                        string Grnum2 = gamelogitem.GameResult.Substring(1, 1);
                        string Grnum3 = gamelogitem.GameResult.Substring(2, 1);
                        string Grnum4 = gamelogitem.GameResult.Substring(3, 1);
                        string Grnum5 = gamelogitem.GameResult.Substring(4, 1);
                        int FullCount = 0;
                        if (Grnum1 == BuyXNum)
                        {
                            FullCount = 1;
                        }
                        if (Grnum1 == BuyXNum && Grnum2 == BuyXNum)
                        {
                            FullCount = 2;
                        }
                        if (Grnum1 == BuyXNum && Grnum2 == BuyXNum && Grnum3 == BuyXNum)
                        {
                            FullCount = 3;
                        }
                        if (Grnum1 == BuyXNum && Grnum2 == BuyXNum && Grnum3 == BuyXNum && Grnum4 == BuyXNum)
                        {
                            FullCount = 4;
                        }
                        if (Grnum1 == BuyXNum && Grnum2 == BuyXNum && Grnum3 == BuyXNum && Grnum4 == BuyXNum && Grnum5 == BuyXNum)
                        {
                            FullCount = 5;
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
                        string Pos_BuyXNum = gamelogitem.Buy_Value.Substring(1, 1);

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
                                    break;
                                case "十":
                                    if (Pos_BGrnum4 == Pos_BuyXNum)
                                    {
                                        gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio;
                                    }
                                    break;
                                case "百":
                                    if (Pos_BGrnum3 == Pos_BuyXNum)
                                    {
                                        gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio;
                                    }
                                    break;
                                case "千":
                                    if (Pos_BGrnum2 == Pos_BuyXNum)
                                    {
                                        gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio;
                                    }
                                    break;
                                case "万":
                                    if (Pos_BGrnum1 == Pos_BuyXNum)
                                    {
                                        gamelogitem.Result_Point = gamelogitem.Buy_Point * gamelogitem.Buy_Ratio;
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
                if (true)
                {

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

                TotalResult tr1 = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
              && t.WX_UserName == reply.WX_UserName
              && t.Buy_Point != 0
              && t.Result_HaveProcess != true
              ).ToList(), MemberSource);
                return "下注不足，" + tr1.ToSlimStringV2() + "余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0"); ;
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
                string CheckResult = "";
                GameLogChangeAndCheck(reply, modiitem, out CheckResult);
                if (CheckResult != "")
                {
                    TotalResult tr2 = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
               && t.WX_UserName == reply.WX_UserName
               && t.Buy_Point != 0
               && t.Result_HaveProcess != true
               ).ToList(), MemberSource);
                    return CheckResult + "," + tr2.ToSlimStringV2() + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0"); ; ;
                }
                else if (modiitem.Buy_Point == 0)
                {

                    modiitem.Result_Point = 0;
                    modiitem.GP_LastModify = reply.ReceiveTime;
                }



                Linq.WX_UserChangeLog cl = null;
                cl = new WX_UserChangeLog();
                cl.aspnet_UserID = GlobalParam.Key;
                cl.WX_UserName = modiitem.WX_UserName;
                cl.ChangePoint = Convert.ToDecimal(Str_BuyPoint);
                cl.NeedNotice = false;
                cl.HaveNotice = false;
                cl.ChangeTime = reply.ReceiveTime;
                cl.RemarkType = "取消";
                cl.Remark = "取消@#" + reply.ReceiveContent;
                cl.FinalStatus = false;
                cl.BuyValue = FirstIndex;
                cl.GamePeriod = modiitem.GamePeriod;
                cl.FinalStatus = true;

                cl.BuyValue = modiitem.Buy_Value;
                cl.GamePeriod = modiitem.GamePeriod;

                db.WX_UserChangeLog.InsertOnSubmit(cl);
                db.SubmitChanges();






                #endregion
            }//循环处理


            TotalResult tr = BuildResult(db.WX_UserGameLog.Where(t => t.aspnet_UserID == GlobalParam.Key
                && t.WX_UserName == reply.WX_UserName
                && t.Buy_Point != 0
                && t.Result_HaveProcess != true
                ).ToList(), MemberSource);

            return tr.ToSlimStringV2() + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");











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

                    Result += Perioditem + "期";
                    foreach (var buyitem in Buys.Where(t => t.ShowPeriod == Perioditem))
                    {
                        Result += (buyitem.TotalTiger == 0 ? "" : ",虎" + ObjectToString(buyitem.TotalTiger, "N0"))
                        + (buyitem.TotalDragon == 0 ? "" : ",龙" + ObjectToString(buyitem.TotalDragon, "N0"))
                        + (buyitem.TotalOK == 0 ? "" : ",合" + ObjectToString(buyitem.TotalOK, "N0"))
                        + (buyitem.TotalLarge == 0 ? "" : ",大" + ObjectToString(buyitem.TotalLarge, "N0"))
                        + (buyitem.TotalSmall == 0 ? "" : ",小" + ObjectToString(buyitem.TotalSmall, "N0"))
                        + (buyitem.TotalMiddle == 0 ? "" : ",和" + ObjectToString(buyitem.TotalMiddle, "N0"))
                        + (buyitem.TotalSingle == 0 ? "" : ",单" + ObjectToString(buyitem.TotalSingle, "N0"))
                        + (buyitem.TotalDouble == 0 ? "" : ",双" + ObjectToString(buyitem.TotalDouble, "N0"));
                    }
                    Result += Environment.NewLine;
                }
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
        public static string WX_UserReplyLog_Create(WX_UserReplyLog reply, DataTable MemberSource)
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

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            if (reply.ReceiveContent != "")
            {
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

                    string Result = "下注期数是" + TodayBuys.Select(t => t.GamePeriod).Count().ToString()
                    + "期，下注总额是" + ObjectToString(TodayBuys.Sum(t => t.Buy_Point), "N0")
                    + ",得分总额是" + ObjectToString(TodayBuys.Sum(t => t.Result_Point), "N0")
                    + ",结果是" + ObjectToString((TodayBuys.Sum(t => t.Result_Point) - TodayBuys.Sum(t => t.Buy_Point)), "N0");

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
                else if (reply.ReceiveContent == "开奖")
                {
                    string Result = "";
                    DateTime TestPeriod = DateTime.Now;
                    if (TestPeriod.Hour <= 8)
                    {
                        TestPeriod=TestPeriod.AddDays(-1);
                    }
                    var TodatBuyGameLog = db.WX_UserGameLog.Where(t =>
                        t.aspnet_UserID == GlobalParam.Key
                        && t.WX_UserName == reply.WX_UserName
                        && t.Buy_Point != 0
                        && t.GameLocalPeriod.StartsWith(TestPeriod.ToString("yyyyMMdd"))
                        );
                    TotalResult tr = BuildResult(TodatBuyGameLog.ToList(), MemberSource);
                    Result = tr.ToOpenString();

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
                    Result = tr.ToOpenString();

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
                else if (reply.ReceiveContent.StartsWith("全"))
                {
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
                    if (reply.ReceiveTime.Hour >= 2 && reply.ReceiveTime.Hour < 9)
                    {
                        return "封盘时间";
                    }
                    Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == reply.ReceiveTime.ToString("HH:mm"));
                    if (testmin != null)
                    {
                        return "整点" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                    }
                    string BuyXnUMBER = reply.ReceiveContent.Substring(1,1);
                    if (BuyXnUMBER == "0" || BuyXnUMBER == "1" || BuyXnUMBER == "2" || BuyXnUMBER == "3" || BuyXnUMBER == "4" || BuyXnUMBER == "5" || BuyXnUMBER == "6" || BuyXnUMBER == "7" || BuyXnUMBER == "8" || BuyXnUMBER == "9")
                    {

                        #region 检查赔率
                        var ratios = db.Game_BasicRatio.Where(t => t.BuyType == "全X" && t.aspnet_UserID == GlobalParam.Key);
                        if (ratios.Count() == 0)
                        {

                        }
                        else
                        { 
                        
                        }
                        #endregion

                        #region 检查余额
                        #endregion
                        WX_UserGameLog gl = new WX_UserGameLog();


                    }
                    else
                    {
                        return "";
                    }


                }//全
                else if (reply.ReceiveContent.StartsWith("个") ||
                    reply.ReceiveContent.StartsWith("十") ||
                    reply.ReceiveContent.StartsWith("百") ||
                    reply.ReceiveContent.StartsWith("千") ||
                    reply.ReceiveContent.StartsWith("万")
                    )
                {
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
                    if (reply.ReceiveTime.Hour >= 2 && reply.ReceiveTime.Hour < 9)
                    {
                        return "封盘时间";
                    }
                    Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == reply.ReceiveTime.ToString("HH:mm"));
                    if (testmin != null)
                    {
                        return "整点" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

                    }
                }//定数字或定大小
               

                else
                {
                    Linq.aspnet_UsersNewGameResultSend myset = db.aspnet_UsersNewGameResultSend.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key);
                    if (myset.IsBlock == true)
                    {
                        return "封盘";
                    }
                  
                    string FirstIndex = reply.ReceiveContent.Substring(0, 1);
                    string Str_BuyPoint = reply.ReceiveContent.Substring(1);
                    decimal BuyPoint = 0;
                    try
                    {
                        BuyPoint = Convert.ToDecimal(Str_BuyPoint);
                    }
                    catch (Exception Str1Error)
                    {
                        #region 组合类
                        if (reply.ReceiveContent.Length>=3)
                        {
                            string Key2 = reply.ReceiveContent.Substring(0, 2);
                            string StrBuyPoint2 = reply.ReceiveContent.Substring(2);

                            try
                            {

                            }
                            catch (Exception Str2Error)
                            {

                                string Key3 = reply.ReceiveContent.Substring(0, 3);
                                string StrBuyPoint3 = reply.ReceiveContent.Substring(3);
                                Math.is

                            }

                        }

                        #endregion
                        return "";
                    }
                    if (BuyPoint == 0)
                    {
                        return "";
                    }
                    #region
                    if (reply.ReceiveTime.Hour >= 2 && reply.ReceiveTime.Hour < 9)
                    {
                        return "封盘时间";
                    }
                    Game_ChongqingshishicaiPeriodMinute testmin = db.Game_ChongqingshishicaiPeriodMinute.SingleOrDefault(t => t.TimeMinute == reply.ReceiveTime.ToString("HH:mm"));
                    if (testmin != null)
                    {
                        return "整点" + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");

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
                            return "";
                    }
                    if (newr != null && NewData == true)
                    {
                        newr.Result_Point = 0;
                        db.WX_UserGameLog.InsertOnSubmit(newr);
                    }
                    if (CheckResult != "")
                    {
                        TotalResult tr = BuildResult(checkHaveBuy, MemberSource);
                        return CheckResult + tr.ToSlimStringV2() + ",余" + ObjectToString(WXUserChangeLog_GetRemainder(reply.WX_UserName), "N0");
                    }
                    else
                    {
                        Linq.WX_UserChangeLog cl = null;

                        cl = new WX_UserChangeLog();
                        cl.aspnet_UserID = GlobalParam.Key;
                        cl.WX_UserName = newr.WX_UserName;
                        cl.ChangePoint = -BuyPoint;
                        cl.NeedNotice = false;
                        cl.HaveNotice = false;
                        cl.ChangeTime = reply.ReceiveTime;
                        cl.RemarkType = "下单";
                        cl.Remark = "下单@#" + reply.ReceiveContent;
                        cl.FinalStatus = false;
                        cl.BuyValue = FirstIndex;
                        cl.GamePeriod = newr.GamePeriod;
                        cl.FinalStatus = true;
                        db.WX_UserChangeLog.InsertOnSubmit(cl);

                        db.SubmitChanges();
                        TotalResult tr = BuildResult(checkHaveBuy, MemberSource);
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

            if (Content == "自动跟踪")
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
            if (Content == "取消自动跟踪")
            {

                string Contact = UserRow.Field<string>("User_ContactID");


                Linq.WX_UserReply toupdate = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == Contact);
                toupdate.IsReply = false;

                db.SubmitChanges();
                UserRow.SetField("User_IsReply", false);

                return "";
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



            WX_UserGameLog CheckExists = CheckHaveBuy.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key
                && t.WX_UserName == replylog.WX_UserName && t.GamePeriod == replylog.ReceiveTime.ToString("yyyyMMdd") + NextPeriod && t.Buy_Value == BuyValue);

            decimal Remainder = WXUserChangeLog_GetRemainder(replylog.WX_UserName);
            if (Remainder < BuyPoint)
            {
                CheckResult = "余分不足";
                NewData = false;
                return null;
            }

            if (CheckExists != null)
            {
                CheckExists.Buy_Point += BuyPoint;

                NewData = false;
                GameLogChangeAndCheck(replylog, CheckExists, out CheckResult);




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

                CheckHaveBuy.Add(newgl);
                GameLogChangeAndCheck(replylog, newgl, out CheckResult);





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

        private static void GameLogChangeAndCheck(WX_UserReplyLog replylog, WX_UserGameLog newgl, out string CheckResult)
        {
            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

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
            if (CheckRatioConfig == null && newgl.Buy_Point != 0)
            {
                Decimal? MaxLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.Key
                    && t.BuyValue == newgl.Buy_Value
                    ).Max(t => t.MaxBuy);
                Decimal? MinLimit = db.Game_BasicRatio.Where(t => t.aspnet_UserID == GlobalParam.Key
                    && t.BuyValue == newgl.Buy_Value
                    ).Min(t => t.MinBuy);

                CheckResult = "超出" + ObjectToString(MinLimit, "N0") + "-" + ObjectToString(MaxLimit, "N0") + "范围";
                return;
            }
            else
            {

                newgl.Buy_Ratio = (CheckRatioConfig == null ? 0 : CheckRatioConfig.BasicRatio);
            }
            #endregion

            #region 检查余分
            decimal? NowRemainder = db.WX_UserChangeLog.Where(t => t.WX_UserName == replylog.WX_UserName
                && t.aspnet_UserID == GlobalParam.Key
                && (t.BuyValue != newgl.Buy_Value || t.BuyValue == null)
                && (t.GamePeriod != newgl.GamePeriod || t.GamePeriod == null)
                && (t.RemarkType != "取消")
                ).Sum(t => t.ChangePoint);
            NowRemainder -= newgl.Buy_Point;
            if (NowRemainder < 0)
            {
                CheckResult = "余分不足";
                return;
            }
            #endregion
            CheckResult = "";

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
