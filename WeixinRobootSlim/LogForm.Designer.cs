namespace WeixinRobootSlim
{
    partial class LogForm
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
            this.tb_log = new System.Windows.Forms.TextBox();
            this.tm_refresh = new System.Windows.Forms.Timer(this.components);
            this.btn_refresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_log
            // 
            this.tb_log.BackColor = System.Drawing.Color.Black;
            this.tb_log.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_log.ForeColor = System.Drawing.SystemColors.Window;
            this.tb_log.Location = new System.Drawing.Point(13, 13);
            this.tb_log.Multiline = true;
            this.tb_log.Name = "tb_log";
            this.tb_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_log.Size = new System.Drawing.Size(826, 237);
            this.tb_log.TabIndex = 0;
            // 
            // tm_refresh
            // 
            this.tm_refresh.Tick += new System.EventHandler(this.tm_refresh_Tick);
            // 
            // btn_refresh
            // 
            this.btn_refresh.Location = new System.Drawing.Point(24, 275);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(75, 23);
            this.btn_refresh.TabIndex = 1;
            this.btn_refresh.Text = "刷新";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 316);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.tb_log);
            this.DoubleBuffered = true;
            this.Name = "LogForm";
            this.Text = "LogForm";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.LogForm_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_log;
        private System.Windows.Forms.Timer tm_refresh;
        private System.Windows.Forms.Button btn_refresh;
    }
}