using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FPTPaidItemSummaryToolkit
{
    public partial class GUI_FillKey : Form
    {
        public string Key { get; set; }
        string inputKey;
        public GUI_FillKey()
        {
            InitializeComponent();
        }

        public GUI_FillKey(string mpirKey)
        {
            InitializeComponent();
            inputKey = mpirKey;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKey.Text))
            {
                errorProvider1.SetError(txtKey, "Không để trống trường này");
                errorProvider1.SetIconAlignment(txtKey, ErrorIconAlignment.MiddleRight);
                errorProvider1.SetIconPadding(txtKey, -20);
                txtKey.Focus();
                return;
            }
            if (inputKey is object)
            {
                this.Key = txtKey.Text;
                if (Key.Equals(inputKey))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sai mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                this.Key = txtKey.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
             this.Close();
        }

        private void btnUnhide_MouseDown(object sender, MouseEventArgs e)
        {
            txtKey.PasswordChar = '\0';
        }

        private void btnUnhide_MouseUp(object sender, MouseEventArgs e)
        {
            txtKey.PasswordChar = '*';
        }
    }
}
