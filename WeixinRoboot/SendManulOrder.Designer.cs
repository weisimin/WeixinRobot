namespace WeixinRoboot
{
    partial class SendManulOrder
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
            this.fd_BuyPoint = new System.Windows.Forms.TextBox();
            this.lbl_User = new System.Windows.Forms.Label();
            this.lbl_BuyPoint = new System.Windows.Forms.Label();
            this.GV_GameLog = new System.Windows.Forms.DataGridView();
            this.BS_GameLog = new System.Windows.Forms.BindingSource(this.components);
            this.Btn_Send = new System.Windows.Forms.Button();
            this.lbl_txtformat = new System.Windows.Forms.Label();
            this.lbl_Date = new System.Windows.Forms.Label();
            this.fd_ReceiveTime = new System.Windows.Forms.DateTimePicker();
            this.dtp_StartDate = new System.Windows.Forms.DateTimePicker();
            this.dtp_EndDate = new System.Windows.Forms.DateTimePicker();
            this.lbl_FromTo = new System.Windows.Forms.Label();
            this.MS_Data = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MI_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.ReceiveTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aspnet_UserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WX_UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceiveContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GamePeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GameLocalPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Buy_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Buy_Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Buy_Point = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GameResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result_Point = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ep_sql = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.GV_GameLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_GameLog)).BeginInit();
            this.MS_Data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ep_sql)).BeginInit();
            this.SuspendLayout();
            // 
            // fd_BuyPoint
            // 
            this.fd_BuyPoint.Location = new System.Drawing.Point(82, 298);
            this.fd_BuyPoint.Name = "fd_BuyPoint";
            this.fd_BuyPoint.Size = new System.Drawing.Size(236, 21);
            this.fd_BuyPoint.TabIndex = 10;
            // 
            // lbl_User
            // 
            this.lbl_User.AutoSize = true;
            this.lbl_User.Location = new System.Drawing.Point(17, 10);
            this.lbl_User.Name = "lbl_User";
            this.lbl_User.Size = new System.Drawing.Size(35, 12);
            this.lbl_User.TabIndex = 9;
            this.lbl_User.Text = "用户:";
            // 
            // lbl_BuyPoint
            // 
            this.lbl_BuyPoint.AutoSize = true;
            this.lbl_BuyPoint.Location = new System.Drawing.Point(17, 301);
            this.lbl_BuyPoint.Name = "lbl_BuyPoint";
            this.lbl_BuyPoint.Size = new System.Drawing.Size(59, 12);
            this.lbl_BuyPoint.TabIndex = 8;
            this.lbl_BuyPoint.Text = "下注点数:";
            // 
            // GV_GameLog
            // 
            this.GV_GameLog.AllowUserToAddRows = false;
            this.GV_GameLog.AllowUserToDeleteRows = false;
            this.GV_GameLog.AllowUserToOrderColumns = true;
            this.GV_GameLog.AutoGenerateColumns = false;
            this.GV_GameLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GV_GameLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ReceiveTime,
            this.aspnet_UserID,
            this.WX_UserName,
            this.ReceiveContent,
            this.GamePeriod,
            this.GameLocalPeriod,
            this.TransTime,
            this.Buy_Type,
            this.Buy_Value,
            this.Buy_Point,
            this.GameResult,
            this.Result_Point});
            this.GV_GameLog.DataSource = this.BS_GameLog;
            this.GV_GameLog.Location = new System.Drawing.Point(17, 31);
            this.GV_GameLog.MultiSelect = false;
            this.GV_GameLog.Name = "GV_GameLog";
            this.GV_GameLog.ReadOnly = true;
            this.GV_GameLog.RowHeadersVisible = false;
            this.GV_GameLog.RowTemplate.Height = 23;
            this.GV_GameLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GV_GameLog.Size = new System.Drawing.Size(597, 208);
            this.GV_GameLog.TabIndex = 7;
            this.GV_GameLog.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GV_GameLog_CellClick);
            // 
            // Btn_Send
            // 
            this.Btn_Send.Location = new System.Drawing.Point(466, 245);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(75, 56);
            this.Btn_Send.TabIndex = 6;
            this.Btn_Send.Text = "保存";
            this.Btn_Send.UseVisualStyleBackColor = true;
            this.Btn_Send.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // lbl_txtformat
            // 
            this.lbl_txtformat.AutoSize = true;
            this.lbl_txtformat.ForeColor = System.Drawing.Color.Red;
            this.lbl_txtformat.Location = new System.Drawing.Point(15, 322);
            this.lbl_txtformat.Name = "lbl_txtformat";
            this.lbl_txtformat.Size = new System.Drawing.Size(155, 12);
            this.lbl_txtformat.TabIndex = 11;
            this.lbl_txtformat.Text = "X???或取消X????(例如龙30)";
            // 
            // lbl_Date
            // 
            this.lbl_Date.AutoSize = true;
            this.lbl_Date.Location = new System.Drawing.Point(17, 266);
            this.lbl_Date.Name = "lbl_Date";
            this.lbl_Date.Size = new System.Drawing.Size(83, 12);
            this.lbl_Date.TabIndex = 12;
            this.lbl_Date.Text = "手机接收时分:";
            // 
            // fd_ReceiveTime
            // 
            this.fd_ReceiveTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.fd_ReceiveTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fd_ReceiveTime.Location = new System.Drawing.Point(82, 261);
            this.fd_ReceiveTime.Name = "fd_ReceiveTime";
            this.fd_ReceiveTime.Size = new System.Drawing.Size(236, 21);
            this.fd_ReceiveTime.TabIndex = 13;
            // 
            // dtp_StartDate
            // 
            this.dtp_StartDate.Location = new System.Drawing.Point(385, 4);
            this.dtp_StartDate.Name = "dtp_StartDate";
            this.dtp_StartDate.Size = new System.Drawing.Size(103, 21);
            this.dtp_StartDate.TabIndex = 14;
            this.dtp_StartDate.ValueChanged += new System.EventHandler(this.dtp_StartDate_ValueChanged);
            // 
            // dtp_EndDate
            // 
            this.dtp_EndDate.Location = new System.Drawing.Point(511, 4);
            this.dtp_EndDate.Name = "dtp_EndDate";
            this.dtp_EndDate.Size = new System.Drawing.Size(103, 21);
            this.dtp_EndDate.TabIndex = 15;
            this.dtp_EndDate.ValueChanged += new System.EventHandler(this.dtp_EndDate_ValueChanged);
            // 
            // lbl_FromTo
            // 
            this.lbl_FromTo.AutoSize = true;
            this.lbl_FromTo.Location = new System.Drawing.Point(494, 10);
            this.lbl_FromTo.Name = "lbl_FromTo";
            this.lbl_FromTo.Size = new System.Drawing.Size(11, 12);
            this.lbl_FromTo.TabIndex = 16;
            this.lbl_FromTo.Text = "-";
            // 
            // MS_Data
            // 
            this.MS_Data.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_Delete});
            this.MS_Data.Name = "MS_Data";
            this.MS_Data.Size = new System.Drawing.Size(101, 26);
            // 
            // MI_Delete
            // 
            this.MI_Delete.Name = "MI_Delete";
            this.MI_Delete.Size = new System.Drawing.Size(100, 22);
            this.MI_Delete.Text = "删除";
            this.MI_Delete.Click += new System.EventHandler(this.MI_Delete_Click);
            // 
            // ReceiveTime
            // 
            this.ReceiveTime.DataPropertyName = "ReceiveTime";
            this.ReceiveTime.HeaderText = "微信时间";
            this.ReceiveTime.Name = "ReceiveTime";
            this.ReceiveTime.ReadOnly = true;
            // 
            // aspnet_UserID
            // 
            this.aspnet_UserID.DataPropertyName = "aspnet_UserID";
            this.aspnet_UserID.HeaderText = "aspnet_UserID";
            this.aspnet_UserID.Name = "aspnet_UserID";
            this.aspnet_UserID.ReadOnly = true;
            this.aspnet_UserID.Visible = false;
            // 
            // WX_UserName
            // 
            this.WX_UserName.DataPropertyName = "WX_UserName";
            this.WX_UserName.HeaderText = "WX_UserName";
            this.WX_UserName.Name = "WX_UserName";
            this.WX_UserName.ReadOnly = true;
            this.WX_UserName.Visible = false;
            // 
            // ReceiveContent
            // 
            this.ReceiveContent.DataPropertyName = "ReceiveContent";
            this.ReceiveContent.HeaderText = "微信消息";
            this.ReceiveContent.Name = "ReceiveContent";
            this.ReceiveContent.ReadOnly = true;
            // 
            // GamePeriod
            // 
            this.GamePeriod.DataPropertyName = "GamePeriod";
            this.GamePeriod.HeaderText = "中心期号";
            this.GamePeriod.Name = "GamePeriod";
            this.GamePeriod.ReadOnly = true;
            this.GamePeriod.Width = 60;
            // 
            // GameLocalPeriod
            // 
            this.GameLocalPeriod.DataPropertyName = "GameLocalPeriod";
            this.GameLocalPeriod.HeaderText = "期号";
            this.GameLocalPeriod.Name = "GameLocalPeriod";
            this.GameLocalPeriod.ReadOnly = true;
            this.GameLocalPeriod.Width = 60;
            // 
            // TransTime
            // 
            this.TransTime.DataPropertyName = "TransTime";
            this.TransTime.HeaderText = "时间";
            this.TransTime.Name = "TransTime";
            this.TransTime.ReadOnly = true;
            this.TransTime.Width = 60;
            // 
            // Buy_Type
            // 
            this.Buy_Type.DataPropertyName = "Buy_Type";
            this.Buy_Type.HeaderText = "类别";
            this.Buy_Type.Name = "Buy_Type";
            this.Buy_Type.ReadOnly = true;
            this.Buy_Type.Width = 60;
            // 
            // Buy_Value
            // 
            this.Buy_Value.DataPropertyName = "Buy_Value";
            this.Buy_Value.HeaderText = "下注";
            this.Buy_Value.Name = "Buy_Value";
            this.Buy_Value.ReadOnly = true;
            this.Buy_Value.Width = 60;
            // 
            // Buy_Point
            // 
            this.Buy_Point.DataPropertyName = "Buy_Point";
            this.Buy_Point.HeaderText = "点数";
            this.Buy_Point.Name = "Buy_Point";
            this.Buy_Point.ReadOnly = true;
            this.Buy_Point.Width = 60;
            // 
            // GameResult
            // 
            this.GameResult.DataPropertyName = "GameResult";
            this.GameResult.HeaderText = "开奖结果";
            this.GameResult.Name = "GameResult";
            this.GameResult.ReadOnly = true;
            // 
            // Result_Point
            // 
            this.Result_Point.DataPropertyName = "Result_Point";
            this.Result_Point.HeaderText = "余额变动";
            this.Result_Point.Name = "Result_Point";
            this.Result_Point.ReadOnly = true;
            // 
            // ep_sql
            // 
            this.ep_sql.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ep_sql.ContainerControl = this;
            // 
            // SendManulOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 356);
            this.Controls.Add(this.lbl_FromTo);
            this.Controls.Add(this.dtp_EndDate);
            this.Controls.Add(this.dtp_StartDate);
            this.Controls.Add(this.fd_ReceiveTime);
            this.Controls.Add(this.lbl_Date);
            this.Controls.Add(this.lbl_txtformat);
            this.Controls.Add(this.fd_BuyPoint);
            this.Controls.Add(this.lbl_User);
            this.Controls.Add(this.lbl_BuyPoint);
            this.Controls.Add(this.GV_GameLog);
            this.Controls.Add(this.Btn_Send);
            this.Name = "SendManulOrder";
            this.Text = "人工下单:";
            this.Load += new System.EventHandler(this.SendManulOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GV_GameLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_GameLog)).EndInit();
            this.MS_Data.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ep_sql)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox fd_BuyPoint;
        private System.Windows.Forms.Label lbl_User;
        private System.Windows.Forms.Label lbl_BuyPoint;
        private System.Windows.Forms.DataGridView GV_GameLog;
        private System.Windows.Forms.Button Btn_Send;
        private System.Windows.Forms.BindingSource BS_GameLog;
        private System.Windows.Forms.Label lbl_txtformat;
        private System.Windows.Forms.Label lbl_Date;
        private System.Windows.Forms.DateTimePicker fd_ReceiveTime;
        private System.Windows.Forms.DateTimePicker dtp_StartDate;
        private System.Windows.Forms.DateTimePicker dtp_EndDate;
        private System.Windows.Forms.Label lbl_FromTo;
        private System.Windows.Forms.ContextMenuStrip MS_Data;
        private System.Windows.Forms.ToolStripMenuItem MI_Delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiveTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn aspnet_UserID;
        private System.Windows.Forms.DataGridViewTextBoxColumn WX_UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiveContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn GamePeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn GameLocalPeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Buy_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Buy_Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Buy_Point;
        private System.Windows.Forms.DataGridViewTextBoxColumn GameResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result_Point;
        private System.Windows.Forms.ErrorProvider ep_sql;

    }
}