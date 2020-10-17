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
    public partial class GUI_AcademicLevel : Form
    {
        List<AcademicLevel> academicList = new List<AcademicLevel>();

        public GUI_AcademicLevel()
        {
            InitializeComponent();
        }

        void Reload()
        {
            academicList.Clear();
            academicList = (List<AcademicLevel>)DAL_DataSerializer.Instance.BinaryDeserialize("AcademicLevels.sf");
            cbbAcaLevel.DataSource = academicList;
            cbbAcaLevel.ValueMember = "Code";
            cbbAcaLevel.DisplayMember = "Code";
            txtDetail.Text = "";
        }

        private void GUI_AcademicLevel_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void Reload(object sender, FormClosedEventArgs e)
        {
            Reload();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            GUI_InsertForm insertForm = new GUI_InsertForm(academicList);
            insertForm.FormClosed += Reload;
            insertForm.ShowDialog();
        }
    }
}
