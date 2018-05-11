namespace WeixinRoboot
{
    partial class RunnerForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gv_contact = new System.Windows.Forms.DataGridView();
            this.User_ContactType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User_Contact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User_ContctID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User_ContactTMPID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User_IsReply = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.User_IsReceiveTransfer = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BS_Contact = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.gv_ReceiveReply = new System.Windows.Forms.DataGridView();
            this.Reply_Contact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reply_ContactID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reply_ContactTEMPID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reply_ReceiveContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reply_ReplyContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reply_ReceiveTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reply_ReplyTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BS_ReceiveReply = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BS_GameLog = new System.Windows.Forms.BindingSource(this.components);
            this.TM_Refresh = new System.Windows.Forms.Timer(this.components);
            this.MouseMenuReply = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MI_IsReply = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_FasongXinxi = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_ChongZhi = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_FanXian = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_OrderManual = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_CleanUp = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_CancelIsReply = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_ReceiveTrans = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_CancelReceiveTrans = new System.Windows.Forms.ToolStripMenuItem();
            this.tb_ContactFilter = new System.Windows.Forms.TextBox();
            this.dtp_EndDate = new System.Windows.Forms.DateTimePicker();
            this.dtp_StartDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.GV_GameLog = new System.Windows.Forms.DataGridView();
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
            this.fd_day = new System.Windows.Forms.DateTimePicker();
            this.fd_Num5 = new System.Windows.Forms.TextBox();
            this.fd_Num4 = new System.Windows.Forms.TextBox();
            this.fd_Num3 = new System.Windows.Forms.TextBox();
            this.fd_Num2 = new System.Windows.Forms.TextBox();
            this.BtnSaveAndDeal = new System.Windows.Forms.Button();
            this.fd_Num1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.fd_Period = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gv_contact)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_Contact)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_ReceiveReply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_ReceiveReply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_GameLog)).BeginInit();
            this.MouseMenuReply.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GV_GameLog)).BeginInit();
            this.SuspendLayout();
            // 
            // gv_contact
            // 
            this.gv_contact.AllowUserToAddRows = false;
            this.gv_contact.AllowUserToDeleteRows = false;
            this.gv_contact.AllowUserToOrderColumns = true;
            this.gv_contact.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gv_contact.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gv_contact.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gv_contact.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.User_ContactType,
            this.User_Contact,
            this.User_ContctID,
            this.User_ContactTMPID,
            this.User_IsReply,
            this.User_IsReceiveTransfer});
            this.gv_contact.DataSource = this.BS_Contact;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gv_contact.DefaultCellStyle = dataGridViewCellStyle2;
            this.gv_contact.Location = new System.Drawing.Point(12, 39);
            this.gv_contact.MultiSelect = false;
            this.gv_contact.Name = "gv_contact";
            this.gv_contact.ReadOnly = true;
            this.gv_contact.RowHeadersVisible = false;
            this.gv_contact.RowTemplate.Height = 23;
            this.gv_contact.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gv_contact.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv_contact.Size = new System.Drawing.Size(189, 611);
            this.gv_contact.TabIndex = 0;
            this.gv_contact.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gv_contact_CellClick);
            this.gv_contact.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gv_contact_DataError);
            this.gv_contact.SelectionChanged += new System.EventHandler(this.gv_contact_SelectionChanged);
            this.gv_contact.Leave += new System.EventHandler(this.gv_contact_Leave);
            // 
            // User_ContactType
            // 
            this.User_ContactType.DataPropertyName = "User_ContactType";
            this.User_ContactType.HeaderText = "类";
            this.User_ContactType.Name = "User_ContactType";
            this.User_ContactType.ReadOnly = true;
            this.User_ContactType.Width = 25;
            // 
            // User_Contact
            // 
            this.User_Contact.DataPropertyName = "User_Contact";
            this.User_Contact.HeaderText = "联系人";
            this.User_Contact.Name = "User_Contact";
            this.User_Contact.ReadOnly = true;
            this.User_Contact.Width = 90;
            // 
            // User_ContctID
            // 
            this.User_ContctID.DataPropertyName = "User_ContctID";
            this.User_ContctID.HeaderText = "联系人ID";
            this.User_ContctID.Name = "User_ContctID";
            this.User_ContctID.ReadOnly = true;
            this.User_ContctID.Visible = false;
            // 
            // User_ContactTMPID
            // 
            this.User_ContactTMPID.HeaderText = "联系人临时ID";
            this.User_ContactTMPID.Name = "User_ContactTMPID";
            this.User_ContactTMPID.ReadOnly = true;
            this.User_ContactTMPID.Visible = false;
            // 
            // User_IsReply
            // 
            this.User_IsReply.DataPropertyName = "User_IsReply";
            this.User_IsReply.HeaderText = "启";
            this.User_IsReply.Name = "User_IsReply";
            this.User_IsReply.ReadOnly = true;
            this.User_IsReply.Width = 30;
            // 
            // User_IsReceiveTransfer
            // 
            this.User_IsReceiveTransfer.DataPropertyName = "User_IsReceiveTransfer";
            this.User_IsReceiveTransfer.HeaderText = "转";
            this.User_IsReceiveTransfer.Name = "User_IsReceiveTransfer";
            this.User_IsReceiveTransfer.ReadOnly = true;
            this.User_IsReceiveTransfer.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.User_IsReceiveTransfer.Width = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "联系人";
            // 
            // gv_ReceiveReply
            // 
            this.gv_ReceiveReply.AllowUserToAddRows = false;
            this.gv_ReceiveReply.AllowUserToDeleteRows = false;
            this.gv_ReceiveReply.AutoGenerateColumns = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gv_ReceiveReply.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gv_ReceiveReply.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_ReceiveReply.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Reply_Contact,
            this.Reply_ContactID,
            this.Reply_ContactTEMPID,
            this.Reply_ReceiveContent,
            this.Reply_ReplyContent,
            this.Reply_ReceiveTime,
            this.Reply_ReplyTime});
            this.gv_ReceiveReply.DataSource = this.BS_ReceiveReply;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gv_ReceiveReply.DefaultCellStyle = dataGridViewCellStyle4;
            this.gv_ReceiveReply.Location = new System.Drawing.Point(209, 166);
            this.gv_ReceiveReply.Name = "gv_ReceiveReply";
            this.gv_ReceiveReply.ReadOnly = true;
            this.gv_ReceiveReply.RowHeadersVisible = false;
            this.gv_ReceiveReply.RowTemplate.Height = 23;
            this.gv_ReceiveReply.Size = new System.Drawing.Size(767, 235);
            this.gv_ReceiveReply.TabIndex = 2;
            this.gv_ReceiveReply.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gv_ReceiveReply_DataError);
            // 
            // Reply_Contact
            // 
            this.Reply_Contact.DataPropertyName = "Reply_Contact";
            this.Reply_Contact.Frozen = true;
            this.Reply_Contact.HeaderText = "联系人";
            this.Reply_Contact.Name = "Reply_Contact";
            this.Reply_Contact.ReadOnly = true;
            // 
            // Reply_ContactID
            // 
            this.Reply_ContactID.DataPropertyName = "Reply_ContactID";
            this.Reply_ContactID.HeaderText = "联系人ID";
            this.Reply_ContactID.Name = "Reply_ContactID";
            this.Reply_ContactID.ReadOnly = true;
            this.Reply_ContactID.Visible = false;
            // 
            // Reply_ContactTEMPID
            // 
            this.Reply_ContactTEMPID.DataPropertyName = "Reply_ContactTEMPID";
            this.Reply_ContactTEMPID.HeaderText = "联系人临时ID";
            this.Reply_ContactTEMPID.Name = "Reply_ContactTEMPID";
            this.Reply_ContactTEMPID.ReadOnly = true;
            this.Reply_ContactTEMPID.Visible = false;
            // 
            // Reply_ReceiveContent
            // 
            this.Reply_ReceiveContent.DataPropertyName = "Reply_ReceiveContent";
            this.Reply_ReceiveContent.HeaderText = "收到";
            this.Reply_ReceiveContent.Name = "Reply_ReceiveContent";
            this.Reply_ReceiveContent.ReadOnly = true;
            this.Reply_ReceiveContent.Width = 300;
            // 
            // Reply_ReplyContent
            // 
            this.Reply_ReplyContent.DataPropertyName = "Reply_ReplyContent";
            this.Reply_ReplyContent.HeaderText = "回复";
            this.Reply_ReplyContent.Name = "Reply_ReplyContent";
            this.Reply_ReplyContent.ReadOnly = true;
            this.Reply_ReplyContent.Width = 300;
            // 
            // Reply_ReceiveTime
            // 
            this.Reply_ReceiveTime.DataPropertyName = "Reply_ReceiveTime";
            this.Reply_ReceiveTime.HeaderText = "接收时间";
            this.Reply_ReceiveTime.Name = "Reply_ReceiveTime";
            this.Reply_ReceiveTime.ReadOnly = true;
            // 
            // Reply_ReplyTime
            // 
            this.Reply_ReplyTime.DataPropertyName = "Reply_ReplyTime";
            this.Reply_ReplyTime.HeaderText = "回复时间";
            this.Reply_ReplyTime.Name = "Reply_ReplyTime";
            this.Reply_ReplyTime.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "收到/自动回复";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(207, 417);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "下单/开奖";
            // 
            // TM_Refresh
            // 
            this.TM_Refresh.Interval = 500;
            this.TM_Refresh.Tick += new System.EventHandler(this.TM_Refresh_Tick);
            // 
            // MouseMenuReply
            // 
            this.MouseMenuReply.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_IsReply,
            this.MI_FasongXinxi,
            this.MI_ChongZhi,
            this.MI_FanXian,
            this.MI_OrderManual,
            this.MI_CleanUp,
            this.MI_CancelIsReply,
            this.MI_ReceiveTrans,
            this.MI_CancelReceiveTrans});
            this.MouseMenuReply.Name = "MouseMenuReply";
            this.MouseMenuReply.Size = new System.Drawing.Size(149, 202);
            // 
            // MI_IsReply
            // 
            this.MI_IsReply.Name = "MI_IsReply";
            this.MI_IsReply.Size = new System.Drawing.Size(148, 22);
            this.MI_IsReply.Text = "自动跟踪";
            this.MI_IsReply.Click += new System.EventHandler(this.MI_IsReply_Click);
            // 
            // MI_FasongXinxi
            // 
            this.MI_FasongXinxi.Name = "MI_FasongXinxi";
            this.MI_FasongXinxi.Size = new System.Drawing.Size(148, 22);
            this.MI_FasongXinxi.Text = "发送消息";
            this.MI_FasongXinxi.Click += new System.EventHandler(this.MI_FasongXinxi_Click);
            // 
            // MI_ChongZhi
            // 
            this.MI_ChongZhi.Name = "MI_ChongZhi";
            this.MI_ChongZhi.Size = new System.Drawing.Size(148, 22);
            this.MI_ChongZhi.Text = "充值";
            this.MI_ChongZhi.Click += new System.EventHandler(this.MI_ChongZhi_Click);
            // 
            // MI_FanXian
            // 
            this.MI_FanXian.Name = "MI_FanXian";
            this.MI_FanXian.Size = new System.Drawing.Size(148, 22);
            this.MI_FanXian.Text = "返现";
            // 
            // MI_OrderManual
            // 
            this.MI_OrderManual.Name = "MI_OrderManual";
            this.MI_OrderManual.Size = new System.Drawing.Size(148, 22);
            this.MI_OrderManual.Text = "人工下单";
            this.MI_OrderManual.Click += new System.EventHandler(this.MI_OrderManual_Click);
            // 
            // MI_CleanUp
            // 
            this.MI_CleanUp.Name = "MI_CleanUp";
            this.MI_CleanUp.Size = new System.Drawing.Size(148, 22);
            this.MI_CleanUp.Text = "清算";
            this.MI_CleanUp.Click += new System.EventHandler(this.MI_CleanUp_Click);
            // 
            // MI_CancelIsReply
            // 
            this.MI_CancelIsReply.Name = "MI_CancelIsReply";
            this.MI_CancelIsReply.Size = new System.Drawing.Size(148, 22);
            this.MI_CancelIsReply.Text = "取消自动跟踪";
            this.MI_CancelIsReply.Click += new System.EventHandler(this.MI_CancelIsReply_Click);
            // 
            // MI_ReceiveTrans
            // 
            this.MI_ReceiveTrans.Name = "MI_ReceiveTrans";
            this.MI_ReceiveTrans.Size = new System.Drawing.Size(148, 22);
            this.MI_ReceiveTrans.Text = "转发";
            this.MI_ReceiveTrans.Click += new System.EventHandler(this.MI_ReceiveTrans_Click);
            // 
            // MI_CancelReceiveTrans
            // 
            this.MI_CancelReceiveTrans.Name = "MI_CancelReceiveTrans";
            this.MI_CancelReceiveTrans.Size = new System.Drawing.Size(148, 22);
            this.MI_CancelReceiveTrans.Text = "取消转发";
            this.MI_CancelReceiveTrans.Click += new System.EventHandler(this.MI_CancelReceiveTrans_Click);
            // 
            // tb_ContactFilter
            // 
            this.tb_ContactFilter.Location = new System.Drawing.Point(60, 12);
            this.tb_ContactFilter.Name = "tb_ContactFilter";
            this.tb_ContactFilter.Size = new System.Drawing.Size(100, 21);
            this.tb_ContactFilter.TabIndex = 6;
            this.tb_ContactFilter.TextChanged += new System.EventHandler(this.tb_ContactFilter_TextChanged);
            // 
            // dtp_EndDate
            // 
            this.dtp_EndDate.Location = new System.Drawing.Point(863, 121);
            this.dtp_EndDate.Name = "dtp_EndDate";
            this.dtp_EndDate.Size = new System.Drawing.Size(113, 21);
            this.dtp_EndDate.TabIndex = 7;
            this.dtp_EndDate.ValueChanged += new System.EventHandler(this.dtp_End_ValueChanged);
            // 
            // dtp_StartDate
            // 
            this.dtp_StartDate.Location = new System.Drawing.Point(726, 121);
            this.dtp_StartDate.Name = "dtp_StartDate";
            this.dtp_StartDate.Size = new System.Drawing.Size(113, 21);
            this.dtp_StartDate.TabIndex = 8;
            this.dtp_StartDate.ValueChanged += new System.EventHandler(this.dtp_Start_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(845, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "-";
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
            this.GV_GameLog.Location = new System.Drawing.Point(207, 442);
            this.GV_GameLog.MultiSelect = false;
            this.GV_GameLog.Name = "GV_GameLog";
            this.GV_GameLog.ReadOnly = true;
            this.GV_GameLog.RowHeadersVisible = false;
            this.GV_GameLog.RowTemplate.Height = 23;
            this.GV_GameLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GV_GameLog.Size = new System.Drawing.Size(767, 208);
            this.GV_GameLog.TabIndex = 10;
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
            // fd_day
            // 
            this.fd_day.CustomFormat = "yyyy-MM-dd";
            this.fd_day.Location = new System.Drawing.Point(214, 39);
            this.fd_day.Name = "fd_day";
            this.fd_day.Size = new System.Drawing.Size(101, 21);
            this.fd_day.TabIndex = 26;
            // 
            // fd_Num5
            // 
            this.fd_Num5.Location = new System.Drawing.Point(419, 68);
            this.fd_Num5.Name = "fd_Num5";
            this.fd_Num5.Size = new System.Drawing.Size(26, 21);
            this.fd_Num5.TabIndex = 25;
            // 
            // fd_Num4
            // 
            this.fd_Num4.Location = new System.Drawing.Point(384, 68);
            this.fd_Num4.Name = "fd_Num4";
            this.fd_Num4.Size = new System.Drawing.Size(26, 21);
            this.fd_Num4.TabIndex = 24;
            // 
            // fd_Num3
            // 
            this.fd_Num3.Location = new System.Drawing.Point(349, 68);
            this.fd_Num3.Name = "fd_Num3";
            this.fd_Num3.Size = new System.Drawing.Size(26, 21);
            this.fd_Num3.TabIndex = 23;
            // 
            // fd_Num2
            // 
            this.fd_Num2.Location = new System.Drawing.Point(314, 68);
            this.fd_Num2.Name = "fd_Num2";
            this.fd_Num2.Size = new System.Drawing.Size(26, 21);
            this.fd_Num2.TabIndex = 22;
            // 
            // BtnSaveAndDeal
            // 
            this.BtnSaveAndDeal.Location = new System.Drawing.Point(488, 39);
            this.BtnSaveAndDeal.Name = "BtnSaveAndDeal";
            this.BtnSaveAndDeal.Size = new System.Drawing.Size(76, 44);
            this.BtnSaveAndDeal.TabIndex = 21;
            this.BtnSaveAndDeal.Text = "保存并开奖";
            this.BtnSaveAndDeal.UseVisualStyleBackColor = true;
            this.BtnSaveAndDeal.Click += new System.EventHandler(this.BtnSaveAndDeal_Click);
            // 
            // fd_Num1
            // 
            this.fd_Num1.Location = new System.Drawing.Point(279, 68);
            this.fd_Num1.Name = "fd_Num1";
            this.fd_Num1.Size = new System.Drawing.Size(26, 21);
            this.fd_Num1.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(214, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "开奖结果:";
            // 
            // fd_Period
            // 
            this.fd_Period.Location = new System.Drawing.Point(382, 39);
            this.fd_Period.Name = "fd_Period";
            this.fd_Period.Size = new System.Drawing.Size(33, 21);
            this.fd_Period.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(322, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "3位期号:";
            // 
            // RunnerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 662);
            this.Controls.Add(this.fd_day);
            this.Controls.Add(this.fd_Num5);
            this.Controls.Add(this.fd_Num4);
            this.Controls.Add(this.fd_Num3);
            this.Controls.Add(this.fd_Num2);
            this.Controls.Add(this.BtnSaveAndDeal);
            this.Controls.Add(this.fd_Num1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.fd_Period);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.GV_GameLog);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtp_StartDate);
            this.Controls.Add(this.dtp_EndDate);
            this.Controls.Add(this.tb_ContactFilter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gv_ReceiveReply);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gv_contact);
            this.Name = "RunnerForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "微信机器人";
            this.Load += new System.EventHandler(this.Runner_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gv_contact)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_Contact)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_ReceiveReply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_ReceiveReply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_GameLog)).EndInit();
            this.MouseMenuReply.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GV_GameLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gv_contact;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gv_ReceiveReply;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer TM_Refresh;
        private System.Windows.Forms.BindingSource BS_Contact;
        private System.Windows.Forms.ContextMenuStrip MouseMenuReply;
        private System.Windows.Forms.ToolStripMenuItem MI_FasongXinxi;
        private System.Windows.Forms.BindingSource BS_ReceiveReply;
        private System.Windows.Forms.TextBox tb_ContactFilter;
        private System.Windows.Forms.ToolStripMenuItem MI_ChongZhi;
        private System.Windows.Forms.ToolStripMenuItem MI_FanXian;
        private System.Windows.Forms.DateTimePicker dtp_EndDate;
        private System.Windows.Forms.DateTimePicker dtp_StartDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reply_Contact;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reply_ContactID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reply_ContactTEMPID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reply_ReceiveContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reply_ReplyContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reply_ReceiveTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reply_ReplyTime;
        private System.Windows.Forms.BindingSource BS_GameLog;
        private System.Windows.Forms.ToolStripMenuItem MI_OrderManual;
        private System.Windows.Forms.ToolStripMenuItem MI_CleanUp;
        private System.Windows.Forms.DataGridView GV_GameLog;
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
        private System.Windows.Forms.ToolStripMenuItem MI_IsReply;
        private System.Windows.Forms.ToolStripMenuItem MI_CancelIsReply;
        private System.Windows.Forms.DataGridViewTextBoxColumn User_ContactType;
        private System.Windows.Forms.DataGridViewTextBoxColumn User_Contact;
        private System.Windows.Forms.DataGridViewTextBoxColumn User_ContctID;
        private System.Windows.Forms.DataGridViewTextBoxColumn User_ContactTMPID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn User_IsReply;
        private System.Windows.Forms.DataGridViewCheckBoxColumn User_IsReceiveTransfer;
        private System.Windows.Forms.ToolStripMenuItem MI_ReceiveTrans;
        private System.Windows.Forms.ToolStripMenuItem MI_CancelReceiveTrans;
        private System.Windows.Forms.DateTimePicker fd_day;
        private System.Windows.Forms.TextBox fd_Num5;
        private System.Windows.Forms.TextBox fd_Num4;
        private System.Windows.Forms.TextBox fd_Num3;
        private System.Windows.Forms.TextBox fd_Num2;
        private System.Windows.Forms.Button BtnSaveAndDeal;
        private System.Windows.Forms.TextBox fd_Num1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox fd_Period;
        private System.Windows.Forms.Label label6;
    }
}