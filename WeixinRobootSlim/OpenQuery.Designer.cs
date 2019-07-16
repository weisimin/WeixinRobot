namespace WeixinRobootSlim
{
    partial class OpenQuery
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btn_ExportToExcel = new System.Windows.Forms.Button();
            this.gv_result = new System.Windows.Forms.DataGridView();
            this.BS_GVResult = new System.Windows.Forms.BindingSource(this.components);
            this.BtnQuery = new System.Windows.Forms.Button();
            this.dtp_startdate = new System.Windows.Forms.DateTimePicker();
            this.lbl_fromto = new System.Windows.Forms.Label();
            this.dtp_enddate = new System.Windows.Forms.DateTimePicker();
            this.cb_SourceType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.gv_result)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_GVResult)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ExportToExcel
            // 
            this.btn_ExportToExcel.Location = new System.Drawing.Point(13, 13);
            this.btn_ExportToExcel.Name = "btn_ExportToExcel";
            this.btn_ExportToExcel.Size = new System.Drawing.Size(75, 23);
            this.btn_ExportToExcel.TabIndex = 0;
            this.btn_ExportToExcel.Text = "导出Excel";
            this.btn_ExportToExcel.UseVisualStyleBackColor = true;
            this.btn_ExportToExcel.Click += new System.EventHandler(this.btn_ExportToExcel_Click);
            // 
            // gv_result
            // 
            this.gv_result.AllowUserToAddRows = false;
            this.gv_result.AllowUserToDeleteRows = false;
            this.gv_result.AllowUserToOrderColumns = true;
            this.gv_result.AutoGenerateColumns = false;
            this.gv_result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_result.DataSource = this.BS_GVResult;
            this.gv_result.Location = new System.Drawing.Point(13, 43);
            this.gv_result.Name = "gv_result";
            this.gv_result.ReadOnly = true;
            this.gv_result.RowHeadersVisible = false;
            this.gv_result.RowTemplate.Height = 23;
            this.gv_result.Size = new System.Drawing.Size(745, 322);
            this.gv_result.TabIndex = 1;
            this.gv_result.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gv_result_DataError);
            // 
            // BtnQuery
            // 
            this.BtnQuery.Location = new System.Drawing.Point(682, 14);
            this.BtnQuery.Name = "BtnQuery";
            this.BtnQuery.Size = new System.Drawing.Size(75, 23);
            this.BtnQuery.TabIndex = 2;
            this.BtnQuery.Text = "查询";
            this.BtnQuery.UseVisualStyleBackColor = true;
            this.BtnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // dtp_startdate
            // 
            this.dtp_startdate.CustomFormat = "yyyy-MM-dd";
            this.dtp_startdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_startdate.Location = new System.Drawing.Point(418, 14);
            this.dtp_startdate.Name = "dtp_startdate";
            this.dtp_startdate.Size = new System.Drawing.Size(105, 21);
            this.dtp_startdate.TabIndex = 3;
            // 
            // lbl_fromto
            // 
            this.lbl_fromto.AutoSize = true;
            this.lbl_fromto.Location = new System.Drawing.Point(532, 18);
            this.lbl_fromto.Name = "lbl_fromto";
            this.lbl_fromto.Size = new System.Drawing.Size(11, 12);
            this.lbl_fromto.TabIndex = 4;
            this.lbl_fromto.Text = "-";
            // 
            // dtp_enddate
            // 
            this.dtp_enddate.CustomFormat = "yyyy-MM-dd";
            this.dtp_enddate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_enddate.Location = new System.Drawing.Point(552, 14);
            this.dtp_enddate.Name = "dtp_enddate";
            this.dtp_enddate.Size = new System.Drawing.Size(105, 21);
            this.dtp_enddate.TabIndex = 5;
            // 
            // cb_SourceType
            // 
            this.cb_SourceType.FormattingEnabled = true;
            this.cb_SourceType.Items.AddRange(new object[] {
            "微",
            "易"});
            this.cb_SourceType.Location = new System.Drawing.Point(333, 15);
            this.cb_SourceType.Name = "cb_SourceType";
            this.cb_SourceType.Size = new System.Drawing.Size(58, 20);
            this.cb_SourceType.TabIndex = 6;
            // 
            // OpenQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 377);
            this.Controls.Add(this.cb_SourceType);
            this.Controls.Add(this.dtp_enddate);
            this.Controls.Add(this.lbl_fromto);
            this.Controls.Add(this.dtp_startdate);
            this.Controls.Add(this.BtnQuery);
            this.Controls.Add(this.gv_result);
            this.Controls.Add(this.btn_ExportToExcel);
            this.Name = "OpenQuery";
            this.Text = "查询";
            this.Load += new System.EventHandler(this.OpenQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gv_result)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_GVResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ExportToExcel;
        private System.Windows.Forms.DataGridView gv_result;
        private System.Windows.Forms.BindingSource BS_GVResult;
        private System.Windows.Forms.Button BtnQuery;
        private System.Windows.Forms.DateTimePicker dtp_startdate;
        private System.Windows.Forms.Label lbl_fromto;
        private System.Windows.Forms.DateTimePicker dtp_enddate;
        private System.Windows.Forms.ComboBox cb_SourceType;
    }
}