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
            if (BS_GVResult.DataSource != null)
            {
                ((DataTable)BS_GVResult.DataSource).Rows.Clear();
            }




            gv_result.Columns.Clear();





            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[ GlobalParam.DataSourceName].ConnectionString);
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
                        ds.aspnet_UserID == GlobalParam.UserKey
        && String.Compare(ds.GameLocalPeriod.Substring(0, 8), dtp_startdate.Value.ToString("yyyyMMdd")) >= 0
        && String.Compare(ds.GameLocalPeriod.Substring(0, 8), dtp_enddate.Value.ToString("yyyyMMdd")) <= 0
        &&ds.WX_SourceType==cb_SourceType.SelectedItem.ToString()
                        select ds).ToList();

            NetFramework.Console.WriteLine("########################################################################",false);
            NetFramework.Console.WriteLine("查询日期" + dtp_startdate.Value.ToString("yyyyMMdd"),false);
            NetFramework.Console.WriteLine("查询日期" + dtp_enddate.Value.ToString("yyyyMMdd"),false);
            NetFramework.Console.WriteLine("########################################################################",false);

            var myWXUSERS = buys.Select(t => t.WX_UserName).Distinct();

            foreach (var item in myWXUSERS)
            {

                DataColumn uc = new DataColumn();
                uc.ColumnName = item;

                DataGridViewColumn dcc = new DataGridViewColumn();

                dcc.Name = item;
                dcc.DataPropertyName = item;
                dcc.CellTemplate = new DataGridViewTextBoxCell();

                gv_result.Columns.Add(dcc);



                DataRow[] ur = RunnerF.MemberSource.Select("User_ContactID='" + item.Replace("'", "''") + "' and User_SourceType='" + cb_SourceType.SelectedItem.ToString() + "'");
                if (ur.Count() != 0)
                {
                    uc.Caption = ur[0].Field<string>("user_Contact");
                }
                else
                {
                    var FidnUser = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey && t.WX_UserName == item && t.WX_SourceType == cb_SourceType.SelectedItem.ToString());

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
            NetFramework.Console.WriteLine("########################################################################",false);
            NetFramework.Console.WriteLine("获得天数" + BuyDays.Count(),false);

            NetFramework.Console.WriteLine("########################################################################", false);

            #region "天数"
            foreach (var item in BuyDays)
            {
                DataRow newr = Result.NewRow();

                newr.SetField("类别", item + "下注");
                foreach (var usritem in myWXUSERS)
                {
                    newr.SetField(usritem, NetFramework.Util_Math.NullToZero(buys.Where(t =>
                        t.WX_UserName == usritem
                        && t.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                        && t.aspnet_UserID == GlobalParam.UserKey
                        && t.GameLocalPeriod.StartsWith(item)).Sum(t => t.Buy_Point)));
                }
                newr.SetField("全部玩家", NetFramework.Util_Math.NullToZero(buys.Where(t =>
                         t.GameLocalPeriod.StartsWith(item)
                            && t.aspnet_UserID == GlobalParam.UserKey
                         ).Sum(t => t.Buy_Point)));


                Application.DoEvents();

                DataRow newr2 = Result.NewRow();
                newr2.SetField("类别", item + "得分");
                foreach (var usritem in myWXUSERS)
                {
                    newr2.SetField(usritem, NetFramework.Util_Math.NullToZero(buys.Where(t =>
                        t.WX_UserName == usritem
                        && t.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                           && t.aspnet_UserID == GlobalParam.UserKey
                         && t.GameLocalPeriod.StartsWith(item)
                        ).Sum(t => t.Result_Point)));
                }
                newr2.SetField("全部玩家", NetFramework.Util_Math.NullToZero(buys.Where(t =>
                        t.GameLocalPeriod.StartsWith(item)
                         && t.aspnet_UserID == GlobalParam.UserKey
                        ).Sum(t => t.Result_Point)));
                Application.DoEvents();


                DataRow newr6 = Result.NewRow();
                newr6.SetField("类别", item + "福利");
                foreach (var usritem in myWXUSERS)
                {
                    newr6.SetField(usritem, NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                        t.WX_UserName == usritem
                          && t.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                         && t.RemarkType == "福利"
                            && t.aspnet_UserID == GlobalParam.UserKey
                         && t.ChangeLocalDay.StartsWith(item)
                        ).Sum(t => t.ChangePoint)));
                }
                newr6.SetField("全部玩家", NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                                     t.RemarkType == "福利"
                                        && t.aspnet_UserID == GlobalParam.UserKey
                         && t.ChangeLocalDay.StartsWith(item)
                        ).Sum(t => t.ChangePoint)));
                Application.DoEvents();


                DataRow newr3 = Result.NewRow();
                newr3.SetField("类别", item + "合计");
                foreach (var usritem in myWXUSERS)
                {
                    newr3.SetField(usritem,

                        NetFramework.Util_Math.NullToZero(buys.Where(t =>
                        t.WX_UserName == usritem
                          && t.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                           && t.aspnet_UserID == GlobalParam.UserKey
                         && t.GameLocalPeriod.StartsWith(item)
                        ).Sum(t => t.Buy_Point))

                                            - NetFramework.Util_Math.NullToZero(buys.Where(t =>
                        t.WX_UserName == usritem
                          && t.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                           && t.aspnet_UserID == GlobalParam.UserKey
                         && t.GameLocalPeriod.StartsWith(item)
                        ).Sum(t => t.Result_Point))


                       - NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                                     t.RemarkType == "福利"
                                     && t.WX_UserName == usritem
                                       && t.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                                        && t.aspnet_UserID == GlobalParam.UserKey
                         && t.ChangeLocalDay.StartsWith(item)
                        ).Sum(t => t.ChangePoint))


                        );
                }
                newr3.SetField("全部玩家",

                        NetFramework.Util_Math.NullToZero(buys.Where(t =>

                         t.GameLocalPeriod.StartsWith(item)
                            && t.aspnet_UserID == GlobalParam.UserKey
                        ).Sum(t => t.Buy_Point))

                                            - NetFramework.Util_Math.NullToZero(buys.Where(t =>

                         t.GameLocalPeriod.StartsWith(item)
                            && t.aspnet_UserID == GlobalParam.UserKey
                        ).Sum(t => t.Result_Point))

                        - NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                                     t.RemarkType == "福利"
                                        && t.aspnet_UserID == GlobalParam.UserKey
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
                          && t.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                            && t.aspnet_UserID == GlobalParam.UserKey
                         && t.GameLocalPeriod.StartsWith(item)
                        ).Select(t => t.GamePeriod).Distinct().Count()));
                }
                newr8.SetField("全部玩家", NetFramework.Util_Math.NullToZero(db.WX_UserGameLog.Where(t =>
                              t.aspnet_UserID == GlobalParam.UserKey
                         && t.GameLocalPeriod.StartsWith(item)
                        ).Select(t => new { t.GamePeriod,t.WX_UserName,t.WX_SourceType }).Distinct().Count()));
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
                              && t.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                               && t.aspnet_UserID == GlobalParam.UserKey
                           ).Sum(t => t.Buy_Point));

                    decimal e_TotalPeriod = NetFramework.Util_Math.NullToZero(db.WX_UserGameLog.Where(t =>
                        t.WX_UserName == usritem
                          && t.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                            && t.aspnet_UserID == GlobalParam.UserKey
                         && t.GameLocalPeriod.StartsWith(item)
                        ).Select(t => t.GamePeriod).Distinct().Count());


                    newr7.SetField(usritem, (e_TotalPeriod == 0 ? 0 : e_TotalBuy / e_TotalPeriod));
                }

                decimal TotalBuy = NetFramework.Util_Math.NullToZero(buys.Where(t =>

                        t.GameLocalPeriod.StartsWith(item)
                           && t.aspnet_UserID == GlobalParam.UserKey
                       ).Sum(t => t.Buy_Point));

                decimal TotalPeriod = NetFramework.Util_Math.NullToZero(db.WX_UserGameLog.Where(t =>

                         t.aspnet_UserID == GlobalParam.UserKey
                     && t.GameLocalPeriod.StartsWith(item)
                    ).Select(t => t.GamePeriod).Distinct().Count());

                newr7.SetField("全部玩家", (TotalPeriod == 0 ? 0 : TotalBuy / TotalPeriod));
                Application.DoEvents();

                #endregion

                Result.Rows.Add(newr);
                Result.Rows.Add(newr2);
                Result.Rows.Add(newr6);
                Result.Rows.Add(newr3);
                Result.Rows.Add(newr8);
                Result.Rows.Add(newr7);

            }//每日循环

            #endregion
            #region 所有天数
            DataRow newr_alldays = Result.NewRow();

            newr_alldays.SetField("类别", "所有天数下注");
            foreach (var usritem in myWXUSERS)
            {
                newr_alldays.SetField(usritem, NetFramework.Util_Math.NullToZero(buys.Where(t =>
                    t.WX_UserName == usritem
                      && t.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                    && t.aspnet_UserID == GlobalParam.UserKey

                    ).Sum(t => t.Buy_Point)));
            }
            newr_alldays.SetField("全部玩家", NetFramework.Util_Math.NullToZero(buys.Where(t =>
                         t.aspnet_UserID == GlobalParam.UserKey
                     ).Sum(t => t.Buy_Point)));


            Application.DoEvents();

            DataRow newr2_alldays = Result.NewRow();
            newr2_alldays.SetField("类别", "所有天数得分");
            foreach (var usritem in myWXUSERS)
            {
                newr2_alldays.SetField(usritem, NetFramework.Util_Math.NullToZero(buys.Where(t =>
                    t.WX_UserName == usritem
                      && t.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                       && t.aspnet_UserID == GlobalParam.UserKey

                    ).Sum(t => t.Result_Point)));
            }
            newr2_alldays.SetField("全部玩家", NetFramework.Util_Math.NullToZero(buys.Where(t =>

                      t.aspnet_UserID == GlobalParam.UserKey
                    ).Sum(t => t.Result_Point)));
            Application.DoEvents();


            DataRow newr6_alldays = Result.NewRow();
            newr6_alldays.SetField("类别", "所有天数福利");
            foreach (var usritem in myWXUSERS)
            {
                newr6_alldays.SetField(usritem, NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                    t.WX_UserName == usritem
                      && t.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                     && t.RemarkType == "福利"
                        && t.aspnet_UserID == GlobalParam.UserKey
                         && (string.Compare(t.ChangeLocalDay, dtp_startdate.Value.ToString("yyyyMMdd")) >= 0)
                                    && (string.Compare(dtp_enddate.Value.ToString("yyyyMMdd"), t.ChangeLocalDay) >= 0)
          ).Sum(t => t.ChangePoint)));
            }
            newr6_alldays.SetField("全部玩家", NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                                 t.RemarkType == "福利"
                                    && t.aspnet_UserID == GlobalParam.UserKey

                                    && (string.Compare(t.ChangeLocalDay, dtp_startdate.Value.ToString("yyyyMMdd")) >= 0)
                                    && (string.Compare(dtp_enddate.Value.ToString("yyyyMMdd"), t.ChangeLocalDay) >= 0)

                    ).Sum(t => t.ChangePoint)));
            Application.DoEvents();


            DataRow newr3_alldays = Result.NewRow();
            newr3_alldays.SetField("类别", "所有天数合计");
            foreach (var usritem in myWXUSERS)
            {
                newr3_alldays.SetField(usritem,

                    NetFramework.Util_Math.NullToZero(buys.Where(t =>
                    t.WX_UserName == usritem
                      && t.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                       && t.aspnet_UserID == GlobalParam.UserKey

                    ).Sum(t => t.Buy_Point))

                                        - NetFramework.Util_Math.NullToZero(buys.Where(t =>
                    t.WX_UserName == usritem
                      && t.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                       && t.aspnet_UserID == GlobalParam.UserKey

                    ).Sum(t => t.Result_Point))


                   - NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                                 t.RemarkType == "福利"

                                 && t.WX_UserName == usritem
                                   && t.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                                    && t.aspnet_UserID == GlobalParam.UserKey
                                     && (string.Compare(t.ChangeLocalDay, dtp_startdate.Value.ToString("yyyyMMdd")) >= 0)
                                    && (string.Compare(dtp_enddate.Value.ToString("yyyyMMdd"), t.ChangeLocalDay) >= 0)
    ).Sum(t => t.ChangePoint))


                    );
            }
            newr3_alldays.SetField("全部玩家",

                    NetFramework.Util_Math.NullToZero(buys.Where(t =>


                         t.aspnet_UserID == GlobalParam.UserKey
                    ).Sum(t => t.Buy_Point))

                                        - NetFramework.Util_Math.NullToZero(buys.Where(t =>


                         t.aspnet_UserID == GlobalParam.UserKey
                    ).Sum(t => t.Result_Point))

                    - NetFramework.Util_Math.NullToZero(db.WX_UserChangeLog.Where(t =>
                                 t.RemarkType == "福利"
                                    && t.aspnet_UserID == GlobalParam.UserKey
     && (string.Compare(t.ChangeLocalDay, dtp_startdate.Value.ToString("yyyyMMdd")) >= 0)
                                    && (string.Compare(dtp_enddate.Value.ToString("yyyyMMdd"), t.ChangeLocalDay) >= 0)
                    ).Sum(t => t.ChangePoint))

                    );


            Application.DoEvents();

            #region 总期数

            DataRow newr8_alldays = Result.NewRow();
            newr8_alldays.SetField("类别", "所有天数期数");
            foreach (var usritem in myWXUSERS)
            {
                newr8_alldays.SetField(usritem, NetFramework.Util_Math.NullToZero(db.WX_UserGameLog.Where(t =>
                    t.WX_UserName == usritem
                      && t.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                        && t.aspnet_UserID == GlobalParam.UserKey
                        && (string.Compare(t.GameLocalPeriod, dtp_startdate.Value.ToString("yyyyMMdd")) >= 1)
                          && (string.Compare(t.GameLocalPeriod, dtp_enddate.Value.ToString("yyyyMMdd")) <= 1)
                    ).Select(t => t.GamePeriod).Distinct().Count()));
            }
            newr8_alldays.SetField("全部玩家", NetFramework.Util_Math.NullToZero(db.WX_UserGameLog.Where(t =>
                         t.aspnet_UserID == GlobalParam.UserKey
                          && (string.Compare(t.GameLocalPeriod, dtp_startdate.Value.ToString("yyyyMMdd")) >= 1)
                          && (string.Compare(t.GameLocalPeriod, dtp_enddate.Value.ToString("yyyyMMdd")) <= 1)
                    ).Select(t => t.GamePeriod).Distinct().Count()));
            Application.DoEvents();
            #endregion

            #region "平均值"

            DataRow newr7_alldays = Result.NewRow();
            newr7_alldays.SetField("类别", "所有天数平均");
            foreach (var usritem in myWXUSERS)
            {

                decimal e_TotalBuy = NetFramework.Util_Math.NullToZero(buys.Where(t =>
                     t.WX_UserName == usritem
                       && t.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                           && t.aspnet_UserID == GlobalParam.UserKey
                       ).Sum(t => t.Buy_Point));

                decimal e_TotalPeriod = NetFramework.Util_Math.NullToZero(db.WX_UserGameLog.Where(t =>
                    t.WX_UserName == usritem
                      && t.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                        && t.aspnet_UserID == GlobalParam.UserKey
                         && (string.Compare(t.GameLocalPeriod, dtp_startdate.Value.ToString("yyyyMMdd")) >= 1)
                          && (string.Compare(t.GameLocalPeriod, dtp_enddate.Value.ToString("yyyyMMdd")) <= 1)
                    ).Select(t => t.GamePeriod).Distinct().Count());


                newr7_alldays.SetField(usritem, (e_TotalPeriod == 0 ? 0 : e_TotalBuy / e_TotalPeriod));
            }

            decimal TotalBuy_alldays = NetFramework.Util_Math.NullToZero(buys.Where(t =>


                        t.aspnet_UserID == GlobalParam.UserKey
                   ).Sum(t => t.Buy_Point));

            decimal TotalPeriod_alldays = NetFramework.Util_Math.NullToZero(db.WX_UserGameLog.Where(t =>

                     t.aspnet_UserID == GlobalParam.UserKey

                ).Select(t => t.GamePeriod).Distinct().Count());

            newr7_alldays.SetField("全部玩家", (TotalPeriod_alldays == 0 ? 0 : TotalBuy_alldays / TotalPeriod_alldays));
            Application.DoEvents();

            #endregion

            Result.Rows.Add(newr_alldays);
            Result.Rows.Add(newr2_alldays);
            Result.Rows.Add(newr6_alldays);
            Result.Rows.Add(newr3_alldays);
            Result.Rows.Add(newr8_alldays);
            Result.Rows.Add(newr7_alldays);

            #endregion


            BS_GVResult.DataSource = Result;



            this.Refresh();



        }

        private void OpenQuery_Load(object sender, EventArgs e)
        {
            cb_SourceType.SelectedIndex = 0;
        }

        private void btn_ExportToExcel_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }









    }
}
