namespace WeixinRoboot
{
    partial class Download163AndDeal
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
            this.gv_GameResult = new System.Windows.Forms.DataGridView();
            this.Gr_GamePeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gr_GameTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gr_GameResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gr_NumTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gr_BigSmall = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gr_SingleDouble = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gr_DragonTiger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gr_GamePrivatePeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BS_GameResult = new System.Windows.Forms.BindingSource(this.components);
            this.Dtp_DownloadDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_download = new System.Windows.Forms.Button();
            this.lbl_GameResult = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gv_GameLogNotDeal = new System.Windows.Forms.DataGridView();
            this.BS_GameLogNotDeal = new System.Windows.Forms.BindingSource(this.components);
            this.Wgl_Contact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Wgl_ContactID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Wgl_TransTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Wgl_GamePeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GameLocalPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Wgl_Buy_Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Wgl_Buy_Point = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gv_GameResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_GameResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_GameLogNotDeal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_GameLogNotDeal)).BeginInit();
            this.SuspendLayout();
            // 
            // gv_GameResult
            // 
            this.gv_GameResult.AllowUserToAddRows = false;
            this.gv_GameResult.AllowUserToDeleteRows = false;
            this.gv_GameResult.AllowUserToOrderColumns = true;
            this.gv_GameResult.AutoGenerateColumns = false;
            this.gv_GameResult.ColumnHeadersHeight = 25;
            this.gv_GameResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gv_GameResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Gr_GamePeriod,
            this.Gr_GameTime,
            this.Gr_GameResult,
            this.Gr_NumTotal,
            this.Gr_BigSmall,
            this.Gr_SingleDouble,
            this.Gr_DragonTiger,
            this.Gr_GamePrivatePeriod});
            this.gv_GameResult.DataSource = this.BS_GameResult;
            this.gv_GameResult.Location = new System.Drawing.Point(13, 21);
            this.gv_GameResult.MultiSelect = false;
            this.gv_GameResult.Name = "gv_GameResult";
            this.gv_GameResult.ReadOnly = true;
            this.gv_GameResult.RowHeadersVisible = false;
            this.gv_GameResult.RowTemplate.Height = 23;
            this.gv_GameResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv_GameResult.Size = new System.Drawing.Size(536, 216);
            this.gv_GameResult.TabIndex = 0;
            // 
            // Gr_GamePeriod
            // 
            this.Gr_GamePeriod.DataPropertyName = "Gr_GamePeriod";
            this.Gr_GamePeriod.HeaderText = "中心期号";
            this.Gr_GamePeriod.Name = "Gr_GamePeriod";
            this.Gr_GamePeriod.ReadOnly = true;
            this.Gr_GamePeriod.Width = 80;
            // 
            // Gr_GameTime
            // 
            this.Gr_GameTime.DataPropertyName = "Gr_GameTime";
            this.Gr_GameTime.HeaderText = "时间";
            this.Gr_GameTime.Name = "Gr_GameTime";
            this.Gr_GameTime.ReadOnly = true;
            this.Gr_GameTime.Width = 80;
            // 
            // Gr_GameResult
            // 
            this.Gr_GameResult.DataPropertyName = "Gr_GameResult";
            this.Gr_GameResult.HeaderText = "结果";
            this.Gr_GameResult.Name = "Gr_GameResult";
            this.Gr_GameResult.ReadOnly = true;
            this.Gr_GameResult.Width = 80;
            // 
            // Gr_NumTotal
            // 
            this.Gr_NumTotal.DataPropertyName = "Gr_NumTotal";
            this.Gr_NumTotal.HeaderText = "和数";
            this.Gr_NumTotal.Name = "Gr_NumTotal";
            this.Gr_NumTotal.ReadOnly = true;
            this.Gr_NumTotal.Width = 80;
            // 
            // Gr_BigSmall
            // 
            this.Gr_BigSmall.DataPropertyName = "Gr_BigSmall";
            this.Gr_BigSmall.HeaderText = "大小";
            this.Gr_BigSmall.Name = "Gr_BigSmall";
            this.Gr_BigSmall.ReadOnly = true;
            this.Gr_BigSmall.Width = 35;
            // 
            // Gr_SingleDouble
            // 
            this.Gr_SingleDouble.DataPropertyName = "Gr_SingleDouble";
            this.Gr_SingleDouble.HeaderText = "单双";
            this.Gr_SingleDouble.Name = "Gr_SingleDouble";
            this.Gr_SingleDouble.ReadOnly = true;
            this.Gr_SingleDouble.Width = 35;
            // 
            // Gr_DragonTiger
            // 
            this.Gr_DragonTiger.DataPropertyName = "Gr_DragonTiger";
            this.Gr_DragonTiger.HeaderText = "龙虎";
            this.Gr_DragonTiger.Name = "Gr_DragonTiger";
            this.Gr_DragonTiger.ReadOnly = true;
            this.Gr_DragonTiger.Width = 35;
            // 
            // Gr_GamePrivatePeriod
            // 
            this.Gr_GamePrivatePeriod.DataPropertyName = "Gr_GamePrivatePeriod";
            this.Gr_GamePrivatePeriod.HeaderText = "本地期号";
            this.Gr_GamePrivatePeriod.Name = "Gr_GamePrivatePeriod";
            this.Gr_GamePrivatePeriod.ReadOnly = true;
            this.Gr_GamePrivatePeriod.Width = 80;
            // 
            // Dtp_DownloadDate
            // 
            this.Dtp_DownloadDate.Location = new System.Drawing.Point(52, 246);
            this.Dtp_DownloadDate.Name = "Dtp_DownloadDate";
            this.Dtp_DownloadDate.Size = new System.Drawing.Size(109, 21);
            this.Dtp_DownloadDate.TabIndex = 1;
            this.Dtp_DownloadDate.ValueChanged += new System.EventHandler(this.Dtp_DownloadDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 250);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "日期:";
            // 
            // btn_download
            // 
            this.btn_download.Location = new System.Drawing.Point(168, 243);
            this.btn_download.Name = "btn_download";
            this.btn_download.Size = new System.Drawing.Size(75, 23);
            this.btn_download.TabIndex = 3;
            this.btn_download.Text = "下载";
            this.btn_download.UseVisualStyleBackColor = true;
            this.btn_download.Click += new System.EventHandler(this.btn_download_Click);
            // 
            // lbl_GameResult
            // 
            this.lbl_GameResult.AutoSize = true;
            this.lbl_GameResult.Location = new System.Drawing.Point(12, 4);
            this.lbl_GameResult.Name = "lbl_GameResult";
            this.lbl_GameResult.Size = new System.Drawing.Size(35, 12);
            this.lbl_GameResult.TabIndex = 4;
            this.lbl_GameResult.Text = "开奖:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 277);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "未兑奖:";
            // 
            // gv_GameLogNotDeal
            // 
            this.gv_GameLogNotDeal.AllowUserToAddRows = false;
            this.gv_GameLogNotDeal.AllowUserToDeleteRows = false;
            this.gv_GameLogNotDeal.AllowUserToOrderColumns = true;
            this.gv_GameLogNotDeal.AutoGenerateColumns = false;
            this.gv_GameLogNotDeal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gv_GameLogNotDeal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Wgl_Contact,
            this.Wgl_ContactID,
            this.Wgl_TransTime,
            this.Wgl_GamePeriod,
            this.GameLocalPeriod,
            this.Wgl_Buy_Value,
            this.Wgl_Buy_Point});
            this.gv_GameLogNotDeal.DataSource = this.BS_GameLogNotDeal;
            this.gv_GameLogNotDeal.Location = new System.Drawing.Point(12, 292);
            this.gv_GameLogNotDeal.MultiSelect = false;
            this.gv_GameLogNotDeal.Name = "gv_GameLogNotDeal";
            this.gv_GameLogNotDeal.ReadOnly = true;
            this.gv_GameLogNotDeal.RowTemplate.Height = 23;
            this.gv_GameLogNotDeal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv_GameLogNotDeal.Size = new System.Drawing.Size(536, 156);
            this.gv_GameLogNotDeal.TabIndex = 6;
            // 
            // Wgl_Contact
            // 
            this.Wgl_Contact.DataPropertyName = "Wgl_Contact";
            this.Wgl_Contact.HeaderText = "微信";
            this.Wgl_Contact.Name = "Wgl_Contact";
            this.Wgl_Contact.ReadOnly = true;
            this.Wgl_Contact.Width = 80;
            // 
            // Wgl_ContactID
            // 
            this.Wgl_ContactID.DataPropertyName = "Wgl_ContactID";
            this.Wgl_ContactID.HeaderText = "Wgl_ContantID";
            this.Wgl_ContactID.Name = "Wgl_ContactID";
            this.Wgl_ContactID.ReadOnly = true;
            this.Wgl_ContactID.Visible = false;
            // 
            // Wgl_TransTime
            // 
            this.Wgl_TransTime.DataPropertyName = "Wgl_TransTime";
            this.Wgl_TransTime.HeaderText = "下单时";
            this.Wgl_TransTime.Name = "Wgl_TransTime";
            this.Wgl_TransTime.ReadOnly = true;
            this.Wgl_TransTime.Width = 80;
            // 
            // Wgl_GamePeriod
            // 
            this.Wgl_GamePeriod.DataPropertyName = "Wgl_GamePeriod";
            this.Wgl_GamePeriod.HeaderText = "中心期号";
            this.Wgl_GamePeriod.Name = "Wgl_GamePeriod";
            this.Wgl_GamePeriod.ReadOnly = true;
            this.Wgl_GamePeriod.Width = 80;
            // 
            // GameLocalPeriod
            // 
            this.GameLocalPeriod.DataPropertyName = "Wgl_GamePrivatePeriod";
            this.GameLocalPeriod.HeaderText = "本地期号";
            this.GameLocalPeriod.Name = "GameLocalPeriod";
            this.GameLocalPeriod.ReadOnly = true;
            this.GameLocalPeriod.Width = 80;
            // 
            // Wgl_Buy_Value
            // 
            this.Wgl_Buy_Value.DataPropertyName = "Wgl_Buy_Value";
            this.Wgl_Buy_Value.HeaderText = "买";
            this.Wgl_Buy_Value.Name = "Wgl_Buy_Value";
            this.Wgl_Buy_Value.ReadOnly = true;
            this.Wgl_Buy_Value.Width = 30;
            // 
            // Wgl_Buy_Point
            // 
            this.Wgl_Buy_Point.DataPropertyName = "Wgl_Buy_Point";
            this.Wgl_Buy_Point.HeaderText = "多少";
            this.Wgl_Buy_Point.Name = "Wgl_Buy_Point";
            this.Wgl_Buy_Point.ReadOnly = true;
            this.Wgl_Buy_Point.Width = 60;
            // 
            // Download163AndDeal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 480);
            this.Controls.Add(this.gv_GameLogNotDeal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_GameResult);
            this.Controls.Add(this.btn_download);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Dtp_DownloadDate);
            this.Controls.Add(this.gv_GameResult);
            this.Name = "Download163AndDeal";
            this.Text = "下载开奖兑奖";
            this.Load += new System.EventHandler(this.Download163AndDeal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gv_GameResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_GameResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_GameLogNotDeal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_GameLogNotDeal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gv_GameResult;
        private System.Windows.Forms.DateTimePicker Dtp_DownloadDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_download;
        private System.Windows.Forms.Label lbl_GameResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView gv_GameLogNotDeal;
        private System.Windows.Forms.BindingSource BS_GameResult;
        private System.Windows.Forms.BindingSource BS_GameLogNotDeal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gr_GamePeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gr_GameTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gr_GameResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gr_NumTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gr_BigSmall;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gr_SingleDouble;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gr_DragonTiger;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gr_GamePrivatePeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn Wgl_Contact;
        private System.Windows.Forms.DataGridViewTextBoxColumn Wgl_ContactID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Wgl_TransTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Wgl_GamePeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn GameLocalPeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn Wgl_Buy_Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Wgl_Buy_Point;
    }
}