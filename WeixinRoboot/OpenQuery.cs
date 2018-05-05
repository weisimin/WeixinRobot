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
            for (int i = 0; i < gv_result.Columns.Count; i++)
            {
                gv_result.Columns.RemoveAt(gv_result.Columns.Count-i-1);
            }


           

            Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
         



            DataTable Result = new DataTable();
            Result.Columns.Add("类别");
            DataGridViewColumn dc = new DataGridViewColumn();
            dc.HeaderText = "类别";
            dc.Name = "类别";
            dc.DataPropertyName = "类别";
            dc.CellTemplate = new DataGridViewTextBoxCell();
            gv_result.Columns.Add(dc);



            var buys = (from ds in db.WX_UserGameLog
                        where
                        ds.aspnet_UserID == GlobalParam.Key
        && String.Compare(ds.GameLocalPeriod.Substring(0,8), dtp_startdate.Value.ToString("yyyyMMdd")) >= 0
        && String.Compare(ds.GameLocalPeriod.Substring(0, 8), dtp_enddate.Value.ToString("yyyyMMdd")) <= 0
                        select ds).ToList();

            var myWXUSERS = buys.Select(t => t.WX_UserName).Distinct();

            foreach (var item in myWXUSERS)
            {

                DataColumn uc = new DataColumn();
                uc.ColumnName = item;

                DataGridViewColumn dcc = new DataGridViewColumn();
               
                dcc.Name = item;
                dcc.DataPropertyName = item;
                gv_result.Columns.Add(dcc);
                dcc.CellTemplate =  new DataGridViewTextBoxCell();


                DataRow[] ur = RunnerF.MemberSource.Select("User_ContactID='" + item + "'");
                if (ur.Count() != 0)
                {
                  uc.Caption = ur[0].Field<string>("user_Contact");

                }
                else
                {
                    uc.Caption = item;
                   
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

            var BuyDays = buys.Select(t => t.GameLocalPeriod.Substring(0,8)).Distinct().OrderBy(t=>t);
            foreach (var item in BuyDays)
            {
                DataRow newr = Result.NewRow();

                newr.SetField("类别", item + "下注");
                foreach (var usritem in myWXUSERS)
                {
                    newr.SetField(usritem, buys.Where(t =>
                        t.WX_UserName == usritem
                        &&t.GameLocalPeriod.StartsWith(item)).Sum(t => t.Buy_Point));
                }
                newr.SetField("全部玩家", buys.Where(t =>
                         t.GameLocalPeriod.StartsWith(item)).Sum(t => t.Buy_Point));


                Application.DoEvents();

                DataRow newr2 = Result.NewRow();
                newr2.SetField("类别", item + "得分");
                foreach (var usritem in myWXUSERS)
                {
                    newr2.SetField(usritem, buys.Where(t => 
                        t.WX_UserName == usritem
                         &&t.GameLocalPeriod.StartsWith(item)
                        ).Sum(t => t.Result_Point));
                }
                newr2.SetField("全部玩家", buys.Where(t =>
                        t.GameLocalPeriod.StartsWith(item)).Sum(t => t.Result_Point));
                Application.DoEvents();

                DataRow newr3 = Result.NewRow();
                newr3.SetField("类别", item + "合计");
                foreach (var usritem in myWXUSERS)
                {
                    newr3.SetField(usritem, buys.Where(t =>
                        t.WX_UserName == usritem
                         && t.GameLocalPeriod.StartsWith(item)
                        ).Sum(t => t.Result_Point-t.Buy_Point));
                }
                newr3.SetField("全部玩家", buys.Where(t =>
                        t.GameLocalPeriod.StartsWith(item)).Sum(t => t.Result_Point-t.Buy_Point));
                Application.DoEvents();


                Result.Rows.Add(newr);
                Result.Rows.Add(newr2);
                Result.Rows.Add(newr3);

            }
            BS_GVResult.DataSource = Result;
           







        }









    }
}
