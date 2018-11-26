namespace WeixinRoboot
{
    partial class PCWeChatSendImageSetting
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
            this.GV_PicSendSetting = new System.Windows.Forms.DataGridView();
            this.BS_GV_PicSendSetting = new System.Windows.Forms.BindingSource(this.components);
            this.btn_refresh = new System.Windows.Forms.Button();
            this.微信 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.开盘 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.开盘小时 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.开盘分钟 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.整点停止 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.封盘 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.封盘小时 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.封盘分钟 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.高速期 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.开奖后请下注 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.龙虎单图 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.名片 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.最后一期 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.文字1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.文字1间隔 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.文字2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.文字2间隔 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.文字3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.文字3间隔 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.球赛图片 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.球赛链接 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.球赛间隔 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GV_PicSendSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_GV_PicSendSetting)).BeginInit();
            this.SuspendLayout();
            // 
            // GV_PicSendSetting
            // 
            this.GV_PicSendSetting.AllowUserToAddRows = false;
            this.GV_PicSendSetting.AllowUserToDeleteRows = false;
            this.GV_PicSendSetting.AutoGenerateColumns = false;
            this.GV_PicSendSetting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GV_PicSendSetting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.微信,
            this.开盘,
            this.开盘小时,
            this.开盘分钟,
            this.整点停止,
            this.封盘,
            this.封盘小时,
            this.封盘分钟,
            this.高速期,
            this.开奖后请下注,
            this.龙虎单图,
            this.名片,
            this.最后一期,
            this.文字1,
            this.文字1间隔,
            this.文字2,
            this.文字2间隔,
            this.文字3,
            this.文字3间隔,
            this.球赛图片,
            this.球赛链接,
            this.球赛间隔});
            this.GV_PicSendSetting.DataSource = this.BS_GV_PicSendSetting;
            this.GV_PicSendSetting.Location = new System.Drawing.Point(12, 12);
            this.GV_PicSendSetting.Name = "GV_PicSendSetting";
            this.GV_PicSendSetting.RowHeadersWidth = 20;
            this.GV_PicSendSetting.RowTemplate.Height = 23;
            this.GV_PicSendSetting.Size = new System.Drawing.Size(995, 510);
            this.GV_PicSendSetting.TabIndex = 0;
            this.GV_PicSendSetting.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.GV_PicSendSetting_DataError);
            // 
            // btn_refresh
            // 
            this.btn_refresh.Location = new System.Drawing.Point(12, 528);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(81, 36);
            this.btn_refresh.TabIndex = 1;
            this.btn_refresh.Text = "刷新";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // 微信
            // 
            this.微信.DataPropertyName = "微信";
            this.微信.HeaderText = "微信";
            this.微信.Name = "微信";
            this.微信.ReadOnly = true;
            this.微信.Width = 150;
            // 
            // 开盘
            // 
            this.开盘.DataPropertyName = "开盘";
            this.开盘.HeaderText = "开盘";
            this.开盘.Name = "开盘";
            this.开盘.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.开盘.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.开盘.Width = 80;
            // 
            // 开盘小时
            // 
            this.开盘小时.DataPropertyName = "开盘小时";
            this.开盘小时.HeaderText = "开盘小时";
            this.开盘小时.Name = "开盘小时";
            // 
            // 开盘分钟
            // 
            this.开盘分钟.DataPropertyName = "开盘分钟";
            this.开盘分钟.HeaderText = "开盘分钟";
            this.开盘分钟.Name = "开盘分钟";
            // 
            // 整点停止
            // 
            this.整点停止.DataPropertyName = "整点停止";
            this.整点停止.HeaderText = "整点停止";
            this.整点停止.Name = "整点停止";
            this.整点停止.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.整点停止.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.整点停止.Width = 80;
            // 
            // 封盘
            // 
            this.封盘.DataPropertyName = "封盘";
            this.封盘.HeaderText = "封盘";
            this.封盘.Name = "封盘";
            this.封盘.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.封盘.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.封盘.Width = 70;
            // 
            // 封盘小时
            // 
            this.封盘小时.DataPropertyName = "封盘小时";
            this.封盘小时.HeaderText = "封盘小时";
            this.封盘小时.Name = "封盘小时";
            // 
            // 封盘分钟
            // 
            this.封盘分钟.DataPropertyName = "封盘分钟";
            this.封盘分钟.HeaderText = "封盘分钟";
            this.封盘分钟.Name = "封盘分钟";
            // 
            // 高速期
            // 
            this.高速期.DataPropertyName = "高速期";
            this.高速期.HeaderText = "高速期";
            this.高速期.Name = "高速期";
            this.高速期.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.高速期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.高速期.Width = 80;
            // 
            // 开奖后请下注
            // 
            this.开奖后请下注.DataPropertyName = "开奖后请下注";
            this.开奖后请下注.HeaderText = "开奖后请下注";
            this.开奖后请下注.Name = "开奖后请下注";
            this.开奖后请下注.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.开奖后请下注.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.开奖后请下注.Width = 90;
            // 
            // 龙虎单图
            // 
            this.龙虎单图.DataPropertyName = "龙虎单图";
            this.龙虎单图.HeaderText = "龙虎单图";
            this.龙虎单图.Name = "龙虎单图";
            this.龙虎单图.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.龙虎单图.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.龙虎单图.Width = 80;
            // 
            // 名片
            // 
            this.名片.DataPropertyName = "名片";
            this.名片.HeaderText = "名片";
            this.名片.Name = "名片";
            this.名片.Width = 80;
            // 
            // 最后一期
            // 
            this.最后一期.DataPropertyName = "最后一期";
            this.最后一期.HeaderText = "最后一期";
            this.最后一期.Name = "最后一期";
            this.最后一期.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.最后一期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.最后一期.Width = 80;
            // 
            // 文字1
            // 
            this.文字1.DataPropertyName = "文字1";
            this.文字1.HeaderText = "文字1";
            this.文字1.Name = "文字1";
            // 
            // 文字1间隔
            // 
            this.文字1间隔.DataPropertyName = "文字1间隔";
            this.文字1间隔.HeaderText = "文字1间隔分钟";
            this.文字1间隔.Name = "文字1间隔";
            // 
            // 文字2
            // 
            this.文字2.DataPropertyName = "文字2";
            this.文字2.HeaderText = "文字2";
            this.文字2.Name = "文字2";
            // 
            // 文字2间隔
            // 
            this.文字2间隔.DataPropertyName = "文字2间隔";
            this.文字2间隔.HeaderText = "文字2间隔分钟";
            this.文字2间隔.Name = "文字2间隔";
            // 
            // 文字3
            // 
            this.文字3.DataPropertyName = "文字3";
            this.文字3.HeaderText = "文字3";
            this.文字3.Name = "文字3";
            // 
            // 文字3间隔
            // 
            this.文字3间隔.DataPropertyName = "文字3间隔";
            this.文字3间隔.HeaderText = "文字3间隔分钟";
            this.文字3间隔.Name = "文字3间隔";
            // 
            // 球赛图片
            // 
            this.球赛图片.DataPropertyName = "球赛图片";
            this.球赛图片.HeaderText = "球赛图片";
            this.球赛图片.Name = "球赛图片";
            // 
            // 球赛链接
            // 
            this.球赛链接.DataPropertyName = "球赛链接";
            this.球赛链接.HeaderText = "球赛链接";
            this.球赛链接.Name = "球赛链接";
            this.球赛链接.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.球赛链接.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // 球赛间隔
            // 
            this.球赛间隔.DataPropertyName = "球赛间隔";
            this.球赛间隔.HeaderText = "球赛间隔";
            this.球赛间隔.Name = "球赛间隔";
            // 
            // PCWeChatSendImageSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 576);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.GV_PicSendSetting);
            this.Name = "PCWeChatSendImageSetting";
            this.Text = "PC微信群发图片设置";
            this.Load += new System.EventHandler(this.PCWeChatSendImageSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GV_PicSendSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_GV_PicSendSetting)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GV_PicSendSetting;
        private System.Windows.Forms.BindingSource BS_GV_PicSendSetting;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn 微信;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 开盘;
        private System.Windows.Forms.DataGridViewTextBoxColumn 开盘小时;
        private System.Windows.Forms.DataGridViewTextBoxColumn 开盘分钟;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 整点停止;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 封盘;
        private System.Windows.Forms.DataGridViewTextBoxColumn 封盘小时;
        private System.Windows.Forms.DataGridViewTextBoxColumn 封盘分钟;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 高速期;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 开奖后请下注;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 龙虎单图;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 名片;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 最后一期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 文字1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 文字1间隔;
        private System.Windows.Forms.DataGridViewTextBoxColumn 文字2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 文字2间隔;
        private System.Windows.Forms.DataGridViewTextBoxColumn 文字3;
        private System.Windows.Forms.DataGridViewTextBoxColumn 文字3间隔;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 球赛图片;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 球赛链接;
        private System.Windows.Forms.DataGridViewTextBoxColumn 球赛间隔;

    }
}