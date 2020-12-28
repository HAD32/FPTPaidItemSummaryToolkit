using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FPTPaidItemSummaryToolkit
{
    public partial class GUI_AcademicLevelManagement : Form
    {
        List<AcademicLevel> academicList = new List<AcademicLevel>();

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        public GUI_AcademicLevelManagement()
        {
            InitializeComponent();
        }

        void Reload()
        {
            try
            {
                academicList = (List<AcademicLevel>)DAL_DataSerializer.Instance.BinaryDeserialize("Academic Levels\\AcademicLevel.fs");
                lstAcademicLevels.DataSource = academicList;
                lstAcademicLevels.ValueMember = "Code";
                lstAcademicLevels.DisplayMember = "Name";
                if (academicList is object && academicList.Count > 0)
                {
                    lstAcademicLevels.SelectedIndex = 0;
                    AcademicLevel academic = (AcademicLevel)lstAcademicLevels.SelectedItem;
                    txtAcadLevelCode.Text = academic.Code;
                    txtAcadLevelName.Text = academic.Name;
                    txtDescription.Text = academic.Description;
                }
                    
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
            if (lstAcademicLevels.SelectedItem is object)
            {
                AcademicLevel acLv = DAL_AcademicLevel.Instance.GetOneAcaLevelByCode(lstAcademicLevels.SelectedValue.ToString(), academicList);
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
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(lstAcademicLevels.SelectedItem is object)
            {
                AcademicLevel acLv = DAL_AcademicLevel.Instance.GetOneAcaLevelByCode(lstAcademicLevels.SelectedValue.ToString(), academicList);
                DialogResult result = MessageBox.Show("Bạn có muốn xóa " + lstAcademicLevels.SelectedValue.ToString() + "?",
                                                    "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DAL_AcademicLevel.Instance.Delete(academicList, acLv.Code);
                    DAL_DataSerializer.Instance.BinarySerialize(academicList, "Academic Levels\\AcademicLevel.fs");
                    Reload();
                    txtAcadLevelCode.Text = "";
                    txtAcadLevelName.Text = "";
                    txtDescription.Text = "";
                    lstCampusList.Items.Clear();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstAcademicLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lstAcademicLevels.SelectedItem is object)
            {
                AcademicLevel academic = (AcademicLevel)lstAcademicLevels.SelectedItem;
                txtAcadLevelCode.Text = academic.Code;
                txtAcadLevelName.Text = academic.Name;
                txtDescription.Text = academic.Description;
                lstCampusList.Items.Clear();
                if(academic.CampusList is object)
                {
                    foreach(Campus c in academic.CampusList)
                    {
                        lstCampusList.Items.Add(c);
                        lstCampusList.DisplayMember = "Name";
                    }
                }
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
            txtAcadLevelCode.ReadOnly = true;
        }

        private void txtAcadLevelCode_TextChanged(object sender, EventArgs e)
        {
            if (!txtAcadLevelCode.Text.Equals(""))
            {
                btnAdd.Enabled = true;
            }
        }

        private void btnAddCampus_Click(object sender, EventArgs e)
        {
            AcademicLevel academic = (AcademicLevel)lstAcademicLevels.SelectedItem;
            if (academic is null)
                return;
            if (string.IsNullOrWhiteSpace(txtAddCampus.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetIconAlignment(this.txtAddCampus, ErrorIconAlignment.MiddleRight);
                errorProvider1.SetIconPadding(this.txtAddCampus, -20);
                errorProvider1.SetError(txtAddCampus, "Không để trống trường này");
                txtAddCampus.Focus();
                return;
            }
            if(academic.CampusList is object)
            {
                if (academic.CampusList.Count > 0)
                {
                    foreach(Campus c in academic.CampusList)
                    {
                        if (c.Name.Equals(txtAddCampus.Text))
                        {
                            MessageBox.Show("Cơ sở này đã tồn tại.!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }
            else
            {
                academic.CampusList = new List<Campus>();
            }
            academic.CampusList.Add(new Campus("", txtAddCampus.Text, ""));
            DAL_DataSerializer.Instance.BinarySerialize(academicList, "Academic Levels\\AcademicLevel.fs");
            MessageBox.Show("Thêm cơ sở " + txtAddCampus.Text + " thành công.","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Reload();
            txtAddCampus.Text = "";
        }

        int indexItem;
        private void lstCampusList_Click(object sender, EventArgs e)
        {
            this.lstCampusList.MouseUp -= new System.Windows.Forms.MouseEventHandler(this.List_RightClick);
            this.lstCampusList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.List_RightClick);
        }

        private void List_RightClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                indexItem = this.lstCampusList.IndexFromPoint(e.Location);
                ContextMenuStrip cm = new ContextMenuStrip();
                cm.Items.Add("Đổi tên");
                cm.Items.Add("Xóa");
                cm.Show(this, this.PointToClient(MousePosition));
                cm.ItemClicked += new ToolStripItemClickedEventHandler(ModifyList);
            }
        }

        private void ModifyList(object sender, ToolStripItemClickedEventArgs e)
        {
            string selectedItem = e.ClickedItem.Text;
            AcademicLevel academic = (AcademicLevel)lstAcademicLevels.SelectedItem;
            switch (selectedItem)
            {
                case "Đổi tên":
                    Campus selectedCampus = (Campus)lstCampusList.SelectedItem;
                    GUI_SimpleModifyForm modForm = new GUI_SimpleModifyForm(selectedCampus.Name);
                    
                    DialogResult modResult = modForm.ShowDialog();
                    if (modResult == DialogResult.OK)
                    {
                        foreach (Campus c in academic.CampusList)
                        {
                            if (selectedCampus.Name.Equals(c.Name))
                            {
                                c.Name = modForm.newName;
                                MessageBox.Show("Đổi tên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                        }
                    }
                    break;
                case "Xóa":
                    DialogResult result = MessageBox.Show("Bạn có muốn xóa mục này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Campus deleteItem = (Campus)lstCampusList.SelectedItem;
                        foreach (Campus c in academic.CampusList)
                        {
                            if (deleteItem.Name.Equals(c.Name))
                            {
                                academic.CampusList.Remove(c);
                                MessageBox.Show("Xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                        }
                    }
                    break;
            }
            DAL_DataSerializer.Instance.BinarySerialize(academicList, "Academic Levels\\AcademicLevel.fs");
            Reload();
        }

        private void txtAddCampus_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
    }
}
