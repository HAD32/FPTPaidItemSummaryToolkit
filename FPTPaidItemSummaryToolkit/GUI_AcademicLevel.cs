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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            AcademicLevel acLv = DAL_AcademicLevel.Instance.GetOneAcaLevelByCode(cbbAcaLevel.SelectedValue.ToString(), academicList);
            GUI_UpdateForm frmUp = new GUI_UpdateForm(academicList, acLv.Code, acLv.Name, acLv.Description);
            frmUp.FormClosed += Reload;
            frmUp.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            AcademicLevel acLv = DAL_AcademicLevel.Instance.GetOneAcaLevelByCode(cbbAcaLevel.SelectedValue.ToString(), academicList);
            DialogResult result = MessageBox.Show("Do you want to delete " + cbbAcaLevel.SelectedValue.ToString() + "?",
                                                "Confirmation Box", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DAL_AcademicLevel.Instance.Delete(academicList, acLv.Code);

            }
            DAL_DataSerializer.Instance.BinarySerialize(academicList, "AcademicLevels.sf");
            Reload();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
