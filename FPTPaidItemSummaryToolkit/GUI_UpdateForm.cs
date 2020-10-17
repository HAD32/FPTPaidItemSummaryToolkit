using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using DTO;

namespace FPTPaidItemSummaryToolkit
{
    public partial class GUI_UpdateForm : Form
    {

        public AcademicLevel acLv = new AcademicLevel();
        List<AcademicLevel> academicLevelsList = new List<AcademicLevel>();

        public GUI_UpdateForm(List<AcademicLevel> list, string code, string name, string description)
        {
            InitializeComponent();
            academicLevelsList = list;
            txtCode.Text = code;
            txtName.Text = name;
            txtDescription.Text = description;
        }

        public GUI_UpdateForm()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (DAL_AcademicLevel.Instance.Update(academicLevelsList, txtCode.Text, txtName.Text, txtDescription.Text))
            {
                DAL_DataSerializer.Instance.BinarySerialize(academicLevelsList, "AcademicLevels.sf");
                MessageBox.Show("Update successful");
                this.Close();
            }
            else
            {
                MessageBox.Show("Update fail");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
