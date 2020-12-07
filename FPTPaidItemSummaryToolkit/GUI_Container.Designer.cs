namespace FPTPaidItemSummaryToolkit
{
    partial class GUI_Container
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
            this.mnAll = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mniSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mniManage = new System.Windows.Forms.ToolStripMenuItem();
            this.managePaidItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnsAcademic = new System.Windows.Forms.ToolStripMenuItem();
            this.UserInfoManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.mniPension = new System.Windows.Forms.ToolStripMenuItem();
            this.mniHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.mnAll.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnAll
            // 
            this.mnAll.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mniManage});
            this.mnAll.Location = new System.Drawing.Point(0, 0);
            this.mnAll.Name = "mnAll";
            this.mnAll.Size = new System.Drawing.Size(885, 24);
            this.mnAll.TabIndex = 3;
            this.mnAll.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniSummary,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // mniSummary
            // 
            this.mniSummary.Name = "mniSummary";
            this.mniSummary.Size = new System.Drawing.Size(180, 22);
            this.mniSummary.Text = "Tổng hợp file ";
            this.mniSummary.Click += new System.EventHandler(this.MniSummary_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeToolStripMenuItem.Text = "Đóng";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // mniManage
            // 
            this.mniManage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.managePaidItemsToolStripMenuItem,
            this.mnsAcademic,
            this.mniPension,
            this.UserInfoManagement,
            this.mniHistory});
            this.mniManage.Name = "mniManage";
            this.mniManage.Size = new System.Drawing.Size(60, 20);
            this.mniManage.Text = "Quản lý";
            // 
            // managePaidItemsToolStripMenuItem
            // 
            this.managePaidItemsToolStripMenuItem.Name = "managePaidItemsToolStripMenuItem";
            this.managePaidItemsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.managePaidItemsToolStripMenuItem.Text = "Định mức thù lao";
            this.managePaidItemsToolStripMenuItem.Click += new System.EventHandler(this.ManagePaidItemsToolStripMenuItem_Click);
            // 
            // mnsAcademic
            // 
            this.mnsAcademic.Name = "mnsAcademic";
            this.mnsAcademic.Size = new System.Drawing.Size(196, 22);
            this.mnsAcademic.Text = "Hệ đào tạo";
            this.mnsAcademic.Click += new System.EventHandler(this.MnsAcademic_Click);
            // 
            // UserInfoManagement
            // 
            this.UserInfoManagement.Name = "UserInfoManagement";
            this.UserInfoManagement.Size = new System.Drawing.Size(196, 22);
            this.UserInfoManagement.Text = "Thông tin người dùng  ";
            this.UserInfoManagement.Click += new System.EventHandler(this.UserInfoManagement_Click);
            // 
            // mniPension
            // 
            this.mniPension.Name = "mniPension";
            this.mniPension.Size = new System.Drawing.Size(196, 22);
            this.mniPension.Text = "Phụ cấp";
            this.mniPension.Click += new System.EventHandler(this.mniPension_Click);
            // 
            // mniHistory
            // 
            this.mniHistory.Name = "mniHistory";
            this.mniHistory.Size = new System.Drawing.Size(196, 22);
            this.mniHistory.Text = "Lịch sử";
            this.mniHistory.Click += new System.EventHandler(this.mniHistory_Click);
            // 
            // GUI_Container
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 485);
            this.Controls.Add(this.mnAll);
            this.IsMdiContainer = true;
            this.Name = "GUI_Container";
            this.Text = "FPT Paid Item Summary Toolkit";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.mnAll.ResumeLayout(false);
            this.mnAll.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnAll;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mniSummary;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mniManage;
        private System.Windows.Forms.ToolStripMenuItem managePaidItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnsAcademic;
        private System.Windows.Forms.ToolStripMenuItem UserInfoManagement;
        private System.Windows.Forms.ToolStripMenuItem mniPension;
        private System.Windows.Forms.ToolStripMenuItem mniHistory;
    }
}

