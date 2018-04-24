namespace WeixinRoboot
{
    partial class F_Game_BasicRatio
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
            this.gv_Game_BasicRatio = new System.Windows.Forms.DataGridView();
            this.GameType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aspnet_userid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.含最小 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MinBuy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxBuy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.赔率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BS_Game_BasicRatio = new System.Windows.Forms.BindingSource(this.components);
            this.Btn_Save = new System.Windows.Forms.Button();
            this.ep_gridview = new System.Windows.Forms.ErrorProvider(this.components);
            this.lbl_class = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Game_BasicRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_Game_BasicRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ep_gridview)).BeginInit();
            this.SuspendLayout();
            // 
            // gv_Game_BasicRatio
            // 
            this.gv_Game_BasicRatio.AllowUserToOrderColumns = true;
            this.gv_Game_BasicRatio.AutoGenerateColumns = false;
            this.gv_Game_BasicRatio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_Game_BasicRatio.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GameType,
            this.aspnet_userid,
            this.BuyType,
            this.含最小,
            this.MinBuy,
            this.MaxBuy,
            this.BuyValue,
            this.赔率});
            this.gv_Game_BasicRatio.DataSource = this.BS_Game_BasicRatio;
            this.gv_Game_BasicRatio.Location = new System.Drawing.Point(12, 33);
            this.gv_Game_BasicRatio.MultiSelect = false;
            this.gv_Game_BasicRatio.Name = "gv_Game_BasicRatio";
            this.gv_Game_BasicRatio.RowHeadersVisible = false;
            this.gv_Game_BasicRatio.RowTemplate.Height = 23;
            this.gv_Game_BasicRatio.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv_Game_BasicRatio.Size = new System.Drawing.Size(666, 264);
            this.gv_Game_BasicRatio.TabIndex = 0;
            this.gv_Game_BasicRatio.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gv_Game_BasicRatio_DataError);
            this.gv_Game_BasicRatio.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.gv_Game_BasicRatio_RowsAdded);
            // 
            // GameType
            // 
            this.GameType.DataPropertyName = "GameType";
            this.GameType.HeaderText = "游戏";
            this.GameType.Name = "GameType";
            // 
            // aspnet_userid
            // 
            this.aspnet_userid.DataPropertyName = "aspnet_UserID";
            this.aspnet_userid.HeaderText = "aspnet_UserID";
            this.aspnet_userid.Name = "aspnet_userid";
            this.aspnet_userid.Visible = false;
            // 
            // BuyType
            // 
            this.BuyType.DataPropertyName = "BuyType";
            this.BuyType.HeaderText = "下注类";
            this.BuyType.Name = "BuyType";
            // 
            // 含最小
            // 
            this.含最小.DataPropertyName = "IncludeMin";
            this.含最小.HeaderText = "含最小";
            this.含最小.Name = "含最小";
            this.含最小.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.含最小.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // MinBuy
            // 
            this.MinBuy.DataPropertyName = "MinBuy";
            this.MinBuy.HeaderText = "最小";
            this.MinBuy.Name = "MinBuy";
            this.MinBuy.Width = 60;
            // 
            // MaxBuy
            // 
            this.MaxBuy.DataPropertyName = "MaxBuy";
            this.MaxBuy.HeaderText = "最大";
            this.MaxBuy.Name = "MaxBuy";
            this.MaxBuy.Width = 60;
            // 
            // BuyValue
            // 
            this.BuyValue.DataPropertyName = "BuyValue";
            this.BuyValue.HeaderText = "下注值";
            this.BuyValue.Name = "BuyValue";
            // 
            // 赔率
            // 
            this.赔率.DataPropertyName = "BasicRatio";
            this.赔率.HeaderText = "赔率";
            this.赔率.Name = "赔率";
            // 
            // BS_Game_BasicRatio
            // 
            this.BS_Game_BasicRatio.Sort = "OrderIndex";
            // 
            // Btn_Save
            // 
            this.Btn_Save.Location = new System.Drawing.Point(12, 303);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(75, 23);
            this.Btn_Save.TabIndex = 1;
            this.Btn_Save.Text = "保存";
            this.Btn_Save.UseVisualStyleBackColor = true;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // ep_gridview
            // 
            this.ep_gridview.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ep_gridview.ContainerControl = this;
            // 
            // lbl_class
            // 
            this.lbl_class.AutoSize = true;
            this.lbl_class.Location = new System.Drawing.Point(12, 9);
            this.lbl_class.Name = "lbl_class";
            this.lbl_class.Size = new System.Drawing.Size(35, 12);
            this.lbl_class.TabIndex = 2;
            this.lbl_class.Text = "分类:";
            // 
            // F_Game_BasicRatio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 338);
            this.Controls.Add(this.lbl_class);
            this.Controls.Add(this.Btn_Save);
            this.Controls.Add(this.gv_Game_BasicRatio);
            this.Name = "F_Game_BasicRatio";
            this.Text = "基本赔率";
            this.Load += new System.EventHandler(this.F_Game_BasicRatio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gv_Game_BasicRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_Game_BasicRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ep_gridview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gv_Game_BasicRatio;
        private System.Windows.Forms.BindingSource BS_Game_BasicRatio;
        private System.Windows.Forms.Button Btn_Save;
        private System.Windows.Forms.ErrorProvider ep_gridview;
        private System.Windows.Forms.Label lbl_class;
        private System.Windows.Forms.DataGridViewTextBoxColumn GameType;
        private System.Windows.Forms.DataGridViewTextBoxColumn aspnet_userid;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 含最小;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinBuy;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxBuy;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn 赔率;
    }
}