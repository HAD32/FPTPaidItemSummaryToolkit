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
        public GUI_FillKey()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtKey.Text))
            {
                this.Key = txtKey.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                errorProvider1.SetError(txtKey, "Không để trống trường này");
                txtKey.Focus();
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
