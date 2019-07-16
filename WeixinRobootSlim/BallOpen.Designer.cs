namespace WeixinRobootSlim
{
    partial class BallOpen
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
            this.gv_balls = new System.Windows.Forms.DataGridView();
            this.GameID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GameVS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gv_result = new System.Windows.Forms.DataGridView();
            this.gv_playersbuys = new System.Windows.Forms.DataGridView();
            this.WX_GameIDUnOpen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WX_UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyRatio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A_WIN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Winless = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.B_Win = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BigWin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SmallWin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_A_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_A_SAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_A_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_SAME_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_SAME_SAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_SAME_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_B_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_B_SAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_B_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R1_0_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R2_0_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R2_1_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R3_0_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R3_1_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R3_2_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R4_0_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R4_1_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R4_2_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R4_3_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R0_0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R1_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R2_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R3_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R4_4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rother = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R1_0_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R2_0_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R2_1_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R3_0_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R3_1_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R3_2_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R4_0_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R4_1_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R4_2_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R4_3_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_open = new System.Windows.Forms.Button();
            this.lbl_gamematch = new System.Windows.Forms.Label();
            this.lbl_result = new System.Windows.Forms.Label();
            this.cb_wxsourcetype = new System.Windows.Forms.ComboBox();
            this.OpenGameID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TeamA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TeamB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gv_balls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_result)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_playersbuys)).BeginInit();
            this.SuspendLayout();
            // 
            // gv_balls
            // 
            this.gv_balls.AllowUserToAddRows = false;
            this.gv_balls.AllowUserToDeleteRows = false;
            this.gv_balls.AllowUserToOrderColumns = true;
            this.gv_balls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_balls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GameID,
            this.GameVS});
            this.gv_balls.Location = new System.Drawing.Point(12, 49);
            this.gv_balls.Name = "gv_balls";
            this.gv_balls.ReadOnly = true;
            this.gv_balls.RowHeadersWidth = 10;
            this.gv_balls.RowTemplate.Height = 23;
            this.gv_balls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv_balls.Size = new System.Drawing.Size(599, 131);
            this.gv_balls.TabIndex = 0;
            this.gv_balls.SelectionChanged += new System.EventHandler(this.GV_BallUnOpen_SelectionChanged);
            // 
            // GameID
            // 
            this.GameID.DataPropertyName = "GameID";
            this.GameID.HeaderText = "GameID";
            this.GameID.Name = "GameID";
            this.GameID.ReadOnly = true;
            this.GameID.Visible = false;
            // 
            // GameVS
            // 
            this.GameVS.DataPropertyName = "GameVS";
            this.GameVS.HeaderText = "比赛";
            this.GameVS.Name = "GameVS";
            this.GameVS.ReadOnly = true;
            this.GameVS.Width = 200;
            // 
            // gv_result
            // 
            this.gv_result.AllowUserToAddRows = false;
            this.gv_result.AllowUserToDeleteRows = false;
            this.gv_result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_result.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OpenGameID,
            this.TimeType,
            this.TeamA,
            this.TeamB});
            this.gv_result.Location = new System.Drawing.Point(617, 49);
            this.gv_result.Name = "gv_result";
            this.gv_result.RowHeadersWidth = 10;
            this.gv_result.RowTemplate.Height = 23;
            this.gv_result.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.gv_result.Size = new System.Drawing.Size(283, 132);
            this.gv_result.TabIndex = 1;
            // 
            // gv_playersbuys
            // 
            this.gv_playersbuys.AllowUserToAddRows = false;
            this.gv_playersbuys.AllowUserToDeleteRows = false;
            this.gv_playersbuys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_playersbuys.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WX_GameIDUnOpen,
            this.WX_UserName,
            this.transtime,
            this.BuyMoney,
            this.BuyRatio,
            this.BuyType,
            this.A_WIN,
            this.Winless,
            this.B_Win,
            this.BigWin,
            this.Total,
            this.SmallWin,
            this.R_A_A,
            this.R_A_SAME,
            this.R_A_B,
            this.R_SAME_A,
            this.R_SAME_SAME,
            this.R_SAME_B,
            this.R_B_A,
            this.R_B_SAME,
            this.R_B_B,
            this.R1_0_A,
            this.R2_0_A,
            this.R2_1_A,
            this.R3_0_A,
            this.R3_1_A,
            this.R3_2_A,
            this.R4_0_A,
            this.R4_1_A,
            this.R4_2_A,
            this.R4_3_A,
            this.R0_0,
            this.R1_1,
            this.R2_2,
            this.R3_3,
            this.R4_4,
            this.Rother,
            this.R1_0_B,
            this.R2_0_B,
            this.R2_1_B,
            this.R3_0_B,
            this.R3_1_B,
            this.R3_2_B,
            this.R4_0_B,
            this.R4_1_B,
            this.R4_2_B,
            this.R4_3_B});
            this.gv_playersbuys.Location = new System.Drawing.Point(13, 201);
            this.gv_playersbuys.Name = "gv_playersbuys";
            this.gv_playersbuys.ReadOnly = true;
            this.gv_playersbuys.RowHeadersWidth = 10;
            this.gv_playersbuys.RowTemplate.Height = 23;
            this.gv_playersbuys.Size = new System.Drawing.Size(887, 216);
            this.gv_playersbuys.TabIndex = 2;
            // 
            // WX_GameIDUnOpen
            // 
            this.WX_GameIDUnOpen.DataPropertyName = "GameID";
            this.WX_GameIDUnOpen.HeaderText = "GameID";
            this.WX_GameIDUnOpen.Name = "WX_GameIDUnOpen";
            this.WX_GameIDUnOpen.ReadOnly = true;
            this.WX_GameIDUnOpen.Visible = false;
            // 
            // WX_UserName
            // 
            this.WX_UserName.DataPropertyName = "WX_UserNameOrRemark";
            this.WX_UserName.HeaderText = "玩家";
            this.WX_UserName.Name = "WX_UserName";
            this.WX_UserName.ReadOnly = true;
            this.WX_UserName.Width = 200;
            // 
            // transtime
            // 
            this.transtime.DataPropertyName = "transtime";
            this.transtime.HeaderText = "时间";
            this.transtime.Name = "transtime";
            this.transtime.ReadOnly = true;
            this.transtime.Width = 150;
            // 
            // BuyMoney
            // 
            this.BuyMoney.DataPropertyName = "BuyMoney";
            this.BuyMoney.HeaderText = "金额";
            this.BuyMoney.Name = "BuyMoney";
            this.BuyMoney.ReadOnly = true;
            // 
            // BuyRatio
            // 
            this.BuyRatio.DataPropertyName = "BuyRatio";
            this.BuyRatio.HeaderText = "赔率";
            this.BuyRatio.Name = "BuyRatio";
            this.BuyRatio.ReadOnly = true;
            // 
            // BuyType
            // 
            this.BuyType.DataPropertyName = "BuyType";
            this.BuyType.HeaderText = "下注";
            this.BuyType.Name = "BuyType";
            this.BuyType.ReadOnly = true;
            // 
            // A_WIN
            // 
            this.A_WIN.DataPropertyName = "A_WIN";
            this.A_WIN.HeaderText = "主";
            this.A_WIN.Name = "A_WIN";
            this.A_WIN.ReadOnly = true;
            // 
            // Winless
            // 
            this.Winless.DataPropertyName = "Winless";
            this.Winless.HeaderText = "让球";
            this.Winless.Name = "Winless";
            this.Winless.ReadOnly = true;
            // 
            // B_Win
            // 
            this.B_Win.DataPropertyName = "B_Win";
            this.B_Win.HeaderText = "客";
            this.B_Win.Name = "B_Win";
            this.B_Win.ReadOnly = true;
            // 
            // BigWin
            // 
            this.BigWin.DataPropertyName = "BigWin";
            this.BigWin.HeaderText = "大球";
            this.BigWin.Name = "BigWin";
            this.BigWin.ReadOnly = true;
            // 
            // Total
            // 
            this.Total.DataPropertyName = "Total";
            this.Total.HeaderText = "总球";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            // 
            // SmallWin
            // 
            this.SmallWin.DataPropertyName = "SmallWin";
            this.SmallWin.HeaderText = "小球";
            this.SmallWin.Name = "SmallWin";
            this.SmallWin.ReadOnly = true;
            // 
            // R_A_A
            // 
            this.R_A_A.DataPropertyName = "R_A_A";
            this.R_A_A.HeaderText = "主/主";
            this.R_A_A.Name = "R_A_A";
            this.R_A_A.ReadOnly = true;
            // 
            // R_A_SAME
            // 
            this.R_A_SAME.DataPropertyName = "R_A_SAME";
            this.R_A_SAME.HeaderText = "主/和";
            this.R_A_SAME.Name = "R_A_SAME";
            this.R_A_SAME.ReadOnly = true;
            // 
            // R_A_B
            // 
            this.R_A_B.DataPropertyName = "R_A_B";
            this.R_A_B.HeaderText = "主/客";
            this.R_A_B.Name = "R_A_B";
            this.R_A_B.ReadOnly = true;
            // 
            // R_SAME_A
            // 
            this.R_SAME_A.DataPropertyName = "R_SAME_A";
            this.R_SAME_A.HeaderText = "和/主";
            this.R_SAME_A.Name = "R_SAME_A";
            this.R_SAME_A.ReadOnly = true;
            // 
            // R_SAME_SAME
            // 
            this.R_SAME_SAME.DataPropertyName = "R_SAME_SAME";
            this.R_SAME_SAME.HeaderText = "和/和";
            this.R_SAME_SAME.Name = "R_SAME_SAME";
            this.R_SAME_SAME.ReadOnly = true;
            // 
            // R_SAME_B
            // 
            this.R_SAME_B.DataPropertyName = "R_SAME_B";
            this.R_SAME_B.HeaderText = "和/客";
            this.R_SAME_B.Name = "R_SAME_B";
            this.R_SAME_B.ReadOnly = true;
            // 
            // R_B_A
            // 
            this.R_B_A.DataPropertyName = "R_B_A";
            this.R_B_A.HeaderText = "客/主";
            this.R_B_A.Name = "R_B_A";
            this.R_B_A.ReadOnly = true;
            // 
            // R_B_SAME
            // 
            this.R_B_SAME.DataPropertyName = "R_B_SAME";
            this.R_B_SAME.HeaderText = "客/和";
            this.R_B_SAME.Name = "R_B_SAME";
            this.R_B_SAME.ReadOnly = true;
            // 
            // R_B_B
            // 
            this.R_B_B.DataPropertyName = "R_B_B";
            this.R_B_B.HeaderText = "客/客";
            this.R_B_B.Name = "R_B_B";
            this.R_B_B.ReadOnly = true;
            // 
            // R1_0_A
            // 
            this.R1_0_A.DataPropertyName = "R1_0_A";
            this.R1_0_A.HeaderText = "1-0主";
            this.R1_0_A.Name = "R1_0_A";
            this.R1_0_A.ReadOnly = true;
            // 
            // R2_0_A
            // 
            this.R2_0_A.DataPropertyName = "R2_0_A";
            this.R2_0_A.HeaderText = "2-0主";
            this.R2_0_A.Name = "R2_0_A";
            this.R2_0_A.ReadOnly = true;
            // 
            // R2_1_A
            // 
            this.R2_1_A.DataPropertyName = "R2_1_A";
            this.R2_1_A.HeaderText = "2-1主";
            this.R2_1_A.Name = "R2_1_A";
            this.R2_1_A.ReadOnly = true;
            // 
            // R3_0_A
            // 
            this.R3_0_A.DataPropertyName = "R3_0_A";
            this.R3_0_A.HeaderText = "3-0主";
            this.R3_0_A.Name = "R3_0_A";
            this.R3_0_A.ReadOnly = true;
            // 
            // R3_1_A
            // 
            this.R3_1_A.DataPropertyName = "R3_1_A";
            this.R3_1_A.HeaderText = "3-1主";
            this.R3_1_A.Name = "R3_1_A";
            this.R3_1_A.ReadOnly = true;
            // 
            // R3_2_A
            // 
            this.R3_2_A.DataPropertyName = "R3_2_A";
            this.R3_2_A.HeaderText = "3-2主";
            this.R3_2_A.Name = "R3_2_A";
            this.R3_2_A.ReadOnly = true;
            // 
            // R4_0_A
            // 
            this.R4_0_A.DataPropertyName = "R4_0_A";
            this.R4_0_A.HeaderText = "4-0主";
            this.R4_0_A.Name = "R4_0_A";
            this.R4_0_A.ReadOnly = true;
            // 
            // R4_1_A
            // 
            this.R4_1_A.DataPropertyName = "R4_1_A";
            this.R4_1_A.HeaderText = "4-1主";
            this.R4_1_A.Name = "R4_1_A";
            this.R4_1_A.ReadOnly = true;
            // 
            // R4_2_A
            // 
            this.R4_2_A.DataPropertyName = "R4_2_A";
            this.R4_2_A.HeaderText = "4-2主";
            this.R4_2_A.Name = "R4_2_A";
            this.R4_2_A.ReadOnly = true;
            // 
            // R4_3_A
            // 
            this.R4_3_A.DataPropertyName = "R4_3_A";
            this.R4_3_A.HeaderText = "4-3主";
            this.R4_3_A.Name = "R4_3_A";
            this.R4_3_A.ReadOnly = true;
            // 
            // R0_0
            // 
            this.R0_0.DataPropertyName = "R0_0";
            this.R0_0.HeaderText = "0-0";
            this.R0_0.Name = "R0_0";
            this.R0_0.ReadOnly = true;
            // 
            // R1_1
            // 
            this.R1_1.DataPropertyName = "R1_1";
            this.R1_1.HeaderText = "1-1";
            this.R1_1.Name = "R1_1";
            this.R1_1.ReadOnly = true;
            // 
            // R2_2
            // 
            this.R2_2.DataPropertyName = "R2_2";
            this.R2_2.HeaderText = "2-2";
            this.R2_2.Name = "R2_2";
            this.R2_2.ReadOnly = true;
            // 
            // R3_3
            // 
            this.R3_3.DataPropertyName = "R3_3";
            this.R3_3.HeaderText = "3-3";
            this.R3_3.Name = "R3_3";
            this.R3_3.ReadOnly = true;
            // 
            // R4_4
            // 
            this.R4_4.DataPropertyName = "R4_4";
            this.R4_4.HeaderText = "4-4";
            this.R4_4.Name = "R4_4";
            this.R4_4.ReadOnly = true;
            // 
            // Rother
            // 
            this.Rother.DataPropertyName = "Rother";
            this.Rother.HeaderText = "其他";
            this.Rother.Name = "Rother";
            this.Rother.ReadOnly = true;
            // 
            // R1_0_B
            // 
            this.R1_0_B.DataPropertyName = "R1_0_B";
            this.R1_0_B.HeaderText = "1-0客";
            this.R1_0_B.Name = "R1_0_B";
            this.R1_0_B.ReadOnly = true;
            // 
            // R2_0_B
            // 
            this.R2_0_B.DataPropertyName = "R2_0_B";
            this.R2_0_B.HeaderText = "2-0客";
            this.R2_0_B.Name = "R2_0_B";
            this.R2_0_B.ReadOnly = true;
            // 
            // R2_1_B
            // 
            this.R2_1_B.DataPropertyName = "R2_1_B";
            this.R2_1_B.HeaderText = "2-1客";
            this.R2_1_B.Name = "R2_1_B";
            this.R2_1_B.ReadOnly = true;
            // 
            // R3_0_B
            // 
            this.R3_0_B.DataPropertyName = "R3_0_B";
            this.R3_0_B.HeaderText = "3-0客";
            this.R3_0_B.Name = "R3_0_B";
            this.R3_0_B.ReadOnly = true;
            // 
            // R3_1_B
            // 
            this.R3_1_B.DataPropertyName = "R3_1_B";
            this.R3_1_B.HeaderText = "3-1客";
            this.R3_1_B.Name = "R3_1_B";
            this.R3_1_B.ReadOnly = true;
            // 
            // R3_2_B
            // 
            this.R3_2_B.DataPropertyName = "R3_2_B";
            this.R3_2_B.HeaderText = "3-2客";
            this.R3_2_B.Name = "R3_2_B";
            this.R3_2_B.ReadOnly = true;
            // 
            // R4_0_B
            // 
            this.R4_0_B.DataPropertyName = "R4_0_B";
            this.R4_0_B.HeaderText = "4-0客";
            this.R4_0_B.Name = "R4_0_B";
            this.R4_0_B.ReadOnly = true;
            // 
            // R4_1_B
            // 
            this.R4_1_B.DataPropertyName = "R4_1_B";
            this.R4_1_B.HeaderText = "4-1客";
            this.R4_1_B.Name = "R4_1_B";
            this.R4_1_B.ReadOnly = true;
            // 
            // R4_2_B
            // 
            this.R4_2_B.DataPropertyName = "R4_2_B";
            this.R4_2_B.HeaderText = "4-2客";
            this.R4_2_B.Name = "R4_2_B";
            this.R4_2_B.ReadOnly = true;
            // 
            // R4_3_B
            // 
            this.R4_3_B.DataPropertyName = "R4_3_B";
            this.R4_3_B.HeaderText = "4-3客";
            this.R4_3_B.Name = "R4_3_B";
            this.R4_3_B.ReadOnly = true;
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(906, 140);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(75, 40);
            this.btn_open.TabIndex = 3;
            this.btn_open.Text = "开奖";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // lbl_gamematch
            // 
            this.lbl_gamematch.AutoSize = true;
            this.lbl_gamematch.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_gamematch.Location = new System.Drawing.Point(12, 26);
            this.lbl_gamematch.Name = "lbl_gamematch";
            this.lbl_gamematch.Size = new System.Drawing.Size(39, 20);
            this.lbl_gamematch.TabIndex = 4;
            this.lbl_gamematch.Text = "球赛";
            // 
            // lbl_result
            // 
            this.lbl_result.AutoSize = true;
            this.lbl_result.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_result.Location = new System.Drawing.Point(617, 26);
            this.lbl_result.Name = "lbl_result";
            this.lbl_result.Size = new System.Drawing.Size(39, 20);
            this.lbl_result.TabIndex = 5;
            this.lbl_result.Text = "结果";
            // 
            // cb_wxsourcetype
            // 
            this.cb_wxsourcetype.FormattingEnabled = true;
            this.cb_wxsourcetype.Items.AddRange(new object[] {
            "微",
            "易"});
            this.cb_wxsourcetype.Location = new System.Drawing.Point(490, 26);
            this.cb_wxsourcetype.Name = "cb_wxsourcetype";
            this.cb_wxsourcetype.Size = new System.Drawing.Size(121, 20);
            this.cb_wxsourcetype.TabIndex = 6;
            this.cb_wxsourcetype.SelectedIndexChanged += new System.EventHandler(this.cb_wxsourcetype_SelectedIndexChanged);
            // 
            // OpenGameID
            // 
            this.OpenGameID.DataPropertyName = "OpenGameID";
            this.OpenGameID.HeaderText = "OpenGameID";
            this.OpenGameID.Name = "OpenGameID";
            this.OpenGameID.Visible = false;
            // 
            // TimeType
            // 
            this.TimeType.DataPropertyName = "TimeType";
            this.TimeType.HeaderText = "类别";
            this.TimeType.Name = "TimeType";
            this.TimeType.ReadOnly = true;
            this.TimeType.Width = 80;
            // 
            // TeamA
            // 
            this.TeamA.DataPropertyName = "TeamA";
            this.TeamA.HeaderText = "主";
            this.TeamA.Name = "TeamA";
            this.TeamA.Width = 70;
            // 
            // TeamB
            // 
            this.TeamB.DataPropertyName = "TeamB";
            this.TeamB.HeaderText = "客";
            this.TeamB.Name = "TeamB";
            this.TeamB.Width = 70;
            // 
            // BallOpen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 442);
            this.Controls.Add(this.cb_wxsourcetype);
            this.Controls.Add(this.lbl_result);
            this.Controls.Add(this.lbl_gamematch);
            this.Controls.Add(this.btn_open);
            this.Controls.Add(this.gv_playersbuys);
            this.Controls.Add(this.gv_result);
            this.Controls.Add(this.gv_balls);
            this.Name = "BallOpen";
            this.Text = "球赛开奖";
            this.Load += new System.EventHandler(this.BallOpen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gv_balls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_result)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_playersbuys)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gv_balls;
        private System.Windows.Forms.DataGridView gv_result;
        private System.Windows.Forms.DataGridView gv_playersbuys;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.Label lbl_gamematch;
        private System.Windows.Forms.Label lbl_result;
        private System.Windows.Forms.ComboBox cb_wxsourcetype;
        private System.Windows.Forms.DataGridViewTextBoxColumn GameID;
        private System.Windows.Forms.DataGridViewTextBoxColumn GameVS;
        private System.Windows.Forms.DataGridViewTextBoxColumn WX_GameIDUnOpen;
        private System.Windows.Forms.DataGridViewTextBoxColumn WX_UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn transtime;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyRatio;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyType;
        private System.Windows.Forms.DataGridViewTextBoxColumn A_WIN;
        private System.Windows.Forms.DataGridViewTextBoxColumn Winless;
        private System.Windows.Forms.DataGridViewTextBoxColumn B_Win;
        private System.Windows.Forms.DataGridViewTextBoxColumn BigWin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn SmallWin;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_A_A;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_A_SAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_A_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_SAME_A;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_SAME_SAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_SAME_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_B_A;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_B_SAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_B_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn R1_0_A;
        private System.Windows.Forms.DataGridViewTextBoxColumn R2_0_A;
        private System.Windows.Forms.DataGridViewTextBoxColumn R2_1_A;
        private System.Windows.Forms.DataGridViewTextBoxColumn R3_0_A;
        private System.Windows.Forms.DataGridViewTextBoxColumn R3_1_A;
        private System.Windows.Forms.DataGridViewTextBoxColumn R3_2_A;
        private System.Windows.Forms.DataGridViewTextBoxColumn R4_0_A;
        private System.Windows.Forms.DataGridViewTextBoxColumn R4_1_A;
        private System.Windows.Forms.DataGridViewTextBoxColumn R4_2_A;
        private System.Windows.Forms.DataGridViewTextBoxColumn R4_3_A;
        private System.Windows.Forms.DataGridViewTextBoxColumn R0_0;
        private System.Windows.Forms.DataGridViewTextBoxColumn R1_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn R2_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn R3_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn R4_4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rother;
        private System.Windows.Forms.DataGridViewTextBoxColumn R1_0_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn R2_0_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn R2_1_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn R3_0_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn R3_1_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn R3_2_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn R4_0_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn R4_1_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn R4_2_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn R4_3_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpenGameID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn TeamA;
        private System.Windows.Forms.DataGridViewTextBoxColumn TeamB;

    }
}