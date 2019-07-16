namespace WeixinRobootSlim
{
    partial class BallGames
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
            this.gv_GameList = new System.Windows.Forms.DataGridView();
            this.bs_gamelist = new System.Windows.Forms.BindingSource(this.components);
            this.bs_ratiocurrent = new System.Windows.Forms.BindingSource(this.components);
            this.bs_ratios = new System.Windows.Forms.BindingSource(this.components);
            this.gv_ratios = new System.Windows.Forms.DataGridView();
            this.gv_ratiocurrent = new System.Windows.Forms.DataGridView();
            this.Team = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R1_0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R2_0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R2_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R3_0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R3_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R3_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R4_0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R4_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R4_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R4_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R0_0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R1_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R2_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R3_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R4_4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rother = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gv_ratiocurrent2 = new System.Windows.Forms.DataGridView();
            this.R_A_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_A_SAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_A_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_SAME_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_SAME_SAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_SAME_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_B_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_B_SAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_B_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_ratiocurrent2 = new System.Windows.Forms.BindingSource(this.components);
            this.GameTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatchClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GameVS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GameKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.球类 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RatioType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RCompanyType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A_WIN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Winless = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.B_Win = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BigWin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SmallWin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gv_GameList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_gamelist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_ratiocurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_ratios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_ratios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_ratiocurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_ratiocurrent2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_ratiocurrent2)).BeginInit();
            this.SuspendLayout();
            // 
            // gv_GameList
            // 
            this.gv_GameList.AllowUserToAddRows = false;
            this.gv_GameList.AllowUserToDeleteRows = false;
            this.gv_GameList.AllowUserToOrderColumns = true;
            this.gv_GameList.AutoGenerateColumns = false;
            this.gv_GameList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_GameList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GameTime,
            this.MatchClass,
            this.GameVS,
            this.GameKey,
            this.球类});
            this.gv_GameList.DataSource = this.bs_gamelist;
            this.gv_GameList.Location = new System.Drawing.Point(22, 28);
            this.gv_GameList.Name = "gv_GameList";
            this.gv_GameList.ReadOnly = true;
            this.gv_GameList.RowHeadersWidth = 10;
            this.gv_GameList.RowTemplate.Height = 23;
            this.gv_GameList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv_GameList.Size = new System.Drawing.Size(960, 154);
            this.gv_GameList.TabIndex = 0;
            this.gv_GameList.SelectionChanged += new System.EventHandler(this.gv_GameList_SelectionChanged);
            // 
            // gv_ratios
            // 
            this.gv_ratios.AllowUserToAddRows = false;
            this.gv_ratios.AllowUserToDeleteRows = false;
            this.gv_ratios.AllowUserToOrderColumns = true;
            this.gv_ratios.AutoGenerateColumns = false;
            this.gv_ratios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_ratios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RatioType,
            this.RCompanyType,
            this.A_WIN,
            this.Winless,
            this.B_Win,
            this.BigWin,
            this.Total,
            this.SmallWin});
            this.gv_ratios.DataSource = this.bs_ratios;
            this.gv_ratios.Location = new System.Drawing.Point(22, 188);
            this.gv_ratios.Name = "gv_ratios";
            this.gv_ratios.ReadOnly = true;
            this.gv_ratios.RowTemplate.Height = 23;
            this.gv_ratios.Size = new System.Drawing.Size(960, 106);
            this.gv_ratios.TabIndex = 1;
            // 
            // gv_ratiocurrent
            // 
            this.gv_ratiocurrent.AllowUserToAddRows = false;
            this.gv_ratiocurrent.AllowUserToDeleteRows = false;
            this.gv_ratiocurrent.AllowUserToOrderColumns = true;
            this.gv_ratiocurrent.AutoGenerateColumns = false;
            this.gv_ratiocurrent.ColumnHeadersHeight = 22;
            this.gv_ratiocurrent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gv_ratiocurrent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Team,
            this.R1_0,
            this.R2_0,
            this.R2_1,
            this.R3_0,
            this.R3_1,
            this.R3_2,
            this.R4_0,
            this.R4_1,
            this.R4_2,
            this.R4_3,
            this.R0_0,
            this.R1_1,
            this.R2_2,
            this.R3_3,
            this.R4_4,
            this.Rother});
            this.gv_ratiocurrent.DataSource = this.bs_ratiocurrent;
            this.gv_ratiocurrent.Location = new System.Drawing.Point(22, 313);
            this.gv_ratiocurrent.Name = "gv_ratiocurrent";
            this.gv_ratiocurrent.ReadOnly = true;
            this.gv_ratiocurrent.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gv_ratiocurrent.RowTemplate.Height = 23;
            this.gv_ratiocurrent.Size = new System.Drawing.Size(960, 76);
            this.gv_ratiocurrent.TabIndex = 2;
            // 
            // Team
            // 
            this.Team.DataPropertyName = "Team";
            this.Team.HeaderText = "队伍";
            this.Team.Name = "Team";
            this.Team.ReadOnly = true;
            // 
            // R1_0
            // 
            this.R1_0.DataPropertyName = "R1_0";
            this.R1_0.HeaderText = "1-0";
            this.R1_0.Name = "R1_0";
            this.R1_0.ReadOnly = true;
            this.R1_0.Width = 30;
            // 
            // R2_0
            // 
            this.R2_0.DataPropertyName = "R2_0";
            this.R2_0.HeaderText = "2-0";
            this.R2_0.Name = "R2_0";
            this.R2_0.ReadOnly = true;
            this.R2_0.Width = 30;
            // 
            // R2_1
            // 
            this.R2_1.DataPropertyName = "R2_1";
            this.R2_1.HeaderText = "2-1";
            this.R2_1.Name = "R2_1";
            this.R2_1.ReadOnly = true;
            this.R2_1.Width = 30;
            // 
            // R3_0
            // 
            this.R3_0.DataPropertyName = "R3_0";
            this.R3_0.HeaderText = "3-0";
            this.R3_0.Name = "R3_0";
            this.R3_0.ReadOnly = true;
            this.R3_0.Width = 30;
            // 
            // R3_1
            // 
            this.R3_1.DataPropertyName = "R3_1";
            this.R3_1.HeaderText = "3-1";
            this.R3_1.Name = "R3_1";
            this.R3_1.ReadOnly = true;
            this.R3_1.Width = 30;
            // 
            // R3_2
            // 
            this.R3_2.DataPropertyName = "R3_2";
            this.R3_2.HeaderText = "3-2";
            this.R3_2.Name = "R3_2";
            this.R3_2.ReadOnly = true;
            this.R3_2.Width = 30;
            // 
            // R4_0
            // 
            this.R4_0.DataPropertyName = "R4_0";
            this.R4_0.HeaderText = "4-0";
            this.R4_0.Name = "R4_0";
            this.R4_0.ReadOnly = true;
            this.R4_0.Width = 30;
            // 
            // R4_1
            // 
            this.R4_1.DataPropertyName = "R4_1";
            this.R4_1.HeaderText = "4-1";
            this.R4_1.Name = "R4_1";
            this.R4_1.ReadOnly = true;
            this.R4_1.Width = 30;
            // 
            // R4_2
            // 
            this.R4_2.DataPropertyName = "R4_2";
            this.R4_2.HeaderText = "4-2";
            this.R4_2.Name = "R4_2";
            this.R4_2.ReadOnly = true;
            this.R4_2.Width = 30;
            // 
            // R4_3
            // 
            this.R4_3.DataPropertyName = "R4_3";
            this.R4_3.HeaderText = "4-3";
            this.R4_3.Name = "R4_3";
            this.R4_3.ReadOnly = true;
            this.R4_3.Width = 30;
            // 
            // R0_0
            // 
            this.R0_0.DataPropertyName = "R0_0";
            this.R0_0.HeaderText = "0-0";
            this.R0_0.Name = "R0_0";
            this.R0_0.ReadOnly = true;
            this.R0_0.Width = 30;
            // 
            // R1_1
            // 
            this.R1_1.DataPropertyName = "R1_1";
            this.R1_1.HeaderText = "1-1";
            this.R1_1.Name = "R1_1";
            this.R1_1.ReadOnly = true;
            this.R1_1.Width = 30;
            // 
            // R2_2
            // 
            this.R2_2.DataPropertyName = "R2_2";
            this.R2_2.HeaderText = "2-2";
            this.R2_2.Name = "R2_2";
            this.R2_2.ReadOnly = true;
            this.R2_2.Width = 30;
            // 
            // R3_3
            // 
            this.R3_3.DataPropertyName = "R3_3";
            this.R3_3.HeaderText = "3-3";
            this.R3_3.Name = "R3_3";
            this.R3_3.ReadOnly = true;
            this.R3_3.Width = 30;
            // 
            // R4_4
            // 
            this.R4_4.DataPropertyName = "R4_4";
            this.R4_4.HeaderText = "4-4";
            this.R4_4.Name = "R4_4";
            this.R4_4.ReadOnly = true;
            this.R4_4.Width = 30;
            // 
            // Rother
            // 
            this.Rother.DataPropertyName = "Rother";
            this.Rother.HeaderText = "其他";
            this.Rother.Name = "Rother";
            this.Rother.ReadOnly = true;
            this.Rother.Width = 40;
            // 
            // gv_ratiocurrent2
            // 
            this.gv_ratiocurrent2.AllowUserToAddRows = false;
            this.gv_ratiocurrent2.AllowUserToDeleteRows = false;
            this.gv_ratiocurrent2.AllowUserToOrderColumns = true;
            this.gv_ratiocurrent2.AutoGenerateColumns = false;
            this.gv_ratiocurrent2.ColumnHeadersHeight = 22;
            this.gv_ratiocurrent2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gv_ratiocurrent2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.R_A_A,
            this.R_A_SAME,
            this.R_A_B,
            this.R_SAME_A,
            this.R_SAME_SAME,
            this.R_SAME_B,
            this.R_B_A,
            this.R_B_SAME,
            this.R_B_B});
            this.gv_ratiocurrent2.DataSource = this.bs_ratiocurrent2;
            this.gv_ratiocurrent2.Location = new System.Drawing.Point(22, 404);
            this.gv_ratiocurrent2.Name = "gv_ratiocurrent2";
            this.gv_ratiocurrent2.ReadOnly = true;
            this.gv_ratiocurrent2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gv_ratiocurrent2.RowTemplate.Height = 23;
            this.gv_ratiocurrent2.Size = new System.Drawing.Size(960, 76);
            this.gv_ratiocurrent2.TabIndex = 3;
            // 
            // R_A_A
            // 
            this.R_A_A.DataPropertyName = "R_A_A";
            this.R_A_A.HeaderText = "主/主";
            this.R_A_A.Name = "R_A_A";
            this.R_A_A.ReadOnly = true;
            this.R_A_A.Width = 50;
            // 
            // R_A_SAME
            // 
            this.R_A_SAME.DataPropertyName = "R_A_SAME";
            this.R_A_SAME.HeaderText = "主/和";
            this.R_A_SAME.Name = "R_A_SAME";
            this.R_A_SAME.ReadOnly = true;
            this.R_A_SAME.Width = 50;
            // 
            // R_A_B
            // 
            this.R_A_B.DataPropertyName = "R_A_B";
            this.R_A_B.HeaderText = "主/客";
            this.R_A_B.Name = "R_A_B";
            this.R_A_B.ReadOnly = true;
            this.R_A_B.Width = 50;
            // 
            // R_SAME_A
            // 
            this.R_SAME_A.DataPropertyName = "R_SAME_A";
            this.R_SAME_A.HeaderText = "和/主";
            this.R_SAME_A.Name = "R_SAME_A";
            this.R_SAME_A.ReadOnly = true;
            this.R_SAME_A.Width = 50;
            // 
            // R_SAME_SAME
            // 
            this.R_SAME_SAME.DataPropertyName = "R_SAME_SAME";
            this.R_SAME_SAME.HeaderText = "和/和";
            this.R_SAME_SAME.Name = "R_SAME_SAME";
            this.R_SAME_SAME.ReadOnly = true;
            this.R_SAME_SAME.Width = 50;
            // 
            // R_SAME_B
            // 
            this.R_SAME_B.DataPropertyName = "R_SAME_B";
            this.R_SAME_B.HeaderText = "和/客";
            this.R_SAME_B.Name = "R_SAME_B";
            this.R_SAME_B.ReadOnly = true;
            this.R_SAME_B.Width = 50;
            // 
            // R_B_A
            // 
            this.R_B_A.DataPropertyName = "R_B_A";
            this.R_B_A.HeaderText = "客/主";
            this.R_B_A.Name = "R_B_A";
            this.R_B_A.ReadOnly = true;
            this.R_B_A.Width = 50;
            // 
            // R_B_SAME
            // 
            this.R_B_SAME.DataPropertyName = "R_B_SAME";
            this.R_B_SAME.HeaderText = "客/和";
            this.R_B_SAME.Name = "R_B_SAME";
            this.R_B_SAME.ReadOnly = true;
            this.R_B_SAME.Width = 50;
            // 
            // R_B_B
            // 
            this.R_B_B.DataPropertyName = "R_B_B";
            this.R_B_B.HeaderText = "客/客";
            this.R_B_B.Name = "R_B_B";
            this.R_B_B.ReadOnly = true;
            this.R_B_B.Width = 50;
            // 
            // GameTime
            // 
            this.GameTime.DataPropertyName = "GameTime";
            this.GameTime.HeaderText = "时间";
            this.GameTime.Name = "GameTime";
            this.GameTime.ReadOnly = true;
            this.GameTime.Width = 110;
            // 
            // MatchClass
            // 
            this.MatchClass.DataPropertyName = "MatchClass";
            this.MatchClass.HeaderText = "类别";
            this.MatchClass.Name = "MatchClass";
            this.MatchClass.ReadOnly = true;
            // 
            // GameVS
            // 
            this.GameVS.DataPropertyName = "GameVS";
            this.GameVS.HeaderText = "对阵";
            this.GameVS.Name = "GameVS";
            this.GameVS.ReadOnly = true;
            this.GameVS.Width = 300;
            // 
            // GameKey
            // 
            this.GameKey.DataPropertyName = "GameKey";
            this.GameKey.HeaderText = "唯一ID";
            this.GameKey.Name = "GameKey";
            this.GameKey.ReadOnly = true;
            // 
            // 球类
            // 
            this.球类.DataPropertyName = "GameType";
            this.球类.HeaderText = "球类";
            this.球类.Name = "球类";
            this.球类.ReadOnly = true;
            // 
            // RatioType
            // 
            this.RatioType.DataPropertyName = "RatioType";
            this.RatioType.HeaderText = "盘型";
            this.RatioType.Name = "RatioType";
            this.RatioType.ReadOnly = true;
            // 
            // RCompanyType
            // 
            this.RCompanyType.DataPropertyName = "RCompanyType";
            this.RCompanyType.HeaderText = "公司";
            this.RCompanyType.Name = "RCompanyType";
            this.RCompanyType.ReadOnly = true;
            // 
            // A_WIN
            // 
            this.A_WIN.DataPropertyName = "A_WIN";
            this.A_WIN.HeaderText = "主队";
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
            this.B_Win.HeaderText = "客队";
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
            // BallGames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.gv_ratiocurrent2);
            this.Controls.Add(this.gv_ratiocurrent);
            this.Controls.Add(this.gv_ratios);
            this.Controls.Add(this.gv_GameList);
            this.Name = "BallGames";
            this.Text = "球赛列表";
            this.Load += new System.EventHandler(this.BallGames_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gv_GameList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_gamelist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_ratiocurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_ratios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_ratios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_ratiocurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_ratiocurrent2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_ratiocurrent2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gv_GameList;
        private System.Windows.Forms.BindingSource bs_gamelist;
        private System.Windows.Forms.BindingSource bs_ratiocurrent;
        private System.Windows.Forms.BindingSource bs_ratios;
        private System.Windows.Forms.DataGridView gv_ratios;
        private System.Windows.Forms.DataGridView gv_ratiocurrent;
        private System.Windows.Forms.DataGridView gv_ratiocurrent2;
        private System.Windows.Forms.BindingSource bs_ratiocurrent2;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_A_A;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_A_SAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_A_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_SAME_A;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_SAME_SAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_SAME_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_B_A;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_B_SAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_B_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn Team;
        private System.Windows.Forms.DataGridViewTextBoxColumn R1_0;
        private System.Windows.Forms.DataGridViewTextBoxColumn R2_0;
        private System.Windows.Forms.DataGridViewTextBoxColumn R2_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn R3_0;
        private System.Windows.Forms.DataGridViewTextBoxColumn R3_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn R3_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn R4_0;
        private System.Windows.Forms.DataGridViewTextBoxColumn R4_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn R4_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn R4_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn R0_0;
        private System.Windows.Forms.DataGridViewTextBoxColumn R1_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn R2_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn R3_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn R4_4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rother;
        private System.Windows.Forms.DataGridViewTextBoxColumn GameTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatchClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn GameVS;
        private System.Windows.Forms.DataGridViewTextBoxColumn GameKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn 球类;
        private System.Windows.Forms.DataGridViewTextBoxColumn RatioType;
        private System.Windows.Forms.DataGridViewTextBoxColumn RCompanyType;
        private System.Windows.Forms.DataGridViewTextBoxColumn A_WIN;
        private System.Windows.Forms.DataGridViewTextBoxColumn Winless;
        private System.Windows.Forms.DataGridViewTextBoxColumn B_Win;
        private System.Windows.Forms.DataGridViewTextBoxColumn BigWin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn SmallWin;
    }
}