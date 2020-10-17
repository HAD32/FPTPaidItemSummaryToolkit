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

        public GUI_UpdateForm()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
