namespace WeixinRobootSlim
{
    partial class UpdateActiveCode
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
            this.fd_activecode = new System.Windows.Forms.TextBox();
            this.lbl_activecode = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fd_activecode
            // 
            this.fd_activecode.Location = new System.Drawing.Point(12, 42);
            this.fd_activecode.Multiline = true;
            this.fd_activecode.Name = "fd_activecode";
            this.fd_activecode.Size = new System.Drawing.Size(283, 154);
            this.fd_activecode.TabIndex = 0;
            // 
            // lbl_activecode
            // 
            this.lbl_activecode.AutoSize = true;
            this.lbl_activecode.Location = new System.Drawing.Point(12, 27);
            this.lbl_activecode.Name = "lbl_activecode";
            this.lbl_activecode.Size = new System.Drawing.Size(41, 12);
            this.lbl_activecode.TabIndex = 1;
            this.lbl_activecode.Text = "激活码";
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(14, 217);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 2;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // UpdateActiveCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 270);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.lbl_activecode);
            this.Controls.Add(this.fd_activecode);
            this.Name = "UpdateActiveCode";
            this.Text = "更新激活码";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UpdateActiveCode_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox fd_activecode;
        private System.Windows.Forms.Label lbl_activecode;
        private System.Windows.Forms.Button btn_Save;
    }
}