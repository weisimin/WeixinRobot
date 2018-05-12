using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WeixinRoboot
{
    public partial class OpenQuery : Form
    {
        public OpenQuery()
        {
            InitializeComponent();

        }
        public RunnerForm RunnerF = null;
        private void gv_result_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            gv_result.Rows.Clear();

            gv_result.Columns.Clear();





            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");




            DataTable Result = new DataTable();
            Result.Columns.Add("类别");
            DataGridViewColumn dc = new DataGridViewColumn();
            dc.HeaderText = "类别";
            dc.Name = "类别";
            dc.DataPropertyName = "类别";
            dc.CellTemplate = new DataGridViewTextBoxCell();
            dc.Frozen = true;
            gv_result.Columns.Add(dc);



            var buys = (from ds in db.WX_UserGameLog
                        where
                        ds.aspnet_UserID == GlobalParam.Key
        && String.Compare(ds.GameLocalPeriod.Substring(0, 8), dtp_startdate.Value.ToString("yyyyMMdd")) >= 0
        && String.Compare(ds.GameLocalPeriod.Substring(0, 8), dtp_enddate.Value.ToString("yyyyMMdd")) <= 0
                        select ds).ToList();

            var myWXUSERS = db.WX_UserReply.Where(t => t.aspnet_UserID == GlobalParam.Key && t.IsReply == true).Select(t => t.WX_UserName).Distinct();

            foreach (var item in myWXUSERS)
            {

                DataColumn uc = new DataColumn();
                uc.ColumnName = item;

                DataGridViewColumn dcc = new DataGridViewColumn();

                dcc.Name = item;
                dcc.DataPropertyName = item;
                dcc.CellTemplate = new DataGridViewTextBoxCell();

                gv_result.Columns.Add(dcc);



                DataRow[] ur = RunnerF.MemberSource.Select("User_ContactID='" + item + "'");
                if (ur.Count() != 0)
                {
                    uc.Caption = ur[0].Field<string>("user_Contact");
                }
                else
                {
                    var FidnUser = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.Key && t.WX_UserName == item);

                    uc.Caption = (FidnUser == null ? item : (FidnUser.RemarkName != "" && FidnUser.RemarkName != null ? FidnUser.RemarkName + "@#" + FidnUser.NickName : FidnUser.NickName));

                }
                dcc.HeaderText = uc.Caption;
                Result.Columns.Add(uc);

            }

            DataColumn ucfull = new DataColumn();
            ucfull.ColumnName = "全部玩家";

            Result.Columns.Add(ucfull);

            DataGridViewColumn dccful = new DataGridViewColumn();

            dccful.Name = "全部玩家";
            dccful.DataPropertyName = "全部玩家";
            dccful.CellTemplate = new DataGridViewTextBoxCell();
            gv_result.Columns.Add(dccful);

            var BuyDays = buys.Select(t => t.GameLocalPeriod.Substring(0, 8)).Distinct().OrderBy(t => t);
            foreach (var item in BuyDays)
            {
                DataRow newr = Result.NewRow();

                newr.SetField("类别", item + "下注");
                foreach (var usritem in myWXUSERS)
                {
                    newr.SetField(usritem, NetFramework.Util_Math.NullToZero(buys.Where(t =>
                        t.WX_UserName == usritem
                        && t.aspnet_UserID == GlobalParam.Key
                        && t.GameLocalPeriod.StartsWith(item)).Sum(t => t.Buy_Point)));
                }
                newr.SetField("全部玩家", NetFramework.Util_Math.NullToZero(buys.Where(t =>
                         t.GameLocalPeriod.StartsWith(item)
                            && t.aspnet_UserID == GlobalParam.Key
                         ).Sum(t => t.Buy_Point)));


                Application.DoEvents();

                DataRow newr2 = Result.NewRow();
                newr2.SetField("类别", item + "得分");
                foreach (var usritem in myWXUSERS)
                {
                    newr2.SetField(usritem, NetFramework.Util_Math.NullToZero(buys.Where(t =>
                        t.WX_UserName == usritem
                           && t.aspnet_UserID == GlobalParam.Key
                         && t.GameLocalPeriod.StartsWith(item)
                        ).Sum(t => t.Result_Point)));
                }
                newr2.SetField("全部玩家", NetFramework.Util_Math.NullToZero(buys.Where(t =>
                        t.GameLocalPeriod.StartsWith(item)
                         && t.aspnet_UserID == GlobalParam.Key
                        ).Sum(t => t.Result_Point)));
                Application.DoEvents();






                //DataRow newr4 = Result.NewRow();
                //newr4.SetField("类别", item + "上分");
                //foreach (var usritem in myWXUSERS)
                //{
                //    newr4.SetField(usritem, NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                //        t.WX_UserName == usritem
                //         && t.RemarkType == "上分"
                //            && t.aspnet_UserID == GlobalParam.Key
                //         && t.ChangeLocalDay.StartsWith(item)
                //        ).Sum(t => t.ChangePoint)));
                //}
                //newr4.SetField("全部玩家", NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                //                     t.RemarkType == "上分"
                //                        && t.aspnet_UserID == GlobalParam.Key
                //         && t.ChangeLocalDay.StartsWith(item)
                //        ).Sum(t => t.ChangePoint)));
                //Application.DoEvents();


                //DataRow newr5 = Result.NewRow();
                //newr5.SetField("类别", item + "下分");
                //foreach (var usritem in myWXUSERS)
                //{
                //    newr5.SetField(usritem, NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                //        t.WX_UserName == usritem
                //         && t.RemarkType == "下分"
                //            && t.aspnet_UserID == GlobalParam.Key
                //         && t.ChangeLocalDay.StartsWith(item)
                //        ).Sum(t => t.ChangePoint)));
                //}
                //newr5.SetField("全部玩家", NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                //                     t.RemarkType == "下分"
                //                        && t.aspnet_UserID == GlobalParam.Key
                //         && t.ChangeLocalDay.StartsWith(item
                //         )
                //        ).Sum(t => t.ChangePoint)));
                //Application.DoEvents();


                DataRow newr6 = Result.NewRow();
                newr6.SetField("类别", item + "福利");
                foreach (var usritem in myWXUSERS)
                {
                    newr6.SetField(usritem, NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                        t.WX_UserName == usritem
                         && t.RemarkType == "福利"
                            && t.aspnet_UserID == GlobalParam.Key
                         && t.ChangeLocalDay.StartsWith(item)
                        ).Sum(t => t.ChangePoint)));
                }
                newr6.SetField("全部玩家", NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                                     t.RemarkType == "福利"
                                        && t.aspnet_UserID == GlobalParam.Key
                         && t.ChangeLocalDay.StartsWith(item)
                        ).Sum(t => t.ChangePoint)));
                Application.DoEvents();





                DataRow newr3 = Result.NewRow();
                newr3.SetField("类别", item + "合计");
                foreach (var usritem in myWXUSERS)
                {
                    newr3.SetField(usritem,
                        //NetFramework.Util_Math.NullToZero( db.WX_UserChangeLog.Where(t =>
                        //    t.WX_UserName == usritem
                        //     && t.RemarkType == "上分"
                        //        && t.aspnet_UserID == GlobalParam.Key
                        //     && t.ChangeLocalDay.StartsWith(item)
                        //    ).Sum(t => t.ChangePoint))

                        NetFramework.Util_Math.NullToZero(buys.Where(t =>
                        t.WX_UserName == usritem
                           && t.aspnet_UserID == GlobalParam.Key
                         && t.GameLocalPeriod.StartsWith(item)
                        ).Sum(t => t.Buy_Point))

                                            - NetFramework.Util_Math.NullToZero(buys.Where(t =>
                        t.WX_UserName == usritem
                           && t.aspnet_UserID == GlobalParam.Key
                         && t.GameLocalPeriod.StartsWith(item)
                        ).Sum(t => t.Result_Point))

                        //- NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                        //t.WX_UserName == usritem
                        // && t.RemarkType == "下分"
                        //    && t.aspnet_UserID == GlobalParam.Key
                        // && t.ChangeLocalDay.StartsWith(item)
                        //).Sum(t => t.ChangePoint))

                       - NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                                     t.RemarkType == "福利"
                                     && t.WX_UserName == usritem
                                        && t.aspnet_UserID == GlobalParam.Key
                         && t.ChangeLocalDay.StartsWith(item)
                        ).Sum(t => t.ChangePoint))


                        );
                }
                newr3.SetField("全部玩家",

                   //NetFramework.Util_Math.NullToZero( db.WX_UserChangeLog.Where(t =>

                        //  t.RemarkType == "上分"
                    //     && t.aspnet_UserID == GlobalParam.Key
                    // && t.ChangeLocalDay.StartsWith(item)
                    //).Sum(t => t.ChangePoint))

                        NetFramework.Util_Math.NullToZero(buys.Where(t =>

                         t.GameLocalPeriod.StartsWith(item)
                            && t.aspnet_UserID == GlobalParam.Key
                        ).Sum(t => t.Buy_Point))

                                            - NetFramework.Util_Math.NullToZero(buys.Where(t =>

                         t.GameLocalPeriod.StartsWith(item)
                            && t.aspnet_UserID == GlobalParam.Key
                        ).Sum(t => t.Result_Point))

                        //- NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>

                        //  t.RemarkType == "下分"
                    // && t.ChangeLocalDay.StartsWith(item)
                    //    && t.aspnet_UserID == GlobalParam.Key
                    //).Sum(t => t.ChangePoint))

                        - NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                                     t.RemarkType == "福利"
                                        && t.aspnet_UserID == GlobalParam.Key
                         && t.ChangeLocalDay.StartsWith(item)
                        ).Sum(t => t.ChangePoint))


                        );




                Application.DoEvents();

                #region 总期数

                DataRow newr8 = Result.NewRow();
                newr8.SetField("类别", item + "期数");
                foreach (var usritem in myWXUSERS)
                {
                    newr8.SetField(usritem, NetFramework.Util_Math.NullToZero(db.WX_UserGameLog.Where(t =>
                        t.WX_UserName == usritem
                            && t.aspnet_UserID == GlobalParam.Key
                         && t.GameLocalPeriod.StartsWith(item)
                        ).Select(t => t.GamePeriod).Distinct().Count()));
                }
                newr8.SetField("全部玩家", NetFramework.Util_Math.NullToZero(db.WX_UserGameLog.Where(t =>
                             t.aspnet_UserID == GlobalParam.Key
                         && t.GameLocalPeriod.StartsWith(item)
                        ).Select(t => t.GamePeriod).Distinct().Count()));
                Application.DoEvents();
                #endregion

                #region "平均值"

                DataRow newr7 = Result.NewRow();
                newr7.SetField("类别", item + "平均");
                foreach (var usritem in myWXUSERS)
                {

                    decimal e_TotalBuy = NetFramework.Util_Math.NullToZero(buys.Where(t =>
                         t.WX_UserName == usritem
                            && t.GameLocalPeriod.StartsWith(item)
                               && t.aspnet_UserID == GlobalParam.Key
                           ).Sum(t => t.Buy_Point));

                    decimal e_TotalPeriod = NetFramework.Util_Math.NullToZero(db.WX_UserGameLog.Where(t =>
                        t.WX_UserName == usritem
                            && t.aspnet_UserID == GlobalParam.Key
                         && t.GameLocalPeriod.StartsWith(item)
                        ).Select(t => t.GamePeriod).Distinct().Count());


                    newr7.SetField(usritem, (e_TotalPeriod == 0 ? 0 : e_TotalBuy / e_TotalPeriod));
                }

                decimal TotalBuy = NetFramework.Util_Math.NullToZero(buys.Where(t =>

                        t.GameLocalPeriod.StartsWith(item)
                           && t.aspnet_UserID == GlobalParam.Key
                       ).Sum(t => t.Buy_Point));

                decimal TotalPeriod = NetFramework.Util_Math.NullToZero(db.WX_UserGameLog.Where(t =>

                         t.aspnet_UserID == GlobalParam.Key
                     && t.GameLocalPeriod.StartsWith(item)
                    ).Select(t => t.GamePeriod).Distinct().Count());

                newr7.SetField("全部玩家", (TotalPeriod == 0 ? 0 : TotalBuy / TotalPeriod));
                Application.DoEvents();

                #endregion










                // Result.Rows.Add(newr4);
                Result.Rows.Add(newr);


                Result.Rows.Add(newr2);

                //Result.Rows.Add(newr5);
                Result.Rows.Add(newr6);
                Result.Rows.Add(newr3);

                Result.Rows.Add(newr8);
                Result.Rows.Add(newr7);


            }//每日循环
            BS_GVResult.DataSource = Result;








        }









    }
}
