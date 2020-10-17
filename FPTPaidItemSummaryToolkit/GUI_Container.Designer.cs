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
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mniManage = new System.Windows.Forms.ToolStripMenuItem();
            this.managePaidItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnsAcademic = new System.Windows.Forms.ToolStripMenuItem();
            this.summaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.loadToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.exportToolStripMenuItem.Text = "Xuất ra file";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeToolStripMenuItem.Text = "Đóng";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // mniManage
            // 
            this.mniManage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.managePaidItemsToolStripMenuItem,
            this.mnsAcademic,
            this.summaryToolStripMenuItem});
            this.mniManage.Name = "mniManage";
            this.mniManage.Size = new System.Drawing.Size(60, 20);
            this.mniManage.Text = "Quản lý";
            // 
            // managePaidItemsToolStripMenuItem
            // 
            this.managePaidItemsToolStripMenuItem.Name = "managePaidItemsToolStripMenuItem";
            this.managePaidItemsToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.managePaidItemsToolStripMenuItem.Text = "Quản lý định mức thù lao ( paid items )";
            this.managePaidItemsToolStripMenuItem.Click += new System.EventHandler(this.managePaidItemsToolStripMenuItem_Click);
            // 
            // mnsAcademic
            // 
            this.mnsAcademic.Name = "mnsAcademic";
            this.mnsAcademic.Size = new System.Drawing.Size(281, 22);
            this.mnsAcademic.Text = "Hệ đào tạo";
            this.mnsAcademic.Click += new System.EventHandler(this.mnsAcademic_Click);
            // 
            // summaryToolStripMenuItem
            // 
            this.summaryToolStripMenuItem.Name = "summaryToolStripMenuItem";
            this.summaryToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.summaryToolStripMenuItem.Text = "Tổng hợp";
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
            this.mnAll.ResumeLayout(false);
            this.mnAll.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnAll;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mniManage;
        private System.Windows.Forms.ToolStripMenuItem managePaidItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnsAcademic;
        private System.Windows.Forms.ToolStripMenuItem summaryToolStripMenuItem;
    }
}

