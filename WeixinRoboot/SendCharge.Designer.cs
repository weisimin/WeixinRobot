namespace WeixinRoboot
{
    partial class SendCharge
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
            this.GV_ChangeLog = new System.Windows.Forms.DataGridView();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemarkType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChangePoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BS_TransLog = new System.Windows.Forms.BindingSource(this.components);
            this.lbl_User = new System.Windows.Forms.Label();
            this.ep_sql = new System.Windows.Forms.ErrorProvider(this.components);
            this.Btn_Send = new System.Windows.Forms.Button();
            this.tb_ChargeMoney = new System.Windows.Forms.TextBox();
            this.lbl_ChargeMoney = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GV_ChangeLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_TransLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ep_sql)).BeginInit();
            this.SuspendLayout();
            // 
            // GV_ChangeLog
            // 
            this.GV_ChangeLog.AllowUserToAddRows = false;
            this.GV_ChangeLog.AllowUserToDeleteRows = false;
            this.GV_ChangeLog.AllowUserToOrderColumns = true;
            this.GV_ChangeLog.AutoGenerateColumns = false;
            this.GV_ChangeLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GV_ChangeLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserName,
            this.RemarkType,
            this.ChangePoint,
            this.Remark});
            this.GV_ChangeLog.DataSource = this.BS_TransLog;
            this.GV_ChangeLog.Location = new System.Drawing.Point(13, 37);
            this.GV_ChangeLog.Name = "GV_ChangeLog";
            this.GV_ChangeLog.ReadOnly = true;
            this.GV_ChangeLog.RowHeadersVisible = false;
            this.GV_ChangeLog.RowTemplate.Height = 23;
            this.GV_ChangeLog.Size = new System.Drawing.Size(544, 208);
            this.GV_ChangeLog.TabIndex = 2;
            // 
            // UserName
            // 
            this.UserName.DataPropertyName = "UserName";
            this.UserName.HeaderText = "用户";
            this.UserName.Name = "UserName";
            this.UserName.ReadOnly = true;
            // 
            // RemarkType
            // 
            this.RemarkType.DataPropertyName = "RemarkType";
            this.RemarkType.HeaderText = "类别";
            this.RemarkType.Name = "RemarkType";
            this.RemarkType.ReadOnly = true;
            // 
            // ChangePoint
            // 
            this.ChangePoint.DataPropertyName = "ChangePoint";
            this.ChangePoint.HeaderText = "变更点数";
            this.ChangePoint.Name = "ChangePoint";
            this.ChangePoint.ReadOnly = true;
            // 
            // Remark
            // 
            this.Remark.DataPropertyName = "Remark";
            this.Remark.HeaderText = "备注";
            this.Remark.Name = "Remark";
            this.Remark.ReadOnly = true;
            // 
            // lbl_User
            // 
            this.lbl_User.AutoSize = true;
            this.lbl_User.Location = new System.Drawing.Point(13, 16);
            this.lbl_User.Name = "lbl_User";
            this.lbl_User.Size = new System.Drawing.Size(35, 12);
            this.lbl_User.TabIndex = 4;
            this.lbl_User.Text = "用户:";
            // 
            // ep_sql
            // 
            this.ep_sql.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ep_sql.ContainerControl = this;
            // 
            // Btn_Send
            // 
            this.Btn_Send.Location = new System.Drawing.Point(506, 260);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(51, 42);
            this.Btn_Send.TabIndex = 11;
            this.Btn_Send.Text = "保存";
            this.Btn_Send.UseVisualStyleBackColor = true;
            this.Btn_Send.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // tb_ChargeMoney
            // 
            this.tb_ChargeMoney.Location = new System.Drawing.Point(79, 272);
            this.tb_ChargeMoney.Name = "tb_ChargeMoney";
            this.tb_ChargeMoney.Size = new System.Drawing.Size(236, 21);
            this.tb_ChargeMoney.TabIndex = 10;
            // 
            // lbl_ChargeMoney
            // 
            this.lbl_ChargeMoney.AutoSize = true;
            this.lbl_ChargeMoney.Location = new System.Drawing.Point(14, 275);
            this.lbl_ChargeMoney.Name = "lbl_ChargeMoney";
            this.lbl_ChargeMoney.Size = new System.Drawing.Size(59, 12);
            this.lbl_ChargeMoney.TabIndex = 9;
            this.lbl_ChargeMoney.Text = "充值金额:";
            // 
            // SendCharge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 338);
            this.Controls.Add(this.Btn_Send);
            this.Controls.Add(this.tb_ChargeMoney);
            this.Controls.Add(this.lbl_ChargeMoney);
            this.Controls.Add(this.lbl_User);
            this.Controls.Add(this.GV_ChangeLog);
            this.Name = "SendCharge";
            this.Text = "充值";
            this.Load += new System.EventHandler(this.SendCharge_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GV_ChangeLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_TransLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ep_sql)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView GV_ChangeLog;
        private System.Windows.Forms.BindingSource BS_TransLog;
        private System.Windows.Forms.Label lbl_User;
        private System.Windows.Forms.ErrorProvider ep_sql;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemarkType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangePoint;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark;
        private System.Windows.Forms.Button Btn_Send;
        private System.Windows.Forms.TextBox tb_ChargeMoney;
        private System.Windows.Forms.Label lbl_ChargeMoney;
    }
}