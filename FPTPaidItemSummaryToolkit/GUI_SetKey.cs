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
    public partial class GUI_SetKey : Form
    {
        public string password { get; set; }
        public GUI_SetKey()
        {
            InitializeComponent();
        }

        private void btnUnhide_MouseDown(object sender, MouseEventArgs e)
        {
            txtKey.PasswordChar = '\0';
        }

        private void btnUnhide_MouseUp(object sender, MouseEventArgs e)
        {
            txtKey.PasswordChar = '*';
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtKey.Text))
            {
                password = txtKey.Text;
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
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
