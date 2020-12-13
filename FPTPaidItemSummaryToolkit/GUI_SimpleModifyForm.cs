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
    public partial class GUI_SimpleModifyForm : Form
    {
        public string newName { get; set; }
        public GUI_SimpleModifyForm(string oldName)
        {
            InitializeComponent();
            txtName.Text = oldName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Equals(""))
            {
                errorProvider1.Clear();
                errorProvider1.SetIconAlignment(this.txtName, ErrorIconAlignment.MiddleRight);
                errorProvider1.SetIconPadding(this.txtName, -20);
                errorProvider1.SetError(txtName, "Không để trống trường này");
                txtName.Focus();
                return;
            }
            else
            {
                this.newName = txtName.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
