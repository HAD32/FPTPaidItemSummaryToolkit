namespace FPTPaidItemSummaryToolkit
{
    partial class GUI_FinalSummary
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
            this.dtgDisplay = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetDetail = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.rdbAccount = new System.Windows.Forms.RadioButton();
            this.rdbName = new System.Windows.Forms.RadioButton();
            this.btnMail = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgDisplay
            // 
            this.dtgDisplay.AllowUserToAddRows = false;
            this.dtgDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgDisplay.Location = new System.Drawing.Point(29, 99);
            this.dtgDisplay.Name = "dtgDisplay";
            this.dtgDisplay.ReadOnly = true;
            this.dtgDisplay.Size = new System.Drawing.Size(970, 344);
            this.dtgDisplay.TabIndex = 0;
            this.dtgDisplay.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgDisplay_CellEnter);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(916, 449);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(83, 41);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(29, 449);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(123, 41);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Xuất ra file excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(280, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(414, 37);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tổng hợp thù lao giảng viên";
            // 
            // btnGetDetail
            // 
            this.btnGetDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetDetail.Location = new System.Drawing.Point(158, 449);
            this.btnGetDetail.Name = "btnGetDetail";
            this.btnGetDetail.Size = new System.Drawing.Size(82, 41);
            this.btnGetDetail.TabIndex = 4;
            this.btnGetDetail.Text = "Chi tiết";
            this.btnGetDetail.UseVisualStyleBackColor = true;
            this.btnGetDetail.Click += new System.EventHandler(this.btnGetDetail_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tìm kiếm";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(92, 77);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(178, 20);
            this.txtSearch.TabIndex = 6;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::FPTPaidItemSummaryToolkit.Properties.Resources.searchIcon;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(276, 77);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(22, 21);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // rdbAccount
            // 
            this.rdbAccount.AutoSize = true;
            this.rdbAccount.Checked = true;
            this.rdbAccount.Location = new System.Drawing.Point(319, 79);
            this.rdbAccount.Name = "rdbAccount";
            this.rdbAccount.Size = new System.Drawing.Size(65, 17);
            this.rdbAccount.TabIndex = 8;
            this.rdbAccount.TabStop = true;
            this.rdbAccount.Text = "Account";
            this.rdbAccount.UseVisualStyleBackColor = true;
            // 
            // rdbName
            // 
            this.rdbName.AutoSize = true;
            this.rdbName.Location = new System.Drawing.Point(390, 79);
            this.rdbName.Name = "rdbName";
            this.rdbName.Size = new System.Drawing.Size(72, 17);
            this.rdbName.TabIndex = 9;
            this.rdbName.Text = "Họ và tên";
            this.rdbName.UseVisualStyleBackColor = true;
            // 
            // btnMail
            // 
            this.btnMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMail.Location = new System.Drawing.Point(246, 449);
            this.btnMail.Name = "btnMail";
            this.btnMail.Size = new System.Drawing.Size(138, 41);
            this.btnMail.TabIndex = 10;
            this.btnMail.Text = "Gửi mail xác nhận";
            this.btnMail.UseVisualStyleBackColor = true;
            this.btnMail.Click += new System.EventHandler(this.btnMail_Click);
            // 
            // GUI_FinalSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 507);
            this.Controls.Add(this.btnMail);
            this.Controls.Add(this.rdbName);
            this.Controls.Add(this.rdbAccount);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGetDetail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dtgDisplay);
            this.Name = "GUI_FinalSummary";
            this.Text = "Tổng hợp ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dtgDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgDisplay;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetDetail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.RadioButton rdbAccount;
        private System.Windows.Forms.RadioButton rdbName;
        private System.Windows.Forms.Button btnMail;
    }
}