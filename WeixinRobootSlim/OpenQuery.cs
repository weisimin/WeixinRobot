using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
namespace WeixinRobootSlim
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
        && ds.WX_SourceType == cb_SourceType.SelectedItem.ToString()
                        select ds).ToList();

            NetFramework.Console.WriteLine("########################################################################", false);
            NetFramework.Console.WriteLine("查询日期" + dtp_startdate.Value.ToString("yyyyMMdd"), false);
            NetFramework.Console.WriteLine("查询日期" + dtp_enddate.Value.ToString("yyyyMMdd"), false);
            NetFramework.Console.WriteLine("########################################################################", false);

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

                dcc.HeaderText = uc.Caption;


            }



            DataGridViewColumn dccful = new DataGridViewColumn();

            dccful.Name = "全部玩家";
            dccful.DataPropertyName = "全部玩家";
            dccful.CellTemplate = new DataGridViewTextBoxCell();
            gv_result.Columns.Add(dccful);

           
  


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
