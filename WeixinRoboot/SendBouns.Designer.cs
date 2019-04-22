namespace WeixinRoboot
{
    partial class SendBouns
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
            this.dtp_querydate = new System.Windows.Forms.DateTimePicker();
            this.lbl_QueryDate = new System.Windows.Forms.Label();
            this.gv_result = new System.Windows.Forms.DataGridView();
            this.NickNameRemarkName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocalPeriodDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PeriodCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalBuy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AverageBuy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FixNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FlowPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IfDivousPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BounsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BS_DataSource = new System.Windows.Forms.BindingSource(this.components);
            this.BTN_QUERY = new System.Windows.Forms.Button();
            this.BTN_SEND = new System.Windows.Forms.Button();
            this.cb_SourceType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.gv_result)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_DataSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dtp_querydate
            // 
            this.dtp_querydate.CustomFormat = "yyyy-MM-dd";
            this.dtp_querydate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_querydate.Location = new System.Drawing.Point(77, 8);
            this.dtp_querydate.Name = "dtp_querydate";
            this.dtp_querydate.Size = new System.Drawing.Size(118, 21);
            this.dtp_querydate.TabIndex = 1;
            // 
            // lbl_QueryDate
            // 
            this.lbl_QueryDate.AutoSize = true;
            this.lbl_QueryDate.Location = new System.Drawing.Point(12, 14);
            this.lbl_QueryDate.Name = "lbl_QueryDate";
            this.lbl_QueryDate.Size = new System.Drawing.Size(59, 12);
            this.lbl_QueryDate.TabIndex = 2;
            this.lbl_QueryDate.Text = "统计日期:";
            // 
            // gv_result
            // 
            this.gv_result.AllowUserToAddRows = false;
            this.gv_result.AllowUserToDeleteRows = false;
            this.gv_result.AllowUserToOrderColumns = true;
            this.gv_result.AutoGenerateColumns = false;
            this.gv_result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_result.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NickNameRemarkName,
            this.LocalPeriodDay,
            this.PeriodCount,
            this.TotalBuy,
            this.AverageBuy,
            this.TotalResult,
            this.FixNumber,
            this.FlowPercent,
            this.IfDivousPercent,
            this.BounsCount,
            this.Remark});
            this.gv_result.DataSource = this.BS_DataSource;
            this.gv_result.Location = new System.Drawing.Point(14, 40);
            this.gv_result.Name = "gv_result";
            this.gv_result.RowTemplate.Height = 23;
            this.gv_result.Size = new System.Drawing.Size(694, 222);
            this.gv_result.TabIndex = 3;
            // 
            // NickNameRemarkName
            // 
            this.NickNameRemarkName.DataPropertyName = "NickNameRemarkName";
            this.NickNameRemarkName.Frozen = true;
            this.NickNameRemarkName.HeaderText = "玩家";
            this.NickNameRemarkName.Name = "NickNameRemarkName";
            this.NickNameRemarkName.ReadOnly = true;
            // 
            // LocalPeriodDay
            // 
            this.LocalPeriodDay.DataPropertyName = "LocalPeriodDay";
            this.LocalPeriodDay.Frozen = true;
            this.LocalPeriodDay.HeaderText = "统计期间";
            this.LocalPeriodDay.Name = "LocalPeriodDay";
            this.LocalPeriodDay.ReadOnly = true;
            // 
            // PeriodCount
            // 
            this.PeriodCount.DataPropertyName = "PeriodCount";
            this.PeriodCount.Frozen = true;
            this.PeriodCount.HeaderText = "期数";
            this.PeriodCount.Name = "PeriodCount";
            this.PeriodCount.ReadOnly = true;
            this.PeriodCount.Width = 70;
            // 
            // TotalBuy
            // 
            this.TotalBuy.DataPropertyName = "TotalBuy";
            this.TotalBuy.HeaderText = "总购买";
            this.TotalBuy.Name = "TotalBuy";
            this.TotalBuy.ReadOnly = true;
            this.TotalBuy.Width = 80;
            // 
            // AverageBuy
            // 
            this.AverageBuy.DataPropertyName = "AverageBuy";
            this.AverageBuy.HeaderText = "平均购买";
            this.AverageBuy.Name = "AverageBuy";
            this.AverageBuy.ReadOnly = true;
            this.AverageBuy.Width = 80;
            // 
            // TotalResult
            // 
            this.TotalResult.DataPropertyName = "TotalResult";
            this.TotalResult.HeaderText = "总得分";
            this.TotalResult.Name = "TotalResult";
            this.TotalResult.ReadOnly = true;
            this.TotalResult.Width = 80;
            // 
            // FixNumber
            // 
            this.FixNumber.DataPropertyName = "FixNumber";
            this.FixNumber.HeaderText = "福利分数";
            this.FixNumber.Name = "FixNumber";
            this.FixNumber.ReadOnly = true;
            this.FixNumber.Width = 80;
            // 
            // FlowPercent
            // 
            this.FlowPercent.DataPropertyName = "FlowPercent";
            this.FlowPercent.HeaderText = "流水比例";
            this.FlowPercent.Name = "FlowPercent";
            this.FlowPercent.ReadOnly = true;
            this.FlowPercent.Width = 80;
            // 
            // IfDivousPercent
            // 
            this.IfDivousPercent.DataPropertyName = "IfDivousPercent";
            this.IfDivousPercent.HeaderText = "负分比例";
            this.IfDivousPercent.Name = "IfDivousPercent";
            this.IfDivousPercent.ReadOnly = true;
            this.IfDivousPercent.Width = 80;
            // 
            // BounsCount
            // 
            this.BounsCount.DataPropertyName = "BounsCount";
            this.BounsCount.HeaderText = "应发放";
            this.BounsCount.Name = "BounsCount";
            // 
            // Remark
            // 
            this.Remark.DataPropertyName = "Remark";
            this.Remark.HeaderText = "备注";
            this.Remark.Name = "Remark";
            this.Remark.ReadOnly = true;
            this.Remark.Width = 300;
            // 
            // BTN_QUERY
            // 
            this.BTN_QUERY.Location = new System.Drawing.Point(12, 268);
            this.BTN_QUERY.Name = "BTN_QUERY";
            this.BTN_QUERY.Size = new System.Drawing.Size(77, 46);
            this.BTN_QUERY.TabIndex = 4;
            this.BTN_QUERY.Text = "查询";
            this.BTN_QUERY.UseVisualStyleBackColor = true;
            this.BTN_QUERY.Click += new System.EventHandler(this.BTN_QUERY_Click);
            // 
            // BTN_SEND
            // 
            this.BTN_SEND.Location = new System.Drawing.Point(482, 268);
            this.BTN_SEND.Name = "BTN_SEND";
            this.BTN_SEND.Size = new System.Drawing.Size(75, 46);
            this.BTN_SEND.TabIndex = 5;
            this.BTN_SEND.Text = "发放";
            this.BTN_SEND.UseVisualStyleBackColor = true;
            this.BTN_SEND.Click += new System.EventHandler(this.BTN_SEND_Click);
            // 
            // cb_SourceType
            // 
            this.cb_SourceType.FormattingEnabled = true;
            this.cb_SourceType.Location = new System.Drawing.Point(202, 8);
            this.cb_SourceType.Name = "cb_SourceType";
            this.cb_SourceType.Size = new System.Drawing.Size(69, 20);
            this.cb_SourceType.TabIndex = 6;
            // 
            // SendBouns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 319);
            this.Controls.Add(this.cb_SourceType);
            this.Controls.Add(this.BTN_SEND);
            this.Controls.Add(this.BTN_QUERY);
            this.Controls.Add(this.gv_result);
            this.Controls.Add(this.lbl_QueryDate);
            this.Controls.Add(this.dtp_querydate);
            this.Name = "SendBouns";
            this.Text = "发放福利";
            this.Load += new System.EventHandler(this.SendBouns_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gv_result)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_DataSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtp_querydate;
        private System.Windows.Forms.Label lbl_QueryDate;
        private System.Windows.Forms.DataGridView gv_result;
        private System.Windows.Forms.Button BTN_QUERY;
        private System.Windows.Forms.Button BTN_SEND;
        private System.Windows.Forms.BindingSource BS_DataSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn NickNameRemarkName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocalPeriodDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn PeriodCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalBuy;
        private System.Windows.Forms.DataGridViewTextBoxColumn AverageBuy;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn FixNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn FlowPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn IfDivousPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn BounsCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark;
        private System.Windows.Forms.ComboBox cb_SourceType;
    }
}