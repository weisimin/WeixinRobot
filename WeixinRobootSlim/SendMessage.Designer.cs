namespace WeixinRobootSlim
{
    partial class SendMessage
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
            this.wb_messages = new System.Windows.Forms.WebBrowser();
            this.wb_messages.ScriptErrorsSuppressed = true;
            this.Btn_Send = new System.Windows.Forms.Button();
            this.Btn_SendImage = new System.Windows.Forms.Button();
            this.tb_MessageContent = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // wb_messages
            // 
            this.wb_messages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wb_messages.Location = new System.Drawing.Point(13, 26);
            this.wb_messages.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb_messages.Name = "wb_messages";
            this.wb_messages.Size = new System.Drawing.Size(544, 218);
            this.wb_messages.TabIndex = 0;
            // 
            // Btn_Send
            // 
            this.Btn_Send.Location = new System.Drawing.Point(482, 251);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(75, 56);
            this.Btn_Send.TabIndex = 1;
            this.Btn_Send.Text = "发送";
            this.Btn_Send.UseVisualStyleBackColor = true;
            this.Btn_Send.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // Btn_SendImage
            // 
            this.Btn_SendImage.Location = new System.Drawing.Point(13, 251);
            this.Btn_SendImage.Name = "Btn_SendImage";
            this.Btn_SendImage.Size = new System.Drawing.Size(75, 56);
            this.Btn_SendImage.TabIndex = 2;
            this.Btn_SendImage.Text = "发送图片";
            this.Btn_SendImage.UseVisualStyleBackColor = true;
            this.Btn_SendImage.Click += new System.EventHandler(this.Btn_SendImage_Click);
            // 
            // tb_MessageContent
            // 
            this.tb_MessageContent.Location = new System.Drawing.Point(94, 251);
            this.tb_MessageContent.Multiline = true;
            this.tb_MessageContent.Name = "tb_MessageContent";
            this.tb_MessageContent.Size = new System.Drawing.Size(382, 56);
            this.tb_MessageContent.TabIndex = 3;
            // 
            // SendMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 319);
            this.Controls.Add(this.tb_MessageContent);
            this.Controls.Add(this.Btn_SendImage);
            this.Controls.Add(this.Btn_Send);
            this.Controls.Add(this.wb_messages);
            this.Name = "SendMessage";
            this.Text = "发送消息";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser wb_messages;
        private System.Windows.Forms.Button Btn_Send;
        private System.Windows.Forms.Button Btn_SendImage;
        private System.Windows.Forms.TextBox tb_MessageContent;
    }
}