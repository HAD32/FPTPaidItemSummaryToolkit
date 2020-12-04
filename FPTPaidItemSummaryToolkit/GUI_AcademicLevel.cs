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

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }
        //}

        void Reload()
        {
            try
            {
                academicList = (List<AcademicLevel>)DAL_DataSerializer.Instance.BinaryDeserialize("Academic Levels\\AcademicLevel.fs");
                listBox1.DataSource = academicList;
                listBox1.ValueMember = "Code";
                listBox1.DisplayMember = "Name";
                txtAcadLevelCode.Text = "";
                txtAcadLevelName.Text = "";
                txtDescription.Text = "";
            }
            catch (Exception)
            {
                academicList = new List<AcademicLevel>();
            }
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
            if (DAL_AcademicLevel.Instance.Update(academicList, txtAcadLevelCode.Text, txtAcadLevelName.Text, txtDescription.Text))
            {
                DAL_DataSerializer.Instance.BinarySerialize(academicList, "Academic Levels\\AcademicLevel.fs");
                MessageBox.Show("Sửa hệ đào tạo thành công");
            }
            else
            {
                MessageBox.Show("Không sửa được hệ đào tạo");
            }
            Reload();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            AcademicLevel acLv = DAL_AcademicLevel.Instance.GetOneAcaLevelByCode(listBox1.SelectedValue.ToString(), academicList);
            DialogResult result = MessageBox.Show("Bạn có muốn xóa " + listBox1.SelectedValue.ToString() + "?",
                                                "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                DAL_AcademicLevel.Instance.Delete(academicList, acLv.Code);

            }
            DAL_DataSerializer.Instance.BinarySerialize(academicList, "Academic Levels\\AcademicLevel.fs");
            Reload();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedValue != null)
            {
                string selectedAcLevel = listBox1.SelectedValue.ToString();

                foreach (AcademicLevel aclv in academicList)
                {
                    if (aclv.Code.Equals(selectedAcLevel))
                    {
                        txtAcadLevelCode.Text = aclv.Code;
                        txtAcadLevelName.Text = aclv.Name;
                        txtDescription.Text = aclv.Description;
                        break;
                    }
                }
            }
            else
            {
                return;
            }
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            txtAcadLevelCode.ReadOnly = true;
        }

        private void txtAcadLevelCode_TextChanged(object sender, EventArgs e)
        {
            if (!txtAcadLevelCode.Text.Equals(""))
            {
                btnAdd.Enabled = true;
            }
        }
    }
}
