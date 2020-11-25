using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using DAL;

namespace FPTPaidItemSummaryToolkit
{
    public partial class GUI_InsertForm : Form
    {
        List<AcademicLevel> academicLevelsList = new List<AcademicLevel>();

        public GUI_InsertForm(List<AcademicLevel> list)
        {
            InitializeComponent();
            academicLevelsList = list;

        }

        public GUI_InsertForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!txtName.Text.Trim().Equals("") && !txtCode.Text.Trim().Equals(""))
            {
                if (DAL_AcademicLevel.Instance.Insert(academicLevelsList, txtCode.Text, txtName.Text, txtDescription.Text))
                {
                    
                    MessageBox.Show("Thêm hệ đào tạo thành công!");

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thêm được hệ đào tạo");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
