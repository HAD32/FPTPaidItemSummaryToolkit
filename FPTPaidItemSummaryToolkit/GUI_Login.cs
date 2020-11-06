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
    
    public partial class GUI_Login : Form
    {
 
        private bool confirm { get; set; }
        public GUI_Login()
        {
            InitializeComponent();
            confirm = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtName.Text.Equals(""))
            {
                errorProvider1.SetError(txtName, "Không để trống trường này");
                txtName.Focus();
            }
            else if (txtCode.Text.Equals(""))
            {
                errorProvider1.SetError(txtCode, "Không để trống trường này");
                txtCode.Focus();
            }
            else
            {
                confirm = true;
                GUI_Container fcon = new GUI_Container(txtCode.Text);

                this.Hide();
                fcon.ShowDialog();
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
