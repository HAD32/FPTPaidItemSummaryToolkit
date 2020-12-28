namespace FPTPaidItemSummaryToolkit
{
    partial class GUI_MailSelection
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.btnChangeInfor = new System.Windows.Forms.Button();
            this.btnSearchEmail = new System.Windows.Forms.Button();
            this.txtSearchContent = new System.Windows.Forms.TextBox();
            this.checkBoxAllEmail = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMail = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkReceiverList = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(334, 89);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(526, 374);
            this.webBrowser1.TabIndex = 24;
            // 
            // btnChangeInfor
            // 
            this.btnChangeInfor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeInfor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeInfor.Location = new System.Drawing.Point(595, 47);
            this.btnChangeInfor.Name = "btnChangeInfor";
            this.btnChangeInfor.Size = new System.Drawing.Size(253, 27);
            this.btnChangeInfor.TabIndex = 27;
            this.btnChangeInfor.Text = "Thay đổi thông tin hỗ trợ và thời hạn";
            this.btnChangeInfor.UseVisualStyleBackColor = true;
            this.btnChangeInfor.Click += new System.EventHandler(this.btnChangeInfor_Click);
            // 
            // btnSearchEmail
            // 
            this.btnSearchEmail.Location = new System.Drawing.Point(272, 62);
            this.btnSearchEmail.Name = "btnSearchEmail";
            this.btnSearchEmail.Size = new System.Drawing.Size(58, 23);
            this.btnSearchEmail.TabIndex = 26;
            this.btnSearchEmail.Text = "Search";
            this.btnSearchEmail.UseVisualStyleBackColor = true;
            this.btnSearchEmail.Click += new System.EventHandler(this.btnSearchEmail_Click);
            // 
            // txtSearchContent
            // 
            this.txtSearchContent.Location = new System.Drawing.Point(141, 64);
            this.txtSearchContent.Name = "txtSearchContent";
            this.txtSearchContent.Size = new System.Drawing.Size(125, 20);
            this.txtSearchContent.TabIndex = 25;
            // 
            // checkBoxAllEmail
            // 
            this.checkBoxAllEmail.AutoSize = true;
            this.checkBoxAllEmail.Location = new System.Drawing.Point(50, 66);
            this.checkBoxAllEmail.Name = "checkBoxAllEmail";
            this.checkBoxAllEmail.Size = new System.Drawing.Size(81, 17);
            this.checkBoxAllEmail.TabIndex = 23;
            this.checkBoxAllEmail.Text = "Chọn tất cả";
            this.checkBoxAllEmail.UseVisualStyleBackColor = true;
            this.checkBoxAllEmail.CheckedChanged += new System.EventHandler(this.checkBoxAllEmail_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(789, 469);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 37);
            this.btnClose.TabIndex = 22;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMail
            // 
            this.btnMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMail.Location = new System.Drawing.Point(697, 469);
            this.btnMail.Name = "btnMail";
            this.btnMail.Size = new System.Drawing.Size(75, 37);
            this.btnMail.TabIndex = 21;
            this.btnMail.Text = "Gửi";
            this.btnMail.UseVisualStyleBackColor = true;
            this.btnMail.Click += new System.EventHandler(this.btnMail_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(47, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 17);
            this.label3.TabIndex = 20;
            this.label3.Text = "Người nhận:";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblEmail.Location = new System.Drawing.Point(138, 16);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(52, 17);
            this.lblEmail.TabIndex = 19;
            this.lblEmail.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(47, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 18;
            this.label2.Text = "Người gửi:";
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(129, 469);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(35, 13);
            this.lblCount.TabIndex = 17;
            this.lblCount.Text = "label2";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 469);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Số người nhận:";
            // 
            // chkReceiverList
            // 
            this.chkReceiverList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chkReceiverList.FormattingEnabled = true;
            this.chkReceiverList.Location = new System.Drawing.Point(47, 89);
            this.chkReceiverList.Name = "chkReceiverList";
            this.chkReceiverList.Size = new System.Drawing.Size(281, 379);
            this.chkReceiverList.TabIndex = 15;
            this.chkReceiverList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkEmailList_ItemCheck);
            this.chkReceiverList.SelectedIndexChanged += new System.EventHandler(this.chkReceiverList_SelectedIndexChanged);
            // 
            // GUI_MailSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 521);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.btnChangeInfor);
            this.Controls.Add(this.btnSearchEmail);
            this.Controls.Add(this.txtSearchContent);
            this.Controls.Add(this.checkBoxAllEmail);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnMail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkReceiverList);
            this.Name = "GUI_MailSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gửi mail xác nhận";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button btnChangeInfor;
        private System.Windows.Forms.Button btnSearchEmail;
        private System.Windows.Forms.TextBox txtSearchContent;
        private System.Windows.Forms.CheckBox checkBoxAllEmail;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox chkReceiverList;
    }
}