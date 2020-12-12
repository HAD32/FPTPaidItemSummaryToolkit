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
using System.IO;

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
                List<Campus> cl = new List<Campus>();
<<<<<<< HEAD
                if (DAL_AcademicLevel.Instance.Insert(academicLevelsList, txtCode.Text, cbbType.SelectedItem.ToString(), txtName.Text, txtDescription.Text, cl))   
=======
                if (DAL_AcademicLevel.Instance.Insert(academicLevelsList, txtCode.Text, txtName.Text, txtDescription.Text, cl))   
>>>>>>> fee946399c48462110513b1f74d3131016d4314d
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

        private void cbbType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
