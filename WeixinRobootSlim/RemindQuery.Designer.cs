namespace WeixinRobootSlim
{
    partial class RemindQuery
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
            this.gv_data = new System.Windows.Forms.DataGridView();
            this.cb_wxsourcetype = new System.Windows.Forms.ComboBox();
            this.gv_talk = new System.Windows.Forms.DataGridView();
            this.lbl_talk = new System.Windows.Forms.Label();
            this.lbl_changepoint = new System.Windows.Forms.Label();
            this.gv_changepoint = new System.Windows.Forms.DataGridView();
            this.玩家 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.余 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WX_UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WX_SourceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.聊天时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.聊天内容 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.下注时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.类型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.下注 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.分数变动 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gv_data)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_talk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_changepoint)).BeginInit();
            this.SuspendLayout();
            // 
            // gv_data
            // 
            this.gv_data.AllowUserToAddRows = false;
            this.gv_data.AllowUserToDeleteRows = false;
            this.gv_data.AllowUserToOrderColumns = true;
            this.gv_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.玩家,
            this.余,
            this.WX_UserName,
            this.WX_SourceType});
            this.gv_data.Location = new System.Drawing.Point(13, 38);
            this.gv_data.Name = "gv_data";
            this.gv_data.ReadOnly = true;
            this.gv_data.RowHeadersWidth = 12;
            this.gv_data.RowTemplate.Height = 23;
            this.gv_data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv_data.Size = new System.Drawing.Size(307, 488);
            this.gv_data.TabIndex = 0;
            this.gv_data.SelectionChanged += new System.EventHandler(this.gv_data_SelectionChanged);
            // 
            // cb_wxsourcetype
            // 
            this.cb_wxsourcetype.FormattingEnabled = true;
            this.cb_wxsourcetype.Items.AddRange(new object[] {
            "微",
            "易"});
            this.cb_wxsourcetype.Location = new System.Drawing.Point(13, 12);
            this.cb_wxsourcetype.Name = "cb_wxsourcetype";
            this.cb_wxsourcetype.Size = new System.Drawing.Size(121, 20);
            this.cb_wxsourcetype.TabIndex = 1;
            this.cb_wxsourcetype.SelectedValueChanged += new System.EventHandler(this.cb_wxsourcetype_SelectedValueChanged);
            // 
            // gv_talk
            // 
            this.gv_talk.AllowUserToAddRows = false;
            this.gv_talk.AllowUserToDeleteRows = false;
            this.gv_talk.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_talk.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.聊天时间,
            this.聊天内容});
            this.gv_talk.Location = new System.Drawing.Point(343, 43);
            this.gv_talk.Name = "gv_talk";
            this.gv_talk.ReadOnly = true;
            this.gv_talk.RowHeadersWidth = 12;
            this.gv_talk.RowTemplate.Height = 23;
            this.gv_talk.Size = new System.Drawing.Size(511, 158);
            this.gv_talk.TabIndex = 2;
            // 
            // lbl_talk
            // 
            this.lbl_talk.AutoSize = true;
            this.lbl_talk.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_talk.Location = new System.Drawing.Point(343, 19);
            this.lbl_talk.Name = "lbl_talk";
            this.lbl_talk.Size = new System.Drawing.Size(74, 21);
            this.lbl_talk.TabIndex = 3;
            this.lbl_talk.Text = "聊天记录";
            // 
            // lbl_changepoint
            // 
            this.lbl_changepoint.AutoSize = true;
            this.lbl_changepoint.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_changepoint.Location = new System.Drawing.Point(343, 216);
            this.lbl_changepoint.Name = "lbl_changepoint";
            this.lbl_changepoint.Size = new System.Drawing.Size(106, 21);
            this.lbl_changepoint.TabIndex = 5;
            this.lbl_changepoint.Text = "下注赔付记录";
            // 
            // gv_changepoint
            // 
            this.gv_changepoint.AllowUserToAddRows = false;
            this.gv_changepoint.AllowUserToDeleteRows = false;
            this.gv_changepoint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_changepoint.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.下注时间,
            this.类型,
            this.下注,
            this.分数变动});
            this.gv_changepoint.Location = new System.Drawing.Point(343, 243);
            this.gv_changepoint.Name = "gv_changepoint";
            this.gv_changepoint.ReadOnly = true;
            this.gv_changepoint.RowHeadersWidth = 12;
            this.gv_changepoint.RowTemplate.Height = 23;
            this.gv_changepoint.Size = new System.Drawing.Size(511, 283);
            this.gv_changepoint.TabIndex = 4;
            // 
            // 玩家
            // 
            this.玩家.DataPropertyName = "玩家";
            this.玩家.HeaderText = "玩家";
            this.玩家.Name = "玩家";
            this.玩家.ReadOnly = true;
            this.玩家.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.玩家.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.玩家.Width = 180;
            // 
            // 余
            // 
            this.余.DataPropertyName = "余";
            this.余.HeaderText = "余";
            this.余.Name = "余";
            this.余.ReadOnly = true;
            this.余.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.余.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // WX_UserName
            // 
            this.WX_UserName.DataPropertyName = "WX_UserName";
            this.WX_UserName.HeaderText = "WX_UserName";
            this.WX_UserName.Name = "WX_UserName";
            this.WX_UserName.ReadOnly = true;
            this.WX_UserName.Visible = false;
            // 
            // WX_SourceType
            // 
            this.WX_SourceType.DataPropertyName = "WX_SourceType";
            this.WX_SourceType.HeaderText = "WX_SourceType";
            this.WX_SourceType.Name = "WX_SourceType";
            this.WX_SourceType.ReadOnly = true;
            this.WX_SourceType.Visible = false;
            // 
            // 聊天时间
            // 
            this.聊天时间.DataPropertyName = "时间";
            this.聊天时间.HeaderText = "时间";
            this.聊天时间.Name = "聊天时间";
            this.聊天时间.ReadOnly = true;
            this.聊天时间.Width = 130;
            // 
            // 聊天内容
            // 
            this.聊天内容.DataPropertyName = "内容";
            this.聊天内容.HeaderText = "内容";
            this.聊天内容.Name = "聊天内容";
            this.聊天内容.ReadOnly = true;
            this.聊天内容.Width = 250;
            // 
            // 下注时间
            // 
            this.下注时间.DataPropertyName = "时间";
            this.下注时间.HeaderText = "时间";
            this.下注时间.Name = "下注时间";
            this.下注时间.ReadOnly = true;
            this.下注时间.Width = 130;
            // 
            // 类型
            // 
            this.类型.DataPropertyName = "类型";
            this.类型.HeaderText = "类型";
            this.类型.Name = "类型";
            this.类型.ReadOnly = true;
            // 
            // 下注
            // 
            this.下注.DataPropertyName = "下注";
            this.下注.HeaderText = "下注";
            this.下注.Name = "下注";
            this.下注.ReadOnly = true;
            // 
            // 分数变动
            // 
            this.分数变动.DataPropertyName = "分数变动";
            this.分数变动.HeaderText = "分数变动";
            this.分数变动.Name = "分数变动";
            this.分数变动.ReadOnly = true;
            // 
            // RemindQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 538);
            this.Controls.Add(this.lbl_changepoint);
            this.Controls.Add(this.gv_changepoint);
            this.Controls.Add(this.lbl_talk);
            this.Controls.Add(this.gv_talk);
            this.Controls.Add(this.cb_wxsourcetype);
            this.Controls.Add(this.gv_data);
            this.Name = "RemindQuery";
            this.Text = "余分查询";
            this.Load += new System.EventHandler(this.RemindQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gv_data)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_talk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_changepoint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gv_data;
        private System.Windows.Forms.ComboBox cb_wxsourcetype;
        private System.Windows.Forms.DataGridView gv_talk;
        private System.Windows.Forms.Label lbl_talk;
        private System.Windows.Forms.Label lbl_changepoint;
        private System.Windows.Forms.DataGridView gv_changepoint;
        private System.Windows.Forms.DataGridViewTextBoxColumn 玩家;
        private System.Windows.Forms.DataGridViewTextBoxColumn 余;
        private System.Windows.Forms.DataGridViewTextBoxColumn WX_UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn WX_SourceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn 聊天时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 聊天内容;
        private System.Windows.Forms.DataGridViewTextBoxColumn 下注时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 类型;
        private System.Windows.Forms.DataGridViewTextBoxColumn 下注;
        private System.Windows.Forms.DataGridViewTextBoxColumn 分数变动;
    }
}