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
            academicList = (List<AcademicLevel>)DAL_DataSerializer.Instance.BinaryDeserialize("AcademicLevels.sf");
            listBox1.DataSource = academicList;
            listBox1.ValueMember = "Code";
            listBox1.DisplayMember = "Code";
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
            AcademicLevel acLv = DAL_AcademicLevel.Instance.GetOneAcaLevelByCode(listBox1.SelectedValue.ToString(), academicList);
            GUI_UpdateForm frmUp = new GUI_UpdateForm(academicList, acLv.Code, acLv.Name, acLv.Description);
            frmUp.FormClosed += Reload;
            frmUp.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            AcademicLevel acLv = DAL_AcademicLevel.Instance.GetOneAcaLevelByCode(listBox1.SelectedValue.ToString(), academicList);
            DialogResult result = MessageBox.Show("Do you want to delete " + listBox1.SelectedValue.ToString() + "?",
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

        private void cbbAcaLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAcLevel = cbbAcaLevel.SelectedValue.ToString();
            foreach (AcademicLevel aclv in academicList)
            {
                if (aclv.Code.Equals(selectedAcLevel))
                {
                    txtDetail.Text = "Mã hệ đào tạo: " + aclv.Code + "\r\nTên hệ đào tạo: " + aclv.Name + "\r\nMô tả: " + aclv.Description;
                    break;
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAcLevel = listBox1.SelectedValue.ToString();
            foreach (AcademicLevel aclv in academicList)
            {
                if (aclv.Code.Equals(selectedAcLevel))
                {
                    txtDetail.Text = "Mã hệ đào tạo: " + aclv.Code + "\r\nTên hệ đào tạo: " + aclv.Name + "\r\nMô tả: " + aclv.Description;
                    break;
                }
            }
        }
    }
}
