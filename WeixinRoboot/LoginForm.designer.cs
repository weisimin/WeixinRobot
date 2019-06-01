namespace WeixinRoboot
{
    public partial class LoginForm
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
            this.lbl_UserName = new System.Windows.Forms.Label();
            this.lbl_Pwd = new System.Windows.Forms.Label();
            this.tb_UserName = new System.Windows.Forms.TextBox();
            this.tb_pwd = new System.Windows.Forms.TextBox();
            this.btn_Login = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.lbl_data = new System.Windows.Forms.Label();
            this.cb_datasource = new System.Windows.Forms.ComboBox();
            this.CB_GameType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbl_UserName
            // 
            this.lbl_UserName.AutoSize = true;
            this.lbl_UserName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_UserName.Location = new System.Drawing.Point(74, 46);
            this.lbl_UserName.Name = "lbl_UserName";
            this.lbl_UserName.Size = new System.Drawing.Size(74, 21);
            this.lbl_UserName.TabIndex = 0;
            this.lbl_UserName.Text = "会员号：";
            // 
            // lbl_Pwd
            // 
            this.lbl_Pwd.AutoSize = true;
            this.lbl_Pwd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Pwd.Location = new System.Drawing.Point(74, 85);
            this.lbl_Pwd.Name = "lbl_Pwd";
            this.lbl_Pwd.Size = new System.Drawing.Size(58, 21);
            this.lbl_Pwd.TabIndex = 1;
            this.lbl_Pwd.Text = "密码：";
            // 
            // tb_UserName
            // 
            this.tb_UserName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_UserName.Location = new System.Drawing.Point(172, 42);
            this.tb_UserName.Name = "tb_UserName";
            this.tb_UserName.Size = new System.Drawing.Size(157, 29);
            this.tb_UserName.TabIndex = 2;
            // 
            // tb_pwd
            // 
            this.tb_pwd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_pwd.Location = new System.Drawing.Point(172, 81);
            this.tb_pwd.Name = "tb_pwd";
            this.tb_pwd.PasswordChar = '*';
            this.tb_pwd.Size = new System.Drawing.Size(157, 29);
            this.tb_pwd.TabIndex = 3;
            // 
            // btn_Login
            // 
            this.btn_Login.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Login.Location = new System.Drawing.Point(78, 174);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(80, 28);
            this.btn_Login.TabIndex = 4;
            this.btn_Login.Text = "登录";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(249, 174);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(80, 28);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // lbl_data
            // 
            this.lbl_data.AutoSize = true;
            this.lbl_data.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_data.Location = new System.Drawing.Point(74, 134);
            this.lbl_data.Name = "lbl_data";
            this.lbl_data.Size = new System.Drawing.Size(74, 21);
            this.lbl_data.TabIndex = 6;
            this.lbl_data.Text = "数据源：";
            // 
            // cb_datasource
            // 
            this.cb_datasource.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_datasource.FormattingEnabled = true;
            this.cb_datasource.Items.AddRange(new object[] {
            "远程服务器",
            "本机",
            "迷你本机"});
            this.cb_datasource.Location = new System.Drawing.Point(172, 134);
            this.cb_datasource.Name = "cb_datasource";
            this.cb_datasource.Size = new System.Drawing.Size(157, 29);
            this.cb_datasource.TabIndex = 7;
            this.cb_datasource.SelectedIndexChanged += new System.EventHandler(this.cb_datasource_SelectedIndexChanged);
            this.cb_datasource.SelectedValueChanged += new System.EventHandler(this.cb_datasource_SelectedValueChanged);
            // 
            // CB_GameType
            // 
            this.CB_GameType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CB_GameType.FormattingEnabled = true;
            this.CB_GameType.Items.AddRange(new object[] {
            "澳彩",
            "重庆",
            "腾十",
            "腾十信",
            "腾五",
            "腾五信",
            "五分",
            "新疆",
            "河内",
            "VR"});
            this.CB_GameType.Location = new System.Drawing.Point(336, 134);
            this.CB_GameType.Name = "CB_GameType";
            this.CB_GameType.Size = new System.Drawing.Size(83, 25);
            this.CB_GameType.TabIndex = 8;
            this.CB_GameType.Visible = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 228);
            this.Controls.Add(this.CB_GameType);
            this.Controls.Add(this.cb_datasource);
            this.Controls.Add(this.lbl_data);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Login);
            this.Controls.Add(this.tb_pwd);
            this.Controls.Add(this.tb_UserName);
            this.Controls.Add(this.lbl_Pwd);
            this.Controls.Add(this.lbl_UserName);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_UserName;
        private System.Windows.Forms.Label lbl_Pwd;
        private System.Windows.Forms.TextBox tb_UserName;
        private System.Windows.Forms.TextBox tb_pwd;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label lbl_data;
        private System.Windows.Forms.ComboBox cb_datasource;
        private System.Windows.Forms.ComboBox CB_GameType;
    }
}