namespace WeixinRoboot
{
    partial class F_WX_BounsRatio
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
            this.GV_DATA = new System.Windows.Forms.DataGridView();
            this.BS_GV_DATA = new System.Windows.Forms.BindingSource(this.components);
            this.BtnSave = new System.Windows.Forms.Button();
            this.aspnet_UserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RowNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartBuyPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndBuyPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartBuyAverage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndBuyAverage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FixNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FlowPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IfDivousPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GV_DATA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_GV_DATA)).BeginInit();
            this.SuspendLayout();
            // 
            // GV_DATA
            // 
            this.GV_DATA.AllowUserToAddRows = false;
            this.GV_DATA.AllowUserToDeleteRows = false;
            this.GV_DATA.AutoGenerateColumns = false;
            this.GV_DATA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GV_DATA.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.aspnet_UserID,
            this.RowNumber,
            this.StartBuyPeriod,
            this.EndBuyPeriod,
            this.StartBuyAverage,
            this.EndBuyAverage,
            this.FixNumber,
            this.FlowPercent,
            this.IfDivousPercent});
            this.GV_DATA.DataSource = this.BS_GV_DATA;
            this.GV_DATA.Location = new System.Drawing.Point(13, 13);
            this.GV_DATA.Name = "GV_DATA";
            this.GV_DATA.RowHeadersVisible = false;
            this.GV_DATA.RowTemplate.Height = 23;
            this.GV_DATA.Size = new System.Drawing.Size(676, 294);
            this.GV_DATA.TabIndex = 0;
            this.GV_DATA.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.GV_DATA_DataError);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(13, 314);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 23);
            this.BtnSave.TabIndex = 1;
            this.BtnSave.Text = "保存";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // aspnet_UserID
            // 
            this.aspnet_UserID.DataPropertyName = "aspnet_UserID";
            this.aspnet_UserID.HeaderText = "用户ID";
            this.aspnet_UserID.Name = "aspnet_UserID";
            this.aspnet_UserID.ReadOnly = true;
            this.aspnet_UserID.Visible = false;
            // 
            // RowNumber
            // 
            this.RowNumber.DataPropertyName = "RowNumber";
            this.RowNumber.HeaderText = "序号";
            this.RowNumber.Name = "RowNumber";
            this.RowNumber.ReadOnly = true;
            this.RowNumber.Width = 60;
            // 
            // StartBuyPeriod
            // 
            this.StartBuyPeriod.DataPropertyName = "StartBuyPeriod";
            this.StartBuyPeriod.HeaderText = "期数开始";
            this.StartBuyPeriod.Name = "StartBuyPeriod";
            this.StartBuyPeriod.Width = 90;
            // 
            // EndBuyPeriod
            // 
            this.EndBuyPeriod.DataPropertyName = "EndBuyPeriod";
            this.EndBuyPeriod.HeaderText = "期数结束";
            this.EndBuyPeriod.Name = "EndBuyPeriod";
            this.EndBuyPeriod.Width = 90;
            // 
            // StartBuyAverage
            // 
            this.StartBuyAverage.DataPropertyName = "StartBuyAverage";
            this.StartBuyAverage.HeaderText = "平均开始";
            this.StartBuyAverage.Name = "StartBuyAverage";
            this.StartBuyAverage.Width = 90;
            // 
            // EndBuyAverage
            // 
            this.EndBuyAverage.DataPropertyName = "EndBuyAverage";
            this.EndBuyAverage.HeaderText = "平均结束";
            this.EndBuyAverage.Name = "EndBuyAverage";
            this.EndBuyAverage.Width = 90;
            // 
            // FixNumber
            // 
            this.FixNumber.DataPropertyName = "FixNumber";
            this.FixNumber.HeaderText = "固定值";
            this.FixNumber.Name = "FixNumber";
            this.FixNumber.Width = 90;
            // 
            // FlowPercent
            // 
            this.FlowPercent.DataPropertyName = "FlowPercent";
            this.FlowPercent.HeaderText = "流水返";
            this.FlowPercent.Name = "FlowPercent";
            this.FlowPercent.Width = 90;
            // 
            // IfDivousPercent
            // 
            this.IfDivousPercent.DataPropertyName = "IfDivousPercent";
            this.IfDivousPercent.HeaderText = "负数返";
            this.IfDivousPercent.Name = "IfDivousPercent";
            this.IfDivousPercent.Width = 90;
            // 
            // F_WX_BounsRatio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 338);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.GV_DATA);
            this.Name = "F_WX_BounsRatio";
            this.Text = "福利设置";
            this.Load += new System.EventHandler(this.F_WX_BounsRatio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GV_DATA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_GV_DATA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GV_DATA;
        private System.Windows.Forms.BindingSource BS_GV_DATA;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn aspnet_UserID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartBuyPeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndBuyPeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartBuyAverage;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndBuyAverage;
        private System.Windows.Forms.DataGridViewTextBoxColumn FixNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn FlowPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn IfDivousPercent;
    }
}