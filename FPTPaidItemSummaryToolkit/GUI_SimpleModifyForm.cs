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
        bool validateValue = false;
        public string newName { get; set; }
        public GUI_SimpleModifyForm(string oldName)
        {
            InitializeComponent();
            txtName.Text = oldName;
        }

        public GUI_SimpleModifyForm(string oldValue, bool validateValue)
        {
            InitializeComponent();
            txtName.Text = oldValue;
            this.validateValue = validateValue;
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
            if (validateValue)
            {
                try
                {
                    float testString = float.Parse(txtName.Text.Trim());
                    this.newName = txtName.Text;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Giá trị định mức chỉ được phép điền số. Xin hãy kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
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

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
    }
}
