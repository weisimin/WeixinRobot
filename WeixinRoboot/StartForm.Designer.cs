namespace WeixinRoboot
{
    partial class StartForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.PicBarCode = new System.Windows.Forms.PictureBox();
            this.lbl_msg = new System.Windows.Forms.Label();
            this.btn_resfresh = new System.Windows.Forms.Button();
            this.tm_refresh = new System.Windows.Forms.Timer(this.components);
            this.TopMenu = new System.Windows.Forms.MenuStrip();
            this.MI_Yonghu = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_UserSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.新用户ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_ModifyUser = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_MyData = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_Ratio = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_Ratio_Setting = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_Bouns_Setting = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_GameLog = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_GameLogManulDeal = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_Bouns_Manul = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_Query = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_OpenQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.Botton_Status = new System.Windows.Forms.StatusStrip();
            this.SI_url = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_ShowError = new System.Windows.Forms.ToolStripStatusLabel();
            this.SI_ShowError = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_waring = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PicBarCode)).BeginInit();
            this.TopMenu.SuspendLayout();
            this.Botton_Status.SuspendLayout();
            this.SuspendLayout();
            // 
            // PicBarCode
            // 
            this.PicBarCode.Location = new System.Drawing.Point(162, 83);
            this.PicBarCode.Name = "PicBarCode";
            this.PicBarCode.Size = new System.Drawing.Size(223, 190);
            this.PicBarCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicBarCode.TabIndex = 0;
            this.PicBarCode.TabStop = false;
            // 
            // lbl_msg
            // 
            this.lbl_msg.AutoSize = true;
            this.lbl_msg.Location = new System.Drawing.Point(251, 295);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(77, 12);
            this.lbl_msg.TabIndex = 1;
            this.lbl_msg.Text = "扫描微信登陆";
            // 
            // btn_resfresh
            // 
            this.btn_resfresh.Location = new System.Drawing.Point(469, 261);
            this.btn_resfresh.Name = "btn_resfresh";
            this.btn_resfresh.Size = new System.Drawing.Size(75, 23);
            this.btn_resfresh.TabIndex = 2;
            this.btn_resfresh.Text = "重启微信";
            this.btn_resfresh.UseVisualStyleBackColor = true;
            this.btn_resfresh.Click += new System.EventHandler(this.btn_resfresh_Click);
            // 
            // tm_refresh
            // 
            this.tm_refresh.Interval = 500;
            this.tm_refresh.Tick += new System.EventHandler(this.tm_refresh_Tick);
            // 
            // TopMenu
            // 
            this.TopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_Yonghu,
            this.MI_Ratio,
            this.MI_GameLog,
            this.MI_Query});
            this.TopMenu.Location = new System.Drawing.Point(0, 0);
            this.TopMenu.Name = "TopMenu";
            this.TopMenu.Size = new System.Drawing.Size(582, 25);
            this.TopMenu.TabIndex = 3;
            this.TopMenu.Text = "menuStrip1";
            // 
            // MI_Yonghu
            // 
            this.MI_Yonghu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_UserSetting,
            this.MI_MyData});
            this.MI_Yonghu.Name = "MI_Yonghu";
            this.MI_Yonghu.Size = new System.Drawing.Size(44, 21);
            this.MI_Yonghu.Text = "用户";
            // 
            // MI_UserSetting
            // 
            this.MI_UserSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新用户ToolStripMenuItem,
            this.MI_ModifyUser});
            this.MI_UserSetting.Name = "MI_UserSetting";
            this.MI_UserSetting.Size = new System.Drawing.Size(124, 22);
            this.MI_UserSetting.Text = "用户设置";
            // 
            // 新用户ToolStripMenuItem
            // 
            this.新用户ToolStripMenuItem.Name = "新用户ToolStripMenuItem";
            this.新用户ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.新用户ToolStripMenuItem.Text = "新用户";
            this.新用户ToolStripMenuItem.Click += new System.EventHandler(this.MI_NewUser_Click);
            // 
            // MI_ModifyUser
            // 
            this.MI_ModifyUser.Name = "MI_ModifyUser";
            this.MI_ModifyUser.Size = new System.Drawing.Size(124, 22);
            this.MI_ModifyUser.Text = "信息更改";
            this.MI_ModifyUser.Click += new System.EventHandler(this.MI_UserSetting_Click);
            // 
            // MI_MyData
            // 
            this.MI_MyData.Name = "MI_MyData";
            this.MI_MyData.Size = new System.Drawing.Size(124, 22);
            this.MI_MyData.Text = "我的资料";
            this.MI_MyData.Click += new System.EventHandler(this.MI_MyData_Click);
            // 
            // MI_Ratio
            // 
            this.MI_Ratio.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_Ratio_Setting,
            this.MI_Bouns_Setting});
            this.MI_Ratio.Name = "MI_Ratio";
            this.MI_Ratio.Size = new System.Drawing.Size(44, 21);
            this.MI_Ratio.Text = "赔率";
            // 
            // MI_Ratio_Setting
            // 
            this.MI_Ratio_Setting.Name = "MI_Ratio_Setting";
            this.MI_Ratio_Setting.Size = new System.Drawing.Size(124, 22);
            this.MI_Ratio_Setting.Text = "赔率设置";
            this.MI_Ratio_Setting.Click += new System.EventHandler(this.MI_Ratio_Setting_Click);
            // 
            // MI_Bouns_Setting
            // 
            this.MI_Bouns_Setting.Name = "MI_Bouns_Setting";
            this.MI_Bouns_Setting.Size = new System.Drawing.Size(124, 22);
            this.MI_Bouns_Setting.Text = "福利设置";
            this.MI_Bouns_Setting.Click += new System.EventHandler(this.MI_Bouns_Setting_Click);
            // 
            // MI_GameLog
            // 
            this.MI_GameLog.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_GameLogManulDeal,
            this.MI_Bouns_Manul});
            this.MI_GameLog.Name = "MI_GameLog";
            this.MI_GameLog.Size = new System.Drawing.Size(68, 21);
            this.MI_GameLog.Text = "人工操作";
            // 
            // MI_GameLogManulDeal
            // 
            this.MI_GameLogManulDeal.Enabled = false;
            this.MI_GameLogManulDeal.Name = "MI_GameLogManulDeal";
            this.MI_GameLogManulDeal.Size = new System.Drawing.Size(152, 22);
            this.MI_GameLogManulDeal.Text = "人工开奖";
            this.MI_GameLogManulDeal.Click += new System.EventHandler(this.MI_GameLogManulDeal_Click);
            // 
            // MI_Bouns_Manul
            // 
            this.MI_Bouns_Manul.Enabled = false;
            this.MI_Bouns_Manul.Name = "MI_Bouns_Manul";
            this.MI_Bouns_Manul.Size = new System.Drawing.Size(152, 22);
            this.MI_Bouns_Manul.Text = "人工福利";
            this.MI_Bouns_Manul.Click += new System.EventHandler(this.MI_Bouns_Manul_Click);
            // 
            // MI_Query
            // 
            this.MI_Query.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_OpenQuery});
            this.MI_Query.Name = "MI_Query";
            this.MI_Query.Size = new System.Drawing.Size(44, 21);
            this.MI_Query.Text = "查询";
            // 
            // MI_OpenQuery
            // 
            this.MI_OpenQuery.Name = "MI_OpenQuery";
            this.MI_OpenQuery.Size = new System.Drawing.Size(124, 22);
            this.MI_OpenQuery.Text = "开奖统计";
            this.MI_OpenQuery.Click += new System.EventHandler(this.MI_OpenQuery_Click);
            // 
            // Botton_Status
            // 
            this.Botton_Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SI_url,
            this.lbl_ShowError,
            this.SI_ShowError});
            this.Botton_Status.Location = new System.Drawing.Point(0, 308);
            this.Botton_Status.Name = "Botton_Status";
            this.Botton_Status.Size = new System.Drawing.Size(582, 22);
            this.Botton_Status.TabIndex = 4;
            this.Botton_Status.Text = "statusStrip1";
            // 
            // SI_url
            // 
            this.SI_url.AutoSize = false;
            this.SI_url.Name = "SI_url";
            this.SI_url.Size = new System.Drawing.Size(400, 17);
            this.SI_url.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_ShowError
            // 
            this.lbl_ShowError.AutoSize = false;
            this.lbl_ShowError.Name = "lbl_ShowError";
            this.lbl_ShowError.Size = new System.Drawing.Size(150, 17);
            // 
            // SI_ShowError
            // 
            this.SI_ShowError.AutoSize = false;
            this.SI_ShowError.Name = "SI_ShowError";
            this.SI_ShowError.Size = new System.Drawing.Size(250, 17);
            this.SI_ShowError.Text = "..";
            this.SI_ShowError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_waring
            // 
            this.lbl_waring.AutoSize = true;
            this.lbl_waring.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_waring.ForeColor = System.Drawing.Color.Red;
            this.lbl_waring.Location = new System.Drawing.Point(89, 44);
            this.lbl_waring.Name = "lbl_waring";
            this.lbl_waring.Size = new System.Drawing.Size(360, 16);
            this.lbl_waring.TabIndex = 5;
            this.lbl_waring.Text = "微信机器人仅用于学习和交流，不得用于非法用途";
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 330);
            this.Controls.Add(this.lbl_waring);
            this.Controls.Add(this.Botton_Status);
            this.Controls.Add(this.btn_resfresh);
            this.Controls.Add(this.lbl_msg);
            this.Controls.Add(this.PicBarCode);
            this.Controls.Add(this.TopMenu);
            this.MainMenuStrip = this.TopMenu;
            this.Name = "StartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "启动";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StartForm_FormClosing);
            this.Load += new System.EventHandler(this.Start_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PicBarCode)).EndInit();
            this.TopMenu.ResumeLayout(false);
            this.TopMenu.PerformLayout();
            this.Botton_Status.ResumeLayout(false);
            this.Botton_Status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PicBarCode;
        private System.Windows.Forms.Label lbl_msg;
        private System.Windows.Forms.Button btn_resfresh;
        private System.Windows.Forms.Timer tm_refresh;
        private System.Windows.Forms.MenuStrip TopMenu;
        private System.Windows.Forms.ToolStripMenuItem MI_Yonghu;
        private System.Windows.Forms.ToolStripMenuItem MI_UserSetting;
        private System.Windows.Forms.ToolStripMenuItem MI_MyData;
        private System.Windows.Forms.ToolStripMenuItem 新用户ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MI_ModifyUser;
        private System.Windows.Forms.StatusStrip Botton_Status;
        private System.Windows.Forms.ToolStripStatusLabel SI_url;
        private System.Windows.Forms.Label lbl_waring;
        private System.Windows.Forms.ToolStripStatusLabel lbl_ShowError;
        private System.Windows.Forms.ToolStripStatusLabel SI_ShowError;
        private System.Windows.Forms.ToolStripMenuItem MI_Ratio;
        private System.Windows.Forms.ToolStripMenuItem MI_Ratio_Setting;
        private System.Windows.Forms.ToolStripMenuItem MI_GameLog;
        private System.Windows.Forms.ToolStripMenuItem MI_GameLogManulDeal;
        private System.Windows.Forms.ToolStripMenuItem MI_Query;
        private System.Windows.Forms.ToolStripMenuItem MI_OpenQuery;
        private System.Windows.Forms.ToolStripMenuItem MI_Bouns_Setting;
        private System.Windows.Forms.ToolStripMenuItem MI_Bouns_Manul;
    }
}

