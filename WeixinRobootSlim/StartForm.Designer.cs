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
            this.lbl_msg = new System.Windows.Forms.Label();
            this.btn_resfresh = new System.Windows.Forms.Button();
            this.tm_refresh = new System.Windows.Forms.Timer(this.components);
            this.Botton_Status = new System.Windows.Forms.StatusStrip();
            this.SI_url = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_ShowError = new System.Windows.Forms.ToolStripStatusLabel();
            this.SI_ShowError = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_waring = new System.Windows.Forms.Label();
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
            this.mi_reminderquery = new System.Windows.Forms.ToolStripMenuItem();
            this.TopMenu = new System.Windows.Forms.MenuStrip();
            this.MI_PCWechatSend = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_PCWechatSendSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_BallMatch = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_BallOpenManul = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_BallGames = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenBlack = new System.Windows.Forms.Button();
            this.codeweixin = new System.Windows.Forms.Label();
            this.codeyixin = new System.Windows.Forms.Label();
            this.btn_refreshyixin = new System.Windows.Forms.Button();
            this.btn_bossreport = new System.Windows.Forms.Button();
            this.PicBarCode_yixin = new System.Windows.Forms.PictureBox();
            this.PicBarCode = new System.Windows.Forms.PictureBox();
            this.cb_refreshball = new System.Windows.Forms.CheckBox();
            this.lbl_six = new System.Windows.Forms.Label();
            this.lbl_qqthread = new System.Windows.Forms.Label();
            this.lbl_during = new System.Windows.Forms.Label();
            this.tb_StartHour = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_StartMinute = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_EndMinute = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_EndHour = new System.Windows.Forms.TextBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.cb_TwoTreeNotSingle = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_liushuiratio = new System.Windows.Forms.TextBox();
            this.tb_fuliratio = new System.Windows.Forms.TextBox();
            this.T_AoZhouCai = new System.Windows.Forms.CheckBox();
            this.T_VRChongQingShiShiCai = new System.Windows.Forms.CheckBox();
            this.T_TengXunShiFen = new System.Windows.Forms.CheckBox();
            this.T_TengXunWuFen = new System.Windows.Forms.CheckBox();
            this.T_WuFenCai = new System.Windows.Forms.CheckBox();
            this.T_XinJiangShiShiCai = new System.Windows.Forms.CheckBox();
            this.T_chongqingshishicai = new System.Windows.Forms.CheckBox();
            this.T_TengXunWuFenXin = new System.Windows.Forms.CheckBox();
            this.T_TengXunShiFenXin = new System.Windows.Forms.CheckBox();
            this.cb_robotstop = new System.Windows.Forms.CheckBox();
            this.T_HeNeiWuFen = new System.Windows.Forms.CheckBox();
            this.Botton_Status.SuspendLayout();
            this.TopMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBarCode_yixin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBarCode)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_msg
            // 
            this.lbl_msg.AutoSize = true;
            this.lbl_msg.Location = new System.Drawing.Point(250, 343);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(77, 12);
            this.lbl_msg.TabIndex = 1;
            this.lbl_msg.Text = "扫描微信登陆";
            // 
            // btn_resfresh
            // 
            this.btn_resfresh.Location = new System.Drawing.Point(161, 327);
            this.btn_resfresh.Name = "btn_resfresh";
            this.btn_resfresh.Size = new System.Drawing.Size(75, 23);
            this.btn_resfresh.TabIndex = 2;
            this.btn_resfresh.Text = "重启微信";
            this.btn_resfresh.UseVisualStyleBackColor = true;
            this.btn_resfresh.Click += new System.EventHandler(this.btn_resfresh_Click);
            // 
            // tm_refresh
            // 
            this.tm_refresh.Interval = 3000;
            this.tm_refresh.Tick += new System.EventHandler(this.tm_refresh_Tick);
            // 
            // Botton_Status
            // 
            this.Botton_Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SI_url,
            this.lbl_ShowError,
            this.SI_ShowError});
            this.Botton_Status.Location = new System.Drawing.Point(0, 397);
            this.Botton_Status.Name = "Botton_Status";
            this.Botton_Status.Size = new System.Drawing.Size(1008, 22);
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
            this.lbl_waring.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_waring.ForeColor = System.Drawing.Color.Red;
            this.lbl_waring.Location = new System.Drawing.Point(80, 35);
            this.lbl_waring.Name = "lbl_waring";
            this.lbl_waring.Size = new System.Drawing.Size(370, 45);
            this.lbl_waring.TabIndex = 5;
            this.lbl_waring.Text = "微信机器人仅用于学习和交流，不得用于非法用途由此产生各种问题，均与作者无关";
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
            // MI_Bouns_Manul
            // 
            this.MI_Bouns_Manul.Name = "MI_Bouns_Manul";
            this.MI_Bouns_Manul.Size = new System.Drawing.Size(152, 22);
            this.MI_Bouns_Manul.Text = "人工福利";
            this.MI_Bouns_Manul.Click += new System.EventHandler(this.MI_Bouns_Manul_Click);
            // 
            // MI_Query
            // 
            this.MI_Query.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_OpenQuery,
            this.mi_reminderquery});
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
            // mi_reminderquery
            // 
            this.mi_reminderquery.Name = "mi_reminderquery";
            this.mi_reminderquery.Size = new System.Drawing.Size(124, 22);
            this.mi_reminderquery.Text = "余分查询";
            this.mi_reminderquery.Click += new System.EventHandler(this.mi_reminderquery_Click);
            // 
            // TopMenu
            // 
            this.TopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_Yonghu,
            this.MI_Ratio,
            this.MI_GameLog,
            this.MI_Query,
            this.MI_PCWechatSend,
            this.MI_BallMatch});
            this.TopMenu.Location = new System.Drawing.Point(0, 0);
            this.TopMenu.Name = "TopMenu";
            this.TopMenu.Size = new System.Drawing.Size(1008, 25);
            this.TopMenu.TabIndex = 3;
            this.TopMenu.Text = "menuStrip1";
            this.TopMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.TopMenu_ItemClicked);
            // 
            // MI_PCWechatSend
            // 
            this.MI_PCWechatSend.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_PCWechatSendSetting});
            this.MI_PCWechatSend.Name = "MI_PCWechatSend";
            this.MI_PCWechatSend.Size = new System.Drawing.Size(68, 21);
            this.MI_PCWechatSend.Text = "注入发图";
            // 
            // MI_PCWechatSendSetting
            // 
            this.MI_PCWechatSendSetting.Name = "MI_PCWechatSendSetting";
            this.MI_PCWechatSendSetting.Size = new System.Drawing.Size(100, 22);
            this.MI_PCWechatSendSetting.Text = "设置";
            this.MI_PCWechatSendSetting.Click += new System.EventHandler(this.MI_PCWechatSendSetting_Click);
            // 
            // MI_BallMatch
            // 
            this.MI_BallMatch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_BallOpenManul,
            this.MI_BallGames});
            this.MI_BallMatch.Name = "MI_BallMatch";
            this.MI_BallMatch.Size = new System.Drawing.Size(44, 21);
            this.MI_BallMatch.Text = "球赛";
            // 
            // MI_BallOpenManul
            // 
            this.MI_BallOpenManul.Name = "MI_BallOpenManul";
            this.MI_BallOpenManul.Size = new System.Drawing.Size(124, 22);
            this.MI_BallOpenManul.Text = "人工开奖";
            this.MI_BallOpenManul.Click += new System.EventHandler(this.MI_BallOpenManul_Click);
            // 
            // MI_BallGames
            // 
            this.MI_BallGames.Name = "MI_BallGames";
            this.MI_BallGames.Size = new System.Drawing.Size(124, 22);
            this.MI_BallGames.Text = "赛事查看";
            this.MI_BallGames.Click += new System.EventHandler(this.MI_BallGames_Click);
            // 
            // OpenBlack
            // 
            this.OpenBlack.Location = new System.Drawing.Point(5, 221);
            this.OpenBlack.Name = "OpenBlack";
            this.OpenBlack.Size = new System.Drawing.Size(79, 26);
            this.OpenBlack.TabIndex = 9;
            this.OpenBlack.Text = "显示/隐藏黑框";
            this.OpenBlack.UseVisualStyleBackColor = true;
            this.OpenBlack.Click += new System.EventHandler(this.OpenBlack_Click);
            // 
            // codeweixin
            // 
            this.codeweixin.AutoSize = true;
            this.codeweixin.Location = new System.Drawing.Point(216, 80);
            this.codeweixin.Name = "codeweixin";
            this.codeweixin.Size = new System.Drawing.Size(65, 12);
            this.codeweixin.TabIndex = 12;
            this.codeweixin.Text = "微信二维码";
            // 
            // codeyixin
            // 
            this.codeyixin.AutoSize = true;
            this.codeyixin.Location = new System.Drawing.Point(580, 80);
            this.codeyixin.Name = "codeyixin";
            this.codeyixin.Size = new System.Drawing.Size(65, 12);
            this.codeyixin.TabIndex = 13;
            this.codeyixin.Text = "易信二维码";
            // 
            // btn_refreshyixin
            // 
            this.btn_refreshyixin.Location = new System.Drawing.Point(524, 327);
            this.btn_refreshyixin.Name = "btn_refreshyixin";
            this.btn_refreshyixin.Size = new System.Drawing.Size(75, 23);
            this.btn_refreshyixin.TabIndex = 14;
            this.btn_refreshyixin.Text = "重启易信";
            this.btn_refreshyixin.UseVisualStyleBackColor = true;
            this.btn_refreshyixin.Click += new System.EventHandler(this.btn_refreshyixin_Click);
            // 
            // btn_bossreport
            // 
            this.btn_bossreport.Location = new System.Drawing.Point(798, 295);
            this.btn_bossreport.Name = "btn_bossreport";
            this.btn_bossreport.Size = new System.Drawing.Size(75, 23);
            this.btn_bossreport.TabIndex = 15;
            this.btn_bossreport.Text = "老板报表";
            this.btn_bossreport.UseVisualStyleBackColor = true;
            this.btn_bossreport.Visible = false;
            this.btn_bossreport.Click += new System.EventHandler(this.btn_bossreport_Click);
            // 
            // PicBarCode_yixin
            // 
            this.PicBarCode_yixin.Location = new System.Drawing.Point(524, 99);
            this.PicBarCode_yixin.Name = "PicBarCode_yixin";
            this.PicBarCode_yixin.Size = new System.Drawing.Size(160, 160);
            this.PicBarCode_yixin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicBarCode_yixin.TabIndex = 11;
            this.PicBarCode_yixin.TabStop = false;
            // 
            // PicBarCode
            // 
            this.PicBarCode.Location = new System.Drawing.Point(178, 99);
            this.PicBarCode.Name = "PicBarCode";
            this.PicBarCode.Size = new System.Drawing.Size(160, 160);
            this.PicBarCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicBarCode.TabIndex = 0;
            this.PicBarCode.TabStop = false;
            // 
            // cb_refreshball
            // 
            this.cb_refreshball.AutoSize = true;
            this.cb_refreshball.Location = new System.Drawing.Point(12, 261);
            this.cb_refreshball.Name = "cb_refreshball";
            this.cb_refreshball.Size = new System.Drawing.Size(72, 16);
            this.cb_refreshball.TabIndex = 25;
            this.cb_refreshball.Text = "采集球赛";
            this.cb_refreshball.UseVisualStyleBackColor = true;
            // 
            // lbl_six
            // 
            this.lbl_six.AutoSize = true;
            this.lbl_six.Location = new System.Drawing.Point(12, 295);
            this.lbl_six.Name = "lbl_six";
            this.lbl_six.Size = new System.Drawing.Size(53, 12);
            this.lbl_six.TabIndex = 26;
            this.lbl_six.Text = "六下期：";
            // 
            // lbl_qqthread
            // 
            this.lbl_qqthread.AutoSize = true;
            this.lbl_qqthread.Location = new System.Drawing.Point(12, 327);
            this.lbl_qqthread.Name = "lbl_qqthread";
            this.lbl_qqthread.Size = new System.Drawing.Size(113, 12);
            this.lbl_qqthread.TabIndex = 27;
            this.lbl_qqthread.Text = "(ALT+O)采集:运行中";
            // 
            // lbl_during
            // 
            this.lbl_during.AutoSize = true;
            this.lbl_during.Location = new System.Drawing.Point(165, 294);
            this.lbl_during.Name = "lbl_during";
            this.lbl_during.Size = new System.Drawing.Size(59, 12);
            this.lbl_during.TabIndex = 28;
            this.lbl_during.Text = "封盘时段:";
            // 
            // tb_StartHour
            // 
            this.tb_StartHour.Location = new System.Drawing.Point(231, 289);
            this.tb_StartHour.Name = "tb_StartHour";
            this.tb_StartHour.Size = new System.Drawing.Size(23, 21);
            this.tb_StartHour.TabIndex = 29;
            this.tb_StartHour.Text = "3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(253, 292);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 30;
            this.label1.Text = ":";
            // 
            // tb_StartMinute
            // 
            this.tb_StartMinute.Location = new System.Drawing.Point(268, 289);
            this.tb_StartMinute.Name = "tb_StartMinute";
            this.tb_StartMinute.Size = new System.Drawing.Size(27, 21);
            this.tb_StartMinute.TabIndex = 31;
            this.tb_StartMinute.Text = "10";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(301, 293);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 32;
            this.label2.Text = "~";
            // 
            // tb_EndMinute
            // 
            this.tb_EndMinute.Location = new System.Drawing.Point(355, 289);
            this.tb_EndMinute.Name = "tb_EndMinute";
            this.tb_EndMinute.Size = new System.Drawing.Size(27, 21);
            this.tb_EndMinute.TabIndex = 35;
            this.tb_EndMinute.Text = "10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(340, 292);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 34;
            this.label3.Text = ":";
            // 
            // tb_EndHour
            // 
            this.tb_EndHour.Location = new System.Drawing.Point(318, 289);
            this.tb_EndHour.Name = "tb_EndHour";
            this.tb_EndHour.Size = new System.Drawing.Size(23, 21);
            this.tb_EndHour.TabIndex = 33;
            this.tb_EndHour.Text = "7";
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(389, 288);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 36;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // cb_TwoTreeNotSingle
            // 
            this.cb_TwoTreeNotSingle.AutoSize = true;
            this.cb_TwoTreeNotSingle.Location = new System.Drawing.Point(268, 316);
            this.cb_TwoTreeNotSingle.Name = "cb_TwoTreeNotSingle";
            this.cb_TwoTreeNotSingle.Size = new System.Drawing.Size(96, 16);
            this.cb_TwoTreeNotSingle.TabIndex = 52;
            this.cb_TwoTreeNotSingle.Text = "23不算单算和";
            this.cb_TwoTreeNotSingle.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(351, 266);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 54;
            this.label5.Text = "福利比例";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(351, 241);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 55;
            this.label6.Text = "流水比例";
            // 
            // tb_liushuiratio
            // 
            this.tb_liushuiratio.Location = new System.Drawing.Point(410, 238);
            this.tb_liushuiratio.Name = "tb_liushuiratio";
            this.tb_liushuiratio.Size = new System.Drawing.Size(51, 21);
            this.tb_liushuiratio.TabIndex = 56;
            // 
            // tb_fuliratio
            // 
            this.tb_fuliratio.Location = new System.Drawing.Point(410, 263);
            this.tb_fuliratio.Name = "tb_fuliratio";
            this.tb_fuliratio.Size = new System.Drawing.Size(51, 21);
            this.tb_fuliratio.TabIndex = 57;
            // 
            // T_AoZhouCai
            // 
            this.T_AoZhouCai.AutoSize = true;
            this.T_AoZhouCai.Location = new System.Drawing.Point(351, 99);
            this.T_AoZhouCai.Name = "T_AoZhouCai";
            this.T_AoZhouCai.Size = new System.Drawing.Size(60, 16);
            this.T_AoZhouCai.TabIndex = 58;
            this.T_AoZhouCai.Text = "澳洲彩";
            this.T_AoZhouCai.UseVisualStyleBackColor = true;
            // 
            // T_VRChongQingShiShiCai
            // 
            this.T_VRChongQingShiShiCai.AutoSize = true;
            this.T_VRChongQingShiShiCai.Location = new System.Drawing.Point(351, 116);
            this.T_VRChongQingShiShiCai.Name = "T_VRChongQingShiShiCai";
            this.T_VRChongQingShiShiCai.Size = new System.Drawing.Size(96, 16);
            this.T_VRChongQingShiShiCai.TabIndex = 59;
            this.T_VRChongQingShiShiCai.Text = "VR重庆时时彩";
            this.T_VRChongQingShiShiCai.UseVisualStyleBackColor = true;
            // 
            // T_TengXunShiFen
            // 
            this.T_TengXunShiFen.AutoSize = true;
            this.T_TengXunShiFen.Location = new System.Drawing.Point(351, 138);
            this.T_TengXunShiFen.Name = "T_TengXunShiFen";
            this.T_TengXunShiFen.Size = new System.Drawing.Size(72, 16);
            this.T_TengXunShiFen.TabIndex = 60;
            this.T_TengXunShiFen.Text = "腾讯十分";
            this.T_TengXunShiFen.UseVisualStyleBackColor = true;
            // 
            // T_TengXunWuFen
            // 
            this.T_TengXunWuFen.AutoSize = true;
            this.T_TengXunWuFen.Location = new System.Drawing.Point(351, 156);
            this.T_TengXunWuFen.Name = "T_TengXunWuFen";
            this.T_TengXunWuFen.Size = new System.Drawing.Size(72, 16);
            this.T_TengXunWuFen.TabIndex = 61;
            this.T_TengXunWuFen.Text = "腾讯五分";
            this.T_TengXunWuFen.UseVisualStyleBackColor = true;
            // 
            // T_WuFenCai
            // 
            this.T_WuFenCai.AutoSize = true;
            this.T_WuFenCai.Location = new System.Drawing.Point(351, 178);
            this.T_WuFenCai.Name = "T_WuFenCai";
            this.T_WuFenCai.Size = new System.Drawing.Size(60, 16);
            this.T_WuFenCai.TabIndex = 62;
            this.T_WuFenCai.Text = "五分彩";
            this.T_WuFenCai.UseVisualStyleBackColor = true;
            // 
            // T_XinJiangShiShiCai
            // 
            this.T_XinJiangShiShiCai.AutoSize = true;
            this.T_XinJiangShiShiCai.Location = new System.Drawing.Point(351, 200);
            this.T_XinJiangShiShiCai.Name = "T_XinJiangShiShiCai";
            this.T_XinJiangShiShiCai.Size = new System.Drawing.Size(84, 16);
            this.T_XinJiangShiShiCai.TabIndex = 63;
            this.T_XinJiangShiShiCai.Text = "新疆时时彩";
            this.T_XinJiangShiShiCai.UseVisualStyleBackColor = true;
            // 
            // T_chongqingshishicai
            // 
            this.T_chongqingshishicai.AutoSize = true;
            this.T_chongqingshishicai.Location = new System.Drawing.Point(351, 221);
            this.T_chongqingshishicai.Name = "T_chongqingshishicai";
            this.T_chongqingshishicai.Size = new System.Drawing.Size(84, 16);
            this.T_chongqingshishicai.TabIndex = 65;
            this.T_chongqingshishicai.Text = "重庆时时彩";
            this.T_chongqingshishicai.UseVisualStyleBackColor = true;
            // 
            // T_TengXunWuFenXin
            // 
            this.T_TengXunWuFenXin.AutoSize = true;
            this.T_TengXunWuFenXin.Location = new System.Drawing.Point(426, 156);
            this.T_TengXunWuFenXin.Name = "T_TengXunWuFenXin";
            this.T_TengXunWuFenXin.Size = new System.Drawing.Size(84, 16);
            this.T_TengXunWuFenXin.TabIndex = 73;
            this.T_TengXunWuFenXin.Text = "腾讯五分信";
            this.T_TengXunWuFenXin.UseVisualStyleBackColor = true;
            // 
            // T_TengXunShiFenXin
            // 
            this.T_TengXunShiFenXin.AutoSize = true;
            this.T_TengXunShiFenXin.Location = new System.Drawing.Point(426, 138);
            this.T_TengXunShiFenXin.Name = "T_TengXunShiFenXin";
            this.T_TengXunShiFenXin.Size = new System.Drawing.Size(84, 16);
            this.T_TengXunShiFenXin.TabIndex = 72;
            this.T_TengXunShiFenXin.Text = "腾讯十分信";
            this.T_TengXunShiFenXin.UseVisualStyleBackColor = true;
            // 
            // cb_robotstop
            // 
            this.cb_robotstop.AutoSize = true;
            this.cb_robotstop.Location = new System.Drawing.Point(90, 261);
            this.cb_robotstop.Name = "cb_robotstop";
            this.cb_robotstop.Size = new System.Drawing.Size(84, 16);
            this.cb_robotstop.TabIndex = 74;
            this.cb_robotstop.Text = "机器人停止";
            this.cb_robotstop.UseVisualStyleBackColor = true;
            // 
            // T_HeNeiWuFen
            // 
            this.T_HeNeiWuFen.AutoSize = true;
            this.T_HeNeiWuFen.Location = new System.Drawing.Point(426, 178);
            this.T_HeNeiWuFen.Name = "T_HeNeiWuFen";
            this.T_HeNeiWuFen.Size = new System.Drawing.Size(72, 16);
            this.T_HeNeiWuFen.TabIndex = 75;
            this.T_HeNeiWuFen.Text = "河内五分";
            this.T_HeNeiWuFen.UseVisualStyleBackColor = true;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 419);
            this.Controls.Add(this.T_HeNeiWuFen);
            this.Controls.Add(this.cb_robotstop);
            this.Controls.Add(this.T_TengXunWuFenXin);
            this.Controls.Add(this.T_TengXunShiFenXin);
            this.Controls.Add(this.T_chongqingshishicai);
            this.Controls.Add(this.T_XinJiangShiShiCai);
            this.Controls.Add(this.T_WuFenCai);
            this.Controls.Add(this.T_TengXunWuFen);
            this.Controls.Add(this.T_TengXunShiFen);
            this.Controls.Add(this.T_VRChongQingShiShiCai);
            this.Controls.Add(this.T_AoZhouCai);
            this.Controls.Add(this.tb_fuliratio);
            this.Controls.Add(this.tb_liushuiratio);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cb_TwoTreeNotSingle);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.tb_EndMinute);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_EndHour);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_StartMinute);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_StartHour);
            this.Controls.Add(this.lbl_during);
            this.Controls.Add(this.lbl_qqthread);
            this.Controls.Add(this.lbl_six);
            this.Controls.Add(this.cb_refreshball);
            this.Controls.Add(this.btn_bossreport);
            this.Controls.Add(this.btn_refreshyixin);
            this.Controls.Add(this.codeyixin);
            this.Controls.Add(this.codeweixin);
            this.Controls.Add(this.PicBarCode_yixin);
            this.Controls.Add(this.OpenBlack);
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
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StartForm_FormClosed);
            this.Load += new System.EventHandler(this.Start_Load);
            this.Shown += new System.EventHandler(this.StartForm_Shown);
            this.Botton_Status.ResumeLayout(false);
            this.Botton_Status.PerformLayout();
            this.TopMenu.ResumeLayout(false);
            this.TopMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBarCode_yixin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBarCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PicBarCode;
        private System.Windows.Forms.Label lbl_msg;
        private System.Windows.Forms.Button btn_resfresh;
        private System.Windows.Forms.Timer tm_refresh;
        private System.Windows.Forms.StatusStrip Botton_Status;
        private System.Windows.Forms.ToolStripStatusLabel SI_url;
        private System.Windows.Forms.Label lbl_waring;
        private System.Windows.Forms.ToolStripStatusLabel lbl_ShowError;
        private System.Windows.Forms.ToolStripStatusLabel SI_ShowError;
        private System.Windows.Forms.ToolStripMenuItem MI_Yonghu;
        private System.Windows.Forms.ToolStripMenuItem MI_UserSetting;
        private System.Windows.Forms.ToolStripMenuItem 新用户ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MI_ModifyUser;
        private System.Windows.Forms.ToolStripMenuItem MI_MyData;
        private System.Windows.Forms.ToolStripMenuItem MI_Ratio;
        private System.Windows.Forms.ToolStripMenuItem MI_Ratio_Setting;
        private System.Windows.Forms.ToolStripMenuItem MI_Bouns_Setting;
        private System.Windows.Forms.ToolStripMenuItem MI_GameLog;
        private System.Windows.Forms.ToolStripMenuItem MI_GameLogManulDeal;
        private System.Windows.Forms.ToolStripMenuItem MI_Bouns_Manul;
        private System.Windows.Forms.ToolStripMenuItem MI_Query;
        private System.Windows.Forms.ToolStripMenuItem MI_OpenQuery;
        private System.Windows.Forms.MenuStrip TopMenu;
        private System.Windows.Forms.Button OpenBlack;
        private System.Windows.Forms.PictureBox PicBarCode_yixin;
        private System.Windows.Forms.Label codeweixin;
        private System.Windows.Forms.Label codeyixin;
        private System.Windows.Forms.Button btn_refreshyixin;
        private System.Windows.Forms.Button btn_bossreport;
        private System.Windows.Forms.ToolStripMenuItem MI_PCWechatSend;
        private System.Windows.Forms.ToolStripMenuItem MI_PCWechatSendSetting;
        private System.Windows.Forms.ToolStripMenuItem mi_reminderquery;
        private System.Windows.Forms.ToolStripMenuItem MI_BallMatch;
        private System.Windows.Forms.ToolStripMenuItem MI_BallOpenManul;
        private System.Windows.Forms.ToolStripMenuItem MI_BallGames;
        private System.Windows.Forms.CheckBox cb_refreshball;
        private System.Windows.Forms.Label lbl_six;
        private System.Windows.Forms.Label lbl_qqthread;
        private System.Windows.Forms.Label lbl_during;
        private System.Windows.Forms.TextBox tb_StartHour;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_StartMinute;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_EndMinute;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_EndHour;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.CheckBox cb_TwoTreeNotSingle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_liushuiratio;
        private System.Windows.Forms.TextBox tb_fuliratio;
        private System.Windows.Forms.CheckBox T_AoZhouCai;
        private System.Windows.Forms.CheckBox T_VRChongQingShiShiCai;
        private System.Windows.Forms.CheckBox T_TengXunShiFen;
        private System.Windows.Forms.CheckBox T_TengXunWuFen;
        private System.Windows.Forms.CheckBox T_WuFenCai;
        private System.Windows.Forms.CheckBox T_XinJiangShiShiCai;
        private System.Windows.Forms.CheckBox T_chongqingshishicai;
        private System.Windows.Forms.CheckBox T_TengXunWuFenXin;
        private System.Windows.Forms.CheckBox T_TengXunShiFenXin;
        private System.Windows.Forms.CheckBox cb_robotstop;
        private System.Windows.Forms.CheckBox T_HeNeiWuFen;

    }
}

